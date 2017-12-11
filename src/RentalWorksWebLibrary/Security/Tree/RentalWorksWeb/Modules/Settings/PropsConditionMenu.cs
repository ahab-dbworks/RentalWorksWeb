using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PropsConditionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PropsConditionMenu() : base("{86C769E8-F8E6-4C59-BC0B-8F2D563C698F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A92AD723-C8CE-4EE9-A60D-CD3AA2165606}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{BCA147F4-6B25-47FD-9D7F-4A0C742053C6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B2CC9641-9FDE-4261-8829-9244F3BBD50C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{2C21E1A6-5ADD-470A-80BC-0119F68A18A2}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8393B027-90E1-4DE5-BED1-ACD48BA8A8F4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2C302042-5311-43F7-B4A9-EC51CD6A23F6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{45ABE593-5C80-461D-AEBA-B3AD3FE676DE}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E214ADE2-6E7B-4BBA-9612-7A98CCEAC87F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C21A6796-B090-46DD-9A06-AFB29B29DFF0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7333EA9A-CDFA-4EE8-AAA6-964FF32AE4B4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5CFECBE5-78DB-4514-8D28-E854315B1B69}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C3E2C98F-80A9-4A52-80EC-4B9B8033DB09}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{B8699313-EBD8-42D1-991F-52C132787655}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}