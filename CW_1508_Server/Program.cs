using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace CW_1508_Server
{
    internal class Program
    {
        static int _stateServer = -1;
        static int LocalPort = 8081;
        static int RemovePort = 8082;
        static string RemoveIP = "127.0.0.1";
        static void Main(string[] args)
        {
            while (true)
            {

                while (_stateServer == -1) _stateServer = int.Parse(ReceiveMessage().ToString());
                Console.WriteLine("Start work");
                while (_stateServer == 0)
                {
                    int _isTimer = -1;
                    while (_isTimer == -1)
                    {
                        _isTimer = int.Parse(ReceiveMessage().ToString());
                    }
                    if (_isTimer == 0)
                    {
                        Console.WriteLine("as timer");
                        try
                        {
                            Timer _timer = new Timer(TimerCallBack);
                            _timer.Change(0, 5000);
                            while (_stateServer == 0) { _stateServer = int.Parse(ReceiveMessage().ToString()); }
                            _timer.Dispose();
                            Console.WriteLine("end work");
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
                        while (_stateServer == 0)
                        {
                            Console.WriteLine("as reminger");
                            int _messageReceive = -1;
                            string _reminger = string.Empty;
                            string _timeReminger = string.Empty;
                            SendMessage("What do you want to reminger: ");
                            while (_reminger == string.Empty) _reminger = ReceiveMessage().ToString();
                            SendMessage("When you want to teminger (Date/time): ");
                            while (_timeReminger == string.Empty) _timeReminger = ReceiveMessage().ToString();
                            _reminger = $"You asked to be reminded at {_timeReminger} next message: {_reminger}\n";
                            TimeSpan remingerTimeSpan = DateTime.Parse(_timeReminger) - DateTime.Now;
                            try
                            {
                                Timer _timer = new Timer(RemingerCallBack, _reminger, 0, 0);
                                _timer.Change((int)remingerTimeSpan.TotalMilliseconds, 0);
                                while (_messageReceive == -1)
                                {
                                    _messageReceive = int.Parse(ReceiveMessage().ToString());
                                }
                                SendMessage("0");
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

                }
            }
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

        private static void TimerCallBack(object o)
        {
            SendMessage(DateTime.Now.ToString("h:mm:ss"));
        }

        private static void RemingerCallBack(object o)
        {
            SendMessage(o);
        }
    }
}
