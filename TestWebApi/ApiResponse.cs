using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi
{
    public class ApiResponse
    {
        [Obsolete]
        public ApiResponse(int statusCode) : this(statusCode, null) { }

        public ApiResponse(int statusCode, object obj)
        {
            StatusCode = statusCode;
            Object = obj;
        }

        public int StatusCode { get; set; }
        public object Object { get; set; }
    }
}
