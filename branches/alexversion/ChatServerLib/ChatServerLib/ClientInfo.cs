using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatServerLib
{
    /// <summary>
    /// This struct's purpose is to keep track of users easily.
    /// </summary>
    struct ClientInfo
    {
        public Socket socket;
        public string name;
        public ClientInfo(Socket s){
            this.socket = s;
            this.name = "User";
        }
        public ClientInfo(string name){
            this.socket = null;
            this.name = name;
        }
        public ClientInfo(Socket s, string name){
            this.socket = s;
            this.name = name;
        }
    }
}
