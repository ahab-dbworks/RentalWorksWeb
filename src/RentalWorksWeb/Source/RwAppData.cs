using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace Web.Source
{
    public class RwAppData
    {
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetUser(FwSqlConnection conn, string usersId)
        {
            FwSqlCommand qry;
            List<dynamic> dataSet;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from users with(nolock)");
            qry.Add("where usersid = @usersid");
            qry.AddParameter("@usersid", usersId);
            dataSet = qry.QueryToDynamicList2();
            if (dataSet.Count == 0)
            {
                throw new Exception("Can't find user.");
            }
            result = dataSet[0];

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetContact(FwSqlConnection conn, string contactId)
        {
            FwSqlCommand qry;
            List<dynamic> rows;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from contact with(nolock)");
            qry.Add("where contactid = @contactid");
            qry.AddParameter("@contactid", contactId);
            rows = qry.QueryToDynamicList2();
            if (rows.Count == 0)
            {
                throw new Exception("Can't find contact.");
            }
            result = rows[0];

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetLocationInfo(FwSqlConnection conn, string locationid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select locationid, location, locationcolor");
            qry.Add("  from location with (nolock)");
            qry.Add(" where locationid = @locationid");
            qry.AddParameter("@locationid", locationid);
            result = qry.QueryToDynamicObject2();

            result.locationid    = FwCryptography.AjaxEncrypt(result.locationid);
            result.locationcolor = FwConvert.OleToHex((int)result.locationcolor);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetWarehouseInfo(FwSqlConnection conn, string warehouseid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select warehouseid, warehouse");
            qry.Add("  from warehouse with (nolock)");
            qry.Add(" where warehouseid = @warehouseid");
            qry.AddParameter("@warehouseid", warehouseid);
            result = qry.QueryToDynamicObject2();

            result.warehouseid = FwCryptography.AjaxEncrypt(result.warehouseid);
            result.warehouse   = FwCryptography.AjaxEncrypt(result.warehouse);
            //result.warehousecolor = FwConvert.OleToHex((int)result.warehousecolor);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetDepartmentInfo(FwSqlConnection conn, string departmentid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select departmentid, department");
            qry.Add("  from department with (nolock)");
            qry.Add(" where departmentid = @departmentid");
            qry.AddParameter("@departmentid", departmentid);
            result = qry.QueryToDynamicObject2();

            result.departmentid = FwCryptography.AjaxEncrypt(result.departmentid);
            result.department = FwCryptography.AjaxEncrypt(result.department);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static dynamic GetUserInfo(FwSqlConnection conn, string webusersid)
        {
            FwSqlCommand qry;
            dynamic result;

            qry = new FwSqlCommand(conn);
            qry.Add("select webusersid");
            qry.Add("  from webusers with (nolock)");
            qry.Add(" where webusersid = @webusersid");
            qry.AddParameter("@webusersid", webusersid);
            result = qry.QueryToDynamicObject2();

            result.webusersid = FwCryptography.AjaxEncrypt(result.webusersid);
      

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}