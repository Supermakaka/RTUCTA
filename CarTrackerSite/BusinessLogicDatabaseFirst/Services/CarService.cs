using BusinessLogic.Helpers.HelperModels;
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

        #region Public

        public IEnumerable<DropDownListModel> GetUserCarsDropDown(int userId)
        {
            return base.GetMany(s => s.UserId == userId).Select(s => new DropDownListModel { Text = s.CarNumber, Value = s.Id.ToString()});
        }

        public IQueryable<Car> GetAll(int userId)
        {
            return base.GetMany(s => s.UserId == userId);
        }

        #endregion
    }

    public interface ICarService : IService<Car>
    {
        IEnumerable<DropDownListModel> GetUserCarsDropDown(int userId);
        IQueryable<Car> GetAll(int userId);
    }
}
