using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HW_0908_Server.Classes
{
    public class Server
    {
        IPEndPoint endP;
        Socket socket;
        public Server(string strAddr, int port)
        {
            endP = new IPEndPoint(IPAddress.Parse(strAddr), port);
        }
        void MyAcceptCallbakFunction(IAsyncResult ia)
        {
            Socket socket = (Socket)ia.AsyncState;
            Socket ns = socket.EndAccept(ia);
            Console.WriteLine(ns.RemoteEndPoint.ToString());
            byte[] sendBufer = System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToString());
            ns.BeginSend(sendBufer, 0, sendBufer.Length, SocketFlags.None, new AsyncCallback(MySendCallbackFunction), ns);
            socket.BeginAccept(new AsyncCallback(MyAcceptCallbakFunction), socket);
        }
        void MySendCallbackFunction(IAsyncResult ia)
        {
            Socket ns = (Socket)ia.AsyncState;
            int n = ((Socket)ia.AsyncState).EndSend(ia);
            ns.Shutdown(SocketShutdown.Send);
            ns.Close();
        }
        public void StartServer()
        {
            if (socket != null)
                return;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endP);
            socket.Listen(10);
            socket.BeginAccept(new AsyncCallback(MyAcceptCallbakFunction), socket);
        }
    }
}
}
