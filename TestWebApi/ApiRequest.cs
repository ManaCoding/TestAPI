using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TestWebApi.Extension;

namespace TestWebApi
{
    public class ApiRequest : HttpRequest
    {
        HttpRequest _httpRequest;

        public ApiRequest(HttpRequest request)
        {
            this._httpRequest = request;
            this.Segments = request.Path.GetSegments();
        }

        public bool HasSegments { get { return this.Segments?.Length > 0; } }

        public string[] Segments { get; set; }
        public override Stream Body { get => _httpRequest.Body; set => _httpRequest.Body = value; }
        public override long? ContentLength { get => _httpRequest.ContentLength; set => _httpRequest.ContentLength = value; }
        public override string ContentType { get => _httpRequest.ContentType; set => _httpRequest.ContentType = value; }
        public override IRequestCookieCollection Cookies { get => _httpRequest.Cookies; set => _httpRequest.Cookies = value; }
        public override IFormCollection Form { get => _httpRequest.Form; set => _httpRequest.Form = value; }
        public override bool HasFormContentType => _httpRequest.HasFormContentType;
        public override IHeaderDictionary Headers => _httpRequest.Headers;
        public override HostString Host { get => _httpRequest.Host; set => _httpRequest.Host = value; }
        public override HttpContext HttpContext => _httpRequest.HttpContext;
        public override bool IsHttps { get => _httpRequest.IsHttps; set => _httpRequest.IsHttps = value; }
        public override string Method { get => _httpRequest.Method; set => _httpRequest.Method = value; }
        public override PathString Path { get => _httpRequest.Path; set => _httpRequest.Path = value; }
        public override PathString PathBase { get => _httpRequest.PathBase; set => _httpRequest.PathBase = value; }
        public override string Protocol { get => _httpRequest.Protocol; set => _httpRequest.Protocol = value; }
        public override IQueryCollection Query { get => _httpRequest.Query; set => _httpRequest.Query = value; }
        public override QueryString QueryString { get => _httpRequest.QueryString; set => _httpRequest.QueryString = value; }
        public override string Scheme { get => _httpRequest.Scheme; set => _httpRequest.Scheme = value; }
        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            return _httpRequest.ReadFormAsync(cancellationToken);
        }
    }
}
