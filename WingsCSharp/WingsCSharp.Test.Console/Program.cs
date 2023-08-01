using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenOcean.Code;

namespace WingsCSharp.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WingsCSharp.Test.CodeGenerator.CodeGenerator.Test();
            //WingsCSharp.Test.CodeGenerator.CodeGenerator.Test2();
        }
    }
}
