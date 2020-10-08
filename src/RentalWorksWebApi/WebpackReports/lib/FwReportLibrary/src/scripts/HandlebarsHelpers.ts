//import * as  Handlebars from 'handlebars/dist/cjs/handlebars';

export class HandlebarsHelpers {
    static registerHelpers(custom: boolean = false) {
        let Handlebars;
        if (custom) {
            Handlebars = require('handlebars/dist/cjs/handlebars');
        } else {
            Handlebars = require('handlebars/runtime');
        }
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('gt', function (arg1: any, arg2: any, options: any) {
            return (arg1 > arg2) ? options.fn(this) : options.inverse(this);
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifEquals', function (arg1: any, arg2: any, options: any) {
            return (arg1 == arg2) ? options.fn(this) : options.inverse(this);
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifNotEquals', function (arg1: any, arg2: any, options: any) {
            return (arg1 != arg2) ? options.fn(this) : options.inverse(this);
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('checkLength', function (arg1: any, arg2: any, options: any) {
            'use strict';
            if (arg1.length > arg2) {
                return options.fn(this);
            }
            return options.inverse(this);
        });
        //--------------------------------------------------------------------------------------------------------------
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
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifInList', function (arg1: string, arg2: string, options: any) {
            var stringArray = arg2.split(',');
            return (stringArray.indexOf(arg1) !== -1) ? options.fn(this) : options.inverse(this);
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifSameAsNext', function (array, currentIndex, field, options) {
            let currentValue: any = array[currentIndex][field];
            let nextValue: any = '';
            if (array.length >= currentIndex) {
                nextValue = array[currentIndex + 1][field];
            }
            if (currentValue == nextValue) {
                return options.fn(this);
            }
            else {
                return options.inverse(this);
            }
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifNotSameAsNext', function (array, currentIndex, field, options) {
            let currentValue: any = array[currentIndex][field];
            let nextValue: any = '';
            if (array.length >= currentIndex) {
                nextValue = array[currentIndex + 1][field];
            }
            if (currentValue == nextValue) {
                return options.inverse(this);
            }
            else {
                return options.fn(this);
            }
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifSameAsPrevious', function (array, currentIndex, field, options) {
            let currentValue: any = array[currentIndex][field];
            let previousValue: any = '';
            if (currentIndex > 0) {
                previousValue = array[currentIndex - 1][field];
            }
            if (currentValue == previousValue) {
                return options.fn(this);
            }
            else {
                return options.inverse(this);
            }
        });
        //--------------------------------------------------------------------------------------------------------------
        Handlebars.registerHelper('ifNotSameAsPrevious', function (array, currentIndex, field, options) {
            let currentValue: any = array[currentIndex][field];
            let previousValue: any = '';
            if (currentIndex > 0) {
                previousValue = array[currentIndex - 1][field];
            }
            if (currentValue == previousValue) {
                return options.inverse(this);
            }
            else {
                return options.fn(this);
            }
        });
        //--------------------------------------------------------------------------------------------------------------
    }
}
