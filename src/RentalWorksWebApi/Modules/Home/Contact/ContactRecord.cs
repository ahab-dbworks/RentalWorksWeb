using System.Threading.Tasks;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Home.Contact
{
    [FwSqlTable("contact")]
    public class ContactRecord : RwDataReadWriteRecord
    {
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string contactid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "salutation", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, precision: 0, scale: 0)]
        public string salutation { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string add1 { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string mi { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string add2 { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string city { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, precision: 0, scale: 0)]
        public string zip { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string phone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string state { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string fax { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string email { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string officephone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string pager { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string pagerpin { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string faxext { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string cellular { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime", maxlength: 8, precision: 23, scale: 3)]
        public string datestamp { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string countryid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string directphone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string ext { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string directext { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string inactive { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "info", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string info { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "website", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, precision: 0, scale: 0)]
        public string website { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string activedate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string inactivedate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "rowguid", modeltype: FwDataTypes.Text, sqltype: "uniqueidentifier", maxlength: 16, precision: 0, scale: 0)]
        public string rowguid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string inputbyid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string modbyid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string inputdate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string moddate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, precision: 0, scale: 0)]
        public string company { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contactrecordtype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string contactrecordtype { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string contacttitleid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contacttype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, precision: 0, scale: 0)]
        public string contacttype { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string dealid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "jobtitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, precision: 0, scale: 0)]
        public string jobtitle { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string locationid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string warehouseid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webstatus", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string webstatus { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webstatusasof", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime", maxlength: 8, precision: 23, scale: 3)]
        public string webstatusasof { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webstatusupdatebyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string webstatusupdatebyusersid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "persontype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, precision: 0, scale: 0)]
        public string persontype { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "overridepastdue", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string overridepastdue { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30, precision: 0, scale: 0)]
        public string fname { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30, precision: 0, scale: 0)]
        public string lname { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string person { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string namefml { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string barcode { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "sourceid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string sourceid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webcatalogid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string webcatalogid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "poordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string poordertypeid { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contactnametype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, precision: 0, scale: 0)]
        public string contactnametype { get; set; }
    }
}
