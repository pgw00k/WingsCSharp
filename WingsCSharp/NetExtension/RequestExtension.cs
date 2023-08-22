using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace System.Net
{
    public static class RequestExtension
    {
        public static string ToStringEx(this HttpListenerRequest request)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string key in request.Headers.AllKeys)
            {
                sb.AppendLine($"{key}:{request.Headers[key]}");
            }
            return sb.ToString();
        }
    }
}
