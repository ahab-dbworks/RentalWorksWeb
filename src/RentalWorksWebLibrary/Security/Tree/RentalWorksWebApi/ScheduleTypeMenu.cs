using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class ScheduleTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ScheduleTypeMenu() : base("{06DA0B03-A6B0-4719-B2AB-9482991F5867}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{0FB3F12F-A187-4AE7-94A6-D4F17E862442}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{AD859E53-31A6-4CFF-9A72-145CA70B3F57}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{2604D4E2-5D4A-4637-B9AC-61AC6354DD49}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{2569C2BA-8DA9-4883-A344-0EA244BAEF1B}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{EAD212A3-8C6D-44B5-B00E-6E56CAA7664F}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{EFEA833D-5D3B-4631-9AB5-BA1221A94B5B}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}