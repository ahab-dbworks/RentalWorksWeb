using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class Item
    {
        [DataType(DataType.Text)]
        public string rectype               { get; set; }

        [DataType(DataType.Text)]
        public string indent                { get; set; }

        [DataType(DataType.Text)]
        public string itemorder             { get; set; }

        [DataType(DataType.Text)]
        public string masteritemid          { get; set; }

        [DataType(DataType.Text)]
        public string masterid              { get; set; }

        [DataType(DataType.Text)]
        public string masterno              { get; set; }

        [DataType(DataType.Text)]
        public string inventorydepartmentid { get; set; }

        [DataType(DataType.Text)]
        public string inventorydepartment   { get; set; }

        [DataType(DataType.Text)]
        public string itemclass             { get; set; }

        [DataType(DataType.Text)]
        public string description           { get; set; }

        [DataType(DataType.Text)]
        public string rentfromdate          { get; set; }

        [DataType(DataType.Text)]
        public string rentfromtime          { get; set; }

        [DataType(DataType.Text)]
        public string renttodate            { get; set; }

        [DataType(DataType.Text)]
        public string renttotime            { get; set; }

        [DataType(DataType.Text)]
        public string orderqtyordered       { get; set; }

        [DataType(DataType.Text)]
        public string subqty                { get; set; }

        [DataType(DataType.Text)]
        public string subvendor             { get; set; }

        [DataType(DataType.Text)]
        public string unit                  { get; set; }

        [DataType(DataType.Text)]
        public string price                 { get; set; }

        [DataType(DataType.Text)]
        public string daysinwk              { get; set; }

        [DataType(DataType.Text)]
        public string notes                 { get; set; }

        [DataType(DataType.Text)]
        public string parentid              { get; set; }

        [DataType(DataType.Text)]
        public string nestedmasteritemid    { get; set; }

        [DataType(DataType.Text)]
        public string unitextended          { get; set; }

        [DataType(DataType.Text)]
        public string weeklyextended        { get; set; }

        [DataType(DataType.Text)]
        public string periodextended        { get; set; }

        [DataType(DataType.Text)]
        public string taxable               { get; set; }

        [DataType(DataType.Text)]
        public string warehouseid           { get; set; }

        [DataType(DataType.Text)]
        public string orderqtystaged        { get; set; }

        [DataType(DataType.Text)]
        public string orderqtyout           { get; set; }

        [DataType(DataType.Text)]
        public string orderqtyin            { get; set; }

        [DataType(DataType.Text)]
        public string orderqtyremaining     { get; set; }

        [DataType(DataType.Text)]
        public string qtyconflict           { get; set; }

        [DataType(DataType.Text)]
        public string availabletofulfillqty { get; set; }

        [DataType(DataType.Text)]
        public string trackedby             { get; set; }

        [DataType(DataType.Text)]
        public string warehouseqcrequired   { get; set; }

        [DataType(DataType.Text)]
        public string warehouseqtyinrepair  { get; set; }

        [DataType(DataType.Text)]
        public string warehouseqtyin        { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class OrderItem
    {
        [DataType(DataType.Text)]
        public string masteritemid        { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string masterid            { get; set; }

        [DataType(DataType.Text)]
        public string description         { get; set; }

        [DataType(DataType.Text)]
        public string rentfromdate        { get; set; }

        [DataType(DataType.Text)]
        public string rentfromtime        { get; set; }

        [DataType(DataType.Text)]
        public string renttodate          { get; set; }

        [DataType(DataType.Text)]
        public string renttotime          { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string qtyordered          { get; set; }

        [DataType(DataType.Text)]
        public string unit                { get; set; }

        [DataType(DataType.Text)]
        public string price               { get; set; }

        [DataType(DataType.Text)]
        public string daysinwk            { get; set; }

        [DataType(DataType.Text)]
        public string notes               { get; set; }

        [DataType(DataType.Text)]
        public string parentid            { get; set; }

        [DataType(DataType.Text)]
        public string unitextended        { get; set; }

        [DataType(DataType.Text)]
        public string periodextended      { get; set; }

        [DataType(DataType.Text)]
        public string weeklyextended      { get; set; }

        [DataType(DataType.Text)]
        public string taxable             { get; set; }

        [DataType(DataType.Text)]
        public string inactive            { get; set; }

        [DataType(DataType.Text)]
        public string itemorder           { get; set; }

        [DataType(DataType.Text)]
        public string packageitemid       { get; set; }

        [DataType(DataType.Text)]
        public string nestedmasteritemid  { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}