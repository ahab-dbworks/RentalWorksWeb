import { BaseTest } from '../shared/BaseTest';
import { TestUtils } from '../shared/TestUtils';
import { GlobalScope } from '../shared/GlobalScope';
import { RentalInventory } from './modules/RentalInventory';
import { Contact } from './modules/Contact';
import { Vendor } from './modules/Vendor';
import { Customer } from './modules/Customer';
import { Deal } from './modules/Deal';
import { Quote } from './modules/Quote';
import { User } from './modules/User';
import { DefaultSettings } from './modules/DefaultSettings';

export class ShallowRegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    globalScopeRef = GlobalScope;
    //---------------------------------------------------------------------------------------
    async ValidateEnvironment() {
        //const userModule: User = new User();
        //this.LoadUserGlobal(userModule);
        //this.ValidateUserAndEnvironment();
    }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        //-----------------------------------//
        //        DEFAULT SETTINGS           //
        //-----------------------------------//
        //if (this.continueTest) {
        //    const defaultSettingsModule: DefaultSettings = new DefaultSettings();

        //    this.TestModuleOpenBrowse(defaultSettingsModule);

        //    this.TestModuleOpenBrowseOpenForm(defaultSettingsModule, 1, true);
        //}


        //-----------------------------------//
        //        RENTAL INVENTORY           //
        //-----------------------------------//
        const rentalInventoryModule: RentalInventory = new RentalInventory();
        this.TestModuleOpenBrowseOpenForm(rentalInventoryModule, 1);


        //-------------//
        //   CONTACT   //
        //-------------//
        const contactModule: Contact = new Contact();
        this.TestModuleOpenBrowseOpenForm(contactModule, 1);


        //-------------//
        //   VENDOR    //
        //-------------//
        const vendorModule: Vendor = new Vendor();
        this.TestModuleOpenBrowseOpenForm(vendorModule, 1);

        //-------------//
        //  CUSTOMER   //
        //-------------//
        const customerModule: Customer = new Customer();
        this.TestModuleOpenBrowseOpenForm(customerModule, 1);


        //-------------//
        //    DEAL     //
        //-------------//
        const dealModule: Deal = new Deal();
        this.TestModuleOpenBrowseOpenForm(dealModule, 1);


        //-------------//
        //    QUOTE    //
        //-------------//
        const quoteModule: Quote = new Quote();
        this.TestModuleOpenBrowseOpenForm(quoteModule, 1);

    }
    //---------------------------------------------------------------------------------------
}

new ShallowRegressionTest().Run();
