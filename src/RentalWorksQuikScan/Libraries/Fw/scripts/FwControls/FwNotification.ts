class FwNotification {
    static $notification: JQuery;
    //----------------------------------------------------------------------------------------------
    static renderNotification(type, message, options?) {
        var html, $notification, maxZIndex;
        jQuery('.fwnotification').each((index: number, element: HTMLElement) => {
            let $this = jQuery(element);
            let top: number = parseInt($this.css('top').substr(0, $this.css('top').length - 2));
            let bottom: number = parseInt($this.css('bottom').substr(0, $this.css('bottom').length - 2));
            $this.css({
                top: (top - 60) + 'px',
                bottom: (bottom + 60) + 'px'
            });
        });
        //FwNotification.closeNotification(jQuery('.fwnotification'));

        html = [];
        html.push('<div class="fwnotification advisory');
        switch (type) {
            case 'SUCCESS':
                html.push(' success');
                break;
            case 'ERROR':
                html.push(' error');
                break;
            case 'INFO':
            case 'PERSISTENTINFO':
                html.push(' info');
                break;
            case 'WARNING':
                html.push(' warning');
                break;
        }
        html.push('">');
        html.push('<div class="message">' + message + '</div>');
        html.push('<div class="messageclose"><i class="material-icons">&#xE5CD;</i></div>');
        html.push('</div>');
        html = html.join('');
        $notification = jQuery(html);

        maxZIndex = FwFunc.getMaxZ('*');

        if (options !== undefined) {
            $notification.css(options)
        }

        $notification
            .css('z-index', maxZIndex)
            .appendTo(jQuery('body'))
            .fadeIn('slow', function () {
                var el = jQuery(this);
                if (type !== 'PERSISTENTINFO') {
                    setTimeout(function () {
                        el.fadeOut('slow', function () {
                            FwNotification.closeNotification(jQuery(this));
                        });
                    }, 4500);
                }
            })
            .on('click', '.messageclose', function () {
                FwNotification.closeNotification(jQuery(this).parent());
            })
            ;

        //FwNotification.renderNotificationToControl(type, message);

        return $notification;
    };
    //----------------------------------------------------------------------------------------------
    static closeNotification($notification) {
        $notification.remove();
    };
    //----------------------------------------------------------------------------------------------
    static fieldNotification($field, type, message) {
        var html, $notification, maxZIndex;

        html = [];
        html.push('<div class="fwnotification');
        switch (type) {
            case 'SUCCESS':
                html.push(' success');
                break;
            case 'ERROR':
                html.push(' error');
                break;
            case 'INFO':
            case 'PERSISTENTINFO':
                html.push(' info');
                break;
            case 'WARNING':
                html.push(' warning');
                break;
        }
        html.push('">');
        html.push('<div class="messageclose"><i class="material-icons">close</i></div>');
        html.push('<div class="message">' + message + '</div>');
        html.push('</div>');
        html = html.join('');
        $notification = jQuery(html);

        maxZIndex = FwFunc.getMaxZ('*');

        $notification
            .css('z-index', maxZIndex)
            .appendTo(jQuery('body'))
            .fadeIn('slow', function () {
                var el = jQuery(this);
                if (type !== 'PERSISTENTINFO') {
                    setTimeout(function () {
                        el.fadeOut('slow', function () {
                            FwNotification.closeNotification(jQuery(this));
                        });
                    }, 4500);
                }
            })
            .on('click', '.messageclose', function () {
                FwNotification.closeNotification(jQuery(this).parent());
            })
            ;

        $notification.css('top', $field.position().top).css('left', ($field.position().left + $field.width() + 10)).css('bottom', 'auto').css('right', 'auto');

        return $notification;
    };
    //----------------------------------------------------------------------------------------------
    static renderNotificationToControl(type, message) {
        var html, $notification, messagecount, maxZIndex;

        html = [];
        html.push('<div class="fwnotification');
        switch (type) {
            case 'SUCCESS': html.push(' success'); break;
            case 'ERROR': html.push(' error'); break;
            case 'INFO':
            case 'PERSISTENTINFO': html.push(' info'); break;
            case 'WARNING': html.push(' warning'); break;
        }
        html.push('">');
        html.push('<div class="message-callout"><div class="message-icon"></div></div>');
        html.push('<div class="message-body">' + message + '</div>');
        html.push('<div class="message-close"></div>');
        html.push('<div class="message-timestamp">' + FwFunc.getTime(true) + '</div>');
        html.push('</div>');
        html = html.join('');
        $notification = jQuery(html);

        messagecount = FwNotification.$notification.find('div.notificationmenu-body div.fwnotification').length;

        if (messagecount == 0) {
            FwNotification.$notification.find('div.notificationmenu-body div.notificationmenu-body-placeholder').hide();
        } else if (messagecount >= 30) {
            FwNotification.$notification.find('div.notificationmenu-body').children('.fwnotification').last().remove();
        }
        FwNotification.$notification.find('div.notificationmenu-body').prepend($notification);

        maxZIndex = FwFunc.getMaxZ('*');
        $notification
            .css('z-index', maxZIndex)
            .on('click', '.message-close', function () {
                FwNotification.closeNotification(jQuery(this).parent());
                if (FwNotification.$notification.find('div.notificationmenu-body div.fwnotification').length == 0) {
                    FwNotification.$notification.find('div.notificationmenu-body div.notificationmenu-body-placeholder').show();
                }
            })
            ;

        return $notification;
    };
    //----------------------------------------------------------------------------------------------
    static generateNotificationArea() {
        var $notificationmenu, html;

        html = [];
        html.push('<div id="notification" class="item">');
        html.push('<div class="notificationicon"></div>');
        html.push('<div class="notificationmenu" style="display:none;">');
        html.push('<div class="notificationmenu-header">');
        html.push('<div class="notificationmenu-title">Notifications</div>');
        html.push('</div>');
        html.push('<div class="notificationmenu-body">');
        html.push('<div class="notificationmenu-body-placeholder">No notifications...</div>');
        html.push('</div>');
        html.push('</div>');
        html.push('<div class="notificationmenu-overlay" style="display:none;"></div>');
        html.push('</div>');

        var $notification = jQuery(html.join(''));
        FwNotification.$notification = $notification;

        $notification
            .on('click', function () {
                var $this, maxZIndex;

                $this = jQuery(this);
                maxZIndex = FwFunc.getMaxZ('*');
                $this.find('.notificationmenu-overlay')
                    .css('z-index', maxZIndex)
                    .show();
                maxZIndex++;
                $this.find('.notificationmenu')
                    .css('z-index', maxZIndex)
                    .show();
            })
            .on('click', '.notificationmenu-overlay', function (e) {
                var $this;
                $this = jQuery(this);

                $this.siblings('.notificationmenu').hide();
                $this.hide();
                e.stopPropagation();
            })
            ;

        return $notification;
    }
    //----------------------------------------------------------------------------------------------
}
