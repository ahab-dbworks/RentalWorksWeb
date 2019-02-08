using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    internal class CompanyTaxOptionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyTaxOptionGridMenu() : base("{B7E9F2F8-D28C-43C6-A91F-40B9B530C8A1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A55B4DCB-546D-43BB-889A-2DFF27F3D2FB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{0230998D-ECD6-4DCE-818D-27F8A97737B6}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9AF3F3BD-1FAC-4A30-82BB-4BC52A8F3D98}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B0841817-E08C-40C6-BFE4-F1EA529E18D3}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{E1854A32-B9D8-45E4-ACEF-2AEC3C236D27}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}