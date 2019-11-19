using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.DiscountTemplateSettings.DiscountTemplate
{
    [FwSqlTable("discounttemplateview")]
    public class DiscountTemplateLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplateid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DiscountTemplateId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplate", modeltype: FwDataTypes.Text)]
        public string DiscountTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iscompany", modeltype: FwDataTypes.Boolean)]
        public bool? IsCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Space { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldw", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacediscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? SpaceDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalasof", modeltype: FwDataTypes.Date)]
        public string RentalAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesasof", modeltype: FwDataTypes.Date)]
        public string SalesAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborasof", modeltype: FwDataTypes.Date)]
        public string LaborAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscasof", modeltype: FwDataTypes.Date)]
        public string MiscAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceasof", modeltype: FwDataTypes.Date)]
        public string SpaceAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedw", modeltype: FwDataTypes.Decimal)]
        public decimal? SpaceDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applydiscounttocustomrate", modeltype: FwDataTypes.Boolean)]
        public bool? ApplyDiscountToCustomRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(iscompany <> 'T')");
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}