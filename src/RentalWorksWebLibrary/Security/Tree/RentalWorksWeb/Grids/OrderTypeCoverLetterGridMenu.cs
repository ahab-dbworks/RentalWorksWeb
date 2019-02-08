using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderTypeCoverLetterGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeCoverLetterGridMenu() : base("{7521D3CC-FF1C-44F5-8F93-9272B6CADC64}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A4C750FF-0761-4ACC-BC23-0C732FAD4BE8}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{998CD3BC-0963-48DD-B068-32C02E5DCBA9}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D12D2591-5E1C-4B96-914C-29E85C40C941}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AAC5EA7F-0FB3-4BE3-B295-39117E815FA3}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{41BCFA02-C8AC-43EA-94D5-94300A0DD007}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}