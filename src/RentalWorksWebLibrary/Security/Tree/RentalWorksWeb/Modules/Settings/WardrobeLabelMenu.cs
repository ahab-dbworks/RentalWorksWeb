using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeLabelMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeLabelMenu() : base("{9C1B5157-C983-44EE-817F-171B4448401A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{55866661-2AA5-4D63-A785-E47335C55DCA}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E300E04B-66D9-4C0A-8B88-39CB2DE73B91}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D1C3DCCA-933D-4162-A233-ED52615FDA74}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7B163E2C-72D7-41A0-A7F8-F0B765D11810}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{EE6425A4-EADB-40FA-9A44-BC6F89C7554F}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{353AD879-64F8-4615-8F76-56017F7D2FBC}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{A46A8A91-98A5-4AFB-BFFE-E79DA7AD7E8F}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FC63E00B-010D-448D-BCFF-76BA766B37CE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A5DCAF0C-F5A0-4FE5-BD3C-80721FFE2EA4}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{22A5526B-AF7C-4AB8-A64F-971D0972EAC4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8F0599A4-B6E0-4EC1-ADDB-0B9C8DDC7894}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{5398EEE4-F01F-44A4-98D1-CCCC2D9EC1D2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A3AD0AF3-67A4-4F12-B6AC-A05B322AB8DE}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

