using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataTables.Mvc;
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

        public CarsController(ICarService carsService)
        {
            this.carService = carsService;
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

        #endregion
    }
}