using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSite.ViewModels.Locations;

namespace WebSite.Controllers
{
    public class DataController : ApiController
    {
        private ILocationService locationService; 

        public DataController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public HttpResponseMessage GetData(int id)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "");

            return response;
        }

        public HttpResponseMessage RetriveDataFromAndroid()
        {
            //var location = Mapper.Map<LocationViewModel, Location>(locationVM);

            //locationService.Add(location);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "");

            return response;
        }
    }
}
