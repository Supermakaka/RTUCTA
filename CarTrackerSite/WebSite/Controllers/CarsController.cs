using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataTables.Mvc;
using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Core;
using WebSite.Core.Helpers;
using WebSite.ViewModels.Cars;
using WebSite.ViewModels.Shared;

namespace WebSite.Controllers
{
    public class CarsController : Controller
    {
        private ICarService carService;
        private ILocationService locationService;

        public CarsController(ICarService carsService, ILocationService locationService)
        {
            this.carService = carsService;
            this.locationService = locationService;
        }

        public ActionResult CarList()
        {   
            return View();
        }

        public ActionResult ListAjax(IDataTablesRequest request)
        {
            request = request ?? new DefaultDataTablesRequest();

            var query = carService.GetAll(HttpContextStorage.CurrentUser.Id);

            var res = DynamicFilter.Execute<Car>(query, FilteringParametersHelper.GetFilteringParams(typeof(CarsListViewModel), request));

            var model = Mapper.Map<IList<Car>, IList<CarsListViewModel>>(res.Items);

            return Json(new DataTablesResponse(request.Draw, model, res.TotalFilteredRecords, res.TotalRecords), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetCarData(int carId, DateTime? dateFrom, DateTime? dateTo)
        //{
        //    //Car car = carService.GetById(carId);

        //    //IQueryable<Location> locations = (dateFrom.HasValue && dateTo.HasValue) ? locationService.GetMany(s => s.CarId == carId && s.Time >= dateFrom && s.Time <= dateTo) 
        //    //    : locationService.GetMany(s => s.CarId == car.Id);

        //    //return Json(new {

        //    //    Success = true,
        //    //    SpeedArray = locations.Select(s => s.Speed).ToArray(),
        //    //    TimeArray = locations.Select(s => s.Time).ToArray(),
        //    //    CarNumber = car.CarNumber,
        //    //    CarId = car.Id

        //    //}, JsonRequestBehavior.AllowGet);
        //}

        #region Cars CRUD Actions

        public ActionResult Create()
        {
            return PartialView("/Views/Cars/PartialViews/Modals/Create.cshtml", new CarViewModel());
        }

        [HttpPost]
        public ActionResult Create(CarViewModel model)
        {   
            if(!ModelState.IsValid)
                return PartialView("/Views/Cars/PartialViews/Modals/Create.cshtml", model);

            carService.Add(Mapper.Map<CarViewModel, Car>(model));

            return Json(new { success = true} );
        }

        public ActionResult Edit(int id)
        {
            return PartialView("/Views/Cars/PartialViews/Modals/Edit.cshtml", Mapper.Map<Car, CarViewModel>(carService.GetById(id)));
        }
        
        [HttpPost]
        public ActionResult Edit(CarViewModel model)
        {
            Car car = carService.GetById(model.Id.Value);
            
            carService.Update(Mapper.Map<CarViewModel, Car>(model, car));

            return Json(new { success = true });
        }

        public ActionResult Delete(int id)
        {   
            return PartialView("/Views/Shared/PartialViews/Modals/Delete.cshtml", Mapper.Map<Car, DeleteApproveViewModel >(carService.GetById(id)));
        }

        public ActionResult DeleteById(int id)
        {
            Car car = carService.GetById(id);

            carService.Delete(car);

            return Json(new { success = true });
        }

        public ActionResult CarInfo(int id, DateTime? dateFrom, DateTime? dateTo)
        {
            Car car = carService.GetById(id);

            IQueryable<Location> locations = (dateFrom.HasValue && dateTo.HasValue) ? locationService.GetMany(s => s.CarId == id && s.Time >= dateFrom && s.Time <= dateTo)
                : locationService.GetMany(s => s.CarId == car.Id);

            Highcharts model = ChartHelper.SpeedChart(car, locations);

            return View(CarInfoViewModelFactory(id, dateFrom, dateTo));
        }

        public ActionResult UpdateCarInfoView(int id, DateTime? dateFrom, DateTime? dateTo)
        {
            return View(CarInfoViewModelFactory(id, dateFrom, dateTo));
        }

        public CarInfoViewModel CarInfoViewModelFactory(int id, DateTime? dateFrom, DateTime? dateTo)
        {
            Car car = carService.GetById(id);

            IQueryable<Location> locations = (dateFrom.HasValue && dateTo.HasValue) ? locationService.GetMany(s => s.CarId == id && s.Time >= dateFrom && s.Time <= dateTo)
                : locationService.GetMany(s => s.CarId == car.Id);

            IQueryable<Location> AllLocations = locationService.GetMany(s => s.CarId == car.Id);

            CarInfoViewModel model = new CarInfoViewModel();

            model.SpeedChart = ChartHelper.SpeedChart(car, locations);
            model.AverageSpeedPerPeriod = ChartHelper.AverageSpeedPerSelectedPeriod(car, locations, "chart2");
            model.AverageSpeedPerAllTime = ChartHelper.AverageSpeedPerSelectedPeriod(car, AllLocations, "chart3");
            model.AverageFuelCompsuntionPerAllTime = ChartHelper.AverageFuelConsumption(car, AllLocations, "chart4");
            model.AverageFuelCompsuntionPerSelectedPeriod = ChartHelper.AverageFuelConsumption(car, locations, "chart5");
            model.ThrotleChart = ChartHelper.ThrotleChart(car, locations);
            model.MillageChart = ChartHelper.MillageChart(car, locations);

            return model;
        }

        #endregion
    }
}