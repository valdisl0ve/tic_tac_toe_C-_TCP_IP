using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {


            
            var ip = IPAddress.Parse("127.0.0.1");
            var port = 8005;
            var ipServer = new IPEndPoint(ip, port);

            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipServer);

            
            string[] field = new string[9] {" ", " ", " ", " ", " ", " ", " ", " ", " "};
            int count = 0;
            
            while (true)
            {
            
                
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
                
                
            string message = Console.ReadLine();
            var data = Encoding.Unicode.GetBytes(message);
            server.Send(data);
            
            var dataSend = new byte[256];
            
            
            
            
            do
            {
                
                var bytes = server.Receive(data);
             
                
                
            } while (server.Available > 0);
            
            Console.Clear();


            int place = Convert.ToInt32(message);

            for (int i = 0; i < 9; i++)
            {
                if (i == place)
                {
                    field[i] = "X";
                }
            }
            
            
            
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
            
            
            Console.WriteLine(message + "THIS IS YOUR TURN");
            }
            
            
            
            
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            

           
        }
    }
}