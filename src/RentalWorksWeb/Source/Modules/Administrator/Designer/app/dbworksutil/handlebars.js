var dbworksutil;
(function (dbworksutil) {
    var handlebars = (function () {
        function handlebars() {
        }
        //static render(template: any, obj: any = {}, $container: JQuery = jQuery('#main_master_body')): JQueryDeferred<string> {
        //    var $promise = jQuery.Deferred();            
        //    $promise.resolve($container.html(template(obj)));
        //    return $promise;
        //}
        handlebars.render = function (source, obj, $container) {
            if (obj === void 0) { obj = {}; }
            if ($container === void 0) { $container = jQuery('#main_master_body'); }
            var $promise = jQuery.Deferred(), template = Handlebars.compile(source), html = template(obj);
            $promise.resolve($container.html(html));
            return $promise;
        };
        return handlebars;
    }());
    dbworksutil.handlebars = handlebars;
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=handlebars.js.map