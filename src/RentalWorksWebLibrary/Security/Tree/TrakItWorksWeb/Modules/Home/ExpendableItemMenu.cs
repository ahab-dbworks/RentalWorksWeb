using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class ExpendableItemMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExpendableItemMenu() : base("{4115FFCE-69BB-4D2F-BCDC-3924AE045AA8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{011A65AE-94D2-4B05-8AC9-E5CBC0E4CFA3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{40717664-3BBC-45E4-8C2A-16B18F786B3D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{9151D04B-5BBB-4FEF-B509-927AE6B3C215}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5F112C56-9858-46BB-B971-9780490A5FDF}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6A3C430C-1E28-46AC-9568-DB9820315BF4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{FD817B63-C307-4453-B5F5-C474AFC14AC6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{22EEBDD5-06AA-4620-BEB0-6CE2DD3515DD}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{AEFE0710-144B-4CC9-8411-56A1D9F1EEE1}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0E2EB4A9-EEA4-4997-AA7B-7AFB60DA742D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{36B3A76A-1206-4396-A827-3713DD17B22D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8B980443-C44D-450F-8A1A-B65FEFCB7A58}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{56B7566E-EE4F-4B52-87BC-DB7B8B05373A}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{00203D83-12B1-4D6D-8679-DD98FF568493}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}