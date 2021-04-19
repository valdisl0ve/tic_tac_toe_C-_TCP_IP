using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    
    internal static class Program
    { 
        static int global;
        static Socket opponent;
        private static void Main()
        {
            string[] field = new string[9] {"0", "1", "2", "3", "4", "5", "6", "7", "8"};
            var ip = IPAddress.Parse("127.0.0.1");
            const int PORT = 8005;
            var ipServer = new IPEndPoint(ip, PORT);
            Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listen.Bind(ipServer);
            listen.Listen(10);
            
           
            
            
            
            //Task.Run(() => MainTask(listen));
            
            string flag = "X";
            string winer = " ";
            ShowField(field);
            var client = listen.Accept();
            opponent = client;
            
           // Task.Run(() => NewClient(client));
            
            do
            {
                if (flag == "X")
                {
                    
                    Console.WriteLine();
                    Console.WriteLine("YOUR TURN [X] PLAYER, ENTER THE NUM OF CELL");
                    int cell_num = Convert.ToInt32(Console.ReadLine());
                    var data = Encoding.Unicode.GetBytes(Convert.ToString(cell_num));
                   
                    opponent.Send(data);
                    
                    
                    UpdateField(cell_num, flag, field);
                    ShowField(field);
                    
                    if (WinCondition(flag, field) == false)
                    {flag = "O";}
                    else
                    {winer = flag; }
                    
                }
                
                
               
              
                
                
                if (flag == "O")
                {
                    Console.WriteLine();
                    Console.WriteLine("YOUR TURN [O] PLAYER, ENTER THE NUM OF CELL");
                    string message;
                    var data1 = new byte[256];
                    do
                    {
                        var bytes = client.Receive(data1);
                        message = Encoding.Unicode.GetString(data1, 0, bytes);
                    } while (client.Available > 0);
                    var temp = "PLAYER [O] TURN IN CELL № - " + message ;
                    Console.WriteLine(temp);
                    global = Convert.ToInt32(message);
                    
                    
                    Console.ReadKey();
                    int cell_num = global;
                    UpdateField(cell_num, flag, field);
                    ShowField(field);
                    
                    if (WinCondition(flag, field) == false)
                    {flag = "X";}
                    else 
                    {winer = flag;}
                }
            } while (winer == " ");
            
            Console.WriteLine();
            Console.WriteLine("ENDGAME");
            Console.WriteLine(flag + " - WINNER");
            
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
      
        
        
        
        static bool WinCondition(string symbol, string[] field)
        {

            if (
                (field[0] == symbol && field[1] == symbol && field[2] == symbol) ||
                (field[3] == symbol && field[4] == symbol && field[5] == symbol) ||
                (field[6] == symbol && field[7] == symbol && field[8] == symbol) ||

                (field[0] == symbol && field[3] == symbol && field[6] == symbol) ||
                (field[1] == symbol && field[4] == symbol && field[7] == symbol) ||
                (field[2] == symbol && field[5] == symbol && field[8] == symbol) ||

                (field[0] == symbol && field[4] == symbol && field[8] == symbol) ||
                (field[6] == symbol && field[4] == symbol && field[2] == symbol)

            )
            {
                return true;
            }
                
            
            
            else {return false;}
            
            
            
        } 
        
    }
}
