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
    
    describe('Inventory', function() {
        it('can get all rental inventory as of today (v1)', async() => {
            var date = new Date().toISOString();
            date = date.substring(0, date.indexOf('T'));
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/inventory/rental/${date}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('rentalinventory');
                    testResults.records = response.rentalinventory;    
                });
        });
        it('can get all sales inventory as of today (v1)', async() => {
            var date = new Date().toISOString();
            date = date.substring(0, date.indexOf('T'));
            var ajaxOptions = constants.getDefaultAjaxOptions(`${process.env.RWAPI_BASEURL}/v1/inventory/sales/${date}`);
            await Ajax.postJson<any>(ajaxOptions)
                .then(response => {
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('salesinventory');
                    testResults.records = response.rentalinventory;    
                });
        });
    });

    return testResults;
}