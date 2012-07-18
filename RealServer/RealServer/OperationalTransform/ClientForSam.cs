using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperationalTransform
{
    using System.Windows.Forms;
    class ClientForSam
    {
        TextTransformCollection queue;
        private System.Net.Sockets.Socket server;
        private Queue<TextTransformActor> thingy;
        /// <summary>
        /// Create new client with the target IP address as a goal
        /// </summary>
        /// <param name="target">the IP address of the target server.</param>
        public ClientForSam(System.Net.IPAddress target) 
        {
            this.server = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.IPv4);
            this.server.Connect(new System.Net.IPEndPoint(target, 6000));
        }
        /// <summary>
        /// Test if the entered string is a valid IPaddress
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns> whether or not the ipaddress is valid</returns>
        public static bool ValidateIPAddress(string input)
        {
            System.Net.IPAddress e;
            return System.Net.IPAddress.TryParse(input, out e);
        }
        private void RecieveandConsolidate() 
        {
            byte[] buffery=new byte[1024];
            while (true) 
            {
                server.Receive(buffery);
                lock(queue)
                    queue.Add(TextTransformActor.GetObjectFromBytes(buffery));
            }
        }
        System.Threading.Thread a, b;
        public void Start() 
        {
            a = new System.Threading.Thread(new System.Threading.ThreadStart(SendTextTransformation));
            b = new System.Threading.Thread(new System.Threading.ThreadStart(RecieveandConsolidate));
        }
        private void SendTextTransformation()
        {
            while (true) 
            {
                while (thingy.Count > 0)
                {
                    server.Send(TextTransformActor.GetObjectInBytes(this.thingy.Dequeue()));
                }
            }
        }
        /// <summary>
        /// Call from rtbtext keypress event.
        /// </summary>
        /// <remarks>
        /// Please set the event argument's .handled=true.
        /// </remarks>
        /// <param name="q">the keypress arguments</param>
        /// <param name="selectionindex">index of the cursor</param>
        public void KeyPressadd(System.Windows.Forms.KeyPressEventArgs q, int selectionindex) 
        {
            TextTransformActor req;
            req = new TextTransformActor(selectionindex, q.KeyChar);
            queue.Add(req);
            thingy.Enqueue(req);
        }
        /// <summary>
        /// Add text Transformation for backspace/Delete
        /// Call from rtbtext.previewkeydown(which is important)
        /// </summary>
        /// <param name="key">The keypress event</param>
        /// <param name="SelectionIndex">The index of the cursor</param>
        public void KeyPressDelete(System.Windows.Forms.PreviewKeyDownEventArgs key,int SelectionIndex)
        {
            TextTransformActor req;
            if (key.KeyCode == Keys.Back) 
            {//Backspace, delete the character before the cursor.
                req = new TextTransformActor(SelectionIndex - 1, 1);
                queue.Add(req);
                thingy.Enqueue(req);
            }
            if (key.KeyCode == Keys.Delete) 
            {//Delete key, deletes the character in front of the cursor
                req = new TextTransformActor(SelectionIndex, 1);
                queue.Add(req);
                thingy.Enqueue(req);
            }
            
        }

    }
}
