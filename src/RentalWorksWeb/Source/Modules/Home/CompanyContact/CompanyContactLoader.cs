using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic; 
namespace RentalWorksWebApi.Modules..CompanyContact 
{ 
[FwSqlTable("compcontactviewweb")] 
public class CompanyContactLoader: RwDataLoadRecord 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)] 
public string ContactId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text)] 
public string CompanyId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text)] 
public string CompanyContactId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)] 
public string Company { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)] 
public string Email { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "mobilephone", modeltype: FwDataTypes.Text)] 
public string MobilePhone { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text)] 
public string Officephone { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text)] 
public string Ext { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text)] 
public string Pager { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text)] 
public string PagerPin { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)] 
public string Fax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text)] 
public string FaxExt { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text)] 
public string Directphone { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text)] 
public string DirectExt { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "activedate", modeltype: FwDataTypes.Date)] 
public string ActiveDate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.Date)] 
public string InactiveDate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "primaryflag", modeltype: FwDataTypes.Boolean)] 
public bool PrimaryFlag { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "authorized", modeltype: FwDataTypes.Boolean)] 
public bool Authorized { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text)] 
public string ContacttitleId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text)] 
public string ContactTtitle { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "jobtitle", modeltype: FwDataTypes.Text)] 
public string Jobtitle { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactrecordtype", modeltype: FwDataTypes.Text)] 
public string ContactRecordType { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactrecordtypecolor", modeltype: FwDataTypes.Text)] 
public string Contactrecordtypecolor { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)] 
public bool Inactive { get; set; } 
//------------------------------------------------------------------------------------ 
protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null) 
{ 
base.SetBaseSelectQuery(select, qry, customFields, request); 
select.Parse(); 
//select.AddWhere("(xxxtype = 'ABCDEF')"); 
//addFilterToSelect("UniqueId", "uniqueid", select, request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
