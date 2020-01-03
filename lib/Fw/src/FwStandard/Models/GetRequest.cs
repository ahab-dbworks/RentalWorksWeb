using FwStandard.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace FwStandard.Models
{
    public class GetManyRequestFilter
    {
        public string FieldName { get; set; } = "";
        readonly string[] AllowedComparisonOperator = new string[] {  "eq", "ne" ,"in", "ni", "sw", "ew", "co", "dnc", "gt", "gte", "lt", "lte" };
        string _ComparisonOperator = "";

        /// <summary>
        /// The following are valid comparison operators: "eq": equals, "ne": not equals,"in": in, "ni": not in, "sw": starts with, "ew": ends with, "co": contains, "dnc": does not contain, "gt": greater than, "gte": greater than or equal, "lt": less than, "lte": less than or equal
        /// </summary>
        public string ComparisonOperator
        {
            get
            {
                return _ComparisonOperator;
            }
            set
            {
                if (!AllowedComparisonOperator.Any(x => x == value)) throw new ArgumentException($"Unsupported comparison operator: {value}.  Supported values are: {string.Join(", ", AllowedComparisonOperator.ToArray<string>())}");
                _ComparisonOperator = value;
            }
        }

        public string FieldValue { get; set; } = "";
        /// <summary>
        /// Validates if the field is defined as a filter expression on the request.  You would set this to false when you want to modify the query server-side without exposing a property.
        /// </summary>
        public bool ValidateFilter { get; set; } = true;


        public GetManyRequestFilter() : base()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">The name of the field in the Loader class that you are filtering on.</param>
        /// <param name="comparisonOperator">The following are valid comparison operators: "eq": equals, "ne": not equals,"in": in, "nin": not in, "sw": starts with, "ew": ends with, "co": contains, "dnc": does not contain, "gt": greater than, "gte": greater than or equal, "lt": less than, "lte": less than or equal</param>
        /// <param name="fieldValue">The string representation of the field's value.  If validateFilter is true you need to pass the value you'd want in an SQL query.</param>
        /// <param name="validateFilter">Validates if the field is defined as a filter expression on the request.  You would set this to false when you want to modify the query server-side without exposing a property.</param>
        public GetManyRequestFilter(string fieldName, string comparisonOperator, string fieldValue, bool validateFilter) : this()
        {
            this.FieldName = fieldName;
            this.ComparisonOperator = comparisonOperator;
            this.FieldValue = fieldValue;
            this.ValidateFilter = validateFilter;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class GetRequestAttribute : Attribute
    {
        public readonly string DefaultSort = string.Empty;

        public GetRequestAttribute(string DefaultSort = "")
        {
            this.DefaultSort = DefaultSort;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GetRequestPropertyAttribute : Attribute
    {
        public readonly bool EnableFiltering = false;
        public readonly bool EnableSorting = false;

        public GetRequestPropertyAttribute(bool EnableFiltering, bool EnableSorting)
        {
            this.EnableFiltering = EnableFiltering;
            this.EnableSorting = EnableSorting;
        }
    }

    public class GetRequest
    {
        // properties - JSON serialized
        /// <summary>
        /// The page number in the result set starting from 1.  PageNo is required when the PageSize is specified.
        /// </summary>
        [Range(1, double.MaxValue)]
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// Limit result set to the specified amount.
        /// </summary>
        [Range(1, 1000)]
        public int PageSize { get; set; } = 1000;
        
        /// <summary>
        /// A sort expression to use of the form: Field1:asc,Field2:desc
        /// </summary>
        [MaxLength(1024)]
        public string Sort { get; set; } = "";
        //protected string activeview { get; set; } = string.Emp

        // fields - not JSON serialized
        [JsonIgnore]
        public Dictionary<string, GetManyRequestFilter> filters = new Dictionary<string, GetManyRequestFilter>();
        private bool _parsed = false;

        public GetRequest()
        {

        }

        public void Parse()
        {
            if (!_parsed)
            {
                if (string.IsNullOrEmpty(Sort))
                {
                    var getRequestAttribute = this.GetType().GetCustomAttribute<GetRequestAttribute>();
                    if (getRequestAttribute == null || string.IsNullOrEmpty(getRequestAttribute.DefaultSort))
                    {
                        var requestTypeName = FwTypeTranslator.GetFriendlyName(this.GetType());
                        throw new Exception($"Default sort expression is required. Please set the following attribute on class: {requestTypeName}.  [GetRequest[DefaultSort: \"FieldName:asc\"]");
                    }
                    this.Sort = getRequestAttribute.DefaultSort;
                }
                var propertyInfos = this.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.GetCustomAttribute<GetRequestPropertyAttribute>() != null)
                    .ToList();
                foreach (var propertyInfo in propertyInfos)
                {
                    var getManyRequestFieldAttribute = propertyInfo.GetCustomAttribute<GetRequestPropertyAttribute>();
                    if (getManyRequestFieldAttribute != null && getManyRequestFieldAttribute.EnableFiltering)
                    {
                        string fieldName = propertyInfo.Name;
                        var propValue = propertyInfo.GetValue(this);
                        if (propValue != null)
                        {
                            string filter = propValue.ToString();
                            if (!string.IsNullOrEmpty(filter))
                            {
                                var requestFilter = new GetManyRequestFilter();
                                requestFilter.FieldName = fieldName;
                                if (filter.StartsWith("eq:"))
                                {
                                    requestFilter.ComparisonOperator = "eq";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("ne:"))
                                {
                                    requestFilter.ComparisonOperator = "ne";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("in:"))
                                {
                                    requestFilter.ComparisonOperator = "in";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("ni:"))
                                {
                                    requestFilter.ComparisonOperator = "ni";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("sw:"))
                                {
                                    requestFilter.ComparisonOperator = "sw";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("ew:"))
                                {
                                    requestFilter.ComparisonOperator = "ew";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("co:"))
                                {
                                    requestFilter.ComparisonOperator = "co";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("dnc:"))
                                {
                                    requestFilter.ComparisonOperator = "dnc";
                                    requestFilter.FieldValue = filter.Substring(4);
                                }
                                else if (filter.StartsWith("gt:"))
                                {
                                    requestFilter.ComparisonOperator = "gt";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("gte:"))
                                {
                                    requestFilter.ComparisonOperator = "gte";
                                    requestFilter.FieldValue = filter.Substring(4);
                                }
                                else if (filter.StartsWith("lt:"))
                                {
                                    requestFilter.ComparisonOperator = "lt";
                                    requestFilter.FieldValue = filter.Substring(3);
                                }
                                else if (filter.StartsWith("lte:"))
                                {
                                    requestFilter.ComparisonOperator = "lte";
                                    requestFilter.FieldValue = filter.Substring(4);
                                }
                                else
                                {
                                    requestFilter.ComparisonOperator = "eq";
                                    requestFilter.FieldValue = filter;
                                }

                                // make sure the fieldName is a valid property name to protect against SQL injection attacks
                                if (propertyInfos.Where(i => i.Name == fieldName).Count() > 0)
                                {
                                    this.filters[fieldName] = requestFilter;
                                }
                            }
                        }
                    }
                }
                _parsed = true;
            }
        }
    }
}
