using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class UnretiredReasonMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public UnretiredReasonMenu() : base("{C8E7F77B-52BC-435C-9971-331CF99284A0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D4981C18-A6E7-4EB5-9324-F176B2DC9B3B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0D8DFC1A-28A3-41C6-8FB5-9B1E01D1ED46}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{4D6C24A4-3053-4F36-A8A7-D8BD1A69AA84}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B7F461CF-3AD7-4604-AEAF-ADDBC71B3C89}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3534F55D-2C1D-4865-BAC5-C3B8C533680B}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{59D06250-457C-47FC-89A5-94CBCD7C7399}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{762750D3-2C95-4152-87F2-E0C05E4A9AA2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E111F70B-5866-4184-9925-3A0308EB287D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{985EF1F6-D262-45BB-A607-61902605BD80}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{9634D381-884F-49FE-9508-76395B459B43}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A45F7395-402A-43E3-A41A-CA8EEF7A2867}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C4689F13-D7C8-419B-9273-8AD95A73FD21}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{732C0A56-89C7-416E-A0A8-0911AEF8ABD0}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}