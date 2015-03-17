using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.ViewModels.Locations;
using BusinessLogic.Models;
using BusinessLogic.Infrastructure;
using BusinessLogic.Services;
using AutoMapper;
using WebSite.Core.Helpers;
using WebSite.Core;

namespace WebSite.Controllers
{
    public class LocationController : BaseController
    {

        private ILocationService locationService;
        private ICarService carService;

        public LocationController(ILocationService locationService, ICarService carService)
        {
            this.locationService = locationService;
            this.carService = carService;
        }

        [HttpPost]
        public ActionResult RetriveDataFromAndroid(LocationViewModel locationVM)
        {
            var location = Mapper.Map<LocationViewModel, Location>(locationVM);

            locationService.Add(location);

            return Json(new { succes = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Map()
        {
            MapViewViewModel location = new MapViewViewModel();

            var carLocations = locationService.GetCarLocations(1);

            location.LocationModel = Mapper.Map<IEnumerable<Location>, IEnumerable<LocationViewModel>>(carLocations);

            location.CarList = DropDownHelper.ConvertToSelectListItemIEnumerable(carService.GetUserCarsDropDown(HttpContextStorage.CurrentUser.Id));

            return View(location);
        }

        [HttpPost]
        public ActionResult Map(int str)
        {
            return View();
        }

        [HttpPost]
        public ActionResult TraceDateInterval(DateTime dateFrom, DateTime dateTo)
        {
            LocationViewModel location = new LocationViewModel();

            var carLocations = locationService.GetCarLocationsByInterval(1, dateFrom, dateTo);

            var carLocationsList = Mapper.Map<IEnumerable<Location>, IEnumerable<LocationViewModel>>(carLocations);

            return Json(new { success = true, locations = carLocationsList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CurrentLocationDate()
        {
            Location location = locationService.GetCurrentLocation(1);

            return Json(new { success = true, longitude = location.Longitude, latitude = location.Latitude }, JsonRequestBehavior.AllowGet);
        }
    }
}