using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWebApi
{
    public class ContactTitleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactTitleMenu() : base("{D756CEE2-AE67-48BD-88A7-FFE855A8E590}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{ADCFFDE3-E33B-4BE8-9B9C-B040617A332E}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{9E71C0F1-70A3-494B-A871-FFE69100BBB3}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{470B79CB-242D-4104-AF97-6416283CBCA8}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{194C54FA-A4AC-4CD9-9B23-16BB87B0B214}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{16D7F840-B67F-497B-804B-F806B413F806}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{48FAF20E-04C5-4A6B-ABAA-21F2A30395E1}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}