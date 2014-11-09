using SomeStore.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SomeStore.WebUI
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //for empty request: exmpl.dom
            routes.MapRoute(
                "Empty",
                "",
                new { Controller = "Product", action = "List", category=(string)null, page=1 }
             );
            //For pagination request: exmpl.dom/page_23
            routes.MapRoute(
                "Paginate",
                "page_{page}",
                new { Controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
        );
            //for categories request: exmpl.dom/books 
            routes.MapRoute(
                "Categories",
                "{category}",
                new { Controller = "Product", action = "List", page = 1 }
             );
            //for categories and pagination request: exmpl.dom/books/page_12 
            routes.MapRoute(
                "CategoriesAndPagination",
                "{category}/Page_{page}",
                new { Controller = "Product", action = "List" },
                new { page = @"\d+" }
             );


            //Default
            routes.MapRoute(
                "Default", // Имя маршрута
                "{controller}/{action}/{id}", // URL-адрес с параметрами
                new { controller = "Product", action = "List", id = UrlParameter.Optional } // Параметры по умолчанию
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Использовать LocalDB для Entity Framework по умолчанию
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Registration NinjectControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}