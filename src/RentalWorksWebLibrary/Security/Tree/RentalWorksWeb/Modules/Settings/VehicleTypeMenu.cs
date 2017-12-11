using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VehicleTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleTypeMenu() : base("{60187072-8990-40BA-8D80-43B451E5BC8B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{4ABB9837-973E-4DDE-BADB-978287D5ECC8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6F8D14A7-5B76-436B-88DB-60EDEE59C0C8}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{7AC76CEE-2D46-4CE7-9908-A0CA15422845}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9FA2709C-16F3-4C71-B538-7307920E8DCD}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A2C9B34F-0AA0-4285-8528-FD27560DAF0E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1C0ED31E-4D5A-42FA-B631-AD7F10956F3A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{5CB48EBB-EC14-4E9B-9B49-2941B5475768}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{786289AD-8A22-4AC0-B4B8-C512CA25541D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{772A8CBA-0933-493F-A88B-E1756719C780}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{A05EC4B7-6A80-48D4-A8DA-89D1A7B74EF6}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{0E4497A0-ADE3-4310-A71A-7DF5D9970C03}", nodeForm.Id);
            tree.AddSaveMenuBarButton("{22DB8F21-3654-45E7-AFD7-DEE5EA98F075}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}