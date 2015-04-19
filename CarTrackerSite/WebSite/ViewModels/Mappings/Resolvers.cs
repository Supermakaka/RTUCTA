using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

namespace WebSite.ViewModels.Mappings
{
    public class DateTimeToFormattedStringResolver : ValueResolver<DateTime?, string>
    {
        protected override string ResolveCore(DateTime? source)
        {
            if (!source.HasValue)
                return "";

            return source.Value.ToString("MM/dd/yyyy h:mm tt");
        }
    }

    //public class StringToDatetimeResolver : ValueResolver<string, DateTime?>
    //{
    //    protected override DateTime ResolveCore(string source)
    //    {
    //        if (!String.IsNullOrEmpty(source))
    //            return DateTime.UtcNow;

    //        return DateTime.ParseExact(source, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture;
    //    }
    //}

    public class DateToFormattedStringResolver : ValueResolver<DateTime?, string>
    {
        protected override string ResolveCore(DateTime? source)
        {
            if (!source.HasValue)
                return "";

            return source.Value.ToString("MM/dd/yyyy");
        }
    }

    public class ThrotleResolver : ValueResolver<string, decimal>
    {
        protected override decimal ResolveCore(string source)
        {
            if (source.Equals("NO DATA"))
                return 0.0M;

            source = source.Remove(source.Length - 4);

            return decimal.Parse(source);
        }
    }

    public class SpeedResolver : ValueResolver<string, decimal>
    {
        protected override decimal ResolveCore(string source)
        {
            if (source.Equals("NO DATA"))
                return 0.0M;

            source = source.Remove(source.Length - 4);

            return decimal.Parse(source);
        }
    }
    public class FuelTankResolver : ValueResolver<string, decimal>
    {
        protected override decimal ResolveCore(string source)
        {
            if (source.Equals("NO DATA"))
                return 75.0M;

            return decimal.Parse(source);
        }
    }
}