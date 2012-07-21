using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ChatServerLib
{
    /// <summary>
    /// ChatServer is designed to simplify the process of multithreading between many clients. All incoming messages are bounced to all connected ChatClients
    /// </summary>
    public class ChatServer
    {
        Socket MyServer = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.IP);
        IPAddress myIP=null;
        public const int bufferSize = 666;
        List<ClientInfo> clients = new List<ClientInfo>();
        List<Thread> threads = new List<Thread>();
        public ParameterizedThreadStart chatSenderThread;
        MessageRecievedListener myMRL = null;
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
        public void start(MessageRecievedListener mrl){
            if(running){
                return;
            }
            myMRL = mrl;
            chatSenderThread = delegate(object o)
            {
                ChatServer cs = (ChatServer)o;
                while (!(cs.running && cs.clients.Count != 0)){

                }
                ClientInfo ci = cs.clients.Last<ClientInfo>();
                Socket s = ci.socket;
                byte[] name_bytes = new byte[bufferSize];
                s.Receive(name_bytes);
                ci.name = Encoding.ASCII.GetString(ChatServer.noNulls(name_bytes));
                name_bytes = Encoding.ASCII.GetBytes(ci.name + ": ");
                while (true)
                {
                    try {
                        byte[] bytes = new byte[bufferSize];
                        s.Receive(bytes);
                        bytes = ChatServer.noNulls(bytes);
                        string msg = Encoding.ASCII.GetString(bytes);
                        //Console.WriteLine("Server Recieved bytes:"+msg);
                        cs.myMRL(ChatServer.getTimeStamp()+" "+ ci.name+": "+msg);
                        cs.broadcast(ChatServer.conCat(name_bytes,bytes),s);
                    } catch(SocketException e) {
                        //Console.WriteLine(e.StackTrace);
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
                    cs.clients.Add(new ClientInfo(MyServer.Accept()));
                    newChatThread();
                }
            };
            addThread(receptions);
        }
        public static string getTimeStamp(){
            string res = "[";
            DateTime now = DateTime.Now;
            res+=now.Day+"/"+now.Month+"/"+now.Year+" "+now.Hour+":"+now.Minute+":"+now.Second+"."+now.Millisecond+"]";
            return res;
        }
        /// <summary>
        /// Concatenates two byte arrays in the order b1, b2
        /// </summary>
        /// <param name="b1">The first part of the resultant array</param>
        /// <param name="b2">The second part of the resultant array</param>
        /// <returns>byte array of size equal to the sum of the sizes of b1 and b2</returns>
        public static byte[] conCat(byte[] b1,byte[] b2){
            byte[] res = new byte[b1.Length + b2.Length];
            int i = 0;
            for(;i<b1.Length;i++){
                res[i] = b1[i];
            }
            for(;i<res.Length;i++){
                res[i] = b2[i-b1.Length];
            }
            return res;
        }
        /// <summary>
        /// Removes empty or null bytes from a null array.
        /// </summary>
        /// <param name="bytes">The byte array to be used</param>
        /// <returns>A new array without empty bytes</returns>
        public static byte[] noNulls(byte[] bytes)
        {
            List<byte> temp = new List<byte>();
            foreach(byte b in bytes){
                if(!(b=='\0')){
                    temp.Add(b);
                }
            }
            return temp.ToArray();
        }
        /// <summary>
        /// Stops the server and closes all threads and connections
        /// </summary>
        public void stop(){
            running = false;
            foreach(Thread t in threads){
                t.Abort();
            }
            foreach(ClientInfo ci in clients){
                ci.socket.Disconnect(true);
            }
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
            broadcast(bytes, null);
        }
        /// <summary>
        /// Broadcasts to all sockets not equal to the parameter "exception"
        /// </summary>
        /// <param name="bytes">The bytes to broadcast</param>
        /// <param name="exception">The socket you don't want to send to.</param>
        public void broadcast(byte[] bytes,Socket exception){
            foreach (ClientInfo ci in clients)
            {
                if(!(ci.socket==exception)){
                    //Console.WriteLine("Server sending bytes to Socket");
                    ci.socket.Send(bytes);
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
