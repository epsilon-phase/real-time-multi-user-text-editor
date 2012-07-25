namespace RealServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Server
    {
        #region Fields

        readonly OperationalTransform.TextTransformActor qw;

        //associates the client to a list of operations, allowing the server to not send the operations to the client that send the message in the first place;
        System.Collections.Generic.List<List<OperationalTransform.TextTransformActor>> absurdity;
        List<RealServer.SocketHandler.clienthandler> clients;
        System.Threading.Thread ClientSendthread;
        List<System.Threading.Thread> clientthreads;

        //OperationalTransform.TextTransformCollection operationslist;
        /// <summary>
        /// The socket used to accept clients on the server
        /// </summary>
        System.Net.Sockets.Socket serversock;

        #endregion Fields

        #region Constructors

        public Server()
        {
            clients = new List<SocketHandler.clienthandler>();
            clientthreads = new List<System.Threading.Thread>();
            serversock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
            //Bind to port 6000 and accept connections from anywhere
            serversock.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 6000));
            absurdity = new List< List<OperationalTransform.TextTransformActor>>();
            //operationslist=new OperationalTransform.TextTransformCollection();
            ClientSendthread = new System.Threading.Thread(new System.Threading.ThreadStart(this.Sendtoclient));
            qw=new OperationalTransform.TextTransformActor(" ").GetWithServerAlteration();
            ClientSendthread.Start();
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {
            System.Threading.Thread r = new System.Threading.Thread(new System.Threading.ThreadStart(connectionlistener));
            r.Start();
            this.ListenAndAdd();
        }

        private void connectionlistener()
        {
            while (true)
            {
                serversock.Listen(5);
                lock (clients)
                {
                    clients.Add(new SocketHandler.clienthandler(serversock.Accept()));
                }
                //Initialize the list for the client.
                absurdity.Add(new List<OperationalTransform.TextTransformActor>());
                //Add it to the list of threads.
                clientthreads.Add(new System.Threading.Thread(new System.Threading.ThreadStart(clients.Last<SocketHandler.clienthandler>().Start)));
                clientthreads[clientthreads.Count - 1].Start();
            }
        }

        /// <summary>
        /// Listen to the clients and add to the queue whenever necessary.
        /// </summary>
        private void ListenAndAdd()
        {
            OperationalTransform.TextTransformActor quick;
            while (true)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    while (clients[i]._clientdatareciever.processed.Count > 0)
                    {
                        //Attempt to remove an element from the top of the stack
                        if (clients[i]._clientdatareciever.processed.TryDequeue(out quick))
                        {
                            Console.WriteLine("Message properly Dequeued on server class listening thread.");
                            //
                            clients[i].AddMessage(quick);
                            //Add to the list of things recieved from the client.
                            absurdity[i].Add(quick);
                        }
                        else
                        {
                            Console.WriteLine("Message not properly Dequeued");
                        }
                    }
                    System.Threading.Thread.Sleep(3);
                }
            }
        }

        /// <summary>
        /// Continually enumerates through the clients and sends them packets that they don't have
        /// </summary>
        private void Sendtoclient()
        {
            while (true)
            {
                for (int a = 0; a < clients.Count; a++)
                {
                    try
                    {
                        //Send the client a space initializer, so that textcollection.initial!=null
                        if (absurdity[a].Count <= 0)
                        {
                            clients[a].AddMessage(qw);
                            absurdity[a].Add(qw);
                        }
                        for (int i = 0; i < absurdity[a].Count; i++)
                        {
                            for (int v = 0; v < clients.Count; v++)
                            {
                                //If the client does not have a given transform, send it to them and add it to
                                //their array.
                                if (!absurdity[v].Contains(absurdity[a][i]))
                                {
                                    clients[v].AddMessage(absurdity[a][i]);
                                    absurdity[v].Add(absurdity[a][i]);
                                    Console.WriteLine("Client has had message added correctly");
                                    Console.WriteLine("Client {0} added change to Client {1}", a, v);
                                }
                                else
                                {
                                    System.Threading.Thread.Sleep(5);
                                }
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException q) 
                    {
                        //Oh well.
                    }
                }
            }
        }

        #endregion Methods
    }
}