using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class FacilityTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FacilityTypeMenu() : base("{3063B747-2536-4DA1-9E29-CB755C29CC11}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D979DDD3-6BC5-41E6-B69A-5963F8B3403E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{72B678C4-60D5-45E2-820F-E0E557F03558}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6F91B4D6-FF9F-4B52-B55E-E85CC4E781C7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8DB339E4-DC4A-4AD2-A63D-A2C4FC519DAE}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D54F36A6-435E-4AB0-AAFF-40CE456D7B0C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{E2F99A4D-B2C7-4AA3-8A44-95BBA49267CC}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{226334DF-3840-414F-B1FD-2921A52FC764}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{077B5882-7DCE-4A98-8923-617C58E11BE8}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E820F1B8-053B-4663-9CCE-EF2BA07CD97F}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{427FF552-A535-424A-83A3-5EE3E342EA89}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{55E70940-5CC0-49BF-ADB7-180E7A91C5A8}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{017718E5-544F-41E1-8572-D90B60C791EC}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{0A522CCC-D0BC-44CE-B7E6-980169A6CA99}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

