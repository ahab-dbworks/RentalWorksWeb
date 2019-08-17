using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventorySettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventorySettingsMenu() : base("{5C7D5BFA-3EA3-42C5-B90A-27A9EA5EA9FC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9258982A-115D-4E62-AB8F-2B20EA02C85B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{A9B44F07-65BF-4FC2-98FC-C5684020C3E6}", nodeBrowse.Id);
            //var nodeBrowseSubMenu = tree.AddSubMenu("{C8C6DD27-79C0-4B8B-9464-E102ED86A388}", nodeBrowseMenuBar.Id);
            //    var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DE9D33EB-26ED-4B53-B8AF-0A5207F35632}", nodeBrowseSubMenu.Id);
            tree.AddViewMenuBarButton("{37F981B1-C4CB-49E8-899C-E6FF6CA47C1C}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{CA68D4B0-4EF1-42BC-BBA5-6E60BEF7E506}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{818AD631-2A43-4DF1-B124-2D406DBD4C79}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{0C207353-0C4E-48D1-BF03-6A1AA5B309E5}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{80829FC7-6F2B-4C45-9FB8-A78801F052CA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{664955C6-E948-4668-A9E3-F400BB1F8938}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
