var dbworksutil;
(function (dbworksutil) {
    var rest = /** @class */ (function () {
        function rest() {
        }
        rest.GET = function (url, reconstitueResponse, failRepsonse) {
            jQuery('#master_loader').fadeIn('fast');
            var $promise = jQuery.Deferred();
            var auth = 'Bearer ' + sessionStorage.getItem('apiToken'); // this is part of the RWW auth process.
            jQuery.ajax({
                type: 'GET',
                url: url,
                contentType: "application/json;",
                dataType: "json",
                cache: false,
                headers: {
                    Authorization: auth
                }
            }).done(function (data) {
                //jQuery.get(url).fail((xhr, textstatus, error) => {
                //    if (xhr.status != (200 || 203 || 206 || 226)) {
                //        dbworksutil.message.message_danger('An error occured. See console for details.');
                //        console.error(xhr.status + ' status code captured.');
                //        $promise.reject(xhr, textstatus, error);
                //    }
                //}).done((data) => {
                $promise.resolve(data);
            }).fail(function (xhr, textstatus, error) {
                if (xhr.status != (200 || 203 || 206 || 226)) {
                    dbworksutil.message.message_danger('An error occured. See console for details.');
                    console.error(xhr.status + ' status code captured.');
                    $promise.reject(xhr, textstatus, error);
                }
            }).always(function (data) {
                jQuery('#master_loader').fadeOut('fast');
            });
            return $promise;
        };
        rest.POST = function (url, data, failRepsonse) {
            return this._common_ajax_pipe(url, 'POST', data);
        };
        rest.PUT = function (url, data, failRepsonse) {
            return this._common_ajax_pipe(url, 'PUT', data);
        };
        rest.DELETE = function (url, data, failRepsonse) {
            return this._common_ajax_pipe(url, 'DELETE', data);
        };
        rest._common_ajax_pipe = function (url, type, data, failRepsonse) {
            var $promise = jQuery.Deferred();
            if (data.length != 0 && data != null) {
                jQuery('#master_loader').fadeIn('fast');
                var auth = 'Bearer ' + sessionStorage.getItem('apiToken'); // this is part of the RWW auth process.
                jQuery.ajax({
                    type: type,
                    url: url,
                    data: JSON.stringify(data),
                    contentType: "application/json;",
                    dataType: "json",
                    cache: false,
                    headers: {
                        Authorization: auth
                    }
                }).done(function (data) {
                    //fourwallutil.message.message_success('Save successful.');
                    $promise.resolve(data);
                }).fail(function (xhr, textstatus, error) {
                    if (xhr.status != (200 || 203 || 206 || 226)) {
                        dbworksutil.message.message_danger('An error occured. See console for details.');
                        console.error(xhr.status + ' status code captured.');
                        $promise.reject(xhr, textstatus, error);
                    }
                }).always(function () {
                    jQuery('#master_loader').fadeOut('fast');
                });
            }
            else {
                dbworksutil.message.message_warning('Nothing to save.');
                $promise.reject("Nothing to Save.");
            }
            return $promise;
        };
        return rest;
    }());
    dbworksutil.rest = rest;
})(dbworksutil || (dbworksutil = {}));
//# sourceMappingURL=rest.js.map