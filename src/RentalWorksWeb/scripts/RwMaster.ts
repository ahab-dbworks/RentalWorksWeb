class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    getUserControl($context: JQuery) {
        var $usercontrol = FwFileMenu.UserControl_render($context);

        this.buildDashboard($context);
        this.buildOfficeLocation($context);

        // Add SystemBarControl: User Name
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname')
        var $controlUserName = jQuery(`<div title="User Type: ${usertype}">${username}</div>`);
        FwFileMenu.UserControl_addSystemBarControl('username', $controlUserName, $usercontrol);

        // Add DropDownMenuItem: User Settings
        var $miUserSettings = jQuery(`<div>${RwLanguages.translate('User Settings')}</div>`);
        FwFileMenu.UserControl_addDropDownMenuItem('usersettings', $miUserSettings, $usercontrol);
        $miUserSettings.on('click', (event) => {
            try {
                program.getModule('module/usersettings');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        // Add DropDownMenuItem: Sign Out
        var $miSignOut = jQuery(`<div>${RwLanguages.translate('Sign Out')}</div>`);
        FwFileMenu.UserControl_addDropDownMenuItem('signout', $miSignOut, $usercontrol);
        $miSignOut.on('click', (event) => {
            try {
                program.navigate('logoff');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    buildOfficeLocationClassic($userControl: JQuery) {
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
                var $select       = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel       = FwConfirmation.addButton($confirmation, 'Cancel', true);

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

                $select.on('click', function() {
                    try {
                        var valid = true, request;
                        var location  = $confirmation.find('div[data-datafield="Location"] .fwformfield-value').val();
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
                                location:  location,
                                warehouse: warehouse,
                                department: department
                            };
                            RwServices.session.updatelocation(request, function (response) {
                                try {
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
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    buildOfficeLocation($usercontrol: JQuery<HTMLElement>) {
        var userlocation = JSON.parse(sessionStorage.getItem('location'));
        var userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        var userdepartment = JSON.parse(sessionStorage.getItem('department'));
        var userid = JSON.parse(sessionStorage.getItem('userid'));


        var html = [];
        html.push('<div class="officelocation">');
        html.push(`  <div class="locationcolor" style= "background-color:${userlocation.locationcolor}" > </div>`);
        html.push(`  <div class="value">${userlocation.location}</div>`);
        html.push('</div>');
        var $officelocation = jQuery(html.join('\n'));
        FwFileMenu.UserControl_addSystemBarControl('officelocation', $officelocation, $usercontrol);

        //$officelocation = jQuery('<div class="item officelocationbtn">Office Location</div>');
        //$userControl.find('.user-dropdown').prepend($officelocation);
        $officelocation.on('click', function () {
            try {
                var userlocation = JSON.parse(sessionStorage.getItem('location'));
                var userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                var userdepartment = JSON.parse(sessionStorage.getItem('department'));
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
                                department: department,
                                userid: userid.webusersid
                            };
                            RwServices.session.updatelocation(request, function (response) {
                                sessionStorage.setItem('authToken', response.authToken);
                                sessionStorage.setItem('location', JSON.stringify(response.location));
                                sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                                sessionStorage.setItem('department', JSON.stringify(response.department));
                                sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                                FwConfirmation.destroyConfirmation($confirmation);

                                program.navigate('home');
                                $usercontrol.find('.officelocation .locationcolor').css('background-color', response.location.locationcolor);
                                $usercontrol.find('.officelocation .value').text(response.location.location);
                            });
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    buildDashboard($usercontrol: JQuery<HTMLElement>) {
        var $dashboard, $userControl;
    
        $dashboard = jQuery('<i class="material-icons dashboard">insert_chart</i>');

        $dashboard.on('click', function () {
            try { program.navigate('home'); } catch (ex) { FwFunc.showError(ex); }
        });

        FwFileMenu.UserControl_addSystemBarControl('dashboard', $dashboard, $usercontrol)
    }
    //----------------------------------------------------------------------------------------------
}
var masterController: RwMaster = new RwMaster();
