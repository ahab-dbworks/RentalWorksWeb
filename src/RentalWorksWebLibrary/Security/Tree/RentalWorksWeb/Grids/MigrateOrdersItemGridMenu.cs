using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class MigrateOrdersItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MigrateOrdersItemGridMenu() : base("{B6709AA1-64C9-429C-BD6E-B244F03ECCC6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F786595B-5E5D-4666-B726-4E463C8BD9F5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B21924A4-FEBD-4D67-A212-7BC6862F9874}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8793A019-08EF-461D-B291-72A9AA1AC236}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{192A1E79-813F-4D67-BB3C-EDC1A924F7D5}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}