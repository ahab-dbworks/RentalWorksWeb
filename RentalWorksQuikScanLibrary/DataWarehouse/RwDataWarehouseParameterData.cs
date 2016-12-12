using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScanLibrary.DataWarehouse
{
    public static class RwDataWarehouseParameterData
    {
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetControlRecord(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select top 1 *");
            qry.Add("from ControlDW with (nolock)");
            result = qry.QueryToTable();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetCompanyDepartments(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Department)");
            qry.Add("     , Value = Department");
            qry.Add("from Department with (nolock)");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        public static DataTable GetCompanyDepartmentsFromCustomerRevenueReport(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Department)");
            qry.Add("     , Value = Department");
            qry.Add("from CustomerRevenueReport with (nolock)");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //SELECT distinct Department as DepartmentValue, rtrim(Department) as DepartmentLabel
        //FROM CustomerRevenueReport
        //ORDER BY DepartmentLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetLocations(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Location)");
            qry.Add("     , Value = Location");
            qry.Add("from Location with (nolock)");
            qry.Add("where Location is not null and rtrim(Location) <> ''");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        public static DataTable GetLocationsFromCustomerRevenueReport(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Location)");
            qry.Add("     , Value = Location");
            qry.Add("from CustomerRevenueReport with (nolock)");
            qry.Add("where Location is not null and rtrim(Location) <> ''");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //        select distinct
        //         rtrim(Location) as LocationLabel,
        //         Location as LocationValue
        //from  CustomerRevenueReport
        //where Location is not null and rtrim(Location) <> ''
        //order by LocationLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetCategories(FwSqlConnection conn, List<string> inventoryDepartmentFilter)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Category)");
            qry.Add("     , Value = Category");
            qry.Add("from Category with (nolock)");
            qry.Add("where InventoryDepartment in (@InventoryDepartment)");
            qry.Add("order by Label");
            qry.AddParameter("@InventoryDepartment", FwConvert.ToSqlParameter(inventoryDepartmentFilter));
            result = qry.QueryToTable();

            return result;
        }
        public static DataTable GetCategoriesFromCustomerRevenueReport(FwSqlConnection conn, List<string> inventoryDepartmentFilter)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Category)");
            qry.Add("     , Value = Category");
            qry.Add("from CustomerRevenueReport with (nolock)");
            qry.Add("where InventoryDepartment in (@InventoryDepartment)");
            qry.Add("order by Label");
            qry.AddParameter("@InventoryDepartment", FwConvert.ToSqlParameter(inventoryDepartmentFilter));
            result = qry.QueryToTable();

            return result;
        }
        //SELECT distinct Category as CategoryValue, rtrim(Category) as CategoryLabel
        //FROM CustomerRevenueReport
        //WHERE InventoryDepartment in (@InventoryDepartment)
        //ORDER BY CategoryLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetDeals(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(DealDescription)");
            qry.Add("     , Value = DealDescription");
            qry.Add("from Deal with (nolock)");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //SELECT distinct DealDescription as DealValue, rtrim(DealDescription) as DealLabel
        //FROM Deal
        //ORDER BY DealLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetDealTypes(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(DealType)");
            qry.Add("     , Value = DealType");
            qry.Add("from Deal with (nolock)");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //SELECT distinct DealType as DealTypeValue, rtrim(DealType) as DealTypeLabel
        //FROM Deal
        //ORDER BY DealTypeLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetCustomers(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Customer)");
            qry.Add("     , Value = Customer");
            qry.Add("from customer with (nolock)");
            qry.Add("where Customer is not null and rtrim(Customer) <> ''");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //select distinct
        //rtrim(Customer) as CustomerLabel,
        //Customer as CustomerValue
        //from  customer
        //where Customer is not null and rtrim(Customer) <> ''
        //order by CustomerLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetActivityTypes(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(ActivityType)");
            qry.Add("     , Value = ActivityType");
            qry.Add("from ActivityType with (nolock)");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //SELECT distinct ActivityType as ActivityTypeValue, rtrim(ActivityType) as ActivityTypeLabel
        //FROM CustomerRevenueReport
        //ORDER BY ActivityTypeLabel
        //----------------------------------------------------------------------------------------------------
        //InventoryDepartment:
        public static DataTable GetInventoryDepartments(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(Department)");
            qry.Add("     , Value = Department");
            qry.Add("from department with (nolock)");
            qry.Add("order by Label");
            result = qry.QueryToTable();

            return result;
        }
        //SELECT distinct Department as DepartmentValue, rtrim(Department) as DepartmentLabel
        //FROM department
        //ORDER BY DepartmentLabel
        //----------------------------------------------------------------------------------------------------
        public static DataTable GetICodes(FwSqlConnection conn)
        {
            DataTable result;
            FwSqlCommand qry;

            qry  = new FwSqlCommand(conn);
            qry.Add("select Label = rtrim(ICode)");
            qry.Add("     , Value = ICode");
            qry.Add("from Inventory with (nolock)");
            qry.Add("order by ICode");
            result = qry.QueryToTable();

            return result;
        }
        //select rtrim(ICode) as ICodeLabel, ICode as ICodeValue
        //from Inventory 
        //order by ICode
        //----------------------------------------------------------------------------------------------------
    }
}