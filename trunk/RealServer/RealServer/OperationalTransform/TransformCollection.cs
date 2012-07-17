using System.Collections.Generic;
using System;
namespace OperationalTransform
{
    public class TextTransformCollection
    {
        #region Fields

        List<TextTransformActor> actions;
        private string initial;

        #endregion Fields

        #region Constructors
        public TextTransformCollection() 
        {
            this.actions=new List<TextTransformActor>();
        }
        /// <summary>
        /// tell the text transform collection to start with a certain string;
        /// </summary>
        /// <param name="initial"></param>
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

        #endregion Properties

        #region Methods

        public void Add(TextTransformActor ax)
        {
            if (ax.Command == TextTransformType.Initialize)
            {
                this.initial = ax.Insert;
            }
            else
            {
                actions.Add(ax);
                //Sort actions in order to keep it clear.
                actions.Sort(CompareTextActorTime);
            }
        }

        /// <summary>
        /// Complicated procedure, should not call unless necessary
        /// goes through list of text transformations and applies them, calculating the necessary offsets to do so.
        /// </summary>
        /// <returns> The Consolidated string</returns>
        public string CalculateConsolidatedString()
        {
            string e = initial;
            int offset = 0;
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
            return e;
        }

        private static int CompareTextActorTime(TextTransformActor a, TextTransformActor b)
        {
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