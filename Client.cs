using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal static class Program
    {
        private static void Main()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            const int PORT = 8005;
            var ipServer = new IPEndPoint(ip, PORT);
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipServer);
            var message = " ";
            string[] field = new string[9] {"0", "1", "2", "3", "4", "5", "6", "7", "8"};
            
            while (true)
            {
                ShowField(field);
               
                
                int bytes = 0;
                
                string msg;
                var dataSend = new byte[256];
                
                do
                {
                    bytes = server.Receive(dataSend);
                    msg =  Encoding.Unicode.GetString(dataSend, 0, bytes);
                } while (server.Available > 0);
                Console.WriteLine();
                Console.WriteLine("X PLAYER TURN IN CELL № - " +msg);
                int cell_num = Convert.ToInt32(msg);
                //string player = "X";
                
                UpdateField( cell_num, "X",  field);
                ShowField(field);
                
                
                
                
                message = Console.ReadLine();
                var data = Encoding.Unicode.GetBytes(message);
                server.Send(data);
                UpdateField( Convert.ToInt32(message), "O",  field);
                
                
            }
        }
        
        
        
        
        
        static void ShowField(string[] field)
        {
            for (int i = 0; i < field.Length; i++)
            {
                Console.Write("[ " + field[i] + " ]");
                if (i == 2 || i == 5)
                {
                    Console.WriteLine();
                }
            }
        }
        static void UpdateField(int cell_num, string player, string[] field)
        {
            for (int i = 0; i < field.Length; i++)
            {
                if (i == cell_num && player == "X")
                {
                    field[i] = player;
                }
                if (i == cell_num && player == "O")
                {
                    field[i] = player;
                }
            }
        }
        
        
        
        
    }
}
