import {
    //home
    Contact, Customer, Deal, Order, 
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
        this.OpenSpecificRecord(new InventorySettings(), null, true);

        let warehouseToSeek: any = {
            Warehouse: "GlobalScope.User~ME.Warehouse",
        }
        this.OpenSpecificRecord(new Warehouse(), warehouseToSeek, true, "MINE");

        //Home - Agent
        this.MediumRegressionOnModule(new Contact());
        this.MediumRegressionOnModule(new Customer());
        this.MediumRegressionOnModule(new Deal());
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
