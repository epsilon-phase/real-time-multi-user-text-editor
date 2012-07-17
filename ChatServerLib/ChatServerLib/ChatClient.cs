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
    /// A place for you to input your own designs into mine whenever the socket reieves a message
    /// </summary>
    /// <param name="s">The message</param>
    public delegate void MessageRecievedListener(string s);
    /// <summary>
    /// A simplified interface for a programmer to communicate with a ChatServer
    /// </summary>
    public class ChatClient
    {
        Socket mySocket = new Socket(SocketType.Stream, ProtocolType.IP);
        public const int bufferSize = 666;
        string myName = "Anonymous";
        /// <summary>
        /// This connects to a local IP supplied by the system and the specified port
        /// </summary>
        /// <param name="port">The port you're connecting to.</param>
        public ChatClient(int port){
            mySocket.Connect(IPAddress.Any, port);
        }
        /// <summary>
        /// Connects to a specific IP and port
        /// </summary>
        /// <param name="ip">The IP of the server</param>
        /// <param name="port">The port of the server</param>
        public ChatClient(IPAddress ip,int port){
            mySocket.Connect(ip, port);
        }
        /// <summary>
        /// Converts the string into an IPAddress
        /// </summary>
        /// <param name="ipstr">The IP address in the form "aaa.bbb.cc.dddd"</param>
        /// <param name="port">The port number</param>
        public ChatClient(string ipstr,int port){
            mySocket.Connect(IPAddress.Parse(ipstr), port);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public ChatClient(long ip,int port){
            mySocket.Connect(new IPAddress(ip), port);
        }
        /// <summary>
        /// Starts the listener thread given
        /// </summary>
        /// <param name="mrl">The messager thread</param>
        public void start(MessageRecievedListener mrl){
            ParameterizedThreadStart pts = delegate(object o)
            {
                ChatClient cc = (ChatClient)o;
                while(true){
                    string s = cc.recieve();
                    //Console.WriteLine("Client recieved bytes");
                    mrl(s);
                }
            };
            Thread t = new Thread(pts);
            t.Start(this);
        }
        public void start(MessageRecievedListener mrl,string username){
            this.myName = username;
            start(mrl);
            this.send(username);
        }
        /// <summary>
        /// Sends the string to the server
        /// </summary>
        /// <param name="s">The string to send</param>
        public void send(string s){
            if(s.Length<=bufferSize){
                mySocket.Send(Encoding.ASCII.GetBytes(s));
                //Console.WriteLine("Client sent bytes.");
            }else{
                send(s.Substring(0,bufferSize));
                send(s.Remove(0, bufferSize));
            }
        }
        /// <summary>
        /// Hangs until a message is recieved then returns it in string form
        /// </summary>
        /// <returns>The string sent by the server</returns>
        public string recieve(){
            byte[] bytes = {};
            mySocket.Receive(bytes);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
