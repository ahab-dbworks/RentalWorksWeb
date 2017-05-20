using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class OrderUnit
    {
        public string orderunitid { get; set; }
        public string orderunit   { get; set; }
        public string description { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CompanyDepartment
    {
        public string departmentid { get; set; }
        public string department   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class InventoryType
    {
        public string inventorytypeid { get; set; }
        public string inventorytype   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class LocationWarehouse
    {
        public string locationid          { get; set; }
        public string location            { get; set; }
        public List<Warehouse> warehouses { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Warehouse
    {
        public string warehouseid { get; set; }
        public string warehouse   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Group
    {
        public string groupsid { get; set; }
        public string name     { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}