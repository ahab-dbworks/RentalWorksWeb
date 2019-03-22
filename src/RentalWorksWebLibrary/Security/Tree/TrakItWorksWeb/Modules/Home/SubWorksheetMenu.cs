using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules
{
    public class SubWorksheetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SubWorksheetMenu() : base("{2227B6C3-587D-48B1-98B6-B9125E0E4D9D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{06105DD6-0BDD-4A62-A2A8-45F39AD5B813}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{845C5A1B-D3CD-4EC6-8EF5-B606DBD58B58}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}