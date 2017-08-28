using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VehicleColorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VehicleColorMenu() : base("{F7A34B70-509A-422F-BFD1-5F30BE2C8186}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1CC971BE-6102-45A1-80E2-051BD1276870}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B6A4AC46-7F5F-413D-A89C-E6047EA6D9B7}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{62F0418B-C3EA-4E29-8F64-9BD7DBD2EE37}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{C869617F-8D18-4C28-921D-84758BAB73AF}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B646CA9A-BD7A-4F39-89EF-C1B2E6CFC29E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{DAF07055-3296-4771-B14C-3DF06C673A57}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D5C32487-1EC7-44B7-889A-B1355C6D123A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FD5F8B56-40D6-4899-8E92-C8C4061F8AC0}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{651CDA5C-4A6E-49BF-A682-5CEE29B88757}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{3F4CE483-ACC7-488F-9194-6EAB81C4BB4E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{963FA778-05B3-4A29-868A-E8707C645522}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{D1C3AC4B-3A7D-4EE9-9798-830219ED626C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{2621966B-E040-4F71-816D-FFE63B6C55BA}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}