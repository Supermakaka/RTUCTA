using BusinessLogic.Helpers;
using BusinessLogic.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebSite.Core.Helpers
{
    public class ChartHelper
    {
        public static Highcharts SpeedChart(Car car, IQueryable<Location> locations)
        {
            List<ChartDecimalDateClass> data = locations.Select(s => new ChartDecimalDateClass { ExecutionDate = s.Time, ExecutionValue = s.Speed }).ToList();
            object[,] chartData = new object[data.Count, 2];
            int i = 0;
            foreach (ChartDecimalDateClass item in data)
            {
                chartData.SetValue(item.ExecutionDate, i, 0);
                chartData.SetValue(item.ExecutionValue, i, 1);
                i++;
            }

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { Type = ChartTypes.Line })
                .SetTitle(new Title { Text = "Chart" })
                .SetXAxis(new XAxis { Type = AxisTypes.Datetime })
                .SetSeries(new Series { Data = new Data(chartData) });

            return chart;
        }

        public static Highcharts AverageSpeedPerSelectedPeriod(Car car, IQueryable<Location> locations, string chartName)
        {
            Highcharts chart = new Highcharts(chartName)
                .InitChart(new Chart
                {
                    Type = ChartTypes.Gauge,
                    PlotBackgroundColor = null,
                    PlotBackgroundImage = null,
                    PlotBorderWidth = 0,
                    PlotShadow = false
                })
                .SetTitle(new Title { Text = "Speedometer" })
                .SetPane(new Pane
                {
                    StartAngle = -150,
                    EndAngle = 150,
                    Background = new[]
                    {
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#FFF" }, { 1, "#333" } }
                            }),
                            BorderWidth = new PercentageOrPixel(0),
                            OuterRadius = new PercentageOrPixel(109, true)
                        },
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#333" }, { 1, "#FFF" } }
                            }),
                            BorderWidth = new PercentageOrPixel(1),
                            OuterRadius = new PercentageOrPixel(107, true)
                        },
                        new BackgroundObject(),
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#DDD")),
                            BorderWidth = new PercentageOrPixel(0),
                            OuterRadius = new PercentageOrPixel(105, true),
                            InnerRadius = new PercentageOrPixel(103, true)
                        }
                    }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 200,

                    //MinorTickInterval = "auto",
                    MinorTickWidth = 1,
                    MinorTickLength = 10,
                    MinorTickPosition = TickPositions.Inside,
                    MinorTickColor = ColorTranslator.FromHtml("#666"),
                    TickPixelInterval = 30,
                    TickWidth = 2,
                    TickPosition = TickPositions.Inside,
                    TickLength = 10,
                    TickColor = ColorTranslator.FromHtml("#666"),
                    Labels = new YAxisLabels
                    {
                        Step = 2,
                        //Rotation = "auto"
                    },
                    Title = new YAxisTitle { Text = "km/h" },
                    PlotBands = new[]
                    {
                        new YAxisPlotBands { From = 0, To = 120, Color = ColorTranslator.FromHtml("#55BF3B") },
                        new YAxisPlotBands { From = 120, To = 160, Color = ColorTranslator.FromHtml("#DDDF0D") },
                        new YAxisPlotBands { From = 160, To = 200, Color = ColorTranslator.FromHtml("#DF5353") }
                    }
                })
                .SetSeries(new Series
                {
                    Name = "Average speed / period",
                    Data = new Data(new object[] { locations.Select(s => s.Speed).Average() })
                });

            return chart;
        }

        public static Highcharts AverageFuelConsumption(Car car, IQueryable<Location> locations, string chartName)
        {
            Highcharts chart = new Highcharts(chartName)
                .InitChart(new Chart
                {
                    Type = ChartTypes.Gauge,
                    PlotBackgroundColor = null,
                    PlotBackgroundImage = null,
                    PlotBorderWidth = 0,
                    PlotShadow = false
                })
                .SetTitle(new Title { Text = "Fuel Consumption" })
                .SetPane(new Pane
                {
                    StartAngle = -150,
                    EndAngle = 150,
                    Background = new[]
                    {
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#FFF" }, { 1, "#333" } }
                            }),
                            BorderWidth = new PercentageOrPixel(0),
                            OuterRadius = new PercentageOrPixel(109, true)
                        },
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(new Gradient
                            {
                                LinearGradient = new[] { 0, 0, 0, 1 },
                                Stops = new object[,] { { 0, "#333" }, { 1, "#FFF" } }
                            }),
                            BorderWidth = new PercentageOrPixel(1),
                            OuterRadius = new PercentageOrPixel(107, true)
                        },
                        new BackgroundObject(),
                        new BackgroundObject
                        {
                            BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#DDD")),
                            BorderWidth = new PercentageOrPixel(0),
                            OuterRadius = new PercentageOrPixel(105, true),
                            InnerRadius = new PercentageOrPixel(103, true)
                        }
                    }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Max = 200,

                    //MinorTickInterval = "auto",
                    MinorTickWidth = 1,
                    MinorTickLength = 10,
                    MinorTickPosition = TickPositions.Inside,
                    MinorTickColor = ColorTranslator.FromHtml("#666"),
                    TickPixelInterval = 30,
                    TickWidth = 2,
                    TickPosition = TickPositions.Inside,
                    TickLength = 10,
                    TickColor = ColorTranslator.FromHtml("#666"),
                    Labels = new YAxisLabels
                    {
                        Step = 2,
                        //Rotation = "auto"
                    },
                    Title = new YAxisTitle { Text = "km/h" },
                    PlotBands = new[]
                    {
                        new YAxisPlotBands { From = 0, To = 120, Color = ColorTranslator.FromHtml("#55BF3B") },
                        new YAxisPlotBands { From = 120, To = 160, Color = ColorTranslator.FromHtml("#DDDF0D") },
                        new YAxisPlotBands { From = 160, To = 200, Color = ColorTranslator.FromHtml("#DF5353") }
                    }
                })
                .SetSeries(new Series
                {
                    Name = "Average speed / period",
                    Data = new Data(new object[] { locations.Select(s => s.Speed).Average() })
                });

            return chart;
        }
    }
}

public class ChartDecimalDateClass
{
    public decimal ExecutionValue { get; set; }
    public DateTime ExecutionDate { get; set; }
}
