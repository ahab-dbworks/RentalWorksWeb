using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryCompleteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompleteGridMenu() : base("{3CB67F46-92B8-4F42-A04C-DB5BA6B52B29}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6EC40087-1154-4DB2-ACEE-E28A1820A0E9}", MODULEID);
            tree.AddEditMenuBarButton("{78E4EFD8-697D-43DB-8FF4-C34403B21526}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{2AC814ED-3DCA-42F1-A428-B4AC73DCE74D}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F9D853EB-2154-418D-AC97-2CA9B3DD092C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}