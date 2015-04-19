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

        public void AddTroubleCodesToCar(Car car, List<string> troubleCodes)
        {
            List<TroubleCode> tempList = car.TroubleCodes.ToList();

            foreach (TroubleCode code in tempList)
                car.TroubleCodes.Remove(code);

            List<TroubleCode> codes = dataContext.TroubleCodes.Where(s => troubleCodes.Contains(s.DTCNumber)).ToList();

            foreach (TroubleCode code in codes)
                car.TroubleCodes.Add(code);

            dataContext.SaveChanges();
        }

        #endregion
    }

    public interface ICarService : IService<Car>
    {
        IEnumerable<DropDownListModel> GetUserCarsDropDown(int userId);
        IQueryable<Car> GetAll(int userId);
        void AddTroubleCodesToCar(Car car, List<string> troubleCodes);
    }
}
