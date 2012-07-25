using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CodeLangLib
{
    public class Language
    {
        public string[] keywords;
        public string commentChar;
        public string multiLineCommentStart;
        public string multiLineCommentEnd;
        public bool supportsMultiLineComments = false;
        public string fileExtension = "";
        public Color keywordColor = Color.Blue;
        public Color stringColor = Color.Salmon;
        public Color commentColor = Color.LightGreen;
        public Color numberColor = Color.Black;

        public static readonly Language LangC = new C();
        public static readonly Language LangCSharp = new CSharp();
        public static readonly Language LangCPlusPlus = new CPlusPlus();
        public static readonly Language LangPython = new Python();
        public static readonly Language LangJava = new Java();
        public static readonly Language LangVBdotNET = new VBdotNET();
    }
}
