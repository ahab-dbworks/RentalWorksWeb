using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class RentalInventoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalInventoryMenu() : base("{FCDB4C86-20E7-489B-A8B7-D22EE6F85C06}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{AF5AC3F2-B932-4D8B-B857-8F196F0F305D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2E83CB22-7B33-437D-A12C-A5B8ACD2E74B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6E7EED4E-D62C-48B5-B2E7-DEC2EECD279F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{93B15428-B70E-4941-BEFD-CD66F5C6DE26}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B1F30DB6-97D4-4E3C-96BA-AB888C5B5F80}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3BCA1586-8E9B-4BA3-A9C3-D244604F411A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2D8EE443-8E79-4904-9530-F93EB6DBA586}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2A873D54-A9E4-4625-AAC2-E8833006C508}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{68581297-218D-4C25-9D80-DFD190B8EA00}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{A51055D5-0BDB-47C2-99E6-4914CB385A3E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AAA54228-6C4C-44B6-9E80-23DD40FE2F51}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{358C23FD-1FCC-420E-9B6B-FF4ECA3037A3}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{A0F4C68E-FE01-4220-918A-781B2C049C6F}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Create Complete", "{B3371C86-740C-44C4-A8FA-E8DE750800F3}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{B6D976BB-987E-4FD1-A655-6DA2656F8989}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}