using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;

namespace FwCore.Modules.Administrator.Group
{
    [FwSqlTable("groups")]
    public class FwGroupRecord : FwDataReadWriteRecord
    {
        public FwGroupRecord() : base() { }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string GroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, required: true)]
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
        protected override async Task AfterSaveAsync(AfterSaveDataRecordEventArgs e)
        {
            string groupIndex = $"{this.GroupId},true";
            FwAmGroupTree groupTree = null;
            if (FwAppManager.Tree.GroupTrees.TryGetValue(groupIndex, out groupTree) && groupTree != null)
            {
                FwAppManager.Tree.GroupTrees.TryRemove(groupIndex, out groupTree);
                await FwAppManager.Tree.GetGroupsTreeAsync(this.GroupId, true);
            }
            await base.AfterSaveAsync(e);
        }
        //------------------------------------------------------------------------------------
    }
}