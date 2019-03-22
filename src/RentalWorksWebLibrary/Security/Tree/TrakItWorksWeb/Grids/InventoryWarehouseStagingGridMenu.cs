using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryWarehouseStagingGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryWarehouseStagingGridMenu() : base("{5D55C05C-3094-4656-B199-3DA137E5D311}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{014EE680-D40B-4ACC-ADC3-11CE541BF6C0}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{06C97987-9126-4AE7-AAD2-B7A8C57836D3}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2FAFCF52-8DCC-4FB0-A48F-AC6FD7881F54}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A6572A2A-D924-4BD9-8F6D-0908A7FE448E}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{CE18B266-736E-4EFC-94D9-D176E4168AD9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

