using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CodeLangLib
{
    class Java:Language
    {
        internal Java(){
            Java.keywords = "abstract,continue,for,new,switch,assert,default,goto,package,synchronized,boolean,do,if,private,this,break,double,implements,protected,throw,byte,else,import,public,throws,case,enum,instanceof,return,transient,catch,extends,int,short,try,char,final,interface,static,void,class,finally,long,strictfp,volatile,const,float,native,super,while".Split(',');
            Java.fileExtension = ".java";
            Java.commentChar = "//";
            Java.supportsMultiLineComments = true;
            Java.multiLineCommentStart = "/*";
            Java.multiLineCommentEnd = "*/";
            Java.keywordColor = Color.Purple;
            Java.stringColor = Color.Blue;
        }
    }
}
