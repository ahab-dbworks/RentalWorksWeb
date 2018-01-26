//----------------------------------------------------------------------------------------------
var FwSettings = {};
//----------------------------------------------------------------------------------------------
FwSettings.init = function () {

};
//----------------------------------------------------------------------------------------------
FwSettings.renderRuntimeHtml = function ($control) {
    var html = [];

    html.push('<div class="fwsettingsheader">');
    html.push('  <div class="input-group pull-right">');
    html.push('    <input type="text" id="settingsSearch" class="form-control" placeholder="Search...">');
    html.push('    <span class="input-group-addon">');
    html.push('      <i class="material-icons">search</i>');
    html.push('    </span>');
    html.push('  </div>');
    html.push('</div>');      
    html.push('<div class="well"></div>');
    //< div class="input-group pull-right" > <input type="text" class="form-control" placeholder="Settings..."><span class="input-group-addon"><i class="material-icons">search</i></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    $control.html(html.join(''));
};
//----------------------------------------------------------------------------------------------
FwSettings.saveForm = function (module, $form, closetab, navigationpath, $control, browseKeys, rowId, moduleName) {
    var $tabpage, fields, ids, mode, isValid, $tab, request, controllername, controller;
    mode           = $form.attr('data-mode');
    $tabpage       = $form.parent();
    $tab           = jQuery('#' + $tabpage.attr('data-tabid'));
    isValid        = FwModule.validateForm($form);
    controllername = $form.attr('data-controller');
    controller     = window[controllername];

    if (isValid) {
        ids    = FwModule.getFormUniqueIds($form);
        fields = FwModule.getFormFields($form, false);
        if (typeof controller.apiurl !== 'undefined') {
            request = FwModule.getFormModel($form, false);
        } else {
            request = {
                module: module,
                mode:   mode,
                ids:    ids,
                fields: fields
            };
        }
        FwServices.module.method(request, module, 'Save', $form, function (response) {
            var $formfields, issubmodule, $browse;

            if (typeof controller.apiurl !== 'undefined' && browseKeys.length !== 0) {
                if (!closetab) {
                    var $body = $control.find('#' + moduleName + '.panel-body');

                    var html = [], $moduleRows;
                    html.push('<div class="panel-record">')
                    html.push('  <div class="panel panel-info container-fluid">');
                    html.push('    <div class="row-heading">');
                    for (var j = 0; j < browseKeys.length; j++) {
                        if (browseKeys.length === 1) {
                            html.push('      <label style="width:25%">' + browseKeys[j] + '</label>');
                            html.push('      <label style="width:25%">' + response[browseKeys[j]] + '</label>');
                            html.push('      <label style="width:25%"></label>');
                            html.push('      <label style="width:25%"></label>');
                        }
                        if (browseKeys.length === 2) {
                            html.push('      <label style="width:25%">' + browseKeys[j] + '</label>');
                            html.push('      <label style="width:25%">' + response[browseKeys[j]] + '</label>');
                        }
                        if (browseKeys.length === 3) {
                            html.push('      <label style="width:15%">' + browseKeys[j] + '</label>');
                            html.push('      <label style="width:15%">' + response[browseKeys[j]] + '</label>');
                        }
                    }
                    html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>');
                    html.push('    </div>');
                    html.push('  </div>');
                    html.push('  <div class="panel-body" style="display:none;" id="' + response[rowId] + '"></div>');
                    html.push('</div>');
                    $moduleRows = jQuery(html.join(''));
                    $moduleRows.data('recorddata', response);
                    //console.log($body);
                    $body.append($moduleRows);                    
                    //Refresh the browse window on saving a record.
                    $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
                    if ($browse.length > 0) {
                        FwBrowse.databind($browse);
                    }

                    //If form is submodule
                    //issubmodule = $form.parent().hasClass('submodule');
                    //if (!issubmodule) {
                    //    jQuery('#' + $form.parent().attr('data-tabid')).find('.caption').html(response.tabname);
                    //} else {
                    //    jQuery('#' + $form.parent().attr('data-tabid')).find('.form-caption').html(response.tabname);
                    //}

                    jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
                    if ($form.attr('data-mode') === 'NEW') {
                        $form.attr('data-mode', 'EDIT');
                        $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                    } else {
                        $formfields = $form.data('fields');
                    }
                    FwFormField.loadForm($formfields, response);
                    $form.attr('data-modified', false);
                    if (typeof controller['afterLoad'] === 'function') {
                        controller['afterLoad']($form);
                    }
                    if (typeof controller['afterSave'] === 'function') {
                        controller['afterSave']($form);
                    }
                } else if (closetab) {
                    FwModule.closeFormTab($tab);
                    if ((typeof navigationpath !== 'undefined') && (navigationpath !== '')) {
                        program.getModule(navigationpath);
                    }
                }
                FwNotification.renderNotification('SUCCESS', 'Record saved.');
            } else if (response.saved === true) {
                //console.log('hi');
                if (!closetab) {
                    //Refresh the browse window on saving a record.
                    $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
                    if ($browse.length > 0) {
                        FwBrowse.databind($browse);
                    }

                    //If form is submodule
                    issubmodule = $form.parent().hasClass('submodule');
                    if (!issubmodule) {
                        jQuery('#' + $form.parent().attr('data-tabid')).find('.caption').html(response.tabname);
                    } else {
                        jQuery('#' + $form.parent().attr('data-tabid')).find('.form-caption').html(response.tabname);
                    }

                    jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
                    if ($form.attr('data-mode') === 'NEW') {
                        $form.attr('data-mode', 'EDIT');
                        $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                    } else {
                        $formfields = $form.data('fields');
                    }
                    FwFormField.loadForm($formfields, response.tables);
                    $form.attr('data-modified', false);
                    if (typeof controller['afterLoad'] === 'function') {
                        controller['afterLoad']($form);
                    }
                    if (typeof controller['afterSave'] === 'function') {
                        controller['afterSave']($form);
                    }
                } else if (closetab) {
                    FwModule.closeFormTab($tab);
                    if ((typeof navigationpath !== 'undefined') && (navigationpath !== '')) {
                        program.getModule(navigationpath);
                    }
                }
                FwNotification.renderNotification('SUCCESS', 'Record saved.');
            } else if (response.saved == false) {
                if ((typeof response.message !== 'undefined') && (response.message != '')) {
                    FwNotification.renderNotification('ERROR', response.message);
                } else {
                    FwNotification.renderNotification('ERROR', 'There is an error on the form.');
                }
            }
        });
    }
};
//----------------------------------------------------------------------------------------------
FwSettings.getCaptions = function (screen) {
    var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
    var modules = FwApplicationTree.getChildrenByType(node, 'Module');
    for (var i = 0; i < modules.length; i++) {
        var moduleName = modules[i].properties.controller.slice(0, -10);
        var $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
        var $fwformfields = $form.find('.fwformfield[data-caption]');
        for (var j = 0; j < $fwformfields.length; j++) {
            var $field = $fwformfields.eq(j);
            var caption = $field.attr('data-caption').toUpperCase();
            if (typeof screen.moduleCaptions[caption] === 'undefined') {
                screen.moduleCaptions[caption] = {};
            }
            if (typeof screen.moduleCaptions[caption][moduleName] === 'undefined') {
                screen.moduleCaptions[caption][moduleName] = [];
            }
            screen.moduleCaptions[caption][moduleName].push($field);
        }
    }
}
//---------------------------------------------------------------------------------------------- 
FwSettings.getRows = function ($body, $control, apiurl, $modulecontainer, moduleName) {
    FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, function onSuccess(response) {
        var $settings, keys, browseKeys = [];
        $settings = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
        keys = $settings.find('.field');
        rowId = jQuery(keys[0]).attr('data-browsedatafield');
        for (var i = 1; i < keys.length; i++) {
            browseKeys.push(jQuery(keys[i]).attr('data-browsedatafield'));
        }
        for (var i = 0; i < response.length; i++) {
            var html = [], $moduleRows;
            html.push('<div class="panel-record">')
            html.push('  <div class="panel panel-info container-fluid">');
            html.push('    <div class="row-heading">');
            html.push('      <i class="material-icons record-selector">keyboard_arrow_down</i>')
            //html.push('<label>' + moduleName + '</label>')
            //html.push('<label>' + row[moduleName] + '</label>');
            for (var j = 0; j < browseKeys.length; j++) {
                if (browseKeys[j] === 'Inactive' || browseKeys[j] === 'Color') {
                    browseKeys.splice(j, 1);
                }
                if (browseKeys.length === 1) {
                    html.push('      <div style="width:100%;padding-left: inherit;">');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                    html.push('        </div>');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                    html.push('        </div>');
                    html.push('      </div>');
                }
                if (browseKeys.length === 2) {
                    html.push('      <div style="width:100%;padding-left: inherit;">');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                    html.push('        </div>');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                    html.push('        </div>');
                    html.push('      </div>');
                }
                if (browseKeys.length === 3) {
                    html.push('      <div style="width:100%;padding-left: inherit;">');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                    html.push('        </div>');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                    html.push('        </div>');
                    html.push('      </div>');
                }
                if (browseKeys.length === 4) {
                    html.push('      <div style="width:100%;padding-left: inherit;">');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                    html.push('        </div>');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                    html.push('        </div>');
                    html.push('      </div>');
                }
                if (browseKeys.length === 5) {
                    html.push('      <div style="width:100%;padding-left: inherit;">');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                    html.push('        </div>');
                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                    html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                    html.push('        </div>');
                    html.push('      </div>');
                }
            }
            html.push('    </div>');
            html.push('  </div>');
            html.push('  <div class="panel-body" style="display:none;" id="' + response[i][rowId] + '"></div>');
            html.push('</div>');
            $moduleRows = jQuery(html.join(''));
            $moduleRows.data('recorddata', response[i]);
            $body.append($moduleRows);
        }

        $control
            .on('click', '.row-heading', function (e) {
                var formKeys = [], formData = [], recordData, $rowBody, $form;
                e.stopPropagation();
                recordData = jQuery(this).parent().parent().data('recorddata');
                $rowBody = $control.find('#' + recordData[moduleName + 'Id'] + '.panel-body');
                $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                if ($rowBody.is(':empty')) {
                    $rowBody.append($form);
                    FwModule.openForm($form, 'EDIT');


                    for (var key in recordData) {
                        var value = recordData[key];
                        var $field = $form.find('[data-datafield="' + key + '"]');
                        var displayfield = $field.attr('data-displayfield');
                        if ($field.length > 0) {
                            if (typeof displayfield !== 'undefined' && typeof recordData[displayfield] !== 'undefined') {
                                var text = recordData[displayfield];
                                FwFormField.setValue($form, '[data-datafield="' + key + '"]', value, text);
                            } else {
                                FwFormField.setValue($form, '[data-datafield="' + key + '"]', value);
                            }
                        }
                    }
                }
            })
            .on('click', '.save', function (e) {
                e.stopPropagation();
                $form = jQuery(this).closest('.panel-record').find('.fwform');
                FwSettings.saveForm(window[moduleName + 'Controller'].Module, $form, false, '');
                $body.find('.new-row').hide();
                $body.empty();
                FwSettings.getRows($body, $control, apiurl, $control.find('#' + moduleName), moduleName);
            })
            ;

    }, null, $modulecontainer);
}
//---------------------------------------------------------------------------------------------- 
FwSettings.newRow = function ($body, $control, apiurl, $modulecontainer, moduleName, $modules) {
    var $form, rowId, newRowHtml = [];

    $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

    if ($body.is(':empty')) {
        FwSettings.getRows($body, $control, apiurl, $modulecontainer, moduleName);
    }

    if ($modules.find('.panel-collapse').css('display') === 'none') {
        $modules.find('.arrow-selector').html('keyboard_arrow_up');
        $modules.find('.panel-collapse').show("fast");
    }

    if ($body.find('.new-row').length === 0) {
        newRowHtml.push('<div class="new-row">');
        newRowHtml.push('  <div class="panel-record">');
        newRowHtml.push('    <div class="panel panel-info container-fluid">');
        newRowHtml.push('      <div class="new-row-heading">');
        newRowHtml.push('        <label style="width:100%">New Record</label>');
        newRowHtml.push('        <div class="pull-right save-new-row"><i class="material-icons">save</i>Save</div>');
        newRowHtml.push('      </div>');
        newRowHtml.push('    </div>');
        newRowHtml.push('  </div>');
        newRowHtml.push('</div>');

        $body.prepend($form);
        $body.prepend(jQuery(newRowHtml.join('')));

        FwModule.openForm($form, 'NEW');
        $form.find('.buttonbar').hide();
    }

    $body.on('click', '.save-new-row', function (e) {
        var $form, save;
        e.stopPropagation();
        $form = jQuery(this).closest('.panel-body').find('.fwform');
        save = $form.find('.btn')
        FwSettings.saveForm(window[moduleName + 'Controller'].Module, $form, false, '', $control, [], rowId, moduleName);
        $body.find('.new-row').hide();
        $body.empty();
        FwSettings.getRows($body, $control, apiurl, $control.find('#' + moduleName), moduleName);
    });
}
//---------------------------------------------------------------------------------------------- 
FwSettings.renderModuleHtml = function ($control, title, moduleName, color, description, menu) {
    var html = [], $settingsPageModules, $rowBody, $modulecontainer, apiurl, $body, $form, browseKeys = [], rowId;
    

    $modulecontainer = $control.find('#' + moduleName);
    apiurl = window[moduleName + 'Controller'].apiurl;
    $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

    html.push('<div class="panel-group" id="' + description+ '" >');
    if (color) {
        html.push('  <div class="panel panel-primary" style="border-color:' + color + '">');
        html.push('    <div data-toggle="collapse" data-target="' + description + '" href="' + description + '" class="panel-heading" style="background-color:' + color + '">');
    } else {
        html.push('  <div class="panel panel-primary">');
        html.push('    <div data-toggle="collapse" data-target="' + description + '" href="' + description + '" class="panel-heading">');
    }
    html.push('      <h4 class="panel-title">');
    html.push('        <a id="title" data-toggle="collapse">' + menu + ' - ' + title )
    html.push('          <i class="material-icons arrow-selector">keyboard_arrow_down</i>');
    html.push('        </a>');
    html.push('        <i class="material-icons heading-menu">more_vert</i>');
    html.push('        <div id="myDropdown" class="dropdown-content">')
    html.push('          <a class="new-row">New Item</a>');
    html.push('        </div>'); 
    html.push('      </h4>');
    html.push('      <small id="description" style="display:none;">' + description + '</small>');
    html.push('      <small>' + description + '</small>');
    html.push('    </div>');
    html.push('    <div class="panel-collapse collapse" style="display:none; "><div class="panel-body" id="' + description + '"></div></div>');
    html.push('  </div>');
    html.push('</div>');
    $settingsPageModules = jQuery(html.join(''));

    $control.find('.well').append($settingsPageModules);

    $settingsPageModules.on('click', '.btn', function (e) {
        $settingsPageModules.find('.heading-menu').next().css('display', 'none');
        $body = $control.find('#' + moduleName + '.panel-body');
        FwSettings.newRow($body, $control, apiurl, $modulecontainer, moduleName, $settingsPageModules);
    });

    $settingsPageModules.on('click', '.new-row', function (e) {
        e.stopPropagation();
        $settingsPageModules.find('.heading-menu').next().css('display', 'none');
        $body = $control.find('#' + moduleName + '.panel-body');
        FwSettings.newRow($body, $control, apiurl, $modulecontainer, moduleName, $settingsPageModules);
    });

    $settingsPageModules
        .on('click', '.panel-heading', function (e) {
            var $this, moduleName, $modulecontainer, apiurl, $body, browseKeys = [], rowId, formKeys = [], keys, $settings;

            $this            = jQuery(this);
            moduleName       = $this.closest('.panel-group').attr('id');
            $modulecontainer = $control.find('#' + moduleName);
            apiurl           = window[moduleName + 'Controller'].apiurl;
            $body            = $control.find('#' + moduleName + '.panel-body')
            //console.log($body)
            if ($body.is(':empty')) {
                FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, function onSuccess(response) { 
                    $settings = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
                    keys      = $settings.find('.field');
                    rowId     = jQuery(keys[0]).attr('data-browsedatafield');

                    for (var i = 1; i < keys.length; i++) {
                        var Key = jQuery(keys[i]).attr('data-browsedatafield');
                        browseKeys.push(Key);
                        if (i === 1 && Key !== 'Inactive' || i === 2 && jQuery(keys[1]).attr('data-browsedatafield') === 'Inactive') {
                            for (var k = 0; k < response.length - 1; k++) {
                                for (var l = 0, sorted; l < response.length - 1; l++) {
                                    if (response[l][Key].toLowerCase() > response[l + 1][Key].toLowerCase()) {
                                        sorted = response[l + 1];
                                        response[l + 1] = response[l];
                                        response[l] = sorted;
                                    }
                                }
                            }
                        }
                    };

                    
                    for(var i = 0; i < response.length; i++) {
                        var html = [], $moduleRows;
                        html.push('<div class="panel-record">')
                        html.push('  <div class="panel panel-info container-fluid">');
                        html.push('    <div class="row-heading">');
                        html.push('      <i class="material-icons record-selector">keyboard_arrow_down</i>')
                        //html.push('<label>' + moduleName + '</label>')
                        //html.push('<label>' + row[moduleName] + '</label>');
                        for (var j = 0; j < browseKeys.length; j++) {
                            if (browseKeys[j] === 'Inactive' || browseKeys[j] === 'Color') {
                                browseKeys.splice(j, 1);
                            }
                            if (browseKeys.length === 1) {
                                html.push('      <div style="width:100%;padding-left: inherit;">');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                                html.push('        </div>');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                                html.push('        </div>');
                                html.push('      </div>');
                            }
                            if (browseKeys.length === 2) {
                                html.push('      <div style="width:100%;padding-left: inherit;">');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                                html.push('        </div>');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                                html.push('        </div>');
                                html.push('      </div>');
                            }
                            if (browseKeys.length === 3) {
                                html.push('      <div style="width:100%;padding-left: inherit;">');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                                html.push('        </div>');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                                html.push('        </div>');
                                html.push('      </div>');
                            }
                            if (browseKeys.length === 4) {
                                html.push('      <div style="width:100%;padding-left: inherit;">');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                                html.push('        </div>');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                                html.push('        </div>');
                                html.push('      </div>');
                            }
                            if (browseKeys.length === 5) {
                                html.push('      <div style="width:100%;padding-left: inherit;">');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label style="font-weight:800;">' + browseKeys[j] + '</label>');
                                html.push('        </div>');
                                html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                html.push('          <label>' + response[i][browseKeys[j]] + '</label>');
                                html.push('        </div>');
                                html.push('      </div>');
                            }
                        }
                        //html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>'); 
                        html.push('    </div>');
                        html.push('  </div>');
                        html.push('  <div class="panel-body" style="display:none;" id="' + response[i][rowId] + '"></div>');
                        html.push('</div>');
                        $moduleRows = jQuery(html.join(''));
                        $moduleRows.data('recorddata', response[i]);
                        $body.append($moduleRows);
                    }


                }, null, $modulecontainer);
            }

            if ($settingsPageModules.find('.panel-collapse').css('display') === 'none') {
                $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_up') 
                $settingsPageModules.find('.panel-collapse').show("fast");
            } else {
                $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_down')  
                $settingsPageModules.find('.panel-collapse').hide('fast');
            }
        })
        .on('click', '.heading-menu', function (e) {
            e.stopPropagation(); 
            if (jQuery(this).next().css('display') === 'none') {
                jQuery(this).next().css('display', 'block');
            } else {
                jQuery(this).next().css('display', 'none');
            }
        })
    ;

    $control
    .unbind().on('click', '.row-heading', function (e) {
        e.stopPropagation();
        var formKeys = [], formData = [], recordData, $rowBody, $form, moduleName;
        recordData = jQuery(this).parent().parent().data('recorddata');

        moduleName = jQuery(this).closest('div.panel-group')[0].id;

        if (moduleName === 'FacilityCategory' || moduleName === 'PartsCategory' || moduleName === 'RentalCategory' || moduleName === 'SalesCategory' || moduleName === 'LaborCategory' || moduleName === 'MiscCategory') {
            $rowBody = $control.find('#' + recordData['InventoryCategoryId'] + '.panel-body');
        } else if (moduleName === 'FacilityScheduleStatus' || moduleName === 'CrewScheduleStatus' || moduleName === 'VehicleScheduleStatus') {
            $rowBody = $control.find('#' + recordData['ScheduleStatusId'] + '.panel-body');
        } else if (moduleName === 'FacilityRate' || moduleName === 'LaborRate' || moduleName === 'MiscRate') {
            $rowBody = $control.find('#' + recordData['RateId'] + '.panel-body');
        } else {
            $rowBody = $control.find('#' + recordData[moduleName + 'Id'] + '.panel-body');
        }

        $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
        if ($rowBody.is(':empty')) {
            $rowBody.append($form);
            FwModule.openForm($form, 'EDIT');                            
            $form.find('div[data-type="NewMenuBarButton"]').off();

            for (var key in recordData) {
                var value = recordData[key];
                var $field = $form.find('[data-datafield="' + key + '"]');
                var displayfield = $field.attr('data-displayfield');
                if ($field.length > 0) {
                    if (typeof displayfield !== 'undefined' && typeof recordData[displayfield] !== 'undefined') {
                        var text = recordData[displayfield];
                        FwFormField.setValue($form, '[data-datafield="' + key + '"]', value, text);
                    } else {
                        FwFormField.setValue($form, '[data-datafield="' + key + '"]', value);
                    }
                }
            }
        }

        if (typeof window[moduleName + 'Controller']['afterLoad'] === 'function') {
            window[moduleName + 'Controller']['afterLoad']($form);
        }

        if ($rowBody.css('display') === 'none' || $rowBody.css('display') === undefined) {
            $rowBody.parent().find('.record-selector').html('keyboard_arrow_up');
            $rowBody.show("fast");
        } else {
            $rowBody.parent().find('.record-selector').html('keyboard_arrow_down');
            $rowBody.hide("fast");
        }
    })
    .on('click', '.save', function (e) {
        e.stopPropagation();
        var $form = jQuery(this).closest('.panel-record').find('.fwform');
        FwSettings.saveForm(window[moduleName + 'Controller'].Module, $form, false, '', $control, browseKeys, rowId, moduleName)
        $body.find('.new-row').hide();
        $body.empty();
        FwSettings.getRows($body, $control, apiurl, $modulecontainer, moduleName);
    });

    //$control.on('keypress', '#settingsSearch', function (e) { 
    //    if (e.which === 13) { 
    //        e.preventDefault(); 
    //        var $settings, val; 
    //        $settings = jQuery('a#title'); 
    //        val = jQuery.trim(this.value).toUpperCase(); 
    //        if (val === "") { 
    //            $settings.parent().show(); 
    //        } else { 
    //            $settings.closest('div.panel-group').hide(); 
    //            $settings.filter(function () { 
    //                return -1 != jQuery(this).text().toUpperCase().indexOf(val); 
    //            }).closest('div.panel-group').show(); 
    //        } 
    //    } 
    //}); 

    return $settingsPageModules;
};
//----------------------------------------------------------------------------------------------
FwSettings.renderRecordHtml = function ($control, moduleName, values) {
    var $browse;

    //$browse = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
    //$control.find('#' + moduleName).empty().append($browse);

    //FwBrowse.init($browse);
    //FwBrowse.renderRuntimeHtml($browse);
    //api then loop then reference
};
//----------------------------------------------------------------------------------------------
//FwSettings.openModule() {
//    var moduleName;

//    $moduleRecords = jQuery(jQuery('#tmpl-modules-' + ).html());
//    $moduleGrid.empty().append($moduleRecords);
//    //$moduleRecords.data('ondatabind', function (request) {
//    //    request.module = 'AccidentEstimate';
//    //    request.uniqueids = {
//    //        accidentid: $form.find('div.fwformfield[data-datafield="accident.accidentid"] input').val()
//    //    };
//    //});
//};
//----------------------------------------------------------------------------------------------