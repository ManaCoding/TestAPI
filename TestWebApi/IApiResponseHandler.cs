using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi
{
    public interface IApiResponseHandler
    {
        Task<ApiResponse> MakeResponseAsync(ApiRequest request);
    }
}
