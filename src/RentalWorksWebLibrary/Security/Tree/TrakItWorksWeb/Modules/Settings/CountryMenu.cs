using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class CountryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CountryMenu() : base("{31F55A90-8EA2-418F-93E6-18AD5CAE8366}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse        = tree.AddBrowse ("{5E26731E-23D9-43DE-B512-88662125164F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{76AB6477-339E-41D9-A703-273A55F98BD7}", nodeBrowse.Id);
            //var nodeBrowseSubMenu = tree.AddSubMenu("{481DF759-51BB-48AE-9641-08294AB6B39B}", nodeBrowseMenuBar.Id);
                  tree.AddNewMenuBarButton     ("{6CE16DBD-C7C5-40FA-9B36-A3EA8C895888}", nodeBrowseMenuBar.Id);
                  tree.AddViewMenuBarButton    ("{0693E0B2-E137-4C8C-9C27-205E98D3391E}", nodeBrowseMenuBar.Id);
                  tree.AddEditMenuBarButton    ("{44CCB5A1-8898-4209-9D0E-D4F19306E499}", nodeBrowseMenuBar.Id);
                  tree.AddDeleteMenuBarButton  ("{542A2DE6-C1A8-429F-9904-7E74F167B2F0}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm        = tree.AddForm   ("{3E9FE4FA-170B-4B3A-AFCD-C5AD5FB75E65}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{DEF39E4E-70B8-4F8C-82F9-C906C60DDC3D}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{131177FF-6C28-4E73-9075-FE617207B2C9}", nodeFormMenuBar.Id);
                tree.AddSaveMenuBarButton        ("{1E3782A3-67EB-4145-AFC1-6E1754A0873B}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
