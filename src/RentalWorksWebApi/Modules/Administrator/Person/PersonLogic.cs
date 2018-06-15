using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.Person
{
    public class PersonLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PersonLoader personLoader = new PersonLoader();
        PersonBrowseLoader personBrowseLoader = new PersonBrowseLoader();
        public PersonLogic()
        {
            dataLoader = personLoader;
            browseLoader = personBrowseLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string ContactId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DealId { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string FullName { get; set; }

        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        //public string LoginName { get ; set ;}
        [FwBusinessLogicField(isReadOnly: true)]
        public string Password { get; set; }
        public string GroupId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GroupName { get; set; }
        public string ScheduleColor { get; set; }
        public string UserTitleId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserTitle { get; set; }
        public string Email { get; set; }
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CountryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeExtension { get; set; }
        public string Fax { get; set; }
        public string DirectPhone { get; set; }
        public string Pager { get; set; }
        public string PagerPin { get; set; }
        public string Cellular { get; set; }
        public string HomePhone { get; set; }

        public string DefaultDepartmentType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryDepartment { get; set; }
        public string RentalDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalDepartment { get; set; }
        public string SalesDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesDepartment { get; set; }
        public string PartsDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PartsDepartment { get; set; }
        public string MiscDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscDepartment { get; set; }
        public string LaborDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborDepartment { get; set; }
        public string FacilityDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityDepartment { get; set; }
        public string TransportationDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransportationDepartment { get; set; }
        public string RentalInventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalInventoryType { get; set; }
        public string SalesInventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesInventoryType { get; set; }
        public string PartsInventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PartsInventoryType { get; set; }
        public string MiscTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscType { get; set; }
        public string LaborTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }
        public string FacilityTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }
        public string TransportationTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransportationType { get; set; }
        public bool? NoMiscellaneousOnQuotes { get; set; }
        public bool? NoMiscellaneousOnOrders { get; set; }
        public bool? NoMiscellaneousOnPurchaseOrders { get; set; }
        public bool? LimitDaysPerWeek { get; set; }
        public decimal? MinimumDaysPerWeek { get; set; }
        public bool? AllowCreditLimitOverride { get; set; }
        public bool? CumulativeDiscountOverride { get; set; }
        public bool? LimitAllowCumulativeDiscount { get; set; }
        public decimal? MaximumCumulativeDiscount { get; set; }
        public bool? LimitDiscount { get; set; }
        public decimal? MaximumDiscount { get; set; }
        public bool? LimitSubDiscount { get; set; }
        public decimal? MaximumSubDiscount { get; set; }
        public string DiscountRule { get; set; }
        public bool? StagingAllowIncreaseDecreaseOrderQuantity { get; set; }
        public bool? AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes { get; set; }
        public bool? AllowContractIfDealRequiresPOAndOrderHasPendingPO { get; set; }
        public bool? AllowContractIfPendingItemsExist { get; set; }
        public bool? AllowContractIfCustomerDealDoesNotHaveApprovedCredit { get; set; }
        public bool? AllowContractIfCustomerDealIsOverTheirCreditLimit { get; set; }
        public bool? AllowContractIfCustomerDealInsuranceCoverageIsLess { get; set; }
        public bool? AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate { get; set; }
        public bool? AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate { get; set; }
        public bool? AllowReceiveSubsWhenPositiveConflictExists { get; set; }
        public bool? AllowStagingOfUnreservedConsignedItems { get; set; }
        public bool? AllowStagingOfUnapprovedItems { get; set; }
        public bool? AllowSubstitutesAtStaging { get; set; }
        public bool? DeleteOriginalOnSubstitution { get; set; }
        public bool? AllowCancelContract { get; set; }
        public bool? QuikActivityAllowPrintDollarAmounts { get; set; }
        public bool? QuikScanAllowCreateContract { get; set; }
        public bool? QuikScanAllowApplyAll { get; set; }
        public bool? AllowCrossICodeExchange { get; set; }
        public bool? AllowCrossICodePendingExchange { get; set; }
        public bool? AllowChangeAvailabilityPriority { get; set; }
        public bool? UserMustChangePassword { get; set; }
        public bool? PasswordExpires { get; set; }
        public int? PasswordExpireDays { get; set; }
        public string PasswordUpdatedDateTime { get; set; }
        public bool? AccountLocked { get; set; }
        public string Memo { get; set; }
        public bool? Inactive { get; set; }
        public string DateStamp { get; set; }

        // WebUserRecord
        public string WebUserId { get; set; }
        //public bool? WebAccess { get ; set ;}
        //public bool? LockAccount { get ; set ;}
        //public string WebPassword { get ; set ;}
        //public bool? ExpirePassword { get ; set ;}
        //public int? ExpireDays { get ; set ;}
        //public bool? ChangePasswordAtNextLogin { get ; set ;}
        //public string PasswordLastUpdated { get ; set ;}

        //------------------------------------------------------------------------------------ 
    }
}