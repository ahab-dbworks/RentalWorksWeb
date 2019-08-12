using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Deal
    {
        [DataType(DataType.Text)]
        public string dealid          { get; set; }

        [DataType(DataType.Text)]
        public string customer        { get; set; }

        [DataType(DataType.Text)]
        public string customerid      { get; set; }

        [DataType(DataType.Text)]
        public string customerno      { get; set; }

        [DataType(DataType.Text)]
        public string dealno          { get; set; }
        [Required]                                 

        [DataType(DataType.Text)]
        public string dealname        { get; set; }

        [DataType(DataType.Text)]
        public string dealtype        { get; set; }

        [DataType(DataType.Text)]
        public string dealtypeid      { get; set; }

        [DataType(DataType.Text)]
        public string dealstatus      { get; set; }

        [DataType(DataType.Text)]
        public string dealstatusid    { get; set; }

        public Address address        { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phone           { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string fax             { get; set; }

        [DataType(DataType.Text)]
        public string creditstatus    { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phonetollfree   { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string phoneother      { get; set; }

        [DataType(DataType.Text)]
        public string billperiod      { get; set; }

        [DataType(DataType.Text)]
        public string officelocation  { get; set; }

        [DataType(DataType.Text)]
        public string paymentterms    { get; set; }

        [DataType(DataType.Text)]
        public string paymenttype     { get; set; }

        [DataType(DataType.Text)]
        public string porequired      { get; set; }

        [DataType(DataType.Text)]
        public string department      { get; set; }

        [DataType(DataType.Text)]
        public string taxfedno        { get; set; }

        [DataType(DataType.DateTime)]
        public string datestamp       { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}