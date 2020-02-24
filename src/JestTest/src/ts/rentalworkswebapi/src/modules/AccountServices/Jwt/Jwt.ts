require('dotenv').config();
import {FwAjax, FwAjaxOptions} from '../../../../../fwjest/FwAjax';
import faker from 'faker';
import { WebApiUtil } from '../../../shared/WebApiUtil';

export class JwtTestResults {
    officerJwt: string;
    requesterJwt: string;
    userJwt: string;
}

export class Jwt {
    static test(): JwtTestResults {
        let testResults = new JwtTestResults();
        describe('Jwt', function() {
            it('can\'t get Jwt Token with an invalid username and password (v1)', async() => {
                try {
                    const ajaxOptions = WebApiUtil.getAnonymousAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/jwt`);
                    ajaxOptions.data = {
                        UserName: 'oxdLQCGZh8ui',
                        Password: 'm6MItvrylGBN'
                    };
                    const response = await FwAjax.postJson<any>(ajaxOptions);
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(typeof response.access_token).toBe('undefined');
                    expect(response).toHaveProperty('statuscode');
                    expect(response).toHaveProperty('statusmessage');    
                    expect(response.statuscode).toEqual(401);
                } catch(reason) {
                    fail(reason);
                }
            });
    
            it('can get Jwt Token for User (v1)', async() => {
                try {
                    const ajaxOptions = WebApiUtil.getAnonymousAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/jwt`);
                    ajaxOptions.data = {
                        UserName: process.env.WEBAPI_USER_USERNAME,
                        Password: process.env.WEBAPI_USER_PASSWORD
                    };
                    const response = await FwAjax.postJson<any>(ajaxOptions);
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('access_token');
                    testResults.userJwt = response.access_token;
                    process.env.WEBAPI_USER_JWT = response.access_token;
                } catch(reason) {
                    fail(reason);
                }    
            });

            it('can get Jwt Token for Contact (v1)', async() => {
                try {
                    const ajaxOptions = WebApiUtil.getAnonymousAjaxOptions(`${process.env.WEBAPI_BASEURL}/api/v1/jwt`);
                    ajaxOptions.data = {
                        UserName: process.env.WEBAPI_CONTACT_EMAIL,
                        Password: process.env.WEBAPI_CONTACT_PASSWORD
                    };
                    const response = await FwAjax.postJson<any>(ajaxOptions);
                    expect(ajaxOptions.request.status).toEqual(200);
                    expect(response).toHaveProperty('access_token');
                    testResults.userJwt = response.access_token;
                    process.env.WEBAPI_CONTACT_JWT = response.access_token;
                } catch(reason) {
                    fail(reason);
                }    
            });
        });
        return testResults;
    }
}