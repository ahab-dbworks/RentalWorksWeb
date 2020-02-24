import { AccountTestResults } from "../../AccountServices/Account/Account";
import { FwBrowseRequest, FwJsonDataTable } from "../../../../../fwjest/FwBrowse";
import { WebApiUtil } from "../../../shared/WebApiUtil";
import { FwAjax } from "../../../../../fwjest/FwAjax";
import faker from 'faker'

const moduleName = 'customer';
const primaryKeyField = 'CustomerId';

export class CustomerTestResults {
    browse: any;
    records: any[];
    addRequest: any;
    addResult: any;
    editRequest: any;
    editResult: any;
}

export class Customer {
    static test(account: AccountTestResults): CustomerTestResults {
        let testResults = new CustomerTestResults();
    
        describe('Customer', function() {
            it('can browse (v1)', async() => {
                try {
                    const ajaxOptions = WebApiUtil.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}/browse`);
                    const browseRequest = new FwBrowseRequest();
                    // browseRequest.filterfields = {
                    //     CampusId: account.officerSession.Campus.CampusId
                    // }
                    ajaxOptions.data = browseRequest;
                    const response = await FwAjax.postJson<any>(ajaxOptions);
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('Rows');
                    testResults.browse = response;
                    testResults.records = FwJsonDataTable.toObjectList(response);
                    //console.log(testResults.records);
                } catch(reason) {
                    fail(reason);
                }
            });
            // it('can getmany (v1)', async() => {
            //     try {
            //         const ajaxOptions = constants.getOfficerAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}`);
            //         const browseRequest = new BrowseRequest();
            //         ajaxOptions.data = browseRequest;
            //         const response = await Ajax.get<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         expect(typeof response).toBe('object');
            //         testResults.records = response;
            //         expect(response.length).toBeGreaterThan(0);
            //         let restrictedPersons = response;
            //         expect(typeof restrictedPersons[0][primaryKeyField]).toBe('string');
            //         expect(restrictedPersons[0][primaryKeyField].length).toBeGreaterThan(0);
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
            // it('can get one record (v1)', async() => {
            //     try {
            //         let record: any = null;
            //         for (let i = 0; i < testResults.records.length; i++) {
            //             record = testResults.records[i];
            //             break;
            //         }
            //         var ajaxOptions = WebApiUtil.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}/${record[primaryKeyField]}`);
            //         const response = await FwAjax.get<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         expect(typeof response).toBe('object');
            //         expect(response).toHaveProperty(primaryKeyField);   
            //         //console.log(response);
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
            // it('can add a record (V1)', async() => {
            //     try {
            //         testResults.addRequest = Object.assign({}, testResults.records[0]); // copy the first record
            //         testResults.addRequest[primaryKeyField] = '';
            //         testResults.addRequest.FirstName = faker.name.firstName().toUpperCase();
            //         testResults.addRequest.LastName = faker.name.lastName().toUpperCase();
            //         var ajaxOptions = WebApiUtil.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}`);
            //         ajaxOptions.data = testResults.addRequest;
            //         const response = await FwAjax.postJson<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         expect(typeof response).toBe('object');
            //         expect(response).toHaveProperty(primaryKeyField);
            //         expect(response[primaryKeyField].length > 0).toBe(true);
            //         testResults.addResult = response;
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
            // it('can edit a record (v1)', async() => {
            //     try {
            //         expect(testResults.addResult.PersonId.length > 0).toBe(true);
            //         testResults.editRequest = Object.assign({}, testResults.addResult); // make a copy of the addResult
            //         //console.log(testResults.editRequest);
            //         var ajaxOptions = WebApiUtil.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}/${testResults.addResult.PersonId}`);
            //         ajaxOptions.data = testResults.editRequest;
            //         const response = await FwAjax.putJson<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         expect(response).toHaveProperty(primaryKeyField);
            //         testResults.editResult = response;    
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
            // it('can delete a record (v1)', async() => {
            //     try {
            //         expect(testResults.addResult.PersonId.length > 0).toBe(true);
            //         var ajaxOptions = WebApiUtil.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}/${testResults.addResult.PersonId}`);
            //         ajaxOptions.data = testResults.editRequest;
            //         const response = await FwAjax.delete<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         testResults.editResult = response;    
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
    
            //==========================================================================================================================================
            // Documents Grid
            it('Documents Grid - can browse (v1)', async() => {
                try {
                    const ajaxOptions = WebApiUtil.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}/A000024Y/document/browse`);
                    const browseRequest = new FwBrowseRequest();
                    browseRequest.filterfields = {
                        //"CampusId": "0002OCDR"
                    }
                    ajaxOptions.data = browseRequest;
                    const response = await FwAjax.postJson<any>(ajaxOptions);
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('Rows');
                    testResults.browse = response;
                    testResults.records = FwJsonDataTable.toObjectList(response);
                    //console.log(testResults.records);
                } catch(reason) {
                    fail(reason);
                }
            });
            // it('Documents Grid - can get one record (v1)', async() => {
            //     try {
            //         let record: any = null;
            //         var ajaxOptions = constants.getOfficerAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/${moduleName}/A000024Y/document/A001U5HF`);
            //         const response = await Ajax.get<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         expect(typeof response).toBe('object');
            //         expect(response).toHaveProperty('AkaId');   
            //         console.log(response);
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
            //==========================================================================================================================================
            
        });

        return testResults;
    }
}