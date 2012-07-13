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
        Socket me = new Socket(SocketType.Stream, ProtocolType.IP);
        /// <summary>
        /// This connects to a local IP supplied by the system and the specified port
        /// </summary>
        /// <param name="port">The port you're connecting to.</param>
        public ChatClient(int port){
            me.Connect(IPAddress.Any, port);
        }
        /// <summary>
        /// Connects to a specific IP and port
        /// </summary>
        /// <param name="ip">The IP of the server</param>
        /// <param name="port">The port of the server</param>
        public ChatClient(IPAddress ip,int port){
            me.Connect(ip, port);
        }
        /// <summary>
        /// Converts the string into an IPAddress
        /// </summary>
        /// <param name="ipstr">The IP address in the form "aaa.bbb.cc.dddd"</param>
        /// <param name="port">The port number</param>
        public ChatClient(string ipstr,int port){
            me.Connect(IPAddress.Parse(ipstr), port);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public ChatClient(long ip,int port){
            me.Connect(new IPAddress(ip), port);
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
                    mrl(s);
                }
            };
            Thread t = new Thread(pts);
            t.Start(this);
        }
        /// <summary>
        /// Sends the string to the server
        /// </summary>
        /// <param name="s">The string to send</param>
        public void send(string s){
            me.Send(Encoding.ASCII.GetBytes(s));
        }
        /// <summary>
        /// Hangs until a message is recieved then returns it in string form
        /// </summary>
        /// <returns>The string sent by the server</returns>
        public string recieve(){
            byte[] bytes = null;
            me.Receive(bytes);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
