using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CustomerCreditMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerCreditMenu() : base("{CCFCD376-FC2B-49F4-BAE0-3FB1F0258F66}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3113A8C1-A6D2-459C-916D-A5C681F853E8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{15E66916-3F58-4947-9135-15199B3243B6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{E444C88B-1E9C-4E29-9FC0-2D3FD46CE763}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{E98DD846-8E9A-4314-BD71-5C7CCA290385}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{40CFC7D0-7478-4E6B-8A45-0CA3D5744BF0}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{D4D1AA0A-B8F6-4C2C-A2AC-CA0D16C9A58C}", nodeBrowseMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}