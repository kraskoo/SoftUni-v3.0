namespace CarDealerApp
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using AutoMapper;
    using CarDealer.Models;
    using CarDealer.Models.ViewModels;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            this.RegisterMappers();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMappers()
        {
            Mapper.Initialize(expression =>
                {
                    expression.CreateMap<Customer, CustomerViewModel>();
                    expression.CreateMap<Car, CarViewModel>();
                    expression.CreateMap<Part, PartViewModel>();
                    expression.CreateMap<Car, CarWithPartsViewModel>();
                    expression.CreateMap<Supplier, SupplierViewModel>()
                        .ForMember(s => s.NumberOfParts, sup => sup.MapFrom(p => p.Parts.Count));
                    expression.CreateMap<Sale, SaleViewModel>();
                }
            );
        }
    }
}