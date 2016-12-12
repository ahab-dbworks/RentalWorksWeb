using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace RentalWorksQuikScanLibrary.DataWarehouse
{
    class RwDataWarehouseParameterService
    {
        //----------------------------------------------------------------------------------------------------
        public static void GetCompanyDepartments(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetCompanyDepartments";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetCompanyDepartmentsFromCustomerRevenueReport(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetLocations(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetLocations";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetLocationsFromCustomerRevenueReport(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetCategories(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetCategoriesFromCustomerRevenueReport";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetCompanyDepartmentsFromCustomerRevenueReport(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetDeals(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetDeals";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetDeals(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetDealTypes(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetDealTypes";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetDealTypes(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetCustomers(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetCustomers";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetCustomers(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetActivityTypes(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetActivityTypes";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetActivityTypes(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetInventoryDepartments(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetInventoryDepartments";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetInventoryDepartments(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
        public static void GetICodes(dynamic request, dynamic response, dynamic session)
        {
            //const string METHOD_NAME = "GetICodes";
            DataTable dt;
            
            dt = RwDataWarehouseParameterData.GetICodes(FwSqlConnection.RentalWorksDW);
        }
        //----------------------------------------------------------------------------------------------------
    }
}
