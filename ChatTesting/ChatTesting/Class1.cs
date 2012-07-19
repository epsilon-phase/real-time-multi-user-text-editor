using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServerLib;

namespace ChatTesting
{
    class MultiUserTest
    {
        public static void Main()
        {
            Console.WriteLine("Server or client?");
            string response = Console.ReadLine();
            if(response.ToUpper()=="SERVER"){
                Console.WriteLine("Port?");
                int port = getInt();
                ChatServer cs = new ChatServer(port);
                MessageRecievedListener mrl = delegate(string s)
                {
                    Console.WriteLine(s);
                };
                cs.start(mrl);
                Console.WriteLine("Server started on port "+port+" type \"/quit\" to close.");
                while(true){
                    response = Console.ReadLine();
                    if(response.ToUpper()=="/QUIT"){
                        cs.stop();
                        Environment.Exit(0);
                        break;
                    }else{
                        cs.broadcast("SERVER ADMIN:"+response);
                    }
                }
            }else if (response.ToUpper() == "CLIENT"){
                Console.WriteLine("IP?");
                System.Net.IPAddress ip = getIP();
                while(ip==null){
                    Console.WriteLine("Invalid input");
                    ip = getIP();
                }
                Console.WriteLine("Port?");
                int port = getInt();
                ChatClient cc = new ChatClient(ip, port);
                Console.WriteLine("Connected to server. You are free to chat.");
                MessageRecievedListener mrl = delegate(string str)
                {
                    Console.WriteLine(str);
                };
                cc.start(mrl);
                response = Console.ReadLine();
                while(!(response.ToUpper()=="/QUIT")){
                    cc.send(response);
                    response = Console.ReadLine();
                }
                Environment.Exit(0);
            }
        }
        public static int getInt(){
            int res = 0;
            string response = "";
            while (!(int.TryParse(response, out res)))
            {
                response = Console.ReadLine();
            }
            return res;
        }
        public static System.Net.IPAddress getIP(){
            try{
                return System.Net.IPAddress.Parse(Console.ReadLine());
            }catch(Exception e){
                return null;
            }
        }
    }
}
