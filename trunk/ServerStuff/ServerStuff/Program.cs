namespace ServerStuff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    #region Delegates

    delegate void stuffs();

    #endregion Delegates

    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            //socket used to accept connections
            System.Net.Sockets.Socket g = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
            g.Bind(new IPEndPoint(IPAddress.Any, 6000));
            Console.WriteLine("Server started at {0}", g.LocalEndPoint);
            List<something> clients = new List<something>();
            List<System.Threading.Thread> clienthreads = new List<System.Threading.Thread>();

            //
            stuffs messagehandling = delegate()
            {
                Queue<string> pendingmessage = new Queue<string>();
                while (true)
                {

                    for (int i = 0; i < clients.Count; i++)
                    {
                        lock (clients[i])
                        {
                            if (clients[i].i.message.Count >= 0)
                            {
                                while (clients[i].i.message.Count > 0)
                                {
                                    pendingmessage.Enqueue(clients[i].i.message.Dequeue());
                                }
                            }//put strings from it into the class

                        }
                    }
                    for (int i = 0; i < clients.Count; i++)
                    {
                        foreach (string v in pendingmessage)
                        {
                            //Console.WriteLine("Adding {0} to Client at {1}", v, clients[i].getipaddress().Address);
                                clients[i].messagestosend.Enqueue(v);
                        }
                    }
                    pendingmessage.Clear();
                }
            };
            System.Threading.Thread handler = new System.Threading.Thread(new System.Threading.ThreadStart(messagehandling));
            while (true)
            {
                g.Listen(5);//the worst way to do threading
                clients.Add(new something(g.Accept()));
                clienthreads.Add(new System.Threading.Thread(new System.Threading.ThreadStart(clients.Last<something>().Startit)));
                clienthreads.Last<System.Threading.Thread>().Start();
                if (handler.ThreadState != System.Threading.ThreadState.Running)
                {
                    handler.Start();
                }
            }
        }

        #endregion Methods
    }

    class something
    {
        #region Fields

        /// <summary>
        /// Reception object for given client
        /// </summary>
        public somethingrecieve i;
        public bool isrunning;

        /// <summary>
        /// Queue of messages to send
        /// </summary>
        public Queue<string> messagestosend;

        System.Threading.Thread q;
        private System.Net.Sockets.Socket sockety;

        #endregion Fields

        #region Constructors

        public something(System.Net.Sockets.Socket p)
        {
            messagestosend = new Queue<string>();
            sockety = p;
            isrunning = false;
            i = new somethingrecieve(p);
            q = new System.Threading.Thread(new System.Threading.ThreadStart(i.startstuff));
            //start thread for listening, this is already the thread for sending
        }

        #endregion Constructors

        #region Methods

        public IPEndPoint getipaddress()
        {
            return (IPEndPoint)sockety.LocalEndPoint;
        }

        public void Startit()
        {
            q.Start();
            Console.WriteLine("Client at {0} connected.", sockety.LocalEndPoint.ToString());
            isrunning = true;
            while (true)
            {
                    if (messagestosend.Count>0){//remove send message and remove from queue
                        Console.WriteLine("Sending message {0}",messagestosend.Peek());
                        sockety.Send(Encoding.ASCII.GetBytes(messagestosend.Dequeue().ToCharArray()));
                    }
            }
        }

        #endregion Methods
    }

    class somethingrecieve
    {
        #region Fields

        /// <summary>
        /// message queue for received packets
        /// </summary>
        public Queue<string> message;
        public int messageno
        {
            get
            {
                return message.Count;
            }
        }

        System.Net.Sockets.Socket y;

        #endregion Fields

        #region Constructors

        public somethingrecieve(System.Net.Sockets.Socket e)
        {
            message = new Queue<string>();
            y = e;
        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Start the Client Receiver
        /// </summary>
        public void startstuff()
        {
            //max byte length of the reciever is 1024
            byte[] j = new byte[1024];
            while (true)
            {
                try
                {
                    y.Receive(j);
                    //Encode the recieved message into a string, trim, and push onto queue
                    message.Enqueue(Encoding.ASCII.GetString(j).Trim());

                    //write the recently recieved message without removing it from the queue
                    Console.WriteLine(message.Peek());
                    
                }
                catch (System.Net.Sockets.SocketException p)
                {
                    return;
                }
            }
        }

        #endregion Methods
    }
}