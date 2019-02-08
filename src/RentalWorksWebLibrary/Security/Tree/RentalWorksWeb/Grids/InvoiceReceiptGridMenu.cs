using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceReceiptGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceReceiptGrid() : base("{1C5F43F4-7428-4246-95B2-F45CE950CAFF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F83BDA34-06B3-41F4-9F2A-CA32CE390FEC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{0793E86B-5870-401D-A289-69E01DE725E0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9B258453-F40A-45F4-A093-EB62FA52FBB7}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A06D47F3-4A89-4ECC-9C24-136B3B379786}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
