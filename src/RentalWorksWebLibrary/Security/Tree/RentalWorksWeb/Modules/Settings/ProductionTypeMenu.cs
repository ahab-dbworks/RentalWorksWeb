using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProductionTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProductionTypeMenu() : base("{993EBF0C-EDF0-47A2-8507-51492502088B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9D895588-8154-494D-A6D1-EAA05EA7BF86}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{120998DD-B62F-46DF-BD43-C2580E2F8AD5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0E0C4909-4C4E-41A1-9AF1-4B0D32A88FB1}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{17E11B7F-A13A-48BB-A29A-D40D274D19D7}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8F82207E-3ADD-44C3-802C-732D62AE4FB7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{E02B8067-E340-4F16-8499-BCE193D0DE86}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F6878545-B613-40EF-85A9-73489081F8EB}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{F6FD6632-5839-4F76-824D-76F3088D94E7}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CC67737A-3FFC-460D-B2DB-879B9BDFF6B9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0EAFBD90-8E64-4D73-98AF-6C0B98FD0884}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{9F1B3CC5-0BA3-4F0A-B217-0E8DC9A04D32}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{7ACF173F-E9A9-4961-90CE-5FB1BC5C8E8C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D4D75FD0-D40D-45BF-9674-6ADDDAFA22E4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}