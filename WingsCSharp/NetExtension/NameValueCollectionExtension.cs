using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Specialized
{
    public static class NameValueCollectionExtension
    {
        public static string ToStringEx(this NameValueCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in collection.Keys)
            {
                sb.AppendLine($"{key}:{collection[key]}");
            }
            return sb.ToString();
        }
    }
}
