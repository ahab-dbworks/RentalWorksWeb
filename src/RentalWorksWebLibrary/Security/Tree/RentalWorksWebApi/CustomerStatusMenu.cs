using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class CustomerStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerStatusMenu() : base("{D9FEADB4-F079-4437-834C-4D8F5C0ACFA5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{3A76D9E8-293B-4DFD-B195-5E2A72F8B9C3}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{6B435C76-F8AB-4237-985B-27BCC7679BCA}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{08B76E5F-E392-4FBA-AF90-34F1D200564B}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{65B4BA20-5B8E-4500-AF29-E40FD3F6FFF7}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{499B6891-64AF-4909-9709-C202028BBC8B}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{6479471B-51F4-4981-8949-DEAD031A3A0A}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}