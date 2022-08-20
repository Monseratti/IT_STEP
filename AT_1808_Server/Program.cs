using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AT_1808_Server
{
    internal class Program
    {
        static Thread Sender = new Thread(new ThreadStart(multicastSend));
        static int Interval = 1000;
        static string message = "";
        static int LocalPort = 7777;
        static IPEndPoint dest = null;
        static Thread listen = new Thread(Listener);


        static void Main(string[] args)
        {
            Sender.IsBackground = true;
            listen.IsBackground = true;
            Sender.Start();
            listen.Start();
            Console.ReadLine();
        }

        static void Listener()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        UdpClient uClient = new UdpClient(LocalPort);
                        IPEndPoint ipEnd = null;
                        byte[] responce = uClient.Receive(ref ipEnd);
                        string strResult = Encoding.Unicode.GetString(responce);
                        message = strResult;
                        Console.Write($"{message} - ");
                        uClient.Close();
                    }
                }
                catch (SocketException sockEx)
                {
                    Console.WriteLine("Ошибка сокета:" + sockEx.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка:" + ex.Message);
                }
            }
        }
        static void multicastSend()
        {
            while (true)
            { 
                if (message != string.Empty)
                {
                    //Thread.Sleep(Interval);
                    Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
                    IPAddress dest = IPAddress.Parse("224.5.5.5");
                    soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(dest));
                    IPEndPoint ipep = new IPEndPoint(dest, 8080);
                    soc.Connect(ipep);
                    soc.Send(Encoding.Unicode.GetBytes(message));
                    Console.WriteLine("send");
                    soc.Close();
                    message = string.Empty;
                }
            }
        }
    }
}


