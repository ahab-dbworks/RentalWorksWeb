require('dotenv').config();
import {FwAjax, FwAjaxOptions} from '../../../fwjest/FwAjax';

export function getAnonymousAjaxOptions(url: string): FwAjaxOptions {
    var ajaxOptions = new FwAjaxOptions();
    ajaxOptions.url = url;
    ajaxOptions.headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    };
    ajaxOptions.request.timeout = 15000;
    return ajaxOptions;
}

export function getUserAjaxOptions(url: string): FwAjaxOptions {
    var ajaxOptions = getAnonymousAjaxOptions(url);
    ajaxOptions.headers.Authorization = `Bearer ${process.env.WEBAPI_USER_JWT}`;
    return ajaxOptions;
}

export function getContactAjaxOptions(url: string): FwAjaxOptions {
    var ajaxOptions = getAnonymousAjaxOptions(url);
    ajaxOptions.headers.Authorization = `Bearer ${process.env.WEBAPI_CONTACT_JWT}`;
    return ajaxOptions;
}