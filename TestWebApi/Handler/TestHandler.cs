using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebApi.Models;
using TestWebApi.Services;

namespace TestWebApi.Handler
{
    public class TestHandler : IApiResponseHandler
    {
        private readonly TestService testService;
        public TestHandler(TestService testService) => this.testService = testService;
        public async Task<ApiResponse> MakeResponseAsync(ApiRequest request)
        {
            if (!request.HasSegments) throw new ArgumentException("Must have at least one segment", nameof(request));

            var segments = request.Segments;

            switch (request.Method)
            {
                case "GET":
                    
                    switch (segments.Length)
                    {
                        // Path : /tests
                        case 1:
                            var tests = await testService.GetTests();
                            return await Task.FromResult(new ApiResponse(StatusCodes.Status200OK, tests));
                        // Path : /tests/{id}
                        case 2:
                            var test = await testService.GetTestById(int.Parse(request.Segments[1].Replace(@"/", string.Empty)));
                            return new ApiResponse(StatusCodes.Status200OK, test);

                        default: return new ApiResponse(StatusCodes.Status404NotFound, null);
                    }
                case "POST":
                    // POST Method
                    return new ApiResponse(StatusCodes.Status404NotFound, null);

                case "PUT":
                    //PUT Method
                    return new ApiResponse(StatusCodes.Status404NotFound, null);

                case "DELETE":
                    //DELETE Method
                    return new ApiResponse(StatusCodes.Status404NotFound, null);

                default:
                    return new ApiResponse(StatusCodes.Status404NotFound, null);
            }
        }
    }
}
