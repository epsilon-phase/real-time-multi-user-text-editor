using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLangLib
{
    class C:Language
    {
        internal C(){
            this.keywords = "auto,break,case,char,const,continue,default,do,double,else,enum,extern,float,for,goto,if,int,long,register,return,short,signed,sizeof,static,struct,switch,typedef,union,unsigned,void,volatile,while".Split(',');
            this.commentChar = "//";
            this.multiLineCommentStart = "/*";
            this.multiLineCommentEnd = "*/";
            this.supportsMultiLineComments = true;
            this.fileExtension = ".c";
        }
    }
}
