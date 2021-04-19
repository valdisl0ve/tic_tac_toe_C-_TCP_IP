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
            
            while (true)
            {
                message = Console.ReadLine();
                var data = Encoding.Unicode.GetBytes(message);
                server.Send(data);
                int bytes = 0;

                string msg;
                var dataSend = new byte[256];
                
                do
                {
                     bytes = server.Receive(data, data.Length, 0);
                    msg =  Encoding.Unicode.GetString(dataSend, 0, bytes);
                } while (server.Available > 0);
                
                Console.WriteLine(msg);
            }
        }
    }
}
