using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WebApi.Data
{
    public class AppReportLoader : FwReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string PrintDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        public List<string> DateFields
        {
            get
            {
                List<string> dateFields = new List<string>();
                PropertyInfo[] properties = this.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    // get the type of this property
                    Type type = property.PropertyType;

                    // if the property is an SqlDataField and is a Date type, then add its fieldname to the list
                    if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlDataFieldAttribute.ModelType.Equals(FwDataTypes.Date))
                        {
                            dateFields.Add(property.Name);
                        }
                    }

                    // if the property is a List, then get the type of the items in the List.  Ispect the type for Date fields
                    if (typeof(System.Collections.IList).IsAssignableFrom(type))
                    {
                        foreach (var listProperty in type.GetProperties())
                        {
                            if ((listProperty.Name.Equals("Item")) && (listProperty.PropertyType != typeof(object)))
                            {
                                var listParameters = listProperty.GetIndexParameters();
                                if ((listParameters.Length == 1) && (listParameters[0].ParameterType == typeof(int)))
                                {
                                    PropertyInfo[] itemProperties = listProperty.PropertyType.GetProperties();
                                    foreach (PropertyInfo itemProperty in itemProperties)
                                    {
                                        if (itemProperty.IsDefined(typeof(FwSqlDataFieldAttribute)))
                                        {
                                            FwSqlDataFieldAttribute sqlDataFieldAttribute = itemProperty.GetCustomAttribute<FwSqlDataFieldAttribute>();
                                            if (sqlDataFieldAttribute.ModelType.Equals(FwDataTypes.Date))
                                            {
                                                dateFields.Add(itemProperty.Name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return dateFields;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
