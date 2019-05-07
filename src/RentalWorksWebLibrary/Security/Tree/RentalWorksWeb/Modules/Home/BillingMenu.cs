using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class BillingMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingMenu() : base("{34E0472E-9057-4C66-8CC2-1938B3222569}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C355874E-FD6D-45A9-AE2D-45623F5A705E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F94FDA27-C427-468E-BE01-A395A8032F10}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{FD2FF91F-EE58-4AF6-BC67-D23FEE1E0D87}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{CE60F0E5-2995-43F1-A72D-E798B08D3B81}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B70C682C-E983-4840-A418-8C449C048158}", nodeBrowseExport.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}