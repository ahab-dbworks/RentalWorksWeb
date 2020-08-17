using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Source
{
    /// <summary>
    /// Caches the department filter for webusers
    /// </summary>
    public class DepartmentFilter
    {
        //----------------------------------------------------------------------------------------------------
        private static Dictionary<string, string> usersDepartmentFilter { get; set; } = new Dictionary<string, string>();
        //----------------------------------------------------------------------------------------------------
        public static async Task LoadUserDepartmentFilterAsync(FwApplicationConfig applicationConfig, string usersid, dynamic session)
        {
            dynamic applicationOptions;
            StringBuilder deptfilter = new StringBuilder();
            if (session == null)
            {
                applicationOptions = await FwSqlData.GetApplicationOptionsAsync(applicationConfig.DatabaseSettings);
            }
            else
            {
                applicationOptions = session.applicationOptions;
            }
            if (applicationOptions.departmentfilter.enabled)
            {
                using (FwSqlConnection conn = new FwSqlConnection(applicationConfig.DatabaseSettings.ConnectionString))
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, applicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select d.departmentid");
                        qry.Add("from  department d join departmentaccess da on (d.departmentid = da.departmentaccessid)");
                        qry.Add("where da.departmentid = dbo.getusersprimarydepartmentid(@usersid)");
                        qry.Add("  and d.inactive <> 'T'");
                        qry.Add("  and orderaccess = 'T'");
                        qry.AddParameter("@usersid", usersid);
                        FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                        deptfilter.Append("departmentid in (''");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            deptfilter.Append(",'");
                            deptfilter.Append(dt.GetValue(i, "departmentid").ToString());
                            deptfilter.Append("'");
                        }
                        deptfilter.Append(")");
                    } 
                }
            }
            usersDepartmentFilter[usersid] = deptfilter.ToString();
        }
        //----------------------------------------------------------------------------------------------------
        public static async Task SetDepartmentFilterAsync(FwApplicationConfig applicationConfig, string usersid, FwSqlCommand qry)
        {
            if (!usersDepartmentFilter.ContainsKey(usersid))
            {
                await LoadUserDepartmentFilterAsync(applicationConfig, usersid, null);
            }
            if (!string.IsNullOrEmpty(usersDepartmentFilter[usersid]))
            {
                qry.Add("  and " + usersDepartmentFilter[usersid]);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static async Task SetDepartmentFilterAsync(FwApplicationConfig applicationConfig, string usersid, FwSqlSelect select)
        {
            if (!usersDepartmentFilter.ContainsKey(usersid))
            {
                await LoadUserDepartmentFilterAsync(applicationConfig, usersid, null);
            }
            if (!string.IsNullOrEmpty(usersDepartmentFilter[usersid]))
            {
                select.Add("  and " + usersDepartmentFilter[usersid]);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
