﻿namespace OperationalTransform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #region Enumerations

    public enum TextTransformType
    {
        Insert,
        Delete,
        Initialize,
        Append
    }

    #endregion Enumerations

    /// <summary>
    /// Text Transform holder(I.E. the instructions about where the text changes, and what that means).
    /// </summary>
    [Serializable]
    public class TextTransformActor : System.Runtime.Serialization.ISerializable
    {
        #region Fields
        
        //Not for use by the client, but, should be enough
        public DateTime time;

        string insert;
        bool isserver;
        int lengthtodelete;
        TextTransformType _command;
        int _uncompensatedindex;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// create a new TextTransform actor which is set to the insert command.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public TextTransformActor(int index, string data)
        {
            
            isserver = false;
            this._command = TextTransformType.Insert;
            this._uncompensatedindex = index;
            insert = data;
        }

        /// <summary>
        /// Initialize for appending
        /// </summary>
        /// <param name="appendix">String to append</param>
        /// <param name="dummy">index</param>
        public TextTransformActor(string appendix, int dummy)
        {
            if (dummy <= 0)
            {
                this._command = TextTransformType.Initialize;
            }
            else
            {
                this._command = TextTransformType.Append;
            }
            this._uncompensatedindex= dummy;
            this.insert = appendix;
        }

        /// <summary>
        /// New initialization command. Use it to set the TextTranformCollection's initial value to something
        /// </summary>
        /// <see cref="TextTransformCollection"/>
        /// <param name="initialization"></param>
        public TextTransformActor(string initialization)
        {
            this.insert = initialization;
            this._command = TextTransformType.Initialize;
        }

        /// <summary>
        /// Initialization for the sake of deletion
        /// </summary>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public TextTransformActor(int index, int length)
        {
            isserver = false;
            _command = TextTransformType.Delete;
            _uncompensatedindex = index;
            this.lengthtodelete = length;
        }

        public TextTransformActor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            this._command = (TextTransformType)info.GetInt32("Command");
            this._uncompensatedindex = info.GetInt32("index");
            this.insert = info.GetString("Insert");
            this.isserver = info.GetBoolean("isserver");
            this.time = DateTime.FromBinary(info.GetInt64("time"));
            this.lengthtodelete =info.GetInt32( "DeleteLength");
        }

        #endregion Constructors

        #region Properties

        public TextTransformType Command
        {
            get
            {
                return _command;
            }
        }

        public bool FromClient
        {
            get
            {
                return !isserver;
            }
        }

        public bool FromServer
        {
            get
            {
                return !isserver;
            }
        }

        public int Index
        {
            get
            {
                return _uncompensatedindex;
            }
        }

        public string Insert
        {
            get
            {
                return insert;
            }
            set 
            { 
                this.insert = value;
            }
        }

        //length of modification made
        public int Length
        {
            get
            {
                if (_command == TextTransformType.Insert)
                    return insert.Length;
                else
                    return this.lengthtodelete;
            }
        }

        #endregion Properties

        #region Methods

        //use datetime
        public static bool Equals(object x, object y)
        {
            return ((TextTransformActor)x).time == ((TextTransformActor)y).time;
        }

        /// <summary>
        /// Convert byte array to TextTransformActor
        /// </summary>
        /// <param name="q">Array of Bytes</param>
        /// <returns>the equivalent(or something like that) TexttransformActor</returns>
        public static TextTransformActor GetObjectFromBytes(byte[] q)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter t = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (TextTransformActor)t.Deserialize(new System.IO.MemoryStream(q));
        }
        
        public static byte[] GetObjectInBytes(TextTransformActor g)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter t = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.Stream y = new System.IO.MemoryStream();
            t.Serialize(y,g);
            byte[] q = ((System.IO.MemoryStream)y).ToArray();
            y.Close();
            return q;
        }

        public void AlterForClient()
        {
            this.isserver = false;
            this.time = DateTime.Now;
        }

        public void AlterforServer()
        {
            this.isserver = true;
            this.time = DateTime.Now;
        }

        public override bool Equals(object x)
        {
            if (((TextTransformActor)x).time == this.time)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Obtain byte array to make it better
        /// </summary>
        /// <param name="info">Reference object to add information to.</param>
        /// <param name="context">Not a clue</param>
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Command", (int)this._command);
            info.AddValue("Insert", insert);
            info.AddValue("DeleteLength", this.Length);
            info.AddValue("index", this._uncompensatedindex);
            info.AddValue("isserver", this.isserver);
            info.AddValue("time", time.ToBinary());
        }

        /// <summary>
        /// Almost in the fashion of java constructors
        /// </summary>
        /// <returns>The altered transform</returns>
        public TextTransformActor GetWithServerAlteration()
        {
            this.AlterforServer();
            return this;
        }

        #endregion Methods
    }
}