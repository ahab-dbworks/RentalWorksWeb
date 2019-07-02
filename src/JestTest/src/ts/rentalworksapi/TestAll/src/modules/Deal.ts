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
    
    describe('Deal', function() {
        it('can get all records (v1)', async() => {
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/deal`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('deals');
                    testResults.records = response.deals;    
                });
        });
        it('can get one by dealid (v1)', async() => {
            let record: any = null;
            expect(testResults.records.length > 0).toBe(true);
            for (let i = 0; i < testResults.records.length; i++) {
                record = testResults.records[i];
                break;
            }
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/deal/${record.dealid}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('deal');   
                });
        });
        it('can get one by dealno (v1)', async() => {
            let record: any = null;
            expect(testResults.records.length > 0).toBe(true);
            for (let i = 0; i < testResults.records.length; i++) {
                record = testResults.records[i];
                break;
            }
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/deal/dealno=${record.dealno}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('deal');   
                });
        });
        it('can add a record (v1)', async() => {
            let customer
            testResults.addRequest = {
                "dealid": "",
                "dealno": faker.random.alphaNumeric(8).toUpperCase(),
                "customer": faker.company.companyName(),
                "customerid": "B0002LXR",
                "dealname": "4WALL - SAM",
                "dealtype": "FEATURE FILM",
                "dealstatus": "ACTIVE",
                "address": {
                  "address1": "1234 Anystreet",
                  "address2": "",
                  "city": "",
                  "state": "",
                  "zipcode": "",
                  "country": "US"
                },
                "phone": "",
                "fax": "",
                "creditstatus": "",
                "phone800": "",
                "phoneother": "",
                "billperiod": "WEEKLY",
                "officelocation": "LOS ANGELES",
                "paymentterms": "",
                "paymenttype": "",
                "porequired": ""
            };
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/deal/save`);
            ajaxOptions.data = testResults.addRequest;
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('deal');
                    expect(response.deal.dealid.length > 0).toBe(true);
                    testResults.addResult = response.deal;    
                    expect(testResults.addResult).toBe({});
                });
        });
        it('can edit a record (v1)', async() => {
            expect(testResults.addResult.dealid.length > 0).toBe(true);
            testResults.editRequest = {
                "dealid": testResults.addResult.dealid,
                "customer": "4WALL",
                "customerid": "B0002LXR",
                "dealname": "4WALL - SAM",
                "dealtype": "FEATURE FILM",
                "dealstatus": "ACTIVE",
                "address": {
                  "address1": "1234 Anystreet",
                  "address2": "",
                  "city": "",
                  "state": "",
                  "zipcode": "",
                  "country": "US"
                },
                "phone": "",
                "fax": "",
                "creditstatus": "",
                "phone800": "",
                "phoneother": "",
                "billperiod": "WEEKLY",
                "officelocation": "LOS ANGELES",
                "paymentterms": "",
                "paymenttype": "",
                "porequired": ""
            };
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/deal/save`);
            ajaxOptions.data = testResults.editRequest;
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('deal');
                    testResults.editResult = response.deal;    
                });
        });
    });

    return testResults;
}