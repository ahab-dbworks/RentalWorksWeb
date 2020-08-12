class FwLocaleClass {
    locale: string = 'en-US';
    //---------------------------------------------------------------------------------
    setLocale(language: string) {
        this.locale = language;
        moment.locale(language);
    }
    //---------------------------------------------------------------------------------
    getDateFormat(language?: string) {
        var format = moment.localeData(this.locale)._longDateFormat.L;

        if (language) {
            format = moment.localeData(language)._longDateFormat.L;
        }
        return format;
    }
    //---------------------------------------------------------------------------------
    formatDateToLocale(date: string, language?: string): string {
        if (!date) {
            return '';
        }
        var localmoment = moment(date);

        if (language) {
            localmoment.locale(language);
        }

        return localmoment.format('L');
    }
    //---------------------------------------------------------------------------------
    formatLocaleDateToIso(date): string {
        if (!date) {
            return '';
        }

        return moment(date, FwLocale.getDateFormat()).format('YYYY-MM-DD');
    }
    //---------------------------------------------------------------------------------
    getDate(date?: string, localFormat?: boolean, modifier?: DateModifier): string {
        var localmoment;
        if (date) {
            localmoment = moment(date);
        } else {
            localmoment = moment();
        }

        if (modifier) {
            localmoment.add(modifier.Quantity, modifier.ObjectModified);
        }

        return (localFormat) ? localmoment.format('L') : localmoment.format('YYYY-MM-DD');
    }
    //---------------------------------------------------------------------------------
    //checkLocalesLanguageExists(language): boolean {
    //    var exists = true;

    //    var lang = language;
    //    if (!this.locales[lang]) {
    //        lang = lang.split('-')[0];
    //        if (!this.locales[lang]) {
    //            exists = false;
    //        }
    //    }

    //    return exists;
    //}
    //---------------------------------------------------------------------------------
    //locales = {
    //    'en-CA': { //Canadian English
    //        dateFormat: 'yyyy-mm-dd'
    //    },
    //    'en-GB': { //British English
    //        dateFormat: 'dd/mm/yyyy'
    //    },
    //    'en-US': { //American English
    //        dateFormat: 'mm/dd/yyyy',
    //    },
    //    'es': { //Spanish
    //        dateFormat: 'dd/mm/yyyy'
    //    },
    //    'de': { //German
    //        dateFormat: 'dd.mm.yyyy'
    //    },
    //    'fr': { //French
    //        dateFormat: 'dd/mm/yyyy'
    //    },
    //    'it': { //Italian
    //        dateFormat: 'mm/dd/yyyy'
    //    },
    //    'ja': { //Japanese
    //        dateFormat: 'yyyy/mm/dd'
    //    }
    //}
    //---------------------------------------------------------------------------------
}

var FwLocale = new FwLocaleClass();

interface DateModifier {
    Quantity:       number;
    ObjectModified: 'days'|'weeks'|'months'|'years';
}