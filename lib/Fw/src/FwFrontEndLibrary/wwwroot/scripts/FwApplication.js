//---------------------------------------------------------------------------------
function FwApplication() {
    var me, activePath;

    me = this;
    me.screens = [];
}
//---------------------------------------------------------------------------------
FwApplication.prototype.pushScreen = function(screen) {
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
        .empty() // added this to get rid of the homepages doubling up in the support site.  Not sure about this line though, probably needs to get fixed in a better way.  MV 10/4/13
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
//---------------------------------------------------------------------------------
FwApplication.prototype.updateScreen = function(screen) {
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
    screen.$view.hide(0, function() {
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
//---------------------------------------------------------------------------------
FwApplication.prototype.popScreen = function() {
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
                screen.$view.hide(0, function() {
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
//---------------------------------------------------------------------------------
FwApplication.prototype.setMedia = function(media) {
    localStorage.setItem('media', media);
};
//---------------------------------------------------------------------------------
FwApplication.prototype.setScanMode = function(scanMode) {
    localStorage.scanMode = scanMode;
    if (typeof window.LineaScanner !== 'undefined') {
        LineaScanner.setScanMode(parseInt(scanMode));
    }
};
//---------------------------------------------------------------------------------
FwApplication.prototype.load = function() {
    var me = this;
    window.addEventListener("dragover",function(e){
      e = e || event;
      e.preventDefault();
    },false);
    window.addEventListener("drop",function(e){
      e = e || event;
      e.preventDefault();
    },false);
    // seems like we need to handle another window changing the browser storage area
    // not sure what's needed yet, but this event fires when it changes
    if (window.addEventListener) {
        // Normal browsers
        window.addEventListener("storage", FwApplication.onstorage, false);
    } else {
        // for IE
        window.attachEvent("onstorage", FwApplication.onstorage);
    }
    if(!localStorage.getItem('media') || (localStorage.getItem('media').length === 0)) {
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
            this.setMedia('mobile');
        } else {
            this.setMedia('desktop');
        }
    } else {
        var media = localStorage.getItem('media');
        this.setMedia(media);
    }
};
//---------------------------------------------------------------------------------
FwApplication.onstorage = function(event) {
    //window.location.reload(false);
    //alert(JSON.stringify(event));
};
//---------------------------------------------------------------------------------
FwApplication.prototype.popOutURL = function(url) {
    var settings, $newwindow, html;

    settings = 'scrollbars=yes,resizeable=yes,toolbar=no,status=no,menubar=no,directories=no,titlebar=no,location=no,addressbar=yes';

    newwindow = window.open(url, '', settings);
    
    //$newwindow = jQuery(newwindow);
    
    //html = [];
    //html.push('<script type="text/javascript">');
    //    html.push('jQuery(function() {');
    //        html.push('program.getModule(' + url + ');');
    //    html.push('};');
    //html.push('</script>');
    //html = html.join('');
    
    //$newwindow.find('body').append(html);
    //newwindow.document.body.innerHTML += (html);

    //newwindow.addEventListener('load', function() {
    //    program.getModule(url);
    //    }, false);

    return false;
};
//---------------------------------------------------------------------------------
FwApplication.prototype.uniqueId = function (idlength) {
    var charstoformid = '_0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz'.split('');
    if (! idlength) {
        idlength = Math.floor(Math.random() * charstoformid.length);
    }
    var uniqid = '';
    for (var i = 0; i < idlength; i++) {
        uniqid += charstoformid[Math.floor(Math.random() * charstoformid.length)];
    }
    // one last step is to check if this ID is already taken by an element before
    if(jQuery("#"+uniqid).length === 0) {
        return uniqid;
    } else {
        return uniqID(20);
    }
};
//---------------------------------------------------------------------------------
FwApplication.prototype.debugCss = function (idlength) {
    var html = [], fields = [], formfields, $appendContent;
    
    jQuery('.application').empty();
    formfields = ['text', 'number', 'password', 'select', 'date', 'time', 'email', 'url', 'phone', 'validation', 'combobox', 'money', 'zipcode', 'percent', 'ssn', 'color', 'checkbox', 'toggleswitch', 'radio', 'searchbox', 'textarea'];

    html.push('<div style="display:flex;flex-wrap:wrap;">');
    for (var i = 0; i < formfields.length; i++) {
        var field, fieldname;
        field     = formfields[i];
        fieldname = field.toLowerCase().replace(/\b[a-z]/g, function(letter) {return letter.toUpperCase();});
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

    //load options for the radio buttons
    $appendContent.find('div[data-type="radio"]').append('<div data-caption="Value1" data-value="A"></div><div data-caption="Value1" data-value="S"></div><div data-caption="Value1" data-value="O"></div>');

    FwControl.renderRuntimeControls($appendContent.find('.fwcontrol'));

    //load options for the selects
    $selectFields = $appendContent.find('div[data-type="select"]');
    $selectFields.each(function(index, element) {
        var $selectField;
        $selectField = jQuery(element);
        FwFormField.loadItems($selectField, [
            {value:'A',     text:'ACTIVE'},
            {value:'D',     text:'DECEASED'},
            {value:'T',     text:'TERMINATED'},
            {value:'N',     text:'NO HIRE'},
            {value:'R',     text:'RETIRED'},
            {value:'I',     text:'INACTIVE'},
            {value:'S',     text:'SUSPENDED'}
        ], true);
    });

    jQuery('.application').append($appendContent);
};
//---------------------------------------------------------------------------------
FwApplication.prototype.forceReloadCss = function() {
    var i,a,s;
    a=document.getElementsByTagName('link');
    for(i = 0; i < a.length; i++) {
        s=a[i];
        if(s.rel.toLowerCase().indexOf('stylesheet')>=0 && s.href) {
            var h = s.href.replace(/(&|%5C?)forceReload=\d+/,'');
            s.href = h + (h.indexOf('?') >=0 ? '&' : '?') + 'forceReload=' + (new Date().valueOf());
        }
    }
};
//---------------------------------------------------------------------------------
