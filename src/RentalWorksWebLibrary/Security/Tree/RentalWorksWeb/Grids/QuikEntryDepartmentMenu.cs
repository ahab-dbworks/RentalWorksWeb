using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class QuikEntryDepartmentMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuikEntryDepartmentMenu() : base("{2AC10F3D-FC50-4454-87C2-54ABBCCD08AB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{38E499D7-6C07-426F-A651-0BA8FA47BE9E}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{FA7E24D0-DCAE-4401-8FDC-96CEF201E781}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{94172C5D-4E80-462C-B561-D68165D8AA11}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{43668D44-26E3-46AE-87D8-5897EDDB7D59}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{9F11128B-FC3B-4A27-8153-1D6AFF3F1069}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{8E45D571-8281-4966-8081-D85A6DA4C00B}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{AD359090-F70E-43FD-8843-25B9120D0B8F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}