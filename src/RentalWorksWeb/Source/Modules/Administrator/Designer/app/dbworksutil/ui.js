var dbworksutil;
(function (dbworksutil) {
    var ui;
    (function (ui) {
        var side_nav = (function () {
            function side_nav(targetID) {
                this._config = new side_nav_config;
                this._container = document.getElementById(targetID);
            }
            side_nav.prototype.side_nav = function (action, config) {
                if (typeof config === 'string') {
                    // do action
                    this._side_nav_action(action);
                }
                else {
                    // do object configuration
                    this._side_nav_configuration();
                }
            };
            side_nav.prototype.open = function () {
                this._side_nav_configuration();
                this._setup();
                this._side_nav_action('open');
                window.addEventListener('click', clickEvent);
                function clickEvent() {
                    alert('test!');
                }
            };
            side_nav.prototype.close = function () {
                this._side_nav_action('close');
            };
            side_nav.prototype._setup = function () {
                //if (!this._is_hidden()) this._container.style.visibility = 'hidden';            
            };
            side_nav.prototype._side_nav_action = function (action) {
                //window.requestAnimationFrame((step) => { this._toggle_slide(step); });
                var _self = this;
                this._toggle_slide();
            };
            side_nav.prototype._click_event = function () {
                alert('test');
                //this._container.removeEventListener("click", () => {
                //    document.addEventListener('click', () => { alert('test'); });
                //});
            };
            side_nav.prototype._side_nav_configuration = function () {
                this._config.hasOverlay = false;
                this._config.width = 315;
                this._config.slideSpeed = 1000;
                this._config.edge = 'left';
            };
            side_nav.prototype._toggle_slide = function (onComplete) {
                var _this = this;
                //console.log(frameInfo);
                //var prevLeft = 0;
                ////var timer = setInterval(() => {
                //    if (prevLeft !== this._config.width) {
                //        prevLeft = (prevLeft + 1);
                //      this._container.style.marginLeft = prevLeft + 'px';
                //    }
                ////}, this._config.slideSpeed);
                //    window.requestAnimationFrame((step) => { this._toggle_slide(step); });
                var i = 0;
                var prevLeft = 0;
                while (i < this._config.width) {
                    setTimeout(function () {
                        if (prevLeft !== _this._config.width) {
                            prevLeft = (prevLeft + 1);
                            _this._container.style.marginLeft = prevLeft + 'px';
                        }
                    }, i * 1);
                    i++;
                }
            };
            // helpers
            side_nav.prototype._is_hidden = function () {
                return (this._container.offsetParent === null);
            };
            return side_nav;
        }());
        ui.side_nav = side_nav;
        var side_nav_config = (function () {
            function side_nav_config() {
            }
            return side_nav_config;
        }());
    })(ui = dbworksutil.ui || (dbworksutil.ui = {}));
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=ui.js.map