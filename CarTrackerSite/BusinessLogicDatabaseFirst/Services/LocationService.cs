using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Infrastructure;

using BCrypt.Net;

namespace BusinessLogic.Services
{
    public class LocationService : BaseService<Location>, ILocationService
    {
        public LocationService(IDataContext dataContext) 
            : base(dataContext) 
        {
        }

        public IEnumerable<Location> GetCarLocations(int carId)
        {
            return from locationContext in dataContext.Locations
                   where locationContext.CarId == carId
                   select locationContext;
        }

        public IEnumerable<Location> GetCarLocationsByInterval(int carId, DateTime dateFrom, DateTime dateTo)
        {
            return from locationContext in dataContext.Locations
                   where locationContext.Time > dateFrom
                   && locationContext.Time < dateTo
                   && locationContext.CarId == carId
                   select locationContext;
        }

    }

    public interface ILocationService : IService<Location>
    {
        IEnumerable<Location> GetCarLocations(int carId);
        IEnumerable<Location> GetCarLocationsByInterval(int carId, DateTime dateFrom, DateTime dateTo);
    }
}
