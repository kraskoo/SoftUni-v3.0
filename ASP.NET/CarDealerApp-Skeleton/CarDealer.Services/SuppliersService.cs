namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data;
    using Data.Interfaces;
    using Models;
    using Models.ViewModels;

    public class SuppliersService : Service
    {
        public SuppliersService(IDataProvidable data) : base(data)
        {
        }

        public SuppliersService() : this(new CarDealerData(new CarDealerContext()))
        {
            
        }

        public IEnumerable<SupplierViewModel> GetSupplier(string type)
        {
            IEnumerable<Supplier> suppliers;

            if (type == "local")
            {
                suppliers = this.Data.Suppliers.GetAll(s => !s.IsImporter);
            }
            else if (type == "importers")
            {
                suppliers = this.Data.Suppliers.GetAll(s => s.IsImporter);
            }
            else
            {
                throw new ArgumentException("Unknown type.");
            }

            return Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierViewModel>>(suppliers);
        }
    }
}