var RwHome = {};
//----------------------------------------------------------------------------------------------
RwHome.getHomeScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $menuObject, nodeModule;
    combinedViewModel = jQuery.extend({
        captionPageTitle:         '<div class="apptitle bgothm center" style="padding-top:10px 0;font-size:18px;color:#f2f2f2;">RentalWorks QuikScan</div>',
        captionPageSubTitle:      ''
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-home').html(), combinedViewModel);
    screen                         = {};
    screen.$view                   = FwMobileMasterController.getMasterView(combinedViewModel);
    screen.viewModel               = viewModel;
    screen.properties              = properties;

    //var nodeApplications = FwApplicationTree.getMyTree();
    //var nodeApplication = null;
    //for (var appno = 0; appno < nodeApplications.children.length; appno++) {
    //    if (nodeApplications.children[appno].id === '8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A') {
    //        nodeApplication = nodeApplications.children[appno];
    //    }
    //}
    //if (nodeApplication === null) {
    //    throw 'Unable to find RentalWorks QuikScan node in group tree. {4537336F-8EDF-4E6B-8CCF-434D72C7D749}';
    //}
    //var nodeHome = nodeApplication.children[0];
    const applicationOptions = program.getApplicationOptions();
    const secNodeMobile = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Mobile');
    var nodeMobile = Constants.Modules.Mobile;
    for (var moduleKey in nodeMobile.children) {
        nodeModule = nodeMobile.children[moduleKey];
        if (nodeModule.visible) {
            switch (nodeModule.nodetype) {
                case 'Module':
                    let hasusertype = true;
                    if (typeof nodeModule.usertype === 'string') {
                        hasusertype = (jQuery.inArray(sessionStorage.userType, nodeModule.usertype.split(',')) !== -1);
                    }
                    let hasApplicationOptions = true;
                    if (typeof nodeModule.applicationoptions === 'string') {
                        const moduleApplicationOptions = nodeModule.applicationoptions.split(',');
                        for (let optionNo = 0; optionNo < moduleApplicationOptions.length; optionNo++) {
                            let option = moduleApplicationOptions[optionNo];
                            if (applicationOptions.hasOwnProperty(option)) {
                                hasApplicationOptions &= option.enabled;
                            }
                        }
                    }
                    if (hasusertype && hasApplicationOptions) {
                        const secNodeModule = FwApplicationTree.getNodeById(secNodeMobile, nodeModule.id);
                        if (secNodeModule !== null && secNodeModule.properties.visible === 'T') {
                            const caption = (typeof nodeModule.htmlcaption === 'string' && nodeModule.htmlcaption.length > 0) ? RwLanguages.translate(nodeModule.htmlcaption) : RwLanguages.translate(nodeModule.caption);
                            $menuObject = RwHome.generateIcon(caption, nodeModule.nav, nodeModule.iconurl, nodeModule.id);
                            screen.$view.find('.fwmenu').append($menuObject);
                        }
                    }
                    break;
            }
        }
    }

    $menuObject = RwHome.generateIcon('Settings', 'account/preferences', 'theme/images/icons/128/preferences.png', '5993B190-F084-4658-AEC2-1D467E26473F');
    screen.$view.find('.fwmenu').append($menuObject);

    $menuObject = RwHome.generateIcon('Logoff', 'logoff', 'theme/images/icons/128/logoff.png', '3F515109-1B5A-41A7-9B86-5ABF2BC12604');
    screen.$view.find('.fwmenu').append($menuObject);

    screen.load = function () {
        RwRFID.init();
    };
    screen.unload = function () {
        window.removeEventListener('resize', screen.onWindowResize, false);
    };
    return screen;
};
//----------------------------------------------------------------------------------------------
RwHome.generateIcon = function(name, nav, icon, id) {
    var html, $menuObject;

    html = [];
    html.push('<div class="fwmenu-item" data-securityid="' + id + '">');
    html.push('  <div class="fwmenu-item-inner">');
    html.push('    <div class="fwmenu-item-icon"><img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + icon + '"/></div>');
    html.push('    <div class="fwmenu-item-description">' + name + '</div>');
    html.push('  </div>');
    html.push('</div>');

    $menuObject = jQuery(html.join(''));

    $menuObject.on('click', function() {
        try {
            program.navigate(nav);
        } catch(ex) {
            FwFunc.showError(ex);
        }
    });

    return $menuObject;
};
//----------------------------------------------------------------------------------------------