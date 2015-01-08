using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CarService : BaseService<Car>, ICarService
    {
        public CarService(DataContext dataContext)
            : base(dataContext)
        {

        }
    }

    public interface ICarService : IService<Car>
    {

    }
}
