using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
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
                    if (property.IsDefined(typeof(FwSqlDataFieldAttribute)))
                    {
                        FwSqlDataFieldAttribute sqlDataFieldAttribute = property.GetCustomAttribute<FwSqlDataFieldAttribute>();
                        if (sqlDataFieldAttribute.ModelType.Equals(FwDataTypes.Date))
                        {
                            dateFields.Add(property.Name);
                        }
                    }
                }
                return dateFields;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
