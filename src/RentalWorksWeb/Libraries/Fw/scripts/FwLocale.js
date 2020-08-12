class FwLocaleClass {
    constructor() {
        this.locale = 'en-US';
    }
    setLocale(language) {
        this.locale = language;
        moment.locale(language);
    }
    getDateFormat(language) {
        var format = moment.localeData(this.locale)._longDateFormat.L;
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
}
var FwLocale = new FwLocaleClass();
//# sourceMappingURL=FwLocale.js.map