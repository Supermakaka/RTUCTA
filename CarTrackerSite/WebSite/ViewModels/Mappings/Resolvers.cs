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

    public class DateToFormattedStringResolver : ValueResolver<DateTime?, string>
    {
        protected override string ResolveCore(DateTime? source)
        {
            if (!source.HasValue)
                return "";

            return source.Value.ToString("MM/dd/yyyy");
        }
    }
}