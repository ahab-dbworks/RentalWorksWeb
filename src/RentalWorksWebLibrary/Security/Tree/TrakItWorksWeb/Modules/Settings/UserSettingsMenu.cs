using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class UserSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UserSettingsMenu() : base("{2563927C-8D51-43C4-9243-6F69A52E2657}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{A64E24A2-F33E-4F53-ABBC-F40E3BCFCA1B}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{74BAECB1-7976-4CD9-B26C-AD1F041D644E}", nodeForm.Id);
            //        tree.AddSubMenu("{655F2E24-F746-48DA-90A9-2F611CC6DBB8}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{1E7743C7-364A-4A1F-AAF9-D0D67E5FDBD6}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}