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
            tree.AddNewMenuBarButton("{F04F8656-2B68-4E6E-92AE-72EC8B7D0E37}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9483C712-5DF2-4555-AC2D-FBD235146CCB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}