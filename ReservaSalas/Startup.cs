using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(ReservaSalas.Startup))]

namespace ReservaSalas
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "ReservaSalas");
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"bin\ReservaSalas.xml");
            });

            app.UseCors(CorsOptions.AllowAll);

            AtivarAccessTokens(app);

            app.UseWebApi(config);
        }

        private void AtivarAccessTokens(IAppBuilder app)
        {
            var confTK = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderTokenDeAcesso()
            };

            app.UseOAuthAuthorizationServer(confTK);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
