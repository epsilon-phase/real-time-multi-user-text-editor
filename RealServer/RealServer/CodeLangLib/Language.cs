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
        public static string[] keywords;
        public static string commentChar;
        public static string multiLineCommentStart;
        public static string multiLineCommentEnd;
        public static bool supportsMultiLineComments = false;
        public static string fileExtension = "";
        public static Color keywordColor = Color.Blue;
        public static Color stringColor = Color.Salmon;
        public static Color commentColor = Color.LightGreen;
        public static Color numberColor = Color.Black;

        public static readonly Language LangC = new C();
        public static readonly Language LangCSharp = new CSharp();
        public static readonly Language LangCPlusPlus = new CPlusPlus();
        public static readonly Language LangPython = new Python();
        public static readonly Language LangJava = new Java();
    }
}
