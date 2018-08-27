import * as Handlebars from 'handlebars/runtime';

export class HandlebarsHelpers {
    static registerHelpers() {
		Handlebars.registerHelper('gt', function (leftSide, rightSide, options) {
            if (leftSide > rightSide) {
                var result = options.fn(this);
                return result;
            }
        });
        Handlebars.registerHelper('ifEquals', function (arg1, arg2, options) {
            return (arg1 == arg2) ? options.fn(this) : options.inverse(this);
        });
    }
}