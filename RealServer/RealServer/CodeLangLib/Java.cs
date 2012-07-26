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
            this.keywords = "abstract,continue,for,new,switch,assert,default,goto,package,synchronized,boolean,do,if,private,this,break,double,implements,protected,throw,byte,else,import,public,throws,case,enum,instanceof,return,transient,catch,extends,int,short,try,char,final,interface,static,void,class,finally,long,strictfp,volatile,const,float,native,super,while".Split(',');
            this.fileExtension = ".java";
            this.commentChar = "//";
            this.supportsMultiLineComments = true;
            this.multiLineCommentStart = "/*";
            this.multiLineCommentEnd = "*/";
            this.keywordColor = Color.Purple;
            this.stringColor = Color.Blue;
        }
    }
}
