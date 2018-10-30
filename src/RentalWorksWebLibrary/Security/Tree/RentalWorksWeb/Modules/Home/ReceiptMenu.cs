using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ReceiptMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReceiptMenu() : base("{57E34535-1B9F-4223-AD82-981CA34A6DEC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D5727954-E94D-4E40-BD64-33B580FB8964}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0B4BB424-DF77-4E90-83DC-67B91C22D971}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B5129D2B-0DA9-4A91-AB04-6BC0F6B69778}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{523FE260-B845-4F8A-A9EB-53151A167EA5}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1672938B-EE4F-4D35-A049-619FF6A6CB0D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{46F364BF-B044-46E2-83DD-6427F90F2D15}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B80F3138-0A8F-457A-A52C-6B2D5A906ABC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{0C33B440-52E0-4787-A5AC-A637218612B4}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6A4C4595-8D15-48F3-A9CC-88861F8A59ED}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{79C51AD1-A9A4-4A68-8504-E99BD34BCB85}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A4653ABC-FAA9-4060-ACBA-B134D373545B}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{E3A03137-C12C-448A-AB7E-1D1CCAEFDEF3}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{59A9AC77-0372-485B-AEB2-715F2BE3C451}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}