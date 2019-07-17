using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WidgetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WidgetMenu() : base("{0CAF7264-D1FB-46EC-96B9-68D242985812}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{78B33436-AFC4-41F7-BE84-23F5946247B8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3EF8FF9F-9338-471E-AFE3-024A805F772E}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6149AA38-58C0-4123-AC9D-88D20D88E7EC}", nodeBrowseMenuBar.Id);
            //tree.AddNewMenuBarButton("{656CADA6-0412-4F61-BB73-3825F18E7946}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{06F6E137-68C2-42FE-9231-18809C694CE2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{91741041-B6F5-4F89-9194-A2DCF3F33024}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{4A06B99F-D732-4D16-985E-0AD8A96B19A3}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D29B092C-07A3-40A5-BBB9-2E7438EBEBF2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A6C84689-BA38-4D6B-A381-41CA80487995}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{42B3ECCC-F7C0-4BE9-A2BA-03EB7EDB181C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{70C55D81-44AA-494F-B504-3B060D2C3772}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}