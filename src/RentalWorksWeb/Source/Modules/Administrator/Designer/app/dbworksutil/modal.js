var dbworksutil;
(function (dbworksutil) {
    // we use bootstrap's modal
    var modal = (function () {
        function modal(target, config) {
            if (typeof target === 'string') {
                this.targetID = target;
                this.$target = jQuery('#' + target);
            }
            if (typeof target === 'object') {
                this.$target = target;
            }
            this.config = new modal_config;
            this._configure();
        }
        modal.prototype._configure = function () {
            //var config: Materialize.ModalOptions = { complete: function () { alert('MODAL CLOSING'); } };
            var config;
            config;
            this.$target.modal(config);
        };
        modal.prototype.open = function () {
            this.$target.modal('show');
            return this;
        };
        modal.prototype.close = function () {
            this.$target.modal('hide');
            return this;
        };
        modal.prototype.toggle = function () {
            this.$target.modal('toggle');
            return this;
        };
        modal.prototype.toggle_loading = function () {
            this.$target.find('.loader').slideToggle('fast');
            return this;
        };
        return modal;
    }());
    dbworksutil.modal = modal;
    var modal_config = (function () {
        function modal_config() {
        }
        return modal_config;
    }());
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=modal.js.map