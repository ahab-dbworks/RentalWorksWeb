using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class UserSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UserSettingsMenu() : base("{A6704904-01E1-4C6B-B75A-C1D3FCB50C01}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{394F33E5-5D3A-48C6-A60E-14D0841A3C1A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A6C6BE64-A923-419B-949E-33E5ABF27A97}", nodeForm.Id);
            //        tree.AddSubMenu("{CD62DC2D-1C6C-453C-9606-35D4F5F63F61}",           nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{625FA36A-4E52-45B7-84D4-E38071899827}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}