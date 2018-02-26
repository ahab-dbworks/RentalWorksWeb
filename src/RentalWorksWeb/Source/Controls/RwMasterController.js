var RwMaster = (function () {
    function RwMaster() {
    }
    RwMaster.prototype.getMasterView = function (viewModel, properties) {
        var combinedViewModel, $view, $footerView, $headerView, applicationtheme;
        combinedViewModel = jQuery.extend({}, viewModel);
        applicationtheme = sessionStorage.getItem('applicationtheme');
        if (applicationtheme == 'theme-classic') {
            $headerView = RwMasterController.getHeaderClassic(combinedViewModel, properties);
        }
        else {
            $headerView = RwMasterController.getHeaderView();
        }
        $view = jQuery(Mustache.render(jQuery('#tmpl-controls-Master').html(), combinedViewModel));
        $view.find('#master-header').append($headerView);
        jQuery('html').addClass(applicationtheme);
        return $view;
    };
    RwMaster.prototype.getHeaderClassic = function (viewModel, properties) {
        var combinedViewModel, $view, $headerRibbon, $userControl, $tabpageFile, $tabpageSettings, $tabpageReports, $tabpageUtilities, $tabpageHelp, $fwcontrols;
        combinedViewModel = jQuery.extend({}, viewModel);
        $view = jQuery(Mustache.render(jQuery('#tmpl-controls-Header').html(), combinedViewModel));
        $headerRibbon = $view.find('#headerRibbon');
        $userControl = $headerRibbon.find('.usercontrol');
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
                switch (nodeLv1MenuItem.properties.nodetype) {
                    case 'Lv1ModuleMenu':
                        $tabpage = FwRibbon.addTab($headerRibbon, nodeLv1MenuItem.properties.caption);
                        for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                            var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                            if (nodeLv2MenuItem.properties.visible === 'T') {
                                switch (nodeLv2MenuItem.properties.nodetype) {
                                    case 'Lv2ModuleMenu':
                                        dropDownMenuItems = [];
                                        for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                            var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                            if (nodeLv3MenuItem.properties.visible === 'T') {
                                                dropDownMenuItems.push({ id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl });
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
            .on('click', '.dashboard', function () {
            try {
                program.navigate('home');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        return $view;
    };
    RwMaster.prototype.getUserControl = function ($context) {
        var $usercontrol = FwFileMenu.UserControl_render($context);
        this.buildDashboard($context);
        this.buildOfficeLocation($context);
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname');
        var $controlUserName = jQuery("<div title=\"User Type: " + usertype + "\">" + username + "</div>");
        FwFileMenu.UserControl_addSystemBarControl('username', $controlUserName, $usercontrol);
        var $miUserSettings = jQuery("<div>" + RwLanguages.translate('User Settings') + "</div>");
        FwFileMenu.UserControl_addDropDownMenuItem('usersettings', $miUserSettings, $usercontrol);
        $miUserSettings.on('click', function (event) {
            try {
                program.getModule('module/usersettings');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        var $miSignOut = jQuery("<div>" + RwLanguages.translate('Sign Out') + "</div>");
        FwFileMenu.UserControl_addDropDownMenuItem('signout', $miSignOut, $usercontrol);
        $miSignOut.on('click', function (event) {
            try {
                program.navigate('logoff');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    RwMaster.prototype.buildOfficeLocationClassic = function ($userControl) {
        var userlocation = JSON.parse(sessionStorage.getItem('location'));
        var userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var userdepartment = JSON.parse(sessionStorage.getItem('department'));
        var $officelocation = jQuery('<div id="officelocation" class="item"><div class="caption">Office Location:</div><div class="value"></div></div>');
        $userControl.append($officelocation);
        $officelocation.find('.value').html(userlocation.location);
        $officelocation.css('background-color', userlocation.locationcolor);
        $officelocation.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Select an Office Location', '');
                var $select = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                var html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="Location" data-validationname="OfficeLocationValidation"></div>');
                html.push('  </div>');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-validationname="WarehouseValidation" data-boundfields="Location"></div>');
                html.push('  </div>');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="Department" data-validationname="DepartmentValidation"></div>');
                html.push('  </div>');
                html.push('</div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $confirmation.find('div[data-datafield="Location"] input.fwformfield-text').val(userlocation.location);
                $confirmation.find('div[data-datafield="Location"] input.fwformfield-value').val(userlocation.locationid);
                $confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-text').val(userwarehouse.warehouse);
                $confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-value').val(userwarehouse.warehouseid);
                $confirmation.find('div[data-datafield="Department"] input.fwformfield-text').val(userdepartment.department);
                $confirmation.find('div[data-datafield="Department"] input.fwformfield-value').val(userdepartment.departmentid);
                $select.on('click', function () {
                    try {
                        var valid = true, request;
                        var location = $confirmation.find('div[data-datafield="Location"] .fwformfield-value').val();
                        var warehouse = $confirmation.find('div[data-datafield="Warehouse"] .fwformfield-value').val();
                        var department = $confirmation.find('div[data-datafield="Department"] .fwformfield-value').val();
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
                                try {
                                    sessionStorage.setItem('authToken', response.authToken);
                                    sessionStorage.setItem('location', JSON.stringify(response.location));
                                    sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                                    sessionStorage.setItem('department', JSON.stringify(response.department));
                                    sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                                    $officelocation.find('.value').html(response.location.location);
                                    $officelocation.css('background-color', response.location.locationcolor);
                                    FwConfirmation.destroyConfirmation($confirmation);
                                    program.navigate('home');
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    RwMaster.prototype.getFooterView = function (viewModel, properties) {
        var combinedViewModel, $view;
        combinedViewModel = jQuery.extend({
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        }, viewModel);
        $view = jQuery(Mustache.render(jQuery('#tmpl-controls-Footer').html(), combinedViewModel));
        $view
            .on('click', '#dbworkslink', function () {
            try {
                window.location.href = 'http://www.dbworks.com';
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        return $view;
    };
    RwMaster.prototype.getHeaderView = function () {
        var $view;
        $view = jQuery('<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>');
        FwControl.renderRuntimeControls($view);
        $view.find('.logo').append('<div class="bgothm">RentalWorks</div>');
        var nodeApplications, nodeApplication = null, baseiconurl, $menu, ribbonItem, dropDownMenuItems, caption;
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
                switch (nodeLv1MenuItem.properties.nodetype) {
                    case 'Lv1ModuleMenu':
                        $menu = FwFileMenu.addMenu($view, nodeLv1MenuItem.properties.caption);
                        for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                            var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                            if (nodeLv2MenuItem.properties.visible === 'T') {
                                switch (nodeLv2MenuItem.properties.nodetype) {
                                    case 'Lv2ModuleMenu':
                                        dropDownMenuItems = [];
                                        for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                            var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                            if (nodeLv3MenuItem.properties.visible === 'T') {
                                                dropDownMenuItems.push({ id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl });
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
        this.getUserControl($view);
        $view
            .on('click', '.bgothm', function () {
            try {
                program.navigate('home');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        return $view;
    };
    RwMaster.prototype.buildOfficeLocation = function ($usercontrol) {
        var userlocation = JSON.parse(sessionStorage.getItem('location'));
        var userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var userdepartment = JSON.parse(sessionStorage.getItem('department'));
        var html = [];
        html.push('<div class="officelocation">');
        html.push("  <div class=\"locationcolor\" style= \"background-color:" + userlocation.locationcolor + "\" > </div>");
        html.push("  <div class=\"value\">" + userlocation.location + "</div>");
        html.push('</div>');
        var $officelocation = jQuery(html.join('\n'));
        FwFileMenu.UserControl_addSystemBarControl('officelocation', $officelocation, $usercontrol);
        $officelocation.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Select an Office Location', '');
                var $select = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                var html = [];
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
                    try {
                        var valid = true;
                        var location = $confirmation.find('div[data-datafield="Location"] .fwformfield-value').val();
                        var warehouse = $confirmation.find('div[data-datafield="Warehouse"] .fwformfield-value').val();
                        var department = $confirmation.find('div[data-datafield="Department"] .fwformfield-value').val();
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
                            var request = {
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
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    RwMaster.prototype.buildDashboard = function ($usercontrol) {
        var $dashboard, $userControl;
        $dashboard = jQuery('<i class="material-icons dashboard">insert_chart</i>');
        $dashboard.on('click', function () {
            try {
                program.navigate('home');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwFileMenu.UserControl_addSystemBarControl('dashboard', $dashboard, $usercontrol);
    };
    return RwMaster;
}());
var RwMasterController = new RwMaster();
//# sourceMappingURL=RwMasterController.js.map