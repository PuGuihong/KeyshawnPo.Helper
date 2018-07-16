using System.Web;
using System.Web.Mvc;

namespace KeyshawnPo.SwaggerDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
