using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class ContactTitleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactTitleMenu() : base("{11df6c43-809e-4eff-990b-e7b15fd352f8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3a2a29b4-a942-41c9-bc98-803fdd237bbe}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{bdd05e00-9616-49cd-9204-65267b02af28}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{33ceed12-4ee7-4d61-877e-d6be8fcdb284}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{340fe347-2a10-4884-b2d5-ca799e434b05}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7e6b596c-0b9d-4b2e-8c10-ee02fa796794}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{6b34c43b-304e-487b-80d6-2baa20525ea1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{111f454c-fa9b-4497-bcc2-c1e6e4349949}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{a7cee672-802d-432a-889d-75bf891cf80e}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{c769ae54-bc87-440a-9a73-1e52437e0430}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{053484a8-39cd-4a02-84e2-ff62f05ec945}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{376a0890-41c3-4872-a893-f6d0bdf2f58f}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{0f7363fc-96b9-49c5-864c-5235744d8695}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{262fdb92-6584-4b25-80b4-2783f2bb163a}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
