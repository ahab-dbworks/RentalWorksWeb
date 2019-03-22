using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryAttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAttributeValueGridMenu() : base("{ECEB623B-C84C-4D55-AE86-8E067E119244}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1693E2EE-81C7-4434-BB13-2D8152DD5A3A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{93230663-99B5-4EB0-8B4C-6B7F4A886185}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{3E0EB6FE-B206-4520-B632-251386C753B4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9DDD7FF8-60C0-46EA-9DD3-1F26A44CEDA0}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{47D192DD-B396-4C45-A96D-47438C7C11C3}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{3B1487EE-A534-4D71-A5F8-02750A15A5AE}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AE87928A-C888-4FE3-8167-ED72578B8120}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
