using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class FillContainerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FillContainerMenu() : base("{0F1050FB-48DF-41D7-A969-37300B81B7B5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{D91C7DC6-9B66-438C-960F-688CC3CAC1B2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{48401D5E-F44C-4794-8D2C-26FE4B239F61}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}