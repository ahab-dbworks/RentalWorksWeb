import {
    AccountingSettings, GlAccount, GlDistribution, Country, State, BillingCycle, Department, ContactEvent, ContactTitle, MailList, Currency,
    CreditStatus, CustomerCategory, CustomerStatus, CustomerType, DealClassification, DealType, DealStatus, ProductionType, ScheduleType, DiscountTemplate,
    DocumentType, EventCategory, PersonnelType, PhotographyType, Building, FacilityType, FacilityRate, FacilityScheduleStatus,
    FacilityStatus, FacilityCategory, SpaceType, FiscalYear, DefaultSettings, 
    InventorySettings, Warehouse, User,
} from './modules/AllModules';
import { MediumRegressionBaseTest } from './RwwMediumRegressionBase';

export class MediumRegressionSettingsTest extends MediumRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        this.MediumRegressionOnModule(new AccountingSettings());
        this.MediumRegressionOnModule(new GlAccount());
        this.MediumRegressionOnModule(new GlDistribution());
        this.MediumRegressionOnModule(new Country());
        this.MediumRegressionOnModule(new State());
        this.MediumRegressionOnModule(new BillingCycle());
        this.MediumRegressionOnModule(new Department());
        this.MediumRegressionOnModule(new ContactEvent());
        this.MediumRegressionOnModule(new ContactTitle());
        this.MediumRegressionOnModule(new MailList());
        this.MediumRegressionOnModule(new Currency());
        this.MediumRegressionOnModule(new CreditStatus());
        this.MediumRegressionOnModule(new CustomerCategory());
        this.MediumRegressionOnModule(new CustomerStatus());
        this.MediumRegressionOnModule(new CustomerType());
        this.MediumRegressionOnModule(new DealClassification());
        this.MediumRegressionOnModule(new DealType());
        this.MediumRegressionOnModule(new DealStatus());
        this.MediumRegressionOnModule(new ProductionType());
        this.MediumRegressionOnModule(new ScheduleType());
        this.MediumRegressionOnModule(new DiscountTemplate());
        this.MediumRegressionOnModule(new DocumentType());
        //this.MediumRegressionOnModule(new CoverLetter());
        //this.MediumRegressionOnModule(new TermsConditions());
        this.MediumRegressionOnModule(new EventCategory());
        this.MediumRegressionOnModule(new PersonnelType());
        this.MediumRegressionOnModule(new PhotographyType());
        this.MediumRegressionOnModule(new Building());
        this.MediumRegressionOnModule(new FacilityType());
        this.MediumRegressionOnModule(new FacilityRate());
        this.MediumRegressionOnModule(new FacilityScheduleStatus());
        this.MediumRegressionOnModule(new FacilityStatus());
        this.MediumRegressionOnModule(new FacilityCategory());
        this.MediumRegressionOnModule(new SpaceType());
        this.MediumRegressionOnModule(new FiscalYear());

    }
    //---------------------------------------------------------------------------------------
}

describe('MediumRegressionSettingsTest', () => {
    try {
        new MediumRegressionSettingsTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
