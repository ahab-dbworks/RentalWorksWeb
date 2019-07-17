using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ContractDetailGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContractDetailGridMenu() : base("{30A4330D-516A-4B84-90FE-C8DDCC54DF02}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{4671F311-7F57-44DA-AC55-050052B888D5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{95DC0A90-F5A5-4718-8A41-DF2B57D28C66}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{5C3E71D4-AEF9-4F39-A486-C5603984A55A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{031E287D-26D1-4DBB-9500-08665BB9EE1C}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Void Items", "{DD6F2FD1-B70F-4525-BCAA-322EF3DBC9C1}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}