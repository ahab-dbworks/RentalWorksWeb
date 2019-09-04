import faker from 'faker';
import { BaseTest } from '../shared/BaseTest';
//import { TestUtils } from '../shared/TestUtils';
import { Vendor } from './modules/Vendor';
import { Customer } from './modules/Customer';
import { Deal } from './modules/Deal';
import { Quote } from './modules/Quote';

export class RegressionTest extends BaseTest {
    //---------------------------------------------------------------------------------------
    PerformTests() {
        var vendorInputs: any;
        var customerInputs: any;
        var dealInputs: any;
        var quoteInputs: any;

        if (this.continueTest) {
            const vendorModule: Vendor = new Vendor();
            vendorInputs = {
                Vendor: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                VendorNumber: faker.random.alphaNumeric(8),
                Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                City: faker.address.city(),
                State: faker.address.state(true),
                ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                WebAddress: faker.internet.url(),
                OfficeLocation: "LAS VEGAS"
            }

            var vendorExpected: any = {
                Vendor: vendorInputs.Vendor.toUpperCase(),
                VendorNumber: vendorInputs.VendorNumber.toUpperCase(),
                Address1: vendorInputs.Address1.toUpperCase(),
                Address2: vendorInputs.Address2.toUpperCase(),
                City: vendorInputs.City.toUpperCase(),
                State: vendorInputs.State.toUpperCase(),
                ZipCode: vendorInputs.ZipCode.toUpperCase(),
                //Phone: faker.phone.phoneNumber(),
                //Fax: faker.phone.phoneNumber(),
                WebAddress: vendorInputs.WebAddress,
                OfficeLocation: vendorInputs.OfficeLocation
            }

            // attempt to create a valid vendor using "vendorInputs", compare the values the system saves with the "vendorExpected" object
            this.TestModule(vendorModule, vendorInputs, vendorExpected);

            // vendor record with a blank Vendor Number
            var missingVendorNumberVendorInputs: any = {
                Vendor: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                //VendorNumber: faker.random.alphaNumeric(8),
                Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                City: faker.address.city(),
                State: faker.address.state(true),
                ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                WebAddress: faker.internet.url(),
                OfficeLocation: "LAS VEGAS"
            }
            this.TestModuleForMissingRequiredField(vendorModule, missingVendorNumberVendorInputs, "VendorNumber");
        }

        //-------------//
        //  CUSTOMER   //
        //-------------//
        if (this.continueTest) {
            const customerModule: Customer = new Customer();
            customerInputs = {
                Customer: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                CustomerNumber: faker.random.alphaNumeric(8),
                Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                City: faker.address.city(),
                State: faker.address.state(true),
                ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                WebAddress: faker.internet.url(),
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
                //Phone: faker.phone.phoneNumber(),
                //Fax: faker.phone.phoneNumber(),
                WebAddress: customerInputs.WebAddress,
            }

            // attempt to create a valid customer using "customerInputs", compare the values the system saves with the "customerExpected" object
            this.TestModule(customerModule, customerInputs, customerExpected);

            // change everything except the Customer Name
            var duplicateCustomer1Inputs: any = {
                Customer: customerInputs.Customer,
                CustomerNumber: faker.random.alphaNumeric(8),
                Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                City: faker.address.city(),
                State: faker.address.state(true),
                ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                WebAddress: faker.internet.url(),
                CustomerTypeId: 1,
                CreditStatusId: 1
            }


            // attempt to create a duplicate customer using the same Customer Name
            this.TestModuleForDuplicate(customerModule, duplicateCustomer1Inputs, 'Customer Name');


            // change everything except the Customer Number
            var duplicateCustomer2Inputs: any = {
                Customer: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                CustomerNumber: customerInputs.CustomerNumber,
                Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                City: faker.address.city(),
                State: faker.address.state(true),
                ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                WebAddress: faker.internet.url(),
                CustomerTypeId: 1,
                CreditStatusId: 1
            }

            // attempt to create a duplicate customer using the same Customer Number
            this.TestModuleForDuplicate(customerModule, duplicateCustomer2Inputs, 'Customer Number');

        }


        //-------------//
        //    DEAL     //
        //-------------//
        if (this.continueTest) {
            const dealModule: Deal = new Deal();

            dealInputs = {
                Deal: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                DealNumber: faker.random.alphaNumeric(8),
                Customer: customerInputs.Customer,
                //Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                //City: faker.address.city(),
                //State: faker.address.state(true),
                //ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
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
                //Phone: faker.phone.phoneNumber(),
                //Fax: faker.phone.phoneNumber(),
            }

            // attempt to create a valid deal using "dealInputs", compare the values the system saves with the "dealExpected" object
            this.TestModule(dealModule, dealInputs, dealExpected);


            // change all other fields except Deal name
            var duplicateDeal1Inputs: any = {
                Deal: dealInputs.Deal,
                DealNumber: faker.random.alphaNumeric(8),
                Customer: customerInputs.Customer,
                //Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                //City: faker.address.city(),
                //State: faker.address.state(true),
                //ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                DealTypeId: 1
            }

            // attempt to create a duplicate deal using the same Deal Name
            this.TestModuleForDuplicate(dealModule, duplicateDeal1Inputs, 'Deal Name');

            // change all other fields except Deal Number
            var duplicateDeal2Inputs: any = {
                Deal: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                DealNumber: dealInputs.DealNumber,
                Customer: customerInputs.Customer,
                //Address1: faker.address.streetAddress(),
                Address2: faker.address.secondaryAddress(),
                //City: faker.address.city(),
                //State: faker.address.state(true),
                //ZipCode: faker.address.zipCode("99999"),
                Phone: faker.phone.phoneNumber(),
                Fax: faker.phone.phoneNumber(),
                DealTypeId: 1
            }

            // attempt to create a duplicate deal using the same Deal Number
            this.TestModuleForDuplicate(dealModule, duplicateDeal2Inputs, 'Deal Number');

        }


        //-------------//
        //    QUOTE    //
        //-------------//
        if (this.continueTest) {
            const quoteModule: Quote = new Quote();

            quoteInputs = {
                Deal: dealInputs.Deal,
                Description: `JEST - ${faker.name.jobTitle().substring(0, 25)} - ${this.testToken}`,
                Location: faker.address.streetName(),
                ReferenceNumber: faker.random.alphaNumeric(8)
            }

            var quoteExpected: any = {
                Deal: quoteInputs.Deal.toUpperCase(),
                Description: quoteInputs.Description.toUpperCase(),
                QuoteNumber: "|NOTEMPTY|",
                Location: quoteInputs.Location.toUpperCase(),
                ReferenceNumber: quoteInputs.ReferenceNumber.toUpperCase()
            }

            // attempt to create a valid Quote using "quoteInputs", compare the values the system saves with the "quoteExpected" object
            this.TestModule(quoteModule, quoteInputs, quoteExpected);

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
    }
    //---------------------------------------------------------------------------------------
}

new RegressionTest().Run();
