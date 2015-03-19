using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DataTables.Mvc;

namespace WebSite.Core.Helpers
{
    public class FilteringParametersHelper
    {
        public static FilteringParams GetFilteringParams(Type viewModelType, IDataTablesRequest request)
        {
            return new FilteringParams()
            {
                Skip = request.Start,
                Take = request.Length,
                GlobalSearchItems = GetSearchableItems(viewModelType, request),
                FilterItems = GetFilteringItems(viewModelType, request),
                SortingItems = GetSortingItems(viewModelType, request)
            };
        }

        public static bool IsFilterEmpty(string propertyName, IDataTablesRequest request)
        {
            return String.IsNullOrEmpty(request.Columns.First(c => c.Data == propertyName).Search.Value);
        }

        private static IList<FilterItem> GetSearchableItems(Type viewModelType, IDataTablesRequest request)
        {
            var filters = new List<FilterItem>();

            if (String.IsNullOrEmpty(request.Search.Value))
                return filters;

            foreach (var col in request.Columns.Where(c => c.Searchable))
            {
                if (String.IsNullOrEmpty(col.Data))
                    continue;

                var attr = GetPropertySettings(viewModelType, col.Data, false);

                if (!(attr != null && attr.GlobalSearchAllowed))
                    continue;

                var propName = String.IsNullOrEmpty(attr.PropertyName) ? col.Data : attr.PropertyName;

                filters.Add(new FilterItem(propName, request.Search.Value, attr.Type));    
            }

            if (filters.Count == 0)
                throw new Exception(String.Format("Filtering error. No properties configured for global search are found in {0}.", viewModelType.Name));

            return filters;
        }

        private static IList<FilterItem> GetFilteringItems(Type viewModelType, IDataTablesRequest request)
        {
            var filters = new List<FilterItem>();

            var filteredColumns = request.Columns.GetFilteredColumns();

            if (filteredColumns.Count() == 0)
                return filters;

            foreach (var column in filteredColumns)
            {
                if (String.IsNullOrEmpty(column.Search.Value))
                    continue;

                var propName = column.Data;

                var attr = GetPropertySettings(viewModelType, propName, true);

                propName = String.IsNullOrEmpty(attr.PropertyName) ? propName : attr.PropertyName;

                filters.Add(new FilterItem(propName, column.Search.Value, attr.Type));
            }

            return filters;
        }

        private static FilterSettingsAttribute GetPropertySettings(Type viewModelType, string propertyName, bool isObligatory)
        {
            var prop = viewModelType.GetProperty(propertyName);

            if (prop != null)
            {
                object typeAttr = prop.GetCustomAttributes(typeof(FilterSettingsAttribute), true).FirstOrDefault();

                if (typeAttr != null)
                    return ((FilterSettingsAttribute)typeAttr);
            }

            if (isObligatory)
                throw new Exception(String.Format("Filtering error. {0}.{1} property is missing [Filterable] attribute.", viewModelType.Name, propertyName));

            return null;
        }
    
        private static IList<SortingItem> GetSortingItems(Type viewModelType, IDataTablesRequest request)
        {
            var sortingItems = new List<SortingItem>();
            var sortedColumns = request.Columns.GetSortedColumns();

            if (sortedColumns.Count() == 0)
                return sortingItems;

            foreach (var column in sortedColumns)
            {
                var propName = column.Data;

                var attr = GetPropertySettings(viewModelType, propName, false);
                if (attr != null)
                    propName = String.IsNullOrEmpty(attr.PropertyName) ? propName : attr.PropertyName;

                sortingItems.Add(new SortingItem(propName, column.SortDirection == Column.OrderDirection.Ascendant ? "Asc" : "Desc"));
            }

            return sortingItems;
        }
    }
}