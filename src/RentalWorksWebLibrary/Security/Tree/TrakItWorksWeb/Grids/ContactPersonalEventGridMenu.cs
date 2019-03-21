using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
  public class ContactPersonalEventGridMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public ContactPersonalEventGridMenu() : base("{EBEE1B5E-727D-4262-B045-906EC349A259}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      var nodeGridMenuBar = tree.AddMenuBar("{CC4CD23B-AAA4-4868-83B8-7E6A0EB53009}", MODULEID);
        var nodeGridSubMenu = tree.AddSubMenu("{1C4FC80D-9A50-4A3E-A78B-D127044A235E}", nodeGridMenuBar.Id);
        var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{FED074A6-0086-44F9-90CE-437368B31339}", nodeGridSubMenu.Id);
        tree.AddDownloadExcelSubMenuItem("{AF2A6D03-615D-4BF2-811E-04D99F6EC463}", nodeBrowseOptions.Id);
        tree.AddNewMenuBarButton("{0735E7AA-5EF8-4829-96C9-DC939A7A99DD}", nodeGridMenuBar.Id);
        tree.AddEditMenuBarButton("{6CF2C667-104B-4880-9F52-202B3126EA05}", nodeGridMenuBar.Id);
        tree.AddDeleteMenuBarButton("{09D80ABB-E6C1-4876-B215-9EECB1763C10}", nodeGridMenuBar.Id);
    }
    //---------------------------------------------------------------------------------------------
  }
}