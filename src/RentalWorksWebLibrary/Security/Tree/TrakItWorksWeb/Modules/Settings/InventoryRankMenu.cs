using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class InventoryRankMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryRankMenu() : base("{93021F1B-A379-4D3E-9668-1444E13D9952}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{DE23939B-A5BB-463A-A566-B2AA7817DC8C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{59CF6A66-BFCA-4965-B637-F556C953C3FD}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3F6DB8EB-9696-44DF-B1F0-AE784AD59E4E}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{BD46D42F-0FBB-41AA-94D2-1C2FFEFAAC18}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1B69F1A4-7BFA-4C80-84F0-CE55FD5EDB73}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{06E509BD-80F3-4B14-AAA1-0CBDD59C98BB}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{95EDD04F-D550-43F7-A393-325D521025C3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FE609EEE-0A99-435C-A350-46DEFDB281D5}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{AA0CFE69-5F30-4E27-B742-24C1F7CAD9CF}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{71F73E06-2701-4B40-B906-7CAF3D3DBCCC}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E8E13EE6-67FB-4BF9-9782-231F22020302}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{7234EA3D-B5B8-4A72-ACD1-047499E8FCD3}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{AB148197-0847-4A07-9FDB-C3265CB50D84}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

