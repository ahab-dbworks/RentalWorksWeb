using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PaymentTermsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PaymentTermsMenu() : base("{44FD799A-1572-4B34-9943-D94FFBEF89D4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{32BE5E69-8F93-4D6B-9DEC-8B7A3B9CFCD8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{DF0E5541-3EE0-450D-9876-77BD9AD442BA}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F001C519-4B2D-40D8-A322-E590D60BD687}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DD622346-B806-4521-93FE-5037304AF429}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{ACBE4693-D845-43D1-B813-A3B434BE382F}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{F14C32DB-AF31-487B-857F-9A72308840A3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{7215C270-D19B-4EDF-AE59-923CDC9BCC93}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EA6D746C-AFB3-49EE-992C-BFC17E1417EF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8E87B504-2C58-46FC-95CA-E3AD49228B5C}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F9DB745F-258C-4F6C-9EF4-1A9C9E837E4A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{CEB39E76-2552-41CB-BE6A-DD6078CD6825}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{E865C5D3-93AB-47F3-9ED9-FDCE4F152A51}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D5467FA8-655C-4946-AD01-A1FB68A40FA8}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}