namespace RealServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        #region Fields

        static ChatServerLib.ChatServer slerver;

        #endregion Fields

        #region Methods

        static void Main(string[] args)
        {
            Console.WriteLine("Server started on Localhost on port 6000");
            ChatServerLib.MessageRecievedListener e = new ChatServerLib.MessageRecievedListener(Program.Messagerthingy);
            slerver = new ChatServerLib.ChatServer(3410);
            slerver.start(e);
            //ChatServerLib.ChatServer tr = new ChatServerLib.ChatServer(1234);
            Server y = new Server();
            y.Start();
            Console.WriteLine("Listening for client");
        }

        static void Messagerthingy(string message)
        {
            Program.slerver.broadcast(message);
        }

        #endregion Methods
    }
}