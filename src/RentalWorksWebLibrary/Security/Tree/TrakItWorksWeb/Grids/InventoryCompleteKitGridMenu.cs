using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryCompleteKitGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompleteKitGridMenu() : base("{9F38BB28-4133-4291-91B8-3F234B5DB437}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5FD4A564-FC26-4644-BC94-83C3BE4AA245}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3E592362-C7A9-4965-85FF-ED68AB1ACAF5}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{85514F5E-B4B1-4BA7-BF9B-485D60377550}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C6302760-D623-4584-A0ED-4854C159CA56}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

