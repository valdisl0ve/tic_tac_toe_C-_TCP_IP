using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static string[] field = new string[9] {" ", " ", " ", " ", " ", " ", " ", " ", " "};
        
        public static void Main(string[] args)
        {


          



          
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            
            int port = 8005;
            
            IPEndPoint ip_server = new IPEndPoint(ip, port);

            Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            listen.Bind(ip_server);
            
            
            listen.Listen(10);
            

            int count = 0;

            var clients = new Dictionary<int, Socket>();
            
           
            
            
            while (true)
            {
                Socket client = listen.Accept();
                count++;
                clients.Add(count, client);
                var count1 = count;
                var task = Task<string>.Run(() => NewClient(client, count1));
                var message = task.Result;
                
                
                byte[] data_send = Encoding.Unicode.GetBytes(message);
                
                foreach (var item in clients)
                {
                    item.Value.Send(data_send);
                }



            }
            
      
        }


        
        
        
        
        
        
        
     
        
        static string NewClient(Socket client, int id)
        {

            string message;
           // StringBuilder message = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[256];

         

            while (true)
            {

                message = "";
                
                do
            {

                bytes = client.Receive(data);
                
                message = message + Encoding.Unicode.GetString(data, 0, bytes);
                
                int place = Convert.ToInt32(message);

                for (int i = 0; i < 9; i++)
                {
                    if (i == place)
                    {
                        field[i] = "x";
                    }
                }
                

            } while (client.Available > 0);

                
                var temp = $"#{id} {DateTime.Now:u}: {message}";
                Console.WriteLine(temp);
                
                
                int count = 0;
                foreach (var el in field)
                {
                    Console.Write(" ["+ el + "] ");
                    count++;
                    if (count == 3 )
                    {
                        Console.WriteLine();
                        count = 0;
                    }
                }
                
                return temp;

            string msg = "MESSAGE Received";
            byte[] data_send = Encoding.Unicode.GetBytes(msg);
            client.Send(data_send);
            }
            
            
                
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            
            
        }
        
       
    }
}