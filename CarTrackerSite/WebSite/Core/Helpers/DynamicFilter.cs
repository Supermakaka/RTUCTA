using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Core.Helpers
{
    using BusinessLogic.Models;

    public enum FilterItemTypeEnum
    {
        Number,
        Enum,
        String,
        Boolean,
        DateRange
    }

    public enum PredicateConditionEnum
    {
        And,
        Or
    }


    public class FilterItem
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public FilterItemTypeEnum Type { get; set; }

        public FilterItem(string propertyName, string propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            Type = FilterItemTypeEnum.String;
        }

        public FilterItem(string propertyName, string propertyValue, FilterItemTypeEnum type)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            Type = type;
        }
    }

    public class SortingItem
    {
        public string PropertyName { get; set; }
        public string Direction { get; set; }

        public SortingItem(string propertyName, string direction)
        {
            PropertyName = propertyName;
            Direction = direction;
        }
    }

    public class FilteringParams
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public IList<FilterItem> GlobalSearchItems { get; set; }
        public IList<FilterItem> FilterItems { get; set; }
        public IList<SortingItem> SortingItems { get; set; }

        public bool CompareFilterItemValue(string propertyName, string valueToCompare)
        {
            string filterItemValue = GetFilterItemValue(propertyName);

            return filterItemValue == valueToCompare;
        }

        public string GetFilterItemValue(string propertyName)
        {
            if (FilterItems == null)
                return string.Empty;

            var filterItem = FilterItems.FirstOrDefault(m => m.PropertyName == propertyName);

            return filterItem != null ? filterItem.PropertyValue : string.Empty;
        }

        public void RemoveFilterItem(string propertyName)
        {
            if (FilterItems != null)
            {
                FilterItems = FilterItems.Where(m => m.PropertyName != propertyName).ToList();
            }
        }
    }

    public class FilteredResult<T>
    {
        public int TotalRecords { get; set; }
        public int TotalFilteredRecords { get; set; }

        public IList<T> Items { get; set; }
    }

    public class DynamicFilter
    {
        public static FilteredResult<T> Execute<T>(IQueryable<T> data, FilteringParams filteringParams)
        {
            var result = new FilteredResult<T>();

            result.TotalRecords = data.Count();

            data = ApplyFilteringCriteria(data, filteringParams).Cast<T>();
            data = ApplySortingCriteria(data, filteringParams).Cast<T>();

            result.TotalFilteredRecords = data.Count();

            //pagination
            if (filteringParams.Take > 0)
                data = data.Skip(filteringParams.Skip).Take(filteringParams.Take);

            result.Items = data.Cast<T>().ToList();

            return result;
        }

        private static IQueryable ApplyFilteringCriteria(IQueryable data, FilteringParams filteringParams)
        {
            var propertyPredicateValues = new List<object>();

            string propertyPredicate = BuildPredicate(filteringParams.FilterItems, PredicateConditionEnum.And, ref propertyPredicateValues);
            string globalSearchPredicate = BuildPredicate(filteringParams.GlobalSearchItems, PredicateConditionEnum.Or, ref propertyPredicateValues);

            var predicate = propertyPredicate;

            if (!String.IsNullOrEmpty(globalSearchPredicate))
            {
                if (!String.IsNullOrEmpty(propertyPredicate))
                    predicate = String.Format("{0} and ({1})", propertyPredicate, globalSearchPredicate);
                else
                    predicate = globalSearchPredicate;
            }

            if (!String.IsNullOrEmpty(predicate))
                return data.Where(predicate, propertyPredicateValues.ToArray());

            return data;
        }

        private static IQueryable ApplySortingCriteria(IQueryable data, FilteringParams filteringParams)
        {
            string sortString = "";

            foreach (var sorting in filteringParams.SortingItems)
            {
                if (!String.IsNullOrEmpty(sortString))
                    sortString += ", ";

                sortString += sorting.PropertyName + " " + sorting.Direction;
            }

            if (!String.IsNullOrEmpty(sortString))
                return data.OrderBy(sortString);

            return data;
        }

        private static string BuildPredicate(IList<FilterItem> filterItmes, PredicateConditionEnum condition, ref List<object> predicateValues)
        {
            var cond = condition == PredicateConditionEnum.And ? " and " : " or ";
            var predicateBuilder = new StringBuilder();

            foreach (var filter in filterItmes)
            {
                if (String.IsNullOrEmpty(filter.PropertyValue))
                    continue;

                if (filter.Type == FilterItemTypeEnum.Number)
                {
                    int val;

                    if (int.TryParse(filter.PropertyValue, out val))
                        predicateBuilder.Append(String.Format("{2}{0} = {1}", filter.PropertyName, val, cond));
                }
                else if (filter.Type == FilterItemTypeEnum.Boolean)
                {
                    bool val;

                    if (bool.TryParse(filter.PropertyValue, out val))
                        predicateBuilder.Append(String.Format("{2}{0} = {1}", filter.PropertyName, val, cond));
                }
                else if (filter.Type == FilterItemTypeEnum.Enum)
                {
                    predicateBuilder.Append(String.Format("{2}{0} = \"{1}\"", filter.PropertyName, filter.PropertyValue, cond));
                }
                else if (filter.Type == FilterItemTypeEnum.DateRange)
                {
                    var range = filter.PropertyValue.Split('-');

                    DateTime dateFrom;
                    DateTime dateTill;

                    if (range != null && DateTime.TryParse(range[0], out dateFrom) && DateTime.TryParse(range[range.Length - 1], out dateTill))
                    {
                        predicateValues.Add(dateFrom);
                        predicateValues.Add(dateTill.AddDays(1));

                        predicateBuilder.Append(String.Format("{3}{0} >= @{1} and {0} < @{2}", filter.PropertyName, predicateValues.Count - 2, predicateValues.Count - 1, cond));
                    }
                }
                else
                {
                    predicateBuilder.Append(String.Format("{2}{0}.Contains(\"{1}\")", filter.PropertyName, filter.PropertyValue, cond));
                }
            }

            return predicateBuilder.ToString().TrimStart(cond.ToCharArray()); //remove leading " and " or " or "
        }
    }
}