require('dotenv').config();
import {FwAjax, FwAjaxOptions} from '../../../../../fwjest/FwAjax';
import faker from 'faker';
import * as util from '../../../shared/Util';

export class AccountTestResults {
    userSession: any;
    contactSession: any;
}

export interface SessionModel {
    applicationOptions: any;
    applicationtree: SecurityTreeNode;
    clientcode: string;
    deal: any | null;
    department: SessionDepartment;
    location: SessionLocation;
    serverVersion: string;
    warehouse: SessionWarehouse;
    webUser: SessionWebUser;
}

export interface SessionDeal {
    DealId: string;
    DealName: string;
}

export interface SessionDepartment {
    department: string;
    departmentid: string;
}

export interface SessionLocation {
    companyname: string;
    location: string;
    locationcolor: string;
    locationid: string;
    ratetype: string;
}

export interface SessionWarehouse {
    promptforcheckinexceptions: string;
    promptforcheckoutexceptions: string;
    warehouse: string;
    warehouseid: string;
}

export interface SessionWarehouse {
    promptforcheckinexceptions: string;
    promptforcheckoutexceptions: string;
    warehouse: string;
    warehouseid: string;
}

export interface SessionWebUser {
    applicationtheme: string;
    browsedefaultrows: string;
    contactid: string;
    department: string;
    departmentid: string;
    email: string;
    fullname: string;
    location: string;
    locationid: string;
    name: string;
    usersid: string;
    usertype: string;
    warehouse: string;
    warehouseid: string;
    webadministrator: boolean;
    webusersid: string;
}

export interface SecurityTreeNode {
    id: string;
    caption: string;
    nodetype: string;
    properties: {[key: string]: string};
    children: SecurityTreeNode[];

}

export class Account {
    static test(): AccountTestResults {
        let testResults = new AccountTestResults();
        describe('Account', function() {
            it('can get User Session (v1)', async() => {
                try {
                    const ajaxOptions = util.getUserAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/account/session?applicationid={0A5F2584-D239-480F-8312-7C2B552A30BA}`);
                    const response = await FwAjax.get<any>(ajaxOptions);
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('webUser');
                    expect(response.webUser).toHaveProperty('usersid');
                    expect(response.webUser.usersid.length).toBeGreaterThan(0);
                    expect(response.webUser).toHaveProperty('usertype');
                    expect(response.webUser.usertype).toEqual('USER');
                    testResults.userSession = response;
                } catch(reason) {
                    fail(reason);
                }
            });

            // this is going to fail until contacts have a security tree
            // it('can get Contact Session (v1)', async() => {
            //     try {
            //         const ajaxOptions = util.getContactAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/account/session?applicationid={0A5F2584-D239-480F-8312-7C2B552A30BA}`);
            //         const response = await FwAjax.get<any>(ajaxOptions);
            //         expect(ajaxOptions.request.status).toEqual(200);
            //         expect(response).toHaveProperty('webUser');
            //         expect(response.webUser).toHaveProperty('contactid');
            //         expect(response.webUser.contactid.length).toBeGreaterThan(0);
            //         expect(response.webUser).toHaveProperty('usertype');
            //         expect(response.webUser.usertype).toEqual('CONTACT');
            //         testResults.contactSession = response;
            //     } catch(reason) {
            //         fail(reason);
            //     }
            // });
        });
        return testResults;
    }
}
