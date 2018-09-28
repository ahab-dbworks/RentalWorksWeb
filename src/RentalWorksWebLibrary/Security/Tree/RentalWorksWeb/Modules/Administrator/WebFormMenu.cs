using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WebFormMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WebFormMenu() : base("{CB2EF8FF-2E8D-4AD0-B880-07037B839C5E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0966EBAE-BD27-4A18-8420-AE9EB138C76F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F3D7D533-DA33-43AD-B474-C8F51AC072A4}", nodeBrowse.Id);
            //var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{EB58FFA8-DE71-434F-A4B4-D9D3786419D2}", nodeBrowseSubMenu.Id);
            //tree.AddDownloadExcelSubMenuItem("{5A45F16F-D7FA-410F-BE8D-304E0CFC1327}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CF64398A-D356-4033-981F-6A36BB92E441}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D9CA53F4-B941-4986-8731-FF57C08C38F2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{33C72E79-D88F-4754-BF76-D140BCA48AF8}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{C550471B-A797-447A-AD7E-55B70D822977}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{EE121DFF-7F47-48AE-9D22-8507F5709522}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{CA96264F-E89A-4CA1-BCDE-BC5EC7D6787F}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{014118ED-AEE7-49AB-814C-0CEE948BEAA7}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{C38638E7-D04A-4777-94FB-5F807A53D34B}", nodeFormMenuBar.Id);
            
        }
        //---------------------------------------------------------------------------------------------
    }
}