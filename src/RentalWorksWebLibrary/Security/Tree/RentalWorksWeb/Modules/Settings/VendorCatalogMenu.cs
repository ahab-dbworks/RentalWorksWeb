using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class VendorCatalogMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorCatalogMenu() : base("{BDA5E2DC-0FD2-4227-B80F-8414F3F912B8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{20FC51AE-5F5A-4A0A-AE37-CAE4187A5976}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{6AA6DFA3-69AF-4D9E-9A66-23805C3E134A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{10931725-F7E6-48F7-8237-12CF44E39C05}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DF0E2980-B837-4CE8-A5AA-AF2143241563}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5D3919E3-85A0-483A-A828-62804DD5E5B5}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{DACC34DA-A2A7-465E-A4B4-1A0834B16427}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{40334DF7-A916-41B0-97AF-F0F3173FF8D2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{60D2BC4B-D4A3-42DC-8B81-CB9CA6A1F9D4}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{BDECC6B1-5348-44A6-B3F1-7D65CB6DD978}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{FC2162DF-73D9-41C7-B33F-7F536033230C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{157A3580-F5E7-40AB-BCC8-461ECCBEF6ED}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{EFCAF0F5-4688-43A8-9701-33163C6FE270}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{CFE8ECC9-2B34-4EF8-BF8A-04E871144A07}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

