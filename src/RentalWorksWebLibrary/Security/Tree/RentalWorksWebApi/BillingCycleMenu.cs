using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class BillingCycleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingCycleMenu() : base("{4CE12A3E-1831-4D35-95AF-01F1BF0A5214}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{652BA08D-4136-42FC-84C5-FE89898E3517}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{6960FFCC-430A-4760-88C6-0F07FCFCF851}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{3C7B524C-E097-421A-BDAC-2571BE6C4753}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{42164CF5-712C-466C-8786-6A30D5E54C3B}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{08053E4B-77F9-4F4E-B99C-58C347F0E38B}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{ECE5B5BA-D7B2-4324-A28C-1DC75BC86B85}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}