using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;

namespace WebApi.Modules.Settings.SystemSettings.DefaultSettings
{
    [FwLogic(Id: "OVIuyEMyfRxya")]
    public class DefaultSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SysControlRecord sysControl = new SysControlRecord();
        WebControlRecord webControl = new WebControlRecord();
        DefaultSettingsLoader defaultSettingsLoader = new DefaultSettingsLoader();

        //------------------------------------------------------------------------------------ 
        public DefaultSettingsLogic()
        {
            dataRecords.Add(sysControl);
            dataRecords.Add(webControl);
            dataLoader = defaultSettingsLoader;
            ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "0D5KWmGH51eK4", IsPrimaryKey: true)]
        public string DefaultSettingsId { get { return sysControl.ControlId; } set { sysControl.ControlId = value; webControl.ControlId = value; } }

        [FwLogicProperty(Id: "abP3EBH042ZAK")]
        public string DefaultUnitId { get { return sysControl.DefaultUnitId; } set { sysControl.DefaultUnitId = value; } }

        [FwLogicProperty(Id: "s8Gy7WImkpsMG", IsRecordTitle: true)]
        public string DefaultSettingsName { get { return "Default Settings"; }  }

        [FwLogicProperty(Id: "pFNQ2jud6qxXM", IsReadOnly: true)]
        public string DefaultUnit { get; set; }

        [FwLogicProperty(Id: "6sIkrtgADZvgy")]
        public string DefaultDealStatusId { get { return sysControl.DefaultDealStatusId; } set { sysControl.DefaultDealStatusId = value; } }

        [FwLogicProperty(Id: "Pc52ahAxjIriy", IsReadOnly: true)]
        public string DefaultDealStatus { get; set; }

        [FwLogicProperty(Id: "iCRdII8kgu28u")]
        public string DefaultCustomerStatusId { get { return sysControl.DefaultCustomerStatusId; } set { sysControl.DefaultCustomerStatusId = value; } }

        [FwLogicProperty(Id: "TeS6IeHKSepCZ", IsReadOnly: true)]
        public string DefaultCustomerStatus { get; set; }

        [FwLogicProperty(Id: "bfs3aBCJIzHkv")]
        public string DefaultDealBillingCycleId { get { return sysControl.DefaultDealBillingCycleId; } set { sysControl.DefaultDealBillingCycleId = value; } }

        [FwLogicProperty(Id: "ElfammamFsZh2", IsReadOnly: true)]
        public string DefaultDealBillingCycle { get; set; }

        [FwLogicProperty(Id: "5wmuV1aELVeLG")]
        public bool? DefaultDealPoRequired { get { return sysControl.DefaultDealPoRequired; } set { sysControl.DefaultDealPoRequired = value; } }

        [FwLogicProperty(Id: "72tjgha0n1Tpj", IsReadOnly: true)]
        public string DefaultDealPoType { get { return sysControl.DefaultDealPoType; } set { sysControl.DefaultDealPoType = value; } }

        [FwLogicProperty(Id: "Y1czh8RoYET0V")]
        public string DefaultNonRecurringBillingCycleId { get { return sysControl.DefaultNonRecurringBillingCycleId; } set { sysControl.DefaultNonRecurringBillingCycleId = value; } }

        [FwLogicProperty(Id: "7aBCzkAxnay6Q", IsReadOnly: true)]
        public string DefaultNonRecurringBillingCycle { get; set; }

        [FwLogicProperty(Id: "6HvjQ2GOCIf7m")]
        public string DefaultCustomerPaymentTermsId { get { return sysControl.DefaultCustomerPaymentTermsId; } set { sysControl.DefaultCustomerPaymentTermsId = value; } }

        [FwLogicProperty(Id: "srEszDYiKWJP0")]
        public string DefaultCustomerPaymentTerms { get; set; }

        [FwLogicProperty(Id: "4oMzN7Q7LTF3p")]
        public string DefaultContactGroupId { get { return webControl.DefaultContactGroupId; } set { webControl.DefaultContactGroupId = value; } }

        [FwLogicProperty(Id: "lx3wWmc9AU4Yy")]
        public string DefaultContactGroupName { get; set; }

        [FwLogicProperty(Id: "mF1eIKmVivq6K")]
        public string DefaultRank { get { return sysControl.DefaultRank; } set { sysControl.DefaultRank = value; } }

        [FwLogicProperty(Id: "Q4pIy8C1q0syR")]
        public string DefaultCreditStatusId { get { return sysControl.DefaultCreditStatusId; } set { sysControl.DefaultCreditStatusId = value; } }

        [FwLogicProperty(Id: "NWB08U3R5NW77", IsReadOnly: true)]
        public string DefaultCreditStatus { get; set; }

        [FwLogicProperty(Id: "mKkvlk6jTcylT")]
        public string DateStamp { get { return sysControl.DateStamp; } set { sysControl.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                isValid = false;
                validateMsg = "Cannot add new records to " + this.BusinessLogicModuleName;
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------    
    }
}
