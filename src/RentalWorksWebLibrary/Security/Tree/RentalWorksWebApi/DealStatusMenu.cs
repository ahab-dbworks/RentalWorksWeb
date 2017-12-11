using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class DealStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealStatusMenu() : base("{01C4EC0C-F4C0-4E8C-A079-D2CA37A5EDA1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{051A9D01-5B55-4805-931A-75937FA04F33}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{A17E8E69-1427-472E-9F15-EA4E31590ABE}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{A8FFA28F-E260-4B28-BB88-B4A5C2F0745B}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{C0DAAFEF-B169-4153-A6D4-C3B6D3C07F94}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{0C9DE590-155F-459E-B33E-90AEABFB5F50}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{27EDAA20-C1E1-4687-9A17-32D7E812A17E}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}