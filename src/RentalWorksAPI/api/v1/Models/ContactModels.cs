using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Contact
    {
        [DataType(DataType.Text)]
        public string contactid      { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string firstname      { get; set; }

        [DataType(DataType.Text)]
        public string middlename     { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string lastname       { get; set; }

        [DataType(DataType.Text)]
        public string email          { get; set; }

        public Address address       { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string officephone    { get; set; }

        [DataType(DataType.Text)]
        public string officeext      { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string homephone      { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string fax            { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string cellular       { get; set; }

        [DataType(DataType.Text)]
        public string contacttitle   { get; set; }

        [DataType(DataType.Text)]
        public string employedbyid   { get; set; }

        [DataType(DataType.Text)]
        public string employedby     { get; set; }

        [DataType(DataType.Text)]
        public string employedbyno   { get; set; }

        [DataType(DataType.Text)]
        public string jobtitle       { get; set; }

        [DataType(DataType.Text)]
        public string status         { get; set; }

        [DataType(DataType.Text)]
        public string authorized     { get; set; }

        [DataType(DataType.Text)]
        public string barcode        { get; set; }

        [DataType(DataType.Text)]
        public string companytype    { get; set; }

        [DataType(DataType.Text)]
        public string directext      { get; set; }

        [DataType(DataType.Text)]
        public string directphone    { get; set; }

        [DataType(DataType.Text)]
        public string faxext         { get; set; }

        [DataType(DataType.Text)]
        public string pager          { get; set; }

        [DataType(DataType.Text)]
        public string pagerpin       { get; set; }

        [DataType(DataType.Text)]
        public string primarycontact { get; set; }

        [DataType(DataType.Text)]
        public string sourceid       { get; set; }

        [DataType(DataType.DateTime)]
        public string datestamp      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ContactOrders
    {
        [Required]
        [DataType(DataType.Text)]
        public string contactid        { get; set; }
        public List<Order> orders      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ContactDeals
    {
        [Required]
        [DataType(DataType.Text)]
        public string contactid        { get; set; }
        public List<Deal> deals        { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}