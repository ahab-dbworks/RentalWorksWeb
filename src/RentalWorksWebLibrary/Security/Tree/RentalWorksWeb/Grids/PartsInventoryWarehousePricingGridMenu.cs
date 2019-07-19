using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PartsInventoryWarehousePricingGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventoryWarehousePricingGridMenu() : base("{57C77B5A-D699-45A5-9B6F-FEE96DF4E141}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{091F5898-9BA6-4874-A67E-62D7486727D5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{2256DB98-9CB4-4AA4-B515-6D4F150B35D2}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{FE457ED9-B5B6-46E6-A0E0-D78EDA718B9F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2538938B-F24B-4866-B1B3-2B121DDF115B}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{D1B82F02-8960-42F9-AB82-1FA74E682324}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}