using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class CreditStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CreditStatusMenu() : base("{CD2569F2-6BEB-488C-AB9F-3A6C6D821ED3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{E9EE7E25-97AB-4FEC-94A6-9E6632A9CAE4}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{4D488DF8-D4D5-4F1C-AD95-3D96BF16D27C}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{08B01AE2-A01D-455B-B170-04C045B1EA40}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{54CB74D5-975C-41C7-A842-673C5998F0C5}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{B6F97D27-4993-4528-9F0E-68A761208810}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{463B4E6A-4D35-4D4F-8E55-0AFD4FACC8F8}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}