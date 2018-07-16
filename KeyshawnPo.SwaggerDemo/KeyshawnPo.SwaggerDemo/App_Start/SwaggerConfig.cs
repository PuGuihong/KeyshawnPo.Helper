using System.Web.Http;
using WebActivatorEx;
using KeyshawnPo.SwaggerDemo;
using Swashbuckle.Application;
using KeyshawnPo.SwaggerDemo.App_Start;
using System;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace KeyshawnPo.SwaggerDemo
{
    public class SwaggerConfig
    {

        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            var SystemName = ConfigClient.CurrentConfig.SystemName;
            if (string.IsNullOrEmpty(SystemName)) SystemName = "";

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", $"{SystemName} APIÃèÊöÎÄµµ V1.0.0");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.IncludeXmlComments(GetModelXmlCommentsPath());
                    c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(thisAssembly, "GoodPartner.Portal.WebApi.Scripts.swaggerui.swagger_lang.js");
                });
        }
        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}\bin\GoodPartner.Portal.WebApi.XML", AppDomain.CurrentDomain.BaseDirectory);
        }

        private static string GetModelXmlCommentsPath()
        {
            return String.Format(@"{0}\bin\GoodPartner.Portal.Model.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
