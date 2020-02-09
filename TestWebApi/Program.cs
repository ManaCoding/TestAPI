using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();

            // web host
            IWebHost host = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
               .ConfigureServices((context, services) =>
               {
                   // use lowercase routes
                   services.AddRouting(options =>
                   {
                       options.LowercaseUrls = true;
                   });

                   // web api
                   services
                       .AddMvcCore()
                       .AddApplicationPart(Assembly.GetCallingAssembly());

                   // CORS
                   services.AddCors(options =>
                   {
                       options.AddDefaultPolicy(
                           builder =>
                           {
                               builder
                                   .AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                           }
                       );
                   });

                   // customization
                   services.AddTestWebApi(context, httpClient);
                   
               })
               .ConfigureAppConfiguration((context, config) =>
               {
                   // blank
               })
               .Configure(app =>
               {
                   IHostEnvironment env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();

                   // development environment
                   if (env != null && env.IsDevelopment())
                   {
                       app.UseDeveloperExceptionPage();
                   }

                   // CORS
                   app.UseCors();

                   // customizations
                   app.UseTestWebApi();
               })
               .Build();

            host.Run();
        }
    }
}
