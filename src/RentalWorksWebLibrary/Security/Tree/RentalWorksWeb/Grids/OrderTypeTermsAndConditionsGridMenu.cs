using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderTypeTermsAndConditionsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeTermsAndConditionsGridMenu() : base("{CD65AB0D-A92D-4CA9-9EB3-1F789BC51717}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {

            var nodeGridMenuBar = tree.AddMenuBar("{486E0C1A-5C27-4A19-ACA3-6734154ED6F9}", MODULEID);
            tree.AddEditMenuBarButton("{A21ED764-DBD4-48FF-B4BB-02DB7B1C5E08}", nodeGridMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}