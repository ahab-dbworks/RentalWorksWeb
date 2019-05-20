using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ControlMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ControlMenu() : base("{B3ADDF49-64EB-4740-AB41-4327E6E56242}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{83B92952-DA34-4EE9-9C62-DCA26CC4BF8E}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{D02BE59C-5CFD-43F0-BA18-6147C405172A}", nodeBrowse.Id);
                    //var nodeBrowseSubMenu = tree.AddSubMenu("{C8C6DD27-79C0-4B8B-9464-E102ED86A388}", nodeBrowseMenuBar.Id);
                    //    var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DE9D33EB-26ED-4B53-B8AF-0A5207F35632}", nodeBrowseSubMenu.Id);
                    tree.AddViewMenuBarButton("{E9608D08-B235-454D-AE3E-75462BEE9638}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{8DCE4D50-BBCE-4147-A05C-A0A00755EA11}", nodeBrowseMenuBar.Id);
            
            // Form
            var nodeForm = tree.AddForm("{58FEB457-0852-4A78-9322-E438A5E736E8}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{6E613966-4DDE-4A44-AFFE-7DC527685329}", nodeForm.Id);
                    //var nodeFormSubMenu = tree.AddSubMenu("{80829FC7-6F2B-4C45-9FB8-A78801F052CA}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{AC6BC37C-4D56-451B-9E49-13AB8B6897DD}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
