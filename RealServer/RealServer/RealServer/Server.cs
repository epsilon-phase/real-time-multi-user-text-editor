namespace RealServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    class Server
    {
        #region Fields

        //associates the client to a list of operations, allowing the server to not send the operations to the client that send the message in the first place;
        System.Collections.Generic.Dictionary<int, List<OperationalTransform.TextTransformActor>> absurdity;
        List<RealServer.SocketHandler.clienthandler> clients;
        List<System.Threading.Thread> clientthreads;
        //OperationalTransform.TextTransformCollection operationslist;
        /// <summary>
        /// The socket used to accept clients on the server
        /// </summary>
        System.Net.Sockets.Socket serversock;
        System.Threading.Thread ClientSendthread;
        readonly OperationalTransform.TextTransformActor qw;
        #endregion Fields

        #region Constructors

        public Server()
        {
            clients = new List<SocketHandler.clienthandler>();
            clientthreads = new List<System.Threading.Thread>();
            serversock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
            //Bind to port 6000 and accept connections from anywhere
            serversock.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 6000));
            absurdity = new Dictionary<int, List<OperationalTransform.TextTransformActor>>();
            //operationslist=new OperationalTransform.TextTransformCollection();
            ClientSendthread = new System.Threading.Thread(new System.Threading.ThreadStart(this.Sendtoclient));
            qw=new OperationalTransform.TextTransformActor(" ").GetWithServerAlteration();
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {
            System.Threading.Thread r = new System.Threading.Thread(new System.Threading.ThreadStart(connectionlistener));
            r.Start();
            this.ListenAndAdd();
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
                    //Send the client a space initializer, so that textcollection.initial!=null
                    if (absurdity[a].Count <= 0)
                    {
                        clients[a].AddMessage(qw);
                        absurdity[a].Add(qw);
                    }
                    foreach (OperationalTransform.TextTransformActor i in absurdity[a])
                    {
                        for (int v = 0; v < clients.Count; v++)
                        {
                            //If the client does not have a given transform, send it to them and add it to
                            //their array.
                            if (!absurdity[v].Contains(i))
                            {
                                clients[v].AddMessage(i);
                                absurdity[v].Add(i);
                                Console.WriteLine("Client has had message added correctly");
                                Console.WriteLine("Client {0} added change to Client {1}", a, v);
                            }
                            else
                            {
                                Console.WriteLine("Client already contains message");
                            }
                        }
                    }
                }
                
            }
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
                absurdity[clients.Count - 1] = new List<OperationalTransform.TextTransformActor>();
                //Add it to the list of threads.
                clientthreads.Add(new System.Threading.Thread(new System.Threading.ThreadStart(clients.Last<SocketHandler.clienthandler>().Start)));
                clientthreads[clientthreads.Count - 1].Start();
            }
        }

        #endregion Methods
    }
}