using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Class_User;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace T_T_T_Server
{
    internal class Program
    {
        static IPEndPoint IPEndPoint = null;
        static int gameID = 0;
        static string UserName { get; set; }
        static List<Game> Games { get; set; }
        static List<int> Ports { get; set; }

        static void Main(string[] args)
        {
            Games = new List<Game>();
            Ports = new List<int>()
            {
                8000,8001,8002,8003,8004
            };
            Thread thread = new Thread(UDP_Receive) { IsBackground = true };
            thread.Start();
            Console.ReadLine();
        }

        static void UDP_Receive()
        {
            while (true)
            {
                try
                {
                    UdpClient udpClient = new UdpClient(8081);
                    byte[] responce = udpClient.Receive(ref IPEndPoint);
                    string resp = Encoding.Unicode.GetString(responce);
                    string[] arr_resp = resp.Split(';');
                    UserName = arr_resp[0];
                    IPEndPoint.Port = int.Parse(arr_resp[1]);
                    Console.WriteLine($"[{DateTime.Now}][{IPEndPoint.Address}:{IPEndPoint.Port}]: {UserName} connected");
                    udpClient?.Close();
                    InitializeUser();
                }
                catch (SocketException se)
                {
                    Console.WriteLine($"Socket error: {se.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void InitializeUser()
        {
            try
            {
                UdpClient udpClient = new UdpClient();
                User newUser = new User() { Name = UserName, GameID = gameID };
                TryConnectGame(ref newUser);
                string json = JsonConvert.SerializeObject(newUser);
                byte[] buffer = Encoding.Unicode.GetBytes(json);
                udpClient.Send(buffer, buffer.Length, IPEndPoint);
                udpClient?.Close();
                Console.WriteLine($"[{DateTime.Now}][{IPEndPoint.Address}:{IPEndPoint.Port}]: Send parametres for {UserName}");
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Socket error: {se.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void TryConnectGame(ref User newUser)
        {
            try
            {
                Games[gameID].User_Two = newUser;
                newUser.GamePort = Games[gameID].GamePort;
                newUser.Mark = "O";
                gameID++;
            }
            catch (Exception)
            {
                Games.Add(new Game() { Id = gameID, GamePort = Ports[gameID], User_One = newUser });
                newUser.GamePort = Ports[gameID];
                newUser.Mark = "X";
            }
        }
    }
}
