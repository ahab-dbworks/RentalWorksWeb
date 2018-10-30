//----------------------------------------------------------------------------------------------
class FwSettingsClass {
    filter: Array<any> = [];
    customFilter: Array<any> = [];
    screen: any = {
        'moduleCaptions': {}
    }
    //----------------------------------------------------------------------------------------------
    init() {
        this.screen.moduleCaptions = {};
        this.getCaptions(this.screen);
    };
    //----------------------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html = [];

        html.push('<div class="fwsettingsheader">');
        //html.push('<div class="settingsmenu">');
        //html.push('</div>')
        html.push('  <div class="input-group pull-right">');
        html.push('    <input type="text" id="settingsSearch" class="form-control" placeholder="Search..." autofocus>');
        html.push('    <span class="input-group-addon">');
        html.push('      <i class="material-icons">search</i>');
        html.push('    </span>');
        html.push('  </div>');
        html.push('</div>');
        html.push('<div class="well"></div>');
        //< div class="input-group pull-right" > <input type="text" class="form-control" placeholder="Settings..."><span class="input-group-addon"><i class="material-icons">search</i></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        var settingsMenu = this.getHeaderView($control);

        $control.html(html.join(''));

        $control.find('.fwsettingsheader').append(settingsMenu);
    };
    //----------------------------------------------------------------------------------------------
    saveForm(module, $form, closetab, navigationpath, $control, browseKeys, rowId, moduleName, emptyRows?, getRows?) {
        var $tabpage, fields, ids, mode, isValid, $tab, request, controllername, controller;
        mode = $form.attr('data-mode');
        $tabpage = $form.parent();
        $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        isValid = FwModule.validateForm($form);
        controllername = $form.attr('data-controller');
        controller = window[controllername];

        if (isValid) {
            ids = FwModule.getFormUniqueIds($form);
            fields = FwModule.getFormFields($form, false);
            if (typeof controller.apiurl !== 'undefined') {
                request = FwModule.getFormModel($form, false);
            } else {
                request = {
                    module: module,
                    mode: mode,
                    ids: ids,
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
                emptyRows();
                getRows();
            });
        }
    };
    //----------------------------------------------------------------------------------------------
    getCaptions(screen) {
        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
        var modules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');

        FwAppData.apiMethod(true, 'GET', 'api/v1/customfield/', null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.length; i++) {
                let fieldName = response[i].FieldName.replace(/\s/g, '').toUpperCase();
                let customObject = {
                    custom: true,
                    datafield: response[i].FieldName,
                    caption: response[i].FieldName,
                    datatype: response[i].FieldType,
                    module: response[i].ModuleName
                };
                if (typeof screen.moduleCaptions[fieldName] === 'undefined') {
                    screen.moduleCaptions[fieldName] = {};
                }
                if (typeof screen.moduleCaptions[fieldName][response[i].ModuleName] === 'undefined') {
                    screen.moduleCaptions[fieldName][response[i].ModuleName] = [];
                }
                screen.moduleCaptions[fieldName][response[i].ModuleName].push(customObject);
            }
            for (var idx = 0; idx < modules.length; idx++) {
                var moduleName = modules[idx].properties.controller.slice(0, -10);
                var $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                var $fwformfields = $form.find('.fwformfield[data-caption]');
                for (var j = 0; j < $fwformfields.length; j++) {
                    var $field = $fwformfields.eq(j);
                    var caption = $field.attr('data-caption').toUpperCase();
                    if ($field.attr('data-type') === 'radio') {
                        var radioCaptions = $field.find('div');
                        for (var k = 0; k < radioCaptions.length; k++) {
                            var radioCaption = jQuery(radioCaptions[k]).attr('data-caption').toUpperCase()
                            screen.moduleCaptions[radioCaption] = {};
                            screen.moduleCaptions[radioCaption][moduleName] = [];
                            screen.moduleCaptions[radioCaption][moduleName].push($field);
                        }
                    }
                    if (typeof screen.moduleCaptions[caption] === 'undefined') {
                        screen.moduleCaptions[caption] = {};
                    }
                    if (typeof screen.moduleCaptions[caption][moduleName] === 'undefined') {
                        screen.moduleCaptions[caption][moduleName] = [];
                    }
                    screen.moduleCaptions[caption][moduleName].push($field);
                }
            }
        }, function onError(response) {
            FwFunc.showError(response);
        }, null);
    }
    //---------------------------------------------------------------------------------------------- 
    getRows($body, $control, apiurl, $modulecontainer, moduleName) {
        FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, function onSuccess(response) {
            let keys, browseKeys = [], rowId;
            let me = this;
            let $browse = window[moduleName + 'Controller'].openBrowse();
            let colors = [];
            let browseData = [];
            let duplicateDatafields = {};
            let withoutDuplicates = [];

            let $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
            keys = $browse.find('.field');
            rowId = jQuery(keys[0]).attr('data-browsedatafield');

            $body.append('<div class="legend"><span class="input-group-addon search"><i class="material-icons">search</i></span><input type="text" id="recordSearch" class="form-control" placeholder="Record Search" autofocus></div>');

            //append legend
            if ($browse.find('.legend').length > 0) {
                $body.append($browse.find('.legend'));
            }

            for (var i = 1; i < keys.length; i++) {
                if (jQuery(keys[i]).attr('data-datafield')) {
                    var Key = jQuery(keys[i]).attr('data-datafield');
                } else if (jQuery(keys[i]).attr('data-browsedatafield')) {
                    var Key = jQuery(keys[i]).attr('data-browsedatafield');
                }
                var cellColor = $browse.find('div[data-browsedatafield="' + Key + '"]').data('cellcolor')
                browseKeys.push(Key);
                var fieldData = {};
                if ($browse.find('div[data-browsedatafield="' + Key + '"]').data('caption') !== undefined) {
                    fieldData['caption'] = $browse.find('div[data-browsedatafield="' + Key + '"]').data('caption');
                } else {
                    fieldData['caption'] = Key;
                };

                if (cellColor) {
                    fieldData['color'] = true;
                    for (var i = 0; i < response.length; i++) {
                        colors.push(response[i].cellColor);
                    }
                }

                fieldData['datatype'] = $browse.find('div[data-browsedatafield="' + Key + '"]').data('datatype');
                fieldData['datafield'] = Key
                browseData.push(fieldData)

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

            if (FwSettings.filter.length > 0) {
                var uniqueFilters = [];
                for (var j = 0; j < FwSettings.filter.length; j++) {
                    if (uniqueFilters.indexOf(FwSettings.filter[j]) === -1) {
                        uniqueFilters.push(FwSettings.filter[j]);
                    }
                }

                for (var i = 0; i < uniqueFilters.length; i++) {
                    var filterField = $form.find(`div[data-datafield="${uniqueFilters[i]}"]`);
                    if (filterField.length > 0 && filterField.attr('data-type') !== 'key') {
                        var filterData = {};
                        if (filterField.attr('data-type') === 'validation') {
                            filterData['datafield'] = filterField.attr('data-displayfield');
                            browseKeys.push(filterField.attr('data-displayfield'));
                        } else {
                            filterData['datafield'] = uniqueFilters[i];
                            browseKeys.push(uniqueFilters[i]);
                        }
                        filterData['caption'] = filterField.attr('data-caption');
                        filterData['datatype'] = filterField.attr('data-type');
                        if (filterField.css('visibility') === 'hidden' || filterField.css('display') === 'none') {
                            filterData['hidden'] = true;
                        }
                        browseData.push(filterData);
                    }
                }
            }
            if (FwSettings.customFilter.length > 0) {
                var uniqueCustomFilter = [];
                for (var j = 0; j < FwSettings.customFilter.length; j++) {
                    if (uniqueCustomFilter.indexOf(FwSettings.customFilter[j]) === -1) {
                        uniqueCustomFilter.push(FwSettings.customFilter[j]);
                    }
                }
                for (var k = 0; k < uniqueCustomFilter.length; k++) {
                    if (uniqueCustomFilter[k].module == $form.data('controller').slice(0, -10)) {
                        browseData.push(uniqueCustomFilter[k]);
                        browseKeys.push(uniqueCustomFilter[k].datafield);
                    }
                }
            }

            browseData.forEach(function (browseField) {
                if (!duplicateDatafields[browseField.datafield]) {
                    withoutDuplicates.push(browseField);
                    duplicateDatafields[browseField.datafield] = true;
                }
            });
            browseData = withoutDuplicates;

            for (var i = 0; i < response.length; i++) {
                var html = [], $moduleRows;

                response[i]['_Custom'].forEach((customField) => {
                    response[i][customField.FieldName] = customField.FieldValue
                });

                html.push('<div class="panel-record" id="' + response[i][rowId] + '">');
                html.push('  <div class="panel panel-info container-fluid">');
                html.push('    <div class="row-heading">');
                html.push('      <i class="material-icons record-selector">keyboard_arrow_down</i>');

                for (var j = 0; j < browseData.length; j++) {
                    if (browseData[j]['caption'] === 'Inactive' && response[i][browseData[j]['caption']] === true) {
                        html[1] = '<div class="panel panel-info container-fluid" style="display:none;">';
                        html[2] = '<div class="inactive-panel row-heading" style="background-color:lightgray;">';
                    }
                    if (browseData[j]['caption'] !== 'Inactive' && browseData[j]['caption'] !== 'Color' && !browseData[j]['hidden']) {
                        html.push('      <div style="width:100%;padding-left: inherit;">');
                        html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                        html.push('          <label style="font-weight:800;">' + browseData[j]['caption'] + '</label>');
                        html.push('        </div>');

                        if (browseData[j]['datatype'] === 'checkbox') {
                            html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="width:50px;">');
                            if (response[i][browseKeys[j]]) {
                                html.push('<div class="checkboxwrapper"><input class="value" data-datafield="' + browseData[j]['datafield'] + '" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" checked><label></label></div>');
                            } else {
                                html.push('<div class="checkboxwrapper"><input class="value" data-datafield="' + browseData[j]['datafield'] + '" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;"><label></label></div>');
                            }
                        } else {
                            if (browseData[j]['color'] && response[i][cellColor] !== '') {
                                html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="color:' + response[i][cellColor] + ';width:8em;white-space:nowrap;height: 0;display:flex;border-bottom: 20px solid transparent;border-top: 20px solid;">');
                            } else {
                                html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                            }
                            html.push('    <label data-datafield="' + browseData[j]['datafield'] + '" style="color:#31708f">' + response[i][browseKeys[j]] + '</label>');
                        }
                        html.push('        </div>');
                        html.push('      </div>');
                        //if (browseKeys[j] === 'Inactive' && response[i][browseKeys[j]] === true) {
                        //    html.push('</div>');
                        //}
                    }
                }
                //html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>'); 
                html.push('    </div>');
                html.push('  </div>');
                html.push('  <div class="panel-body data-panel" style="display:none;" id="' + response[i][rowId] + '" data-type="settings-row"></div>');
                html.push('</div>');
                $moduleRows = jQuery(html.join(''));
                $moduleRows.data('recorddata', response[i]);
                $moduleRows.data('browsedata', browseData);
                $body.append($moduleRows);
                $body.find('#recordSearch').focus();
            }

            $body.find('#recordSearch').on('keypress', function (e) {
                if (e.which === 13) {
                    let dataKeys = [];
                    let query = jQuery.trim(this.value).toUpperCase();
                    let matches = [];
                    let $panelBody = jQuery(this).closest('.panel-body')
                    for (var key in response[0]) {
                        if (key !== 'DateStamp' && key !== 'RecordTitle' && key !== '_Custom' && key !== 'Inactive' && key !== rowId) {
                            dataKeys.push(key)
                        }
                    }
                    for (var i = 0; i < dataKeys.length; i++) {
                        for (var j = 0; j < response.length; j++) {
                            if (typeof response[j][dataKeys[i]] === 'string' && response[j][dataKeys[i]].toUpperCase().indexOf(query) !== -1) {
                                matches.push(response[j][rowId]);
                            }
                        }
                    }
                    $panelBody.find('.panel-record').hide();
                    for (var k = 0; k < matches.length; k++) {
                        $panelBody.find('#' + matches[k]).show();
                    }
                }
            })

            $control
                .on('click', '.row-heading', function (e) {
                    var recordData, $rowBody, $form, controller;
                    e.stopPropagation();
                    recordData = jQuery(this).parent().parent().data('recorddata');
                    $rowBody = $control.find('#' + recordData[moduleName + 'Id'] + '.panel-body');
                    $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                    controller = $form.data('controller');
                    if ($rowBody.is(':empty')) {
                        $form = window[controller].openForm('EDIT');
                        $rowBody.append($form);

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
                    var $form = jQuery(this).closest('.panel-record').find('.fwform');
                    me.saveForm(window[moduleName + 'Controller'].Module, $form, false, '');
                    $body.find('.new-row').hide();
                    $body.empty();
                    me.getRows($body, $control, apiurl, $control.find('#' + moduleName), moduleName);
                })
                ;

        }, null, $modulecontainer);
    }
    //---------------------------------------------------------------------------------------------- 
    newRow($body, $control, apiurl, $modulecontainer, moduleName, $modules) {
        var $form, controller, rowId, newRowHtml = [], me = this;

        $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

        if ($body.is(':empty')) {
            this.getRows($body, $control, apiurl, $modulecontainer, moduleName);
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
            newRowHtml.push('        <div class="pull-right close-new-row" style="padding-right:20px;"><i class="material-icons">clear</i>Cancel</div>');
            newRowHtml.push('      </div>');
            newRowHtml.push('    </div>');
            newRowHtml.push('  </div>');
            newRowHtml.push('</div>');

            controller = $form.data('controller');
            $form = window[controller].openForm('NEW');
            $body.prepend($form);
            $body.prepend(jQuery(newRowHtml.join('')));
        }

        $body.on('click', '.close-new-row', function (e) {
            e.stopPropagation();
            var $form;
            $form = jQuery(this).closest('.panel-body').find('.fwform');
            $body.find('.new-row').remove();
            $form.remove();
        });

        $body.on('click', '.save-new-row', function (e) {
            var $form;
            e.stopPropagation();
            $form = jQuery(this).closest('.panel-body').find('.fwform');
            me.saveForm(window[moduleName + 'Controller'].Module, $form, false, '', $control, [], rowId, moduleName, me.getRows($body, $control, apiurl, $control.find('#' + moduleName), moduleName), $body.empty());

        });
    }
    //---------------------------------------------------------------------------------------------- 
    renderModuleHtml($control, title, moduleName, description, menu, moduleId) {
        var html = [], $settingsPageModules, $rowBody, $modulecontainer, apiurl, $body, $form, browseKeys = [], rowId, filter = [], me = this;

        $modulecontainer = $control.find('#' + moduleName);
        apiurl = window[moduleName + 'Controller'].apiurl;
        $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());

        html.push('<div class="panel-group" id="' + moduleName + '" data-id="' + moduleId + '">');
        html.push('  <div class="panel panel-primary">');
        html.push('    <div data-toggle="collapse" data-target="' + moduleName + '" href="' + moduleName + '" class="panel-heading">');
        html.push('      <div class="flexrow" style="max-width:none;">');
        html.push('        <i class="material-icons arrow-selector">keyboard_arrow_down</i>');
        html.push('        <h4 class="panel-title">');
        html.push('        <a id="title" data-toggle="collapse">' + menu + ' - ' + title + '</a>');
        html.push('        <div id="myDropdown" class="dropdown-content">');
        html.push('          <a class="new-row-menu">New Item</a>');
        html.push('          <a class="show-inactive">Show Inactive</a>');
        html.push('          <a class="hide-inactive" style="display:none;">Hide Inactive</a>');
        html.push('          <a class="pop-out">Pop Out Module</a>');
        html.push('        </div>');
        html.push('        <div style="margin-left:auto;">');
        html.push('          <i class="material-icons refresh">cached</i>');
        html.push('          <i class="material-icons heading-menu">more_vert</i>');
        html.push('        </div>');
        html.push('        </h4>');
        html.push('      </div>');
        if (description === "") {
            html.push('      <small id="searchId" style="display:none;">' + moduleName + '</small>');
            html.push('      <small id="description-text">' + moduleName + '</small>');
        } else {
            html.push('      <small id="searchId" style="display:none;">' + moduleName + '</small>');
            html.push('      <small id="description-text">' + description + '</small>');
        }
        html.push('    </div>');
        html.push('    <div class="panel-collapse collapse" style="display:none; "><div class="panel-body header-content" id="' + moduleName + '"></div></div>');
        html.push('  </div>');
        html.push('</div>');
        $settingsPageModules = jQuery(html.join(''));

        $control.find('.well').append($settingsPageModules);

        $settingsPageModules.on('click', '.btn', function (e) {
            $body.empty();
            me.getRows($body, $control, apiurl, $control.find('#' + moduleName), moduleName);
        });

        $settingsPageModules.on('click', '.new-row-menu', function (e) {
            e.stopPropagation();
            if (jQuery(this).parent().find('.hidden').length === 0) {
                jQuery(this).parent().find('div[data-mode="NEW"]').addClass('hidden').hide('fast');
            } else {
                jQuery(this).parent().find('div[data-mode="NEW"]').removeClass('hidden').show('fast');
            }
            $body = $control.find('#' + moduleName + '.panel-body');
            me.newRow($body, $control, apiurl, $modulecontainer, moduleName, $settingsPageModules);
            jQuery(this).parent().hide();
        });

        $settingsPageModules.on('click', '.show-inactive', function (e) {
            e.stopPropagation();
            if (jQuery(this).closest('.panel').find('.panel-collapse').is(':visible')) {
                jQuery(this).closest('.panel').find('.inactive-panel').closest('.panel-record').show();
                jQuery(this).closest('.panel').find('.inactive-panel').parent().show();
                jQuery(this).hide();
                jQuery(this).parent().find('.hide-inactive').show();
            }
            jQuery(this).parent().hide();
        });

        $settingsPageModules.on('click', '.hide-inactive', function (e) {
            e.stopPropagation();
            if (jQuery(this).closest('.panel').find('.panel-collapse').is(':visible')) {
                jQuery(this).closest('.panel').find('.inactive-panel').closest('.panel-record').hide();
                jQuery(this).closest('.panel').find('.inactive-panel').parent().hide();
                jQuery(this).hide();
                jQuery(this).parent().find('.show-inactive').show();
            }
            jQuery(this).parent().hide();
        });

        $settingsPageModules.on('click', '.pop-out', function (e) {
            e.stopPropagation();
            program.popOutTab('#/module/' + moduleName);
            jQuery(this).parent().hide();
        });

        $settingsPageModules
            .on('click', '.panel-heading', function (e) {
                var $this, moduleName, $browse, $modulecontainer, apiurl, $body, browseData = [], browseKeys = [], rowId, formKeys = [], keys, $settings, $form, duplicateDatafields, colors = [];

                $this = jQuery(this);
                moduleName = $this.closest('.panel-group').attr('id');
                $browse = window[moduleName + 'Controller'].openBrowse();
                $modulecontainer = $control.find('#' + moduleName);
                apiurl = window[moduleName + 'Controller'].apiurl;
                $body = $control.find('#' + moduleName + '.panel-body');
                duplicateDatafields = {};
                var withoutDuplicates = [];

                if ($body.is(':empty')) {

                    //append legend
                    $body.append('<div class="legend"><span class="input-group-addon search"><i class="material-icons">search</i></span><input type="text" id="recordSearch" class="form-control" placeholder="Record Search" autofocus></div>');
                    if ($browse.find('.legend').length > 0) {
                        $body.append($browse.find('.legend'));
                    }

                    FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, function onSuccess(response) {
                        $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                        keys = $browse.find('.field');
                        rowId = jQuery(keys[0]).attr('data-browsedatafield');

                        for (var i = 1; i < keys.length; i++) {
                            if (jQuery(keys[i]).attr('data-datafield')) {
                                var Key = jQuery(keys[i]).attr('data-datafield');
                            } else if (jQuery(keys[i]).attr('data-browsedatafield')) {
                                var Key = jQuery(keys[i]).attr('data-browsedatafield');
                            }
                            var cellColor = $browse.find('div[data-browsedatafield="' + Key + '"]').data('cellcolor')
                            browseKeys.push(Key);
                            var fieldData = {};
                            if ($browse.find('div[data-browsedatafield="' + Key + '"]').data('caption') !== undefined) {
                                fieldData['caption'] = $browse.find('div[data-browsedatafield="' + Key + '"]').data('caption');
                            } else {
                                fieldData['caption'] = Key;
                            };

                            if (cellColor) {
                                fieldData['color'] = true;
                                for (var i = 0; i < response.length; i++) {
                                    colors.push(response[i].cellColor);
                                }
                            }

                            fieldData['datatype'] = $browse.find('div[data-browsedatafield="' + Key + '"]').data('datatype');
                            fieldData['datafield'] = Key;
                            browseData.push(fieldData);

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

                        if (me.filter.length > 0) {
                            var uniqueFilters = [];
                            for (var j = 0; j < me.filter.length; j++) {
                                if (uniqueFilters.indexOf(me.filter[j]) === -1) {
                                    uniqueFilters.push(me.filter[j]);
                                }
                            }

                            for (var i = 0; i < uniqueFilters.length; i++) {
                                var filterField = $form.find(`div[data-datafield="${uniqueFilters[i]}"]`);
                                if (filterField.length > 0 && filterField.attr('data-type') !== 'key') {
                                    var filterData = {};
                                    if (filterField.attr('data-type') === 'validation') {
                                        filterData['datafield'] = filterField.attr('data-displayfield');
                                        browseKeys.push(filterField.attr('data-displayfield'));
                                    } else {
                                        filterData['datafield'] = uniqueFilters[i];
                                        browseKeys.push(uniqueFilters[i]);
                                    }
                                    filterData['caption'] = filterField.attr('data-caption');
                                    filterData['datatype'] = filterField.attr('data-type');
                                    if (filterField.css('visibility') === 'hidden' || filterField.css('display') === 'none') {
                                        filterData['hidden'] = true;
                                    }
                                    browseData.push(filterData);
                                }
                            }
                        }
                        if (me.customFilter.length > 0) {
                            var uniqueCustomFilter = [];
                            for (var j = 0; j < me.customFilter.length; j++) {
                                if (uniqueCustomFilter.indexOf(me.customFilter[j]) === -1) {
                                    uniqueCustomFilter.push(me.customFilter[j]);
                                }
                            }
                            for (var k = 0; k < uniqueCustomFilter.length; k++) {
                                if (uniqueCustomFilter[k].module == $form.data('controller').slice(0, -10)) {
                                    browseData.push(uniqueCustomFilter[k]);
                                    browseKeys.push(uniqueCustomFilter[k].datafield);
                                }
                            }
                        }
                        // remove duplicated fields
                        browseData.forEach(function (browseField) {
                            if (!duplicateDatafields[browseField.datafield]) {
                                withoutDuplicates.push(browseField);
                                duplicateDatafields[browseField.datafield] = true;
                            }
                        });
                        browseData = withoutDuplicates;

                        for (var i = 0; i < response.length; i++) {
                            var html = [], $moduleRows;

                            response[i]['_Custom'].forEach((customField) => {
                                response[i][customField.FieldName] = customField.FieldValue
                            });

                            html.push('<div class="panel-record" id="' + response[i][rowId] + '">');
                            html.push('  <div class="panel panel-info container-fluid">');
                            html.push('    <div class="row-heading">');
                            html.push('      <i class="material-icons record-selector">keyboard_arrow_down</i>');

                            for (var j = 0; j < browseData.length; j++) {
                                if (browseData[j]['caption'] === 'Inactive' && response[i][browseData[j]['caption']] === true) {
                                    html[1] = '<div class="panel panel-info container-fluid" style="display:none;">';
                                    html[2] = '<div class="inactive-panel row-heading" style="background-color:lightgray;">';
                                }
                                if (browseData[j]['caption'] !== 'Inactive' && browseData[j]['caption'] !== 'Color' && !browseData[j]['hidden']) {
                                    html.push('      <div style="width:100%;padding-left: inherit;">');
                                    html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                    html.push('          <label style="font-weight:800;">' + browseData[j]['caption'] + '</label>');
                                    html.push('        </div>');

                                    if (browseData[j]['datatype'] === 'checkbox') {
                                        html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="width:50px;">');
                                        if (response[i][browseKeys[j]]) {
                                            html.push('<div class="checkboxwrapper"><input class="value" data-datafield="' + browseData[j]['datafield'] + '" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" checked><label></label></div>');
                                        } else {
                                            html.push('<div class="checkboxwrapper"><input class="value" data-datafield="' + browseData[j]['datafield'] + '" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;"><label></label></div>');
                                        }
                                    } else {
                                        if (browseData[j]['color'] && response[i][cellColor] !== '') {
                                            html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="color:' + response[i][cellColor] + ';width:8em;white-space:nowrap;height: 0;display:flex;border-bottom: 20px solid transparent;border-top: 20px solid;">');
                                        } else {
                                            html.push('    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                        }
                                        html.push('    <label data-datafield="' + browseData[j]['datafield'] + '" style="color:#31708f">' + response[i][browseKeys[j]] + '</label>');
                                    }
                                    html.push('        </div>');
                                    html.push('      </div>');
                                    //if (browseKeys[j] === 'Inactive' && response[i][browseKeys[j]] === true) {
                                    //    html.push('</div>');
                                    //}
                                }
                            }
                            //html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>'); 
                            html.push('    </div>');
                            html.push('  </div>');
                            html.push('  <div class="panel-body data-panel" style="display:none;" id="' + response[i][rowId] + '" data-type="settings-row"></div>');
                            html.push('</div>');
                            $moduleRows = jQuery(html.join(''));
                            $moduleRows.data('recorddata', response[i]);
                            $moduleRows.data('browsedata', browseData);
                            $body.append($moduleRows);
                            $body.find('#recordSearch').focus();
                        }

                        $body.find('#recordSearch').on('keypress', function (e) {
                            if (e.which === 13) {
                                let dataKeys = [];
                                let query = jQuery.trim(this.value).toUpperCase();
                                let matches = [];
                                let $panelBody = jQuery(this).closest('.panel-body')
                                for (var key in response[0]) {
                                    if (key !== 'DateStamp' && key !== 'RecordTitle' && key !== '_Custom' && key !== 'Inactive' && key !== rowId) {
                                        dataKeys.push(key)
                                    }
                                }
                                for (var i = 0; i < dataKeys.length; i++) {
                                    for (var j = 0; j < response.length; j++) {
                                        if (typeof response[j][dataKeys[i]] === 'string' && response[j][dataKeys[i]].toUpperCase().indexOf(query) !== -1) {
                                            matches.push(response[j][rowId]);
                                        }
                                    }
                                }
                                $panelBody.find('.panel-record').hide();
                                for (var k = 0; k < matches.length; k++) {
                                    $panelBody.find('#' + matches[k]).show();
                                }
                            }
                        })
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
                let menuButton: any = jQuery(this);
                if (menuButton.parent().prev().css('display') === 'none') {
                    menuButton.parent().prev().css('display', 'block');
                    jQuery(document).one('click', function closeMenu(e) {
                        if (menuButton.has(e.target).length === 0) {
                            menuButton.parent().prev().css('display', 'none');
                        }
                    })
                } else {
                    menuButton.parent().prev().css('display', 'none');
                }
            })
            .on('click', '.refresh', function (e) {
                e.stopPropagation();
                let $body = $control.find('#' + moduleName + '.panel-body');
                if (!$body.is(':empty')) {
                    $body.empty();
                    me.getRows($body, $control, apiurl, $modulecontainer, moduleName);
                }
                if ($settingsPageModules.find('.panel-collapse').css('display') === 'none') {
                    $body.empty();
                    me.getRows($body, $control, apiurl, $modulecontainer, moduleName);
                    $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_up')
                    $settingsPageModules.find('.panel-collapse').show("fast");
                }
            });

        $control
            .unbind().on('click', '.row-heading', function (e) {
                e.stopPropagation();
                var formKeys = [], formData = [], recordData, $rowBody, $form, moduleName, moduleId, controller, uniqueids = {};
                recordData = jQuery(this).parent().parent().data('recorddata');
                moduleName = jQuery(this).closest('div.panel-group')[0].id;
                $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                moduleId = jQuery($form.find('.fwformfield[data-isuniqueid="true"]')[0]).data('datafield');
                uniqueids[moduleId] = recordData[moduleId];
                $rowBody = $control.find('#' + recordData[moduleId] + '.panel-body');
                controller = $form.data('controller');

                if ($rowBody.is(':empty')) {
                    $form = window[controller].openForm('EDIT');
                    $rowBody.append($form);
                    $form.find('.highlighted').removeClass('highlighted');
                    $form.find('div[data-type="NewMenuBarButton"]').off();
                    $form.find('.buttonbar').append('<div class="btn-delete" data-type="DeleteMenuBarButton"><i class="material-icons"></i><div class="btn-text">Delete</div></div>');

                    for (var key in recordData) {
                        for (var i = 0; i < me.filter.length; i++) {
                            var highlightField = $form.find('[data-datafield="' + key + '"]');
                            var hightlightFieldTabId = highlightField.closest('.tabpage').attr('data-tabid');
                            if (me.filter[i] === key) {
                                if ($form.find('[data-datafield="' + key + '"]').attr('data-type') === 'checkbox') {
                                    $form.find('[data-datafield="' + key + '"] label').addClass('highlighted');
                                } else {
                                    highlightField.find('.fwformfield-caption').addClass('highlighted');
                                    highlightField.parents('.fwtabs .fwcontrol').find('#' + hightlightFieldTabId).addClass('highlighted');

                                }
                            }
                        };
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
                    FwModule.loadForm(moduleName, $form);
                }


                if ($form.find('.fwappimage')[0]) {
                    FwAppImage.getAppImages($form.find('.fwappimage'))
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

                $form.data('afterLoadCustomFields', function () {
                    for (var key in recordData) {
                        for (var i = 0; i < me.filter.length; i++) {
                            if (me.filter[i] === key) {
                                $form.find('[data-datafield="' + key + '"]').find('.fwformfield-caption').css({ 'background': 'yellow' });
                            }
                        };
                        if (key === '_Custom') {
                            var value = recordData[key][0]['FieldValue'];
                            key = recordData[key][0]['FieldName'];
                        } else {
                            var value = recordData[key];
                        }
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
                });

            })
            .on('click', '.btn', function () {
                var browsedata = jQuery(this).closest('.panel-record').data('browsedata');
                var browsedatafields = [];
                var record = jQuery(this).closest('.panel-record').find('.panel-info');
                var $form = jQuery(this).closest('.panel-record').find('.fwform')
                if (typeof browsedata !== 'undefined') {
                    for (var i = 0; i < browsedata.length; i++) {
                        browsedatafields.push(browsedata[i].datafield);
                    }
                    for (var j = 0; j < browsedatafields.length; j++) {
                        if (jQuery(this).closest('.panel-record').find('.panel-info').find('label[data-datafield="' + browsedatafields[j] + '"]')) {
                            // check if field is valid or else may be a validation
                            if ($form.find('div[data-datafield="' + browsedatafields[j] + '"]').length > 0) {
                                if (browsedatafields[j] === 'Inactive') {
                                    if (FwFormField.getValueByDataField($form, browsedatafields[j])) {
                                        jQuery(this).closest('.panel-record').find('.row-heading').addClass('inactive-panel').css('background-color', 'lightgray');
                                    } else {
                                        jQuery(this).closest('.panel-record').find('.row-heading').removeClass('inactive-panel').css('background-color', '#d9edf7');
                                    }
                                } else {
                                    jQuery(this).closest('.panel-record').find('.panel-info').find('label[data-datafield="' + browsedatafields[j] + '"]').text(FwFormField.getValueByDataField($form, browsedatafields[j]));
                                    jQuery(this).closest('.panel-record').find('.panel-info').find('[data-datafield="' + browsedatafields[j] + '"]').prop('checked', FwFormField.getValueByDataField($form, browsedatafields[j]));
                                }
                            } else {
                                var validationValue: any = $form.find('div[data-displayfield="' + browsedatafields[j] + '"] input.fwformfield-text').val()
                                jQuery(this).closest('.panel-record').find('.panel-info').find('label[data-datafield="' + browsedatafields[j] + '"]').text(validationValue);
                            }
                        }
                    }
                }
            })
            .on('click', '.save', function (e) {
                e.stopPropagation();
                var $form = jQuery(this).closest('.panel-record').find('.fwform');
                me.saveForm(window[moduleName + 'Controller'].Module, $form, false, '', $control, browseKeys, rowId, moduleName)
                $body.find('.new-row').hide();
                $body.empty();
                me.getRows($body, $control, apiurl, $modulecontainer, moduleName);
            });

        $control.on('keypress', '#settingsSearch', function (e) {
            if (e.which === 13) {
                var $settings, val, $module, $settingsDescriptions, filter, customFilter;

                filter = [];
                customFilter = [];
                $settings = jQuery('small#searchId');
                $settingsDescriptions = jQuery('small#description-text');
                $module = jQuery('a#title');
                val = jQuery.trim(this.value).toUpperCase();
                if (val === "") {
                    $settings.closest('div.panel-group').show();
                } else {
                    var results = [];
                    results.push(val);
                    $settings.closest('div.panel-group').hide();
                    for (var caption in me.screen.moduleCaptions) {
                        if (caption.indexOf(val) !== -1 || caption.indexOf(val.split(' ').join('')) !== -1) {
                            for (var moduleName in me.screen.moduleCaptions[caption]) {
                                if (me.screen.moduleCaptions[caption][moduleName][0].custom) {
                                    customFilter.push(me.screen.moduleCaptions[caption][moduleName][0]);
                                } else {
                                    filter.push(me.screen.moduleCaptions[caption][moduleName][0].data().datafield);
                                }
                                results.push(moduleName.toUpperCase());
                            }
                        }
                    }
                    me.filter = filter;
                    me.customFilter = customFilter;
                    for (var i = 0; i < results.length; i++) {
                        //check descriptions for match
                        var module = $settingsDescriptions.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().indexOf(results[i]);
                        }).closest('div.panel-group');
                        module.find('.highlighted').removeClass('highlighted');

                        let description = module.find('small#description-text');
                        let title = module.find('a#title');
                        for (var j = 0; j < description.length; j++) {
                            if (description[j] !== undefined) {
                                let descriptionIndex = jQuery(description[j]).text().toUpperCase().indexOf(val);
                                let titleIndex = jQuery(title[j]).text().toUpperCase().indexOf(val);
                                if (descriptionIndex > -1) {
                                    description[j].innerHTML = jQuery(description[j]).text().substring(0, descriptionIndex) + '<span class="highlighted">' + jQuery(description[j]).text().substring(descriptionIndex, descriptionIndex + val.length) + '</span>' + jQuery(description[j]).text().substring(descriptionIndex + val.length);
                                }
                                if (titleIndex > -1) {
                                    title[j].innerHTML = jQuery(title[j]).text().substring(0, titleIndex) + '<span class="highlighted">' + jQuery(title[j]).text().substring(titleIndex, titleIndex + val.length) + '</span>' + jQuery(title[j]).text().substring(titleIndex + val.length);
                                }
                            }
                        }
                        module.show();

                        if ($module.filter(function () { return -1 != jQuery(this).text().toUpperCase().indexOf(val) }).length > 0) {
                            let titleHtml = $module.filter(function () {
                                return -1 != jQuery(this).text().toUpperCase().indexOf(val);
                            }).html();
                            let titleHtmlIndex = titleHtml.indexOf(val);
                            titleHtml = titleHtml.slice(0, titleHtmlIndex) + '<span class="highlighted">' + titleHtml.slice(titleHtmlIndex, titleHtmlIndex + val.length) + '</span>' + titleHtml.slice(titleHtmlIndex + val.length);
                            $module.filter(function () {
                                return -1 != jQuery(this).text().toUpperCase().split(' ').join('').indexOf(results[i]);
                            }).html(titleHtml).closest('div.panel-group').show();
                        }

                        $module.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().split(' ').join('').indexOf(results[i]);
                        }).closest('div.panel-group').show();
                    }

                    let searchResults = $control.find('.panel-heading:visible');

                    if (searchResults.length === 1 && searchResults.parent().find('.panel-body.header-content').is(':empty')) {
                        searchResults[0].click();
                    }
                }
            }
        });

        $control.on('click', '.appmenu', function (e) {
            let searchInput = $control.find('#settingsSearch');
            if (searchInput.val() !== '') {
                let event = jQuery.Event('keypress');
                event.which = 13;
                searchInput.val('');
                searchInput.trigger(event);
            }
        });

        $control.on('click', '.btn-delete', function (e) {
            let $form = jQuery(this).closest('.panel-record').find('.fwform');
            let ids = {};
            let $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Are you sure you want to delete this record?');
            let $yes = FwConfirmation.addButton($confirmation, 'Yes');
            FwConfirmation.addButton($confirmation, 'No');
            $yes.on('click', function () {
                let controller = $form.data('controller');
                ids = FwModule.getFormUniqueIds($form);
                let request = {
                    module: window[controller].Module,
                    ids: ids
                };
                try {
                    FwServices.module.method(request, window[controller].Module, 'Delete', $form, function (response) {
                        $form = FwModule.getFormByUniqueIds(ids);
                        if ((typeof $form != 'undefined') && ($form.length > 0)) {
                            $form.closest('.panel-record').remove();
                        }
                        FwNotification.renderNotification('SUCCESS', 'Record deleted.');
                    }, function (error) {
                        FwNotification.renderNotification('ERROR', 'Error deleting form.');
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        });

        return $settingsPageModules;
    };


    getHeaderView($control) {
        var $view, me = this;

        $view = jQuery('<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>');

        FwControl.renderRuntimeControls($view);

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
            if (nodeLv1MenuItem.properties.visible === 'T' && nodeLv1MenuItem.properties.caption === 'Settings') {
                switch (nodeLv1MenuItem.properties.nodetype) {
                    case 'Lv1SettingsMenu':
                        $menu = FwFileMenu.addMenu($view, nodeLv1MenuItem.properties.caption)
                        for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                            var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                            if (nodeLv2MenuItem.properties.visible === 'T') {
                                switch (nodeLv2MenuItem.properties.nodetype) {
                                    case 'SettingsMenu':
                                        dropDownMenuItems = [];
                                        for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                            var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                            if (nodeLv3MenuItem.properties.visible === 'T') {
                                                dropDownMenuItems.push({ id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl, moduleName: nodeLv3MenuItem.properties.controller.slice(0, -10) });
                                            }
                                        }
                                        me.generateDropDownModuleBtn($menu, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                        break;
                                    case 'SettingsModule':
                                        me.generateStandardModuleBtn($menu, $control, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl, nodeLv2MenuItem.properties.controller.slice(0, -10));
                                        break;
                                }
                            }
                        }
                        break;
                }
            }
        }

        return $view;
    };
    //----------------------------------------------------------------------------------------------
    generateDropDownModuleBtn($menu, $control, securityid, caption, imgurl, subitems) {
        var $modulebtn, btnHtml, subitemHtml, $subitem, version;

        version = $menu.closest('.fwfilemenu').attr('data-version');
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnHtml = [];
                btnHtml.push('<div id="btnModule' + securityid + '" class="ddmodulebtn" data-securityid="' + securityid + '">');
                btnHtml.push('<div class="ddmodulebtn-caption">');
                btnHtml.push('<div class="ddmodulebtn-text">');
                btnHtml.push(caption);
                btnHtml.push('</div>');
                if (version == '1') { btnHtml.push('<i class="material-icons">&#xE315;</i>'); } //keyboard_arrow_right
                if (version == '2') { btnHtml.push('<i class="material-icons">&#xE5CC;</i>'); } //chevron_right
                btnHtml.push('</div>');
                btnHtml.push('<div class="ddmodulebtn-dropdown" style="display:none"></div>');
                btnHtml.push('</div>');
                $modulebtn = jQuery(btnHtml.join(''));

                subitemHtml = [];
                subitemHtml.push('<div id="" class="ddmodulebtn-dropdown-btn">');
                subitemHtml.push('<div class="ddmodulebtn-dropdown-btn-text"></div>');
                subitemHtml.push('</div>');
                jQuery.each(subitems, function (index, value) {
                    $subitem = jQuery(subitemHtml.join(''));
                    $subitem.attr('data-securityid', subitems[index].id);
                    $subitem.find('.ddmodulebtn-dropdown-btn-text').html(value.caption);

                    $subitem.on('click', function () {
                        try {
                            if ($control.find('#' + value.moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                                $control.find('#' + value.moduleName + ' > div > div.panel-heading').click();
                            }
                            jQuery('html, body').animate({
                                scrollTop: $control.find('#' + value.moduleName).offset().top
                            }, 1);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });

                    $modulebtn.find('.ddmodulebtn-dropdown').append($subitem);
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwRibbon.generateDropDownModuleBtn: ' + securityid + ' caption is not defined in translation';
        }

        $menu.find('.menu').append($modulebtn);
    };
    //----------------------------------------------------------------------------------------------
    generateStandardModuleBtn($menu, $control, securityid, caption, modulenav, imgurl, moduleName) {
        var $modulebtn, btnHtml, btnId, version;
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnId = 'btnModule' + securityid;
                btnHtml = [];
                btnHtml.push('<div id="' + btnId + '" class="modulebtn" data-securityid="' + securityid + '">');
                btnHtml.push('<div class="modulebtn-text">');
                btnHtml.push(caption);
                btnHtml.push('</div>');
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwRibbon.generateStandardModuleBtn: ' + caption + ' caption is not defined in translation';
        }

        $modulebtn
            .on('click', function () {
                try {
                    if (modulenav != '') {
                        if ($control.find('#' + moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                            $control.find('#' + moduleName + ' > div > div.panel-heading').click();
                        }
                        jQuery('html, body').animate({
                            scrollTop: $control.find('#' + moduleName).offset().top
                        }, 1);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            ;

        $menu.find('.menu').append($modulebtn);
    };
}

var FwSettings = new FwSettingsClass();