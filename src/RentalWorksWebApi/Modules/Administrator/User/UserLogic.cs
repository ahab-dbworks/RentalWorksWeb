using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.Administrator.User
{
    [FwLogic(Id: "0ccx5woMungnC")]
    public class UserLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        UserRecord user = new UserRecord();
        WebUserRecord webUser = new WebUserRecord();
        UserLoader userLoader = new UserLoader();
        private bool? passwordChanged = false;

        public UserLogic()
        {
            dataRecords.Add(user);
            dataRecords.Add(webUser);
            dataLoader = userLoader;

            BeforeSave += OnBeforeSave;

            user.AfterSave += AfterSaveUser;
            //webUser.AfterSave += AfterSaveWebUser;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "DyZgfLOzppd7z", IsPrimaryKey: true)]
        public string UserId { get { return user.UserId; } set { user.UserId = value; webUser.UserId = value; } }

        //[FwLogicProperty(Id:"3oSCd6YGv")]
        //public string ContactId { get; set; }

        //[FwLogicProperty(Id:"cIl4ij0H4Q")]
        //public string DealId { get; set; }

        [FwLogicProperty(Id: "SrH4IAs1rP")]
        public string Name { get { return user.Name; } set { user.Name = value; } }

        [FwLogicProperty(Id: "pxrxIuxzKZ")]
        public string LoginName { get { return user.LoginName; } set { user.LoginName = value; } }

        [FwLogicProperty(Id: "80mfrQDizPEHC", IsRecordTitle: true, IsReadOnly: true)]
        public string FullName { get; set; }

        [FwLogicProperty(Id: "HKSzI7UwFR")]
        public string FirstName { get { return user.FirstName; } set { user.FirstName = value; } }

        [FwLogicProperty(Id: "VozxsIzXnp")]
        public string MiddleInitial { get { return user.MiddleInitial; } set { user.MiddleInitial = value; } }

        [FwLogicProperty(Id: "P0SzZ2rdKR")]
        public string LastName { get { return user.LastName; } set { user.LastName = value; } }

        [FwLogicProperty(Id: "zxsHyG4ois")]
        public string Password

        {
            get { return "?????????"; }
            set
            {
                if (value != null)
                {
                    passwordChanged = true;
                }
                user.Password = value;
            }
        }
        [FwLogicProperty(Id: "FXQ5AbMN8T")]
        public bool? PasswordChanged { get { return passwordChanged; } }

        [FwLogicProperty(Id: "eRVx8rBVyIpoI")]
        public string BarCode { get { return user.BarCode; } set { user.BarCode = value; } }

        [FwLogicProperty(Id: "o6LbAuCaLD")]
        public string GroupId { get { return user.GroupId; } set { user.GroupId = value; } }

        [FwLogicProperty(Id: "wTjCyiOdYi0Eo", IsReadOnly: true)]
        public string GroupName { get; set; }

        [FwLogicProperty(Id: "s9MZxWodEP")]
        public string ScheduleColor { get { return user.ScheduleColor; } set { user.ScheduleColor = value; } }

        [FwLogicProperty(Id: "uf1Qmc5HEm")]
        public string UserTitleId { get { return user.UserTitleId; } set { user.UserTitleId = value; } }

        [FwLogicProperty(Id: "uhIvZet9G8O8h", IsReadOnly: true)]
        public string UserTitle { get; set; }

        [FwLogicProperty(Id: "4F1oGL3ZCcc")]
        public string Email { get { return user.Email; } set { user.Email = value; } }

        [FwLogicProperty(Id: "qDroQSIPNqj")]
        public string OfficeLocationId { get { return user.OfficeLocationId; } set { user.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "xNORtbDISsKSx", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "PDbniHsprqo")]
        public string WarehouseId { get { return user.WarehouseId; } set { user.WarehouseId = value; } }

        [FwLogicProperty(Id: "rEBCBhBiVAz3T", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "SLtzevor29I")]
        public string Address1 { get { return user.Address1; } set { user.Address1 = value; } }

        [FwLogicProperty(Id: "DPVDfHHgtii")]
        public string Address2 { get { return user.Address2; } set { user.Address2 = value; } }

        [FwLogicProperty(Id: "R4NT8wvdIoQ")]
        public string City { get { return user.City; } set { user.City = value; } }

        [FwLogicProperty(Id: "z1tlbPZItnH")]
        public string State { get { return user.State; } set { user.State = value; } }

        [FwLogicProperty(Id: "ERkCSzUm5js")]
        public string ZipCode { get { return user.ZipCode; } set { user.ZipCode = value; } }

        [FwLogicProperty(Id: "HiOJMCieJWQ")]
        public string CountryId { get { return user.CountryId; } set { user.CountryId = value; } }

        [FwLogicProperty(Id: "sbag6szuIvBy4", IsReadOnly: true)]
        public string Country { get; set; }

        [FwLogicProperty(Id: "hJI8czaqU5M")]
        public string OfficePhone { get { return user.OfficePhone; } set { user.OfficePhone = value; } }

        [FwLogicProperty(Id: "g5fqNTix6Lx")]
        public string OfficeExtension { get { return user.OfficeExtension; } set { user.OfficeExtension = value; } }

        [FwLogicProperty(Id: "EwiJozlT4WI")]
        public string Fax { get { return user.Fax; } set { user.Fax = value; } }

        [FwLogicProperty(Id: "R0QPbruWlTD")]
        public string DirectPhone { get { return user.DirectPhone; } set { user.DirectPhone = value; } }

        [FwLogicProperty(Id: "E4bHZNnF106")]
        public string Pager { get { return user.Pager; } set { user.Pager = value; } }

        [FwLogicProperty(Id: "GZLGYHJgFNI")]
        public string PagerPin { get { return user.PagerPin; } set { user.PagerPin = value; } }

        [FwLogicProperty(Id: "LoEwO2QPobk")]
        public string Cellular { get { return user.Cellular; } set { user.Cellular = value; } }

        [FwLogicProperty(Id: "LBggwGSeZVK")]
        public string HomePhone { get { return user.HomePhone; } set { user.HomePhone = value; } }


        [FwLogicProperty(Id: "y6IxFCfJupi")]
        public string DefaultDepartmentType { get { return user.DefaultDepartmentType; } set { user.DefaultDepartmentType = value; } }

        [FwLogicProperty(Id: "NNYgc7mP0hH0y", IsReadOnly: true)]
        public string PrimaryDepartmentId { get; set; }

        [FwLogicProperty(Id: "NNYgc7mP0hH0y", IsReadOnly: true)]
        public string PrimaryDepartment { get; set; }

        [FwLogicProperty(Id: "rI1tcrjw1Jt")]
        public string RentalDepartmentId { get { return user.RentalDepartmentId; } set { user.RentalDepartmentId = value; } }

        [FwLogicProperty(Id: "NIcxsxA3fEtmx", IsReadOnly: true)]
        public string RentalDepartment { get; set; }

        [FwLogicProperty(Id: "7kkhbeKyVm0")]
        public string SalesDepartmentId { get { return user.SalesDepartmentId; } set { user.SalesDepartmentId = value; } }

        [FwLogicProperty(Id: "RSQk4sW3ieGsy", IsReadOnly: true)]
        public string SalesDepartment { get; set; }

        [FwLogicProperty(Id: "hjoy5UVJyrC")]
        public string PartsDepartmentId { get { return user.PartsDepartmentId; } set { user.PartsDepartmentId = value; } }

        [FwLogicProperty(Id: "egvSgeZbZTCIL", IsReadOnly: true)]
        public string PartsDepartment { get; set; }

        [FwLogicProperty(Id: "y7xCbrKb7Zr")]
        public string MiscDepartmentId { get { return user.MiscDepartmentId; } set { user.MiscDepartmentId = value; } }

        [FwLogicProperty(Id: "r0cGcuSgudc47", IsReadOnly: true)]
        public string MiscDepartment { get; set; }

        [FwLogicProperty(Id: "2YvgIXgndBw")]
        public string LaborDepartmentId { get { return user.LaborDepartmentId; } set { user.LaborDepartmentId = value; } }

        [FwLogicProperty(Id: "qq1lMtc9aOdaR", IsReadOnly: true)]
        public string LaborDepartment { get; set; }

        [FwLogicProperty(Id: "MoJZhLzSon0")]
        public string FacilityDepartmentId { get { return user.FacilityDepartmentId; } set { user.FacilityDepartmentId = value; } }

        [FwLogicProperty(Id: "ZxzGV0WEEVzo6", IsReadOnly: true)]
        public string FacilityDepartment { get; set; }

        [FwLogicProperty(Id: "Vj1WLgGlYdj")]
        public string TransportationDepartmentId { get { return user.TransportationDepartmentId; } set { user.TransportationDepartmentId = value; } }

        [FwLogicProperty(Id: "SsjidDoL9QlJT", IsReadOnly: true)]
        public string TransportationDepartment { get; set; }

        [FwLogicProperty(Id: "Lb7dKw0VamH")]
        public string RentalInventoryTypeId { get { return user.RentalInventoryTypeId; } set { user.RentalInventoryTypeId = value; } }

        [FwLogicProperty(Id: "MLuPKKPpeoSN7", IsReadOnly: true)]
        public string RentalInventoryType { get; set; }

        [FwLogicProperty(Id: "LK1R2dWCswS")]
        public string SalesInventoryTypeId { get { return user.SalesInventoryTypeId; } set { user.SalesInventoryTypeId = value; } }

        [FwLogicProperty(Id: "yuqR8BOxy12WZ", IsReadOnly: true)]
        public string SalesInventoryType { get; set; }

        [FwLogicProperty(Id: "4uuMBgkNhdu")]
        public string PartsInventoryTypeId { get { return user.PartsInventoryTypeId; } set { user.PartsInventoryTypeId = value; } }

        [FwLogicProperty(Id: "PHe0xncCKrILr", IsReadOnly: true)]
        public string PartsInventoryType { get; set; }

        [FwLogicProperty(Id: "gWhcEWs4rXF")]
        public string MiscTypeId { get { return user.MiscTypeId; } set { user.MiscTypeId = value; } }

        [FwLogicProperty(Id: "dC4jIIxBf4Zh8", IsReadOnly: true)]
        public string MiscType { get; set; }

        [FwLogicProperty(Id: "UGgMVOH1EPy")]
        public string LaborTypeId { get { return user.LaborTypeId; } set { user.LaborTypeId = value; } }

        [FwLogicProperty(Id: "CRDcdB0oh9eEc", IsReadOnly: true)]
        public string LaborType { get; set; }

        [FwLogicProperty(Id: "ITAgfqbtHcr")]
        public string FacilityTypeId { get { return user.FacilityTypeId; } set { user.FacilityTypeId = value; } }

        [FwLogicProperty(Id: "9FNzgsOZCc0we", IsReadOnly: true)]
        public string FacilityType { get; set; }

        [FwLogicProperty(Id: "bFpzVdfUDzT")]
        public string TransportationTypeId { get { return user.TransportationTypeId; } set { user.TransportationTypeId = value; } }

        [FwLogicProperty(Id: "yBJZCDNZKo9TF", IsReadOnly: true)]
        public string TransportationType { get; set; }

        [FwLogicProperty(Id: "8Gw5xb0Czc4")]
        public bool? NoMiscellaneousOnQuotes { get { return user.NoMiscellaneousOnQuotes; } set { user.NoMiscellaneousOnQuotes = value; } }

        [FwLogicProperty(Id: "50sXrrzTOfX")]
        public bool? NoMiscellaneousOnOrders { get { return user.NoMiscellaneousOnOrders; } set { user.NoMiscellaneousOnOrders = value; } }

        [FwLogicProperty(Id: "LMsvfM9JNdD")]
        public bool? NoMiscellaneousOnPurchaseOrders { get { return user.NoMiscellaneousOnPurchaseOrders; } set { user.NoMiscellaneousOnPurchaseOrders = value; } }

        [FwLogicProperty(Id: "cmLvlIuqaXB")]
        public bool? LimitDaysPerWeek { get { return user.LimitDaysPerWeek; } set { user.LimitDaysPerWeek = value; } }

        [FwLogicProperty(Id: "ZDgWerDKnZE")]
        public decimal? MinimumDaysPerWeek { get { return user.MinimumDaysPerWeek; } set { user.MinimumDaysPerWeek = value; } }

        [FwLogicProperty(Id: "Y3l18YiA9T6")]
        public bool? AllowCreditLimitOverride { get { return user.AllowCreditLimitOverride; } set { user.AllowCreditLimitOverride = value; } }

        //[FwLogicProperty(Id:"DRk8eJsAw9C")]
        //public bool? CumulativeDiscountOverride { get { return user.CumulativeDiscountOverride; } set { user.CumulativeDiscountOverride = value; } }

        //[FwLogicProperty(Id:"JPKBIVfbGuG")]
        //public bool? LimitAllowCumulativeDiscount { get { return user.LimitAllowCumulativeDiscount; } set { user.LimitAllowCumulativeDiscount = value; } }

        //[FwLogicProperty(Id:"8E8lhgkDbn2")]
        //public decimal? MaximumCumulativeDiscount { get { return user.MaximumCumulativeDiscount; } set { user.MaximumCumulativeDiscount = value; } }

        [FwLogicProperty(Id: "9A8BoZhtKNJ")]
        public bool? LimitDiscount { get { return user.LimitDiscount; } set { user.LimitDiscount = value; } }

        [FwLogicProperty(Id: "wGJfj9Cb9Sx")]
        public decimal? MaximumDiscount { get { return user.MaximumDiscount; } set { user.MaximumDiscount = value; } }

        [FwLogicProperty(Id: "UKwekU1BTUW")]
        public bool? LimitSubDiscount { get { return user.LimitSubDiscount; } set { user.LimitSubDiscount = value; } }

        [FwLogicProperty(Id: "nXtbOFkNAaj")]
        public decimal? MaximumSubDiscount { get { return user.MaximumSubDiscount; } set { user.MaximumSubDiscount = value; } }

        [FwLogicProperty(Id: "aklM1nHF3sv")]
        public string DiscountRule { get { return user.DiscountRule; } set { user.DiscountRule = value; } }

        [FwLogicProperty(Id: "KJDvmkeDhWb")]
        public bool? StagingAllowIncreaseDecreaseOrderQuantity { get { return user.StagingAllowIncreaseDecreaseOrderQuantity; } set { user.StagingAllowIncreaseDecreaseOrderQuantity = value; } }

        [FwLogicProperty(Id: "D5MU1srj2Ud")]
        public bool? AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes { get { return user.AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes; } set { user.AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes = value; } }

        [FwLogicProperty(Id: "jPEI9FafT3e")]
        public bool? AllowContractIfDealRequiresPOAndOrderHasPendingPO { get { return user.AllowContractIfDealRequiresPOAndOrderHasPendingPO; } set { user.AllowContractIfDealRequiresPOAndOrderHasPendingPO = value; } }

        [FwLogicProperty(Id: "TWv45xZDkKy")]
        public bool? AllowContractIfPendingItemsExist { get { return user.AllowContractIfPendingItemsExist; } set { user.AllowContractIfPendingItemsExist = value; } }

        [FwLogicProperty(Id: "Qby2ONj2kj1")]
        public bool? AllowContractIfCustomerDealDoesNotHaveApprovedCredit { get { return user.AllowContractIfCustomerDealDoesNotHaveApprovedCredit; } set { user.AllowContractIfCustomerDealDoesNotHaveApprovedCredit = value; } }

        [FwLogicProperty(Id: "sF8u7vCommX")]
        public bool? AllowContractIfCustomerDealIsOverTheirCreditLimit { get { return user.AllowContractIfCustomerDealIsOverTheirCreditLimit; } set { user.AllowContractIfCustomerDealIsOverTheirCreditLimit = value; } }

        [FwLogicProperty(Id: "G4OfwH2nuQT")]
        public bool? AllowContractIfCustomerDealInsuranceCoverageIsLess { get { return user.AllowContractIfCustomerDealInsuranceCoverageIsLess; } set { user.AllowContractIfCustomerDealInsuranceCoverageIsLess = value; } }

        [FwLogicProperty(Id: "1fJ1ZocILXg")]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate { get { return user.AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate; } set { user.AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate = value; } }

        [FwLogicProperty(Id: "Hc8Q4j6qOjv")]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate { get { return user.AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate; } set { user.AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate = value; } }

        [FwLogicProperty(Id: "PLrPwN2nddS")]
        public bool? AllowReceiveSubsWhenPositiveConflictExists { get { return user.AllowReceiveSubsWhenPositiveConflictExists; } set { user.AllowReceiveSubsWhenPositiveConflictExists = value; } }

        [FwLogicProperty(Id: "vBiMcCDh78e")]
        public bool? AllowStagingOfUnreservedConsignedItems { get { return user.AllowStagingOfUnreservedConsignedItems; } set { user.AllowStagingOfUnreservedConsignedItems = value; } }

        [FwLogicProperty(Id: "Io7PLh2luEV")]
        public bool? AllowStagingOfUnapprovedItems { get { return user.AllowStagingOfUnapprovedItems; } set { user.AllowStagingOfUnapprovedItems = value; } }

        [FwLogicProperty(Id: "5ee3l6v9QLV")]
        public bool? AllowSubstitutesAtStaging { get { return user.AllowSubstitutesAtStaging; } set { user.AllowSubstitutesAtStaging = value; } }

        [FwLogicProperty(Id: "g0AJR2wPAFj")]
        public bool? DeleteOriginalOnSubstitution { get { return user.DeleteOriginalOnSubstitution; } set { user.DeleteOriginalOnSubstitution = value; } }

        //[FwLogicProperty(Id: "2CCV8JrJbYr")]
        //public bool? AllowCancelContract { get { return user.AllowCancelContract; } set { user.AllowCancelContract = value; } }

        [FwLogicProperty(Id: "yDueJWdmkgy")]
        public bool? QuikActivityAllowPrintDollarAmounts { get { return user.QuikActivityAllowPrintDollarAmounts; } set { user.QuikActivityAllowPrintDollarAmounts = value; } }

        [FwLogicProperty(Id: "kyMBJ5BJ5EE")]
        public bool? QuikScanAllowCreateContract { get { return user.QuikScanAllowCreateContract; } set { user.QuikScanAllowCreateContract = value; } }

        [FwLogicProperty(Id: "iOzuAmf09UN")]
        public bool? QuikScanAllowApplyAll { get { return user.QuikScanAllowApplyAll; } set { user.QuikScanAllowApplyAll = value; } }

        [FwLogicProperty(Id: "FqTCeH6OreC")]
        public bool? AllowCrossICodeExchange { get { return user.AllowCrossICodeExchange; } set { user.AllowCrossICodeExchange = value; } }

        [FwLogicProperty(Id: "NrreU7aa49d")]
        public bool? AllowCrossICodePendingExchange { get { return user.AllowCrossICodePendingExchange; } set { user.AllowCrossICodePendingExchange = value; } }

        [FwLogicProperty(Id: "whubFjg5S2J")]
        public bool? AllowChangeAvailabilityPriority { get { return user.AllowChangeAvailabilityPriority; } set { user.AllowChangeAvailabilityPriority = value; } }

        [FwLogicProperty(Id: "19f2FulUESW")]
        public bool? UserMustChangePassword { get { return user.UserMustChangePassword; } set { user.UserMustChangePassword = value; } }

        [FwLogicProperty(Id: "rHhcpexvEpB")]
        public bool? PasswordExpires { get { return user.PasswordExpires; } set { user.PasswordExpires = value; } }

        [FwLogicProperty(Id: "VkiQ1h3F05I")]
        public int? PasswordExpireDays { get { return user.PasswordExpireDays; } set { user.PasswordExpireDays = value; } }

        [FwLogicProperty(Id: "3qp488RbrP5")]
        public string PasswordUpdatedDateTime { get { return user.PasswordUpdatedDateTime; } set { user.PasswordUpdatedDateTime = value; } }

        [FwLogicProperty(Id: "HeKpuuY50yW")]
        public bool? AccountLocked { get { return user.AccountLocked; } set { user.AccountLocked = value; } }

        [FwLogicProperty(Id: "y3JIRyhmBW6")]
        public string Memo { get { return user.Memo; } set { user.Memo = value; } }

        [FwLogicProperty(Id: "zgn7YIW3mpk20")]
        public bool? AllowCrossLocationEditAndDelete { get { return user.AllowCrossLocationEditAndDelete; } set { user.AllowCrossLocationEditAndDelete = value; } }

        [FwLogicProperty(Id: "ha24YF58zDM")]
        public bool? Inactive { get { return user.Inactive; } set { user.Inactive = value; } }

        [FwLogicProperty(Id: "HwPw3pfqmJV")]
        public string DateStamp { get { return user.DateStamp; } set { user.DateStamp = value; } }


        // WebUserRecord
        [FwLogicProperty(Id: "U6qaKCGiV36")]
        public string WebUserId { get { return webUser.WebUserId; } set { webUser.WebUserId = value; } }

        //temporary - can be removed once exporting direct to QBO
        [FwLogicProperty(Id:"OBcFFr8Zm7y")]
        public bool? WebAccess { get { return webUser.WebAccess; } set { webUser.WebAccess = value; } }

        //[FwLogicProperty(Id:"kuTxEDAKkfr")]
        //public bool? LockAccount { get { return webUser.LockAccount; } set { webUser.LockAccount = value; } }

        //[FwLogicProperty(Id:"6nNsdJJgnx4")]
        //public string WebPassword { get { return webUser.WebPassword; } set { webUser.WebPassword = value; } }

        //[FwLogicProperty(Id:"LmBDPz7i8CM")]
        //public bool? ExpirePassword { get { return webUser.ExpirePassword; } set { webUser.ExpirePassword = value; } }

        //[FwLogicProperty(Id:"cWcwsuXuPhI")]
        //public int? ExpireDays { get { return webUser.ExpireDays; } set { webUser.ExpireDays = value; } }

        //[FwLogicProperty(Id:"4Mi64CwWTOJ")]
        //public bool? ChangePasswordAtNextLogin { get { return webUser.ChangePasswordAtNextLogin; } set { webUser.ChangePasswordAtNextLogin = value; } }

        //[FwLogicProperty(Id:"n69lRcG4ix8")]
        //public string PasswordLastUpdated { get { return webUser.PasswordLastUpdated; } set { webUser.PasswordLastUpdated = value; } }

        [FwLogicProperty(Id: "sfdYwaSiBzgqS")]
        public bool? WebAdministrator { get { return webUser.WebAdministrator; } set { webUser.WebAdministrator = value; } }


        [FwLogicProperty(Id: "FXfH6IV8OL0HW")]
        public int? BrowseDefaultRows { get { return webUser.BrowseDefaultRows; } set { webUser.BrowseDefaultRows = value; } }
        [FwLogicProperty(Id: "dKsvF60zSz8KK")]
        public string ApplicationTheme { get { return webUser.ApplicationTheme; } set { webUser.ApplicationTheme = value; } }
        [FwLogicProperty(Id: "if3Cu5Qm5hkPh")]
        public string HomeMenuGuid { get { return webUser.HomeMenuGuid; } set { webUser.HomeMenuGuid = value; } }
        [FwLogicProperty(Id: "VOBXEZF5GOcy6")]
        public string HomeMenuPath { get { return webUser.HomeMenuPath; } set { webUser.HomeMenuPath = value; } }
        [FwLogicProperty(Id: "BAOVebjzaG5Eb")]
        public string SuccessSoundId { get { return webUser.SuccessSoundId; } set { webUser.SuccessSoundId = value; } }
        [FwLogicProperty(Id: "1RvfHBCyLNY6r", IsReadOnly: true)]
        public string SuccessSound { get; set; }
        [FwLogicProperty(Id: "1NCCcS6Uetdti", IsReadOnly: true)]
        public string SuccessBase64Sound { get; set; }
        [FwLogicProperty(Id: "39lZCXsIzQfoL")]
        public string ErrorSoundId { get { return webUser.ErrorSoundId; } set { webUser.ErrorSoundId = value; } }
        [FwLogicProperty(Id: "J2f8cPIy7zPv8", IsReadOnly: true)]
        public string ErrorSound { get; set; }
        [FwLogicProperty(Id: "NgqAUw6SWkmq4", IsReadOnly: true)]
        public string ErrorBase64Sound { get; set; }
        [FwLogicProperty(Id: "GLp6gEs03MmaP")]
        public string NotificationSoundId { get { return webUser.NotificationSoundId; } set { webUser.NotificationSoundId = value; } }
        [FwLogicProperty(Id: "6GJq6P73wLwHt", IsReadOnly: true)]
        public string NotificationSound { get; set; }
        [FwLogicProperty(Id: "jj9nN7EG0ANjG", IsReadOnly: true)]
        public string NotificationBase64Sound { get; set; }
        [FwLogicProperty(Id: "OJrOlZkYyD44C")]
        public int? FirstDayOfWeek { get { return webUser.FirstDayOfWeek; } set { webUser.FirstDayOfWeek = value; } }
        [FwLogicProperty(Id: "V1l821OvHhE4e")]
        public bool? SettingsNavigationMenuVisible { get { return webUser.SettingsNavigationMenuVisible; } set { webUser.SettingsNavigationMenuVisible = value; } }
        [FwLogicProperty(Id: "715OUeIzJmXjr")]
        public bool? ReportsNavigationMenuVisible { get { return webUser.ReportsNavigationMenuVisible; } set { webUser.ReportsNavigationMenuVisible = value; } }
        [FwLogicProperty(Id: "sjKqUWSqxjd0")]
        public bool? WebQuoteRequest { get { return webUser.WebQuoteRequest; } set { webUser.WebQuoteRequest = value; } }
        //------------------------------------------------------------------------------------
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    UserLogic orig = ((UserLogic)e.Original);
                    WebUserId = orig.WebUserId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        private void AfterSaveUser(object sender, AfterSaveDataRecordEventArgs e)
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
        //------------------------------------------------------------------------------------   
    }
}
