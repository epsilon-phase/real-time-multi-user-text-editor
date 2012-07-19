namespace OperationalTransform
{
    using System;
    using System.Collections.Generic;

    public class TextTransformCollection
    {
        #region Fields

        List<TextTransformActor> actions;
        private string initial;
        private bool _server;

        #endregion Fields

        #region Constructors

        public TextTransformCollection()
        {
            this.actions = new List<TextTransformActor>();
        }

        public TextTransformCollection(bool Server)
        {
            _server = Server;
            this.actions = new List<TextTransformActor>();
        }

        /// <summary>
        /// tell the text transform collection to start with a certain string;
        /// </summary>
        /// <param name="initial">Initial string to start with</param>
        public TextTransformCollection(string initial)
        {
            this.initial = initial;
            this.actions = new List<TextTransformActor>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Consolidates string entry using text actor stuff.
        /// </summary>
        public string consolidated
        {
            get
            {
                return CalculateConsolidatedString();
            }
        }

        public bool Server
        {
            get
            {
                return _server;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Transform a text file(given as a stream).
        /// </summary>
        /// <param name="fileyo"></param>
        /// <returns>Representation of text file separated into pieces less than 1024 bytes long</returns>
        public static TextTransformCollection PrepareCollectionFromLargeText(System.IO.Stream fileyo)
        {
            byte[] q = new byte[900];
            List<byte[]> tik = new List<byte[]>();
            TextTransformCollection e = new TextTransformCollection();
            string funny;
            int w = 0;
            while (fileyo.Position > fileyo.Length)
            {
                if (fileyo.Length - (fileyo.Position + 900) >= 0)
                {
                    fileyo.Read(q, 0, 900);
                    funny = System.Text.Encoding.ASCII.GetChars(q).ToString();
                }
                else
                {
                    fileyo.Read(q, 0, (int)(fileyo.Length - fileyo.Position));
                    funny = System.Text.Encoding.ASCII.GetChars(q).ToString();
                    //replace the nulls on the string with spaces
                    int y = funny.IndexOf('\0');
                }
                e.Add(new TextTransformActor(funny, w));
            }
            return e;
        }

        public void Add(TextTransformActor ax)
        {
            if (ax.Command == TextTransformType.Initialize)
            {
                //set initial string and discard the actor
                this.initial = ax.Insert;
                ax = null;
            }
            else
            {
                actions.Add(ax);
            }
        }

        /// <summary>
        /// Complicated procedure, should not call unless necessary
        /// goes through list of text transformations and applies them, calculating the necessary offsets to do so.
        /// </summary>
        /// <returns> The Consolidated string</returns>
        public string CalculateConsolidatedString()
        {

            //Sort the operations based on time or appending, just so that it must work.

            string e = initial;
            int offset = 0;
            lock (actions)
            {
                actions.Sort(CompareTextActorTime);
                for (int i = 0; i < actions.Count; i++)
                {
                    offset = CalculateIndexOffset(actions[i]);
                    if (actions[i].Command == TextTransformType.Delete)
                        e = e.Remove(actions[i].Index + offset, actions[i].Length);
                    if (actions[i].Command == TextTransformType.Insert)
                    {
                        try
                        {
                            e = e.Insert(actions[i].Index + offset, actions[i].Insert);
                        }
                        catch (IndexOutOfRangeException q)
                        {
                            e = e + actions[i].Insert;
                        }
                    }
                    if (actions[i].Command == TextTransformType.Append)
                    {
                        e += actions[i].Insert;
                    }
                }
            }
            return e;
        }

        /// <summary>
        /// Checks Whether a specific transform is inside the pool
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool ContainsTransform(TextTransformActor t)
        {
            return this.actions.Contains(t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">this is foo</param>
        /// <param name="b">this is bar</param>
        /// <returns>The comparison, context sensitive of course, used to order the list.</returns>
        private static int CompareTextActorTime(TextTransformActor a, TextTransformActor b)
        {
            //If both a and b are append commands, then it should be sorted out based on index. Should not come up on client side.
            if (a.Command == TextTransformType.Append && b.Command == TextTransformType.Append)
                return a.Index.CompareTo(b.Index);
            //Appends are the most important part, they should be applied first
            if (a.Command == TextTransformType.Append && b.Command != TextTransformType.Append)
                return -1;
            if (a.Command != TextTransformType.Append && b.Command == TextTransformType.Append)
                return 1;
            return DateTime.Compare(a.time, b.time);
        }
        private int CalculateIndexOffset(TextTransformActor d)
        {
            int totaloffset = 0;
            for (int i = 0; actions[i].time < d.time; i++)
            {
                if (actions[i].Index <= d.Index)
                {
                    if (actions[i].Command == TextTransformType.Insert)
                    {
                        totaloffset += actions[i].Length;
                    }
                    else
                    {
                        totaloffset -= actions[i].Length;
                    }
                }
            }
            return totaloffset;
        }

        #endregion Methods
    }
}