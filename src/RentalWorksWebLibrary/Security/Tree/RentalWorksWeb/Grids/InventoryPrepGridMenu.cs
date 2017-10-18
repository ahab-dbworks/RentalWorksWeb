using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryPrepGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryPrepGridMenu() : base("{338934FD-CA10-48F4-9498-2D5250F4E6FA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{852B6C17-8097-4B05-90E2-5FACE17346E0}", MODULEID);
            tree.AddEditMenuBarButton("{C7E8FD35-CE85-435A-9803-9DCF35704D86}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}