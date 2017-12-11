using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class PaymentTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PaymentTypeMenu() : base("{326750A2-B9DA-4245-9D44-485724CD6669}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{EC4EEF82-098C-44BB-AA37-EEED5CAD4A1F}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{CB090F27-15DB-403F-9C2D-40B76B94B9D7}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{AD05A11C-2165-4928-B16B-63B473D3D3AB}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{27B7D3E1-40FF-4798-9CD1-5AE439B0CEC4}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{ADEE07E7-7E95-4354-B1FF-474F1D9C4196}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{7D36626F-F871-4894-A2BC-27E84A85B280}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}