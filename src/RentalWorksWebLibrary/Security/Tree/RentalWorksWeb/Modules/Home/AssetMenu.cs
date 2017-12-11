using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class AssetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AssetMenu() : base("{1C45299E-F8DB-4AE4-966F-BE142295E3D6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A17EA3A6-C1D3-4A80-A005-59B33462382A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{668F1FB7-B4B3-4327-8C7D-3C65AD51AAF3}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{04DD2FAA-91D4-495C-9D77-ECC363C2CD56}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{E0BF75B1-2579-46AA-A583-B0C0F581926E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{71AA2092-F6F5-426A-BA13-9BC3CFDF403C}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{E02B5164-378C-4BE0-B2AA-5E2FF24B4497}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{212CA338-F48D-4D3E-B54C-0F918446BD8D}", nodeBrowseMenuBar.Id);
           
            // Form
            var nodeForm = tree.AddForm("{300C734C-A159-401E-B521-551A4870B97A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C7E446A0-018E-4754-843F-B468154DEA38}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{3ADDEFD7-A2A3-4143-80B5-A0710B08C0E4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D1C61195-6D35-44F9-945F-08EE56318B83}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}