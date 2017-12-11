using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProjectDrawingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectDrawingsMenu() : base("{7486859D-243F-4817-8177-6DCB81392C36}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{5FBC0BC6-7908-455F-9AA0-1A16AD326DDF}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{EAF22A7F-1D25-43F6-A9C4-7152C8C51B9D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{E8147DB2-AF8C-4EEF-9B28-CE711B9D0B49}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{81DC4EE1-2B00-440F-B606-EB66D0F4A552}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{72B5CE4B-A3F3-4E0F-B8D9-A9640A8E2A3D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A9D69390-5BBB-434D-90F6-C491EB38D323}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{8DABFC83-29CC-4BA8-ACE9-E669B8697895}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E56EEA9F-7F55-47DE-A25B-495B017A8C4F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{ED51281D-2F94-4980-8BEA-E1CD8CC54F55}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{CC64255F-825A-46EE-83B2-7307ED561E2C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6AF279D7-A0ED-4BB3-A929-EADCE3DDDE14}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{F85194F1-28E0-40A6-ADF2-F4D8E8BD0A36}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{12DB686E-6714-46A9-8381-ED7016AA3DF1}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}