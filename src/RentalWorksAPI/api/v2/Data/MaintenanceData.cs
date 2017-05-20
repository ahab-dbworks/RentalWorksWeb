using Fw.Json.Services;
using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Data
{
    public class MaintenanceData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<OrderUnit> GetOrderUnits()
        {
            FwSqlCommand qry;
            dynamic qryresult;
            List<OrderUnit> result = new List<OrderUnit>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from apirest_orderunitview");
            qry.Add(" where inactive <> 'T'");
            qry.Add("order by orderunit");

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                OrderUnit orderunit = new OrderUnit();

                orderunit.orderunitid = qryresult[i].orderunitid;
                orderunit.orderunit   = qryresult[i].orderunit;
                orderunit.description = qryresult[i].description;

                result.Add(orderunit);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<CompanyDepartment> GetCompanyDepartments()
        {
            FwSqlCommand qry;
            dynamic qryresult;
            List<CompanyDepartment> result = new List<CompanyDepartment>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from apirest_departmentview");
            qry.Add(" where inactive <> 'T'");
            qry.Add("order by department");

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                CompanyDepartment companydepartment = new CompanyDepartment();

                companydepartment.departmentid = qryresult[i].departmentid;
                companydepartment.department   = qryresult[i].department;

                result.Add(companydepartment);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<InventoryType> GetInventoryTypes()
        {
            FwSqlCommand qry;
            dynamic qryresult;
            List<InventoryType> result = new List<InventoryType>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from apirest_inventorytypeview");
            qry.Add(" where inactive <> 'T'");
            qry.Add("order by inventorydepartment");

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                InventoryType inventorytype = new InventoryType();

                inventorytype.inventorytypeid = qryresult[i].inventorydepartmentid;
                inventorytype.inventorytype   = qryresult[i].inventorydepartment;

                result.Add(inventorytype);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<LocationWarehouse> GetLocationWarehouses()
        {
            FwSqlCommand qry;
            dynamic qryresult;
            List<LocationWarehouse> result = new List<LocationWarehouse>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from apirest_locationview");
            qry.Add(" where inactive <> 'T'");
            qry.Add("order by location");

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                LocationWarehouse locationwarehouse = new LocationWarehouse();

                locationwarehouse.locationid = qryresult[i].locationid;
                locationwarehouse.location   = qryresult[i].location;
                locationwarehouse.warehouses = GetWarehousesByLocation(qryresult[i].locationid);

                result.Add(locationwarehouse);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Warehouse> GetWarehousesByLocation(string locationid)
        {
            FwSqlCommand qry;
            dynamic qryresult;
            List<Warehouse> result = new List<Warehouse>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from dbo.apirest_warehouselocationfunc(@locationid)");
            qry.AddParameter("@locationid", locationid);

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                Warehouse warehouse = new Warehouse();

                warehouse.warehouseid = qryresult[i].warehouseid;
                warehouse.warehouse   = qryresult[i].warehouse;

                result.Add(warehouse);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Group> GetGroups()
        {
            FwSqlCommand qry;
            dynamic qryresult;
            List<Group> result = new List<Group>();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            //qry.AddColumn("datestamp", false, FwJsonDataTableColumn.DataTypes.Date);
            qry.Add("select *");
            qry.Add("  from apirest_groupsview");
            qry.Add("order by name");

            qryresult = qry.QueryToDynamicList2();

            for (int i = 0; i < qryresult.Count; i++)
            {
                Group group = new Group();

                group.groupsid = qryresult[i].groupsid;
                group.name     = qryresult[i].name;

                result.Add(group);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}