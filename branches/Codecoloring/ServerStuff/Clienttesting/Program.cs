namespace Clienttesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    class Program
    {
        #region Methods
        public static void Recieve(object i)
        {
            byte[] e = new byte[1024];
            while (true)
            {
                ((System.Net.Sockets.Socket)i).Receive(e);
                Console.WriteLine(Encoding.ASCII.GetChars(e).);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Client is here.");
            System.Net.Sockets.Socket i = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
            Console.WriteLine("What IP address would you like to connect to?");
            i.Connect(new IPEndPoint(IPAddress.Parse(Console.ReadLine()), 6000));
            Console.WriteLine("Connected");
            System.Threading.Thread fun = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Recieve));
            fun.Start(i);
            while (true)
            {
                i.Send(Encoding.ASCII.GetBytes(Console.ReadLine().ToCharArray()));
            }
            i.Close();
        }

        #endregion Methods
    }
}