using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goods;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CW_0908_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Goods.Goods> MyGoods = new List<Goods.Goods>()
            {
                new Goods.Goods(){ ID = 0, Name = "test", Manufacturer = "Test", Price = 123},
                new Goods.Goods(){ ID = 1, Name = "test1", Manufacturer = "Test1", Price = 312},
                new Goods.Goods(){ ID = 2, Name = "test2", Manufacturer = "Test2", Price = 321},
                new Goods.Goods(){ ID = 3, Name = "test3", Manufacturer = "Test3", Price = 132},
                new Goods.Goods(){ ID = 4, Name = "test4", Manufacturer = "Test4", Price = 231},
            };
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 8080);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Bind(iPEndPoint);
            socket.Listen(10);
            try
            {
                while (true)
                {
                    Socket handler = socket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[1024]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    int ID = int.Parse(builder.ToString());
                    Goods.Goods tmp = MyGoods.Where(o => o.ID == ID).First();
                    string json = JsonConvert.SerializeObject(tmp);
                    data = Encoding.Unicode.GetBytes(json);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
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
