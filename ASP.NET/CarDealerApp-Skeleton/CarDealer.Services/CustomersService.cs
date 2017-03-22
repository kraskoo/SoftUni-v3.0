using System.Data.Entity;

namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Interfaces;
    using Models;
    using Models.ViewModels;

    public class CustomersService : Service
    {
        public CustomersService(IDataProvidable data) : base(data)
        {
        }

        public CustomersService() : this(new CarDealerData(new CarDealerContext()))
        {
        }

        public SalesViewModel GetSalesByCustomerId(int id)
        {
            var customer = this.Data.Customers.Find(id);
            var customerName = customer.Name;
            var sales = this.Data
                .Sales
                .GetAll(s => s.Customer.Id == id)
                .Include(s => s.Car)
                .ToArray()
                .Select(s => new
                {
                    s.Car.Make,
                    s.Car.Model,
                    s.Discount,
                    CarPrice = (decimal)s.Car.Parts.Sum(p => p.Price.GetValueOrDefault() * p.Quantity)
                });

            return new SalesViewModel
            {
                CustomerId = id,
                CustomerName = customerName,
                SoldCars = sales.Select(s => new SoldCarViewModel
                {
                    MakeAndModel = $"{s.Make} {s.Model}",
                    Discount = s.Discount,
                    PriceWithDiscount = s.CarPrice - ((decimal)s.Discount * s.CarPrice),
                    PriceWithoutDiscount = s.CarPrice
                })
            };
        }

        public IEnumerable<CustomerViewModel> GetCustomersInOrder(string order)
        {
            IEnumerable<Customer> customers;
            if (order == "ascending")
            {
                customers = this.Data
                    .Customers
                    .GetAll()
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.Name);
            }
            else if (order == "descending")
            {
                customers = this.Data
                    .Customers
                    .GetAll()
                    .OrderByDescending(c => c.BirthDate)
                    .ThenBy(c => c.Name);
            }
            else
            {
                throw new ArgumentException("Unknown order");
            }

            return Mapper
                .Instance
                .Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(customers);
        }
    }
}