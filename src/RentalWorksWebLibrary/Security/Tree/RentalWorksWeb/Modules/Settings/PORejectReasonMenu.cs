using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PORejectReasonMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PORejectReasonMenu() : base("{2C6910A8-51BC-421E-898F-C23938B624B4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{74A4A7EE-031D-48C9-8E2C-FF567C68F4A8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{30D9D446-AD60-427B-86C2-E6FB0CB722E5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5BFDE4F0-01FB-437C-A1FB-8C7D6DC9C6C3}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{2EE5FD8F-F4D1-4A44-873B-D243F36718FA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D28C13F1-6AC9-4404-8A2D-065F95F70420}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{91E37FDD-75BC-4FAB-A25E-16E5139B5A8D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{7E027E22-550F-440D-9560-CEAC714FD85A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B2A6B20F-E25C-412F-8AC4-9802CF07BC07}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5A0FE796-4F84-4715-A664-0C29B57A8AF4}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0E96BA7E-2518-487B-BF39-883175033038}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{3476EBB3-D1DA-4146-B864-501A35EB35D6}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{39D5FD98-D8B9-4C8A-ABB1-0DA5B43537ED}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}