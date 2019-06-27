using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class OrderUnit
    {
        [DataType(DataType.Text)]
        public string orderunitid { get; set; }

        [DataType(DataType.Text)]
        public string orderunit   { get; set; }

        [DataType(DataType.Text)]
        public string description { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CompanyDepartment
    {
        [DataType(DataType.Text)]
        public string departmentid { get; set; }

        [DataType(DataType.Text)]
        public string department   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class InventoryType
    {
        [DataType(DataType.Text)]
        public string inventorytypeid { get; set; }

        [DataType(DataType.Text)]
        public string inventorytype   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class LocationWarehouse
    {
        [DataType(DataType.Text)]
        public string locationid          { get; set; }

        [DataType(DataType.Text)]
        public string location            { get; set; }

        public List<Warehouse> warehouses { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Warehouse
    {
        [DataType(DataType.Text)]
        public string warehouseid { get; set; }

        [DataType(DataType.Text)]
        public string warehouse   { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Group
    {
        [DataType(DataType.Text)]
        public string groupsid { get; set; }

        [DataType(DataType.Text)]
        public string name     { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}