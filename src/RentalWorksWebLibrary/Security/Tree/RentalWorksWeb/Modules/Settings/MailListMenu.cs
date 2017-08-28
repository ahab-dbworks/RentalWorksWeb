using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class MailListMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MailListMenu() : base("{255ceb68-fb87-4248-ab99-37c18a192300}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{c4929f81-3ca0-4338-b3ec-20c368e88eff}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{22ddcd18-af0d-42dc-9ee0-61bba91c8350}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{99dab862-ca3e-43f3-b346-f8b7fb26b3a7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{14e129bc-a3f6-4f49-9353-3e3d46befed2}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{fbddb7ca-4bd2-4ff8-b51b-33472cc71b44}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0e17b144-b762-4609-a643-2f7e877914b3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{f28dde67-83be-419c-aaf7-f6c8b64e5434}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{219c504a-3476-416a-b7c1-e6e7b2cc466a}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{531a1e25-f5f2-4027-9ebe-4b560dedf634}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{f9b0b925-65de-47df-aa6b-8218e5b5b6aa}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8665448f-6b0d-436d-b76e-26af51cdf3ac}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{69979df4-4674-4422-a5af-8d1dfbc92aa8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{e2b878ec-c47d-4992-abb1-1e70aa8abcce}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}