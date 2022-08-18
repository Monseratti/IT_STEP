using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;

namespace CW_1608_Client
{
    internal class Program
    {
        static TcpClient client;
        static IPAddress _serverIP = IPAddress.Parse("127.0.0.1");
        static int _serverPort = 8084;

        static int _period = 0;

        static void Main(string[] args)
        {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            _period = int.Parse(ReceiveMessage().ToString());
            Timer timer = new Timer(SendScreenshots);
            timer.Change(0, _period);
            while (true) { }
        }

        private static object ReceiveMessage()
        {
            object o;
            try
            {
                client = new TcpClient();
                client.Connect(_serverIP, _serverPort);
                Console.WriteLine("Connected");
                NetworkStream nstream = client.GetStream();
                StreamReader sr = new StreamReader(nstream, Encoding.Unicode);
                o = sr.ReadToEnd();
                sr.Close();
                nstream.Close();
                client.Close();
                return o;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return 10000;
            }
        }

        private static void SendScreenshots(object o)
        {
            try
            {
                using (Bitmap _screenshot = new Bitmap(1000, 900))
                {

                    Size s = new Size(_screenshot.Width, _screenshot.Height);
                    Graphics g = Graphics.FromImage(_screenshot);
                    g.CopyFromScreen(0, 0, 0, 0, s);
                    MemoryStream ms = new MemoryStream();
                    _screenshot.Save(ms, ImageFormat.Jpeg);

                    client = new TcpClient();
                    client.Connect(_serverIP, _serverPort);
                    NetworkStream nstream = client.GetStream();
                    byte[] barray = ms.ToArray();
                    nstream.Write(barray, 0, barray.Length);
                    nstream.Close();
                    client.Close();
                }
            }
            catch (Exception Ex)
            {
            }
        }

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                //listener?.Stop();
            }
            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
    }
}
