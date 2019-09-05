using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Reflection;

namespace FwStandard.Data
{
    public abstract class FwReportLoader : FwDataReadWriteRecord 
    {
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
