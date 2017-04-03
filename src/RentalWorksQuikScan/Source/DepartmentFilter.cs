using Fw.Json.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        public static void LoadUserDepartmentFilter(string usersid, dynamic session)
        {
            dynamic applicationOptions;
            StringBuilder deptfilter = new StringBuilder();
            if (session == null)
            {
                applicationOptions = FwSqlData.GetApplicationOptions(FwSqlConnection.RentalWorks);
            }
            else
            {
                applicationOptions = session.applicationOptions;
            }
            if (applicationOptions.departmentfilter.enabled)
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("select d.departmentid");
                    qry.Add("from  department d join departmentaccess da on (d.departmentid = da.departmentaccessid)");
                    qry.Add("where da.departmentid = dbo.getusersprimarydepartmentid(@usersid)");
                    qry.Add("  and d.inactive <> 'T'");
                    qry.Add("  and orderaccess = 'T'");
                    qry.AddParameter("@usersid", usersid);
                    DataTable dt = qry.QueryToTable();
                    deptfilter.Append("departmentid in (''");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        deptfilter.Append(",'");
                        deptfilter.Append(dt.Rows[i]["departmentid"].ToString());
                        deptfilter.Append("'");
                    }
                    deptfilter.Append(")");
                }
            }
            usersDepartmentFilter[usersid] = deptfilter.ToString();
        }
        //----------------------------------------------------------------------------------------------------
        public static void SetDepartmentFilter(string usersid, FwSqlCommand qry)
        {
            if (!usersDepartmentFilter.ContainsKey(usersid))
            {
                LoadUserDepartmentFilter(usersid, null);
            }
            if (!string.IsNullOrEmpty(usersDepartmentFilter[usersid]))
            {
                qry.Add("  and " + usersDepartmentFilter[usersid]);
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static void SetDepartmentFilter(string usersid, FwSqlSelect select)
        {
            if (!usersDepartmentFilter.ContainsKey(usersid))
            {
                LoadUserDepartmentFilter(usersid, null);
            }
            if (!string.IsNullOrEmpty(usersDepartmentFilter[usersid]))
            {
                select.Add("  and " + usersDepartmentFilter[usersid]);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
