using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class ContractMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractMenu() : base("{F6D42CC1-FAC6-49A9-9BF2-F370FE408F7B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{EB79516A-083A-4402-8259-C8B2429C2362}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2833AF05-8058-41DC-AC64-95851F67874B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{AF695A32-09D7-461F-B231-25CCFB678250}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3A74FCBD-0684-4097-B2D2-BE3197F2C41E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2B766422-0F3E-4F70-88E4-812CD34EFEAD}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{A33E32C1-1E85-4459-A7E9-20F70DB27B0F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B8ACA907-B6CE-4D2F-9E66-ABA403086640}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{1B74BEFE-B83C-4BA6-99A5-4A9223FEC8C0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{530BF311-88FB-4C43-9771-513AB14076D4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8648359D-CEB7-45B7-B1EB-C8E74F55D615}", nodeForm.Id);
            //tree.AddSaveMenuBarButton("{7934BF58-9E3F-4FAA-B1C3-A8470AC4ACDE}", nodeFormMenuBar.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{2D94F00A-E1A5-4020-AC7B-0BEBADBCCCEE}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{C9D0D465-0EF3-408E-8577-C1D7736721A0}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Print Order", "{8E3727B5-8E75-4648-AE52-FB3BD7729F02}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}