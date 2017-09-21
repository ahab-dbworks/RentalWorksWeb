using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Settings.BarCodeRange
{
    [FwSqlTable("barcoderangeview")]
    public class BarCodeRangeLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcoderangeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string BarCodeRangeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefix", modeltype: FwDataTypes.Text)]
        public string Prefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodefrom", modeltype: FwDataTypes.Integer)]
        public int BarcodeFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeto", modeltype: FwDataTypes.Integer)]
        public int BarcodeTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}