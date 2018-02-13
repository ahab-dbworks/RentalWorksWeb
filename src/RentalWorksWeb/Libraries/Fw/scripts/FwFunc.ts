class FwFunc {
    //---------------------------------------------------------------------------------
    static showError(ex: any) {
        var $confirmation, $btnOK, message;
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
    static showWebApiError(status, error, responseText, fullurl) {
        var $confirmation, $btnOK;
        if (status === 401 || status === 403) {
            sessionStorage.clear();
            window.location.reload(false);
        }
        else if (status === 0) {
            //console.error(apiException.Message + '\n' + apiException.StackTrace);
            $confirmation = FwConfirmation.renderConfirmation('No Response', fullurl);
            $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
            setTimeout(function () {
                $btnOK.focus();
            }, 100);
        }
        else {
            // try to show the Json Error message
            if (typeof responseText === 'string') {
                try {
                    var apiException = JSON.parse(responseText);
                    console.error(apiException.Message + '\n' + apiException.StackTrace);
                    $confirmation = FwConfirmation.renderConfirmation(status + ' ' + error, apiException.Message);
                    $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
                    setTimeout(function () {
                        $btnOK.focus();
                    }, 100);
                } catch (ex) {
                    //console.error(apiException.Message + '\n' + apiException.StackTrace);
                    $confirmation = FwConfirmation.renderConfirmation(status + ' Error', responseText);
                    $btnOK = FwConfirmation.addButton($confirmation, 'OK', true);
                    setTimeout(function () {
                        $btnOK.focus();
                    }, 100);
                }
            }
        }
    }
    //---------------------------------------------------------------------------------
    static showMessage(message, onbuttonclick?) {
        var $confirmation = FwConfirmation.showMessage('Message', message, false, true, 'OK', onbuttonclick);
        // mv 2016-12-19 This caused a bug in the create contract button, where you could skip the navigation event after create a contract by clicking the background
        //$confirmation.on('click', function() {
        //    FwConfirmation.destroyConfirmation($confirmation);
        //});
        $confirmation.on('click', '.fwconfirmationbox', function (event) {
            event.stopPropagation();
        });
    }
    //---------------------------------------------------------------------------------
    static getDate = function (paramdate, modifier) {
        var date, dd, mm, yyyy;
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
    static getTime = function (showseconds: boolean) {
        var date, hh, mm, ss, period;

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
    static htmlEscape(str) {
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
    }
    //---------------------------------------------------------------------------------
    static htmlUnescape(value) {
        return String(value)
            .replace(/&quot;/g, '"')
            .replace(/&#39;/g, "'")
            .replace(/&lt;/g, '<')
            .replace(/&gt;/g, '>')
            .replace(/&amp;/g, '&')
    }
    //---------------------------------------------------------------------------------
    static getMaxZ(selector) {
        return Math.max.apply(null, jQuery(selector).map(function () {
            var z;
            return isNaN(z = parseInt(jQuery(this).css("z-index"), 10)) ? 0 : z;
        }));
    }
    //---------------------------------------------------------------------------------
    static round(num, decimalplaces) {
        return Math.round(num * Math.pow(10, decimalplaces)) / Math.pow(10, decimalplaces);
    }
    //---------------------------------------------------------------------------------
    static convert12to24(time) {
        var hours = Number(time.match(/^(\d+)/)[1]);
        var minutes = Number(time.match(/:(\d+)/)[1]);
        var AMPM = time.match(/\s(.*)$/)[1];
        if (AMPM.toUpperCase() == "PM" && hours < 12) hours = hours + 12;
        if (AMPM.toUpperCase() == "AM" && hours == 12) hours = hours - 12;
        var sHours = hours.toString();
        var sMinutes = minutes.toString();
        if (hours < 10) sHours = "0" + sHours;
        if (minutes < 10) sMinutes = "0" + sMinutes;
        return sHours + ":" + sMinutes + ":00";
    }
    //---------------------------------------------------------------------------------
    static convert24to12(time) {
        time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

        if (time.length > 1) {
            time = time.slice(1);
            time[5] = +time[0] < 12 ? ' AM' : ' PM';
            time[0] = +time[0] % 12 || 12;
        }
        return time.join('');
    }
    //---------------------------------------------------------------------------------
    static isDesktop = function () {
        var isDesktop = jQuery('html').hasClass('desktop');
        return isDesktop;
    }
    //---------------------------------------------------------------------------------
    static isMobile = function () {
        var isMobile = jQuery('html').hasClass('mobile');
        return isMobile;
    }
    //---------------------------------------------------------------------------------
    static fixCaps = function (string) {
        var returnStr;

        if ((string == null) || (string == '')) {
            returnStr = '';
        } else {
            returnStr = string.toLowerCase().replace(/\b[a-z]/g, function (letter) { return letter.toUpperCase(); });
        }

        return returnStr;
    }
    //---------------------------------------------------------------------------------
}
