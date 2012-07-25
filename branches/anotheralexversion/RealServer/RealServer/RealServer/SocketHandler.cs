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
                processed = new System.Collections.Concurrent.ConcurrentQueue<OperationalTransform.TextTransformActor>();
            }

            Queue<byte[]> messages;
            public System.Collections.Concurrent.ConcurrentQueue<OperationalTransform.TextTransformActor> processed;

            public void StartListening()
            {
                Console.WriteLine("Listening to client");
                byte[] buffer = new byte[1024];
                System.Threading.Thread r = new System.Threading.Thread(new System.Threading.ThreadStart(this.ProcessPacket));
                r.Start();
                while (true)
                {
                    try
                    {
                        _recptionsocket.Receive(buffer);
                        messages.Enqueue(buffer);
                    }
                    catch (SocketException e) { return; }
                }
            }

            /// <summary>
            /// Alters for server.
            /// </summary>
            public void ProcessPacket()
            {
                Console.WriteLine("Packed Processor online");
                while (true)
                {
                    if (messages.Count > 0)
                    {
                        OperationalTransform.TextTransformActor e = OperationalTransform.TextTransformActor.GetObjectFromBytes(this.messages.Dequeue());
                        //Set datestamp for server's sake
                        e.AlterforServer();
                        processed.Enqueue(e);
                        Console.WriteLine("Message recieved");
                        Console.WriteLine("Message contains {0} command", e.Command);
                    }
                    else
                    {//Might reduce the processor load a bit
                        System.Threading.Thread.Sleep(5);
                    }

                }
            }
        }

        class clienthandler
        {
            public clienthandler(Socket p)
            {
                _transmissionsocket = p;
                _clientdatareciever = new receptionhandler(p);
                _running = false;
                _pendingmessage = new System.Collections.Concurrent.ConcurrentQueue<byte[]>();
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
                byte[] ack;
                Console.WriteLine("Client at {0} has connected to the server", this._transmissionsocket.LocalEndPoint);
                _running = true;
                clientdatarecp = new System.Threading.Thread(new System.Threading.ThreadStart(_clientdatareciever.StartListening));
                clientdatarecp.Start();
                try
                {
                    while (true)
                    {
                        if (_pendingmessage.Count > 0)
                        {
                            //If it decodes, and dequeues a message properly, then send it back to the client.
                            if (_pendingmessage.TryDequeue(out ack))
                            {
                                _transmissionsocket.Send(ack);
                                Console.WriteLine("Sending Message to Client");
                            }
                            else
                            {
                                //Report the trouble.
                                Console.WriteLine("Failed to Dequeue message to send stack properly.");
                            }
                        }
                        else {
                            System.Threading.Thread.Sleep(5);
                        }
                    }
                }
                catch (SocketException r)
                {
                    Console.WriteLine("Client at {0} Dropped connection.", _transmissionsocket.LocalEndPoint);
                    _running = false;
                    clientdatarecp.Abort();
                    clientdatarecp = null;
                }
            }

            public receptionhandler _clientdatareciever;
            Socket _transmissionsocket;
            System.Collections.Concurrent.ConcurrentQueue<byte[]> _pendingmessage;

            /// <summary>
            /// Add Message to clienthandler.
            /// </summary>
            /// <param name="message">Message to Send</param>
            public void AddMessage(OperationalTransform.TextTransformActor message)
            {
                _pendingmessage.Enqueue(OperationalTransform.TextTransformActor.GetObjectInBytes(message));
                Console.WriteLine("Message added to pending stack");
            }
        }
    }
}