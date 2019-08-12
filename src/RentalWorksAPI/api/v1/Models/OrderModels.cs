using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Order
    {
        [DataType(DataType.Text)]
        public string orderid                    { get; set; }

        [DataType(DataType.Text)]
        public string orderno                    { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string orderdesc                  { get; set; }

        [DataType(DataType.Text)]
        public string location                   { get; set; }

        [DataType(DataType.Text)]
        public string warehouse                  { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string rental                     { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string sales                      { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string estrentfrom                { get; set; }

        [DataType(DataType.Text)]
        public string estfromtime                { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string estrentto                  { get; set; }

        [DataType(DataType.Text)]
        public string esttotime                  { get; set; }

        [DataType(DataType.Text)]
        public string billperiodstart            { get; set; }

        [DataType(DataType.Text)]
        public string billperiodend              { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string webusersid                 { get; set; }

        [DataType(DataType.Text)]
        public OrderDeal deal                    { get; set; }

        [DataType(DataType.Text)]
        public Customer customer                 { get; set; }

        [DataType(DataType.Text)]
        public string pono                       { get; set; }

        [DataType(DataType.Text)]
        public string status                     { get; set; }

        [DataType(DataType.Text)]
        public string ordertype                  { get; set; }

        [DataType(DataType.Text)]
        public string ratetype                   { get; set; }

        [DataType(DataType.Text)]
        public List<OrderNote> ordernotes        { get; set; }

        [DataType(DataType.Text)]
        public Delivery outgoingdelivership      { get; set; }

        [DataType(DataType.Text)]
        public string datestamp                  { get; set; }

        [DataType(DataType.Text)]
        public List<OrderItem> items             { get; set; }

        [DataType(DataType.Text)]
        public string orderdate                  { get; set; }

        [DataType(DataType.Text)]
        public string agent                      { get; set; }

        [DataType(DataType.Text)]
        public string projectmanager             { get; set; }

        [DataType(DataType.Text)]
        public string dealtype                   { get; set; }

        [DataType(DataType.Text)]
        public string department                 { get; set; }

        [DataType(DataType.Text)]
        public string orderlocation              { get; set; }

        [StringLength(20, ErrorMessage = "{0} must be no longer than {1} characters long.")]
        public string refno                      { get; set; }

        [DataType(DataType.Text)]
        public string taxoption                  { get; set; }

        [DataType(DataType.Text)]
        public string requiredbydate             { get; set; }

        [DataType(DataType.Text)]
        public string requiredbytime             { get; set; }

        [DataType(DataType.Text)]
        public string deliverondate              { get; set; }

        [DataType(DataType.Date)]
        public string pickdate                   { get; set; }

        [DataType(DataType.Time)]
        public string picktime                   { get; set; }

        [DataType(DataType.Text)]
        public string loadindate                 { get; set; }

        [DataType(DataType.Date)]
        public string testdate                   { get; set; }

        [DataType(DataType.Text)]
        public string strikedate                 { get; set; }

        [DataType(DataType.Text)]
        public string rentaltaxrate1             { get; set; }

        [DataType(DataType.Text)]
        public string rentaltaxrate2             { get; set; }

        [DataType(DataType.Text)]
        public string agentid                    { get; set; }

        [DataType(DataType.Text)]
        public string departmentid { get; set; }

        [DataType(DataType.Text)]
        public string projectmanagerid           { get; set; }

        [DataType(DataType.Text)]
        public string dealtypeid                 { get; set; }

        [DataType(DataType.Text)]
        public string orderunitid                { get; set; }

        [DataType(DataType.Text)]
        public string orderunit                  { get; set; }

        [DataType(DataType.Text)]
        public string ordertotal                 { get; set; }

        [DataType(DataType.Text)]
        public string ordergrosstotal            { get; set; }

        [DataType(DataType.Text)]
        public string ordertypedescription       { get; set; }

        [DataType(DataType.Text)]
        public string onlineorderno              { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderItems
    {
        [Required]
        [DataType(DataType.Text)]
        public string orderid         { get; set; }

        [DataType(DataType.Text)]
        public List<OrderItem> items  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderSubmit
    {
        [Required]
        [DataType(DataType.Text)]
        public string orderid    { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string webusersid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class NewQuoteVersionResult : Error
    {
        [DataType(DataType.Text)]
        public string orderid    { get; set; }

        [DataType(DataType.Text)]
        public string neworderid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class DeleteLineItem
    {
        [Required]
        [DataType(DataType.Text)]
        public string orderid      { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string masteritemid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderDeal
    {
        [Required]
        [DataType(DataType.Text)]
        public string dealid          { get; set; }

        [DataType(DataType.Text)]
        public string customer        { get; set; }

        [DataType(DataType.Text)]
        public string customerid      { get; set; }

        [DataType(DataType.Text)]
        public string dealno          { get; set; }

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

        [DataType(DataType.DateTime)]
        public string datestamp       { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderParameters
    {
        public List<String> statuses  { get; set; }

        [DataType(DataType.Text)]
        public string       rental    { get; set; }

        [DataType(DataType.Text)]
        public string       sales     { get; set; }

        [DataType(DataType.Text)]
        public string       dealid    { get; set; }

        [DataType(DataType.Text)]
        public string       datestamp { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class QuoteToOrderParameters
    {
        [Required]
        [DataType(DataType.Text)]
        public string orderid    { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string webusersid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class QuoteToOrderResult : Error
    {
        [DataType(DataType.Text)]
        public string neworderid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CancelOrderParameters
    {
        [Required]
        [DataType(DataType.Text)]
        public string orderid    { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string webusersid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CopyOrderParameters
    {
        [Required]
        [DataType(DataType.Text)]
        public string orderid            { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string webusersid         { get; set; }

        //public string dealid             { get; set; } //2017-03-08 MY: Not present in 2015

        [DataType(DataType.Text)]
        public string ordertype          { get; set; }

        [DataType(DataType.Text)]
        public string copyquoterates     { get; set; }

        [DataType(DataType.Text)]
        public string copyinventoryrates { get; set; }

        [DataType(DataType.Text)]
        public string copyquotedates     { get; set; }

        [DataType(DataType.Text)]
        public string usecurrentdate     { get; set; }

        [DataType(DataType.Text)]
        public string copylineitemnotes  { get; set; }

        [DataType(DataType.Text)]
        public string combinesubs        { get; set; }

        [DataType(DataType.Text)]
        public string copydocuments      { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CopyOrderResult : Error
    {
        [DataType(DataType.Text)]
        public string neworderid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}