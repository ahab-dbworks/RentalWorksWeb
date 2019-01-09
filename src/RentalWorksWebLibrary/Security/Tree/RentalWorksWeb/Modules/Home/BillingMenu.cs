using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class BillingMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingMenu() : base("{34E0472E-9057-4C66-8CC2-1938B3222569}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C355874E-FD6D-45A9-AE2D-45623F5A705E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{F94FDA27-C427-468E-BE01-A395A8032F10}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{FD2FF91F-EE58-4AF6-BC67-D23FEE1E0D87}", nodeBrowseMenuBar.Id);
            //tree.AddNewMenuBarButton("{47F0F323-EC4B-409C-87B8-D7042541227F}", nodeBrowseMenuBar.Id);
            //tree.AddViewMenuBarButton("{0F5DC392-5895-4E19-A6FF-3E194FF4CAAC}", nodeBrowseMenuBar.Id);
            //tree.AddEditMenuBarButton("{5230AAE3-E2DF-4DFC-9DA9-FCFFCB13CA8A}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{12021AED-2868-4ADD-B5A1-6194581A058B}", nodeBrowseMenuBar.Id);

            // Form
            //var nodeForm = tree.AddForm("{23AA845C-B254-488E-93BD-E710681186C2}", MODULEID);
            //var nodeFormMenuBar = tree.AddMenuBar("{EFA3370F-7B1A-4555-82A7-030B7C9CDEE1}", nodeForm.Id);
            //tree.AddSaveMenuBarButton("{79BB5C05-6E28-420D-AB1A-7EC9275D6017}", nodeFormMenuBar.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{56AF631A-2969-48FE-AEA8-53574D37FD58}", nodeFormMenuBar.Id);
          
        }
        //---------------------------------------------------------------------------------------------
    }
}