using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi
{
    public class ApiResponseHandlerCollection : IApiResponseHandler
    {
        private readonly Dictionary<string, IApiResponseHandler> _collection;

        public ApiResponseHandlerCollection()
        {
            _collection = new Dictionary<string, IApiResponseHandler>();
        }

        public int Version { get; set; }

        public IApiResponseHandler this[string key]
        {
            get { return _collection[key]; }
            set { _collection[key] = value; }
        }

        public Task<ApiResponse> MakeResponseAsync(HttpContext context)
        {
            ApiRequest request = new ApiRequest(context.Request);
            return this.MakeResponseAsync(request);
        }

        public async Task<ApiResponse> MakeResponseAsync(ApiRequest request)
        {
            // root
            if (!request.HasSegments)
            {
                if (!_collection.ContainsKey("/"))
                {
                    return new ApiResponse(StatusCodes.Status404NotFound, null);
                }

                return await _collection["/"].MakeResponseAsync(request);
            }

            if (!_collection.ContainsKey(request.Segments[0]))
            {
                return new ApiResponse(StatusCodes.Status404NotFound, null);
            }

            return await _collection[request.Segments[0]].MakeResponseAsync(request);
        }
    }
}
