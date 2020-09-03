class FwFunc {

    //---------------------------------------------------------------------------------
    static showError(ex: any): void {
        let $confirmation, $btnOK, message;
        if ((typeof ex === 'object') && (typeof ex.message === 'string')) {
            message = ex.message;
            if ((typeof console.error !== 'undefined') && (typeof ex.stack === 'string')) {
                console.error(ex.message, ex.stack);
            }
        } else if (typeof ex === 'string') {
            message = ex;
            if (typeof console.error !== 'undefined') {
                console.error(ex);
            }
        }
        $confirmation = FwConfirmation.renderConfirmation('Error', message);
        $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
        setTimeout(function () {
            $btnOK.focus();
        }, 100);
    }
    //---------------------------------------------------------------------------------
    static showWebApiError(status: number, error: string | { message: string, stack: string }, responseText: string, fullurl: string) {
        if (status === 401 /*|| status === 403*/) {
            FwNotification.renderNotification('ERROR', `${status} - ${error}`, `Url: ${fullurl}`);
            setTimeout(() => {
                sessionStorage.clear();
                window.location.reload(true);
            }, 3000);
            //FwConfirmation.showMessage(`${status} - ${error}`, `Url: ${fullurl}`, false, true, 'OK',
            //    (event) => {
            //        try {
            //            sessionStorage.clear();
            //            window.location.reload(false);
            //        }
            //        catch (ex) {
            //            FwFunc.showError(ex);
            //        }
            //    }
            //);
        }
        else if (status === 0) {
            //console.error(apiException.Message + '\n' + apiException.StackTrace);
            let $confirmation = FwConfirmation.renderConfirmation('No Response', fullurl);
            let $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
            setTimeout(function () {
                $btnOK.focus();
            }, 100);
        }
        else if (status === 200 && typeof error === 'object' && typeof error.message === 'string' && typeof error.stack === 'string') {
            let message = error.message;
            if (applicationConfig.debugMode) {
                message += '\n' + error.stack;
            }
            console.log(message);
            let $confirmation = FwConfirmation.renderConfirmation(status + ' ' + error, message);
            let $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
            setTimeout(function () {
                $btnOK.focus();
            }, 100);
        }
        else if (status === 400) {
            console.error(responseText);
            let $confirmation = FwConfirmation.renderConfirmation(status + ' ' + error, responseText);
            let $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
            setTimeout(function () {
                $btnOK.focus();
            }, 100);
        }
        else {
            // try to show the Json Error message
            if (typeof responseText === 'string' && responseText.length > 0) {
                try {
                    let apiException = JSON.parse(responseText);
                    console.error(apiException.Message + '\n' + apiException.StackTrace);

                    let $confirmation = FwConfirmation.renderConfirmation(status + ' ' + error, apiException.Message);
                    let $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
                    setTimeout(function () {
                        $btnOK.focus();
                    }, 100);
                } catch (ex) {
                    //console.error(apiException.Message + '\n' + apiException.StackTrace);
                    let $confirmation = FwConfirmation.renderConfirmation(status + ' Error', responseText);
                    let $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
                    setTimeout(function () {
                        $btnOK.focus();
                    }, 100);
                }
            }
        }
    }
    //---------------------------------------------------------------------------------
    static showMessage(message: string, onbuttonclick?: (event: JQuery.ClickEvent) => void): void {
        let $confirmation = FwConfirmation.showMessage('Message', message, false, true, 'OK', onbuttonclick);
        // mv 2016-12-19 This caused a bug in the create contract button, where you could skip the navigation event after create a contract by clicking the background
        //$confirmation.on('click', function() {
        //    FwConfirmation.destroyConfirmation($confirmation);
        //});
        $confirmation.on('click', '.fwconfirmationbox', function (event2: JQuery.ClickEvent) {
            event2.stopPropagation();
        });
    }
    //---------------------------------------------------------------------------------
    static getDate = function (paramdate?: string, modifier?: number): string {
        let date, dd, mm, yyyy;
        if (typeof paramdate === 'undefined') {
            date = new Date();
        } else {
            date = new Date(paramdate)
        }
        if (typeof modifier === 'number') {
            date.setDate(date.getDate() + modifier);
        }
        dd = ((date.getDate() < 10) ? '0' + date.getDate() : date.getDate());
        mm = ((date.getMonth() + 1 < 10) ? '0' + (date.getMonth() + 1) : date.getMonth() + 1); //January is 0!
        yyyy = date.getFullYear();
        date = mm + '/' + dd + '/' + yyyy;

        return date;
    }
    //---------------------------------------------------------------------------------
    static getTime = function (showseconds: boolean): string {
        let date, hh, mm, ss, period;

        date = new Date();
        hh = date.getHours() % 12 || 12;
        mm = ((date.getMinutes() < 10) ? '0' + (date.getMinutes()) : date.getMinutes());
        ss = ((date.getSeconds() < 10) ? '0' + (date.getSeconds()) : date.getSeconds());
        period = ((date.getHours() > 12) ? 'PM' : 'AM');
        if (showseconds) {
            date = hh + ':' + mm + ':' + ss + ' ' + period;
        } else {
            date = hh + ':' + mm + ' ' + period;
        }

        return date;
    }
    //---------------------------------------------------------------------------------
    static htmlEscape(str: string): string {
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
    }
    //---------------------------------------------------------------------------------
    static htmlUnescape(value: string): string {
        return String(value)
            .replace(/&quot;/g, '"')
            .replace(/&#39;/g, "'")
            .replace(/&lt;/g, '<')
            .replace(/&gt;/g, '>')
            .replace(/&amp;/g, '&')
    }
    //---------------------------------------------------------------------------------
    static getMaxZ(selector: string): number {
        var maxZIndex = 1;
        var $elements = jQuery('#fw-app-header,#fw-app-menu,.fwcontextmenu,.fwoverlay,.fwpopup,.fwconfirmation,.submenu');
        for (var i = 0; i < $elements.length; i++) {
            const z: number = +jQuery($elements[i]).css("z-index");
            if (z > maxZIndex) {
                maxZIndex = z;
            }
        }
        return maxZIndex;
    }
    //---------------------------------------------------------------------------------
    static round(num: number, decimalplaces: number): number {
        return Math.round(num * Math.pow(10, decimalplaces)) / Math.pow(10, decimalplaces);
    }
    //---------------------------------------------------------------------------------
    static convert12to24(time: string): string {
        let hours = Number(time.match(/^(\d+)/)[1]);
        let minutes = Number(time.match(/:(\d+)/)[1]);
        let AMPM = time.match(/\s(.*)$/)[1];
        if (AMPM.toUpperCase() == "PM" && hours < 12) hours = hours + 12;
        if (AMPM.toUpperCase() == "AM" && hours == 12) hours = hours - 12;
        let sHours = hours.toString();
        let sMinutes = minutes.toString();
        if (hours < 10) sHours = "0" + sHours;
        if (minutes < 10) sMinutes = "0" + sMinutes;
        return sHours + ":" + sMinutes + ":00";
    }
    //---------------------------------------------------------------------------------
    static convert24to12(time: any): string {
        time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
        if (time.length > 1) {
            time = time.slice(1);
            time[5] = +time[0] < 12 ? ' AM' : ' PM';
            time[0] = +time[0] % 12 || 12;
        }
        return time.join('');
    }
    //---------------------------------------------------------------------------------
    static isDesktop(): boolean {
        let isDesktop = jQuery('html').hasClass('desktop');
        return isDesktop;
    }
    //---------------------------------------------------------------------------------
    static isMobile(): boolean {
        let isMobile = jQuery('html').hasClass('mobile');
        return isMobile;
    }
    //---------------------------------------------------------------------------------
    static fixCaps(string: string) {
        let returnStr;

        if ((string == null) || (string == '')) {
            returnStr = '';
        } else {
            returnStr = string.toLowerCase().replace(/\b[a-z]/g, function (letter) { return letter.toUpperCase(); });
        }

        return returnStr;
    }
    //---------------------------------------------------------------------------------
    static getObjects(obj, key, val) {
        var objects = [];
        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                objects = objects.concat(FwFunc.getObjects(obj[i], key, val));
            } else if (i == key && obj[key] == val) {
                objects.push(obj);
            }
        }
        return objects;
    }
    //---------------------------------------------------------------------------------
    static debounce(func: any, wait: number, immediate?: boolean): any {
        // Returns a function, that, as long as it continues to be invoked, will not be triggered. The function will be called after it stops being called for
        // N milliseconds. If `immediate` is passed, trigger the function on the leading edge, instead of the trailing. Returned function must be invoked.
        let timeout;
        return function () {
            const context = this, args = arguments;
            const later = function () {
                timeout = null;
                if (!immediate) func.apply(context, args);
            };
            const callNow = immediate && !timeout;
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
            if (callNow) func.apply(context, args);
        };
    }
    //---------------------------------------------------------------------------------
    static stringFormat(str: string, ...args: any[]): string {
        return str.replace(/{(\d+)}/g, (match, index) => args[index] || '');
    }
    //---------------------------------------------------------------------------------
    static keys: {
        INSERT: 45,
        DELETE: 46,
        BACKSPACE: 8,
        TAB: 9,
        ENTER: 13,
        ESC: 27,
        LEFT: 37,
        UP: 38,
        RIGHT: 39,
        DOWN: 40,
        END: 35,
        HOME: 36,
        SPACEBAR: 32,
        PAGEUP: 33,
        PAGEDOWN: 34,
        F2: 113,
        F10: 121,
        F12: 123,
        NUMPAD_PLUS: 107,
        NUMPAD_MINUS: 109,
        NUMPAD_DOT: 110
    }
    //---------------------------------------------------------------------------------
    static playSuccessSound() {
        let successSoundUrl = jQuery('#application').attr('data-SuccessSoundUrl');

        if (successSoundUrl !== '') {
            const sound = new Audio(successSoundUrl);
            sound.play();
        } else {
            this.getBase64Sound('Success')
                .then(() => {
                    successSoundUrl = jQuery('#application').attr('data-SuccessSoundUrl');
                    if (successSoundUrl) {
                        const sound = new Audio(successSoundUrl);
                        sound.play();
                    } else {
                        FwNotification.renderNotification('INFO', 'No Success Sound set up. Visit User Profile to choose a sound.')
                    }
                });
        }
    }
    //---------------------------------------------------------------------------------
    static playErrorSound() {
        let errorSoundUrl = jQuery('#application').attr('data-ErrorSoundUrl');
        if (errorSoundUrl) {
            const sound = new Audio(errorSoundUrl);
            sound.play();
        } else {
            this.getBase64Sound('Error')
                .then(() => {
                    errorSoundUrl = jQuery('#application').attr('data-ErrorSoundUrl');
                    if (errorSoundUrl) {
                        const sound = new Audio(errorSoundUrl);
                        sound.play();
                    } else {
                        FwNotification.renderNotification('INFO', 'No Error Sound set up. Visit User Profile to choose a sound.')
                    }
                });
        }
    }
    //---------------------------------------------------------------------------------
    static playNotificationSound() {
        let notificationSoundUrl = jQuery('#application').attr('data-NotificationSoundUrl');
        if (notificationSoundUrl !== '') {
            const sound = new Audio(notificationSoundUrl);
            sound.play();
        } else {
            this.getBase64Sound('Notification')
                .then(() => {
                    notificationSoundUrl = jQuery('#application').attr('data-NotificationSoundUrl');
                    if (notificationSoundUrl) {
                        const sound = new Audio(notificationSoundUrl);
                        sound.play();
                    } else {
                        FwNotification.renderNotification('INFO', 'No Notification Sound set up. Visit User Profile to choose a sound.')
                    }
                });
        }
    }
    //---------------------------------------------------------------------------------
    static getBase64Sound(tag: string, userSettingsObject?: any): Promise<any> {
        // gets base64sound for input tag and loads blob into app and assigns resulting url to DOM for streaming elsewhere (ex. FwFunc.playErrorSound())
        return new Promise<any>(async (resolve, reject) => {
            try {
                if (userSettingsObject) {
                    const base64Sound = userSettingsObject[`${tag}Base64Sound`];
                    const blob = this.b64SoundtoBlob(base64Sound);
                    const blobUrl = URL.createObjectURL(blob);
                    jQuery('#application').attr(`data-${tag}SoundUrl`, blobUrl);
                } else {
                    const webUsersId = JSON.parse(sessionStorage.getItem('userid')).webusersid;
                    const promiseGetUserSettings = FwAjax.callWebApi<any, any>({
                        httpMethod: 'GET',
                        url: `${applicationConfig.apiurl}api/v1/userprofile/${webUsersId}`,
                        $elementToBlock: jQuery('#application'),
                    })
                        .then((responseGetUserSettings: any) => {
                            const base64Sound = responseGetUserSettings[`${tag}Base64Sound`];
                            const blob = this.b64SoundtoBlob(base64Sound);
                            const blobUrl = URL.createObjectURL(blob);
                            jQuery('#application').attr(`data-${tag}SoundUrl`, blobUrl);
                            resolve();
                        });
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    static b64SoundtoBlob(b64Data) {
        const byteCharacters = atob(b64Data.replace(/^data:audio\/(wav|mp3|ogg\mpeg);base64,/, ''));
        const byteArrays = [];

        for (let offset = 0; offset < byteCharacters.length; offset += 512) {
            const slice = byteCharacters.slice(offset, offset + 512);

            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        const blob = new Blob(byteArrays, { type: '' });
        return blob;
    }
    //---------------------------------------------------------------------------------
    static getWeekStartInt(): number {
        let weekStartInt = 0;
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid) {
            if (userid.firstdayofweek) {
                weekStartInt = userid.firstdayofweek;
            }
        } else {
            console.error('userid property not found in sessionStorage. weekStartInt will be defaulted to 0 (Sunday).')
        }

        return weekStartInt;
    }
    //---------------------------------------------------------------------------------
}
