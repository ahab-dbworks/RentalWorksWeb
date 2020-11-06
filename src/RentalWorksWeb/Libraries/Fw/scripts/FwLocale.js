class FwLocaleClass {
    constructor() {
        this.Locale = 'en-US';
    }
    setLocale(language) {
        if (language) {
            this.Locale = language;
            moment.locale(language);
        }
        else {
            this.Locale = window.navigator.language;
            moment.locale(window.navigator.language);
        }
    }
    getDateFormat(language) {
        var format = moment.localeData(this.Locale)._longDateFormat.L;
        if (language) {
            format = moment.localeData(language)._longDateFormat.L;
        }
        return format;
    }
    formatDateToLocale(date, language) {
        if (!date) {
            return '';
        }
        var localmoment = moment(date);
        if (language) {
            localmoment.locale(language);
        }
        return localmoment.format('L');
    }
    formatLocaleDateToIso(date) {
        if (!date) {
            return '';
        }
        return moment(date, FwLocale.getDateFormat()).format('YYYY-MM-DD');
    }
    getDate(date, localFormat, modifier) {
        var localmoment;
        if (date) {
            localmoment = moment(date);
        }
        else {
            localmoment = moment();
        }
        if (modifier) {
            localmoment.add(modifier.Quantity, modifier.ObjectModified);
        }
        return (localFormat) ? localmoment.format('L') : localmoment.format('YYYY-MM-DD');
    }
    getTime(time, localFormat, modifier) {
        let hour12 = true;
        const options = {
            hour: 'numeric',
            minute: 'numeric',
            hour12: hour12,
        };
        return Intl.DateTimeFormat('default', options).format(new Date());
    }
    getNumber(value, language, options) {
        if (typeof value === 'string') {
            value = parseFloat(value);
        }
        var locale = (language) ? language : FwLocale.Locale;
        return Intl.NumberFormat(locale, options).format(value);
    }
}
var FwLocale = new FwLocaleClass();
//# sourceMappingURL=FwLocale.js.map