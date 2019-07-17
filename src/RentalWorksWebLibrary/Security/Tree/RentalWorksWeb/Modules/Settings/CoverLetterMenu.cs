using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CoverLetterMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CoverLetterMenu() : base("{BE13DA09-E3AA-4520-A16C-F43F1A207EA5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B779E30F-EE1B-4849-83CB-140D5A227645}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D6FCD09B-CB0A-4EE3-9C05-69DC495EFE67}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{181E23FA-AE10-4710-AE7C-1D558E250CD8}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7EE9032E-8A31-4ABF-9E88-250BCC65BEB0}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{55D76D83-3845-4E78-9E43-86908B4B09F8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{C020A9B2-9F64-4795-9557-EA9B869E3154}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{CEB5F2C4-EE95-472C-ADB4-4A4F1D99BC27}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{DD12BFE6-FE3E-48EE-A514-0BBAB319204D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B5D2F474-5B19-4C91-A747-41643251EAD6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{48E2C57E-9892-4A64-82A4-CD534A72BF6D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BFB7BA4B-F1E6-4F82-B2B4-67033956E450}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{9B1D493F-1030-44BF-906B-2D397E948A58}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9B291F64-B292-4CAD-9A0C-8567C1DE29FE}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

