using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperationalTransform
{
    public class TextTransformCollection 
    {
        private static int CompareTextActorTime(TextTransformActor a, TextTransformActor b)
        {
            return DateTime.Compare(a.time, b.time);
        }
        public TextTransformCollection(string initial)
        {
            this.initial = initial;
            this.actions = new List<TextTransformActor>();
        }
        public void add(TextTransformActor ax)
        {
            actions.Add(ax);
            //Sort actions in order to keep it clear.
            actions.Sort(CompareTextActorTime);
        }
        private int calculateindexoffset(TextTransformActor d)
        {
            int total = 0;
            for (int i = 0; actions[i].time<d.time; i++)
            {
                if (actions[i].Index <= d.Index)
                {
                    if (actions[i].Command == TextTransformType.Insert)
                    {
                        total += actions[i].Length;
                    }
                    else
                    {
                        total -= actions[i].Length;
                    }
                }
            }
            return total;
        }
        /// <summary>
        /// Complicated procedure, should not call unless necessary
        /// goes through list of text transformations and applies them, calculating the necessary offsets to do so.
        /// </summary>
        /// <returns> The Consolidated string</returns>
        public string CalculateConsolidatedString()
        {
            string e = initial;
            int offset=0;
            for (int i = 0; i < actions.Count; i++)
            {
                
                offset=calculateindexoffset(actions[i]);
                if (actions[i].Command == TextTransformType.Delete)
                    e=e.Remove(actions[i].Index + offset, actions[i].Length);
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
            }
            return e;
        }
        private string initial;
        /// <summary>
        /// Consolidates string entry using text actor stuff.
        /// </summary>
        public string consolidated{
            get
            {
                return CalculateConsolidatedString();
            }
        }
        List<TextTransformActor> actions;

    }
    public enum TextTransformType 
    {
        Insert,
        Delete
    }
    [Serializable]
    public class TextTransformActor:System.Runtime.Serialization.ISerializable
    {
        public DateTime time;
        int _uncompensatedindex;
        string insert;
        public string Insert
        {
            get
            {
                return insert;
            }
        }
        public static TextTransformActor GetObjectFromBytes(byte[] q)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter t = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (TextTransformActor)t.Deserialize((System.IO.Stream)new System.IO.MemoryStream(q));
        }
        public static byte[] GetObjectInBytes(TextTransformActor g)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter t = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream y = new System.IO.MemoryStream();
            t.Serialize(y, g);
            byte[] q=new byte[y.Length];
            y.Read(q,0,(int)y.Length);
            y.Close();
            return q;
        }
        public int Length 
        {
            get{
                if(_command==TextTransformType.Insert)
                    return insert.Length;
                else
                    return this.lengthtodelete;
               }
        }
        int lengthtodelete;
        public int Index
        {
            get
            {
                return _uncompensatedindex;
            }
        }
        public TextTransformActor(int index, string data,DateTime stamp) 
        {
            this._command = TextTransformType.Insert;
            this._uncompensatedindex = index;
            insert = data;
            this.time = stamp;
        }
        public TextTransformActor(int index, int length,DateTime stamp) 
        {
            _command = TextTransformType.Delete;
            _uncompensatedindex = index;
            this.lengthtodelete = length;
            time = stamp;
        }
        
        TextTransformType _command;
        public TextTransformType Command 
        { 
            get 
            {
                return _command; 
            } 
        }
        /// <summary>
        /// Guess What! It should work
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Command", (int)this._command);
            info.AddValue("TimeStamp", time.ToBinary());
            if (_command == TextTransformType.Insert)
                info.AddValue("Insert", insert);
            else
                info.AddValue("DeleteLength", this.Length);
            info.AddValue("index", this._uncompensatedindex);
        }
    }
}
