using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Customer
    {
        [DataType(DataType.Text)]
        public string customerid             { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string customername           { get; set; }

        public Address address               { get; set; }

        [DataType(DataType.Text)]
        public string website                { get; set; }

        [DataType(DataType.Text)]
        public string phone                  { get; set; }

        [DataType(DataType.Text)]
        public string fax                    { get; set; }

        [DataType(DataType.Text)]
        public string creditstatus           { get; set; }

        [DataType(DataType.Text)]
        public string creditlimit            { get; set; }

        [DataType(DataType.Text)]
        public string billtoaddress          { get; set; }

        [DataType(DataType.Text)]
        public string billtoattention        { get; set; }

        [DataType(DataType.Text)]
        public string certificateofinsurance { get; set; }

        [DataType(DataType.Text)]
        public string creditthroughdate      { get; set; }

        [DataType(DataType.Text)]
        public string customercategory       { get; set; }

        [DataType(DataType.Text)]
        public string customerno             { get; set; }

        [DataType(DataType.Text)]
        public string customertype           { get; set; }

        [DataType(DataType.Text)]
        public string insurancevalidthru     { get; set; }

        [DataType(DataType.Text)]
        public string location               { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone800               { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phoneother             { get; set; }

        [DataType(DataType.Text)]
        public string taxable                { get; set; }

        [DataType(DataType.Text)]
        public string customerstatus         { get; set; }

        [DataType(DataType.Text)]
        public string taxfedno               { get; set; }

        [DataType(DataType.Text)]
        public string department             { get; set; }

        [DataType(DataType.DateTime)]
        public string datestamp              { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CustomerOrders
    {
        [Required]
        [DataType(DataType.Text)]
        public string customerid       { get; set; }
        public List<Order> Orders      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}