using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StageQuantityItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StageQuantityItemGridMenu() : base("{3CCB3EB0-983F-4974-9F7F-8B12A8C7DDE9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F367CE76-4D4A-4523-91A8-52731138DABC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{DF13E78B-BD55-494A-87B2-83E5029E7D8A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6330748F-D585-4E62-91AB-D27C472056B4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C7E33B5B-EAB7-4885-82C7-83A6F8CFDAF1}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Unstage Selected Items", "{FECB5FC0-4E01-4F99-8D29-2F9CE446846B}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}