//----------------------------------------------------------------------------------------------
var RwMasterController = {};
//----------------------------------------------------------------------------------------------
RwMasterController.getMasterView = function(viewModel, properties) {
    var combinedViewModel, $view, $footerView, $headerView, applicationtheme;
    combinedViewModel = jQuery.extend({
        
    }, viewModel);

    applicationtheme = sessionStorage.getItem('applicationtheme');
    if (applicationtheme == 'theme-classic') {
        $headerView = RwMasterController.getHeaderClassic();
    } else {
        $headerView = RwMasterController.getHeaderView();
    }
    //$footerView = RwMasterController.getFooterView();

    $view = jQuery(Mustache.render(jQuery('#tmpl-controls-Master').html(), combinedViewModel));
    $view.find('#master-header').append($headerView);
    //$view.find('#master-footer').append($footerView);

    jQuery('html').addClass(applicationtheme);

    return $view;
};
//----------------------------------------------------------------------------------------------
RwMasterController.getHeaderClassic = function(viewModel, properties) {
    var combinedViewModel, $view, $headerRibbon, $userControl, $tabpageFile, $tabpageSettings, $tabpageReports, $tabpageUtilities, $tabpageHelp, $fwcontrols;
    combinedViewModel = jQuery.extend({

    }, viewModel);
    $view = jQuery(Mustache.render(jQuery('#tmpl-controls-Header').html(), combinedViewModel));
    
    $headerRibbon       = $view.find('#headerRibbon');
    $userControl        = $headerRibbon.find('.usercontrol');
    RwMasterController.getUserControl($userControl);
    
    var nodeSystem, nodeApplication, baseiconurl, $tabpage, ribbonItem, dropDownMenuItems, caption;
    nodeSystem = FwApplicationTree.getMyTree();
    for (var appno = 0; appno < nodeSystem.children.length; appno++) {
        if (nodeSystem.children[appno].id === '0A5F2584-D239-480F-8312-7C2B552A30BA') {
            nodeApplication = nodeSystem.children[appno];
        }
    }
    if (nodeApplication === null) {
        sessionStorage.clear();
        window.location.reload(true);
    }
    baseiconurl = 'theme/images/icons/home/';
    for (var lv1childno = 0; lv1childno < nodeApplication.children.length; lv1childno++) {
        var nodeLv1MenuItem = nodeApplication.children[lv1childno];
        if (nodeLv1MenuItem.properties.visible === 'T') {
            switch(nodeLv1MenuItem.properties.nodetype) {
                case 'Lv1ModuleMenu':
                    $tabpage = FwRibbon.addTab($headerRibbon, nodeLv1MenuItem.properties.caption)
                    for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                        var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                        if (nodeLv2MenuItem.properties.visible === 'T') {
                            switch(nodeLv2MenuItem.properties.nodetype) {
                                case 'Lv2ModuleMenu':
                                    dropDownMenuItems = [];
                                    for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                        var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                        if (nodeLv3MenuItem.properties.visible === 'T') {
                                            dropDownMenuItems.push({id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl});
                                        }
                                    }
                                    caption = (typeof nodeLv2MenuItem.properties.htmlcaption === 'string') ? nodeLv2MenuItem.properties.htmlcaption : nodeLv2MenuItem.properties.caption;
                                    ribbonItem = FwRibbon.generateDropDownModuleBtn(nodeLv2MenuItem.id, caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                    $tabpage.append(ribbonItem);
                                    break;
                                case 'Module':
                                    caption = (typeof nodeLv2MenuItem.properties.htmlcaption === 'string') ? nodeLv2MenuItem.properties.htmlcaption : nodeLv2MenuItem.properties.caption;
                                    ribbonItem = FwRibbon.generateStandardModuleBtn(nodeLv2MenuItem.id, caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                    $tabpage.append(ribbonItem);
                                    break;
                                case 'Report':
                                    caption = (typeof nodeLv2MenuItem.properties.htmlcaption === 'string') ? nodeLv2MenuItem.properties.htmlcaption : nodeLv2MenuItem.properties.caption;
                                    ribbonItem = FwRibbon.generateStandardModuleBtn(nodeLv2MenuItem.id, caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                    $tabpage.append(ribbonItem);
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    $fwcontrols = $view.find('.fwcontrol');
    FwControl.init($fwcontrols);
    FwControl.renderRuntimeHtml($fwcontrols);

    $view
        .on('click', '.dashboard', function() {
            try {
                program.navigate('home');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    return $view;
};
//----------------------------------------------------------------------------------------------
RwMasterController.getUserControl = function($userControl) {
    var $user, $logoff, $notification;

    $user = jQuery('<div id="username" class="item">' + FwFunc.fixCaps(sessionStorage.getItem('userType')) + ': ' + FwFunc.fixCaps(sessionStorage.getItem('fullname')) + '</div>');
    $userControl.append($user);

    RwMasterController.buildOfficeLocationClassic($userControl);

    //$notification = FwNotification.generateNotificationArea();
    //$userControl.append($notification);

    $usersettings = jQuery('<div id="usersettings" class="item"><div class="usersettingsicon"></div></div>');
    $usersettings.on('click', function() {
        try {
            program.getModule('module/usersettings');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });
    $userControl.append($usersettings);

    $logoff = jQuery('<div id="logoff" class="item">' + RwLanguages.translate('Logoff') + '</div>');
    $logoff.on('click', function() { 
        try {
            program.navigate('logoff');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });
    $userControl.append($logoff);
};
//----------------------------------------------------------------------------------------------
RwMasterController.buildOfficeLocationClassic = function ($userControl) {
    var $officelocation, userlocation;

    userlocation = JSON.parse(sessionStorage.getItem('location'));
    userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
    userdepartment = JSON.parse(sessionStorage.getItem('department'));

    $officelocation = jQuery('<div id="officelocation" class="item"><div class="caption">Office Location:</div><div class="value"></div></div>');
    $userControl.append($officelocation);

    $officelocation.find('.value').html(userlocation.location);
    $officelocation.css('background-color', userlocation.locationcolor);

    $officelocation.on('click', function() {
        var $confirmation, $ok, $cancel, html = [];
        $confirmation = FwConfirmation.renderConfirmation('Select an Office Location', '');
        $select       = FwConfirmation.addButton($confirmation, 'Select', false);
        $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="Location" data-validationname="OfficeLocationValidation"></div>');
        html.push('</div>');
        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-validationname="WarehouseValidation" data-boundfields="Location"></div>');
        html.push('</div>');
        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="Department" data-validationname="DepartmentValidation"></div>');
        html.push('</div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $confirmation.find('div[data-datafield="Location"] input.fwformfield-text').val(userlocation.location);
        $confirmation.find('div[data-datafield="Location"] input.fwformfield-value').val(userlocation.locationid);
        $confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-text').val(userwarehouse.warehouse);
        $confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-value').val(userwarehouse.warehouseid);
        $confirmation.find('div[data-datafield="Department"] input.fwformfield-text').val(userdepartment.department);
        $confirmation.find('div[data-datafield="Department"] input.fwformfield-value').val(userdepartment.departmentid);

        $select.on('click', function() {
            var valid = true, request, location, warehouse;
            location  = $confirmation.find('div[data-datafield="Location"] .fwformfield-value').val();
            warehouse = $confirmation.find('div[data-datafield="Warehouse"] .fwformfield-value').val();
            department = $confirmation.find('div[data-datafield="Department"] .fwformfield-value').val();
            if (location == '') {
                $confirmation.find('div[data-datafield="Location"]').addClass('error');
                valid = false;
            }
            if (warehouse == '') {
                $confirmation.find('div[data-datafield="Warehouse"]').addClass('error');
                valid = false;
            }
                 if (department == '') {
                $confirmation.find('div[data-datafield="Department"]').addClass('error');
                valid = false;
            }
            if (valid) {
                request = {
                    location:  location,
                    warehouse: warehouse,
                    department: department
                };
                RwServices.session.updatelocation(request, function (response) {
                    //-- Updates session storage
                    sessionStorage.setItem('authToken', response.authToken);
                    sessionStorage.setItem('location', JSON.stringify(response.location));
                    sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                    sessionStorage.setItem('department', JSON.stringify(response.department));
                    sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                    $officelocation.find('.value').html(response.location.location);
                    $officelocation.css('background-color', response.location.locationcolor);
                    FwConfirmation.destroyConfirmation($confirmation);
                    program.navigate('home');
                });
            }
        });
    });
};
//----------------------------------------------------------------------------------------------
RwMasterController.getFooterView = function(viewModel, properties) {
    var combinedViewModel, $view;
    combinedViewModel = jQuery.extend({
        valueYear:    new Date().getFullYear(),
        valueVersion: applicationConfig.version
    }, viewModel);
    $view = jQuery(Mustache.render(jQuery('#tmpl-controls-Footer').html(), combinedViewModel));

    $view
        .on('click', '#dbworkslink', function() {
            try {
                window.location.href = 'http://www.dbworks.com';
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    return $view;
};
//----------------------------------------------------------------------------------------------
RwMasterController.getHeaderView = function() {
    var $view;

    $view = jQuery('<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>');

    FwControl.renderRuntimeControls($view);

    $view.find('.logo').append('<div class="bgothm">RentalWorks</div>');

    var nodeApplications, nodeApplication=null, baseiconurl, $menu, ribbonItem, dropDownMenuItems, caption;
    nodeApplications = FwApplicationTree.getMyTree();
    for (var appno = 0; appno < nodeApplications.children.length; appno++) {
        if (nodeApplications.children[appno].id === '0A5F2584-D239-480F-8312-7C2B552A30BA') {
            nodeApplication = nodeApplications.children[appno];
        }
    }
    if (nodeApplication === null) {
        sessionStorage.clear();
        window.location.reload(true);
    }
    baseiconurl = 'theme/images/icons/home/';
    for (var lv1childno = 0; lv1childno < nodeApplication.children.length; lv1childno++) {
        var nodeLv1MenuItem = nodeApplication.children[lv1childno];
        if (nodeLv1MenuItem.properties.visible === 'T') {
            switch(nodeLv1MenuItem.properties.nodetype) {
                case 'Lv1ModuleMenu':
                    $menu = FwFileMenu.addMenu($view, nodeLv1MenuItem.properties.caption)
                    for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                        var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                        if (nodeLv2MenuItem.properties.visible === 'T') {
                            switch(nodeLv2MenuItem.properties.nodetype) {
                                case 'Lv2ModuleMenu':
                                    dropDownMenuItems = [];
                                    for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                        var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                        if (nodeLv3MenuItem.properties.visible === 'T') {
                                            dropDownMenuItems.push({id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl});
                                        }
                                    }
                                    FwFileMenu.generateDropDownModuleBtn($menu, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                    break;
                                case 'Module':
                                    FwFileMenu.generateStandardModuleBtn($menu, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                    break;
                                case 'Report':
                                    FwFileMenu.generateStandardModuleBtn($menu, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    FwFileMenu.renderUserControl($view, sessionStorage.getItem('userType'), sessionStorage.getItem('fullname'));
    $view.find('.user-controls .copyright').html('Database Works © ' + new Date().getFullYear());
    $view.find('.user-controls .version').html('RentalWorks v' + applicationConfig.version);
    RwMasterController.buildOfficeLocation($view);
    RwMasterController.buildDashboard($view);
    $view
        .on('click', '.user-controls .usersettings', function() {
            try { program.getModule('module/usersettings'); } catch (ex) { FwFunc.showError(ex); }
        })
        .on('click', '.user-controls .logoff', function() {
            try { program.navigate('logoff'); } catch (ex) { FwFunc.showError(ex); }
        })
        .on('click', '.bgothm', function () {
            try { program.navigate('home'); } catch (ex) { FwFunc.showError(ex); }
        })
    ;

    return $view;
};
//----------------------------------------------------------------------------------------------
RwMasterController.buildOfficeLocation = function($view) {
    var $officelocation, userlocation, $userControl;

    userlocation = JSON.parse(sessionStorage.getItem('location'));
    userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
    userdepartment = JSON.parse(sessionStorage.getItem('department'));
    $userControl = $view.find('.user-controls');

    $officelocation = jQuery('<div class="officelocation"><div class="locationcolor" style="background-color:' + userlocation.locationcolor + '"></div><div class="value">' + userlocation.location + '</div></div>');
    $userControl.prepend($officelocation);

    $officelocation = jQuery('<div class="item officelocationbtn">Office Location</div>');
    $userControl.find('.user-dropdown').prepend($officelocation);
    $officelocation.on('click', function () {
        var $confirmation, $ok, $cancel, html = [];
        $confirmation = FwConfirmation.renderConfirmation('Select an Office Location', '');
        $select = FwConfirmation.addButton($confirmation, 'Select', false);
        $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);

        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="Location" data-validationname="OfficeLocationValidation"></div>');
        html.push('</div>');
        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-validationname="WarehouseValidation" data-boundfields="Location"></div>');
        html.push('</div>');
        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="Department" data-validationname="DepartmentValidation" data-boundfields="Location"></div>');
        html.push('</div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $confirmation.find('div[data-datafield="Location"] input.fwformfield-text').val(userlocation.location);
        $confirmation.find('div[data-datafield="Location"] input.fwformfield-value').val(userlocation.locationid);
        $confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-text').val(userwarehouse.warehouse);
        $confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-value').val(userwarehouse.warehouseid);
        $confirmation.find('div[data-datafield="Department"] input.fwformfield-text').val(userdepartment.department);
        $confirmation.find('div[data-datafield="Department"] input.fwformfield-value').val(userdepartment.departmentid);

        $select.on('click', function () {
            var valid = true, request, location, warehouse;
            location = $confirmation.find('div[data-datafield="Location"] .fwformfield-value').val();
            warehouse = $confirmation.find('div[data-datafield="Warehouse"] .fwformfield-value').val();
            department = $confirmation.find('div[data-datafield="Department"] .fwformfield-value').val();
            if (location == '') {
                $confirmation.find('div[data-datafield="Location"]').addClass('error');
                valid = false;
            }
            if (warehouse == '') {
                $confirmation.find('div[data-datafield="Warehouse"]').addClass('error');
                valid = false;
            }
            if (department == '') {
                $confirmation.find('div[data-datafield="Department"]').addClass('error');
                valid = false;
            }
            if (valid) {
                request = {
                    location: location,
                    warehouse: warehouse,
                    department: department
                };
                RwServices.session.updatelocation(request, function (response) {
                    sessionStorage.setItem('authToken', response.authToken);
                    sessionStorage.setItem('location', JSON.stringify(response.location));
                    sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                    sessionStorage.setItem('department', JSON.stringify(response.department));
                    sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                    FwConfirmation.destroyConfirmation($confirmation);
                    program.navigate('home');
                });
            }
        });
    });
};
//----------------------------------------------------------------------------------------------
RwMasterController.buildDashboard = function ($view) {
    var $dashboard, $userControl;
    
    $dashboard = jQuery('<i class="material-icons dashboard">insert_chart</i>');
    $userControl = $view.find('.user-controls');

    $userControl.prepend($dashboard)

    $dashboard.on('click', function () {
        try { program.navigate('home'); } catch (ex) { FwFunc.showError(ex); }
    })
}
//----------------------------------------------------------------------------------------------
