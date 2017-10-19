using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class POTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POTypeMenu() : base("{BB8D68B3-012A-4B05-BE7F-844EB5C96896}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{527D6373-C2D7-45BF-9E54-B7D7D2EBA994}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{8188F4E9-1D22-4362-A9BA-56C50132F3EB}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{2AE13BB2-7750-4D4A-8542-719A36F35A4F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{1DBD93D1-C8EA-4A14-9BB5-B1FEF438CFAA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{14C61981-09E1-4FE0-9514-8CD389BB005C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{D007ED53-580D-43BE-AC6D-8D9A98054FDE}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{428ED7D2-7814-4989-BAD1-B2EF6E56AA62}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4B509C6F-C8B9-4E6B-83C0-6EF55B5E8DBC}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AD50DF9A-FD7A-48FC-A048-B8964D7D96ED}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{6CA60DB8-F90C-44FA-BA7E-E94F848960DF}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E690A2F9-563A-4490-B8DC-399B23E16A44}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6C4EC2EB-4522-466E-B487-3EA26CD73DB8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{C03372C0-A977-4B10-9BA4-222E1620505A}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}