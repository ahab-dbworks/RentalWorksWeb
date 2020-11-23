import {
    Contact, Customer, Deal, Order, Project, PurchaseOrder, Quote, Vendor,
    Asset, PartsInventory, PhysicalInventory, RentalInventory, RepairOrder, SalesInventory,
    Contract, PickList, Container, Manifest, TransferOrder, TransferReceipt,
    BankAccount, Invoice, Receipt, Payment, VendorInvoice,
} from './modules/AllModules';
import { ShallowRegressionBaseTest } from './RwwShallowRegressionBase';


export class ShallowRegressionHomeTest extends ShallowRegressionBaseTest {
    //---------------------------------------------------------------------------------------
    async PerformTests() {

        //Home - Agent
        this.ShallowRegressionOnModule(new Contact());
        this.ShallowRegressionOnModule(new Customer());
        this.ShallowRegressionOnModule(new Deal());
        this.ShallowRegressionOnModule(new Order());
        this.ShallowRegressionOnModule(new Project());
        this.ShallowRegressionOnModule(new PurchaseOrder());
        this.ShallowRegressionOnModule(new Quote());
        this.ShallowRegressionOnModule(new Vendor());

        //Home - Inventory
        this.ShallowRegressionOnModule(new Asset());
        this.ShallowRegressionOnModule(new PartsInventory());
        this.ShallowRegressionOnModule(new PhysicalInventory());
        this.ShallowRegressionOnModule(new RentalInventory());
        this.ShallowRegressionOnModule(new RepairOrder());
        this.ShallowRegressionOnModule(new SalesInventory());

        //Home - Warehouse
        this.ShallowRegressionOnModule(new Contract());
        this.ShallowRegressionOnModule(new PickList());

        //Home - Container
        this.ShallowRegressionOnModule(new Container());

        //Home - Transfer
        this.ShallowRegressionOnModule(new Manifest());
        this.ShallowRegressionOnModule(new TransferOrder());
        this.ShallowRegressionOnModule(new TransferReceipt());

        //Home - Billing
        this.ShallowRegressionOnModule(new BankAccount());
        this.ShallowRegressionOnModule(new Invoice());
        this.ShallowRegressionOnModule(new Receipt());
        this.ShallowRegressionOnModule(new Payment());
        this.ShallowRegressionOnModule(new VendorInvoice());

    }
    //---------------------------------------------------------------------------------------
}

describe('ShallowRegressionHomeTest', () => {
    try {
        new ShallowRegressionHomeTest().Run();
    } catch(ex) {
        fail(ex);
    }
});
