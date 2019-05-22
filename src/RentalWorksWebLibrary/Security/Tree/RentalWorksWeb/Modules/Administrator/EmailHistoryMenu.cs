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
            var nodeForm = tree.AddForm("{D96AF7E0-617D-498B-8F5B-838AAA3E38D4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{8C05B251-0746-43CE-9E7C-C5EBB28326A8}", nodeForm.Id);
            tree.AddSubMenu("{F8D33B05-8AD7-4234-A759-1D081D6FED65}", nodeFormMenuBar.Id);
            //        tree.AddSaveMenuBarButton("{EF08246D-2D0E-4F8F-9529-3D6A4BC845B3}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}