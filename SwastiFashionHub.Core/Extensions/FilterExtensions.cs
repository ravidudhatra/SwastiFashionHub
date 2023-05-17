using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SwastiFashionHub.Core.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<T> ApplyFiltersX<T>(this IQueryable<T> query, IEnumerable<FilterDescriptor> filterDescriptors)
        {
            if (filterDescriptors == null)
            {
                return query;
            }

            var filterString = string.Empty;

            foreach (var filter in filterDescriptors)
            {
                if (filterString != string.Empty)
                    filterString += " and ";

                var member = filter.Property.Replace(".", "_");
                var value = filter.FilterValue;

                if (filter.FilterOperator == FilterOperator.Contains)
                {
                    filterString += $"{member}.ToLower().Contains(\"{filter.FilterValue.ToString().ToLower()}\")";
                }
                else if (filter.FilterOperator == FilterOperator.Equals)
                {
                    if (filter.FilterValue == null)
                        filterString += $"{member} == null";
                    else
                        filterString += $"{member} == {value}";
                }
                else if (filter.FilterOperator == FilterOperator.NotEquals)
                {
                    if (filter.FilterValue == null)
                    {
                        filterString += $"{member} != null";
                    }
                    else
                    {
                        filterString += $"{member} != {value}";
                    }
                }
                else if (filter.FilterOperator == FilterOperator.GreaterThan)
                {
                    filterString += $"{member} > {value}";
                }
                else if (filter.FilterOperator == FilterOperator.GreaterThanOrEquals)
                {
                    filterString += $"{member} >= {value}";
                }
                else if (filter.FilterOperator == FilterOperator.LessThan)
                {
                    filterString += $"{member} < {value}";
                }
                else if (filter.FilterOperator == FilterOperator.LessThanOrEquals)
                {
                    filterString += $"{member} <= {value}";
                }
                else if (filter.FilterOperator == FilterOperator.StartsWith)
                {
                    value = $@"{filter.FilterValue}%";
                    filterString += $"{member}.StartsWith({value})";
                }
                else if (filter.FilterOperator == FilterOperator.EndsWith)
                {
                    value = $@"%{filter.FilterValue}";
                    filterString += $"{member}.EndsWith({value})";
                }
                else if (filter.FilterOperator == FilterOperator.DoesNotContain)
                {
                    value = $@"%{filter.FilterValue}%";
                    filterString += $"!{member}.Contains({value})";
                }
                else if (filter.FilterOperator == FilterOperator.IsEmpty)
                {
                    filterString += $"{member} == null || {member} == \"\"";
                }
                else if (filter.FilterOperator == FilterOperator.IsNotEmpty)
                {
                    filterString += $"{member} != null && {member} != \"\"";
                }
            }
            
            //foreach (var filter in filterDescriptors)
            //{
            //    var member = filter.Property.Replace(".", "_");
            //    switch (filter.FilterOperator.ToLower())
            //    {
            //        case "contains":
            //            filterString += $"{member}.ToLower().Contains(\"{filter.Value.ToLower()}\") and ";
            //            break;
            //        case "doesnotcontain":
            //            filterString += $"!{member}.ToLower().Contains(\"{filter.Value.ToLower()}\") and ";
            //            break;
            //        case "equals":
            //            filterString += $"{member}.ToLower() == \"{filter.Value.ToLower()}\" and ";
            //            break;
            //        case "notequals":
            //            filterString += $"{member}.ToLower() != \"{filter.Value.ToLower()}\" and ";
            //            break;
            //        case "startswith":
            //            filterString += $"{member}.ToLower().StartsWith(\"{filter.Value.ToLower()}\") and ";
            //            break;
            //        case "endswith":
            //            filterString += $"{member}.ToLower().EndsWith(\"{filter.Value.ToLower()}\") and ";
            //            break;
            //        case "isempty":
            //            filterString += $"{member} == null or {member} == \"\" and ";
            //            break;
            //        case "isnotempty":
            //            filterString += $"{member} != null and {member} != \"\" and ";
            //            break;
            //        case "isnull":
            //            filterString += $"{member} == null and ";
            //            break;
            //        case "isnotnull":
            //            filterString += $"{member} != null and ";
            //            break;
            //        case "in":
            //            var values = filter.Value.Split(',');
            //            var inClause = string.Join(",", values.Select(v => $"\"{v.ToLower()}\""));
            //            filterString += $"{inClause}.Contains({member}.ToLower()) and ";
            //            break;
            //        case "notin":
            //            values = filter.Value.Split(',');
            //            var notInClause = string.Join(",", values.Select(v => $"\"{v.ToLower()}\""));
            //            filterString += $"!{notInClause}.Contains({member}.ToLower()) and ";
            //            break;
            //        default:
            //            throw new NotSupportedException($"Operator '{filter.Operator}' is not supported.");
            //    }
            //}

            //// Remove the trailing " and " from the filter string
            //if (!string.IsNullOrEmpty(filterString))
            //{
            //    filterString = filterString.Substring(0, filterString.Length - 5);
            //}

            if (filterString != string.Empty)
            {
                query = query.Where(filterString);
            }

            return query;
        }

        //public static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string filterString, params object[] filterParameters)
        //{
        //    var filterExpression = BuildFilterExpression<T>(filterString, filterParameters);
        //    return query.Where(filterExpression);
        //}
        //public static Expression<Func<T, bool>> BuildFilterExpression<T>(string filterString, params object[] filterParameters)
        //{
        //    var builder = new FilterDescriptorToExpressionBuilder<T>();
        //    var converter = new ExpressionConverter();
        //    var descriptor = builder.Parse(filterString, filterParameters);
        //    var expression = converter.Convert(descriptor.Filter, descriptor.Parameters.ToArray());
        //    return (Expression<Func<T, bool>>)expression;
        //}



    }
}
