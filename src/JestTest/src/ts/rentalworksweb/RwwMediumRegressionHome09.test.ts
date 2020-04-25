import {
    Contract, PickList, Container, Manifest, TransferOrder, TransferReceipt,
    Invoice, Receipt, VendorInvoice,
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

        //Home - Warehouse
        this.MediumRegressionOnModule(new Contract());
        this.MediumRegressionOnModule(new PickList());

        //Home - Container
        this.MediumRegressionOnModule(new Container());

        //Home - Transfer
        this.MediumRegressionOnModule(new Manifest());
        this.MediumRegressionOnModule(new TransferOrder());
        this.MediumRegressionOnModule(new TransferReceipt());

        //Home - Billing
        this.MediumRegressionOnModule(new Invoice());
        this.MediumRegressionOnModule(new Receipt());
        this.MediumRegressionOnModule(new VendorInvoice());

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
