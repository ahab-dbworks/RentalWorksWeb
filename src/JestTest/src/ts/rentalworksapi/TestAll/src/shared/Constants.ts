require('dotenv').config();
import {Ajax, AjaxOptions} from '../../../../shared/Ajax';

export function getDefaultAjaxOptions(url: string): any {
    var ajaxOptions = new AjaxOptions();
    ajaxOptions.url = url;
    ajaxOptions.headers = {
        'Authorization': 'Basic ' + btoa(`${process.env.RWAPI_APIUSERNAME}:${process.env.RWAPI_APIPASSWORD}`),
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    };
    return ajaxOptions;
}
