using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryConditionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryConditionMenu() : base("{BF711CAC-1E69-4C92-B509-4CBFA29FAED3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C947A3EF-9D36-40D5-AF40-3C4A3B1B5F0D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{CC0F1981-6516-4DA5-ACC6-08D9F3ADEE17}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{1689F05B-E866-44BF-92EE-43B275AB997F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F305C4A2-AF4A-4A1F-8961-4C8AEBC41616}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8BCB8E38-5005-4633-9F07-996A064BAA6E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1EE12434-FDE1-41A8-B9C2-B0ADD3DB9FD6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D035D36E-0A94-445B-A3DC-F6D98257D892}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{34B624F9-90BB-4229-BBB9-49633B70DC20}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CCC8FDBF-5798-4E22-81FC-FA33171E55FB}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{B9455179-0EDA-4F7D-B226-9A11BF43E839}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AF707C0C-45A1-4B41-BBE2-C06E7E411D09}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{8CDB25D7-5D23-42BB-8BD6-AEAE3614A6A0}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{0944513F-5BB1-46E7-882A-ABCB91A7269A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}