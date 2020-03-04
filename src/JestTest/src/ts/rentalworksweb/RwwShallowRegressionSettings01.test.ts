import {
    ActivityType, AccountingSettings, GlAccount, GlDistribution, Country, State, BillingCycle, Department, ContactEvent, ContactTitle, MailList, Currency,
    CreditStatus, CustomerCategory, CustomerStatus, CustomerType, DealClassification, DealType, DealStatus, ProductionType, ScheduleType, DiscountTemplate,
    DocumentType, CoverLetter, TermsConditions, EventCategory, EventType, PersonnelType, PhotographyType, Building, FacilityType, FacilityRate, FacilityScheduleStatus,
    FacilityStatus, FacilityCategory, SpaceType, FiscalYear, GeneratorFuelType, GeneratorMake, GeneratorRating, GeneratorWatts, GeneratorType, Holiday,
    BlackoutStatus, BarCodeRange,
} from './modules/AllModules';
import { ShallowRegressionBaseTest } from './RwwShallowRegressionBase';

export class ShallowRegressionSettingsTest extends ShallowRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        this.ShallowRegressionOnModule(new ActivityType());
        this.ShallowRegressionOnModule(new AccountingSettings());
        this.ShallowRegressionOnModule(new GlAccount());
        this.ShallowRegressionOnModule(new GlDistribution());
        this.ShallowRegressionOnModule(new Country());
        this.ShallowRegressionOnModule(new State());
        this.ShallowRegressionOnModule(new BillingCycle());
        this.ShallowRegressionOnModule(new Department());
        this.ShallowRegressionOnModule(new ContactEvent());
        this.ShallowRegressionOnModule(new ContactTitle());
        this.ShallowRegressionOnModule(new MailList());
        this.ShallowRegressionOnModule(new Currency());
        this.ShallowRegressionOnModule(new CreditStatus());
        this.ShallowRegressionOnModule(new CustomerCategory());
        this.ShallowRegressionOnModule(new CustomerStatus());
        this.ShallowRegressionOnModule(new CustomerType());
        this.ShallowRegressionOnModule(new DealClassification());
        this.ShallowRegressionOnModule(new DealType());
        this.ShallowRegressionOnModule(new DealStatus());
        this.ShallowRegressionOnModule(new ProductionType());
        this.ShallowRegressionOnModule(new ScheduleType());
        this.ShallowRegressionOnModule(new DiscountTemplate());
        this.ShallowRegressionOnModule(new DocumentType());
        this.ShallowRegressionOnModule(new CoverLetter());
        this.ShallowRegressionOnModule(new TermsConditions());
        this.ShallowRegressionOnModule(new EventCategory());
        this.ShallowRegressionOnModule(new EventType());
        this.ShallowRegressionOnModule(new PersonnelType());
        this.ShallowRegressionOnModule(new PhotographyType());
        this.ShallowRegressionOnModule(new Building());
        this.ShallowRegressionOnModule(new FacilityType());
        this.ShallowRegressionOnModule(new FacilityRate());
        this.ShallowRegressionOnModule(new FacilityScheduleStatus());
        this.ShallowRegressionOnModule(new FacilityStatus());
        this.ShallowRegressionOnModule(new FacilityCategory());
        this.ShallowRegressionOnModule(new SpaceType());
        this.ShallowRegressionOnModule(new FiscalYear());
        this.ShallowRegressionOnModule(new GeneratorFuelType());
        this.ShallowRegressionOnModule(new GeneratorMake());
        this.ShallowRegressionOnModule(new GeneratorRating());
        this.ShallowRegressionOnModule(new GeneratorWatts());
        this.ShallowRegressionOnModule(new GeneratorType());
        this.ShallowRegressionOnModule(new Holiday());
        this.ShallowRegressionOnModule(new BlackoutStatus());
        this.ShallowRegressionOnModule(new BarCodeRange());
    }
    //---------------------------------------------------------------------------------------
}

describe('ShallowRegressionSettingsTest', () => {
    try {
        new ShallowRegressionSettingsTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
