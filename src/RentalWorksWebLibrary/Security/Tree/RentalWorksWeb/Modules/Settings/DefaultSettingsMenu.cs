using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DefaultSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DefaultSettingsMenu() : base("{3F551271-C157-44F6-B06A-8F835F7A2084}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3120191B-6AEA-4F44-B999-3443FFAC6A1B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{BF359AAE-90EB-4843-9DE2-C7F66B3C0709}", nodeBrowse.Id);
            //var nodeBrowseSubMenu = tree.AddSubMenu("{C8C6DD27-79C0-4B8B-9464-E102ED86A388}", nodeBrowseMenuBar.Id);
            //    var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DE9D33EB-26ED-4B53-B8AF-0A5207F35632}", nodeBrowseSubMenu.Id);
            tree.AddViewMenuBarButton("{86829D82-33F4-4660-AF1C-AC9477EF0752}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3B639423-9161-4564-B54C-F7E7C91E895B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7ADED315-00B3-4883-98C6-56AFF75FF18D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{749E9CC6-1225-41C9-A504-78A2B1F58801}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{80829FC7-6F2B-4C45-9FB8-A78801F052CA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A0ABBF22-726F-4BFE-BA08-306B26012CD6}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
