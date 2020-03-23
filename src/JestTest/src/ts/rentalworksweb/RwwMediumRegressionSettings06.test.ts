import {
    ActivityType, EventType, UnretiredReason, WarehouseCatalog, Crew, LaborRate, LaborPosition, LaborType, LaborCategory,
    CrewScheduleStatus, CrewStatus, MiscRate, MiscType, MiscCategory, OfficeLocation, OrderType, DiscountReason, MarketSegment, MarketType, 
    POType, DefaultSettings, InventorySettings, Warehouse, User,
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

        this.MediumRegressionOnModule(new UnretiredReason());
        this.MediumRegressionOnModule(new WarehouseCatalog());
        this.MediumRegressionOnModule(new Crew());
        this.MediumRegressionOnModule(new LaborRate());
        this.MediumRegressionOnModule(new LaborPosition());
        this.MediumRegressionOnModule(new LaborType());
        this.MediumRegressionOnModule(new LaborCategory());
        this.MediumRegressionOnModule(new CrewScheduleStatus());
        this.MediumRegressionOnModule(new CrewStatus());
        this.MediumRegressionOnModule(new MiscRate());
        this.MediumRegressionOnModule(new MiscType());
        this.MediumRegressionOnModule(new MiscCategory());
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
