using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class GlDistributionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GlDistributionMenu() : base("{7C249F59-B5E3-4DAE-933D-38D30858CF7C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{ED61648F-73E9-4758-A783-4C90621F15DE}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{04283F29-8148-44C0-B1B4-CC64643CC967}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D59F3FC2-D55D-4A12-A79C-54328B2F85A3}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0FC6417E-4B96-46F5-BD32-688E8115D58F}", nodeBrowseSubMenu.Id);
            tree.AddViewMenuBarButton("{22746185-5E0F-4D97-8D34-6977D432A0F9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{201FE7AA-AFC0-493A-8034-F4146E7DB57A}", nodeBrowseMenuBar.Id);
            
            // Form
            var nodeForm = tree.AddForm("{E403AD70-B6D7-4B39-A09D-589EB5857609}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2ED357B0-30B1-43BE-BCF7-D30AB4AFEFD4}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5E90F93C-6112-4553-A9BA-94A44839AE76}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A0732DA3-E810-4E95-8338-EFB03F71240A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}