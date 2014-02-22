using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using MvcVsWebApi.Formatters;
using MvcVsWebApi.Handlers;
using MvcVsWebApi.Models;

namespace MvcVsWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services
            //// Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("xml", new MediaTypeHeaderValue("application/xml")));
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", new MediaTypeHeaderValue("application/json")));

            var bson = new BsonMediaTypeFormatter();
            bson.MediaTypeMappings.Add(new QueryStringMapping("type", "bson", new MediaTypeHeaderValue("application/bson")));
            bson.MediaTypeMappings.Add(new UriPathExtensionMapping("bson", new MediaTypeHeaderValue("application/bson")));
            config.Formatters.Add(bson);

            var csv = new CsvMediaTypeFormatter();
            csv.MediaTypeMappings.Add(new QueryStringMapping("type", "csv", new MediaTypeHeaderValue("text/csv")));
            csv.MediaTypeMappings.Add(new UriPathExtensionMapping("csv", new MediaTypeHeaderValue("text/csv")));
            config.Formatters.Add(csv);

            config.MessageHandlers.Add(new GenericHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            //for /api/batch
            config.Routes.MapHttpBatchRoute(
                routeName: "Batch",
                routeTemplate: "api/batch",
                batchHandler: new DefaultHttpBatchHandler(new HttpServer(config)));

            //for /api/odata
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Person>("Persons");
            config.Routes.MapODataRoute("OData", "api/odata", builder.GetEdmModel());

            //for /api/person/50.xml
            config.Routes.MapHttpRoute(
                name: "UrlExtensionApiWithId",
                routeTemplate: "api/{controller}/{id}.{ext}",
                defaults: new { id = RouteParameter.Optional }
            );

            //for /api/person.xml
            config.Routes.MapHttpRoute(
                name: "UrlExtensionApi",
                routeTemplate: "api/{controller}.{ext}"
            );

            //for /api/person
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
