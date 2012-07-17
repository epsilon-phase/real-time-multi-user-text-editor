namespace RealServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace SocketHandler
    {
        using System.Net.Sockets;

        class receptionhandler
        {
            private Socket _recptionsocket;

            public receptionhandler(Socket p)
            {
                _recptionsocket = p;
                this.messages = new Queue<byte[]>();
                processed = new Queue<OperationalTransform.TextTransformActor>();
            }

            Queue<byte[]> messages;
            public Queue<OperationalTransform.TextTransformActor> processed;

            public void StartListening()
            {
                byte[] buffer = new byte[1024];
                System.Threading.Thread r=new System.Threading.Thread(new System.Threading.ThreadStart(this.ProcessPacket));
                while (true)
                {
                    _recptionsocket.Receive(buffer);
                    //lock messages, ensuring that it is not written to otherwise
                    messages.Enqueue(buffer);
                    r.Start();
                }
            }

            public void ProcessPacket()
            {
                OperationalTransform.TextTransformActor e=OperationalTransform.TextTransformActor.GetObjectFromBytes(this.messages.Dequeue());
                e.AlterforServer();
                processed.Enqueue(e);
            }
        }

        class clienthandler
        {
            public clienthandler(Socket p)
            {
                _transmissionsocket = p;
                _clientdatareciever = new receptionhandler(p);
                _running = false;
            }

            System.Threading.Thread clientdatarecp;
            /// <summary>
            /// Status of the thread. Returns whether or not the "Start" function has concluded.
            /// </summary>
            public bool Running
            {
                get
                {
                        return _running;
                }
            }

            private bool _running;

            public void Start()
            {
                Console.WriteLine("Client at {0} has connected to the server",this._transmissionsocket.LocalEndPoint);
                _running = true;
                clientdatarecp = new System.Threading.Thread(new System.Threading.ThreadStart(_clientdatareciever.StartListening));
                clientdatarecp.Start();
                try
                {
                    while (true)
                    {
                        lock (_pendingmessage)
                        {
                            if (_pendingmessage.Count > 0)
                                _transmissionsocket.Send(_pendingmessage.Dequeue());
                        }
                    }
                }
                catch (SocketException r)
                {
                    Console.WriteLine("Client at {0} Dropped connection.",_transmissionsocket.LocalEndPoint);
                    _running=false;
                    clientdatarecp.Abort();
                    clientdatarecp=null;
                }
            }

            public DateTimeOffset fixer;
            public receptionhandler _clientdatareciever;
            Socket _transmissionsocket;
            Queue<byte[]> _pendingmessage;

            /// <summary>
            /// Add Message to clienthandler with proper thread locking
            /// </summary>
            /// <param name="message">Message to Send</param>
            public void AddMessage(OperationalTransform.TextTransformActor message)
            {
                lock (_pendingmessage)
                {
                    _pendingmessage.Enqueue(OperationalTransform.TextTransformActor.GetObjectInBytes(message));
                }
            }
        }
    }

    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
        }

        #endregion Methods
    }

    class Server
    {
        #region Fields

        System.Collections.Generic.Dictionary<RealServer.SocketHandler.clienthandler, List<OperationalTransform.TextTransformActor>> absurdity;
        List<RealServer.SocketHandler.clienthandler> clients;
        List<System.Threading.Thread> clientthreads;
        System.Net.Sockets.Socket serversock;
        System.Threading.Thread timesyncthread;
        OperationalTransform.TextTransformCollection operationslist;
        #endregion Fields
        #region Constructors

        public Server()
        {
            clients = new List<SocketHandler.clienthandler>();
            clientthreads = new List<System.Threading.Thread>();
            serversock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IP);
            serversock.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 6000));
            absurdity = new Dictionary<SocketHandler.clienthandler, List<OperationalTransform.TextTransformActor>>();
            operationslist=new OperationalTransform.TextTransformCollection();
        }
        
        #endregion Constructors

        #region Methods

        public void Start()
        {
            System.Threading.Thread r = new System.Threading.Thread(new System.Threading.ThreadStart(connectionlistener));
            
            r.Start();
            while (true)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    while(clients[i]._clientdatareciever.processed.Count>1)
                    {
                        lock(clients[i]._clientdatareciever.processed)
                            this.operationslist.Add(clients[i]._clientdatareciever.processed.Dequeue());
                    }
                }
            }
        }

        private void connectionlistener()
        {
            while (true)
            {
                serversock.Listen(5);
                clients.Add(new SocketHandler.clienthandler(serversock.Accept()));
                clientthreads.Add(new System.Threading.Thread(new System.Threading.ThreadStart(clients.Last<SocketHandler.clienthandler>().Start)));
                clientthreads[clientthreads.Count - 1].Start();
            }
        }

        #endregion Methods
    }
}