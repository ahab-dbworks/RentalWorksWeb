using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryGroupMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryGroupMenu() : base("{43AF2FBA-69FB-46A8-8E5A-2712486B66F3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{AC5D1883-F49F-4C67-9FCB-C90DF8F92E4F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{397BC42F-D1CD-4DBD-94D8-3180529B0CB5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{ACAE24E3-AFA1-4DAF-A84A-4E11148BD1FA}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{916BCFE6-4634-45D7-85C2-E1293AAA70FE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8D94F586-ED2F-4339-9362-DC74760A170E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{3A2D79A6-97B2-45D8-BFB5-6EBD9B5DC479}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{3CD474F5-22C8-4A2E-A209-8541B303021D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EF0C74EE-DBEF-48E3-A068-66AA8CEE8CE7}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9D257587-1706-4B8C-9D13-4A057831A150}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{75FA2FF9-AB61-4151-BAB8-71031DF5ECA0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A7C0C314-E64A-4E0F-9311-4EB20B002130}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{0D0AE506-997C-461E-807B-DE7BDD174450}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{297B935E-C488-4EFC-AB5D-6BFAD4FB304A}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}