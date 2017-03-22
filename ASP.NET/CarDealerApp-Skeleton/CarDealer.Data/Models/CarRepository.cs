namespace CarDealer.Data.Models
{
    using System.Data.Entity;
    using CarDealer.Models;
    using Interfaces;

    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(DbContext context) : base(context)
        {
        }
    }
}