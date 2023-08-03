using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net
{
    public static class RequestExtension
    {
        public static string ToStringEx(this HttpListenerRequest request)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string headerKey in request.Headers.AllKeys)
            {
                sb.AppendLine($"{headerKey}:{request.Headers[headerKey]}");
            }

            return sb.ToString();
        }
    }
}
