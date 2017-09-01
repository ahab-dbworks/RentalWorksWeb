using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class StateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StateMenu() : base("{053B9AE1-160F-4FC7-AEEC-A5669E8879D6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{3B028B7E-1A1D-4E63-969B-8A5EC6B5AC37}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{597335CA-EC33-47F3-8CBF-11B6B22D5688}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{C2494727-1248-47F4-83B0-30A9DED95C78}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{830B92E5-57C8-412D-8206-F17AC21D273F}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{E577BB1E-254D-4A2B-8EA8-AA13FB104F81}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{7A293ECA-C36F-4414-94E0-1AA6231CC2BE}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}