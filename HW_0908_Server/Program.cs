using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_0908_Server.Classes;
using MyStreet;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HW_0908_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Bind(iPEndPoint);
            socket.Listen(10);
            try
            {
                while (true)
                {
                    Socket header = socket.Accept();
                    byte[] data = new byte[1024];
                    int bytes = 0;
                    string message = "";
                    do
                    {
                        bytes = header.Receive(data);
                        message = Encoding.Unicode.GetString(data,0,bytes);
                    } while (header.Available>0);
                    Console.WriteLine(DateTime.Now + ", " + header.RemoteEndPoint.ToString() + ": " + message);
                    int postID = int.Parse(message);
                    List<Streets> streets = new List<Streets>();
                    using(StreetContext db = new StreetContext())
                    {
                        streets = db.Streets.Where(o=>o.postID == postID).ToList();
                    }
                    string json = JsonConvert.SerializeObject(streets);
                    header.Send(Encoding.Unicode.GetBytes(json));

                    header.Shutdown(SocketShutdown.Both);
                    header.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": " +  ex.Message);
            }
        }
    }
}
