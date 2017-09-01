using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class DealClassificationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealClassificationMenu() : base("{3CABAC95-7BEC-433F-9D32-11E76AAE229A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{16457FA2-FB52-4FA9-A94D-3DAB697D6B21}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{EC95C419-BD71-46CB-8BF6-17CB1164552C}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{73EEDAAC-6133-476A-837B-FCDAED43BDF7}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{3BB08EBF-52CE-4F9C-980D-F162570018CC}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{941CE445-FC04-44ED-A041-F9705334AE9A}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{71765BAC-A405-4B5C-8C0C-1EC6D5E7598F}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}