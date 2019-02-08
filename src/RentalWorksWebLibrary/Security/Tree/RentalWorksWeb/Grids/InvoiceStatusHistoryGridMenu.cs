using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceStatusHistoryGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceStatusHistoryGrid() : base("{08E2713B-9B57-4B1F-8859-E7B10E116EAA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A8CC4584-74E2-4231-ADEC-11688A16DA8B}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{E88541E4-E3CA-483B-9532-C51693F42520}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B2C0D19C-8FBB-418B-B77D-072C9E637485}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{457B09FB-563B-419A-B316-F81285990951}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
