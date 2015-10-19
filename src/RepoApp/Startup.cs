
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace RepoApp
{
    public class Startup
    {

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC services to the services container.
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<HttpsRedirectMiddleware>();

            // Add MVC to the request pipeline.
            app.UseMvcWithDefaultRoute();
        }
    }
}
