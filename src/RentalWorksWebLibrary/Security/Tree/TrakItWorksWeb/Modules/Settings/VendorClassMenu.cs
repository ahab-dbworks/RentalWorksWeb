using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class VendorClassMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorClassMenu() : base("{4934AA80-7C62-4566-AAE6-7AACD45307C6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{DE5B62E8-8F02-455F-B40F-2166D56D6505}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{CD168E57-A4E3-4D7D-A476-FA9AC6D93507}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0081D533-5E38-469B-B844-5C72EE419A07}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{22A57AD2-FB9F-4059-A014-C6C9208BFC5A}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1DE63E42-929F-4CE9-AB81-4DC4646666BC}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{F3FEF891-1FDB-4172-97F1-507A50D25D28}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{890AA4E2-B20E-4D16-985D-EE3AA4E2F5BF}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{620D3C08-C7A3-4346-B5F3-3F37008EA00D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E2BE6A05-3314-4CF7-867A-97FAC1377310}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{3790AD27-B851-439B-B7E8-32FBD3D0788B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4C6C664B-D8B1-48BF-BA84-B69F018ACB35}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{552FFB44-D63F-4801-ACC4-2B23E0A79D07}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{BB932DED-1761-4575-8EE2-9A40DBAB517C}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

