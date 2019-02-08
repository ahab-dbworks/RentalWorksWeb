using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderTypeNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeNoteGridMenu() : base("{DD3B6D98-DBAC-467D-A3A8-244FCD4E750A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{AB700EC6-9BF3-4193-ACFF-04998D1B727E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{31E95607-5460-4653-BE70-69A3EC8BC87E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F29A610C-32C5-4383-A249-8862F81131F6}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{CCA3CEA0-8B23-4853-BD1A-823417C69994}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{F4DCA901-0E82-4FC4-A509-B113DB2EE8C7}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{1E10221A-500E-4D80-A129-CB589946B851}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8497BD23-6661-4B54-ABB1-A8FAB8651AEA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
