using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class POApprovalStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POApprovalStatusMenu() : base("{22EF1328-FBB1-44D0-A965-4E96675B96CD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{49929696-2893-4C09-BC16-45553FD3DB4F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{06A357D2-0CCF-4D3F-BFCD-B0489C86D9C3}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{602550AC-AA46-49F0-A18E-F54BD4C6A155}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0F560ACB-4FC6-4ED8-8DB3-74A5812C7C08}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D677F7C4-1110-4C0C-A1A1-D85A1F424340}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{E22F1E2C-1283-4CB5-9774-30A68B9EF72C}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{8072D8EF-715C-4464-93E3-AFD4618AC4CC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{42B3BB55-2986-4EDF-8D94-76CA35FEDBF4}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{22029887-08DB-4A8D-8406-4FC7C8922CFB}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{180501A4-D678-4A86-AD50-5D95B0CF3F20}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AAD9A749-FFA2-4695-9443-8E70AA6200F1}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{08249D17-51F9-4F86-99E2-5E36343E7059}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{CA66EA67-A263-48CC-AE22-F670D41F7F59}", nodeFormMenuBar.Id);
        }


        //---------------------------------------------------------------------------------------------
    }
}