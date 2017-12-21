namespace dbworksutil {

    export class message {

        message?: string;
        secondInstanceMessage?: string;
        duration?: number;
        alertType?: string;

        static message(res: imessage): void {
            if (res == undefined || null) res = {};
            if (res.message == undefined || null) res.message = 'An unknown error occured.';
            if (res.duration == undefined || null) res.duration = 5000;
            if (res.alertType == undefined || null) res.alertType = 'warning';
                       
            //var $container = jQuery('<div id="master_notification_container"></div>').appendTo('body');

            var $alert = jQuery('<div class="alert alert_' + res.alertType + '"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>' + res.message + '&nbsp;&nbsp;</div>').appendTo('#master_designer_container');

            setTimeout(function () {
                $alert.fadeOut('fast', () => {
                    $alert.remove();
                });
            }, res.duration);

        }        

        static message_success(message: string): void {
            this.message({ message: message, duration: 5000, alertType: 'success' });
        }

        static message_warning(message: string): void {
            this.message({ message: message, duration: 5000, alertType: 'warning' });
        }

        static message_danger(message: string): void {
            this.message({ message: message, duration: 5000, alertType: 'danger' });
        }

        static message_informational(message: string): void {
            this.message({ message: message, duration: 5000, alertType: 'info' });
        }

        static message_default(message: string): void {
            this.message({ message: message, duration: 5000, alertType: 'fourwalldefault' });
        }

        static alert_setup(): void {

            var $parent = jQuery('#master_designer_container');

            $parent.on('click', 'div.alert button', (e) => {

                var $alert = jQuery(e.target).parent();

                $alert.fadeOut('fast', () => {
                    $alert.remove();
                });

            });

        }

    }

    export interface imessage {
        message?: string;
        secondInstanceMessage?: string; // TODO - if the same error happens twice in a row, show this message
        duration?: number;
        alertType?: string;
    }
}