using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class BuildingMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BuildingMenu() : base("{2D344845-7E77-40C9-BB9D-04A930D352EB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A0FB9549-8C85-4644-8441-34890961BC9F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2DCCA744-1B7C-4F1A-B29C-94B63E288E80}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{821CB6F2-4897-4DEC-B83D-D9730CB37AD3}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B27CD024-1D6F-4BF5-84AE-21123B48C8D8}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5CFA0D72-15E0-4C6C-A64C-859C37C1E1F2}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{01AD7919-1A74-4383-92E5-95F8941A7B5B}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C49D3ADB-5C16-469C-9E54-22F9C5304B03}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5867FB59-FB2E-41A2-B9EC-BAD8FFADFB71}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AC0A6BE2-7D87-4C87-A887-F50AC6ECC043}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{6A9EED8D-FBB3-4BE5-AA8D-32F28D0D2108}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{68A5FEAC-F62D-4465-8209-6DDAA55A3592}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{DE3D98C3-8172-4E78-8DA3-D303DE73BE4C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4E711942-8E54-4AC4-A7FE-A495B1CF07F9}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}
