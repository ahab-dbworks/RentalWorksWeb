using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Customer
    {
        public string customerid             { get; set; }
        [Required]
        public string customername           { get; set; }
        public Address address               { get; set; }
        public string website                { get; set; }
        public string phone                  { get; set; }
        public string fax                    { get; set; }
        public string creditstatus           { get; set; }
        public string creditlimit            { get; set; }
        public string billtoaddress          { get; set; }
        public string billtoattention        { get; set; }
        public string certificateofinsurance { get; set; }
        public string creditthroughdate      { get; set; }
        public string customercategory       { get; set; }
        public string customerno             { get; set; }
        public string customertype           { get; set; }
        public string insurancevalidthru     { get; set; }
        public string location               { get; set; }
        public string phone800               { get; set; }
        public string phoneother             { get; set; }
        public string taxable                { get; set; }
        public string customerstatus         { get; set; }
        public string taxfedno               { get; set; }
        public string department             { get; set; }
        public string datestamp              { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CustomerOrders
    {
        [Required]
        public string customerid       { get; set; }
        public List<Order> Orders      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}