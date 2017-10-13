using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SpaceTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SpaceTypeMenu() : base("{EDF05CFB-9F6B-4771-88EB-6FD254CFE6C6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{2A3542FD-4B88-448C-8763-5BC9AA5E9399}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{A7A53761-160E-4FE5-969A-57F3921DABD2}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{8D36031D-AFB4-4707-B20A-1DC435EEC2AF}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{24F43C36-3730-47DC-B44F-B72799D5B3FE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D4CAC6CA-9C44-4FD9-9B39-53FF9DB4E2F3}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{66397EF6-3B28-496B-84AC-5EA8B1394B61}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{5D889524-548A-4FF8-BADF-82581D740470}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4FEA89E0-9240-43DC-B85F-F8F9445BC11A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CA37EDAD-DDE2-49DE-AF8F-D3E467B913D4}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{64C2A72D-A279-4504-9AA8-AC9C0C84C1E3}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6008719A-9D32-4D5A-A084-4AD07023A01D}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{668296CB-C035-4C01-9350-15251303A075}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{396C4B73-6BA1-4E2B-885C-BE4BEDC221B7}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}