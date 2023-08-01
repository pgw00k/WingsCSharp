using GenOcean.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WingsCSharp.Test.CodeGenerator
{
    /// <summary>
    /// 生成源码文件的测试类
    /// <para>Test -- 生成对应的源文件</para>
    /// <para>Test2 -- 对对应的源文件进行处理</para>
    /// </summary>
    public class CodeGenerator
    {
        /// <summary>
        /// 测试用例的入口
        /// <para>需要运行一次，刷新 GenTest.g.cs 文件之后，才可以启用 Test2</para>
        /// </summary>
        public static void Test()
        {
            BaseCodeGenerator gen = new BaseCodeGenerator();
            gen.Namespace = "GenOcean.Code";
            gen.ClassName = "GenTest";
            gen.ParentClassName = "System.Object";
            gen.UseNamespaces.Add("System");
            gen.UseNamespaces.Add("System.Text");
            gen.UseNamespaces.Add("System.Collections");
            gen.UseNamespaces.Add("System.Collections.Generic");
            gen.MemberInfoes.Add(new ClassLinkPropertyInfo("float", "TestFloat"));
            gen.MemberInfoes.Add(new ClassLinkPropertyInfo("float", "TestFloat2", "5.00f"));

            using (StreamWriter sw = new StreamWriter("../../Test.CodeGenerator/GenTest.g.cs", false, Encoding.UTF8))
            {
                gen.Generate(sw);
            }

            foreach (var item in gen.MemberInfoes)
            {
                System.Console.WriteLine(item);
            }
            System.Console.ReadKey();
        }

        public static void Test2()
        {
            GenTest gt = new GenTest();

            System.Console.WriteLine(gt.TestFloat2* gt.TestFloat2);
            System.Console.ReadKey();
        }
    }
}
