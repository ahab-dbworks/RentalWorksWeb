using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class BlackoutStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BlackoutStatusMenu() : base("{43D7C88D-8D8C-424E-94D3-A2C537F0C76E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9B0ED754-C476-42CE-9394-E46B8492DC20}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{1E090C3E-80C6-447C-B2D0-7168FDFFBAD9}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{411CD9D0-BC85-4CC8-A460-11E211B0B4EC}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5B58111B-6D2D-4F36-B789-9E21E4CB51DC}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2AB9E78C-F39A-4514-9D6C-69787CFF6784}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{C17DB2FC-D26A-4EE7-9FE0-C692862D94E0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{11226753-2A5A-4106-B592-8FA5EC0FFA4C}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D4E4F8D8-5AC3-4617-BEF1-AECE78A2EDB6}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0A18B82C-F0AD-4DB2-8167-B7F8F9635296}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{32C6C9EF-F16D-4D74-9626-0037BF71377B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BC997AF8-FB6D-45C8-AFFA-BD863BADB89B}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{F0D4FBB1-0315-444B-96B6-12AE8227D7B8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{BC4F8EA7-4E3B-4E50-BEF3-ED5DC4FDE5E4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
