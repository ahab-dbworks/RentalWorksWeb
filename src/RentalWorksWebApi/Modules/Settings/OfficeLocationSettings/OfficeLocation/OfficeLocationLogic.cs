using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [FwLogic(Id: "SsvsPskrsl1v")]
    public class OfficeLocationLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        OfficeLocationRecord location = new OfficeLocationRecord();
        OfficeLocationLoader locationLoader = new OfficeLocationLoader();

        public OfficeLocationLogic()
        {
            dataRecords.Add(location);
            dataLoader = locationLoader;

            AfterSave += OnAfterSave;
            ForceSave = true;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id: "0Hj1fA4EoP7P", IsPrimaryKey: true)]
        public string LocationId { get { return location.LocationId; } set { location.LocationId = value; } }

        [FwLogicProperty(Id: "0Hj1fA4EoP7P", IsRecordTitle: true)]
        public string Location { get { return location.Location; } set { location.Location = value; } }

        [FwLogicProperty(Id: "s72vwwq6Bvd")]
        public string LocationCode { get { return location.LocationCode; } set { location.LocationCode = value; } }

        [FwLogicProperty(Id: "ZeOmhYbh0kY6D")]
        public string CompanyName { get { return location.CompanyName; } set { location.CompanyName = value; } }

        [FwLogicProperty(Id: "bi2KnYhOrPi94")]
        public string FederalId { get { return location.FederalId; } set { location.FederalId = value; } }

        [FwLogicProperty(Id: "DyLYzbbaRHFCB")]
        public string Address1 { get { return location.Address1; } set { location.Address1 = value; } }

        [FwLogicProperty(Id: "WmcJsMTwldiwF")]
        public string Address2 { get { return location.Address2; } set { location.Address2 = value; } }

        [FwLogicProperty(Id: "p7WEwHOTr1vd2")]
        public string City { get { return location.City; } set { location.City = value; } }

        [FwLogicProperty(Id: "k3o1NoZTb7Oe4")]
        public string Zip { get { return location.Zip; } set { location.Zip = value; } }

        [FwLogicProperty(Id: "O5h4HpR8p6xRt")]
        public string State { get { return location.State; } set { location.State = value; } }

        [FwLogicProperty(Id: "AIAesoiOBJfUx")]
        public string CountryId { get { return location.CountryId; } set { location.CountryId = value; } }

        [FwLogicProperty(Id: "LVGrEQslMkIwE", IsReadOnly: true)]
        public string Country { get; set; }

        [FwLogicProperty(Id: "yl1dYckdmE8KZ")]
        public string Phone { get { return location.Phone; } set { location.Phone = value; } }

        [FwLogicProperty(Id: "gbmqrkj4A2auZ")]
        public string Fax { get { return location.Fax; } set { location.Fax = value; } }

        [FwLogicProperty(Id: "vkQn6TgUTnDWo")]
        public string Email { get { return location.Email; } set { location.Email = value; } }

        [FwLogicProperty(Id: "U5purbrsmbnUJ")]
        public string WebAddress { get { return location.WebAddress; } set { location.WebAddress = value; } }

        [FwLogicProperty(Id: "fhFrVlijkiDf1")]
        public string RemitToCompanyName { get { return location.RemitToCompanyName; } set { location.RemitToCompanyName = value; } }

        [FwLogicProperty(Id: "KOdr66LzHCThF")]
        public string RemitToAddress1 { get { return location.RemitToAddress1; } set { location.RemitToAddress1 = value; } }

        [FwLogicProperty(Id: "r859YheYSjFwu")]
        public string RemitToAddress2 { get { return location.RemitToAddress2; } set { location.RemitToAddress2 = value; } }

        [FwLogicProperty(Id: "u8IyOKcDaIgUc")]
        public string RemitToCity { get { return location.RemitToCity; } set { location.RemitToCity = value; } }

        [FwLogicProperty(Id: "O6ku880yQILkN")]
        public string RemitToZip { get { return location.RemitToZip; } set { location.RemitToZip = value; } }

        [FwLogicProperty(Id: "c6pHG5ziTYrLg")]
        public string RemitToState { get { return location.RemitToState; } set { location.RemitToState = value; } }

        [FwLogicProperty(Id: "XcZ6rBXgTrCzS")]
        public string RemitToCountryId { get { return location.RemitToCountryId; } set { location.RemitToCountryId = value; } }

        [FwLogicProperty(Id: "SWBuJkrcUQT1s", IsReadOnly: true)]
        public string RemitToCountry { get; set; }

        [FwLogicProperty(Id: "m4zDbpqKEr5O5")]
        public string RemitToPhone { get { return location.RemitToPhone; } set { location.RemitToPhone = value; } }

        [FwLogicProperty(Id: "waIUA6NAfDvhH")]
        public string RemitToFax { get { return location.RemitToFax; } set { location.RemitToFax = value; } }

        [FwLogicProperty(Id: "AnucOO0fJrtwW")]
        public string RemitToEmail { get { return location.RemitToEmail; } set { location.RemitToEmail = value; } }

        [FwLogicProperty(Id: "lGRLyD6Zq20")]
        public string RateType { get { return location.RateType; } set { location.RateType = value; } }

        [FwLogicProperty(Id: "TD0CoCaw0vFpJ", IsReadOnly: true)]
        public string RateTypeDisplay { get; set; }

        [FwLogicProperty(Id: "17YN2fTfnBCZH")]
        public string Color { get { return location.Color; } set { location.Color = value; } }

        [FwLogicProperty(Id: "iU02kRoiKzq")]
        public string DefaultPurchasePoTypeId { get { return location.DefaultPurchasePoTypeId; } set { location.DefaultPurchasePoTypeId = value; } }

        [FwLogicProperty(Id: "BuPhPT7D3XD", IsReadOnly: true)]
        public string DefaultPurchasePoType { get; set; }

        [FwLogicProperty(Id: "KUV4zO00aBt0v")]
        public string GlPrefix { get { return location.GlPrefix; } set { location.GlPrefix = value; } }

        [FwLogicProperty(Id: "kQSgwE143S0sR")]
        public string GlSuffix { get { return location.GlSuffix; } set { location.GlSuffix = value; } }

        [FwLogicProperty(Id: "7sJFv7oKrwuXx")]
        public bool? UseNumberPrefix { get { return location.UseNumberPrefix; } set { location.UseNumberPrefix = value; } }

        [FwLogicProperty(Id: "Zb3GsDaZZLFJq")]
        public string NumberPrefix { get { return location.NumberPrefix; } set { location.NumberPrefix = value; } }

        [FwLogicProperty(Id: "m5RhkDjMvklnT")]
        public bool? UseRequisitionNumbers { get { return location.UseRequisitionNumbers; } set { location.UseRequisitionNumbers = value; } }

        [FwLogicProperty(Id: "2iDk3wdztJqYE")]
        public bool? UseSameNumberForQuoteAndOrder { get { return location.UseSameNumberForQuoteAndOrder; } set { location.UseSameNumberForQuoteAndOrder = value; } }

        [FwLogicProperty(Id: "XyYSpJdYXeBWY")]
        public bool? UseSameNumberForAllExportBatches { get { return location.UseSameNumberForAllExportBatches; } set { location.UseSameNumberForAllExportBatches = value; } }

        [FwLogicProperty(Id: "FaYkG70UZGgIc")]
        public bool? UserOrderNumberAndSuffixForInvoice { get { return location.UserOrderNumberAndSuffixForInvoice; } set { location.UserOrderNumberAndSuffixForInvoice = value; } }

        [FwLogicProperty(Id: "oUX9anIe2VxyM")]
        public bool? UseHInHiatusInvoiceNumbers { get { return location.UseHInHiatusInvoiceNumbers; } set { location.UseHInHiatusInvoiceNumbers = value; } }

        [FwLogicProperty(Id: "vOrKm2KiBUh1p", IsReadOnly: true)]
        public string DefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "Lr66u6QGlFEH0", IsReadOnly: true)]
        public string DefaultCurrencyCode { get; set; }

        [FwLogicProperty(Id: "TNVs0dD8Wk87t", IsReadOnly: true)]
        public string DefaultCurrency { get; set; }

        [FwLogicProperty(Id: "2i7CCRqzReILc")]
        public string Tax1ReferenceName { get { return location.Tax1ReferenceName; } set { location.Tax1ReferenceName = value; } }

        [FwLogicProperty(Id: "VRj4ZBLmKRjpU")]
        public string Tax1ReferenceNumber { get { return location.Tax1ReferenceNumber; } set { location.Tax1ReferenceNumber = value; } }

        [FwLogicProperty(Id: "XhHpy8VEchE8J")]
        public string Tax2ReferenceName { get { return location.Tax2ReferenceName; } set { location.Tax2ReferenceName = value; } }

        [FwLogicProperty(Id: "xTQ5PrPy55wqo")]
        public string Tax2ReferenceNumber { get { return location.Tax2ReferenceNumber; } set { location.Tax2ReferenceNumber = value; } }

        [FwLogicProperty(Id: "h4mK8U5crfAu0")]
        public bool? DisableCreditStatusMessages { get { return location.DisableCreditStatusMessages; } set { location.DisableCreditStatusMessages = value; } }

        [FwLogicProperty(Id: "aQsSV5xGtr3y0")]
        public bool? DisableCreditThroughDateMessages { get { return location.DisableCreditThroughDateMessages; } set { location.DisableCreditThroughDateMessages = value; } }

        [FwLogicProperty(Id: "BdpPMKmIsknKp")]
        public bool? DisableInsuranceStatusMessages { get { return location.DisableInsuranceStatusMessages; } set { location.DisableInsuranceStatusMessages = value; } }

        [FwLogicProperty(Id: "j2AP4pitbCGE7")]
        public bool? DisableInsuranceThroughDateMessages { get { return location.DisableInsuranceThroughDateMessages; } set { location.DisableInsuranceThroughDateMessages = value; } }

        [FwLogicProperty(Id: "NGVDXcxGHbnEs")]
        public string InvoiceMessage { get; set; }

        [FwLogicProperty(Id: "ogpbNChKrpTrI")]
        public bool? Taxable { get { return location.Taxable; } set { location.Taxable = value; } }

        [FwLogicProperty(Id: "GDXu3A4umCh")]
        public bool? Inactive { get { return location.Inactive; } set { location.Inactive = value; } }

        [FwLogicProperty(Id: "TWUrrSVl5Ll")]
        public string DateStamp { get { return location.DateStamp; } set { location.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    if (string.IsNullOrEmpty(DefaultCurrencyId))
                    {
                        isValid = false;
                        validateMsg = "Default Currency is required on new Office Locations.";
                    }
                }
                else // updating
                {
                    if ((DefaultCurrencyId != null) && (DefaultCurrencyId.Equals(string.Empty)))
                    {
                        isValid = false;
                        validateMsg = "Default Currency is required.";
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (DefaultCurrencyId != null)
            {
                bool b = OfficeLocationFunc.SetOfficeLocationDefaultCurrency(AppConfig, UserSession, LocationId, DefaultCurrencyId, e.SqlConnection).Result;
            }


            bool doSaveInvoiceMessage = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveInvoiceMessage = true;
            }
            else if ((e.Original != null) && (InvoiceMessage != null))
            {
                OfficeLocationLogic orig = (OfficeLocationLogic)e.Original;
                doSaveInvoiceMessage = (!orig.InvoiceMessage.Equals(InvoiceMessage));
            }
            if (doSaveInvoiceMessage)
            {
                bool saved = location.SaveInvoiceMessageASync(InvoiceMessage).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }


        }
        //------------------------------------------------------------------------------------

    }
}
