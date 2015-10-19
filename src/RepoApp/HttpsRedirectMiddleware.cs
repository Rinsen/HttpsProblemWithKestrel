using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;

namespace RepoApp
{
    public class HttpsRedirectMiddleware
    {
        readonly RequestDelegate _next;

        public HttpsRedirectMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.HttpContext.Request.IsHttps)
            {
                HandleNonHttpsRequest(context);
            }
            else
            {
                await _next(context);
            }
        }

        void HandleNonHttpsRequest(HttpContext context)
        {
            // only redirect for GET requests, otherwise the browser might not propagate the verb and request
            // body correctly.
            if (!string.Equals(context.Request.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                var newUrl = string.Concat(
                    "https://",
                    context.Request.Host.ToUriComponent(),
                    context.Request.PathBase.ToUriComponent(),
                    context.Request.Path.ToUriComponent(),
                    context.Request.QueryString.ToUriComponent());
                
                context.Response.Redirect(newUrl, permanent: true);
            }
        }
    }
}
