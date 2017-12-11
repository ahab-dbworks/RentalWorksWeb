using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class GeneratorFuelTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorFuelTypeMenu() : base("{8A331FE0-B92A-4DD2-8A59-29E4E6D6EA4F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{34831E20-CD94-4D29-9697-FCA0B32E6724}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{70DF7DED-CC08-421C-9163-389819D5CA13}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{4D57647C-FE1B-4895-8F29-7E9A7995BB80}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B2274C2F-DB0B-400A-8C5F-BDF8E0B97789}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5A2F04FE-33F8-4DA8-8D57-D2464C41FB9A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0EF6C775-9094-455F-A07E-25F18A66A654}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{7D1490D0-57B9-45AD-8DBE-879C7314B673}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{887A67A9-71D1-43BE-8453-1E38AE31496A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E6035FA7-23AB-4FC2-987B-FCB5DBD42973}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{C6BF89A2-9CF3-4B6F-80AD-5504D03A78B8}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{B292C5C7-B8A3-402C-B26E-7F518EC2D3B2}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{78E2CCCF-CD2C-4301-9E17-91C5B056A7C1}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4C245728-3DD3-4469-BD1A-B14E43ADC2D4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}