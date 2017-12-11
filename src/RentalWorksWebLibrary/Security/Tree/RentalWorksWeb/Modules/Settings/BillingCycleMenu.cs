using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class BillingCycleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingCycleMenu() : base("{5736D549-CEA7-4FCF-86DA-0BCD4C87FA04}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9CCB0431-9E7A-4346-9A66-698FB64E91CF}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6DBD0824-2D6A-42E3-9E41-EEED9B24311E}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{60B8512E-F6B3-4F71-8CB5-2B8EC8DFAF94}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{76022E24-BE70-45FE-A1FA-E498DF42B01E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2DE91518-4A53-4AC0-B56A-840C1706C9A7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{50FFC970-FA69-4AAC-A81B-45B5191A262E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{31210478-87A5-402E-93E2-38D0D01F866D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{0A423733-3D2F-49FF-A327-BB0390FD9788}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D00C136D-3DF7-4424-AFD3-A71529010830}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{5EF9226E-6F6E-4B01-AE77-FB8E3BA85CF6}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{3FBB7953-1033-4B1F-8180-46F7D5559087}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6C6AF59C-9550-4CF6-A3E0-ABC3197D3E1A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}