using BusinessLogic.Models;
using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.ViewModels.Cars
{
    public class CarInfoViewModel
    {   
        public int ID { get; set; }
        public Car UserCar { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public Highcharts SpeedChart { get; set; }
        public Highcharts AverageSpeedPerPeriod { get; set; }
        public Highcharts AverageSpeedPerAllTime { get; set; }
        public Highcharts AverageFuelCompsuntionPerSelectedPeriod { get; set; }
        public Highcharts AverageFuelCompsuntionPerAllTime { get; set; }
        public Highcharts ThrotleChart { get; set; }
        public Highcharts MillageChart { get; set; }
        public Highcharts DrivenKilometersPerPeriodChart { get; set; }
    }
}