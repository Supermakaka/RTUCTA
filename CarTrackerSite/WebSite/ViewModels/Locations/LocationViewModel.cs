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
        public int? Id { get; set; }
        
        [Display(Name = "Longitude")]
        public decimal? Longitude { get; set; }

        [Display(Name = "Latitude")]
        public decimal? Latitude { get; set; }

        [Display(Name = "Time")]
        public System.DateTime? Time { get; set; }

        [Display(Name = "Altitude")]
        public decimal? Altitude { get; set; }

        [Display(Name = "Accuracy")]
        public decimal? Accuracy { get; set; }

        [Display(Name = "Speed")]
        public decimal? Speed { get; set; }

        [Display(Name = "Car ID")]
        public int? CarId { get; set; }

    }
}