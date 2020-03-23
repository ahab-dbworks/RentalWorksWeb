import {
    //home
    Order, 
    DefaultSettings, 
    Warehouse,
    User,
} from './modules/AllModules';
import { MediumRegressionBaseTest } from './RwwMediumRegressionBase';

export class MediumRegressionHomeTest extends MediumRegressionBaseTest {
    testTimeout: number = 300000; // 300 seconds
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        this.LoadMyUserGlobal(new User());
        this.OpenSpecificRecord(new DefaultSettings(), null, true);
        //this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        //Home - Agent
        this.MediumRegressionOnModule(new Order());

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
