﻿using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.ViewModels.Cars
{
    public class CarInfoViewModel
    {   
        public int ID { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public Highcharts SpeedChart { get; set; }
        public Highcharts AverageSpeedPerPeriod { get; set; }
        public Highcharts AverageSpeedPerAllTime { get; set; }
        public Highcharts AverageFuelCompsuntionPerTime { get; set; }
        public Highcharts DrivenKilometersPerPeriodChart { get; set; }
    }
}