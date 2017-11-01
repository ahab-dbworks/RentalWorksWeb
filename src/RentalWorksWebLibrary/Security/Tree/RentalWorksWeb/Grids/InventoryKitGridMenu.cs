using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryKitGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryKitGridMenu() : base("{989C0F67-5F4D-4BC2-832F-D8009256AF0F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5AA8C8F7-79E0-4E5C-B419-8B35AF5A1AA3}", MODULEID);
            tree.AddEditMenuBarButton("{D47D414B-4D5F-4D43-A0BC-D3811F1E7988}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{FF784C95-8B5F-448C-806A-ED7270FE1E34}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{28E1988B-455D-4A99-852B-47D02402C5CA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}