﻿import * as Handlebars from 'handlebars/runtime';

export class HandlebarsHelpers {
    static registerHelpers() {
		Handlebars.registerHelper('gt', function (arg1, arg2, options) {
            return (arg1 > arg2) ? options.fn(this) : options.inverse(this);
        });
        Handlebars.registerHelper('ifEquals', function (arg1, arg2, options) {
            return (arg1 == arg2) ? options.fn(this) : options.inverse(this);
        });
        Handlebars.registerHelper('ifNotEquals', function (arg1, arg2, options) {
            return (arg1 != arg2) ? options.fn(this) : options.inverse(this);
        });
        Handlebars.registerHelper('checkLength', function (arg1, arg2, options) {
            'use strict';
            if (arg1.length > arg2) {
                return options.fn(this);
            }
            return options.inverse(this);
        });
    }
}