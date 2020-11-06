class FwLocaleClass {
    Locale: string = 'en-US';
    //---------------------------------------------------------------------------------
    setLocale(language: string) {
        if (language) {
            this.Locale = language;
            moment.locale(language);
        } else {
            this.Locale = window.navigator.language;
            moment.locale(window.navigator.language);
        }
    }
    //---------------------------------------------------------------------------------
    getDateFormat(language?: string) {
        var format = moment.localeData(this.Locale)._longDateFormat.L;

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
    /**
     * @param date Optional. If null date is provided function will operate on Today.
     * @param localFormat Optional. true value returns localized date format, otherwise ISO format is returned.
     * @param modifier Optional.
     */
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
    /**
     * @param time Optional. If null time is provided it will operate on now.
     * @param time12 Optional. true value returns 12 hour time. 24 hour format is the default.
     */
    getTime(time?: string, time12?: boolean): string {
        var localmoment;
        if (time) {
            localmoment = moment(time, 'HH:mm');
        } else {
            localmoment = moment();
        }

        const options = {
            hour:   '2-digit',
            minute: '2-digit',
            hour12:  (time12) ? time12 : false
        }
        return Intl.DateTimeFormat(this.Locale, options).format(localmoment);
    }
    //---------------------------------------------------------------------------------
    /**
     * Returns @param value in localized format
     * @param value 
     * @param language Optional. Forces the function to use an alternate locale.
     * @param options Optional. Ref https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl/NumberFormat/NumberFormat
     */
    getNumber(value: number|string, language?: string, options?: Intl.NumberFormatOptions) {
        if (typeof value === 'string') {
            value = parseFloat(value as string);
        }

        var locale = (language) ? language : FwLocale.Locale;

        return Intl.NumberFormat(locale, options).format(value as number);
    }
    //---------------------------------------------------------------------------------
}

var FwLocale = new FwLocaleClass();

interface DateModifier {
    Quantity:       number;
    ObjectModified: 'days'|'weeks'|'months'|'years';
}