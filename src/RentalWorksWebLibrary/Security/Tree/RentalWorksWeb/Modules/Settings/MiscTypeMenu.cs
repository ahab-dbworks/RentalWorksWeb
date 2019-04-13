using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class MiscTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MiscTypeMenu() : base("{EAFEE5C7-84BB-419E-905A-3AE86E18DFAB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C6E59920-5249-4244-B2FC-26A73CDBB364}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D161001B-6C64-4AF3-9410-5697F457EAED}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{176B212B-07C3-470A-A56F-B75A79FF74E8}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{CDB83F3A-8166-494B-A50B-94CD832305C4}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B7EE4661-34DC-4DA4-9681-CD46EF13AE8E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{65C6906F-C2B6-439F-B66D-DA4873A67E4E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2EB53ACF-F726-4ED4-B620-1C96BD0AA3C1}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{52B836C5-C55A-44D3-A157-05F9B8EFC352}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{451AC3F8-B9CF-4C78-99FC-3AEEE12A5562}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{57CD4665-ADB4-42C7-843D-4AAC40CE797D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{92C7A2B7-F339-4FC4-969B-A0A9C467E28E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{3B8523A6-5C50-4D29-AFBA-95532198CDB9}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{834C33B5-7E2D-40DA-9CF4-8E66FD762779}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}