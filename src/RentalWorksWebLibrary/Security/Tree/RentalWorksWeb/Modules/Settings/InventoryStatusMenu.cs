using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryStatusMenu() : base("{E8E24D94-A07D-4388-9F2F-58FE028F24BB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{10585CCF-D0A0-4CBF-90D3-15761CCE31D2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E9C2C8C4-BF0E-4A95-8209-2579A808B055}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{4007D65D-828E-4DAC-9049-980656DFABBD}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{11718254-397C-4246-B9F0-C672A52F2258}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{010FB981-658D-40EE-AD77-FBB9855D164E}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{36F0240B-094D-45BF-AEDC-AEB412F611C3}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D5A6EE97-3A65-4546-AE43-04E4B815ED7B}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C05D44AE-09BA-4D9D-8A91-28E8C8E83C4B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{396F50CE-84A6-447E-A59A-767D7B41A228}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F9F4A7D8-F911-4A69-9D2B-DBC3112A6D55}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2343AD16-6D11-460F-80D9-D4C2D2D1C625}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{EE385BF9-1AE7-4FF9-9E6C-7763A4D1EB1F}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{983C2FBC-800D-4DA7-B764-4E6AAE317D52}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}