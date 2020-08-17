var FwMobileMasterController = {};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.getMasterView = function(viewModel, properties) {
    jQuery('html').removeClass('desktop').addClass("mobile");
    var combinedViewModel, $view, siteName, $formcontrols, $modulecontrols;
    if (!sessionStorage.getItem('fullname')) {
        sessionStorage.setItem('fullname', '');
    }
    siteName = '';
    if(applicationConfig.debugMode) {
        if (typeof sessionStorage.siteName === 'string') {
            siteName = sessionStorage.getItem('siteName') + ':';
        }
    }
    combinedViewModel = jQuery.extend({
        valueWebUserFullName:   sessionStorage.getItem('fullname')
    }, viewModel);

    $view = jQuery(Mustache.render(jQuery('#tmpl-pages-Master').html(), combinedViewModel));

    FwMobileMasterController.modulecontrols = $view.find('#module-controls');
    FwMobileMasterController.formcontrols   = $view.find('#master-header-row3');
    FwMobileMasterController.tabcontrols    = $view.find('#master-header-row4');

    $view
        .on('click', '#btnmenu', function() {
            $view.find('.menu').addClass('is-active');
        })
        .on('click', '.menu-close', function() {
            $view.find('.menu').removeClass('is-active');
        })
        .on('click','.menu-body-footer', function() {
            program.forceReloadCss();
        })
    ;

    FwMobileMasterController.modulecontrols
        .on({
            'clearcontrols': function() {
                jQuery(this).empty();
            },
            'addnew': function() {
                var $addnew = jQuery('<div id="add-icon"></div>');
                jQuery(this).append($addnew);
                return $addnew;
            }
        })
    ;
    
    FwMobileMasterController.formcontrols
        .on({
            'clearcontrols': function() {
                jQuery(this).empty();
            },
            'addsave': function() {
                var $save = jQuery('<div id="save" class="btn">Save</div>');
                jQuery(this).append($save);
                return $save;
            },
            'addsubmit': function() {
                var $save = jQuery('<div id="save" class="btn">Submit</div>');
                jQuery(this).append($save);
                return $save;
            },
            'addcancel': function() {
                var $cancel = jQuery('<div id="cancel" class="btn">Cancel</div>');
                jQuery(this).append($cancel);
                return $cancel;
            },
            'addcontinue': function() {
                var $cancel = jQuery('<div id="continue" class="btn">Continue</div>');
                jQuery(this).append($cancel);
                return $cancel;
            },
            'addback': function() {
                var $cancel = jQuery('<div id="back" class="btn">Back</div>');
                jQuery(this).append($cancel);
                return $cancel;
            }
        })
    ;

    FwMobileMasterController.tabcontrols
        .on('click', '.tab', function() {
            var $this = jQuery(this);
            var $tabs = $this.siblings();
            $tabs.each(function(index, element) {
                var $tab = jQuery(element);
                $tab.removeClass('active');
            });
            $this.addClass('active');
        })
    ;
    FwMobileMasterController.tabcontrols.clearcontrols = function() {
        jQuery(this).empty();
    };
    FwMobileMasterController.tabcontrols.addtab = function(caption, active) {
        var $tab = jQuery('<div class="tab">' + caption + '</div>');
        if (active === true) $tab.addClass('active');
        jQuery(this).append($tab);
        return $tab;
    };

    FwMobileMasterController.generateDeviceStatusIcons($view);

    FwMobileMasterController.generateMenu();
    $view.append(FwMobileMasterController.menu);

    return $view;
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.generateMenu = function() {
    var html = [];
    html.push('<div class="menu">');
    html.push('  <div class="menu-body">');
    html.push('    <div class="menu-body-top">');
    html.push('      <div class="apptitle center">' + program.htmlname + '</div>');
    html.push('      <div class="menu-top-controls">');
    html.push('        <div class="username" style="overflow:auto;"><div class="caption" style="float:left;">User:</div><div class="value" style="float:left;margin-left:5px;">' + FwFunc.fixCaps(sessionStorage.getItem('fullname')) + '</div></div>');
    html.push('      </div>');
    html.push('    </div>');
    html.push('    <div class="menu-body-links"></div>');
    html.push('    <div class="menu-body-footer center">' + program.name + ' v' + applicationConfig.version + '</div>');
    html.push('  </div>');
    html.push('  <div class="menu-close"></div>');
    html.push('</div>');
    var $menu = jQuery(html.join(''));

    FwMobileMasterController.generateMenuLinks($menu);

    FwMobileMasterController.menu = $menu;
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.generateDeviceStatusIcons = function ($containgelement) {
    var $devicestatusicons;
    if (typeof $containgelement !== 'undefined') {
        $devicestatusicons = $containgelement.find('#device-status-icons');
    } else {
        $devicestatusicons = jQuery('#device-status-icons');
    }
    var html = [];
    if (program.name === 'RentalWorks') {
        if (program.runningInCordova === true) {
            // Network Connection
            // disabling this until next appstore release (to avoid questions)
            //html.push('<div class="device-status-icon net">');
            //if (program.online === true) {
            //    html.push('  <div class="icon"><i class="material-icons" style="color:#ffffff;">&#xE1D8;</i></div>');
            //} else {
            //    html.push('  <div style="text-align:center;"><i class="material-icons" style="color:#ff0000;">&#xE1DA;</i></div>');
            //}
            //html.push('  <div class="caption">NET</div>');
            //html.push('</div>');

            //RFID
            // disabling this until next appstore release (doesn't work right without the release)
            //if (typeof program.showRfidStatusIcon !== 'undefined' && program.showRfidStatusIcon === true) {
            //    html.push('<div class="device-status-icon rfid">');
            //    if (RwRFID.isConnected === true) {
            //        html.push('  <div class="icon"><i class="material-icons" style="color:#ffffff;">&#xE1A7;</i></div>');
            //    } else {
            //        html.push('  <div class="icon"><i class="material-icons" style="color:#ff0000;">&#xE1A9;</i></div>');
            //    }
            //    html.push('  <div class="caption">RFID</div>')
            //    html.push('</div>');
            //}

            //Battery
            //html.push('<div class="device-status-icon battery">');
            //if (program.lineaPro_BatteryStatus_Status === 'unknown') {
            //    html.push('  <div class="icon"><i class="material-icons" style="color:#ffffff;">&#xE1A6;</i></div>');
            //}
            //else if (program.lineaPro_BatteryStatus_Status === 'critical') {
            //    html.push('  <div class="icon"><i class="material-icons" style="color:#ff0000;">&#xE19C;</i></div>');
            //}
            //else if (program.lineaPro_BatteryStatus_Status === 'low') {
            //    html.push('  <div class="icon"><i class="material-icons" style="color:#ffff00;">&#xE19C;</i></div>');
            //}
            //else { //ok and anything else
            //    html.push('  <div class="icon"><i class="material-icons" style="color:#ffffff;">&#xE1A4;</i></div>');
            //}
            //html.push('  <div class="caption">' + program.lineaPro_BatteryStatus_Level + '</div>')
            //html.push('</div>');
        }
    }
    $devicestatusicons.empty().append(html.join('\n'));
}
//----------------------------------------------------------------------------------------------
FwMobileMasterController.generateMenuLinks = function($menu) {
    var html, $link, $linkgroup;

    var nodeApplication = FwApplicationTree.getMyTree();
    var applicationOptions = program.getApplicationOptions();

    $link      = FwMobileMasterController.generateLink($menu, 'Home',        'theme/images/icons/128/home.001.png',    'home/home');           $menu.find('.menu-body-links').append($link);
    $link.addClass('navHome');
    
    var secNodeMobile = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Mobile');
    var nodeMobile = Constants.Modules.Mobile;
    for (var moduleKey in nodeMobile.children) {
        nodeModule = nodeMobile.children[moduleKey];
        if (nodeModule.visible) {
            switch (nodeModule.nodetype) {
                case 'Module':
                    if (typeof nodeModule.usertype === 'string') {
                        hasusertype = (jQuery.inArray(sessionStorage.userType, nodeModule.usertype.split(',')) !== -1);
                    } else {
                        hasusertype = true;
                    }
                    var hasApplicationOptions = true;
                    if (typeof nodeModule.applicationoptions === 'string') {
                        var moduleApplicationOptions = nodeModule.applicationoptions.split(',');
                        for (var optionNo = 0; optionNo < moduleApplicationOptions.length; optionNo++) {
                            var option = moduleApplicationOptions[optionNo];
                            if (applicationOptions.hasOwnProperty(option)) {
                                hasApplicationOptions &= option.enabled;
                            }
                        }
                    }
                    if (hasusertype && hasApplicationOptions) {
                        var secNodeModule = FwApplicationTree.getNodeById(secNodeMobile, nodeModule.id);
                        if (secNodeModule !== null && secNodeModule.properties.visible === 'T') {
                            var caption = nodeModule.caption;
                            if (typeof AppLanguages !== 'undefined') {
                                caption = AppLanguages.translate(caption);
                            }
                            $link = FwMobileMasterController.generateLink($menu, caption, nodeModule.iconurl, nodeModule.nav); $menu.find('.menu-body-links').append($link);
                        }
                    }
                    break;
            }
        }
    }

    $link      = FwMobileMasterController.generateLink($menu, 'Settings',    'theme/images/icons/128/preferences.png', 'account/preferences'); $menu.find('.menu-body-links').append($link);
    if (!applicationConfig.demoMode) {
        $link      = FwMobileMasterController.generateLink($menu, 'Logoff',      'theme/images/icons/128/logoff.png',      'logoff');      $menu.find('.menu-body-links').append($link);
    }
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.generateLink = function($menu, caption, imagesrc, nav) {
    var html = [];
    html.push('<div class="menu-body-link">');
        html.push('<div class="menu-body-link-icon"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + imagesrc + '" class="linkicon" /></div>');
        html.push('<div class="menu-body-link-caption">' + caption + '</div>');
    html.push('</div>');
    var $link = jQuery(html.join(''));

    $link.on('click', function(e) {
        try {
            program.navigate(nav);
            //$menu.removeClass('is-active');
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    return $link;
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.generateLinkGroup = function(caption) {
    var html = [];
    html.push('<div class="menu-body-linkgroup">');
        html.push('<div class="menu-body-linkgroupheader">' + caption + '</div>');
    html.push('</div>');
    var $linkgroup = jQuery(html.join(''));

    return $linkgroup;
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.setTitle = function(object) {
    if (typeof object === 'string') {
        if (object.length > 0) {
            object = jQuery('<div>' + object + '</div>');
        } else {
            jQuery('#master-header-row2').empty().hide();
            return;
        }
    }
    var $title = jQuery('<div class="title"></div>');
    $title.append(object);

    jQuery('#master-header-row2').empty().append($title).show();
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.setModuleCaption = function($object) {
    jQuery('#master-header-caption').empty().append($object);
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.addFormControl = function (screen, name, direction, icon, isvisible, onclick) {
    icon = icon === 'back' ? '&#xE5CB;' : icon; //chevron_left
    icon = icon === 'continue' ? '&#xE5CC;' : icon; //chevron_right
    icon = icon === 'cancel' ? '&#xE14C;' : icon; //clear
    var $btn = jQuery('<div data-name="' + name + '" class="btn ' + direction + '"><i class="material-icons">' + icon + '</i>' + name + '</div>');
    var $formcontrols = FwMobileMasterController.findFormControls(screen);
    $btn.on('click', onclick);
    $btn.toggle(isvisible);
    $formcontrols.append($btn);
    return $btn;
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.findFormControls = function(screen) {
    return screen.$view.find('#master-header-row3');
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.findFormControl = function(screen, name) {
    var $formcontrols = FwMobileMasterController.findFormControls(screen);
    return $formcontrols.find('div[data-name="' + name + '"]');
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.toggleFormControl = function(screen, name, isvisible) {
    var $btn = FwMobileMasterController.findFormControl(screen, name);
    $btn.toggle(isvisible);
};
//----------------------------------------------------------------------------------------------
FwMobileMasterController.removeFormControl = function(screen, name) {
    var $btn = FwMobileMasterController.findFormControl(screen, name);
    $btn.remove();
};
//----------------------------------------------------------------------------------------------
