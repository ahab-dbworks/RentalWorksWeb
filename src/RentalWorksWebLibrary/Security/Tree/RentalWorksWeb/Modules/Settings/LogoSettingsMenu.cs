using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class LogoSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LogoSettingsMenu() : base("{B3ADDF49-64EB-4740-AB41-4327E6E56242}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B57CC410-3E18-4697-9BF4-3FF62581B8A5}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{A44DF2D0-59F4-4EFA-B58C-D9165CA875D3}", nodeBrowse.Id);
            //var nodeBrowseSubMenu = tree.AddSubMenu("{C8C6DD27-79C0-4B8B-9464-E102ED86A388}", nodeBrowseMenuBar.Id);
            //var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DE9D33EB-26ED-4B53-B8AF-0A5207F35632}", nodeBrowseSubMenu.Id);
            tree.AddViewMenuBarButton("{EAE39CDF-E080-4DE5-907C-0E28C0A352D7}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{57AB81E5-4754-45D3-A50D-A53ED5B1B577}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{EA11CEEF-1E92-42AC-A0D4-2C3DB0820FB1}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{26E69188-CE86-47DE-9B50-6891D7C48098}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{80829FC7-6F2B-4C45-9FB8-A78801F052CA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{E54385A0-B486-4109-9BA8-C91BF9E32343}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
