class FwApplication {
    constructor() {
        this.screens = [];
        this.setAudioMode('none');
        var $templates = jQuery('script[data-ajaxload="true"]');
        $templates.each(function () {
            if (jQuery(this).attr("src")) {
                jQuery.ajax(jQuery(this).attr("src"), {
                    async: true,
                    context: this,
                    success: function (data) { jQuery(this).html(data); }
                });
            }
        });
    }
    pushScreen(screen) {
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
    }
    updateScreen(screen) {
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
    }
    popScreen() {
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
    }
    setAudioMode(mode) {
        switch (mode) {
            case 'DTDevices':
                this.audioMode = 'DTDevices';
                this.audioSuccessArray = [1200, 300];
                this.audioErrorArray = [800, 200, 600, 200];
                break;
            case 'html5':
                this.audioMode = 'html5';
                if (typeof this.audioSuccess === 'undefined') {
                    this.audioSuccess = new Audio('theme/fwaudio/success2.wav');
                    this.audioError = new Audio('theme/fwaudio/error2.wav');
                }
                break;
        }
    }
    playStatus(isSuccessful) {
        if (isSuccessful) {
            switch (this.audioMode) {
                case 'DTDevices':
                    window.DTDevices.playSound(this.audioSuccessArray);
                    break;
                case 'html5':
                    this.audioSuccess.currentTime = 0;
                    this.audioSuccess.play();
                    break;
            }
        }
        else {
            switch (this.audioMode) {
                case 'DTDevices':
                    window.DTDevices.playSound(this.audioErrorArray);
                    break;
                case 'html5':
                    this.audioError.currentTime = 0;
                    this.audioError.play();
                    break;
            }
        }
    }
    setMedia(media) {
        localStorage.setItem('media', media);
    }
    setScanMode(scanMode) {
        localStorage.setItem('scanMode', scanMode);
        if (typeof window['LineaScanner'] !== 'undefined') {
            window['LineaScanner'].setScanMode(parseInt(scanMode));
        }
    }
    load() {
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
    }
    popOutURL(url) {
        let settings = 'scrollbars=yes,resizeable=yes,toolbar=no,status=no,menubar=no,directories=no,titlebar=no,location=no,addressbar=yes';
        var newwindow = window.open(url, '', settings);
    }
    ;
    popOutTab(url) {
        let settings = 'scrollbars=yes,resizeable=yes,toolbar=no,status=no,menubar=no,directories=no,titlebar=no,location=yes,addressbar=yes';
        var newwindow = window.open(url, '_blank');
        newwindow.focus();
    }
    ;
    uniqueId(idlength) {
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
    }
    ;
    debugCss(idlength) {
        var html = [], fields = [], formfields, $appendContent;
        jQuery('.application').empty();
        formfields = ['text', 'number', 'password', 'select', 'date', 'time', 'email', 'url', 'phone', 'validation', 'multiselectvalidation', 'combobox', 'money', 'zipcode', 'percent', 'ssn', 'color', 'checkbox', 'toggleswitch', 'radio', 'searchbox', 'textarea'];
        html.push('<div style="display:flex;flex-wrap:wrap;">');
        for (var i = 0; i < formfields.length; i++) {
            var field, fieldname;
            field = formfields[i];
            fieldname = field.toLowerCase().replace(/\b[a-z]/g, function (letter) { return letter.toUpperCase(); });
            html.push(`<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="${fieldname} Section" style="min-width:300px;">`);
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" data-type="${field}" class="fwcontrol fwformfield" data-caption="${fieldname} Field"></div>`);
            html.push('  </div>');
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" data-type="${field}" class="fwcontrol fwformfield" data-caption="${fieldname} Field" data-enabled="false"></div>`);
            html.push('  </div>');
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" data-type="${field}" class="fwcontrol fwformfield" data-caption="${fieldname} Field" data-required="true"></div>`);
            html.push('  </div>');
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
    }
    ;
    forceReloadCss() {
        var a = document.getElementsByTagName('link');
        for (var i = 0; i < a.length; i++) {
            var s = a[i];
            if (s.rel.toLowerCase().indexOf('stylesheet') >= 0 && s.href) {
                var h = s.href.replace(/(&|%5C?)forceReload=\d+/, '');
                s.href = h + (h.indexOf('?') >= 0 ? '&' : '?') + 'forceReload=' + (new Date().valueOf());
            }
        }
    }
    ;
    getModule(path) {
        var screen, $bodyContainer, $modifiedForms, $form, $tab;
        $bodyContainer = jQuery('#master-body');
        $modifiedForms = $bodyContainer.find('div[data-type="form"][data-modified="true"]');
        path = path.toLowerCase();
        if ($modifiedForms.length > 0) {
            if (jQuery($modifiedForms[0]).parent().data('type') === 'settings-row') {
                this.navigate(path);
            }
            $form = jQuery($modifiedForms[0]);
            $tab = jQuery('#' + $form.parent().attr('data-tabid'));
            $tab.click();
            FwModule.closeForm($form, $tab, path);
        }
        else {
            this.navigate(path);
        }
    }
    ;
    getApplicationOptions() {
        return JSON.parse(sessionStorage.getItem('applicationOptions'));
    }
    ;
    navigateHashChange(path) {
        var screen, $appendToContainer;
        FwPopup.destroy(jQuery('.FwPopup-divPopup,.FwPopup-divOverlay'));
        FwAppData.abortAllRequests();
        path = path.toLowerCase();
        var foundmatch = false;
        for (var i = 0; i < routes.length; i++) {
            var match = routes[i].pattern.exec(path);
            if (match != null) {
                foundmatch = true;
                screen = routes[i].action(match);
                break;
            }
        }
        if (foundmatch && typeof screen !== 'undefined' && screen !== null) {
            if ((this.screens.length > 0) && (typeof this.screens[0].unload !== 'undefined')) {
                this.screens[0].unload();
                this.screens = [];
            }
            this.screens[0] = screen;
            var $applicationContainer = jQuery('#application');
            if ((jQuery('#master').length == 0) && (sessionStorage.getItem('authToken') != null)) {
                $applicationContainer.empty().append(masterController.getMasterView()).removeClass('hidden');
            }
            if (jQuery('#master').length > 0) {
                $appendToContainer = jQuery('#master-body');
            }
            else {
                $appendToContainer = $applicationContainer;
            }
            $appendToContainer.empty().append(screen.$view);
            if (typeof screen.load !== 'undefined') {
                screen.load();
            }
            document.body.scrollTop = 0;
        }
        if (!foundmatch) {
            FwFunc.showError(`404: Not Found - ${path}`);
        }
    }
    ;
    navigate(path) {
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
    }
    ;
    loadDefaultPage() {
        if (sessionStorage.getItem('authToken')) {
            if (window.location.hash.replace('#/', '') !== '' && window.location.hash.replace('#/', '') !== 'home') {
                sessionStorage.setItem('redirectPath', window.location.hash.replace('#/', ''));
            }
            this.navigate('home');
        }
        else {
            this.navigate('default');
        }
    }
    setApplicationTheme(setTheme) {
        jQuery('html').removeClass(function (index, className) {
            return (className.match(/(^|\s)theme-\S+/g) || []).join(' ');
        });
        jQuery('html').addClass(setTheme);
    }
}
window.onhashchange = function () {
    program.navigateHashChange(window.location.hash.replace('#/', ''));
};
//# sourceMappingURL=FwApplication.js.map