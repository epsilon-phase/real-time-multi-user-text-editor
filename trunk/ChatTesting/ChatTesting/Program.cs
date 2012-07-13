using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServerLib;

namespace ChatTesting
{
    public class Program
    {
        public static void Main()
        {
            System.Net.IPAddress ip = System.Net.IPAddress.Any;
            ChatServer server = new ChatServer(ip,1341);
            server.start();
            ChatClient client = new ChatClient("126",1341);
            MessageRecievedListener mrl = delegate(string s)
            {
                Console.WriteLine(s);
            };
            client.start(mrl);
            Console.WriteLine("Type your text:");
            while (true)
            {
                client.send(Console.ReadLine());
            }
        }
    }
}
