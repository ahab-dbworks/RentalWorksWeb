using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PartsCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsCategoryMenu() : base("{4750DFBD-6C60-41EF-83FE-49C8340D6062}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{413A7675-C10B-494D-B963-2A5A1A75DA32}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{25624874-7B52-4624-9148-07D3356984F2}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D2704CD0-519A-4CE8-BF72-325319F7CD19}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{62E2FB2F-8C04-411B-8D89-EA07E701B0ED}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C2E5D9B4-5EBC-47DA-B738-C3E7C34CCE36}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5B5404E0-C64E-451D-8E9A-7574188B368A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{8D8AB37C-1EF0-4807-A806-CC6FEE08D6E9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{60F37B7B-1C8C-4A76-B043-F9E322A294AB}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F9BBE470-947A-4010-9520-FBB21FC39E8A}", nodeBrowseMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}