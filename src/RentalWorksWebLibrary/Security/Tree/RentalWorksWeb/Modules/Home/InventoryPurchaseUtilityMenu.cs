using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class InventoryPurchaseUtilityMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryPurchaseUtilityMenu() : base("{5EEED3A9-40FF-4038-B53D-DB6E777FAC7C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            //Form
            var nodeForm = tree.AddForm("{294A307B-6BE6-406A-88CA-77979805C97B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{59BEEBAD-BCF9-4E33-98EB-B1390799210A}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{9E865363-ECCF-4AD9-BD52-0F2F0286B749}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{938E89FA-AC99-4968-B2E3-DCF6831B33E0}", nodeFormSubMenu.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}