using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class CustomerCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerCategoryMenu() : base("{545C1F2F-BB9C-422C-8E41-A69DAF9DAC9E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{33F721F5-0D91-464C-AFA7-FA46622CE3C0}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{D1CECF78-CC21-4B3C-8F16-11D31B996032}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{FF697F23-9150-4252-8C58-A0063419B88E}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{0178C657-4473-4889-945A-A2F88B3D31C0}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{CDA85B7B-F766-410C-9B8E-D0DEFA313341}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{D212CE16-E6D8-4EBE-8ACD-4F9C086E1F03}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}