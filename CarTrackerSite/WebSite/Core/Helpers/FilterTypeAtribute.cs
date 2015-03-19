using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterSettingsAttribute : ActionFilterAttribute
    {
        public string PropertyName { get; set; }
        public FilterItemTypeEnum Type { get; set; }
        public bool GlobalSearchAllowed { get; set; }

        public FilterSettingsAttribute()
        {
            PropertyName = String.Empty;
            Type = FilterItemTypeEnum.String;
            GlobalSearchAllowed = false;
        }
    }
}