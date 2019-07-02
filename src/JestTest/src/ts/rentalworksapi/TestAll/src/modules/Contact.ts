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
    
    describe('Contact', function() {
        it('can get all records (v1)', async() => {
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/contact`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('contacts');
                    testResults.records = response.contacts;    
                });
        });
        it('can get one record (v1)', async() => {
            let record: any = null;
            for (let i = 0; i < testResults.records.length; i++) {
                record = testResults.records[i];
                break;
            }
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/contact/${record.contactid}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('contact');
                    //testResults.records = response.contact;    
                });
        });
        it('can add a record (V1)', async() => {
            testResults.addRequest = {
                "contactid": "",
                "firstname": faker.name.firstName().toUpperCase(),
                "middlename": faker.name.firstName().toUpperCase(),
                "lastname": faker.name.lastName().toUpperCase(),
                "email": faker.internet.email().toUpperCase(),
                "address": {
                  "address1": faker.address.streetAddress().toUpperCase(),
                  "address2": faker.address.secondaryAddress().toUpperCase(),
                  "city": faker.address.city().toUpperCase(),
                  "state": faker.address.stateAbbr().toUpperCase(),
                  "zipcode": faker.address.zipCode(),
                  "country": faker.address.countryCode().toUpperCase()
                },
                "officephone": faker.phone.phoneNumber('(###) ###-####'),
                "officeext": "123",
                "homephone": faker.phone.phoneNumber('(###) ###-####'),
                "fax": faker.phone.phoneNumber('(###) ###-####'),
                "cellular": faker.phone.phoneNumber('(###) ###-####'),
                "contacttitle": "",
                "employedbyid": "",
                "employedby": "",
                "employedbyno": "",
                "jobtitle": faker.name.jobTitle(),
                "status": "",
                "authorized": "",
                "barcode": "",
                "companytype": "",
                "directext": "",
                "directphone": faker.phone.phoneNumber('(###) ###-####'),
                "faxext": "",
                "pager": "",
                "pagerpin": "",
                "primarycontact": "",
                "sourceid": ""
            };
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/contact/save`);
            ajaxOptions.data = testResults.addRequest;
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('contact');
                    expect(response.contact.contactid.length > 0).toBe(true);
                    testResults.addResult = response.contact;    
                });
        });
        it('can edit a record (v1)', async() => {
            expect(testResults.addResult.contactid.length > 0).toBe(true);
            testResults.editRequest = {
                "contactid": testResults.addResult.contactid,
                "firstname": faker.name.firstName().toUpperCase(),
                "middlename": faker.name.firstName().toUpperCase(),
                "lastname": faker.name.lastName().toUpperCase(),
                "email": faker.internet.email().toUpperCase(),
                "address": {
                  "address1": faker.address.streetAddress().toUpperCase(),
                  "address2": faker.address.secondaryAddress().toUpperCase(),
                  "city": faker.address.city().toUpperCase(),
                  "state": faker.address.stateAbbr().toUpperCase(),
                  "zipcode": faker.address.zipCode(),
                  "country": faker.address.countryCode().toUpperCase()
                },
                "officephone": faker.phone.phoneNumber('(###) ###-####'),
                "officeext": "123",
                "homephone": faker.phone.phoneNumber('(###) ###-####'),
                "fax": faker.phone.phoneNumber('(###) ###-####'),
                "cellular": faker.phone.phoneNumber('(###) ###-####'),
                "contacttitle": "",
                "employedbyid": "",
                "employedby": "",
                "employedbyno": "",
                "jobtitle": faker.name.jobTitle(),
                "status": "",
                "authorized": "",
                "barcode": "",
                "companytype": "",
                "directext": "",
                "directphone": faker.phone.phoneNumber('(###) ###-####'),
                "faxext": "",
                "pager": "",
                "pagerpin": "",
                "primarycontact": "",
                "sourceid": ""
            };
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/contact/save`);
            ajaxOptions.data = testResults.editRequest;
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('contact');
                    testResults.editResult = response.contact;    
                });
        });
    });

    return testResults;
}