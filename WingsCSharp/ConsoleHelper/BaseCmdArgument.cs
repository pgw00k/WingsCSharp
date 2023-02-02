using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenOcean.Common
{
    public class BaseCmdArgument
    {
        public delegate int OptionAction(BaseCmdArgument self, string arg, int argIndex);

        public string[] Arguments;

        public Dictionary<string, OptionAction> OptionActions = new Dictionary<string, OptionAction>();

        public BaseCmdArgument(string[] args)
        {
            Arguments = args;
        }

        public virtual void ProcessArgs()
        {
            int count = Arguments.Length;
            for (int i = 0; i < count; i++)
            {
                var arg = Arguments[i];
                if(arg.StartsWith("-"))
                {
                    arg = arg.ToLower();
                    if(OptionActions.TryGetValue(arg,out OptionAction act))
                    {
                        i+=act.Invoke(this, arg, i);
                    }else
                    {
                        Console.WriteLine($"Argument {arg} is not defined.");
                    }
                }
            }
        }
    }
}
