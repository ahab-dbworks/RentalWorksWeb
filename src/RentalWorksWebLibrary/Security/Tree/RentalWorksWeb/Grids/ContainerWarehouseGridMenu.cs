using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ContainerWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContainerWarehouseGridMenu() : base("{97F0F1B5-5C90-4861-A840-54FE35F58835}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{ABA1BA8A-3773-4068-8E7D-5DE929C8760F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{29A324E4-E9FC-4BCA-9BDD-A1F7BB99D42C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{84701674-36D3-427B-9D5C-33F02481B214}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{79953A08-3990-434F-BCCC-1DD70382B467}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{BB75D05D-E020-4F02-8FAE-E088F52BE2A9}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}