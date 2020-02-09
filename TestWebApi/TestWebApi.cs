using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebApi.Handler;
using TestWebApi.Services;

namespace TestWebApi
{
    public static class TestWebApi
    {
        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static IServiceCollection AddTestWebApi(this IServiceCollection services, WebHostBuilderContext context, HttpClient client)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (client == null) throw new ArgumentNullException(nameof(client));

            IConfiguration config = context.Configuration;

            // test
            var test = new TestService(config["Import:Key"]);

            // handlers
            ApiResponseHandlerCollection handlers = new ApiResponseHandlerCollection();

            // TODO: Make a builder
            handlers.Version = 1;
            handlers["/"] = new RootHandler();
            handlers["/testapi"] = new TestHandler(test);

            services.AddSingleton(handlers);

            return services;
        }

        public static IApplicationBuilder UseTestWebApi(this IApplicationBuilder app)
        {
            var handlers = app.ApplicationServices.GetRequiredService<ApiResponseHandlerCollection>();

            app.Run(async (context) =>
            {
                ApiResponse response;

                try
                {
                    response = await handlers.MakeResponseAsync(context);
                }
                catch (HttpRequestException ex)
                {
                    response = new ApiResponse(StatusCodes.Status400BadRequest, new { Error = ex.Message });
                }
                catch (Exception ex)
                {
                    response = new ApiResponse(StatusCodes.Status500InternalServerError, new { Error = ex.Message });
                }

                await context.Response.WriteJsonAsync(response);
                return;
            });

            return app;
        }

        private static async Task WriteJsonAsync(this HttpResponse httpResponse, ApiResponse apiResponse)
        {
            httpResponse.StatusCode = apiResponse.StatusCode;
            httpResponse.ContentType = "application/json";

            if (apiResponse.Object == null)
            {
                await httpResponse.WriteAsync(String.Empty);
            }
            else
            {
                await httpResponse.WriteAsync(JsonSerializer.Serialize(apiResponse.Object, _serializerOptions));
            }
        }
    }
}
