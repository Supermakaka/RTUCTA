using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace WebSite.ViewModels.Locations
{
    public class LocationViewModel
    {
        [Display(Name="ID")]
        public string Id { get; set; }
        
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Time")]
        public string Time { get; set; }

        [Display(Name = "Altitude")]
        public string Altitude { get; set; }

        [Display(Name = "Accuracy")]
        public string Accuracy { get; set; }

        [Display(Name = "Speed")]
        public string Speed { get; set; }

        [Display(Name = "Car ID")]
        public string CarId { get; set; }
        
        [Display(Name = "Milage")]
        public string Mileage { get; set; }
        
        [Display(Name = "Fuel Tank Capacity")]
        public string FuelTank { get; set; }
        
        [Display(Name = "")]
        public string IsTurnedOn { get; set; }

        public string TroubleCodes { get; set; }
        
        [Display(Name = "Throtle")]
        public string Throtle { get; set; }
        
        [Display(Name = "Fuel Consumption")]
        public string FuelConsumption { get; set; }
    }
}