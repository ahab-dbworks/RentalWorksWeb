import {
    DefaultSettings, 
    InventorySettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, User,
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

        this.MediumRegressionOnModule(new TaxOption());
        this.MediumRegressionOnModule(new Template());
        this.MediumRegressionOnModule(new UserStatus());
        this.MediumRegressionOnModule(new Sound());
        this.MediumRegressionOnModule(new LicenseClass());
        this.MediumRegressionOnModule(new VehicleColor());
        this.MediumRegressionOnModule(new VehicleFuelType());
        this.MediumRegressionOnModule(new VehicleMake());
        this.MediumRegressionOnModule(new VehicleScheduleStatus());
        this.MediumRegressionOnModule(new VehicleStatus());
        this.MediumRegressionOnModule(new VehicleType());
        this.MediumRegressionOnModule(new OrganizationType());
        this.MediumRegressionOnModule(new VendorCatalog());
        this.MediumRegressionOnModule(new VendorClass());
        this.MediumRegressionOnModule(new SapVendorInvoiceStatus());
        this.MediumRegressionOnModule(new WardrobeCare());
        this.MediumRegressionOnModule(new WardrobeColor());
        this.MediumRegressionOnModule(new WardrobeCondition());
        this.MediumRegressionOnModule(new WardrobeGender());
        this.MediumRegressionOnModule(new WardrobeLabel());
        this.MediumRegressionOnModule(new WardrobeMaterial());
        this.MediumRegressionOnModule(new WardrobePattern());
        this.MediumRegressionOnModule(new WardrobePeriod());
        this.MediumRegressionOnModule(new WardrobeSource());
        this.MediumRegressionOnModule(new Warehouse());
        this.MediumRegressionOnModule(new Widget());
        //this.MediumRegressionOnModule(new WorkWeek());     // module cannot be tested because there is no unique field that can be searched to validate or delete the record

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
