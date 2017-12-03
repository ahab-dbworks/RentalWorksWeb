using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Administrator.Group
{
    [FwSqlTable("groups")]
    public class GroupRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string GroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "memo", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Memo { get; set; }
        //------------------------------------------------------------------------------------
        //[FwSqlDataField(column: "hidesecitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Hidesecitems { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "components", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Components { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "menuitem", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Menuitem { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablevalidationoptions", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablevalidationoptions { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "security", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1)]
        public string Security { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hidenewmenuoptionsbydefault", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? HideNewMenuOptionsByDefault { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qehideaccessories", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qehideaccessories { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "securitydefaultoff", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Securitydefaultoff { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}