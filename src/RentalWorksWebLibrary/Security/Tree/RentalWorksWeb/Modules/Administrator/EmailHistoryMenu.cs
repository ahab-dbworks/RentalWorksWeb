using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class EmailHistoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EmailHistoryMenu() : base("{3F44AC27-CE34-46BA-B4FB-A8AEBB214167}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{84CD9174-BA21-4702-A133-3EA413A0093B}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{12D694FA-376D-4F38-B01A-7F689CB74605}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{A38E3129-06CF-44CF-8B76-884BB3743C3D}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{22D6178C-0739-4B2B-B989-98E82BC1C02A}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{EC7B9B5D-AA4C-47D3-ABA4-0939F465849A}", nodeBrowseExport.Id);
                            tree.AddViewMenuBarButton("{14670CED-4723-4EDD-B504-46755910CEEE}", nodeBrowseMenuBar.Id);


            //// Form
            //var nodeForm = tree.AddForm("{2B3738CE-5774-4CF0-B5CB-2B72A129F0B5}", MODULEID);
            //    var nodeFormMenuBar = tree.AddMenuBar("{7C627B3E-E707-4F6C-82F5-81D3996D1628}", nodeForm.Id);
            //        //tree.AddSubMenu("{AC96612C-C81D-4BFB-8E84-2FA3086C2B4A}", nodeFormMenuBar.Id);
            //        tree.AddSaveMenuBarButton("{EF08246D-2D0E-4F8F-9529-3D6A4BC845B3}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}