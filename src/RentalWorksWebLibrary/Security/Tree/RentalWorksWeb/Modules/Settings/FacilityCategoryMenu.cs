using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class FacilityCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityCategoryMenu() : base("{67A9BEC5-4865-409C-9327-B2B8714DDAA8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C7C18837-876B-41DC-84C5-610D0A62ED05}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B3060A6E-90F3-4C39-A610-4C529F84C1F6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F9F23BEF-78E1-4649-96C4-57F50EEE0DB4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{A5642B4A-7C2F-430A-9791-8F28A64E6318}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A614D119-23C5-41EA-9967-7ECCD71EC5A7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0E461AC4-E6B0-4037-8991-0550ABF9C0A1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D2537095-8C69-432B-9DA1-FF269583DDB6}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C745D41E-5342-4E4D-970E-FD9C508F46CB}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D1BCD2F8-4D5D-4364-A24E-9E9A37C6FDD1}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{39931105-EA8C-471E-A7FD-14B371D51C4E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7258169C-CAB2-4D23-9086-C2086C744180}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{07E414D9-F945-448A-A2E8-BD9A2E5EC5B4}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{F203F289-B851-46B3-8DA7-DF61A407CEA2}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}