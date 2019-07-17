using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class RegionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RegionMenu() : base("{A50C7F59-AF91-44D5-8253-5C4A4D5DFB8B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{2527AB6D-AF40-4826-8ECC-43E19FF6ABC1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{EB1EEC1C-5FA3-4652-A61C-BFD66E05D392}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0B5C855C-1ADA-499B-9C39-52E42A282D85}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{956DEBB5-D672-4B80-88BF-85D38F3F008A}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9032E7B2-D84B-4FE0-9D9F-9CC7E2168F5C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5289CAFA-0911-4075-B4B1-C5061F1C69B5}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BFA2D730-3122-485C-875C-E4336D00DF6F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B4C0682C-9C03-43B0-BBC0-8D51FE5E3761}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6FC5D5BD-1EBC-4B2E-BCE8-54E53DC082A9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{FC74E976-FB46-4E6C-B24C-BD49D9E88B66}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BB683102-7F29-4243-A7CC-C5D454A98DFB}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{89B55F09-1CB3-4E18-92A0-E9377610D712}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8F136188-666A-4204-B46C-249D3DD654C2}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}