namespace CarDealer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Interfaces;
    using Models;
    using Models.ViewModels;

    public class CarsService : Service
    {
        public CarsService(IDataProvidable data) : base(data)
        {
        }

        public CarsService() : this(new CarDealerData(new CarDealerContext()))
        {
        }

        public IEnumerable<CarViewModel> GetAllCars()
        {
            return Mapper
                .Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(
                    this.Data
                    .Cars
                    .GetAll()
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance));
        }

        public IEnumerable<CarViewModel> GetCarFromMake(string make)
        {
            IEnumerable<Car> cars = this.Data.Cars.GetAll(c => c.Make == make);
            return Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(cars);
        }

        public CarWithPartsViewModel GetCarWithPars(int id)
        {
            var car = this.Data.Cars.Find(id);
            return Mapper.Map<Car, CarWithPartsViewModel>(car);
        }
    }
}