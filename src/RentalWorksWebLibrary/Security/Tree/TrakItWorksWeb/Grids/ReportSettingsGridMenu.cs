using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class ReportSettingsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReportSettingsGridMenu() : base("{748FFFB6-175B-4425-A63C-FFFAA6F425E7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{487ABEB7-2D29-4B6F-81F8-2497067EF509}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{A2A0E4A6-92D2-456F-84B4-9724F020C9A2}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F505E2BB-2035-4B64-9AC0-5386C3513B2C}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{41CBD8C9-E981-4FDC-AD34-1252AF886BFA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

