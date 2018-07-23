import * as Handlebars from 'handlebars/runtime';
export class HandlebarsHelpers {
    static registerHelpers() {
        Handlebars.registerHelper('gt', function (leftSide, rightSide, options) {
            if (leftSide > rightSide) {
                var result = options.fn(this);
                return result;
            }
        });
    }
}
//# sourceMappingURL=HandlebarsHelpers.js.map