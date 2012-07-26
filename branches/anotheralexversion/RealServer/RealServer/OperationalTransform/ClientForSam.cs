﻿namespace OperationalTransform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Client for the sake of Sam, our resident(other than adam) VB coder and GUI designer.
    /// </summary>
    public class ClientForSam
    {
        #region Fields
        /// <summary>
        /// Has the text been changed?
        /// </summary>
        public Boolean changed;
        /// <summary>
        /// Send and recieve threads
        /// </summary>
        System.Threading.Thread a, b;
        private string consolidated;

        /// <summary>
        /// Server socket to be sure boss
        /// </summary>
        private System.Net.Sockets.Socket server;

        /// <summary>
        /// Some... thingy... of a kind
        /// </summary>
        private System.Collections.Concurrent.ConcurrentQueue<TextTransformActor> thingy;

        /// <summary>
        /// Not a Queue by any means
        /// </summary>
        TextTransformCollection TransformPool;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Create new client with the target IP address as a goal
        /// </summary>
        /// <remarks>
        /// Please put this in a try-catch block, if you use it right Sam, you should be able to force the client to show a proper error message and demand that the user enter a new IPaddress</remarks>
        /// <param name="target">the IP address of the target server.</param>
        public ClientForSam(System.Net.IPAddress target)
        {
            thingy = new System.Collections.Concurrent.ConcurrentQueue<TextTransformActor>();
            //intialize the text transform collection into a non server profile
            TransformPool = new TextTransformCollection(false);
            this.server = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                System.Net.Sockets.SocketType.Stream,
                System.Net.Sockets.ProtocolType.IP);
            this.server.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Socket, System.Net.Sockets.SocketOptionName.ReceiveBuffer, 512);
            this.server.Connect(new System.Net.IPEndPoint(target, 6000));
        }

        #endregion Constructors

        #region Methods

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

        /// <summary>
        /// Close connection to the server Once the client is done
        /// </summary>
        public void CloseConnection()
        {
            this.server.Close();
        }

        /// <summary>
        /// add text transformation for the "cut" operation
        /// </summary>
        /// <param name="selectionstart">Selection where the text was cut from</param>
        /// <param name="selectionend">Selection where the cut ended.</param>
        public void CutAdd(int selectionstart, int selectionend)
        {
            TextTransformActor t = new TextTransformActor(selectionstart, selectionend);
            this.thingy.Enqueue(t);
        }
        /// <summary>
        /// generate instructions to replace text that is currently selected
        /// </summary>
        /// <param name="selectionstart"></param>
        /// <param name="selectionlength"></param>
        /// <param name="insertion"></param>
        public void Generatereplace(int selectionstart, int selectionlength,string insertion)
        {
            TextTransformActor deletion = new TextTransformActor(selectionstart, selectionlength);
            TextTransformActor insert = new TextTransformActor(selectionstart, insertion);
            thingy.Enqueue(deletion);
            thingy.Enqueue(insert);
        }
        public void AddDelete(int selectionstart, int selectionlength)
        {
            TextTransformActor e = new TextTransformActor(selectionstart, selectionlength);
            thingy.Enqueue(e);
        }
        public void AddInsert(int selectionstart, string insertion) 
        {
            TextTransformActor e = new TextTransformActor(selectionstart, insertion);
            thingy.Enqueue(e);
        }
        /// <summary>
        /// gets the calculated consolidated string
        /// </summary>
        /// <returns>The consolidated string</returns>
        public string getconsolidatedstring()
        {
            this.changed = false;
                return consolidated;
        }

        public int GetOffsetCursorPosition(int selectionstart)
        {
            return TransformPool.CalculateOffsetCursorPosition(selectionstart);
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
            req = new TextTransformActor(selectionindex, q.KeyChar.ToString());

            //just to ensure that the datetime is registered correctly. which really shouldn't be needed now that I think about it.
            req.AlterForClient();
            //queue.Add(req);
            thingy.Enqueue(req);
        }

        /// <summary>
        /// Add text Transformation for backspace/Delete
        /// Call from rtbtext.previewkeydown(which is important)
        /// </summary>
        /// <param name="key">The keypress event</param>
        /// <param name="SelectionIndex">The index of the cursor</param>
        public void KeyPressDelete(System.Windows.Forms.PreviewKeyDownEventArgs key,int SelectionIndex,int selectionlength)
        {
            TextTransformActor req=null;
            if (key.KeyCode == Keys.Back)
            {//Backspace, delete the character before the cursor.
                
                    req = new TextTransformActor(SelectionIndex - 1, 1);
                req.AlterForClient();
                //Add to the list of things to send to the server
                
            }
            else if (key.KeyCode == Keys.Delete)
            {
                //Delete key, deletes the character in front of the cursor
                req = new TextTransformActor(SelectionIndex, selectionlength);
                req.AlterForClient();
                if (selectionlength > 0)
                    req = new TextTransformActor(SelectionIndex, selectionlength);
                else
                    req = new TextTransformActor(SelectionIndex, 1);
                //Add to the list of things to send to hte server
                
            }
            else if (key.KeyCode == Keys.Enter)
            {
                req = new TextTransformActor(SelectionIndex, "\n");
                
            }
            if(req!=null)
                thingy.Enqueue(req);
        }

        //Handle pasting things
        public void PasteAdd(int selectionstart, string insertedtext)
        {
            TextTransformActor r;
            //nine hundred bytes should prevent 1024 byte long packets from being too little
            if (insertedtext.Length <= 100)
            {
                r = new TextTransformActor(selectionstart, insertedtext);
                thingy.Enqueue(r);
            }
            else
            {

                for (int i = 0; i < insertedtext.Length; i++) 
                {
                    thingy.Enqueue(new TextTransformActor(selectionstart+i,insertedtext[i]));
                    System.Threading.Thread.Sleep(10);
                }
            }
        }
        
        /// <summary>
        /// Start listening, handing everything off to two new threads.
        /// </summary>
        public void Start()
        {
            a = new System.Threading.Thread(new System.Threading.ThreadStart(SendTextTransformation));
            b = new System.Threading.Thread(new System.Threading.ThreadStart(RecieveandConsolidate));
            a.Start();
            b.Start();
            
        }

        /// <summary>
        /// Consolidate thread, listen for messages, and change the "changed" flag to notify the user's thingy.
        /// </summary>
        private void RecieveandConsolidate()
        {
            byte[] buffery=new byte[512];
            while (true)
            {//Clear buffer(hopefully)
                
                try
                {
                    //Blocking call, minimizes the amount of work that the thread does needlessly,
                    //now only if I could find something like it for the queue
                    server.Receive(buffery);
                    lock (TransformPool)
                    {
                        TransformPool.Add(TextTransformActor.GetObjectFromBytes(buffery));
                        consolidated = TransformPool.CalculateConsolidatedString();
                        changed = true;
                    }
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    MessageBox.Show("The server has dropped your connection");
                    //escape the loop, allowing the thread to end.
                    return;
                }
            }
        }
        /// <summary>
        /// Wrap selected text in parenthesis
        /// </summary>
        /// <param name="selectionstart">the index of the start of the selection</param>
        /// <param name="selectionlength">the length of the selection</param>
        public void AddParenthesis(int selectionstart, int selectionlength) 
        {
            TextTransformActor begin = new TextTransformActor(selectionstart, "("), end = new TextTransformActor(1+selectionstart + selectionlength, ")");
            thingy.Enqueue(begin);
            thingy.Enqueue(end);
        }

        private void SendTextTransformation()
        {
            byte[] d;
            TextTransformActor Holder;
            while (true)
            {
                while (thingy.Count > 0)
                {
                    thingy.TryDequeue(out Holder);
                    try
                    {
                        
                        d = TextTransformActor.GetObjectInBytes(Holder);
                        
                        server.Send(d);
                        System.Threading.Thread.Sleep(5);
                    }
                    catch (System.Net.Sockets.SocketException serverproblem)
                    {//end the thread quickly when there is a socket error.
                        return;
                    }
                }
            }
        }
        #endregion Methods
    }
}