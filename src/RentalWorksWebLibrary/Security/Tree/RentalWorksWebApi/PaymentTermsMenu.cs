using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class PaymentTermsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PaymentTermsMenu() : base("{E37D94D8-FA1F-41F2-A0E5-8562ED76F015}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{73B16727-CC8B-49BA-8115-5BF6F7401915}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{9D2B252C-9EF5-4C0C-9A00-363F119B1901}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{031A74F2-B9C8-439B-AFEC-D05F390814B3}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{670213AE-A905-4385-A473-58F70CA85E38}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{E944F927-644D-4D55-A127-5650BB7F10A2}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{87492918-4C31-4593-9DCE-2823106B3E74}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}