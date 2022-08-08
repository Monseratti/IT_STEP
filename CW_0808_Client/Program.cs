using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CW_0808_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 8081);
            Console.WriteLine($"{address}");
            Console.WriteLine($"{endPoint}");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Console.Write("Input string: ");
            string tmp = Console.ReadLine();
            try
            {
                socket.Connect(endPoint);
                if (socket.Connected)
                {
                    socket.Send(Encoding.Unicode.GetBytes(tmp));
                    byte[] buff = new byte[1024];
                    string str = "";
                    int len = 0;
                    do
                    {
                        len = socket.Receive(buff);
                        str += Encoding.Unicode.GetString(buff, 0, len);
                    } while (len > 0);
                    Console.WriteLine($"Received message:\n{str}");
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // socket.Shutdown(SocketShutdown.Both);
            }
            Console.ReadKey();
        }
    }
}
