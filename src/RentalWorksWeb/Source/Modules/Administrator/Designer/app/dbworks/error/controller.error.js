var dbworks;
(function (dbworks) {
    var error;
    (function (error_1) {
        var controllers;
        (function (controllers) {
            var error = /** @class */ (function () {
                //export class error extends main.controllers.main {
                function error() {
                }
                error.prototype.init = function () {
                    this.bind();
                };
                error.prototype.bind = function () {
                    $('#main_master_body').html(templates.error());
                };
                return error;
            }());
            controllers.error = error;
        })(controllers = error_1.controllers || (error_1.controllers = {}));
    })(error = dbworks.error || (dbworks.error = {}));
})(dbworks || (dbworks = {}));
//# sourceMappingURL=controller.error.js.map