var dbworksutil;
(function (dbworksutil) {
    var message = /** @class */ (function () {
        function message() {
        }
        message.message = function (res) {
            if (res == undefined || null)
                res = {};
            if (res.message == undefined || null)
                res.message = 'An unknown error occured.';
            if (res.duration == undefined || null)
                res.duration = 5000;
            if (res.alertType == undefined || null)
                res.alertType = 'warning';
            //var $container = jQuery('<div id="master_notification_container"></div>').appendTo('body');
            var $alert = jQuery('<div class="alert alert_' + res.alertType + '"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' + res.message + '&nbsp;&nbsp;</div>').appendTo('#master_designer_container');
            setTimeout(function () {
                $alert.fadeOut('fast', function () {
                    $alert.remove();
                });
            }, res.duration);
        };
        message.message_success = function (message) {
            this.message({ message: message, duration: 5000, alertType: 'success' });
        };
        message.message_warning = function (message) {
            this.message({ message: message, duration: 5000, alertType: 'warning' });
        };
        message.message_danger = function (message) {
            this.message({ message: message, duration: 5000, alertType: 'danger' });
        };
        message.message_informational = function (message) {
            this.message({ message: message, duration: 5000, alertType: 'info' });
        };
        message.message_default = function (message) {
            this.message({ message: message, duration: 5000, alertType: 'fourwalldefault' });
        };
        message.alert_setup = function () {
            var $parent = jQuery('#master_designer_container');
            $parent.on('click', 'div.alert button', function (e) {
                var $alert = jQuery(e.target).parent();
                $alert.fadeOut('fast', function () {
                    $alert.remove();
                });
            });
        };
        return message;
    }());
    dbworksutil.message = message;
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=message.js.map