using BusinessLogic.Helpers.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Core.Helpers
{
    public static class DropDownHelper
    {
        public static IEnumerable<SelectListItem> SecondsIntervarList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "5 s", Value = "5000"},
                new SelectListItem{Text = "10 s", Value = "10000", Selected = true},
                new SelectListItem{Text = "30 s", Value = "30000"},
                new SelectListItem{Text = "60 s", Value = "60000"},
                new SelectListItem{Text = "2 min", Value = "120000"},

            };
            return items;
        }

        public static IEnumerable<SelectListItem> ConvertToSelectListItemIEnumerable(IEnumerable<DropDownListModel> model)
        {
            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var modelItem in model)
            {
                items.Add(new SelectListItem { Text = modelItem.Text, Value = modelItem.Value, Selected = modelItem.Selected });
            }

            return items;
        }
    }
}