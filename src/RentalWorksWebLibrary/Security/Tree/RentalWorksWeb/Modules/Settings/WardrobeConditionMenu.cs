using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeConditionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeConditionMenu() : base("{4EEBE71C-139A-4D09-B589-59DA576C83FD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C11F80F1-6176-4D20-BB4B-A8CE6A6DF19D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{23FE6C47-5762-4B19-AB38-21E05F968721}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6FC3D757-881A-474F-8104-5FF116DBFE0F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3E0305E1-DDE8-473C-8217-27807BA8AB4D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E532A1B6-85FD-40C8-AABA-C447464D0563}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{BB69652D-84A3-4433-A29E-DB41D1A80E2D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{9CDC8682-846F-429E-B6F8-4C6235E50F42}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4B0A29ED-4FCE-456A-BF02-0507F0994412}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FF720407-741E-4EF1-9452-15FC2BB5163D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{ADFF69DE-0FEA-4C46-94F8-1F55D5A63323}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BB35F35E-AF5F-4499-8427-E4A0813B21F0}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{F31B99B9-AE0F-44E4-83A0-DFBC83470012}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{312671F8-CC60-4E49-A1FF-2A99312376BC}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}