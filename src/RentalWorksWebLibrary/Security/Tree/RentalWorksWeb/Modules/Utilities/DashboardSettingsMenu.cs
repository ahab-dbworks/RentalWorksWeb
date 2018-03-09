using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DashboardSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DashboardSettingsMenu() : base("{1B40C62A-1FA0-402E-BE52-9CBFDB30AD3F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{B89B0690-1FE0-446F-B867-A0022822B724}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{36F95167-4B5E-4F33-9327-A6D9525E0992}", nodeForm.Id);
            //        tree.AddSubMenu("{CD62DC2D-1C6C-453C-9606-35D4F5F63F61}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{74B7475A-FC53-4FC9-8D15-E6C04321FB39}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}