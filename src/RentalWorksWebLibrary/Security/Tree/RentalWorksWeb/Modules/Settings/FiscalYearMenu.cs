using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class FiscalYearMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FiscalYearMenu() : base("{6F87E62B-F17A-48CB-B673-16D12B6DFFB9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{EAF9832A-9E7B-412C-87FC-8E28813DAA57}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3B08BFAB-CBF4-4AD7-AC95-468B7ABF96AF}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{A77B11DE-E2D4-43A5-B448-C8D9227E5CDD}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{62A3C9E1-4577-42EA-8771-ED5D854CF071}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{41899C39-7DBB-47F2-A97D-0ABFF20CF9F0}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{BD447DB1-5F61-4E5F-87D3-6BBE8243350D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F921CA3F-6447-44A5-9A31-F2803DF88F51}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{34082CEE-8F85-48D2-BE5A-50A542C0009F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9FF2A40A-A44A-4529-B11C-FD2D5B4E64B0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{A3F85058-0055-4FE3-83E2-B66A20DC6975}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{04FB7C7A-81F3-4F28-A620-AED452A0F0F0}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{1FF98652-9464-4273-A9C6-AC3AD1BFD261}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}