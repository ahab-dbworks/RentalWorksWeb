using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DealClassificationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealClassificationMenu() : base("{D1035FCC-D92B-4A3A-B985-C7E02CBE3DFD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{4F6799B2-8A16-41DC-9162-28437D327B7A}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{4C4443C7-36BF-4CAA-BABD-49FF53A6D741}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C35BADEF-27EF-413B-A927-75EA8175FD33}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DDBDE3BB-D590-4BC2-806C-ACCA69A81C9D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{54979403-4462-4206-96BD-FE41B5968CBB}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{AB281D89-1EF5-4842-A0F9-2A099105BAC6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{623DEB9F-8426-4F1C-8B37-36D47B4B2342}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{53CD5FD0-23B7-4A66-B708-C09239E72F98}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{3651CA35-6A1C-405B-A097-42C6409F5413}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{EE5BF4FC-BFAA-4009-9134-30276B94EAC0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{833E70E3-C68F-42B5-A73A-F44C7CC82DE1}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{E5048C2B-64D0-4517-B20C-D2DA4060E814}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{DA5E30D8-957F-41EF-BABE-3CEC58D1FBB0}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}