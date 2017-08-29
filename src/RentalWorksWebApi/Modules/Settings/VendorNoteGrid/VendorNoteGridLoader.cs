using System.Threading.Tasks;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.VendorNoteGrid
{
    [FwSqlTable("vendnoteview")]
    public class VendorNoteGridLoader : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string vendorid { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendnoteid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string vendnoteid { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string notedate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string name { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notesdesc", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string notesdesc { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 255, precision: 0, scale: 0)]
        public string notes { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notify", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string notify { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "notestbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string notestbyid { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime", maxlength: 8, precision: 23, scale: 3)]
        public string datestamp { get; set; }

    }

}
