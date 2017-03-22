namespace CarDealerApp
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Start Page",
                "",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                "Customer In Order View",
                "customers/all/{order}",
                new
                {
                    controller = "Customers",
                    action = "All",
                    order = "ascending"
                },
                new { order = @"ascending|descending" }
            );

            routes.MapRoute(
                "Cars makes",
                "cars/make/{make}",
                new
                {
                    controller = "Cars",
                    action = "Make"
                }
            );

            routes.MapRoute(
                "Car with parts",
                "cars/{id}/parts",
                new
                {
                    controller = "Cars",
                    action = "Parts",
                    id = "{id}"
                }
            );

            routes.MapRoute(
                "All Cars",
                "cars/all",
                new
                {
                    controller = "Cars",
                    action = "All"
                }
            );

            routes.MapRoute(
                "Suppliers by Type",
                "suppliers/{type}",
                new
                {
                    controller = "Suppliers",
                    action = "GetSuppliers",
                    type = "{type}"
                }
            );

            routes.MapRoute(
                "Customers by Id",
                "customers/{id}",
                new
                {
                    controller = "Customers",
                    action = "SalesByCustomer",
                    id = "{id}"
                }
            );

            routes.MapRoute(
                "All Sales",
                "sales",
                new
                {
                    controller = "Sales",
                    action = "GetAllSales"
                }
            );

            routes.MapRoute(
                "Discounted Sales",
                "sales/discounted/{percentage}",
                new
                {
                    controller = "Sales",
                    action = "GetDiscounted",
                    percentage = "{percentage}"
                }
            );

            routes.MapRoute(
                "Sale By Id",
                "sales/{id}",
                new
                {
                    controller = "Sales",
                    action = "SaleById",
                    id = "{id}"
                }
            );
        }
    }
}