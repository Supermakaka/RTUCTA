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

namespace WebSite.Controllers
{
    public class LocationController : BaseController
    {

        private ILocationService locationService;

        public LocationController(ILocationService locationService) 
        {
            this.locationService = locationService;
        }

        [HttpPost]
        public ActionResult RetriveDataFromAndroid(LocationViewModel locationVM)
        {
            var location = Mapper.Map<LocationViewModel, Location>(locationVM);
            
            locationService.Add(location);

            return Json(new { succes = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Map()
        {
            LocationViewModel location = new LocationViewModel();

            var carLocations = locationService.GetCarLocations(1);

            var carList = Mapper.Map<IEnumerable<Location>, IEnumerable<LocationViewModel>>(carLocations);

            return View(carList);
        }

        [HttpPost]
        public ActionResult Map(int str) 
        {
            return View();
        }

        [HttpPost]
        public ActionResult TraceDateInterval(String dateFrom, String dateTo)
        {
            LocationViewModel location = new LocationViewModel();

            var from = DateTime.ParseExact(RecivedDataParser(dateFrom) , "MMddyyyyhhmmtt", System.Globalization.CultureInfo.InvariantCulture);
            var to = DateTime.ParseExact(RecivedDataParser(dateTo), "MMddyyyyhhmmtt", System.Globalization.CultureInfo.InvariantCulture);

            var carLocations = locationService.GetCarLocationsByInterval(1, from, to);

            var carLocationsList = Mapper.Map<IEnumerable<Location>, IEnumerable<LocationViewModel>>(carLocations);

            return View(carLocationsList);
        }

        public String RecivedDataParser(String date)
        {
            string[] charsToRemove = new string[] { "/", ":", " " };

            foreach (var character in charsToRemove)
            {
                date = date.Replace(character, string.Empty);
            }

            return date;
        }
	}
}