using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class LicenseClassMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LicenseClassMenu() : base("{422F777F-B57F-43DF-8485-F12F3F7BF662}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{53BDAB37-6795-4DD7-9B28-B3FB992AED51}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{CBD02EB4-D990-433A-9760-441444D281FE}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{A689B279-A2E8-482E-A871-FB910183782A}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{C2CF57B9-6EB3-4616-83B0-76815E08D05D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{612F0346-58AC-44FA-94D1-100478A7C754}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{38A2A1DE-B565-4FB4-A6F5-793384C2F451}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4EFCF812-138F-4012-A56A-3ABAD7AFBC9D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{98B79639-442C-4354-9235-09C78ECA147F}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{09DC7C4F-0BF1-4E9E-859C-15275B2A6954}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D9EFFEA2-34E9-4D4A-91A0-B6F8623CF3DF}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{FDE4C06C-C60F-435C-8EDC-CA9DF3DF579E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{A88AD161-2CEC-4B23-8570-B430DDDE9091}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{F494E9CF-E426-40C6-93FC-8DDEA2154946}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}