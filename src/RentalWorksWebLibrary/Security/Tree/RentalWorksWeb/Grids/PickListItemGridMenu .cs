using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PickListItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PickListItemGridMenu() : base("{F8514841-7652-469B-AF43-3520A34EA5F0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{7FF4354A-43C2-46FE-8C5A-8665DEA71DC4}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{A83FC917-BF1C-40EB-9482-E8AB9EAA8651}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{35F24FEF-C65E-4D33-B6AE-1998397895A8}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{39E2AF61-FC8F-4D20-A5E4-7C061CF9FF78}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{EA23169A-42B2-4689-935A-D483E8A6892E}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1F48A3C2-61AE-4BB4-B16C-1AB697CA3230}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}