using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceOrderGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceOrderGrid() : base("{D4B2DBB4-FDB8-461E-A3BE-EE81F43A61C6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6748F828-5120-4E52-86AD-9499EF58351D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{FE1DBB1C-CFD4-4163-B289-7D9B69349C7A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8EBBC3DE-60F9-4BD6-9CA5-E661DA0A3C61}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F57F6338-908C-4D44-A067-D0584CCD520C}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
