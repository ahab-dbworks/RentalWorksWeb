var dbworksutil;
(function (dbworksutil) {
    // TODO
    // - kill events that take longer than the _timer property
    var warden = (function () {
        function warden() {
            this._timer = 500;
            this._interval = null;
            this._hooked_events = [];
        }
        warden.prototype.watch = function () {
            this._runner();
        };
        warden.prototype.hook = function (singleton) {
            this._hooked_events.push(singleton);
        };
        warden.prototype.stop_watch = function () {
            clearInterval(this._interval);
        };
        warden.prototype._runner = function () {
            var _this = this;
            this._interval = setInterval(function () {
                console.log('warden running');
                _this._hooked_events.forEach(function (event) {
                    event();
                });
            }, this._timer);
        };
        return warden;
    }());
    dbworksutil.warden = warden;
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=warden.js.map