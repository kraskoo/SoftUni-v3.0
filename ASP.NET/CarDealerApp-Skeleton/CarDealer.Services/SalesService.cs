namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Interfaces;
    using Models;
    using Models.ViewModels;
    using System;

    public class SalesService : Service
    {
        public SalesService(IDataProvidable data) : base(data)
        {
        }

        public SalesService() : this(new CarDealerData(new CarDealerContext()))
        {
        }

        public IEnumerable<SaleViewModel> GetAllSales()
        {
            return Mapper
                .Map<IEnumerable<Sale>, IEnumerable<SaleViewModel>>(
                    this.Data
                    .Sales
                    .GetAll()
                    .Include(s => s.Customer)
                    .Include(s => s.Car));
        }

        public SaleViewModel SaleById(int id)
        {
            return Mapper.Map<Sale, SaleViewModel>(this.Data.Sales.Find(id));
        }

        public IEnumerable<SaleViewModel> AllDiscountedSales(int? percentage = null)
        {
            return Mapper
                .Map<IEnumerable<Sale>, IEnumerable<SaleViewModel>>(
                    this.Data
                    .Sales
                    .GetAll(s => percentage == null ? s.Discount > 0 : (int)(s.Discount * 100) == percentage.Value)
                    .Include(s => s.Customer)
                    .Include(s => s.Car));
        }
    }
}