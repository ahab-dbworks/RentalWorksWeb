using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CountryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CountryMenu() : base("{D6E787E6-502B-4D36-B0A6-FA691E6D10CF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{FD3E097F-343A-4A45-8691-65EFE4B00B24}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3B83C4C0-6701-406E-8BF0-F6345D42719F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{1CA57D85-41C6-4705-8CBB-1A476B699976}", nodeBrowseMenuBar.Id);
            tree.AddNewMenuBarButton("{13C3C59D-60D3-4669-A63D-C889193829DD}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{76818701-63DA-44DC-A851-5CB5A40B2791}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B2AB67C2-9E44-4D9B-B60D-215B540E7A06}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A060D301-7DC1-4339-A271-F806980857EE}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{2753BF67-29C7-4531-B127-855E3B36C6F3}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DA6957ED-071E-44E6-A2E4-B684E9350978}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{59153003-24AC-4F03-94E6-BCFA0283FFCF}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{195A3582-3481-457A-AABC-E6FCA8AC4C15}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}