require('dotenv').config();
import {Ajax, AjaxOptions} from '../../../../shared/Ajax';
import faker from 'faker';
import * as constants from '../shared/Constants';

export class TestResults {
    records: any;
    addRequest: any;
    addResult: any;
    editRequest: any;
    editResult: any;
}

export function test(): TestResults {
    let testResults = new TestResults();
    
    describe('Customer', function() {
        it('can get all records (v1)', async() => {
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/customer`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('customers');
                    testResults.records = response.customers;    
                });
        });
        it('can get one by customerid (v1)', async() => {
            let record: any = null;
            expect(testResults.records.length > 0).toBe(true);
            for (let i = 0; i < testResults.records.length; i++) {
                record = testResults.records[i];
                break;
            }
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/customer/${record.customerid}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('customer');
                    //testResults.records = response.customer;    
                });
        });
        it('can get one by customerno (v1)', async() => {
            let record: any = null;
            expect(testResults.records.length > 0).toBe(true);
            for (let i = 0; i < testResults.records.length; i++) {
                record = testResults.records[i];
                break;
            }
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/customer/customerno=${record.customerno}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('customer');
                    //testResults.records = response.customer;    
                });
        });
        it('can get a customer\'s orders (v1)', async() => {
            let record: any = null;
            expect(testResults.records.length > 0).toBe(true);
            for (let i = 0; i < testResults.records.length; i++) {
                record = testResults.records[i];
                break;
            }
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/customer/${record.customerid}/orders`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('customerorders');
                    //testResults.records = response.customer;    
                });
        });
        it('can add a record (v1)', async() => {
            testResults.addRequest = {
                "customerid": "",
                "customerno": faker.random.alphaNumeric(8).toUpperCase(),
                "customername": faker.company.companyName(),
                "customertype": process.env.RWAPI_CUSTOMERTYPE,
                "customercategory": process.env.RWAPI_CUSTOMERCATEGORY,
                "address": {
                    "address1": faker.address.streetAddress(),
                    "address2": faker.address.secondaryAddress(),
                    "city": faker.address.city(),
                    "state": faker.address.state(),
                    "zipcode": faker.address.zipCode(),
                    "country": faker.address.countryCode()
                },
                "website": faker.internet.url(),
                "phone": faker.phone.phoneNumber('(###) ###-####'),
                "fax": faker.phone.phoneNumber('(###) ###-####'),
                "creditstatus": "ACTIVE",
                "creditlimit": "0",
                "billtoaddress": "CUSTOMER",
                "billtoattention": "",
                "certificateofinsurance": "",
                "creditthroughdate": "",
                "insurancevalidthru": "",
                "location": process.env.RWAPI_LOCATION,
                "phone800": faker.phone.phoneNumber('(###) ###-####'),
                "phoneother": faker.phone.phoneNumber('(###) ###-####'),
                "taxable": "T",
                "customerstatus": "ACTIVE",
                "taxfedno": "",
                "department": process.env.RWAPI_COMPANYDEPARTMENT
            };
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/customer/save`);
            ajaxOptions.data = testResults.addRequest;
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('customer');
                    expect(response.customer.customerid.length > 0).toBe(true);
                    testResults.addResult = response.customer;    
                    //expect(testResults.addResult).toBe({});
                });
        });
        it('can edit a record (v1)', async() => {
            expect(testResults.addResult.customerid.length > 0).toBe(true);
            testResults.addRequest = {
                "customerid": testResults.addResult.customerid,
                "customerno": faker.random.alphaNumeric(8).toUpperCase(),
                "customername": faker.company.companyName(),
                "customertype": process.env.RWAPI_CUSTOMERTYPE,
                "customercategory": process.env.RWAPI_CUSTOMERCATEGORY,
                "address": {
                    "address1": faker.address.streetAddress(),
                    "address2": faker.address.secondaryAddress(),
                    "city": faker.address.city(),
                    "state": faker.address.state(),
                    "zipcode": faker.address.zipCode(),
                    "country": faker.address.countryCode()
                },
                "website": faker.internet.url(),
                "phone": faker.phone.phoneNumber('(###) ###-####'),
                "fax": faker.phone.phoneNumber('(###) ###-####'),
                "creditstatus": "ACTIVE",
                "creditlimit": "0",
                "billtoaddress": "CUSTOMER",
                "billtoattention": "",
                "certificateofinsurance": "",
                "creditthroughdate": "",
                "insurancevalidthru": "",
                "location": process.env.RWAPI_LOCATION,
                "phone800": faker.phone.phoneNumber('(###) ###-####'),
                "phoneother": faker.phone.phoneNumber('(###) ###-####'),
                "taxable": "T",
                "customerstatus": "ACTIVE",
                "taxfedno": "",
                "department": process.env.RWAPI_COMPANYDEPARTMENT
            };
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/customer/save`);
            ajaxOptions.data = testResults.editRequest;
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('customer');
                    testResults.editResult = response.customer;    
                });
        });
    });

    return testResults;
}