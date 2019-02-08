using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInOrderGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInOrderGrid() : base("{F314F7FA-8740-4851-8CB5-DA15EC02A5E7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A9341475-F894-41A2-9C09-B9325934F357}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{DE98B8A1-45C0-48FE-8054-98F733FF2BC8}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{360D997C-E729-424D-9E8C-770629EA6632}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F84B1C5C-0591-4D7E-9A6E-62D6524BE514}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
