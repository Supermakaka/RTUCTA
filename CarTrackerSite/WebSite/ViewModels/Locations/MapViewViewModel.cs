using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.ViewModels.Locations
{
    public class MapViewViewModel
    {
        public IEnumerable<LocationViewModel> LocationModel { get; set; }
        public IEnumerable<SelectListItem> CarList { get; set; }
    }
}