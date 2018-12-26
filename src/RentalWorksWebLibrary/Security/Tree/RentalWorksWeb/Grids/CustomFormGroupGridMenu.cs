using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomFormGroupGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomFormGroupGridMenu() : base("{2D12FA3B-2BC3-4838-9B79-05303F7D3120}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{36F5D6A6-0AD5-4825-8598-A3CB4CAB5206}", MODULEID);
                tree.AddNewMenuBarButton("{4F999468-11FF-4F6A-8B29-EA1DED43EA98}", nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{14E250F4-5282-4AC0-A25B-D056F44525E0}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{5B2ABB95-D40A-4BBE-84FC-091F3E693A14}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}