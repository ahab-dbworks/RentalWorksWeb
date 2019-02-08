using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomFormGroupGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomFormGroupGridMenu() : base("{2D12FA3B-2BC3-4838-9B79-05303F7D3120}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{36F5D6A6-0AD5-4825-8598-A3CB4CAB5206}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{F0F2CE44-1989-4094-882B-E1DCF3D96EA4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{003DC977-6755-47D5-AB1B-E6A8DDD3A037}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{53D1A09B-570F-48B5-BFBC-F153682BD0A0}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{4F999468-11FF-4F6A-8B29-EA1DED43EA98}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{14E250F4-5282-4AC0-A25B-D056F44525E0}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5B2ABB95-D40A-4BBE-84FC-091F3E693A14}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}