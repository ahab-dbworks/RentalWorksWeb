using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules
{
    public class DealMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealMenu() : base("{393DE600-2911-4753-85FD-ABBC4F0B1407}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{09396E72-1E63-41F7-837D-493F5D6F4C78}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{109D2979-F2DA-4483-84C2-F7BC42696495}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B9F01F79-2FF0-4F3D-80FE-D65757666523}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DAF816F2-854F-428D-BE8C-8A22CB992CA1}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A35BF8FD-A021-423E-AFDA-F9E4150D417B}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B430DD46-94AF-41F3-9CD7-1D3DA32412F3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{39BE07F8-16E2-4D04-BA8B-35A3A27CAB83}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A578B804-B7BF-4FDC-B54A-3F12886705C8}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A832C139-C472-42C4-B49D-39694B6C4A2D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{CA2E4FB0-149F-4B9A-90B8-F48ACA00BEAA}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4561EB93-BB82-4DBB-BFF7-E2E4F3EB82B2}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{DB28C5AB-270D-40E7-A8F4-F4873880D512}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{2118B828-3A77-404F-93D9-D397BCFEFD00}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
