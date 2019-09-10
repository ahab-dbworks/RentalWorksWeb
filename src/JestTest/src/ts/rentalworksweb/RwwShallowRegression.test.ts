import { BaseTest } from '../shared/BaseTest';
import { GlobalScope } from '../shared/GlobalScope';
import {
    Contact, Customer, Deal, Order, Project, PurchaseOrder, Quote, Vendor,
    Asset, PartsInventory, PhysicalInventory, RentalInventory, RepairOrder, SalesInventory, 
    Contract,
    DefaultSettings, AccountingSettings, GlAccount,
    User
} from './modules/AllModules';


export class ShallowRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    globalScopeRef = GlobalScope;
    //---------------------------------------------------------------------------------------
    async ValidateEnvironment() { }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        //Home - Agent
        this.TestModuleOpenBrowseOpenForm(new Contact());
        this.TestModuleOpenBrowseOpenForm(new Customer());
        this.TestModuleOpenBrowseOpenForm(new Deal());
        this.TestModuleOpenBrowseOpenForm(new Order());
        this.TestModuleOpenBrowseOpenForm(new Project());
        this.TestModuleOpenBrowseOpenForm(new PurchaseOrder());
        this.TestModuleOpenBrowseOpenForm(new Quote());
        this.TestModuleOpenBrowseOpenForm(new Vendor());

        //Home - Inventory
        this.TestModuleOpenBrowseOpenForm(new Asset());
        this.TestModuleOpenBrowseOpenForm(new PartsInventory());
        this.TestModuleOpenBrowseOpenForm(new PhysicalInventory());
        this.TestModuleOpenBrowseOpenForm(new RentalInventory());
        this.TestModuleOpenBrowseOpenForm(new RepairOrder());
        this.TestModuleOpenBrowseOpenForm(new SalesInventory());

        //Home - Warehouse
        this.TestModuleOpenBrowseOpenForm(new Contract());

        //Settings
        this.TestModuleOpenBrowseOpenForm(new AccountingSettings());
        this.TestModuleOpenBrowseOpenForm(new GlAccount());
        this.TestModuleOpenBrowseOpenForm(new DefaultSettings());

        //Administrator
        this.TestModuleOpenBrowseOpenForm(new User());
    }
    //---------------------------------------------------------------------------------------
}

new ShallowRegressionTest().Run();
