using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ItemAttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemAttributeValueGridMenu() : base("{22D75843-E915-4956-9B25-C52E815F3C5E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{BBA5F3C3-C693-41FB-A7AB-220BD37AACEF}", MODULEID);
            tree.AddEditMenuBarButton("{C0DC47EE-80FE-49C4-B56D-E5845FA1B361}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}