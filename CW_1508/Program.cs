using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CW_1508
{
    internal class Program
    {
        static int LocalPort = 8082;
        static int RemovePort = 8081;
        static string RemoveIP = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Write("1(reminger)/0(timer): ");
            int _isTimer = int.Parse(Console.ReadLine());
            SendMessage(_isTimer.ToString());
            if (_isTimer == 0)
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine(ReceiveMessage().ToString());
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
            else if (_isTimer == 1)
            {
                while (true)
                {
                    int _messageReceive = -1;
                    Console.WriteLine(ReceiveMessage().ToString());
                    SendMessage(Console.ReadLine());
                    Console.WriteLine(ReceiveMessage().ToString());
                    SendMessage(Console.ReadLine());
                    while (_messageReceive == -1)
                    {
                        Console.WriteLine(ReceiveMessage().ToString());
                        SendMessage("0");
                        _messageReceive = int.Parse(ReceiveMessage().ToString());
                    }
                }
            }
        }

        private static object ReceiveMessage()
        {
            object o;
            UdpClient uClient = new UdpClient(LocalPort);
            IPEndPoint ipEnd = null;
            byte[] responce = uClient.Receive(ref ipEnd);
            o = Encoding.Unicode.GetString(responce);
            uClient.Close();
            return o;
        }

        private static void SendMessage(object message)
        {
            string _message = message.ToString();
            IPAddress iPAddress = IPAddress.Parse(RemoveIP);
            UdpClient uClient = new UdpClient();
            IPEndPoint ipEnd = new IPEndPoint(iPAddress, RemovePort);
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(_message);
                uClient.Send(bytes, bytes.Length, ipEnd);
            }
            catch (SocketException sockEx)
            {
                Console.WriteLine("Ошибка сокета:" + sockEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка:" + ex.Message);
            }
            finally
            {
                uClient.Close();
            }
        }
    }
}
