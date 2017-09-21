using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class RentalCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalCategoryMenu() : base("{91079439-A188-4637-B733-A7EF9A9DFC22}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{CB3810FB-529F-4A23-8148-A62BD04B0D4A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{5DFFB6E7-A7B7-4D54-B02A-CADAD9F93D4C}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B63B8844-53FF-43B4-ACE2-E887F3E8E475}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{86DE290D-4D84-4610-8CDD-1A4662DFADCC}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3E44E730-C5BC-4678-B8DD-E812F7A1E024}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{7B2A43CF-E982-42AE-A95A-1872B74D8BB8}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{17B18E85-D15B-438D-81D4-871473C51EB5}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{1D8092B2-6A0F-4239-A5A9-BFF36E951429}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{BBFC4F72-5C1D-4B0E-9327-DA60ED980BFF}", nodeBrowseMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}