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
        //this.OpenSpecificRecord(new DefaultSettings(), null, true);
        //this.OpenSpecificRecord(new InventorySettings(), null, true);
        //
        //let warehouseToSeek: any = {
        //    Warehouse: "GlobalScope.User~ME.Warehouse",
        //}
        //this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

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
