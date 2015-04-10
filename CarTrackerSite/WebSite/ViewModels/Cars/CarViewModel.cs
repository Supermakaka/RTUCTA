using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSite.ViewModels.Cars
{
    public class CarViewModel
    {   
        [Display(Name="ID")]
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Car Manufacurer")]
        public string CarManufacurer { get; set; }

        [Required]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Car Number")]
        public string CarNumber { get; set; }

        [Required]
        [Display(Name = "VIN")]
        public string VINNumber { get; set; }

        [Display(Name = "Fuel Tank Capacity (l)")]
        public decimal FuelTankCapacity { get; set; }

        [Display(Name = "Fuel Consumption Urban (l/100km)")]
        public decimal FuelConsumptionUrban { get; set; }

        [Display(Name = "Fuel Consumption Extra Urban (l/100km)")]
        public decimal FuelConsumptionExtraUrban { get; set; }
    }
}