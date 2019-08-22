using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PurchaseOrderMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PurchaseOrderMenu() : base("{67D8C8BB-CF55-4231-B4A2-BB308ADF18F0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{2045E74D-2FAE-4EA6-8711-8B763160D08D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{8D7CEABF-5A49-482B-92F0-C1384F51756A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{DA73B5F6-9D05-4982-9F81-BAD1B42BAA16}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9C6F753E-AD60-4951-9F97-7D74ACD837A1}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{C92CF1AB-6253-4888-B383-B9173A7D3B8E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A0084E1D-7D42-4C99-985E-D26118E3FB72}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C6B4B553-9E32-4D22-822C-B31D0C85498F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{BB6C3378-EA91-49B3-95F3-BE9D66DB8783}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{02E99B0F-8DD7-453E-9D0F-B1A17E8F674B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{FC4A0D99-A29C-4A47-ABD2-7A040E2C93E9}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{354106DE-1632-40FE-A004-7BBDC13A4AC2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D69624AF-F2C8-425A-AC46-757936472566}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{EF9A1E0F-B8F8-42AB-8957-5D25E6FA34C8}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Search", "{D512214F-F6BD-4098-8473-0AC7F675893D}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Receive From Vendor", "{4BB0AB54-641E-4638-89B4-0F9BFE88DF82}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Return To Vendor", "{B287428E-FF45-469A-8203-3BFF18E90810}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Assign Bar Codes", "{649E744B-0BDD-43ED-BB6E-5945CBB0BFA5}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}