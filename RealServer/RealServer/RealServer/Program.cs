namespace RealServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

 
    class Program
    {
        #region Methods
        static void Messagerthingy(string message) 
        {
            Program.slerver.broadcast(message);
        }
        static ChatServerLib.ChatServer slerver;
        static void Main(string[] args)
        {

            ChatServerLib.MessageRecievedListener e = new ChatServerLib.MessageRecievedListener(Program.Messagerthingy);
            slerver = new ChatServerLib.ChatServer(3410);
            slerver.start(e);
            //ChatServerLib.ChatServer tr = new ChatServerLib.ChatServer(1234);
            Server y = new Server();
            y.Start();
        }
        #endregion Methods
    }

   
}