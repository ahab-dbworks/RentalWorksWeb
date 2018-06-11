using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
  public class ContactPersonalEventGridMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public ContactPersonalEventGridMenu() : base("{96B55326-31BB-46C1-BD11-DE1201A8CB51}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      var nodeGridMenuBar = tree.AddMenuBar("{E85E1E55-B68F-4903-9355-B2CA82ECBCE7}", MODULEID);
        tree.AddNewMenuBarButton("{C2B11AF9-81A4-485C-B33B-AAFD482035B1}", nodeGridMenuBar.Id);
        tree.AddEditMenuBarButton("{96975DC0-9EE0-42D6-8043-4529B8491359}", nodeGridMenuBar.Id);
        tree.AddDeleteMenuBarButton("{3FF117ED-F5AA-4E4C-B7B1-3529F7C41A0D}", nodeGridMenuBar.Id);
    }
    //---------------------------------------------------------------------------------------------
  }
}