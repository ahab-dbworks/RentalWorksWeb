var dbworksutil;
(function (dbworksutil) {
    var debug = /** @class */ (function () {
        function debug() {
        }
        debug.display_log = function (obj, message) {
            // this is dumb
            console.log(obj);
            console.log(message);
            var overlay_log = document.createElement('div');
            overlay_log.style.width = '100px';
            overlay_log.style.height = '100px';
            overlay_log.style.backgroundColor = 'gray';
            overlay_log.style.position = 'absolute';
            overlay_log.style.left = '0px';
            overlay_log.style.right = '0px';
            overlay_log.nodeValue = message;
            document.body.appendChild(overlay_log);
        };
        return debug;
    }());
    dbworksutil.debug = debug;
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=debug.js.map