using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Item
    {
        public string rectype               { get; set; }
        public string indent                { get; set; }
        public string itemorder             { get; set; }
        public string masteritemid          { get; set; }
        public string masterid              { get; set; }
        public string masterno              { get; set; }
        public string inventorydepartmentid { get; set; }
        public string inventorydepartment   { get; set; }
        public string itemclass             { get; set; }
        public string description           { get; set; }
        public string rentfromdate          { get; set; }
        public string rentfromtime          { get; set; }
        public string renttodate            { get; set; }
        public string renttotime            { get; set; }
        public string qtyordered            { get; set; }
        public string subqty                { get; set; }
        public string subvendor             { get; set; }
        public string unit                  { get; set; }
        public string price                 { get; set; }
        public string daysinwk              { get; set; }
        public string notes                 { get; set; }
        public string parentid              { get; set; }
        public string unitextended          { get; set; }
        public string weeklyextended        { get; set; }
        public string periodextended        { get; set; }
        public string taxable               { get; set; }
        public string warehouseid           { get; set; }
        public string qtystaged             { get; set; }
        public string qtyout                { get; set; }
        public string qtyin                 { get; set; }
        public string qtyremaining          { get; set; }
        public string qtyconflict           { get; set; }
        public string availabletofulfillqty { get; set; }
        public string trackedby             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}