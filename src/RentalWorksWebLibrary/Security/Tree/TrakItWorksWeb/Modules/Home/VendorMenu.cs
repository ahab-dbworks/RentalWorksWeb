//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using FwStandard.Security;

//namespace Web.Source.Modules
namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class VendorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorMenu() : base("{92E6B1BE-C9E1-46BD-91A0-DF257A5F909A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9854D7FF-F252-4837-8BAE-99C71CFF8F86}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{1A3D1BAB-F464-4635-9F5E-AA4B1B4C7A79}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{046C4DA7-C5C1-42CD-858F-E1338CD93F96}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{C03B4695-D5C5-4647-AE0A-D1BFEB3EDD49}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{24E57DCF-6B50-4E97-B4D6-0DA6A9D76B84}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2E666681-A621-4FB4-8E99-30A4044A2557}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{13697588-8546-4B25-8AAE-16F36A61D27B}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D83B58A5-C681-4D4C-8BE4-12011FB24D49}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D64ED920-3AE8-4B15-BBAF-5B87B1AC13C6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{80BA5827-5DC2-4162-900E-3AA475A006BD}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{C1B0539E-C986-4A50-A1E7-EB1579BA4F9E}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{718AAA23-B83F-4DF4-AE5E-6409F8F21040}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{19B41A51-58A9-4A41-8B6A-E8D2B791EE7E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}