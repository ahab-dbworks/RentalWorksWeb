using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class AlertMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AlertMenu() : base("{6E5F47FB-1F18-443E-B464-9D2351857361}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{32B9E014-3704-4357-B58E-98CFA7E13431}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{060D3ACD-F15B-471D-BCC9-430B1EE4E8FB}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{4951498F-B9CA-4020-9AC0-49D0C1E6C05A}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{17C8C483-FE15-44E9-84E3-376703F63247}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F67C1B88-F4A2-41DD-8FD7-F88A582DFFF8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{080155F3-F42E-43E9-B376-E74B3D947CC1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{82CC0BB8-F19E-48D2-A240-1F4E3A4C4AB7}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{548D72E1-5414-4580-BAEA-E19099029213}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7C6FAB6D-9193-4EED-8F37-D21CFF724568}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{50596055-B3CB-414F-A1F8-9ECF366622E5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D64DC610-7159-4D8C-B5CA-C675E8A0C7A4}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{28EFB222-7B8D-4227-92A3-8FD5DA4072AE}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}