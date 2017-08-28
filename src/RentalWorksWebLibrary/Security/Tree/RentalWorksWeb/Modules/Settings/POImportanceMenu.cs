using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class POImportanceMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POImportanceMenu() : base("{82BF3B3E-0EF8-4A6E-8577-33F23EA9C4FB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{99C2D5FF-C0E7-484D-ACFA-C6896CF5F3D5}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B37ACB3C-9A91-4E69-AD1F-A1ACAF05CA5F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B4C8E02F-003C-4849-8331-9C408C58C4A2}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8EA10D4F-B606-475E-91A5-6044EA16211C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F3C5CAAC-ECF2-43B3-9B4A-25178C35C9B4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B1F41DFF-B015-4CAA-A00D-CD516F723A26}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F9369446-BB83-4D69-A7AE-0ED2DB768808}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{AC6D343F-EE8A-48D2-A381-391DB4EEEB7F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{26ED0D85-A653-491A-85B1-FC324ADA8DC9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D79D153F-126A-45F1-9539-4093E087AD49}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{23235AA9-861F-49FF-83F5-B5562BDDF384}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{618422B6-C980-4DB5-A3FC-9FA772C52703}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{64DAD34C-3754-4FED-83C8-963FCAA7719C}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}