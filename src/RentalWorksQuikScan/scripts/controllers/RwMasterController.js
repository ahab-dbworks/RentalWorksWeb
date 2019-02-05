var RwMasterController = {};
//----------------------------------------------------------------------------------------------
//RwMasterController.getMasterLoggedInView = function(viewModel, properties) {
//    var combinedViewModel, view, siteName;
//    if (!sessionStorage.getItem('fullname')) {
//        sessionStorage.setItem('fullname', '');
//    }
//    siteName = '';
//    if(applicationConfig.debugMode) {
//        if (typeof sessionStorage.siteName !== 'undefined') {
//            siteName = sessionStorage.getItem('siteName') + ':';
//        }
//    }
//    combinedViewModel = jQuery.extend({
//        valueWebUserFullName:   sessionStorage.getItem('fullname')
//      , valueVersion:           siteName + applicationConfig.version
//    }, viewModel);
//    view = jQuery(Mustache.render(jQuery('#tmpl-masterLoggedIn').html(), combinedViewModel));

//    view.on('click', '#masterLoggedInView-btnHome', function() {
//        try {
//            if (RwAppData.verifyHasAuthToken()) {
//                application.navigate('home/home');
//            } else {
//                application.navigate('account/login');
//            }
//        } catch(ex) {
//            FwFunc.showError(ex);
//        }
//    });

//    return view;
//};
//----------------------------------------------------------------------------------------------
RwMasterController.getMasterView = function(viewModel, properties) {
    var combinedViewModel, $view, siteName, version;
    if (!sessionStorage.getItem('fullname')) {
        sessionStorage.setItem('fullname', '');
    }
    siteName = '';
    if(applicationConfig.debugMode) {
        if (typeof sessionStorage.siteName === 'string') {
            siteName = sessionStorage.getItem('siteName') + ':';
        }
    }
    if (typeof sessionStorage.getItem('authToken') === 'string') {
        version = (typeof localStorage.getItem('gate') === 'string')  ? localStorage.getItem('gate') : '&nbsp;'
    } else {
        version = siteName + applicationConfig.version
    }
    combinedViewModel = jQuery.extend({
        valueWebUserFullName:   sessionStorage.getItem('fullname')
      , valueVersion:           version
    }, viewModel);
     
    $view = jQuery(Mustache.render(jQuery('#tmpl-masterLoggedIn').html(), combinedViewModel));

    $view
        .on('click', '#btnmenu', function() {
            $view.find('.menu').addClass('is-active');
        })
        .on('click', '.menu-close', function() {
            $view.find('.menu').removeClass('is-active');
        })
        .on('click','.menu-body-footer', function() {
            var i,a,s;
            a=document.getElementsByTagName('link');
            for(i = 0; i < a.length; i++) {
                s=a[i];
                if(s.rel.toLowerCase().indexOf('stylesheet')>=0 && s.href) {
                    var h = s.href.replace(/(&|%5C?)forceReload=\d+/,'');
                    s.href = h + (h.indexOf('?') >=0 ? '&' : '?') + 'forceReload=' + (new Date().valueOf())
                }
            }
        })
    ;

    //if (typeof MasterController.menu == 'undefined') {
        RwMasterController.generateMenu();
    //}

    $view.append(RwMasterController.menu);

    return $view;
};
//----------------------------------------------------------------------------------------------
RwMasterController.generateMenu = function() {
    var html, $menu;
    
    html = [];

    html.push('<div class="menu">');
    html.push('  <div class="menu-body">');
    html.push('    <div class="menu-body-top">');
    html.push('      <div class="apptitle bgothm center" style="padding-top:10px 0;background-color:rgba(0,0,0,.4);font-size:18px;color:#2f2f2f;">Rental<span style="color:#6f30b3;">Works</span> QuikScan</div>');
    html.push('      <div class="" style="padding:5px;background-color:rgba(0,0,0,.4);overflow:auto;font-size:12px;">');
    html.push('      <div class="username" style="float:left;"><div class="caption" style="float:left;">User:</div><div class="value" style="float:left;margin-left:5px;">' + RwAppData.fixCaps(sessionStorage.getItem('fullname')) + '</div></div>');
    html.push('    </div>');
    html.push('  </div>');
    html.push('  <div class="menu-body-links"></div>');
    html.push('    <div class="menu-body-footer center" style="font-size:10px;padding:5px 0;background-color:rgba(0,0,0,.4);width:100%;">RentalWorks QuikScan v' + applicationConfig.version + '</div>');
    html.push('  </div>')
    html.push('  <div class="menu-close"></div>');
    html.push('</div>');
    $menu = jQuery(html.join(''));

    RwMasterController.generateMenuLinks($menu);

    RwMasterController.menu = $menu;
};
//----------------------------------------------------------------------------------------------
RwMasterController.generateMenuLinks = function($menu) {
    var html, $link, $linkgroup, appOptions;

    appOptions = application.getApplicationOptions();

    $link      = RwMasterController.generateLink($menu, 'Home',        'theme/images/icons/128/home.001.png',    'home/home');           $menu.find('.menu-body-links').append($link);
    $link.addClass('navHome');

    for (var i = 0; i < ModuleList.length; i++) {
        var hasusertype          = (jQuery.inArray(sessionStorage.userType, ModuleList[i].usertype) != -1);
        var hasapplicationoption = ((typeof appOptions[ModuleList[i].appoption] != 'undefined') && (appOptions[ModuleList[i].appoption].enabled));

        if (((ModuleList[i].appoption == '') || hasapplicationoption) && (hasusertype)) {
            $link = RwMasterController.generateLink($menu, ModuleList[i].name, ModuleList[i].icon, ModuleList[i].nav); $menu.find('.menu-body-links').append($link);
        }
    }

    $link      = RwMasterController.generateLink($menu, 'Settings',    'theme/images/icons/128/preferences.png', 'account/preferences'); $menu.find('.menu-body-links').append($link);
    $link      = RwMasterController.generateLink($menu, 'Logoff',      'theme/images/icons/128/logoff.png',      'account/logoff');      $menu.find('.menu-body-links').append($link);
};
//----------------------------------------------------------------------------------------------
RwMasterController.generateLink = function($menu, caption, imagesrc, nav) {
    var html, $link;

    html = [];
    html.push('<div class="menu-body-link">');
        html.push('<div class="menu-body-link-icon"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + imagesrc + '" class="linkicon" /></div>');
        html.push('<div class="menu-body-link-caption">' + caption + '</div>');
    html.push('</div>');
    $link = jQuery(html.join(''));

    $link.on('click', function(e) {
        application.navigate(nav);
        //$menu.removeClass('is-active');
    });

    return $link;
};
//----------------------------------------------------------------------------------------------
RwMasterController.generateLinkGroup = function(caption) {
    var html, $linkgroup;

    html = [];
    html.push('<div class="menu-body-linkgroup">');
        html.push('<div class="menu-body-linkgroupheader">' + caption + '</div>');
    html.push('</div>');
    $linkgroup = jQuery(html.join(''));

    return $linkgroup;
};
//----------------------------------------------------------------------------------------------
RwMasterController.setTitle = function(object) {
    var $title;

    $title = jQuery('<div class="title"></div>');
    $title.append(object);

    jQuery('#master-header-row3').empty().append($title)
};
//----------------------------------------------------------------------------------------------
RwMasterController.setModuleCaption = function($object) {
    jQuery('#master-header-caption').empty().append($object);
}
//----------------------------------------------------------------------------------------------

