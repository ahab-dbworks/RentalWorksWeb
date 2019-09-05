using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class SearchPreviewGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public SearchPreviewGrid() : base("{A6C93317-0DDC-4781-9B01-2EFC78ECED40}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C8EE8BBE-DE0C-451F-85C2-1980813F1685}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B5CCC35A-75FA-48CA-AE3B-784D83F1A97C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4049E35D-0DCE-45A2-8F69-BB3FB270AE78}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{755276A8-67A2-4958-B5AB-58E0A8556FBC}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{D2A9476F-9273-469E-9193-122CDC3F274A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A3002AF7-C2A8-408F-98AF-414D4D678926}", nodeGridMenuBar.Id);
            //tree.AddSubMenuItem("Refresh Availability", "{3756AF3A-1611-4BCD-BBD9-E3233F5A772E}", nodeBrowseOptions.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}