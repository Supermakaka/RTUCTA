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
    }
}