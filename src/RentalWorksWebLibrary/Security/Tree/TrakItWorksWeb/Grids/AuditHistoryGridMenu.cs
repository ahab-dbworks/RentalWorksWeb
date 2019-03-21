using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class AuditHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AuditHistoryGridMenu() : base("{977B65BB-DD67-4B5E-9B62-944E5DBECFD4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F430D30D-5DD1-4C7A-B17D-0DDF9590B912}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3907F674-E8B9-4CD0-87AA-06B1AC6DB002}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{ACED486B-B0C6-4FAD-A122-BC725DD6AE60}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AC847222-65E3-4148-9ECC-C7832AFBA99F}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}