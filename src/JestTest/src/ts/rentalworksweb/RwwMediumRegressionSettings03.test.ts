import {
    GeneratorFuelType, GeneratorMake, GeneratorRating, GeneratorWatts, GeneratorType,
    BlackoutStatus, BarCodeRange, InventoryAdjustmentReason, Attribute, InventoryCondition, InventoryGroup, InventoryType,
    PartsCategory, RentalCategory, RetiredReason, SalesCategory, Unit, DefaultSettings, 
    InventorySettings, Warehouse, User,
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

        this.MediumRegressionOnModule(new GeneratorFuelType());
        this.MediumRegressionOnModule(new GeneratorMake());
        this.MediumRegressionOnModule(new GeneratorRating());
        this.MediumRegressionOnModule(new GeneratorWatts());
        this.MediumRegressionOnModule(new GeneratorType());
        //this.MediumRegressionOnModule(new Holiday()); // module cannot be tested becuase data fields repeat and become invisible based on holiday.Type
        this.MediumRegressionOnModule(new BlackoutStatus());
        this.MediumRegressionOnModule(new BarCodeRange());
        this.MediumRegressionOnModule(new InventoryAdjustmentReason());
        this.MediumRegressionOnModule(new Attribute());
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
