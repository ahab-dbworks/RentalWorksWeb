import faker from 'faker';
import { BaseTest } from '../shared/BaseTest';
import { Customer } from './modules/Customer';
import { Deal } from './modules/Deal';

export class RegressionTest extends BaseTest {
    PerformTests() {

        let testName: string = "";
        let customerInputs: any;
        let customerResolved: any;
        let duplicateCustomer1Inputs: any;
        let duplicateCustomer2Inputs: any;
        let dealInputs: any;
        let dealResolved: any;

        //-------------
        //CUSTOMER
        //------------
        if (this.continueTest) {
            const customerModule: Customer = new Customer();
            describe('Create new Customer, fill out form, save record', () => {
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Open Customer Module';
                    test(testName, async () => {
                        await customerModule.openModule().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Open Customer Form in New mode';
                    test(testName, async () => {
                        await customerModule.createNewRecord().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Fill in Customer form data';
                    test(testName, async () => {
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
                            WebAddress: faker.internet.url()
                        }
                        await customerModule.populateNew(customerInputs).then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Save new Customer';
                    test(testName, async () => {
                        let saved: boolean | void = await customerModule.saveRecord().then().catch(err => this.LogError(testName, err));
                        expect(saved).toBe(true);
                        this.continueTest = saved;
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Check Customer';
                    test(testName, async () => {
                        customerResolved = await customerModule.getCustomer().then().catch(err => this.LogError(testName, err));
                        expect(customerResolved.Customer).toBe(customerInputs.Customer.toUpperCase());
                        expect(customerResolved.CustomerNumber).toBe(customerInputs.CustomerNumber.toUpperCase());
                        expect(customerResolved.Address1).toBe(customerInputs.Address1.toUpperCase());
                        expect(customerResolved.Address2).toBe(customerInputs.Address2.toUpperCase());
                        expect(customerResolved.City).toBe(customerInputs.City.toUpperCase());
                        expect(customerResolved.State).toBe(customerInputs.State.toUpperCase());
                        expect(customerResolved.ZipCode).toBe(customerInputs.ZipCode.toUpperCase());
                        //expect(customerResolved.Phone).toBe(customerInputs.Phone.toUpperCase());
                        //expect(customerResolved.Fax).toBe(customerInputs.Fax.toUpperCase());
                        expect(customerResolved.WebAddress).toBe(customerInputs.WebAddress);
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Close Record';
                    test(testName, async () => {
                        await customerModule.closeRecord().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
            });


            describe('Attempt to create a duplicate Customer Name', () => {
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Open Customer Module';
                    test(testName, async () => {
                        await customerModule.openModule().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Open Customer Form in New mode';
                    test(testName, async () => {
                        await customerModule.createNewRecord().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Fill in Customer form data';
                    test(testName, async () => {
                        // change everything except the Customer Name
                        duplicateCustomer1Inputs = {
                            Customer: customerInputs.Customer,
                            CustomerNumber: faker.random.alphaNumeric(8),
                            Address1: faker.address.streetAddress(),
                            Address2: faker.address.secondaryAddress(),
                            City: faker.address.city(),
                            State: faker.address.state(true),
                            ZipCode: faker.address.zipCode("99999"),
                            Phone: faker.phone.phoneNumber(),
                            Fax: faker.phone.phoneNumber(),
                            WebAddress: faker.internet.url()
                        }
                        await customerModule.populateNew(duplicateCustomer1Inputs).then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Try to save new Customer (expect error)';
                    test(testName, async () => {
                        this.continueTest = await customerModule.saveRecord().then().catch(err => this.LogError(testName, err));
                        expect(this.continueTest).toBe(false);
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Detect error message notification popup';
                    test(testName, async () => {
                        await customerModule.checkForDuplicatePrompt().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Close duplicate notification popup';
                    test(testName, async () => {
                        await customerModule.closeDuplicatePrompt().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Close Record';
                    test(testName, async () => {
                        await customerModule.closeModifiedRecordWithoutSaving().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
            });

            describe('Attempt to create a duplicate Customer Numer', () => {
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Open Customer Module';
                    test(testName, async () => {
                        await customerModule.openModule().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Open Customer Form in New mode';
                    test(testName, async () => {
                        await customerModule.createNewRecord().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Fill in Customer form data';
                    test(testName, async () => {
                        // change everything except the Customer Number
                        duplicateCustomer1Inputs = {
                            Customer: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                            CustomerNumber: customerInputs.CustomerNumber,
                            Address1: faker.address.streetAddress(),
                            Address2: faker.address.secondaryAddress(),
                            City: faker.address.city(),
                            State: faker.address.state(true),
                            ZipCode: faker.address.zipCode("99999"),
                            Phone: faker.phone.phoneNumber(),
                            Fax: faker.phone.phoneNumber(),
                            WebAddress: faker.internet.url()
                        }
                        await customerModule.populateNew(duplicateCustomer1Inputs).then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Try to save new Customer (expect error)';
                    test(testName, async () => {
                        this.continueTest = await customerModule.saveRecord().then().catch(err => this.LogError(testName, err));
                        expect(this.continueTest).toBe(false);
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Detect error message notification popup';
                    test(testName, async () => {
                        await customerModule.checkForDuplicatePrompt().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Close duplicate notification popup';
                    test(testName, async () => {
                        await customerModule.closeDuplicatePrompt().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
                if (this.continueTest) {
                    testName = 'Close Record';
                    test(testName, async () => {
                        await customerModule.closeModifiedRecordWithoutSaving().then().catch(err => this.LogError(testName, err));
                    }, this.testTimeout);
                }
                //---------------------------------------------------------------------------------------
            });
        }


        //-------------
        //DEAL
        //------------
        if (this.continueTest) {
            const dealModule: Deal = new Deal();
            describe('Create new Deal, fill out form, save record', () => {
                //---------------------------------------------------------------------------------------
                testName = 'Open Deal Module';
                test(testName, async () => {
                    await dealModule.openModule().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
                //---------------------------------------------------------------------------------------
                testName = 'Open Deal Form in New mode';
                test(testName, async () => {
                    await dealModule.createNewRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
                //---------------------------------------------------------------------------------------
                testName = 'Fill in Deal form data';
                test(testName, async () => {
                    dealInputs = {
                        Customer: customerResolved.Customer,
                        Deal: `JEST - ${faker.company.companyName()} - ${this.testToken}`,
                        DealNumber: faker.random.alphaNumeric(8),
                        //Address1: faker.address.streetAddress(),
                        Address2: faker.address.secondaryAddress(),
                        //City: faker.address.city(),
                        //State: faker.address.state(true),
                        //ZipCode: faker.address.zipCode("99999"),
                        Phone: faker.phone.phoneNumber(),
                        Fax: faker.phone.phoneNumber(),
                        //WebAddress: faker.internet.url()
                    }
                    await dealModule.populateNew(dealInputs).then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
                //---------------------------------------------------------------------------------------
                testName = 'Save new Deal';
                test(testName, async () => {
                    await dealModule.saveRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
                //---------------------------------------------------------------------------------------
                testName = 'Check Deal';
                test(testName, async () => {
                    dealResolved = await dealModule.getDeal().then().catch(err => this.LogError(testName, err));
                    expect(dealResolved.Deal).toBe(dealInputs.Deal.toUpperCase());
                    expect(dealResolved.Customer).toBe(dealInputs.Customer.toUpperCase());
                    expect(dealResolved.DealNumber).toBe(dealInputs.DealNumber.toUpperCase());
                    expect(dealResolved.Address1).toBe(customerInputs.Address1.toUpperCase());
                    expect(dealResolved.Address2).toBe(dealInputs.Address2.toUpperCase());
                    expect(dealResolved.City).toBe(customerInputs.City.toUpperCase());
                    expect(dealResolved.State).toBe(customerInputs.State.toUpperCase());
                    expect(dealResolved.ZipCode).toBe(customerInputs.ZipCode.toUpperCase());
                    //expect(dealResolved.Phone).toBe(dealInputs.Phone.toUpperCase());
                    //expect(dealResolved.Fax).toBe(dealInputs.Fax.toUpperCase());
                }, this.testTimeout);
                //---------------------------------------------------------------------------------------
                testName = 'Close Record';
                test(testName, async () => {
                    await dealModule.closeRecord().then().catch(err => this.LogError(testName, err));
                }, this.testTimeout);
                //---------------------------------------------------------------------------------------
            });
        }


        ////quote
        //if (continueTest) {
        //    const quoteModule: Quote = new Quote();
        //    describe('Create new Quote, fill out form, save record', () => {
        //        test('Open quoteModule and create', async () => {
        //            await quoteModule.openModule()
        //                .then()
        //                .catch(err => logger.error('openModule: ', err));
        //        }, 10000);
        //        test('Create New record', async () => {
        //            await quoteModule.createNewRecord(1)
        //                .then()
        //                .catch(err => logger.error('createNewRecord: ', err));
        //        }, 10000);
        //        test('Fill in Quote form data', async () => {
        //            quoteDescription = await quoteModule.populateNew(dealName)
        //                .then()
        //                .catch(err => logger.error('populateNewQuote: ', err))
        //        }, 10000);
        //        test('Save new Quote', async () => {
        //            await quoteModule.saveRecord()
        //                .then()
        //                .catch(err => logger.error('saveRecord: ', err));
        //        }, 10000);


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



        //        test('Close Record', async () => {
        //            await quoteModule.closeRecord()
        //                .then()
        //                .catch(err => logger.error('closeRecord: ', err));
        //        }, 10000);
        //    });
        //}

    }
}

new RegressionTest().Run();
