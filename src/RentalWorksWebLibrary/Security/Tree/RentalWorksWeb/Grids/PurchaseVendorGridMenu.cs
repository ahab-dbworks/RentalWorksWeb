using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PurchaseVendorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PurchaseVendorGridMenu() : base("{41264374-CF9C-468F-90C9-A7E1DCD39068}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C7AE4A42-82F8-48B2-80C6-91CE38976392}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{D5938A3E-7D24-482D-A30C-E64920F28987}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4D35DFF4-F81B-4195-9631-EDDCBA6329D6}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1147FED0-B118-4662-BDE2-178D1399798E}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}