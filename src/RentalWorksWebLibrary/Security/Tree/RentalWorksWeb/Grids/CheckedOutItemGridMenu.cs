using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckedOutItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckedOutItemGrid() : base("{48CC9E19-7B73-4BA7-9531-20BEA3780193}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A8F5673A-6406-4603-9A6E-007B171BA608}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3C329501-1BCD-41A6-B9AE-A7D6FF1FF239}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{CC5ABCFB-EAA9-4BDA-BEB1-F121BC6C1FEE}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{33E98620-0495-4641-AB6F-273BD36E4914}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
