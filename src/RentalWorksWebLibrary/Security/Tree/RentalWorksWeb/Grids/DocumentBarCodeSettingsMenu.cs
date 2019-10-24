using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DocumentBarCodeSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DocumentBarCodeSettingsMenu() : base("{FA203471-A8BA-4607-AE29-B211995D276F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{28D036FC-1BE6-4CED-84B0-DFF74482CC6E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{CB68F2AD-9BDF-4C55-B70E-9A047D52385B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0FDA3575-BB36-4A6E-AC6B-8D6C33630AA5}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DE7B31AD-3A14-4D3B-BB10-A65B91C53902}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6DD74767-B98D-4B2C-B54C-F2F055E2F660}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{375F28AD-E458-49A3-86D2-E3FB76F10A52}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B71B8C56-3E81-4FA4-811B-0597CEE435E6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{574DBE10-9B48-4890-B49E-937AC9F3CA37}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F5512D5B-B7AE-47EF-80A6-D9E3C6B1A6DE}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{FED43B97-11AE-4919-BACA-81AF5560964C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9C2FEC50-DAC2-4D3D-A8E6-BB2D75645959}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}




