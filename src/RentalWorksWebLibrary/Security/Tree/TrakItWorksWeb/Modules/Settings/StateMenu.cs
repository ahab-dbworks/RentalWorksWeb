using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class StateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StateMenu() : base("{D98F68C1-E567-424E-AF44-9016AA112131}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse        = tree.AddBrowse ("{36237606-AA65-4F69-9D08-7476AEF95BED}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{78139F92-C71A-4BDB-8921-54079289EE6A}", nodeBrowse.Id);
            //var nodeBrowseSubMenu = tree.AddSubMenu("{E13080B0-0773-4B43-ABD8-16391C255914}", nodeBrowseMenuBar.Id);
                  tree.AddNewMenuBarButton     ("{E2F3A3CB-F777-4E22-AB98-1CE62F694EA3}", nodeBrowseMenuBar.Id);
                  tree.AddViewMenuBarButton    ("{F680D7EA-92AC-4F7C-8538-68B4D8BD27A5}", nodeBrowseMenuBar.Id);
                  tree.AddEditMenuBarButton    ("{E2F9A774-E60E-44BE-AE29-D7E4C5534F2E}", nodeBrowseMenuBar.Id);
                  tree.AddDeleteMenuBarButton  ("{8CD9452B-236E-40F6-B686-57236BD97F0F}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm        = tree.AddForm   ("{2B35A5E1-F0FC-444F-8911-102B145D981B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F14E37B0-227F-4B54-918A-642A3CAC2BBB}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{ED9DF932-614E-42C7-867B-707610A0B9BA}", nodeFormMenuBar.Id);
                tree.AddSaveMenuBarButton        ("{F40429B1-8264-415E-AFAD-B30A2F292013}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
