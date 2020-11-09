using FwStandard.Data;
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Threading.Tasks; 
using System.Data; 
using System.Reflection; 
namespace WebApi.Modules.Reports.DeliveryLabel 
{ 
[FwSqlTable("deliveryview")] 
public class DeliveryLabelLoader: AppReportLoader 
{ 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)] 
public string RowType { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text, isPrimaryKey: true)] 
public string DeliveryLabelId { get; set; } = ""; 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "deliverytype", modeltype: FwDataTypes.Text)] 
public string Deliverytype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "requireddate", modeltype: FwDataTypes.Date)] 
public string Requireddate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)] 
public string Requiredtime { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "targetshipdate", modeltype: FwDataTypes.Date)] 
public string Targetshipdate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "targetshiptime", modeltype: FwDataTypes.Text)] 
public string Targetshiptime { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromlocation", modeltype: FwDataTypes.Text)] 
public string Fromlocation { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcontact", modeltype: FwDataTypes.Text)] 
public string Fromcontact { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcontactalternate", modeltype: FwDataTypes.Text)] 
public string Fromcontactalternate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcontactphone", modeltype: FwDataTypes.Text)] 
public string Fromcontactphone { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcontactphonealternate", modeltype: FwDataTypes.Text)] 
public string Fromcontactphonealternate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromattention", modeltype: FwDataTypes.Text)] 
public string Fromattention { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromadd1", modeltype: FwDataTypes.Text)] 
public string Fromadd1 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromadd2", modeltype: FwDataTypes.Text)] 
public string Fromadd2 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcity", modeltype: FwDataTypes.Text)] 
public string Fromcity { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromstate", modeltype: FwDataTypes.Text)] 
public string Fromstate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromzip", modeltype: FwDataTypes.Text)] 
public string Fromzip { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcountry", modeltype: FwDataTypes.Text)] 
public string Fromcountry { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcrossstreets", modeltype: FwDataTypes.Text)] 
public string Fromcrossstreets { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tolocation", modeltype: FwDataTypes.Text)] 
public string Tolocation { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "toattention", modeltype: FwDataTypes.Text)] 
public string Toattention { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "toadd1", modeltype: FwDataTypes.Text)] 
public string Toadd1 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "toadd2", modeltype: FwDataTypes.Text)] 
public string Toadd2 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocity", modeltype: FwDataTypes.Text)] 
public string Tocity { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tostate", modeltype: FwDataTypes.Text)] 
public string Tostate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tozip", modeltype: FwDataTypes.Text)] 
public string Tozip { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocountry", modeltype: FwDataTypes.Text)] 
public string Tocountry { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocountryid", modeltype: FwDataTypes.Text)] 
public string TocountryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocrossstreets", modeltype: FwDataTypes.Text)] 
public string Tocrossstreets { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tometric", modeltype: FwDataTypes.Boolean)] 
public bool? Tometric { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocontact", modeltype: FwDataTypes.Text)] 
public string Tocontact { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocontactalternate", modeltype: FwDataTypes.Text)] 
public string Tocontactalternate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocontactphone", modeltype: FwDataTypes.Text)] 
public string Tocontactphone { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocontactphonealternate", modeltype: FwDataTypes.Text)] 
public string Tocontactphonealternate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "tocontactfax", modeltype: FwDataTypes.Text)] 
public string Tocontactfax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "deliverynotes", modeltype: FwDataTypes.Text)] 
public string Deliverynotes { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text)] 
public string CarrierId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)] 
public string Carrier { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text)] 
public string ShipviaId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "shipvia", modeltype: FwDataTypes.Text)] 
public string Shipvia { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)] 
public string InvoiceId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "vendorinvoiceid", modeltype: FwDataTypes.Text)] 
public string VendorinvoiceId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)] 
public string Delivertype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)] 
public string Add1 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)] 
public string Add2 { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)] 
public string City { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)] 
public string State { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)] 
public string Zip { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)] 
public string CountryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)] 
public string Location { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contact", modeltype: FwDataTypes.Text)] 
public string Contact { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactphone", modeltype: FwDataTypes.Text)] 
public string Contactphone { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "carrieracct", modeltype: FwDataTypes.Text)] 
public string Carrieracct { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "estimatedfreight", modeltype: FwDataTypes.Decimal)] 
public decimal? Estimatedfreight { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "freightinvamt", modeltype: FwDataTypes.Decimal)] 
public decimal? Freightinvamt { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "chargetype", modeltype: FwDataTypes.Text)] 
public string Chargetype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "freighttrackno", modeltype: FwDataTypes.Text)] 
public string Freighttrackno { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "trackingurl", modeltype: FwDataTypes.Text)] 
public string Trackingurl { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactfax", modeltype: FwDataTypes.Text)] 
public string Contactfax { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "fromcountryid", modeltype: FwDataTypes.Text)] 
public string FromcountryId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "template", modeltype: FwDataTypes.Text)] 
public string Template { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "addresstype", modeltype: FwDataTypes.Text)] 
public string Addresstype { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text)] 
public string Attention { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "direction", modeltype: FwDataTypes.Text)] 
public string Direction { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "dropship", modeltype: FwDataTypes.Boolean)] 
public bool? Dropship { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "ondate", modeltype: FwDataTypes.Date)] 
public string Ondate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)] 
public string OrderId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "packagecode", modeltype: FwDataTypes.Text)] 
public string Packagecode { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactalternate", modeltype: FwDataTypes.Text)] 
public string Contactalternate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "contactphonealternate", modeltype: FwDataTypes.Text)] 
public string Contactphonealternate { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "crossstreets", modeltype: FwDataTypes.Text)] 
public string Crossstreets { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "billpofreightonorder", modeltype: FwDataTypes.Boolean)] 
public bool? Billpofreightonorder { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "onlineorderno", modeltype: FwDataTypes.Text)] 
public string Onlineorderno { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "onlineorderstatus", modeltype: FwDataTypes.Text)] 
public string Onlineorderstatus { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "venueid", modeltype: FwDataTypes.Text)] 
public string VenueId { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "venue", modeltype: FwDataTypes.Text)] 
public string Venue { get; set; } 
//------------------------------------------------------------------------------------ 
[FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)] 
public string DateStamp { get; set; } 
//------------------------------------------------------------------------------------ 
public async Task<FwJsonDataTable> RunReportAsync(DeliveryLabelRequest request) 
{ 
useWithNoLock = false; 
FwJsonDataTable dt = null; 
using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString)) 
{ 
//--------------------------------------------------------------------------------- 
// below uses a "select" query to populate the FwJsonDataTable 
FwSqlSelect select = new FwSqlSelect(); 
select.EnablePaging = false; 
select.UseOptionRecompile = true; 
using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout)) 
{ 
SetBaseSelectQuery(select, qry); 
select.Parse(); 
//select.AddWhere("(xxxxid ^> ')"); 
//select.AddWhereIn("locationid", request.OfficeLocationId); 
//select.AddWhereIn("departmentid", request.DepartmentId); 
//addDateFilterToSelect("datefieldname1", request.DateValue1, select, "^>=", "dateparametername(optional)"); 
//addDateFilterToSelect("datefieldname2", request.DateValue2, select, "^<=", "dateparametername(optional)"); 
//if (!request.BooleanField.GetValueOrDefault(false)) 
//{ 
//    select.AddWhere("somefield ^<^> 'T'"); 
//} 
select.AddOrderBy("field1,field2"); 
dt = await qry.QueryToFwJsonTableAsync(select, false); 
} 
//--------------------------------------------------------------------------------- 
//--------------------------------------------------------------------------------- 
//--------------------------------------------------------------------------------- 
// below uses a stored procedure to populate the FwJsonDataTable 
using (FwSqlCommand qry = new FwSqlCommand(conn, "procedurename", this.AppConfig.DatabaseSettings.ReportTimeout)) 
{ 
qry.AddParameter("@someid", SqlDbType.Text, ParameterDirection.Input, request.SomeId); 
qry.AddParameter("@somedate", SqlDbType.Date, ParameterDirection.Input, request.SomeDate); 
AddPropertiesAsQueryColumns(qry); 
dt = await qry.QueryToFwJsonTableAsync(false, 0); 
} 
//--------------------------------------------------------------------------------- 
} 
if (request.IncludeSubHeadingsAndSubTotals) 
{ 
string[] totalFields = new string[] { "RentalTotal", "SalesTotal" }; 
dt.InsertSubTotalRows("GroupField1", "RowType", totalFields); 
dt.InsertSubTotalRows("GroupField2", "RowType", totalFields); 
dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields); 
} 
return dt; 
} 
//------------------------------------------------------------------------------------ 
} 
} 
