using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatServerLib
{
    /// <summary>
    /// ChatServer is designed to simplify the process of multithreading between many clients. All incoming messages are bounced to all connected ChatClients
    /// </summary>
    public class ChatServer
    {
        Socket MyServer = new Socket(SocketType.Stream, ProtocolType.IP);
        IPAddress myIP=null;
        List<Socket> clients = new List<Socket>();
        List<Thread> threads = new List<Thread>();
        public ParameterizedThreadStart chatSenderThread;
        private bool running = false;
        /// <summary>
        /// This constructor binds the server to an abstract EndPoint object
        /// </summary>
        /// <param name="endPoint">The server endpoint</param>
        public ChatServer(EndPoint endPoint){
            MyServer.Bind(endPoint);
        }
        /// <summary>
        /// This Constructor binds the server to an IP address and port
        /// </summary>
        /// <param name="ip">The IPAdress you want to use</param>
        /// <param name="port">The port you want to bind to</param>
        public ChatServer(IPAddress ip,int port){
            myIP = ip;
            MyServer.Bind(new IPEndPoint(ip, port));
        }
        /// <summary>
        /// This constructor creates the IPaddress and endpoint from the string and port
        /// </summary>
        /// <param name="ip">The IP in the form of "xxx.yyy.ww.zz"</param>
        /// <param name="port">The port you wish to bind to</param>
        public ChatServer(string ip, int port){
            IPAddress IP = IPAddress.Parse(ip);
            MyServer.Bind(new IPEndPoint(IP, port));
            myIP = IP;
        }
        /// <summary>
        /// The simplest constructor, this takes the local IP of the machine and the port
        /// </summary>
        /// <param name="port">The port to bind to</param>
        public ChatServer(int port){
            MyServer.Bind(new IPEndPoint(IPAddress.Any,port));
            myIP = IPAddress.Any;
        }
        /// <summary>
        /// Starts the server going and taking connections
        /// </summary>
        public void start(){
            if(running){
                return;
            }
            chatSenderThread = delegate(object o)
            {
                ChatServer cs = (ChatServer)o;
                while (!(cs.running && cs.clients.Count != 0)){

                }
                Socket s = cs.clients.Last<Socket>();
                while (true)
                {
                    try {
                        byte[] bytes = {};
                        s.Receive(bytes);
                        Console.WriteLine("Server Recieved bytes:"+Encoding.ASCII.GetString(bytes));
                        cs.broadcastException(bytes,s);
                    } catch(SocketException e) {
                        Console.WriteLine(e.StackTrace);
                    }
                }
            };
            MyServer.Listen(5);
            running = true;
            ParameterizedThreadStart receptions = delegate(object o)
            {
                ChatServer cs = (ChatServer)o;
                while (running)
                {
                    cs.clients.Add(MyServer.Accept());
                    newChatThread();
                }
            };
            addThread(receptions);
        }
        /// <summary>
        /// Stops the server and closes all threads and connections
        /// </summary>
        public void stop(){
            running = false;
            foreach(Thread t in threads){
                t.Abort();
            }
            foreach(Socket s in clients){
                s.Disconnect(true);
            }
            MyServer.Disconnect(true);
        }
        /// <summary>
        /// Starts a new chat thread
        /// </summary>
        public void newChatThread(){
            addThread(chatSenderThread);
        }
        /// <summary>
        /// Creates a new thread and starts it
        /// </summary>
        /// <param name="chatSenderThread">The threadStarter you want to use in the Thread</param>
        private void addThread(ParameterizedThreadStart chatSenderThread)
        {
            Thread t = new Thread(chatSenderThread, 0);
            threads.Add(t);
            t.Start(this);
        }
        /// <summary>
        /// Creates a new thread and starts it
        /// </summary>
        /// <param name="chatSenderThread">The threadStarter you want to use in the Thread</param>
        public void addThread(ThreadStart ts){
            Thread t = new Thread(ts, 0);
            threads.Add(t);
            t.Start(this);
        }
        /// <summary>
        /// Sends a string out to all connected sockets
        /// </summary>
        /// <param name="s">The string to send</param>
        public void broadcast(string s){
            broadcast(Encoding.ASCII.GetBytes(s));
        }
        /// <summary>
        /// Sends a byte array out to all connected sockets
        /// </summary>
        /// <param name="s">The byte array to send</param>
        public void broadcast(byte[] bytes)
        {
            broadcastException(bytes, null);
        }
        /// <summary>
        /// Broadcasts to all sockets not equal to the parameter "exception"
        /// </summary>
        /// <param name="bytes">The bytes to broadcast</param>
        /// <param name="exception">The socket you don't want to send to.</param>
        public void broadcastException(byte[] bytes, Socket exception){
            foreach (Socket s in clients)
            {
                if(!(s==exception)){
                    Console.WriteLine("Server sending bytes to Socket");
                    s.Send(bytes);
                }
            }
        }
        /// <summary>
        /// Gets the IP address
        /// </summary>
        /// <returns>System.Net.IPAdress</returns>
        public IPAddress getIPAddress(){
            return myIP;
        }
    }
}
