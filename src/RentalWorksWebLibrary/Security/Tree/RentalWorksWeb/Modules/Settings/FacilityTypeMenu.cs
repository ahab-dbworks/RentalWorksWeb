using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class FacilityTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityTypeMenu() : base("{197BBE51-28A8-4D00-BD0C-098C0F88DD0E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1BD7796E-5A24-449F-BC9A-E8336AAC69C1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6A96AAE7-D764-452F-9103-93A42F94D1FD}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6669B7A5-E034-41AF-9647-60C804EC0452}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3F71A02B-4008-48A4-B52E-A420178B2209}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F332FAA4-D416-4A7F-B265-D170801434A7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{86D062D3-4E2B-4A57-96DA-504A39D5FAF9}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{448CE4D1-CC3A-46C9-980E-A32F91518EA5}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{39B1C8C9-8199-40D1-B6C6-28230340841C}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{34FD7B04-11B7-40DE-9EB3-791C1473BE5D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{170C5A98-A6F0-4496-A243-424F589FBC05}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7C0C0146-F62D-432B-816F-470EA26F3F22}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{4803EBF5-0EBD-436A-B15F-1279C5F448FC}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9EFCC42B-53C7-429F-A38E-FCD6FFE6101D}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
