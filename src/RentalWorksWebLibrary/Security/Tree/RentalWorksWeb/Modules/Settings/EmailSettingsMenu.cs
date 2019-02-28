using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class EmailSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EmailSettingsMenu() : base("{8C9613E0-E7E5-4242-9DF6-4F57F59CE2B9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{BA5C6201-35AC-44AB-95EA-367A15F1E40F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{21C996E1-AD02-4F13-9CBF-7551A66E8790}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B3E369B2-1C12-4E48-B3F3-3EB6F0B82926}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{FF4E0CE5-D7C5-441C-B180-46166B7FC96B}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{8326C8CC-2599-418B-A728-62724B8033A9}", nodeBrowseMenuBar.Id);
   
            // Form
            var nodeForm = tree.AddForm("{EE334814-C972-47C6-A6F0-89DA466F7499}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5A95BB84-11D7-421B-83F5-A339B8511C4F}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{42ACDB92-C53D-4A42-8604-EF4FC007D1D2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A925EC29-DF4F-4C6F-B353-84135AA00661}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}