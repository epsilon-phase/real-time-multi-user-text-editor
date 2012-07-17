using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatServerLib
{
    class ClientInfo
    {
        public Socket socket = null;
        public string name = "User";
        public ClientInfo(Socket s){
            this.socket = s;
        }
    }
}
