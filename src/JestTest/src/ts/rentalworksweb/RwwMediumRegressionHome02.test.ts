import {
    //home
    Project, PurchaseOrder, Quote, Vendor,
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
        this.MediumRegressionOnModule(new Project());
        this.MediumRegressionOnModule(new PurchaseOrder());
        this.MediumRegressionOnModule(new Quote());
        this.MediumRegressionOnModule(new Vendor());

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
