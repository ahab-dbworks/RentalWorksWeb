using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.User
{
    public class UserLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        UserRecord user = new UserRecord();
        WebUserRecord webUser = new WebUserRecord();
        UserLoader userLoader = new UserLoader();
        UserBrowseLoader userBrowseLoader = new UserBrowseLoader();
        public UserLogic()
        {
            dataRecords.Add(user);
            dataRecords.Add(webUser);
            dataLoader = userLoader;
            browseLoader = userBrowseLoader;

            user.AfterSave += AfterSaveUser;
            //webUser.AfterSave += AfterSaveWebUser;

        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserId { get { return user.UserId; } set { user.UserId = value; webUser.UserId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string ContactId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DealId { get; set; }
        public string Name { get { return user.Name; } set { user.Name = value; } }
        public string LoginName { get { return user.LoginName; } set { user.LoginName = value; } }
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string FullName { get; set; }

        public string FirstName { get { return user.FirstName; } set { user.FirstName = value; } }
        public string MiddleInitial { get { return user.MiddleInitial; } set { user.MiddleInitial = value; } }
        public string LastName { get { return user.LastName; } set { user.LastName = value; } }
        //public string LoginName { get { return user.LoginName; } set { user.LoginName = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Password { get { return "?????????"; }  set { user.Password = value; } }
        public string GroupId { get { return user.GroupId; } set { user.GroupId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GroupName { get; set; }
        public string ScheduleColor { get { return user.ScheduleColor; } set { user.ScheduleColor = value; } }
        public string UserTitleId { get { return user.UserTitleId; } set { user.UserTitleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserTitle { get; set; }
        public string Email { get { return user.Email; } set { user.Email = value; } }
        public string OfficeLocationId { get { return user.OfficeLocationId; } set { user.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string WarehouseId { get { return user.WarehouseId; } set { user.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public string Address1 { get { return user.Address1; } set { user.Address1 = value; } }
        public string Address2 { get { return user.Address2; } set { user.Address2 = value; } }
        public string City { get { return user.City; } set { user.City = value; } }
        public string State { get { return user.State; } set { user.State = value; } }
        public string ZipCode { get { return user.ZipCode; } set { user.ZipCode = value; } }
        public string CountryId { get { return user.CountryId; } set { user.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string OfficePhone { get { return user.OfficePhone; } set { user.OfficePhone = value; } }
        public string OfficeExtension { get { return user.OfficeExtension; } set { user.OfficeExtension = value; } }
        public string Fax { get { return user.Fax; } set { user.Fax = value; } }
        public string DirectPhone { get { return user.DirectPhone; } set { user.DirectPhone = value; } }
        public string Pager { get { return user.Pager; } set { user.Pager = value; } }
        public string PagerPin { get { return user.PagerPin; } set { user.PagerPin = value; } }
        public string Cellular { get { return user.Cellular; } set { user.Cellular = value; } }
        public string HomePhone { get { return user.HomePhone; } set { user.HomePhone = value; } }

        public string DefaultDepartmentType { get { return user.DefaultDepartmentType; } set { user.DefaultDepartmentType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryDepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PrimaryDepartment { get; set; }
        public string RentalDepartmentId { get { return user.RentalDepartmentId; } set { user.RentalDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalDepartment { get; set; }
        public string SalesDepartmentId { get { return user.SalesDepartmentId; } set { user.SalesDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesDepartment { get; set; }
        public string PartsDepartmentId { get { return user.PartsDepartmentId; } set { user.PartsDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PartsDepartment { get; set; }
        public string MiscDepartmentId { get { return user.MiscDepartmentId; } set { user.MiscDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscDepartment { get; set; }
        public string LaborDepartmentId { get { return user.LaborDepartmentId; } set { user.LaborDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborDepartment { get; set; }
        public string FacilityDepartmentId { get { return user.FacilityDepartmentId; } set { user.FacilityDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityDepartment { get; set; }
        public string TransportationDepartmentId { get { return user.TransportationDepartmentId; } set { user.TransportationDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransportationDepartment { get; set; }
        public string RentalInventoryTypeId { get { return user.RentalInventoryTypeId; } set { user.RentalInventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalInventoryType { get; set; }
        public string SalesInventoryTypeId { get { return user.SalesInventoryTypeId; } set { user.SalesInventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesInventoryType { get; set; }
        public string PartsInventoryTypeId { get { return user.PartsInventoryTypeId; } set { user.PartsInventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PartsInventoryType { get; set; }
        public string MiscTypeId { get { return user.MiscTypeId; } set { user.MiscTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscType { get; set; }
        public string LaborTypeId { get { return user.LaborTypeId; } set { user.LaborTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborType { get; set; }
        public string FacilityTypeId { get { return user.FacilityTypeId; } set { user.FacilityTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }
        public string TransportationTypeId { get { return user.TransportationTypeId; } set { user.TransportationTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransportationType { get; set; }
        public bool? NoMiscellaneousOnQuotes { get { return user.NoMiscellaneousOnQuotes; } set { user.NoMiscellaneousOnQuotes = value; } }
        public bool? NoMiscellaneousOnOrders { get { return user.NoMiscellaneousOnOrders; } set { user.NoMiscellaneousOnOrders = value; } }
        public bool? NoMiscellaneousOnPurchaseOrders { get { return user.NoMiscellaneousOnPurchaseOrders; } set { user.NoMiscellaneousOnPurchaseOrders = value; } }
        public bool? LimitDaysPerWeek { get { return user.LimitDaysPerWeek; } set { user.LimitDaysPerWeek = value; } }
        public decimal? MinimumDaysPerWeek { get { return user.MinimumDaysPerWeek; } set { user.MinimumDaysPerWeek = value; } }
        public bool? AllowCreditLimitOverride { get { return user.AllowCreditLimitOverride; } set { user.AllowCreditLimitOverride = value; } }
        //public bool? CumulativeDiscountOverride { get { return user.CumulativeDiscountOverride; } set { user.CumulativeDiscountOverride = value; } }
        //public bool? LimitAllowCumulativeDiscount { get { return user.LimitAllowCumulativeDiscount; } set { user.LimitAllowCumulativeDiscount = value; } }
        //public decimal? MaximumCumulativeDiscount { get { return user.MaximumCumulativeDiscount; } set { user.MaximumCumulativeDiscount = value; } }
        public bool? LimitDiscount { get { return user.LimitDiscount; } set { user.LimitDiscount = value; } }
        public decimal? MaximumDiscount { get { return user.MaximumDiscount; } set { user.MaximumDiscount = value; } }
        public bool? LimitSubDiscount { get { return user.LimitSubDiscount; } set { user.LimitSubDiscount = value; } }
        public decimal? MaximumSubDiscount { get { return user.MaximumSubDiscount; } set { user.MaximumSubDiscount = value; } }
        public string DiscountRule { get { return user.DiscountRule; } set { user.DiscountRule = value; } }
        public bool? StagingAllowIncreaseDecreaseOrderQuantity { get { return user.StagingAllowIncreaseDecreaseOrderQuantity; } set { user.StagingAllowIncreaseDecreaseOrderQuantity = value; } }
        public bool? AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes { get { return user.AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes; } set { user.AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes = value; } }
        public bool? AllowContractIfDealRequiresPOAndOrderHasPendingPO { get { return user.AllowContractIfDealRequiresPOAndOrderHasPendingPO; } set { user.AllowContractIfDealRequiresPOAndOrderHasPendingPO = value; } }
        public bool? AllowContractIfPendingItemsExist { get { return user.AllowContractIfPendingItemsExist; } set { user.AllowContractIfPendingItemsExist = value; } }
        public bool? AllowContractIfCustomerDealDoesNotHaveApprovedCredit { get { return user.AllowContractIfCustomerDealDoesNotHaveApprovedCredit; } set { user.AllowContractIfCustomerDealDoesNotHaveApprovedCredit = value; } }
        public bool? AllowContractIfCustomerDealIsOverTheirCreditLimit { get { return user.AllowContractIfCustomerDealIsOverTheirCreditLimit; } set { user.AllowContractIfCustomerDealIsOverTheirCreditLimit = value; } }
        public bool? AllowContractIfCustomerDealInsuranceCoverageIsLess { get { return user.AllowContractIfCustomerDealInsuranceCoverageIsLess; } set { user.AllowContractIfCustomerDealInsuranceCoverageIsLess = value; } }
        public bool? AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate { get { return user.AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate; } set { user.AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate = value; } }
        public bool? AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate { get { return user.AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate; } set { user.AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate = value; } }
        public bool? AllowReceiveSubsWhenPositiveConflictExists { get { return user.AllowReceiveSubsWhenPositiveConflictExists; } set { user.AllowReceiveSubsWhenPositiveConflictExists = value; } }
        public bool? AllowStagingOfUnreservedConsignedItems { get { return user.AllowStagingOfUnreservedConsignedItems; } set { user.AllowStagingOfUnreservedConsignedItems = value; } }
        public bool? AllowStagingOfUnapprovedItems { get { return user.AllowStagingOfUnapprovedItems; } set { user.AllowStagingOfUnapprovedItems = value; } }
        public bool? AllowSubstitutesAtStaging { get { return user.AllowSubstitutesAtStaging; } set { user.AllowSubstitutesAtStaging = value; } }
        public bool? DeleteOriginalOnSubstitution { get { return user.DeleteOriginalOnSubstitution; } set { user.DeleteOriginalOnSubstitution = value; } }
        public bool? AllowCancelContract { get { return user.AllowCancelContract; } set { user.AllowCancelContract = value; } }
        public bool? QuikActivityAllowPrintDollarAmounts { get { return user.QuikActivityAllowPrintDollarAmounts; } set { user.QuikActivityAllowPrintDollarAmounts = value; } }
        public bool? QuikScanAllowCreateContract { get { return user.QuikScanAllowCreateContract; } set { user.QuikScanAllowCreateContract = value; } }
        public bool? QuikScanAllowApplyAll { get { return user.QuikScanAllowApplyAll; } set { user.QuikScanAllowApplyAll = value; } }
        public bool? AllowCrossICodeExchange { get { return user.AllowCrossICodeExchange; } set { user.AllowCrossICodeExchange = value; } }
        public bool? AllowCrossICodePendingExchange { get { return user.AllowCrossICodePendingExchange; } set { user.AllowCrossICodePendingExchange = value; } }
        public bool? AllowChangeAvailabilityPriority { get { return user.AllowChangeAvailabilityPriority; } set { user.AllowChangeAvailabilityPriority = value; } }
        public bool? UserMustChangePassword { get { return user.UserMustChangePassword; } set { user.UserMustChangePassword = value; } }
        public bool? PasswordExpires { get { return user.PasswordExpires; } set { user.PasswordExpires = value; } }
        public int? PasswordExpireDays { get { return user.PasswordExpireDays; } set { user.PasswordExpireDays = value; } }
        public string PasswordUpdatedDateTime { get { return user.PasswordUpdatedDateTime; } set { user.PasswordUpdatedDateTime = value; } }
        public bool? AccountLocked { get { return user.AccountLocked; } set { user.AccountLocked = value; } }
        public string Memo { get { return user.Memo; } set { user.Memo = value; } }
        public bool? Inactive { get { return user.Inactive; } set { user.Inactive = value; } }
        public string DateStamp { get { return user.DateStamp; } set { user.DateStamp = value; } }

        // WebUserRecord
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }
        //public bool? WebAccess { get { return webUser.WebAccess; } set { webUser.WebAccess = value; } }
        //public bool? LockAccount { get { return webUser.LockAccount; } set { webUser.LockAccount = value; } }
        //public string WebPassword { get { return webUser.WebPassword; } set { webUser.WebPassword = value; } }
        //public bool? ExpirePassword { get { return webUser.ExpirePassword; } set { webUser.ExpirePassword = value; } }
        //public int? ExpireDays { get { return webUser.ExpireDays; } set { webUser.ExpireDays = value; } }
        //public bool? ChangePasswordAtNextLogin { get { return webUser.ChangePasswordAtNextLogin; } set { webUser.ChangePasswordAtNextLogin = value; } }
        //public string PasswordLastUpdated { get { return webUser.PasswordLastUpdated; } set { webUser.PasswordLastUpdated = value; } }

        //------------------------------------------------------------------------------------ 
        private void AfterSaveUser(object sender, AfterSaveEventArgs e)
        {
            if (e.SavePerformed)
            {
                if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert) 
                {
                    this.webUser.UserId = UserId;
                }
                else if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) 
                {
                    UserLogic l2 = new UserLogic();
                    l2.SetDependencies(AppConfig, UserSession);
                    object[] pk = GetPrimaryKeys();
                    bool b = l2.LoadAsync<UserLogic>(pk).Result;
                    WebUserId = l2.WebUserId;
                }
            }
        }
        //------------------------------------------------------------------------------------   
    }
}