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

        this.MediumRegressionOnModule(new InventoryCondition());
        this.MediumRegressionOnModule(new InventoryGroup());
        //this.MediumRegressionOnModule(new InventoryRank());  // module cannot be tested because there is no unique field that can be searched to validate or delete the record
        //this.MediumRegressionOnModule(new InventoryStatus());  // module cannot be tested because of unique index on the "statustype" field. no adds allowed
        this.MediumRegressionOnModule(new InventoryType());
        this.MediumRegressionOnModule(new PartsCategory());
        this.MediumRegressionOnModule(new RentalCategory());
        this.MediumRegressionOnModule(new RetiredReason());
        this.MediumRegressionOnModule(new SalesCategory());
        this.MediumRegressionOnModule(new Unit());
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
