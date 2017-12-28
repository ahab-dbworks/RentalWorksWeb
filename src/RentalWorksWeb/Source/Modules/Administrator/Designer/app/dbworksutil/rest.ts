declare var Materialize: Materialize.Materialize;

namespace dbworksutil {

    export class rest {

        static GET<T>(url: string, reconstitueResponse?: boolean, failRepsonse?: dbworksutil.message): JQueryPromise<T> {
            jQuery('#master_loader').fadeIn('fast');
            var $promise = jQuery.Deferred<any>();

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
            }).done((data) => {

            //jQuery.get(url).fail((xhr, textstatus, error) => {

            //    if (xhr.status != (200 || 203 || 206 || 226)) {
            //        dbworksutil.message.message_danger('An error occured. See console for details.');
            //        console.error(xhr.status + ' status code captured.');
            //        $promise.reject(xhr, textstatus, error);
            //    }
                
            //}).done((data) => {
            $promise.resolve(<T>data);                                
        }).fail((xhr, textstatus, error) => {

            if (xhr.status != (200 || 203 || 206 || 226)) {
                dbworksutil.message.message_danger('An error occured. See console for details.');
                console.error(xhr.status + ' status code captured.');
                $promise.reject(xhr, textstatus, error);
            }

        }).always((data) => {
                jQuery('#master_loader').fadeOut('fast');
            });            
            return $promise;
        }

        static POST<T>(url: string, data: any, failRepsonse?: dbworksutil.message): JQueryPromise<T> {
            return this._common_ajax_pipe(url, 'POST', data);
        }

        static PUT<T>(url: string, data: any, failRepsonse?: dbworksutil.message): JQueryPromise<T> {
            return this._common_ajax_pipe(url, 'PUT', data);
        }

        static DELETE<T>(url: string, data: any, failRepsonse?: dbworksutil.message): JQueryPromise<T> {
            return this._common_ajax_pipe(url, 'DELETE', data);
        }

        private static _common_ajax_pipe<T>(url: string, type: string, data: any, failRepsonse?: dbworksutil.message): JQueryPromise<T> {            

            var $promise = jQuery.Deferred<T>();

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
                }).done((data) => {

                    //fourwallutil.message.message_success('Save successful.');
                    $promise.resolve(<T>data);
                    
                }).fail((xhr, textstatus, error) => {

                    if (xhr.status != (200 || 203 || 206 || 226)) {
                        dbworksutil.message.message_danger('An error occured. See console for details.');
                        console.error(xhr.status + ' status code captured.');
                        $promise.reject(xhr, textstatus, error);
                    }

                }).always(() => {
                    jQuery('#master_loader').fadeOut('fast');     
                });

            } else {                
                dbworksutil.message.message_warning('Nothing to save.');
                $promise.reject("Nothing to Save.");
            }
            
            return $promise;
        }
    }
}