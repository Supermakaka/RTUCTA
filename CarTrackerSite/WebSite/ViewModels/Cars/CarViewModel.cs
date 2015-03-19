using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.ViewModels.Cars
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string CarManufacurer { get; set; }
        public string CarModel { get; set; }
        public int UserId { get; set; }
        public string CarNumber { get; set; }
        public string VINNumber { get; set; }
    }
}