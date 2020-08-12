using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace FwCore.Modules.Administrator.Group
{

    [FwLogic(Id: "7ug1DbMT8Fu")]
    public class FwGroupLogic : FwBusinessLogic
    {
        public static string MY_GROUP_COLOR = "#ff8000";
        //------------------------------------------------------------------------------------ 
        FwGroupRecord group = new FwGroupRecord();
        FwGroupLoader groupLoader = new FwGroupLoader();
        public FwGroupLogic() : base()
        {
            dataRecords.Add(group);
            dataLoader = groupLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "hd3IJSvhcoe", IsPrimaryKey: true)]
        public string GroupId { get { return group.GroupId; } set { group.GroupId = value; } }

        [FwLogicProperty(Id: "H8GowCzMARG", IsRecordTitle: true)]
        public string Name { get { return group.Name; } set { group.Name = value; } }

        [FwLogicProperty(Id: "ISyc8zQRyyl")]
        public string Memo { get { return group.Memo; } set { group.Memo = value; } }

        //public bool? Hidesecitems { get { return group.Hidesecitems; } set { group.Hidesecitems = value; } }
        //public string Components { get { return group.Components; } set { group.Components = value; } }
        //public string Menuitem { get { return group.Menuitem; } set { group.Menuitem = value; } }
        //public bool? Disablevalidationoptions { get { return group.Disablevalidationoptions; } set { group.Disablevalidationoptions = value; } }

        [FwLogicProperty(Id: "JKXvafeErCU")]
        public string Security { get { return group.Security; } set { group.Security = value; } }

        [FwLogicProperty(Id: "EHd0Z8iGIcd")]
        public bool? HideNewMenuOptionsByDefault { get { return group.HideNewMenuOptionsByDefault; } set { group.HideNewMenuOptionsByDefault = value; } }

        [FwLogicProperty(Id: "JTUQvSiDM7PEx", IsReadOnly: true)]
        public bool? IsMyGroup { get; set; }

        [FwLogicProperty(Id: "oX0ymm4ONYoad")]
        public string GroupColor { get; set; }

        //public bool? Qehideaccessories { get { return group.Qehideaccessories; } set { group.Qehideaccessories = value; } }
        //public bool? Securitydefaultoff { get { return group.Securitydefaultoff; } set { group.Securitydefaultoff = value; } }

        [FwLogicProperty(Id: "iFwagsh4fuF")]
        public string DateStamp { get { return group.DateStamp; } set { group.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}