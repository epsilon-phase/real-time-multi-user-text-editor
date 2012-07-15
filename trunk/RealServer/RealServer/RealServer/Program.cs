using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealServer
{
    namespace SocketHandler
    {
        using System.Net.Sockets;
        class receptionhandler
        {
            private Socket _recptionsocket;
            public receptionhandler(Socket p)
            { }
            Queue<string> messages;
            public bool MessageAvailable{
                get{
                return messages.Count>0?true:false;
                }
            }
            public string MostRecentMessage { 
                get {
                    lock (messages)
                    {
                        return messages.Dequeue();
                    }
                }
            }
            public void StartListening() { }
        }

        class clienthandler {
            public clienthandler(Socket p)
            {
                _transmissionsocket = p;
                clientdatareciever = new receptionhandler(p);
            }
            System.Threading.Thread clientdatarecp;
            public void start() 
            {
                clientdatarecp = new System.Threading.Thread(new System.Threading.ThreadStart(clientdatareciever.StartListening));
                clientdatarecp.Start();

            }
            receptionhandler clientdatareciever;
            Socket _transmissionsocket;
            Queue<string> pendingmessage;
            public void addmessage(string message)
            {
                lock (pendingmessage)
                {
                    pendingmessage.Enqueue(message);
                }
            }
        }
    }
    class Server 
    {
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
