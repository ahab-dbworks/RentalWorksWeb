import { TestUtils } from '../shared/TestUtils';
import {
    //home
    Asset, PartsInventory, PhysicalInventory, RentalInventory, RepairOrder, SalesInventory,
    DefaultSettings, 
    InventorySettings, 
    Warehouse,
    User,
} from './modules/AllModules';
import { MediumRegressionBaseTest } from './RwwMediumRegressionBase';

export class MediumRegressionHomeTest extends MediumRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        //this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        //Home - Inventory
        this.MediumRegressionOnModule(new Asset());
        this.MediumRegressionOnModule(new PhysicalInventory());
        //this.MediumRegressionOnModule(new RepairOrder());    // this module cannot be tested because we cannot search on a unique field. also the bar code validation allows all statuses, so we can't be sure that a record we pick there will be allowable in repair

    }
    //---------------------------------------------------------------------------------------
}

describe('MediumRegressionHomeTest', () => {
    try {
        new MediumRegressionHomeTest().Run();
    } catch (ex) {
        fail(ex);
    }
});
