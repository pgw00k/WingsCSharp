using System;
using System.Collections.Generic;
using System.Text;

namespace GenOcean.Code
{
    public interface IClassMemberInfo
    {
        string Name { get; set; }
        string Type { get; set; }
        string DefaultValue { get; set; }
        string Comment { get; set; }
    }

    public class ClassMemberInfo: IClassMemberInfo
    {
        protected string _Level = "protected";
        protected string _Type = "int";
        protected string _Name = "_nInt";
        protected string _DefaultValue = string.Empty;
        protected string _Comment = string.Empty;

        public string Level { get { return _Level; } set { _Level = value; } }
        public string Type { get { return _Type; } set { _Type = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string DefaultValue { get { return _DefaultValue; } set { _DefaultValue = value; } }
        public string Comment { get { return _Comment; } set { _Comment = value; } }

        public ClassMemberInfo(
            string level = "protected", 
            string type = "int", 
            string name = "_nInt", 
            string defaultValue = "", 
            string comment = ""
            )
        {
            Level = level;
            Type = type;
            Name = name;
            DefaultValue = defaultValue;
            Comment = comment;
        }

        public override bool Equals(object obj)
        {
            IClassMemberInfo other = obj as IClassMemberInfo;
            if (other == null)
            {
                return false;
            }
            return other.Name.Equals(Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public virtual string GetComment()
        {
            string finalComment = "";
            if (!string.IsNullOrEmpty(Comment))
            {
                if (!Comment.StartsWith("//") && !Comment.StartsWith("/*"))
                {
                    if (Comment.Contains("\n"))
                    {
                        finalComment = $"/*\n{Comment}\n*/";
                    }
                    else
                    {
                        finalComment = $"//{Comment}";
                    }
                }
                else
                {
                    finalComment = Comment;
                }

                if (!finalComment.EndsWith("\n"))
                {
                    finalComment += "\n";
                }
            }

            return finalComment;
        }
    }

    public class ClassFieldInfo : ClassMemberInfo
    {
        public override string ToString()
        {
            string finalComment = GetComment();

            string finalResult = $"{finalComment}{Level} {Type} {Name}";
            if (!string.IsNullOrEmpty(DefaultValue))
            {
                finalResult += $" = {DefaultValue}";
            }

            finalResult += ";";

            return finalResult;
        }
    }

    /// <summary>
    /// 属性
    /// <para>属性作为接口存在，其构造字符串时并不会自动添加 '=' 号，如果需要自带初始化值（不设置 get/set），需要在DefaultValue中手动加入 '=' 号</para>
    /// </summary>
    public class ClassPropertyInfo : ClassMemberInfo
    {
        protected string _Level = "public";
        protected string _Name = "nInt";
        protected string _DefaultValue = "{get;set;}";

        public ClassPropertyInfo(
            string level = "public",
            string type = "int",
            string name = "nInt",
            string defaultValue = "{get;set;}",
            string comment = ""
            ):base(level,type, name, defaultValue, comment)
        {
        }

        public override string ToString()
        {
            string finalComment = GetComment();
            return $"{finalComment}{Level} {Type} {Name} {DefaultValue};";
        }
    }

    /// <summary>
    /// 特殊的属性对象，会连带生成一个字段，同时会改变Getter和Setter的内容
    /// </summary>
    public class ClassLinkPropertyInfo : ClassPropertyInfo
    {
        protected ClassFieldInfo LinkFieldInfo;

        public ClassLinkPropertyInfo(
            string type = "int",
            string name = "_nInt",
            string defaultValue = ""
            )
        {
            Type = type;
            Name = name;

            LinkFieldInfo = new ClassFieldInfo();
            LinkFieldInfo.Type = Type;
            LinkFieldInfo.Name = $"_{Name}";
            LinkFieldInfo.DefaultValue = defaultValue;
        }

        public override string ToString()
        {
            string finalComment = GetComment();
            return $"{LinkFieldInfo}\n" +
                $"{finalComment}{Level} {Type} {Name} \n" +
                $"{{\n get{{return {LinkFieldInfo.Name};}}" +
                $"\n set{{{LinkFieldInfo.Name}=value;}}\n}}";
        }
    }
}
