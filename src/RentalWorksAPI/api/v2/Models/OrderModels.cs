using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class OrdersAndItemsResponse
    {
        public string dealid      { get; set; }
        public string deal        { get; set; }
        public List<Order> orders { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Order
    {
        public string orderid               { get; set; }
        public string orderno               { get; set; }
        public string orderdesc             { get; set; }
        public string orderdate             { get; set; }
        public string csrid                 { get; set; }
        public string csr                   { get; set; }
        public string pickdate              { get; set; }
        public string picktime              { get; set; }
        public string estrentfrom           { get; set; }
        public string estfromtime           { get; set; }
        public string estrentto             { get; set; }
        public string esttotime             { get; set; }
        public string ordertypeid           { get; set; }
        public string ordertype             { get; set; }
        public string departmentid          { get; set; }
        public string department            { get; set; }
        public string rental                { get; set; }
        public string sales                 { get; set; }
        public string labor                 { get; set; }
        public string misc                  { get; set; }
        public string orderedbycontact      { get; set; }
        public string createdbyuserid       { get; set; }
        public string createdby             { get; set; }
        public string createddate           { get; set; }
        public string lastmodifieddatetime  { get; set; }
        public string lastoutcontractdate   { get; set; }
        public string status                { get; set; }
        public string pono                  { get; set; }
        public string orderunitid           { get; set; }
        public string orderunit             { get; set; }
        public List<OrderNote> ordernotes   { get; set; }
        public Delivery outgoingdelivership { get; set; }
        public Delivery incomingdelivership { get; set; }
        public List<Item> items             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderNote
    {
        public string ordernoteid { get; set; }
        public string notes       { get; set; }
        public string notedate    { get; set; }
        public string notesdesc   { get; set; }
        public string datestamp   { get; set; }
        public string name        { get; set; }
        public string webusersid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}