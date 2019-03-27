using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class CheckInOrderGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInOrderGrid() : base("{C1752A81-400D-46F7-82BB-0B1CCD78C890}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{56EC3E7E-2825-491D-ADD9-F674F64D94B8}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{65EC7B6D-D5B7-4F62-928A-CD60E1FDC1A7}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F9F0A05A-EB04-4F36-A749-7C86EDE0DCC2}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9AE3FD12-469C-4578-9718-67FCF6C4C6CE}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

