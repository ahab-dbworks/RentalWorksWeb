using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.Administrator.Person
{
    [FwLogic(Id:"2sjxVgcctkZR")]
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
        [FwLogicProperty(Id:"XiZnktmGymsW", IsPrimaryKey:true)]
        public string UserId { get; set; }

        //[FwLogicProperty(Id:"1LXjFrDylvFXq")]
        //public string ContactId { get; set; }

        //[FwLogicProperty(Id:"6EdExULk6j7Cu")]
        //public string DealId { get; set; }

        [FwLogicProperty(Id:"qQqKv4acSlWvH")]
        public string Name { get; set; }

        [FwLogicProperty(Id:"Uply44NTW4GjC")]
        public string LoginName { get; set; }

        [FwLogicProperty(Id:"K73OrMfKM7UA", IsRecordTitle:true, IsReadOnly:true)]
        public string FullName { get; set; }


        [FwLogicProperty(Id:"XtKilcPCGZx6N")]
        public string FirstName { get; set; }

        [FwLogicProperty(Id:"Xzz0kYoe354n8")]
        public string MiddleInitial { get; set; }

        [FwLogicProperty(Id:"gurnqiQg6x3om")]
        public string LastName { get; set; }

        //[FwLogicProperty(Id:"6npgne7L5kBz7")]
        //public string LoginName { get ; set ;}

        [FwLogicProperty(Id:"0ISaI9qHYnDZ", IsReadOnly:true)]
        public string Password { get; set; }

        [FwLogicProperty(Id:"TbohwKPwVNr6k")]
        public string GroupId { get; set; }

        [FwLogicProperty(Id:"H1jihBElXtM5", IsReadOnly:true)]
        public string GroupName { get; set; }

        [FwLogicProperty(Id:"o2ibM6YtfI3tV")]
        public string ScheduleColor { get; set; }

        [FwLogicProperty(Id:"nNggdPS5Ooqic")]
        public string UserTitleId { get; set; }

        [FwLogicProperty(Id:"qbBLwSr7mIoc", IsReadOnly:true)]
        public string UserTitle { get; set; }

        [FwLogicProperty(Id:"z2YyZ6we6Jilk")]
        public string Email { get; set; }

        [FwLogicProperty(Id:"Yz97DZQKbSqyn")]
        public string OfficeLocationId { get; set; }

        [FwLogicProperty(Id:"ioPUVlr5e3gq", IsReadOnly:true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id:"Nyh2KTY5ZQa3F")]
        public string WarehouseId { get; set; }

        [FwLogicProperty(Id:"aKGPGqXXlg7o", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"mUASTBpPyaWsH")]
        public string Address1 { get; set; }

        [FwLogicProperty(Id:"w1gjzOhcKQrxi")]
        public string Address2 { get; set; }

        [FwLogicProperty(Id:"7eoif7fEWvmI1")]
        public string City { get; set; }

        [FwLogicProperty(Id:"RJ6Iww8MQKzDN")]
        public string State { get; set; }

        [FwLogicProperty(Id:"je0AVytGDjkfg")]
        public string ZipCode { get; set; }

        [FwLogicProperty(Id:"b7soDPEuKvj7f")]
        public string CountryId { get; set; }

        [FwLogicProperty(Id:"F4VftomRg2m3", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"7VT9vuTWdGf0l")]
        public string OfficePhone { get; set; }

        [FwLogicProperty(Id:"sY7hJMF1VV8Aa")]
        public string OfficeExtension { get; set; }

        [FwLogicProperty(Id:"fJS5pLPQqmUrd")]
        public string Fax { get; set; }

        [FwLogicProperty(Id:"i66g0iPK9gDEP")]
        public string DirectPhone { get; set; }

        [FwLogicProperty(Id:"LD8wg70EDpzkh")]
        public string Pager { get; set; }

        [FwLogicProperty(Id:"uqRwHL9Ah9mk3")]
        public string PagerPin { get; set; }

        [FwLogicProperty(Id:"0ytlQbp1ZTuow")]
        public string Cellular { get; set; }

        [FwLogicProperty(Id:"sYjEx1uS7j1ih")]
        public string HomePhone { get; set; }


        [FwLogicProperty(Id:"ewDmHVmEkL9mG")]
        public string DefaultDepartmentType { get; set; }

        [FwLogicProperty(Id:"NBx54CZ85j7Q", IsReadOnly:true)]
        public string PrimaryDepartmentId { get; set; }

        [FwLogicProperty(Id:"NBx54CZ85j7Q", IsReadOnly:true)]
        public string PrimaryDepartment { get; set; }

        [FwLogicProperty(Id:"HsmmdJNmbRHkj")]
        public string RentalDepartmentId { get; set; }

        [FwLogicProperty(Id:"RJuLMHhU3y3T", IsReadOnly:true)]
        public string RentalDepartment { get; set; }

        [FwLogicProperty(Id:"SlnGgmhLmSrSx")]
        public string SalesDepartmentId { get; set; }

        [FwLogicProperty(Id:"OuzjrD9enWuF", IsReadOnly:true)]
        public string SalesDepartment { get; set; }

        [FwLogicProperty(Id:"PuJEZveLokUKt")]
        public string PartsDepartmentId { get; set; }

        [FwLogicProperty(Id:"x4Ow89vat8B8", IsReadOnly:true)]
        public string PartsDepartment { get; set; }

        [FwLogicProperty(Id:"SfWuwfG5F3Gcl")]
        public string MiscDepartmentId { get; set; }

        [FwLogicProperty(Id:"OSSwfFrz0FPx", IsReadOnly:true)]
        public string MiscDepartment { get; set; }

        [FwLogicProperty(Id:"Rct6M2dnB9lhb")]
        public string LaborDepartmentId { get; set; }

        [FwLogicProperty(Id:"m5navyAiVKvK", IsReadOnly:true)]
        public string LaborDepartment { get; set; }

        [FwLogicProperty(Id:"l128oMlhIBtqI")]
        public string FacilityDepartmentId { get; set; }

        [FwLogicProperty(Id:"tokpgST56dRo", IsReadOnly:true)]
        public string FacilityDepartment { get; set; }

        [FwLogicProperty(Id:"G5UieiKrGhpin")]
        public string TransportationDepartmentId { get; set; }

        [FwLogicProperty(Id:"nCMjdosPPDVx", IsReadOnly:true)]
        public string TransportationDepartment { get; set; }

        [FwLogicProperty(Id:"22WVdtCl6ricU")]
        public string RentalInventoryTypeId { get; set; }

        [FwLogicProperty(Id:"i1fo1A199K2I", IsReadOnly:true)]
        public string RentalInventoryType { get; set; }

        [FwLogicProperty(Id:"1Vpkoqw6x6MCg")]
        public string SalesInventoryTypeId { get; set; }

        [FwLogicProperty(Id:"TtYf491MB9jW", IsReadOnly:true)]
        public string SalesInventoryType { get; set; }

        [FwLogicProperty(Id:"0p9indIbMGkTE")]
        public string PartsInventoryTypeId { get; set; }

        [FwLogicProperty(Id:"0YuGBfsNQFzW", IsReadOnly:true)]
        public string PartsInventoryType { get; set; }

        [FwLogicProperty(Id:"BjDnvR6kfm6kV")]
        public string MiscTypeId { get; set; }

        [FwLogicProperty(Id:"Wdme7bCvjHWK", IsReadOnly:true)]
        public string MiscType { get; set; }

        [FwLogicProperty(Id:"plWcBgouo8wkn")]
        public string LaborTypeId { get; set; }

        [FwLogicProperty(Id:"isIWcu9vcRpw", IsReadOnly:true)]
        public string LaborType { get; set; }

        [FwLogicProperty(Id:"GUvODKjZfn5lT")]
        public string FacilityTypeId { get; set; }

        [FwLogicProperty(Id:"LDdixF0uYG3B", IsReadOnly:true)]
        public string FacilityType { get; set; }

        [FwLogicProperty(Id:"zdoDtZ744G8P9")]
        public string TransportationTypeId { get; set; }

        [FwLogicProperty(Id:"JmcHUcUd3Tb2", IsReadOnly:true)]
        public string TransportationType { get; set; }

        [FwLogicProperty(Id:"GbxxHdPPz6IF9")]
        public bool? NoMiscellaneousOnQuotes { get; set; }

        [FwLogicProperty(Id:"2OPGn6CcpCgAi")]
        public bool? NoMiscellaneousOnOrders { get; set; }

        [FwLogicProperty(Id:"6vZPCovRpzCjg")]
        public bool? NoMiscellaneousOnPurchaseOrders { get; set; }

        [FwLogicProperty(Id:"GihIKv2xdSifN")]
        public bool? LimitDaysPerWeek { get; set; }

        [FwLogicProperty(Id:"UmR2pbP1X0Ngh")]
        public decimal? MinimumDaysPerWeek { get; set; }

        [FwLogicProperty(Id:"5TQS892H20Qua")]
        public bool? AllowCreditLimitOverride { get; set; }

        //[FwLogicProperty(Id:"P2E6eAr7jEqN7")]
        //public bool? CumulativeDiscountOverride { get; set; }

        //[FwLogicProperty(Id:"r2jVpQEBViJEN")]
        //public bool? LimitAllowCumulativeDiscount { get; set; }

        //[FwLogicProperty(Id:"XO9M8mAgYAxUr")]
        //public decimal? MaximumCumulativeDiscount { get; set; }

        [FwLogicProperty(Id:"k4eOXrBbWd0Wn")]
        public bool? LimitDiscount { get; set; }

        [FwLogicProperty(Id:"jjXwsJweevbj8")]
        public decimal? MaximumDiscount { get; set; }

        [FwLogicProperty(Id:"b1UNf6TviojCz")]
        public bool? LimitSubDiscount { get; set; }

        [FwLogicProperty(Id:"FoSyNJyR7uiw7")]
        public decimal? MaximumSubDiscount { get; set; }

        [FwLogicProperty(Id:"cGoXmDLNFjj2A")]
        public string DiscountRule { get; set; }

        [FwLogicProperty(Id:"RtpNIwQYSmcHo")]
        public bool? StagingAllowIncreaseDecreaseOrderQuantity { get; set; }

        [FwLogicProperty(Id:"oWco6ZFAUho4o")]
        public bool? AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes { get; set; }

        [FwLogicProperty(Id:"FMah7xjB5HXtR")]
        public bool? AllowContractIfDealRequiresPOAndOrderHasPendingPO { get; set; }

        [FwLogicProperty(Id:"UFuJDmVU54uLV")]
        public bool? AllowContractIfPendingItemsExist { get; set; }

        [FwLogicProperty(Id:"EjrO4hLDMLxWh")]
        public bool? AllowContractIfCustomerDealDoesNotHaveApprovedCredit { get; set; }

        [FwLogicProperty(Id:"BP7ZyVd6Dpwjq")]
        public bool? AllowContractIfCustomerDealIsOverTheirCreditLimit { get; set; }

        [FwLogicProperty(Id:"myxBtGNo0MguU")]
        public bool? AllowContractIfCustomerDealInsuranceCoverageIsLess { get; set; }

        [FwLogicProperty(Id:"7alS3eS0ndp1t")]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate { get; set; }

        [FwLogicProperty(Id:"FMCqo6pzUPw18")]
        public bool? AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate { get; set; }

        [FwLogicProperty(Id:"8x1hDI4sZOi2F")]
        public bool? AllowReceiveSubsWhenPositiveConflictExists { get; set; }

        [FwLogicProperty(Id:"5vVH9OrE7uJKf")]
        public bool? AllowStagingOfUnreservedConsignedItems { get; set; }

        [FwLogicProperty(Id:"kNMnCicLauloC")]
        public bool? AllowStagingOfUnapprovedItems { get; set; }

        [FwLogicProperty(Id:"cvGjg7bh2XHIS")]
        public bool? AllowSubstitutesAtStaging { get; set; }

        [FwLogicProperty(Id:"rpCSKmQv1gZkp")]
        public bool? DeleteOriginalOnSubstitution { get; set; }

        [FwLogicProperty(Id:"TwwmRuSemCOV2")]
        public bool? AllowCancelContract { get; set; }

        [FwLogicProperty(Id:"vJz0Sq1Yc3KBl")]
        public bool? QuikActivityAllowPrintDollarAmounts { get; set; }

        [FwLogicProperty(Id:"M3OHdQB4hBnTW")]
        public bool? QuikScanAllowCreateContract { get; set; }

        [FwLogicProperty(Id:"JIfSSIYvKvCuJ")]
        public bool? QuikScanAllowApplyAll { get; set; }

        [FwLogicProperty(Id:"oxv4zI9OwJmcQ")]
        public bool? AllowCrossICodeExchange { get; set; }

        [FwLogicProperty(Id:"fnFTMHr2RoEGU")]
        public bool? AllowCrossICodePendingExchange { get; set; }

        [FwLogicProperty(Id:"PttcJuOUjnGAm")]
        public bool? AllowChangeAvailabilityPriority { get; set; }

        [FwLogicProperty(Id:"eZFOY7K1ObAvM")]
        public bool? UserMustChangePassword { get; set; }

        [FwLogicProperty(Id:"3z9wSArSZFHgk")]
        public bool? PasswordExpires { get; set; }

        [FwLogicProperty(Id:"FkHsyqqNaDGXL")]
        public int? PasswordExpireDays { get; set; }

        [FwLogicProperty(Id:"SwOdLL8TE0m80")]
        public string PasswordUpdatedDateTime { get; set; }

        [FwLogicProperty(Id:"NeoF7FMlVZDGv")]
        public bool? AccountLocked { get; set; }

        [FwLogicProperty(Id:"Cnbl93osD0L4g")]
        public string Memo { get; set; }

        [FwLogicProperty(Id:"6DXd93vGKZcU4")]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id:"g2FqnqmtoiCPq")]
        public string DateStamp { get; set; }


        // WebUserRecord
        [FwLogicProperty(Id:"rx8m6xVuUssYs")]
        public string WebUserId { get; set; }

        //[FwLogicProperty(Id:"1pekRyeqMBHFN")]
        //public bool? WebAccess { get ; set ;}

        //[FwLogicProperty(Id:"Foe7GxXR5i73I")]
        //public bool? LockAccount { get ; set ;}

        //[FwLogicProperty(Id:"cDlK8uac2p7XK")]
        //public string WebPassword { get ; set ;}

        //[FwLogicProperty(Id:"y2IWs50ZxfvrO")]
        //public bool? ExpirePassword { get ; set ;}

        //[FwLogicProperty(Id:"EFIjHM8hTVuUv")]
        //public int? ExpireDays { get ; set ;}

        //[FwLogicProperty(Id:"UMHhvwWQ6hJgG")]
        //public bool? ChangePasswordAtNextLogin { get ; set ;}

        //[FwLogicProperty(Id:"0gcYLLVIYgAL3")]
        //public string PasswordLastUpdated { get ; set ;}


        //------------------------------------------------------------------------------------ 
    }
}
