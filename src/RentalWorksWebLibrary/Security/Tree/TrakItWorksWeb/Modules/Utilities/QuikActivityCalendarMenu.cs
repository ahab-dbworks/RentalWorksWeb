using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Utilities
{
    public class QuikActivityCalendarMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuikActivityCalendarMenu() : base("{FB114A8F-1675-4C7C-BC9C-A4C005A405D7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{8599A5FA-BC38-42B4-942C-676E66B77114}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{7CE38E3C-6E6C-4A46-9067-B01ED1323504}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}