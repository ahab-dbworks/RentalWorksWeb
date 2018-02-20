var FwApplication = (function () {
    function FwApplication() {
        this.screens = [];
    }
    FwApplication.prototype.pushScreen = function (screen) {
        var me, applicationContainer, viewAnimationContainer, $previousView;
        FwAppData.abortAllRequests();
        me = this;
        applicationContainer = jQuery('#application');
        if ((this.screens.length > 0) && (applicationContainer.children().length > 0)) {
            this.screens[this.screens.length - 1].$view = applicationContainer.children().detach();
            if (typeof this.screens[this.screens.length - 1].unload !== 'undefined') {
                this.screens[this.screens.length - 1].unload();
            }
        }
        screen.$view.hide();
        applicationContainer
            .empty()
            .append(screen.$view)
            .removeClass('hidden');
        me.screens.push(screen);
        screen.$view
            .addClass('active')
            .show(0);
        jQuery(screen.scanTarget).addClass('scanTarget');
        if (typeof screen.load !== 'undefined') {
            screen.load();
        }
        document.body.scrollTop = 0;
    };
    FwApplication.prototype.updateScreen = function (screen) {
        var me, applicationContainer, viewAnimationContainer, $previousView;
        FwAppData.abortAllRequests();
        me = this;
        applicationContainer = jQuery('#application');
        if (this.screens.length > 0) {
            if (typeof this.screens[this.screens.length - 1].unload !== 'undefined') {
                this.screens[this.screens.length - 1].unload();
            }
            this.screens[this.screens.length - 1] = screen;
        }
        screen.$view.hide(0, function () {
            screen.$view.hide();
            applicationContainer
                .empty()
                .append(screen.$view)
                .removeClass('hidden');
            screen.$view.show(0);
            if (typeof screen.load !== 'undefined') {
                screen.load();
            }
        });
    };
    FwApplication.prototype.popScreen = function () {
        var applicationContainer, oldScreen, screen;
        FwAppData.abortAllRequests();
        applicationContainer = jQuery('#application');
        if (this.screens.length > 1) {
            if (applicationContainer.children().length > 0) {
                oldScreen = this.screens.pop();
                if (typeof oldScreen.unload !== 'undefined') {
                    oldScreen.unload();
                }
                if (this.screens.length > 0) {
                    screen = this.screens[this.screens.length - 1];
                    screen.$view.hide(0, function () {
                        applicationContainer.children().hide();
                        screen.$view.hide();
                        applicationContainer
                            .empty()
                            .append(screen.$view)
                            .removeClass('hidden');
                        if (typeof screen.load !== 'undefined') {
                            screen.load();
                        }
                        screen.$view.show(0);
                    });
                }
            }
        }
    };
    FwApplication.prototype.setMedia = function (media) {
        localStorage.setItem('media', media);
    };
    FwApplication.prototype.setScanMode = function (scanMode) {
        localStorage.scanMode = scanMode;
        if (typeof window['LineaScanner'] !== 'undefined') {
            window['LineaScanner'].setScanMode(parseInt(scanMode));
        }
    };
    FwApplication.prototype.load = function () {
        var me = this;
        window.addEventListener("dragover", function (e) {
            e.preventDefault();
        }, false);
        window.addEventListener("drop", function (e) {
            e.preventDefault();
        }, false);
        if (!localStorage.getItem('media') || (localStorage.getItem('media').length === 0)) {
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
                this.setMedia('mobile');
            }
            else {
                this.setMedia('desktop');
            }
        }
        else {
            var media = localStorage.getItem('media');
            this.setMedia(media);
        }
    };
    FwApplication.prototype.popOutURL = function (url) {
        var settings = 'scrollbars=yes,resizeable=yes,toolbar=no,status=no,menubar=no,directories=no,titlebar=no,location=no,addressbar=yes';
        var newwindow = window.open(url, '', settings);
    };
    ;
    FwApplication.prototype.uniqueId = function (idlength) {
        var charstoformid = '_0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz'.split('');
        if (!idlength) {
            idlength = Math.floor(Math.random() * charstoformid.length);
        }
        var uniqid = '';
        for (var i = 0; i < idlength; i++) {
            uniqid += charstoformid[Math.floor(Math.random() * charstoformid.length)];
        }
        if (jQuery("#" + uniqid).length === 0) {
            return uniqid;
        }
        else {
            return this.uniqueId(idlength);
        }
    };
    ;
    FwApplication.prototype.debugCss = function (idlength) {
        var html = [], fields = [], formfields, $appendContent;
        jQuery('.application').empty();
        formfields = ['text', 'number', 'password', 'select', 'date', 'time', 'email', 'url', 'phone', 'validation', 'combobox', 'money', 'zipcode', 'percent', 'ssn', 'color', 'checkbox', 'toggleswitch', 'radio', 'searchbox', 'textarea'];
        html.push('<div style="display:flex;flex-wrap:wrap;">');
        for (var i = 0; i < formfields.length; i++) {
            var field, fieldname;
            field = formfields[i];
            fieldname = field.toLowerCase().replace(/\b[a-z]/g, function (letter) { return letter.toUpperCase(); });
            html.push('<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="' + fieldname + ' Section" style="min-width:300px;">');
            html.push('<div class="flexrow">');
            html.push('<div data-control="FwFormField" data-type="' + field + '" class="fwcontrol fwformfield" data-caption="' + fieldname + ' Field"></div>');
            html.push('</div>');
            html.push('<div class="flexrow">');
            html.push('<div data-control="FwFormField" data-type="' + field + '" class="fwcontrol fwformfield" data-caption="' + fieldname + ' Field" data-enabled="false"></div>');
            html.push('</div>');
            html.push('<div class="flexrow">');
            html.push('<div data-control="FwFormField" data-type="' + field + '" class="fwcontrol fwformfield" data-caption="' + fieldname + ' Field" data-required="true"></div>');
            html.push('</div>');
            html.push('</div>');
        }
        html.push('</div>');
        $appendContent = jQuery(html.join(''));
        $appendContent.find('div[data-type="radio"]').append('<div data-caption="Value1" data-value="A"></div><div data-caption="Value1" data-value="S"></div><div data-caption="Value1" data-value="O"></div>');
        FwControl.renderRuntimeControls($appendContent.find('.fwcontrol'));
        var $selectFields = $appendContent.find('div[data-type="select"]');
        $selectFields.each(function (index, element) {
            var $selectField;
            $selectField = jQuery(element);
            FwFormField.loadItems($selectField, [
                { value: 'A', text: 'ACTIVE' },
                { value: 'D', text: 'DECEASED' },
                { value: 'T', text: 'TERMINATED' },
                { value: 'N', text: 'NO HIRE' },
                { value: 'R', text: 'RETIRED' },
                { value: 'I', text: 'INACTIVE' },
                { value: 'S', text: 'SUSPENDED' }
            ], true);
        });
        jQuery('.application').append($appendContent);
    };
    ;
    FwApplication.prototype.forceReloadCss = function () {
        var a = document.getElementsByTagName('link');
        for (var i = 0; i < a.length; i++) {
            var s = a[i];
            if (s.rel.toLowerCase().indexOf('stylesheet') >= 0 && s.href) {
                var h = s.href.replace(/(&|%5C?)forceReload=\d+/, '');
                s.href = h + (h.indexOf('?') >= 0 ? '&' : '?') + 'forceReload=' + (new Date().valueOf());
            }
        }
    };
    ;
    FwApplication.prototype.getModule = function (path) {
        var screen, $bodyContainer, $modifiedForms, $form, $tab;
        $bodyContainer = jQuery('#master-body');
        $modifiedForms = $bodyContainer.find('div[data-type="form"][data-modified="true"]');
        path = path.toLowerCase();
        if ($modifiedForms.length > 0) {
            $form = jQuery($modifiedForms[0]);
            $tab = jQuery('#' + $form.parent().attr('data-tabid'));
            $tab.click();
            FwModule.closeForm($form, $tab, path);
        }
        else {
            if (window.location.hash.replace('#/', '') !== path) {
                for (var i = 0; i < routes.length; i++) {
                    var match = routes[i].pattern.exec(path);
                    if (match != null) {
                        screen = routes[i].action(match);
                        break;
                    }
                }
                if (screen != null) {
                    window.location.hash = '/' + path;
                    if (this.screens.length > 0) {
                        if (typeof this.screens[this.screens.length - 1].unload !== 'undefined') {
                            this.screens[this.screens.length - 1].unload();
                        }
                        this.screens = [];
                    }
                    FwPopup.destroy(jQuery('.FwPopup-divPopup,.FwPopup-divOverlay'));
                    $bodyContainer
                        .empty()
                        .append(screen.$view);
                    this.screens.push(screen);
                    if (typeof screen.load !== 'undefined') {
                        screen.load();
                    }
                    document.body.scrollTop = 0;
                }
            }
        }
    };
    ;
    FwApplication.prototype.getApplicationOptions = function () {
        return JSON.parse(sessionStorage.getItem('applicationOptions'));
    };
    ;
    FwApplication.prototype.navigateHashChange = function (path) {
        var screen, match;
        path = path.toLowerCase();
        for (var i = 0; i < routes.length; i++) {
            match = routes[i].pattern.exec(path);
            if (match != null) {
                screen = routes[i].action(match);
                break;
            }
        }
        if (screen != null) {
            this.updateScreen(screen);
        }
    };
    ;
    FwApplication.prototype.navigate = function (path) {
        var me, screen;
        me = this;
        path = path.toLowerCase();
        if (window.location.hash.replace('#/', '') !== path) {
            var url = '/' + path;
            window.location.hash = url;
        }
        else {
            this.navigateHashChange(path);
        }
    };
    ;
    FwApplication.prototype.loadDefaultPage = function () {
        if (sessionStorage.getItem('authToken')) {
            this.navigate('home');
        }
        else {
            this.navigate('default');
        }
    };
    return FwApplication;
}());
window.onhashchange = function () {
    program.navigateHashChange(window.location.hash.replace('#/', ''));
};
//# sourceMappingURL=FwApplication.js.map