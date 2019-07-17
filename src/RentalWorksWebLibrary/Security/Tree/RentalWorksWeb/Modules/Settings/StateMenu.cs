using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class StateMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StateMenu() : base("{B70B4B88-51EB-4635-971B-1F676243B810}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0D902A75-779E-432E-B5FA-3ABF2897A174}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{78AC55C8-93E7-4F71-A72E-BFD56E9BB5B0}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{069BBFB0-1DE3-46F9-9438-77E96FE8ABF5}", nodeBrowseMenuBar.Id);
            tree.AddNewMenuBarButton("{6590E27F-DFDF-44EE-B1BA-BC17E1C2ED6A}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{51169064-BAF9-449A-8093-AFEBEE840800}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4603352E-2B14-4B75-807D-16981E1DC8FF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{DB0CEE97-E0FB-4F6B-9893-3FA03E622C99}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{13C9E967-AF8D-4C4B-8544-4291E38EED39}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{22573A48-F4DB-47C7-8C38-CCA3D191DCD1}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{235B479F-77F2-4590-B312-E1A9FFB85EFD}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{143974C1-B676-48A8-8505-2C6D0F781F77}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}