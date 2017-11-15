using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CrewPositionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CrewPositionGridMenu() : base("{C87470C4-6D8A-4040-A7EF-E9B393B583CA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F9873F73-4466-4CDC-AE4F-0445BD2ECBF9}", MODULEID);
            tree.AddEditMenuBarButton("{F17D11EF-1A10-46B1-B682-5B3919A57318}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}