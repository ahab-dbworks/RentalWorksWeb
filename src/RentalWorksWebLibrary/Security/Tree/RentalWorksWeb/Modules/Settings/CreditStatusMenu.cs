using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CreditStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CreditStatusMenu() : base("{A28D0CC9-B922-4259-BA4A-A5DE474ADFA4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0012EF81-106F-4C94-9CD1-729E9E0CF0DA}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{DBB15008-C0FE-4BD1-947C-CF3365CAC47A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{A568E984-4CC8-4628-B78C-A340EB026B36}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F26F417E-AE94-415C-AE33-E3CD8E3DE3CE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{94F77E23-20D9-4D3A-8A4D-C83192BBF97B}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{F4C04574-C7AF-4DE2-8B75-FDB2F106FF0C}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{9F12C805-D0BE-43E5-8877-80D904CB83F9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{47B6C776-F6EA-46C5-9E20-24552EBBC46D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{48CBE989-2EC6-4E7A-9F09-188679882279}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{EAEB8B65-86AE-40D4-9B54-E9C0E6B46075}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{06B5130F-DA2A-4F9B-BBC0-222EC3874B38}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{901C305B-3107-4280-9FCD-448801B272C9}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4F6C05F3-1120-4965-B64C-6A3B1F960C63}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

