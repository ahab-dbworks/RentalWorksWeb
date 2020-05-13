//import * as  Handlebars from 'handlebars/dist/cjs/handlebars';

export class HandlebarsHelpers {
    static registerHelpers(custom: boolean = false) {
        let Handlebars;
        if (custom) {
            Handlebars = require('handlebars/dist/cjs/handlebars');
        } else {
            Handlebars = require('handlebars/runtime');
        }
        Handlebars.registerHelper('gt', function (arg1: any, arg2: any, options: any) {
            return (arg1 > arg2) ? options.fn(this) : options.inverse(this);
        });
        Handlebars.registerHelper('ifEquals', function (arg1: any, arg2: any, options: any) {
            return (arg1 == arg2) ? options.fn(this) : options.inverse(this);
        });
        Handlebars.registerHelper('ifNotEquals', function (arg1: any, arg2: any, options: any) {
            return (arg1 != arg2) ? options.fn(this) : options.inverse(this);
        });
        Handlebars.registerHelper('checkLength', function (arg1: any, arg2: any, options: any) {
            'use strict';
            if (arg1.length > arg2) {
                return options.fn(this);
            }
            return options.inverse(this);
        });
        Handlebars.registerHelper('ifCond', function (v1: any, operator: any, v2: any, options: any) {

            switch (operator) {
                case '==':
                    return (v1 == v2) ? options.fn(this) : options.inverse(this);
                case '===':
                    return (v1 === v2) ? options.fn(this) : options.inverse(this);
                case '!=':
                    return (v1 != v2) ? options.fn(this) : options.inverse(this);
                case '!==':
                    return (v1 !== v2) ? options.fn(this) : options.inverse(this);
                case '<':
                    return (v1 < v2) ? options.fn(this) : options.inverse(this);
                case '<=':
                    return (v1 <= v2) ? options.fn(this) : options.inverse(this);
                case '>':
                    return (v1 > v2) ? options.fn(this) : options.inverse(this);
                case '>=':
                    return (v1 >= v2) ? options.fn(this) : options.inverse(this);
                case '&&':
                    return (v1 && v2) ? options.fn(this) : options.inverse(this);
                case '||':
                    return (v1 || v2) ? options.fn(this) : options.inverse(this);
                default:
                    return options.inverse(this);
            }
        });
    }
}
