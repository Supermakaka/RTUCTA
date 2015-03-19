using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Core.Helpers;

namespace WebSite.ViewModels.Cars
{
    public class CarsListViewModel
    {
        [FilterSettings(Type = FilterItemTypeEnum.Number)]
        public int Id { get; set; }

        public string CarManufacurer { get; set; }
        public string CarModel { get; set; }

        [FilterSettings(PropertyName = "User.FirstName", GlobalSearchAllowed = true)]
        public string UserName { get; set; }

        [FilterSettings(PropertyName = "User.Email", GlobalSearchAllowed = true)]
        public string UserEmail { get; set; }
        public string CarNumber { get; set; }
        public string VINNumber { get; set; }
    }
}