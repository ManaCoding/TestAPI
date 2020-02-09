using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Handler
{
    public class RootHandler : IApiResponseHandler
    {
        public async Task<ApiResponse> MakeResponseAsync(ApiRequest request)
        {
            switch (request.Method)
            {
                case "GET":
                    return new ApiResponse(StatusCodes.Status200OK, "Web API Developer Console API");
                default:
                    return await Task.FromResult(new ApiResponse(StatusCodes.Status405MethodNotAllowed, null));
            }
        }
    }
}
