namespace RealServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

 
    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            //ChatServerLib.ChatServer tr = new ChatServerLib.ChatServer(1234);
            Server y = new Server();
            y.Start();
        }
        #endregion Methods
    }

   
}