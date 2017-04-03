using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Contact
    {
        public string contactid      { get; set; }
        [Required]
        public string firstname      { get; set; }
        public string middlename     { get; set; }
        [Required]
        public string lastname       { get; set; }
        public string email          { get; set; }
        public Address address       { get; set; }
        public string officephone    { get; set; }
        public string officeext      { get; set; }
        public string homephone      { get; set; }
        public string fax            { get; set; }
        public string cellular       { get; set; }
        public string contacttitle   { get; set; }
        public string employedbyid   { get; set; }
        public string employedby     { get; set; }
        public string employedbyno   { get; set; }
        public string jobtitle       { get; set; }
        public string status         { get; set; }
        public string authorized     { get; set; }
        public string barcode        { get; set; }
        public string companytype    { get; set; }
        public string directext      { get; set; }
        public string directphone    { get; set; }
        public string faxext         { get; set; }
        public string pager          { get; set; }
        public string pagerpin       { get; set; }
        public string primarycontact { get; set; }
        public string sourceid       { get; set; }
        public string datestamp      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ContactOrders
    {
        [Required]
        public string contactid        { get; set; }
        public List<Order> orders      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ContactDeals
    {
        [Required]
        public string contactid        { get; set; }
        public List<Deal> deals        { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}