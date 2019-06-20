using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class PurchaseVendorInvoiceItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public PurchaseVendorInvoiceItemGrid() : base("{0658B954-7791-4C85-8782-2AA0C5BF6CEC}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{52B01731-5C75-4021-B3F1-6688B0CC2000}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{36DACEEF-D85C-4ABE-AFC7-29F99A81363E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{94EE9E08-302E-48F0-B77A-CCDE68D78906}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{500A8256-DB97-43BE-9E5F-E4732F4F6C16}", nodeBrowseOptions.Id);
            //tree.AddEditMenuBarButton("{3DD93CEC-0AE4-4962-8CF0-949123603E9D}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}