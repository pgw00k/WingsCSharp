using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenOcean.Common
{
    public class LoggerConsoleWriter : StreamWriter
    {
        public LoggerConsoleWriter(Stream stream) : base(stream)
        {
            AutoFlush = true;
            Console.SetOut(this);
        }
    }
}
