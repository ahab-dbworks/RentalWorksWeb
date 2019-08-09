using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebApi.Logic;

namespace WebApi.Modules.Home.OrderContact
{
    [FwSqlTable("dbo.funccompcontact2(@orderid,'F')")]
    public class OrderContactLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordercontactid", modeltype: FwDataTypes.Integer, identity: true, isPrimaryKey: true)]
        public int? OrderContactId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)]
        public string NameFml { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text)]
        public string NameLfm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text)]
        public string Person { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PersonColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text)]
        public string ContactTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text)]
        public string MobilePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "jobtitle", modeltype: FwDataTypes.Text)]
        public string JobTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text)]
        public string CompanyContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflag", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedbyflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsOrderedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productioncontact", modeltype: FwDataTypes.Boolean)]
        public bool? IsProductionContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrintable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactonorder", modeltype: FwDataTypes.Boolean)]
        public bool? ContactOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;

            if ((OrderId == null) || (OrderId.Equals(string.Empty)))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("OrderId"))
                    {
                        OrderId = uniqueIds["OrderId"].ToString();
                    }
                }
            }

            //if ((OrderId == null) || (OrderId.Equals(string.Empty)))
            if (string.IsNullOrEmpty(OrderId))
            {
                OrderId = AppFunc.GetStringDataAsync(AppConfig, "ordercontact", "ordercontactid", OrderContactId.ToString(), "orderid").Result;
            }


            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            //addFilterToSelect("OrderId", "orderid", select, request);

            if (!OrderId.Equals(string.Empty))
            {
                select.AddParameter("@orderid", OrderId);
            }

        }
        //------------------------------------------------------------------------------------ 
    }
}
