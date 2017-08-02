using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class DealStatusMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealStatusMenu() : base("{543F8F83-20AB-4001-8283-1E73A9D795DF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3131205D-CC31-4D99-B21E-7670CACC3D03}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7E780506-E6A0-4394-B90E-5F8F85E629ED}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{7FBB0CBB-FD4C-4CCA-A05D-26F23BC0C3DB}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{14F6A109-7430-40B2-9A7B-42D1947778C0}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{88A700DD-9820-468C-9463-10B51F65F333}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2A66B290-ED95-4417-81C4-FE972A2CEC69}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4B6892AA-1DC0-4D3A-8900-1810D4E33687}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EAB7559B-7C7C-4BD0-A5D4-C4442C2E2A2D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{44D56B1A-476E-46F7-AFFB-63B6A433D28B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F84E9296-FBB0-4046-AD45-43BB5B7AEC9A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7999A2CD-5A62-4077-987B-A71B43F208B0}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{FA4D39E7-B636-435C-9C82-E40AD135DBBB}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{2525E73D-AB72-4DDC-A807-1FAE4E5EE2AF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}