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
        protected Action<string> _LogCallback = null;
        public LoggerConsoleWriter(Stream stream) : base(stream)
        {
            AutoFlush = true;
            Console.SetOut(this);
        }

        public virtual void RegisterLogCallback(Action<string> cb)
        {
            _LogCallback = cb;
        }

        public override void WriteLine(string value)
        {
            base.WriteLine(value);
            _LogCallback?.Invoke(value);
        }

    }
}
