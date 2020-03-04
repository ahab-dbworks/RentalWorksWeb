import {
    PropsCondition,
    Region, RepairItemStatus, SetCondition, SetSurface, SetOpening, WallDescription, WallType, ShipVia, Source, AvailabilitySettings, DefaultSettings, EmailSettings,
    InventorySettings, LogoSettings, DocumentBarCodeSettings, SystemSettings, TaxOption, Template, UserStatus, Sound, LicenseClass, VehicleColor, VehicleFuelType, VehicleMake, VehicleScheduleStatus, VehicleStatus,
    VehicleType, OrganizationType, VendorCatalog, VendorClass, SapVendorInvoiceStatus, WardrobeCare, WardrobeColor, WardrobeCondition, WardrobeGender, WardrobeLabel,
    WardrobeMaterial, WardrobePattern, WardrobePeriod, WardrobeSource, Warehouse, Widget, WorkWeek,
} from './modules/AllModules';
import { ShallowRegressionBaseTest } from './RwwShallowRegressionBase';

export class ShallowRegressionSettingsTest extends ShallowRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        this.ShallowRegressionOnModule(new PropsCondition());
        this.ShallowRegressionOnModule(new Region());
        this.ShallowRegressionOnModule(new RepairItemStatus());
        this.ShallowRegressionOnModule(new SetCondition());
        this.ShallowRegressionOnModule(new SetSurface());
        this.ShallowRegressionOnModule(new SetOpening());
        this.ShallowRegressionOnModule(new WallDescription());
        this.ShallowRegressionOnModule(new WallType());
        this.ShallowRegressionOnModule(new ShipVia());
        this.ShallowRegressionOnModule(new Source());
        this.ShallowRegressionOnModule(new AvailabilitySettings());
        this.ShallowRegressionOnModule(new DefaultSettings());
        this.ShallowRegressionOnModule(new EmailSettings());
        this.ShallowRegressionOnModule(new InventorySettings());
        this.ShallowRegressionOnModule(new LogoSettings());
        this.ShallowRegressionOnModule(new DocumentBarCodeSettings());
        this.ShallowRegressionOnModule(new SystemSettings());
        this.ShallowRegressionOnModule(new TaxOption());
        this.ShallowRegressionOnModule(new Template());
        this.ShallowRegressionOnModule(new UserStatus());
        this.ShallowRegressionOnModule(new Sound());
        this.ShallowRegressionOnModule(new LicenseClass());
        this.ShallowRegressionOnModule(new VehicleColor());
        this.ShallowRegressionOnModule(new VehicleFuelType());
        this.ShallowRegressionOnModule(new VehicleMake());
        this.ShallowRegressionOnModule(new VehicleScheduleStatus());
        this.ShallowRegressionOnModule(new VehicleStatus());
        this.ShallowRegressionOnModule(new VehicleType());
        this.ShallowRegressionOnModule(new OrganizationType());
        this.ShallowRegressionOnModule(new VendorCatalog());
        this.ShallowRegressionOnModule(new VendorClass());
        this.ShallowRegressionOnModule(new SapVendorInvoiceStatus());
        this.ShallowRegressionOnModule(new WardrobeCare());
        this.ShallowRegressionOnModule(new WardrobeColor());
        this.ShallowRegressionOnModule(new WardrobeCondition());
        this.ShallowRegressionOnModule(new WardrobeGender());
        this.ShallowRegressionOnModule(new WardrobeLabel());
        this.ShallowRegressionOnModule(new WardrobeMaterial());
        this.ShallowRegressionOnModule(new WardrobePattern());
        this.ShallowRegressionOnModule(new WardrobePeriod());
        this.ShallowRegressionOnModule(new WardrobeSource());
        this.ShallowRegressionOnModule(new Warehouse());
        this.ShallowRegressionOnModule(new Widget());
        this.ShallowRegressionOnModule(new WorkWeek());
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
