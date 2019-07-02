require('dotenv').config();
import {Ajax, AjaxOptions} from '../../../../shared/Ajax';
import faker from 'faker';
import * as constants from '../shared/Constants';

export class TestResults {
    userSession: any;
    contactSession: any;
}

export function test(): TestResults {
    let testResults = new TestResults();
    describe('Account', function() {
        it('can get User Session (v1)', async() => {
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/account/login`);
            ajaxOptions.data = {
                email: process.env.RWAPI_CONTACT_EMAIL,
                password: process.env.RWAPI_CONTACT_PASSWORD
            };
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('user');
                    testResults.userSession = response.user;    
                });
        });

        it('can get Contact Session (v1)', async() => {
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/account/login`);
            ajaxOptions.data = {
                email: process.env.RWAPI_CONTACT_EMAIL,
                password: process.env.RWAPI_CONTACT_PASSWORD
            };
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('user');
                    testResults.contactSession = response.user;
                });
        });
    });
    return testResults;
}