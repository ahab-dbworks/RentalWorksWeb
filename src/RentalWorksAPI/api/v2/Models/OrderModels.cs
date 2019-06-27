using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class OrdersAndItemsResponse
    {
        [DataType(DataType.Text)]
        public string dealid      { get; set; }

        [DataType(DataType.Text)]
        public string deal        { get; set; }

        public List<Order> orders { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Order
    {
        [DataType(DataType.Text)]
        public string orderid               { get; set; }

        [DataType(DataType.Text)]
        public string orderno               { get; set; }

        [DataType(DataType.Text)]
        public string orderdesc             { get; set; }

        [DataType(DataType.Text)]
        public string orderdate             { get; set; }

        [DataType(DataType.Text)]
        public string csrid                 { get; set; }

        [DataType(DataType.Text)]
        public string csr                   { get; set; }

        [DataType(DataType.Text)]
        public string pickdate              { get; set; }

        [DataType(DataType.Text)]
        public string picktime              { get; set; }

        [DataType(DataType.Text)]
        public string estrentfrom           { get; set; }

        [DataType(DataType.Text)]
        public string estfromtime           { get; set; }

        [DataType(DataType.Text)]
        public string estrentto             { get; set; }

        [DataType(DataType.Text)]
        public string esttotime             { get; set; }

        [DataType(DataType.Text)]
        public string ordertypeid           { get; set; }

        [DataType(DataType.Text)]
        public string ordertype             { get; set; }

        [DataType(DataType.Text)]
        public string departmentid          { get; set; }

        [DataType(DataType.Text)]
        public string department            { get; set; }

        [DataType(DataType.Text)]
        public string rental                { get; set; }

        [DataType(DataType.Text)]
        public string sales                 { get; set; }

        [DataType(DataType.Text)]
        public string labor                 { get; set; }

        [DataType(DataType.Text)]
        public string misc                  { get; set; }

        [DataType(DataType.Text)]
        public string orderedbycontact      { get; set; }

        [DataType(DataType.Text)]
        public string createdbyuserid       { get; set; }

        [DataType(DataType.Text)]
        public string createdby             { get; set; }

        [DataType(DataType.Text)]
        public string createddate           { get; set; }

        [DataType(DataType.Text)]
        public string lastmodifieddatetime  { get; set; }

        [DataType(DataType.Text)]
        public string lastoutcontractdate   { get; set; }

        [DataType(DataType.Text)]
        public string status                { get; set; }

        [DataType(DataType.Text)]
        public string pono                  { get; set; }

        [DataType(DataType.Text)]
        public string orderunitid           { get; set; }

        [DataType(DataType.Text)]
        public string orderunit             { get; set; }

        public List<OrderNote> ordernotes   { get; set; }

        public Delivery outgoingdelivership { get; set; }

        public Delivery incomingdelivership { get; set; }

        public List<Item> items             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderNote
    {
        [DataType(DataType.Text)]
        public string ordernoteid { get; set; }

        [DataType(DataType.Text)]
        public string notes       { get; set; }

        [DataType(DataType.Text)]
        public string notedate    { get; set; }

        [DataType(DataType.Text)]
        public string notesdesc   { get; set; }

        [DataType(DataType.Text)]
        public string datestamp   { get; set; }

        [DataType(DataType.Text)]
        public string name        { get; set; }

        [DataType(DataType.Text)]
        public string webusersid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}