using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FwStandard.Security;

namespace RentalWorksWeb.Source.Modules
{
    public class VendorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorMenu() : base("{AE4884F4-CB21-4D10-A0B5-306BD0883F19}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{E3CE5A74-515B-422F-873E-D2EA526A897D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{22E50A4E-23A5-4488-A440-C2B7DA375874}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F557CE03-A117-4353-95F3-6E304B8F4FF1}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{215B04A9-95A8-481A-B4C5-D2821D357B68}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{282752E4-E123-41E8-A0D6-1C692E501B7C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0E2FCCF7-5916-4D93-8685-5BB453AF4DA5}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{EA841B44-7406-48C5-A82A-14D011D65161}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{11FB9896-A785-4473-9235-21345C027D0D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{87A48776-8687-4EF9-A771-D210081FDA8D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{38BB0346-60E2-4D77-ABB8-E7005597D5DF}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A8B04AB8-EE23-4E2E-9336-77BE22ADF15C}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{69952825-5066-435D-BFE2-F6E4FB3B37BA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{0A46927A-CCDA-4B74-A752-2F5504F8CC14}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}