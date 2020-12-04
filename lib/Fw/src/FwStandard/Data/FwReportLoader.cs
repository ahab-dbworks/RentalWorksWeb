using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FwStandard.Data
{
    public abstract class FwReportLoader : FwDataReadWriteRecord 
    {
        //------------------------------------------------------------------------------------ 
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public string PrintDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        public virtual void MakePreview()
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                //// get the type of this property
                //Type propertyType = property.PropertyType;

                if (property.Name.Equals("RowType"))
                {
                    property.SetValue(this, "detail");
                }

                //// if the property is a List, then get the type of the items in the List.  Find the RowType property and set it to "detail"
                //if (typeof(IList).IsAssignableFrom(propertyType))
                //{
                //    IList theList = property.GetValue(l, null) as IList;
                //
                //    foreach (var listItem in theList)
                //    {
                //        PropertyInfo[] listItemProperties = listItem.GetType().GetProperties();
                //        foreach (PropertyInfo listItemProperty in listItemProperties)
                //        {
                //            if (listItemProperty.Name.Equals("RowType"))
                //            {
                //                listItemProperty.SetValue(listItem, "detail");
                //                break;
                //            }
                //        }
                //    }
                //}
            }
        }
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
        public virtual void HideDetailColumnsInSummaryDataTable(FwReportRequest request, FwJsonDataTable dt)
        {
            if (request.IsSummary.GetValueOrDefault(false))
            {
                PropertyInfo[] properties = this.GetType().GetProperties();   // get an array of all of the properties (fields) on this object (AgentBillingReportLoader)
                foreach (FwJsonDataTableColumn col in dt.Columns)             // iterate through each column in the FwJsonDataTable
                {
                    foreach (PropertyInfo property in properties)             // iterate through each Property trying to find one that matches the name of the column
                    {
                        if (property.Name.Equals(col.DataField))              // we found the Property whose name matches the column name
                        {
                            if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))     // make sure the Property has a FwSqlDataFieldAttribute defined on it
                            {
                                foreach (Attribute attribute in property.GetCustomAttributes())   // iterate through each attribute to find the one that is FwSqlDataFieldAttribute
                                {
                                    if (attribute.GetType() == typeof(FwSqlDataFieldAttribute))   // we found the one attribute that is FwSqlDataFieldAttribute
                                    {
                                        FwSqlDataFieldAttribute fieldAttribute = (FwSqlDataFieldAttribute)attribute;  // create a variable to hold the attribute info
                                        if (fieldAttribute.HideInSummary)                                             // if the Property (field) is defined as "HideInSummary" then make the column invisible
                                        {
                                            col.IsVisible = false;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
