using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProjectCommissioningMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectCommissioningMenu() : base("{0EFE9BBA-0685-4046-A7D6-EC3D34AD01AA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{80C53C61-92CE-451B-A524-AA79745C20E2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{2CBEBAE5-F6AB-4B70-8A91-D56231822DF2}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6F893C15-61C7-4231-8E79-9E02B358E0FA}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3AEC043B-C949-4716-A88F-23558A011B0B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{82E83BCD-F082-46A7-B262-67BDF0C119D8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CE986CC3-09D8-4A63-8EC6-436BF5930D7E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C70737EC-0E05-4F5D-910A-6A97BB1742FB}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{82A48EA0-9392-4A37-993E-F50C22F2C581}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0DC65C5A-946F-4ED5-8796-ED1E4846013A}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4C2F4D25-0B72-4C4D-96BF-3706EFB040DC}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2CDAB08C-FCE3-484E-ABE0-F0B7BADCC2C0}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{0EB24B04-A992-4F5F-8786-6044E87C4F0A}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{ECA1E3A9-5D68-4078-BD13-83DDEEFE17A8}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}


    

