using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CW_0808
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(address, 8081);
            Socket socket = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.IP);
            socket.Bind(endPoint);
            socket.Listen(10);
            try
            {
                while (true)
                {
                    Socket handler = socket.Accept();
                    Console.WriteLine(handler.RemoteEndPoint.ToString());
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; 
                    byte[] data = new byte[1024];

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    Console.WriteLine(DateTime.Now + ": " + builder.ToString());
                    handler.Send(Encoding.Unicode.GetBytes($"{DateTime.Now}"));
                    handler.Shutdown(SocketShutdown.Send);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
