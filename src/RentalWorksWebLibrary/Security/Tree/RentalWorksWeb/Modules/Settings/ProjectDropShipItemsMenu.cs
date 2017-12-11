using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProjectDropShipItemsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectDropShipItemsMenu() : base("{20CD34E6-7E35-4EAF-B4D3-587870412C85}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{835F6700-54EA-4A6B-810C-F97FBA19FCA5}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{44B56891-045C-4D24-88C1-B0DB21B19BB5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5C262926-854D-4628-9108-98991F03FB4E}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{55A400AC-C7D3-4B7B-9D8C-142FA7D618B0}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{83A63C3F-7857-4AF3-AB13-1F476057E4EB}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{117843DD-4E3E-42CF-9EE9-9AF323F5479D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F3856A73-A773-45C2-86E4-8D12D30C0048}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{261A5A96-1ADB-428B-9187-56515C7D2B08}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6FB8CC79-1C91-499F-B8A7-44C021E78060}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{8E378B90-DEDE-42F0-8CA1-04C1CAB5CE3C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6019D8C8-A81D-4A0E-A99F-9076F6E5D31C}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{BD70E7D4-58B1-402A-8D7C-3A0871590FAC}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{0C3B0522-BD8E-416B-9D81-B5DED1E4CFF6}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}