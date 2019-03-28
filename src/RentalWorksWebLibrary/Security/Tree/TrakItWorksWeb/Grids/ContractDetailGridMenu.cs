using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ContractDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractDetailGridMenu() : base("{A48C1102-249A-43B1-95E9-97A1DAEEE92C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9D9A327C-6EB5-4441-8F37-D60BCB7122EB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{9F7272B5-AB3E-446A-903B-C8E5318386B7}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{AF2E77F6-CC8C-4B95-9E52-ACB965EDE27F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{690B05DE-2235-46F0-814F-686EE5755AB7}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
