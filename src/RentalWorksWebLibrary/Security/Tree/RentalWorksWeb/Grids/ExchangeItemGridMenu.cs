using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ExchangeItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeItemGridMenu() : base("{B58D8E40-D6C1-45D4-97B8-18A1270822B9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B166CDFB-13E3-4559-8B4F-2C56F061FDFB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CACD8B25-40C5-45DC-8F65-9B5BD0135589}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F00E1246-DD82-47A0-9F18-861140D3ECC4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AAFBB730-E5C0-4BE0-A1A1-734A48287827}", nodeBrowseOptions.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}