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

export class RegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    globalScopeRef = GlobalScope;
    //---------------------------------------------------------------------------------------
    async ValidateEnvironment() {
        const userModule: User = new User();
        this.LoadUserGlobal(userModule);
        this.ValidateUserAndEnvironment();
    }
    //---------------------------------------------------------------------------------------
    async PerformTests() {
        var rentalInventoryInputs: any;
        var contactInputs: any;
        var vendorInputs: any;
        var customerInputs: any;
        var dealInputs: any;
        var quoteInputs: any;

        //-----------------------------------//
        //        DEFAULT SETTINGS           //
        //-----------------------------------//
        if (this.continueTest) {
            const defaultSettingsModule: DefaultSettings = new DefaultSettings();

            this.TestModuleOpenBrowse(defaultSettingsModule);

            this.TestModuleOpenBrowseOpenForm(defaultSettingsModule, 1, true);
        }



        /*
        //-----------------------------------//
        //        RENTAL INVENTORY           //
        //-----------------------------------//
        if (this.continueTest) {
            const rentalInventoryModule: RentalInventory = new RentalInventory();

            var defaultRentalInventoryExpected: any = {
                Unit: "GlobalScope.DefaultSettings~1.DefaultUnit",   // ie. "EA"
            }

            this.TestModuleDefaultsOnNewForm(rentalInventoryModule, defaultRentalInventoryExpected);



            rentalInventoryInputs = {
                ICode: TestUtils.randomAlphanumeric(7),
                Description: `${TestUtils.randomProductName()} - ${this.testToken}`,
                InventoryTypeId: 2,
                CategoryId: 1,
                UnitId: 1,
                ManufacturerPartNumber: TestUtils.randomAlphanumeric(8),
                Rank: 3,
            }

            var rentalInventoryExpected: any = {
                ICode: rentalInventoryInputs.ICode.toUpperCase().substr(0, 5) + '-' + rentalInventoryInputs.ICode.toUpperCase().substr(5),
                Description: rentalInventoryInputs.Description.toUpperCase(),
                ManufacturerPartNumber: rentalInventoryInputs.ManufacturerPartNumber.toUpperCase(),
            }


            // attempt to create a valid rentalInventory using "rentalInventoryInputs", compare the values the system saves with the "rentalInventoryExpected" object
            this.TestModuleCreateNewRecord(rentalInventoryModule, rentalInventoryInputs, rentalInventoryExpected);

        }


        //-------------//
        //   CONTACT   //
        //-------------//
        if (this.continueTest) {
            const contactModule: Contact = new Contact();
            contactInputs = {
                FirstName: `${TestUtils.randomFirstName()} - ${this.testToken}`,
                LastName: TestUtils.randomLastName(),
                Email: TestUtils.randomEmail(),
                Address1: TestUtils.randomAddress1(),
                Address2: TestUtils.randomAddress2(),
                City: TestUtils.randomCity(),
                State: TestUtils.randomStateCode(),
                ZipCode: TestUtils.randomZipCode(),
                OfficePhone: TestUtils.randomPhone(),
                OfficeExtension: TestUtils.randomPhoneExtension(),
                DirectPhone: TestUtils.randomPhone(),
                MobilePhone: TestUtils.randomPhone(),
                HomePhone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
            }

            var contactExpected: any = {
                FirstName: contactInputs.FirstName.toUpperCase(),
                LastName: contactInputs.LastName.toUpperCase(),
                Email: contactInputs.Email,
                Address1: contactInputs.Address1.toUpperCase(),
                Address2: contactInputs.Address2.toUpperCase(),
                City: contactInputs.City.toUpperCase(),
                State: contactInputs.State.toUpperCase(),
                ZipCode: contactInputs.ZipCode.toUpperCase(),
                OfficePhone: TestUtils.formattedPhone(contactInputs.OfficePhone),
                OfficeExtension: contactInputs.OfficeExtension,
                DirectPhone: TestUtils.formattedPhone(contactInputs.DirectPhone),
                MobilePhone: TestUtils.formattedPhone(contactInputs.MobilePhone),
                HomePhone: TestUtils.formattedPhone(contactInputs.HomePhone),
                Fax: TestUtils.formattedPhone(contactInputs.Fax),
            }

            // attempt to create a valid contact using "contactInputs", compare the values the system saves with the "contactExpected" object
            this.TestModuleCreateNewRecord(contactModule, contactInputs, contactExpected);

        }
        */
        //-------------//
        //   VENDOR    //
        //-------------//
        if (this.continueTest) {
            const vendorModule: Vendor = new Vendor();


            var defaultVendorExpected: any = {
                OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
            }

            this.TestModuleDefaultsOnNewForm(vendorModule, defaultVendorExpected);


            vendorInputs = {
                Vendor: `${TestUtils.randomCompanyName()} - ${this.testToken}`,
                VendorNumber: TestUtils.randomAlphanumeric(8),
                Address1: TestUtils.randomAddress1(),
                Address2: TestUtils.randomAddress2(),
                City: TestUtils.randomCity(),
                State: TestUtils.randomState(),
                ZipCode: TestUtils.randomZipCode(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                WebAddress: TestUtils.randomUrl(),
                OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
            }

            var vendorExpected: any = {
                Vendor: vendorInputs.Vendor.toUpperCase(),
                VendorNumber: vendorInputs.VendorNumber.toUpperCase(),
                Address1: vendorInputs.Address1.toUpperCase(),
                Address2: vendorInputs.Address2.toUpperCase(),
                City: vendorInputs.City.toUpperCase(),
                State: vendorInputs.State.toUpperCase(),
                ZipCode: vendorInputs.ZipCode.toUpperCase(),
                Phone: TestUtils.formattedPhone(vendorInputs.Phone),
                Fax: TestUtils.formattedPhone(vendorInputs.Fax),
                WebAddress: vendorInputs.WebAddress,
                OfficeLocation: vendorInputs.OfficeLocation
            }

            // attempt to create a valid vendor using "vendorInputs", compare the values the system saves with the "vendorExpected" object
            this.TestModuleCreateNewRecord(vendorModule, vendorInputs, vendorExpected);

            // vendor record with a blank Vendor Number
            var missingVendorNumberVendorInputs: any = {
                Vendor: `${TestUtils.randomCompanyName()} - ${this.testToken}`,
                //VendorNumber: faker.random.alphaNumeric(8),
                Address1: TestUtils.randomAddress1(),
                Address2: TestUtils.randomAddress2(),
                City: TestUtils.randomCity(),
                State: TestUtils.randomState(),
                ZipCode: TestUtils.randomZipCode(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                WebAddress: TestUtils.randomUrl(),
                OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
            }
            //this.TestModuleForMissingRequiredField(vendorModule, missingVendorNumberVendorInputs, "VendorNumber");


            // try to seek for the vendor record and open it
            var findVendorInputs: any = {
                VendorDisplayName: this.testToken
            }
            this.TestModuleOpenSpecificRecord(vendorModule, findVendorInputs);



            // try to seek for the vendor record and delete it
            var deleteVendorInputs: any = {
                VendorDisplayName: this.testToken
            }
            this.TestModuleDeleteSpecificRecord(vendorModule, deleteVendorInputs);
        }


        //-------------//
        //  CUSTOMER   //
        //-------------//
        if (this.continueTest) {
            const customerModule: Customer = new Customer();
            customerInputs = {
                Customer: `${TestUtils.randomCompanyName()} - ${this.testToken}`,
                CustomerNumber: TestUtils.randomAlphanumeric(8),
                Address1: TestUtils.randomAddress1(),
                Address2: TestUtils.randomAddress2(),
                City: TestUtils.randomCity(),
                State: TestUtils.randomState(),
                ZipCode: TestUtils.randomZipCode(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                WebAddress: TestUtils.randomUrl(),
                CustomerTypeId: 1,
                CreditStatusId: 1
            }

            var customerExpected: any = {
                Customer: customerInputs.Customer.toUpperCase(),
                CustomerNumber: customerInputs.CustomerNumber.toUpperCase(),
                Address1: customerInputs.Address1.toUpperCase(),
                Address2: customerInputs.Address2.toUpperCase(),
                City: customerInputs.City.toUpperCase(),
                State: customerInputs.State.toUpperCase(),
                ZipCode: customerInputs.ZipCode.toUpperCase(),
                Phone: TestUtils.formattedPhone(customerInputs.Phone),
                Fax: TestUtils.formattedPhone(customerInputs.Fax),
                WebAddress: customerInputs.WebAddress,
            }

            // attempt to create a valid customer using "customerInputs", compare the values the system saves with the "customerExpected" object
            this.TestModuleCreateNewRecord(customerModule, customerInputs, customerExpected);

            // change everything except the Customer Name
            var duplicateCustomer1Inputs: any = {
                Customer: customerInputs.Customer,
                CustomerNumber: TestUtils.randomAlphanumeric(8),
                Address1: TestUtils.randomAddress1(),
                Address2: TestUtils.randomAddress2(),
                City: TestUtils.randomCity(),
                State: TestUtils.randomState(),
                ZipCode: TestUtils.randomZipCode(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                WebAddress: TestUtils.randomUrl(),
                CustomerTypeId: 1,
                CreditStatusId: 1
            }


            // attempt to create a duplicate customer using the same Customer Name
            this.TestModulePreventDuplicate(customerModule, duplicateCustomer1Inputs, 'Customer Name');


            // change everything except the Customer Number
            var duplicateCustomer2Inputs: any = {
                Customer: `${TestUtils.randomCompanyName()} - ${this.testToken}`,
                CustomerNumber: customerInputs.CustomerNumber,
                Address1: TestUtils.randomAddress1(),
                Address2: TestUtils.randomAddress2(),
                City: TestUtils.randomCity(),
                State: TestUtils.randomState(),
                ZipCode: TestUtils.randomZipCode(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                WebAddress: TestUtils.randomUrl(),
                CustomerTypeId: 1,
                CreditStatusId: 1
            }

            // attempt to create a duplicate customer using the same Customer Number
            this.TestModulePreventDuplicate(customerModule, duplicateCustomer2Inputs, 'Customer Number');

        }

        //-------------//
        //    DEAL     //
        //-------------//
        if (this.continueTest) {
            const dealModule: Deal = new Deal();

            var defaultDealExpected: any = {
                OfficeLocation: "GlobalScope.User~ME.OfficeLocation",                  // ie. "LAS VEGAS"
                DealStatus: "GlobalScope.DefaultSettings~1.DefaultDealStatus",   // ie. "ACTIVE"
            }

            this.TestModuleDefaultsOnNewForm(dealModule, defaultDealExpected);


            dealInputs = {
                Deal: `${TestUtils.randomCompanyName()} - ${this.testToken}`,
                DealNumber: customerInputs.CustomerNumber,
                Customer: customerInputs.Customer,
                Address2: TestUtils.randomAddress2(),
                Fax: TestUtils.randomPhone(),
                DealTypeId: 1
            }

            var dealExpected: any = {
                Customer: dealInputs.Customer.toUpperCase(),
                Deal: dealInputs.Deal.toUpperCase(),
                DealNumber: dealInputs.DealNumber.toUpperCase(),
                Address1: customerInputs.Address1.toUpperCase(),
                Address2: dealInputs.Address2.toUpperCase(),
                City: customerInputs.City.toUpperCase(),
                State: customerInputs.State.toUpperCase(),
                ZipCode: customerInputs.ZipCode.toUpperCase(),
                Phone: TestUtils.formattedPhone(customerInputs.Phone),
                Fax: TestUtils.formattedPhone(dealInputs.Fax),
            }

            // attempt to create a valid deal using "dealInputs", compare the values the system saves with the "dealExpected" object
            this.TestModuleCreateNewRecord(dealModule, dealInputs, dealExpected);


            // change all other fields except Deal name
            var duplicateDeal1Inputs: any = {
                Deal: dealInputs.Deal,
                DealNumber: customerInputs.CustomerNumber,
                Customer: customerInputs.Customer,
                Address2: TestUtils.randomAddress2(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                DealTypeId: 1
            }

            // attempt to create a duplicate deal using the same Deal Name
            this.TestModulePreventDuplicate(dealModule, duplicateDeal1Inputs, 'Deal Name');

            // change all other fields except Deal Number
            var duplicateDeal2Inputs: any = {
                Deal: `${TestUtils.randomCompanyName()} - ${this.testToken}`,
                DealNumber: dealInputs.DealNumber,
                Customer: customerInputs.Customer,
                Address2: TestUtils.randomAddress2(),
                Phone: TestUtils.randomPhone(),
                Fax: TestUtils.randomPhone(),
                DealTypeId: 1
            }

            // attempt to create a duplicate deal using the same Deal Number
            this.TestModulePreventDuplicate(dealModule, duplicateDeal2Inputs, 'Deal Number');

        }



        //-------------//
        //    QUOTE    //
        //-------------//
        if (this.continueTest) {
            const quoteModule: Quote = new Quote();

            quoteInputs = {
                Description: `${TestUtils.randomJobTitle().substring(0, 25)} - ${this.testToken}`,
                Deal: dealInputs.Deal,
                Location: TestUtils.randomStreetName(),
                ReferenceNumber: TestUtils.randomAlphanumeric(8)
            }

            var quoteExpected: any = {
                Deal: quoteInputs.Deal.toUpperCase(),
                Description: quoteInputs.Description.toUpperCase(),
                QuoteNumber: "|NOTEMPTY|",
                Location: quoteInputs.Location.toUpperCase(),
                ReferenceNumber: quoteInputs.ReferenceNumber.toUpperCase()
            }

            // attempt to create a valid Quote using "quoteInputs", compare the values the system saves with the "quoteExpected" object
            this.TestModuleCreateNewRecord(quoteModule, quoteInputs, quoteExpected);

            //        test('Add one grid row in rental tab', async () => {
            //            const fieldObject = {
            //                InventoryId: '100686' // I-Code
            //            }
            //            await quoteModule.clickTab("Rental");
            //            await quoteModule.addGridRow('OrderItemGrid', 'R', null, fieldObject)
            //                .then()
            //                .catch(err => logger.error('saveRecord: ', err));
            //        }, 200000);
            //        test('Save new Quote', async () => {
            //            await quoteModule.saveRecord()
            //                .then()
            //                .catch(err => logger.error('saveRecord: ', err));
            //        }, 10000);
        }
        //---------------------------------------------------------------------------------------



    }
}

new RegressionTest().Run();
