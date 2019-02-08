using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInExceptionGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInExceptionGrid() : base("{E6A2B313-ADEC-41DD-824E-947097E63060}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6530DC2B-CE31-4249-8836-D1E607EAE5F3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8D61BB15-24D9-4382-A7A4-D7783C99146F}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8800E9E7-6008-4CA7-BBD1-897DB56EE8C9}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6F26C728-C9F3-4EF0-AD47-15F232966078}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
