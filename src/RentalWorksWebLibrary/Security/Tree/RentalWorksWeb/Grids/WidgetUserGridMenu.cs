using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class WidgetUserGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WidgetUserGridMenu() : base("{C1341ABD-B5C0-4D8D-8848-D0E58A2EC972}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B06F6C58-6BA7-4558-9223-058585A484A1}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{22983519-9D74-49DB-B687-2AECB4A29C23}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{36470488-C750-45B4-8D89-4C4210D3FFCD}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{79342898-030B-45D3-A9D5-AE5C8BE187D8}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{F5E9E476-4BC6-4DCF-90AA-2B43B458C477}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{26925A8D-D310-418F-BF8E-E5ADB1E2F1F6}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D110D32A-7AC0-49F0-B0B6-3F5B8543C2D7}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}