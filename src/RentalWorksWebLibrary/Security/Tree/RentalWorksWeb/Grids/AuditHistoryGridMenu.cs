using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class AuditHistoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AuditHistoryGridMenu() : base("{FA958D9E-7863-4B03-94FE-A2D2B9599FAB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B3456F29-6CE4-4A62-9462-034A9D8835F7}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{19C1F41C-A828-4A95-9EDE-44C512018EB4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{075E301F-DBAB-4C52-B5D0-AE90765584FF}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6B7CEB20-7462-4A57-B368-E66745730B8E}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}