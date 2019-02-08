using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorInvoicePaymentGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorInvoicePaymentGrid() : base("{8B63442E-DE46-47BD-B995-342B2A49E77E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{652B1D29-014B-4F88-B24F-00B133A98A63}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8B737DC8-472D-47F1-903A-CD3E6C7947D0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6BED85AB-AEAA-458E-B41C-92D5604AB7FC}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B56293A3-B17E-4607-B8AC-37CEB27E38F7}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
