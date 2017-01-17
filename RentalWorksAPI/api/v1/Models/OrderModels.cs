using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v1.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Order
    {
        public string orderid                    { get; set; }
        public string orderno                    { get; set; }
        [Required]
        public string orderdesc                  { get; set; }
        public string location                   { get; set; }
        public string warehouse                  { get; set; }
        [Required]
        public string rental                     { get; set; }
        [Required]
        public string sales                      { get; set; }
        [Required]
        public string estrentfrom                { get; set; }
        public string estfromtime                { get; set; }
        [Required]
        public string estrentto                  { get; set; }
        public string esttotime                  { get; set; }
        public string billperiodstart            { get; set; }
        public string billperiodend              { get; set; }
        [Required]
        public string webusersid                 { get; set; }
        public OrderDeal deal                    { get; set; }
        public Customer customer                 { get; set; }
        public string pono                       { get; set; }
        public string status                     { get; set; }
        public string ordertype                  { get; set; }
        public string ratetype                   { get; set; }
        public List<OrderNote> ordernotes        { get; set; }
        public Delivery outgoingdelivership      { get; set; }
        public string datestamp                  { get; set; }
        public List<OrderItem> items             { get; set; }
        public string orderdate                  { get; set; }
        public string agent                      { get; set; }
        public string projectmanager             { get; set; }
        public string dealtype                   { get; set; }
        public string department                 { get; set; }
        public string orderlocation              { get; set; }
        [StringLength(20, ErrorMessage = "{0} must be no longer than {1} characters long.")]
        public string refno                      { get; set; }
        public string taxoption                  { get; set; }
        public string requiredbydate             { get; set; }
        public string requiredbytime             { get; set; }
        public string deliverondate              { get; set; }
        public string pickdate                   { get; set; }
        public string picktime                   { get; set; }
        public string loadindate                 { get; set; }
        public string testdate                   { get; set; }
        public string strikedate                 { get; set; }
        public string rentaltaxrate1             { get; set; }
        public string rentaltaxrate2             { get; set; }
        public string agentid                    { get; set; }
        public string departmentid               { get; set; }
        public string projectmanagerid           { get; set; }
        public string dealtypeid                 { get; set; }
        public string orderunitid                { get; set; }
        public string orderunit                  { get; set; }
        public string ordertotal                 { get; set; }
        public string ordergrosstotal            { get; set; }
        public string ordertypedescription       { get; set; }
        public string onlineorderno              { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderItems
    {
        [Required]
        public string orderid         { get; set; }
        public List<OrderItem> items  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderSubmit
    {
        [Required]
        public string orderid    { get; set; }
        [Required]
        public string webusersid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class NewQuoteVersionResult : Error
    {
        public string orderid    { get; set; }
        public string neworderid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class DeleteLineItem
    {
        [Required]
        public string orderid      { get; set; }
        [Required]
        public string masteritemid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderDeal
    {
        [Required]
        public string dealid          { get; set; }
        public string customer        { get; set; }
        public string customerid      { get; set; }
        public string dealno          { get; set; }
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
        public string datestamp       { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderParameters
    {
        public List<String> statuses  { get; set; }
        public string       rental    { get; set; }
        public string       sales     { get; set; }
        public string       dealid    { get; set; }
        public string       datestamp { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class QuoteToOrderParameters
    {
        [Required]
        public string orderid    { get; set; }
        [Required]
        public string webusersid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class QuoteToOrderResult : Error
    {
        public string neworderid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class CancelOrderParameters
    {
        [Required]
        public string orderid    { get; set; }
        [Required]
        public string webusersid { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}