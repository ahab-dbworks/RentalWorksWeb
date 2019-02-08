using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CrewPositionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CrewPositionGridMenu() : base("{C87470C4-6D8A-4040-A7EF-E9B393B583CA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F9873F73-4466-4CDC-AE4F-0445BD2ECBF9}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6E9934F2-5475-4E24-920D-2AB25832C5D0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{AC8B087D-B7AF-4B93-9A6E-5D90E3221FE1}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{0B10C3F6-B579-4F9F-A746-D1FC4923B7CD}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{F17D11EF-1A10-46B1-B682-5B3919A57318}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{F04F8656-2B68-4E6E-92AE-72EC8B7D0E37}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9483C712-5DF2-4555-AC2D-FBD235146CCB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}