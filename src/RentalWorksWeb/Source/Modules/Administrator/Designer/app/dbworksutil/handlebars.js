var dbworksutil;
(function (dbworksutil) {
    var handlebars = (function () {
        function handlebars() {
        }
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