using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DealTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealTypeMenu() : base("{A021AE67-0F33-4C97-9149-4CD5560EE10A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{5B35ABE5-D7A0-425B-BCAE-20FCDFCFF809}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{11E40DA7-0468-444C-9050-5440C03E8D03}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{814E0A04-F7D5-4724-8820-3DA46DA055D8}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{6F67E869-74F8-4037-9A3A-1A1FBFD5F061}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{84EBAE0B-D394-41FB-82D0-ACBE7A60300D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B84E3346-CEFF-4A05-8533-6AEA5A6102BA}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{5D101321-F2D1-4910-814E-9A9989C20D3A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B28EFF65-3F0D-4C0A-8900-DBB0CB5328E6}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8B81F164-6326-40D8-A8B6-64AA3D02B84D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{3303E19D-90E2-437C-8016-9C8462079F6A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{3A82A237-3532-46A3-8B67-172F27A0C2C4}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{DF06311F-3784-49AB-8245-20E1C201F892}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9325A96C-FFF1-4361-BC17-922BFFFBE540}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

