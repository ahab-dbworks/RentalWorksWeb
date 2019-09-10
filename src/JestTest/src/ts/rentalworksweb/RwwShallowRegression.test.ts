import { BaseTest } from '../shared/BaseTest';
import { GlobalScope } from '../shared/GlobalScope';
import { RentalInventory, SalesInventory, PartsInventory, Contact, Vendor, Customer, Deal, Quote, User, Contract, DefaultSettings, Order, Project, PurchaseOrder } from './modules/AllModules';


export class ShallowRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    globalScopeRef = GlobalScope;
    //---------------------------------------------------------------------------------------
    async ValidateEnvironment() { }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        //Home
        this.TestModuleOpenBrowseOpenForm(new Contact());
        this.TestModuleOpenBrowseOpenForm(new Customer());
        this.TestModuleOpenBrowseOpenForm(new Deal());
        this.TestModuleOpenBrowseOpenForm(new Order());
        this.TestModuleOpenBrowseOpenForm(new Project());
        this.TestModuleOpenBrowseOpenForm(new PurchaseOrder());
        this.TestModuleOpenBrowseOpenForm(new Quote());
        this.TestModuleOpenBrowseOpenForm(new Vendor());
        this.TestModuleOpenBrowseOpenForm(new RentalInventory());
        this.TestModuleOpenBrowseOpenForm(new SalesInventory());
        this.TestModuleOpenBrowseOpenForm(new PartsInventory());
        this.TestModuleOpenBrowseOpenForm(new Contract());

        //Settings
        this.TestModuleOpenBrowseOpenForm(new DefaultSettings());

        //Administrator
        this.TestModuleOpenBrowseOpenForm(new User());
    }
    //---------------------------------------------------------------------------------------
}

new ShallowRegressionTest().Run();
