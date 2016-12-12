using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Deal
    {
        public string dealid          { get; set; }
        public string customer        { get; set; }
        public string customerid      { get; set; }
        public string customerno      { get; set; }
        public string dealno          { get; set; }
        [Required]
        public string dealname        { get; set; }
        public string dealtype        { get; set; }
        public string dealtypeid      { get; set; }
        public string dealstatus      { get; set; }
        public string dealstatusid    { get; set; }
        public Address address        { get; set; }
        public string phone           { get; set; }
        public string fax             { get; set; }
        public string creditstatus    { get; set; }
        public string phone800        { get; set; }
        public string phoneother      { get; set; }
        public string billperiod      { get; set; }
        public string officelocation  { get; set; }
        public string paymentterms    { get; set; }
        public string paymenttype     { get; set; }
        public string porequired      { get; set; }
        public string department      { get; set; }
        public string taxfedno        { get; set; }
        public string datestamp       { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}