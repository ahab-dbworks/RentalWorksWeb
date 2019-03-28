using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ContractExchangeItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractExchangeItemGridMenu() : base("{02007E1B-9ED2-43E3-BDAA-3A1EA4A5ABFD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8AA45E37-6D74-45AF-8DDB-429A1710EB6D}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8EBA7C0D-F79F-4664-A763-11B57A2A2B42}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D21820D0-0694-4F10-B637-4D46F0C31F77}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A9014B99-24FE-4312-B13C-D6550385C630}", nodeBrowseOptions.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}
