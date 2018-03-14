var FwHybridMasterController = {};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.getMasterView = function(viewModel, properties) {
    jQuery('html').removeClass('desktop').addClass("mobile");
    jQuery('title').html(viewModel.captionPageTitle);
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
    $view.css({
        'max-width': '1024px',
        'font-family':"'Open Sans', sans-serif'"
    });
    $view.find('#master-header-row1').hide();
    FwHybridMasterController.modulecontrols = $view.find('#module-controls');
    FwHybridMasterController.formcontrols   = $view.find('#master-header-row3');
    FwHybridMasterController.tabcontrols    = $view.find('#master-header-row4');

    //$view
    //    .on('click', '#btnmenu', function() {
    //        $view.find('.menu').addClass('is-active');
    //    })
    //    .on('click', '.menu-close', function() {
    //        $view.find('.menu').removeClass('is-active');
    //    })
    //    .on('click','.menu-body-footer', function() {
    //        program.forceReloadCss();
    //    })
    //;

    FwHybridMasterController.modulecontrols
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
    
    FwHybridMasterController.formcontrols
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

    FwHybridMasterController.tabcontrols
        .on('click', '.tab', function() {
            var $this, $tabs;
            $this = jQuery(this);
            $tabs = FwHybridMasterController.tabcontrols.find('.tab');
            $tabs.each(function(index, element) {
                var $tab;
                $tab = jQuery(element);
                $tab.removeClass('active');
            });
            $this.addClass('active');
        })
    ;
    FwHybridMasterController.tabcontrols.clearcontrols = function() {
        jQuery(this).empty();
    };
    FwHybridMasterController.tabcontrols.addtab = function(caption, active) {
        var $tab = jQuery('<div class="tab">' + caption + '</div>');
        if (active === true) $tab.addClass('active');
        jQuery(this).append($tab);
        return $tab;
    };

    //if (typeof FwHybridMasterController.menu == 'undefined') {
        //FwHybridMasterController.generateMenu();
    //}

   // $view.append(FwHybridMasterController.menu);

    return $view;
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.generateMenu = function() {
    var html, $menu, systemname;
    
    html = [];

    html.push('<div class="menu">');
        html.push('<div class="menu-body">');
            html.push('<div class="menu-body-top">');
                html.push('<div class="apptitle center" style="padding-top:10px 0;background-color:rgba(0,0,0,.4);font-size:20px;">' + program.htmlname + '</div>');
                html.push('<div class="" style="padding:5px;background-color:rgba(0,0,0,.4);overflow:auto;font-size:12px;">');
                    html.push('<div class="username" style="float:left;"><div class="caption" style="float:left;">User:</div><div class="value" style="float:left;margin-left:5px;">' + FwFunc.fixCaps(sessionStorage.getItem('fullname')) + '</div></div>');
                    if (program.name === 'GateWorks') {
                        html.push('<div class="gate" style="float:right"><div class="caption" style="float:left;">Gate:</div><div class="value" style="float:left;margin-left:5px;">' + localStorage.getItem('gate') + '</div></div>');
                    }
                html.push('</div>');
            html.push('</div>');
            html.push('<div class="menu-body-links"></div>');
            html.push('<div class="menu-body-footer center" style="font-size:10px;padding:5px 0;background-color:rgba(0,0,0,.4);width:100%;">' + program.name + ' v' + applicationConfig.version + '</div>');
        html.push('</div>');
        html.push('<div class="menu-close"></div>');
    html.push('</div>');
    $menu = jQuery(html.join(''));

    FwHybridMasterController.generateMenuLinks($menu);

    FwHybridMasterController.menu = $menu;
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.generateMenuLinks = function($menu) {
    var html, $link, $linkgroup, appOptions;

    appOptions = program.getApplicationOptions();

    $link      = FwHybridMasterController.generateLink($menu, 'Home',        'theme/images/icons/128/home.001.png',    'home/home');           $menu.find('.menu-body-links').append($link);
    $link.addClass('navHome');

    //for (var i = 0; i < ModuleList.length; i++) {
    //    var hasusertype          = (jQuery.inArray(sessionStorage.userType, ModuleList[i].usertype) !== -1);
    //    var hasapplicationoption = ((typeof appOptions[ModuleList[i].appoption] !== 'undefined') && (appOptions[ModuleList[i].appoption].enabled));

    //    if (((ModuleList[i].appoption === '') || hasapplicationoption) && (hasusertype)) {
    //        $link = FwHybridMasterController.generateLink($menu, ModuleList[i].name, ModuleList[i].icon, ModuleList[i].nav); $menu.find('.menu-body-links').append($link);
    //    }
    //}

    var nodeSystem = FwApplicationTree.getMyTree();
    var nodeApplication = null;
    for (var appno = 0; appno < nodeSystem.children.length; appno++) {
        if (typeof FwApplicationTree.currentApplicationId === 'undefined') {
            throw 'Need to set FwApplicationTree.currentApplicationId in Application.js. {90B5B30D-F455-406E-9ABD-E2BBAE711CE4}';
        }
        if (nodeSystem.children[appno].id === FwApplicationTree.currentApplicationId) {
            nodeApplication = nodeSystem.children[appno];
        }
    }
    if (nodeApplication === null) {
        throw 'Unable to find Application node in group tree. {742BE959-EB77-46BE-A52A-67D9AD108353}';
    }
    var nodeHome = nodeApplication.children[0];
    for (var moduleno = 0; moduleno < nodeHome.children.length; moduleno++) {
        nodeModule = nodeHome.children[moduleno];
        if (nodeModule.properties.visible === 'T') {
            switch (nodeModule.properties.nodetype) {
                case 'Module':
                    if (typeof nodeModule.properties.usertype === 'string') {
                        hasusertype = (jQuery.inArray(sessionStorage.userType, nodeModule.properties.usertype.split(',')) !== -1);
                    } else {
                        hasusertype = true;
                    }
                    if (hasusertype) {
                        $link = FwHybridMasterController.generateLink($menu, nodeModule.properties.caption, nodeModule.properties.iconurl, nodeModule.properties.modulenav); $menu.find('.menu-body-links').append($link);
                    }
                    break;
            }
        }
    }

    $link      = FwHybridMasterController.generateLink($menu, 'Settings',    'theme/images/icons/128/preferences.png', 'account/preferences'); $menu.find('.menu-body-links').append($link);
    $link      = FwHybridMasterController.generateLink($menu, 'Logoff',      'theme/images/icons/128/logoff.png',      'account/logoff');      $menu.find('.menu-body-links').append($link);
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.generateLink = function($menu, caption, imagesrc, nav) {
    var html, $link;

    html = [];
    html.push('<div class="menu-body-link">');
        html.push('<div class="menu-body-link-icon"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + imagesrc + '" class="linkicon" /></div>');
        html.push('<div class="menu-body-link-caption">' + caption + '</div>');
    html.push('</div>');
    $link = jQuery(html.join(''));

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
FwHybridMasterController.generateLinkGroup = function(caption) {
    var html, $linkgroup;

    html = [];
    html.push('<div class="menu-body-linkgroup">');
        html.push('<div class="menu-body-linkgroupheader">' + caption + '</div>');
    html.push('</div>');
    $linkgroup = jQuery(html.join(''));

    return $linkgroup;
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.setTitle = function(object) {
    var $title;
    if (typeof object === 'string') {
        object = jQuery('<div>' + object + '</div>');
    }
    $title = jQuery('<div class="title"></div>');
    $title.append(object);

    jQuery('#master-header-row2').empty().append($title);
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.setModuleCaption = function($object) {
    jQuery('#master-header-caption').empty().append($object);
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.addFormControl = function(screen, name, direction, icon, isvisible, onclick) {
    var html = [];
    html.push('<div data-name="' + name + '" class="btn ' + direction + '">');
    if (direction === 'left' && typeof icon === 'string' && icon.length > 0) {
        html.push('<i class="material-icons">' + icon + '</i>');
    }
    html.push(name)
    if (direction === 'right' && typeof icon === 'string' && icon.length > 0) {
        html.push('<i class="material-icons">' + icon + '</i>');
    }
    html.push('</div>');
    html = html.join('');
    var $btn = jQuery(html);
    var $formcontrols = FwHybridMasterController.findFormControls(screen);
    $btn.on('click', onclick);
    $btn.toggle(isvisible);
    $formcontrols.append($btn);
    return $btn;
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.findFormControls = function(screen) {
    return screen.$view.find('#master-header-row3');
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.findFormControl = function(screen, name) {
    var $formcontrols = FwHybridMasterController.findFormControls(screen);
    return $formcontrols.find('div[data-name="' + name + '"]');
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.toggleFormControl = function(screen, name, isvisible) {
    var $btn = FwHybridMasterController.findFormControl(screen, name);
    $btn.toggle(isvisible);
};
//----------------------------------------------------------------------------------------------
FwHybridMasterController.removeFormControl = function(screen, name) {
    var $btn = FwHybridMasterController.findFormControl(screen, name);
    $btn.remove();
};
//----------------------------------------------------------------------------------------------
