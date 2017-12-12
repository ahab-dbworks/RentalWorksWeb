using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.Group
{
    public class GroupLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GroupRecord group = new GroupRecord();
        public GroupLogic()
        {
            dataRecords.Add(group);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GroupId { get { return group.GroupId; } set { group.GroupId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Name { get { return group.Name; } set { group.Name = value; } }
        public string Memo { get { return group.Memo; } set { group.Memo = value; } }
        //public bool? Hidesecitems { get { return group.Hidesecitems; } set { group.Hidesecitems = value; } }
        //public string Components { get { return group.Components; } set { group.Components = value; } }
        //public string Menuitem { get { return group.Menuitem; } set { group.Menuitem = value; } }
        //public bool? Disablevalidationoptions { get { return group.Disablevalidationoptions; } set { group.Disablevalidationoptions = value; } }
        public string Security { get { return group.Security; } set { group.Security = value; } }
        public bool? HideNewMenuOptionsByDefault { get { return group.HideNewMenuOptionsByDefault; } set { group.HideNewMenuOptionsByDefault = value; } }
        //public bool? Qehideaccessories { get { return group.Qehideaccessories; } set { group.Qehideaccessories = value; } }
        //public bool? Securitydefaultoff { get { return group.Securitydefaultoff; } set { group.Securitydefaultoff = value; } }
        public string DateStamp { get { return group.DateStamp; } set { group.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}