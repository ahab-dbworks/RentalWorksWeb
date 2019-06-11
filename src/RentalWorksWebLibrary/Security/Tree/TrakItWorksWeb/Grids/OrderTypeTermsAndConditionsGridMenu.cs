using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderTypeTermsAndConditionsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeTermsAndConditionsGridMenu() : base("{91786335-2906-4309-8CE5-6701C226258D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {

            var nodeGridMenuBar = tree.AddMenuBar("{C376E2EF-7A12-4F18-8A9D-6B812A518970}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{30E1902E-E38E-4E77-8FA8-A9AC0637B630}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{37FFABF9-D056-4262-ADB4-AF0ABD0F603F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{31DD7ADB-B224-47BD-BA1A-CF92A8B37E45}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{4793075C-583D-4CB0-9FCB-A5EFCCADC35E}", nodeGridMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}