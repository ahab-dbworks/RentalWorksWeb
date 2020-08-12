using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;

namespace FwCore.Modules.Administrator.Group
{
    [FwSqlTable("groups")]
    public class FwGroupLoader : FwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public FwGroupLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string GroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "memo", modeltype: FwDataTypes.Text)]
        public string Memo { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hidesecitems", modeltype: FwDataTypes.Boolean)]
        //public bool? Hidesecitems { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "components", modeltype: FwDataTypes.Text)]
        //public string Components { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "menuitem", modeltype: FwDataTypes.Text)]
        //public string Menuitem { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablevalidationoptions", modeltype: FwDataTypes.Boolean)]
        //public bool? Disablevalidationoptions { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "security", modeltype: FwDataTypes.Text)]
        public string Security { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hidenewmenuoptionsbydefault", modeltype: FwDataTypes.Boolean)]
        public bool? Hidenewmenuoptionsbydefault { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qehideaccessories", modeltype: FwDataTypes.Boolean)]
        //public bool? Qehideaccessories { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "securitydefaultoff", modeltype: FwDataTypes.Boolean)]
        //public bool? Securitydefaultoff { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Boolean)]
        public bool? IsMyGroup
        {
            get
            {
                return getIsMyGroup(GroupId);
            }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string GroupColor
        {
            get { return getGroupColor(GroupId); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
        }
        //------------------------------------------------------------------------------------ 
        private bool? getIsMyGroup(string groupId)
        {
            return (!string.IsNullOrEmpty(groupId)) && (groupId.Equals(UserSession.GroupsId));
        }
        //------------------------------------------------------------------------------------ 
        private string getGroupColor(string groupId)
        {
            string color = null;
            if (getIsMyGroup(groupId).GetValueOrDefault(false))
            {
                color = FwGroupLogic.MY_GROUP_COLOR;
            }
            return color;
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("IsMyGroup")] = getIsMyGroup(row[dt.GetColumnNo("GroupId")].ToString());
                        row[dt.GetColumnNo("GroupColor")] = getGroupColor(row[dt.GetColumnNo("GroupId")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
    }
