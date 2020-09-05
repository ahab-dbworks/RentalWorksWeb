class FwSettingsClass {
    filter: Array<any> = [];
    customFilter: Array<any> = [];
    sectionFilter: Array<any> = [];
    searchValue: string;
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
        const me = this;
        const html: Array<string> = [];

        html.push('<div class="fwsettingsheader">');
        html.push('  <div class="settings-header-title">Settings</div>');
        html.push('  <div class="input-group pull-right">');
        html.push('    <div style="display:flex;width:255px;">');
        html.push('    <input type="text" id="settingsSearch" class="form-control" placeholder="Search..." autofocus>');
        html.push('    <span class="input-group-clear" style="display:none;">');
        html.push('      <i class="material-icons">clear</i>');
        html.push('    </span>');
        html.push('    </div>');
        html.push('    <span class="input-group-search">');
        html.push('      <i class="material-icons">search</i>');
        html.push('    </span>');
        html.push('  </div>');
        html.push('</div>');
        html.push('<div class="settings-content">');
        html.push('  <div class="menu-expand"><i class="material-icons">keyboard_arrow_right</i></div>');
        html.push('  <div class="navigation flexrow">');
        html.push('    <div class="navigation-menu flexcolumn"></div>');
        html.push('  </div>');
        html.push('  <div class="well"></div>');
        html.push('</div>');
        //< div class="input-group pull-right" > <input type="text" class="form-control" placeholder="Settings..."><span class="input-group-addon"><i class="material-icons">search</i></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        const settingsMenu = this.getHeaderView($control);
        settingsMenu.append('<div class="flexcolumn menu-collapse"><i class="material-icons">keyboard_arrow_left</i></div>');
        $control.html(html.join(''));

        const menuExpand = $control.find('.menu-expand');
        menuExpand.on('click', e => {
            menuCollapse.closest('.navigation').show();
            jQuery(e.currentTarget).hide();
            this.updateUserIdNavExpanded('settings', true);
        });

        const menuCollapse = settingsMenu.find('.menu-collapse');
        menuCollapse.on('click', e => {
            menuExpand.show();
            jQuery(e.currentTarget).closest('.navigation').hide();
            this.updateUserIdNavExpanded('settings', false);
        });

        settingsMenu.find('.menu').addClass('flexcolumn');
        $control.find('.navigation-menu').append(settingsMenu);

        // Remembering user last navigation column state
        setTimeout(() => {
            const userid = JSON.parse(sessionStorage.getItem('userid'));
            if (userid) {
                if (userid.settingsnavexpanded) {
                    if (userid.settingsnavexpanded === 'true') {
                        menuExpand.click()
                    } else {
                        menuCollapse.click();
                    }
                }
            }
        }, 0);

        // Clear 'X' button
        $control.find('.input-group-clear').on('click', e => {
            const $this = jQuery(e.currentTarget);
            const event = jQuery.Event("keyup", { which: 13 });
            $this.parent().find('#settingsSearch').val('').trigger(event);
        });
        // Search Input and icon
        $control.find('.input-group-search').on('click', e => {
            const $search = jQuery(e.currentTarget).parent().find('#settingsSearch');
            const event = jQuery.Event("keyup", { which: 13 });
            $search.trigger(event);
        });
        $control.find('#settingsSearch').on('change', e => {
            const $search = jQuery(e.currentTarget);
            const event = jQuery.Event("keyup", { which: 13 });
            $search.trigger(event);
        });
        $control.find('#settingsSearch').on('keyup', function (e) {
            const val = jQuery.trim(this.value).toUpperCase();
            const $searchClear = jQuery(this).parent().find('.input-group-clear');
            if ($searchClear.is(":hidden") && val !== '') {
                $searchClear.show();
            } else if (val === '') {
                $searchClear.hide();
            }
            if (e.which === 13) {
                var $settings, $module, $settingsTitles, $settingsDescriptions, filter, customFilter, sectionFilter;
                $control.find('.selected').removeClass('selected');

                filter = [];
                customFilter = [];
                sectionFilter = [];
                $settings = jQuery('#searchId');
                $settingsTitles = $control.find('a#title');
                $settingsDescriptions = jQuery('#description-text');
                $module = jQuery('.panel-group');
                $module.find('.highlighted').removeClass('highlighted');
                if (val === "") {
                    $module.show();
                } else {
                    var results = [];
                    results.push(val);
                    $searchClear.show();
                    //$settings.closest('div.panel-group').hide();
                    $module.hide();
                    for (var caption in me.screen.moduleCaptions) {
                        if (caption.indexOf(val) !== -1 || caption.indexOf(val.split(' ').join('')) !== -1) {
                            for (var moduleName in me.screen.moduleCaptions[caption]) {
                                if (me.screen.moduleCaptions[caption][moduleName][0].custom) {
                                    customFilter.push(me.screen.moduleCaptions[caption][moduleName][0]);
                                } else if (me.screen.moduleCaptions[caption][moduleName][0].data('type') === 'section') {
                                    sectionFilter.push(me.screen.moduleCaptions[caption][moduleName][0].data('caption'));
                                } else {
                                    filter.push(me.screen.moduleCaptions[caption][moduleName][0].data('datafield'));
                                }
                                results.push(moduleName.toUpperCase());
                            }
                        }
                    }
                    me.filter = filter;
                    me.sectionFilter = sectionFilter;
                    me.customFilter = customFilter;
                    me.searchValue = val;

                    var highlightSearch = function (element, search) {
                        let searchStrLen = search.length;
                        let startIndex = 0, index, indicies = [];
                        let htmlStringBuilder = [];
                        search = search.toUpperCase();
                        while ((index = element.textContent.toUpperCase().indexOf(search, startIndex)) > -1) {
                            indicies.push(index);
                            startIndex = index + searchStrLen;
                        }
                        for (var i = 0; i < indicies.length; i++) {
                            if (i === 0) {
                                htmlStringBuilder.push(jQuery(element).text().substring(0, indicies[0]));
                            } else {
                                htmlStringBuilder.push(jQuery(element).text().substring(indicies[i - 1] + searchStrLen, indicies[i]))
                            }
                            htmlStringBuilder.push('<span class="highlighted">' + jQuery(element).text().substring(indicies[0], indicies[0] + searchStrLen) + '</span>');
                            if (i === indicies.length - 1) {
                                htmlStringBuilder.push(jQuery(element).text().substring(indicies[i] + searchStrLen, jQuery(element).text().length));
                                element.innerHTML = htmlStringBuilder.join('');
                            }
                        }
                    }

                    var module: any = [];
                    for (var i = 0; i < results.length; i++) {
                        //check descriptions for match
                        for (var k = 0; k < $module.length; k++) {
                            // match results
                            if ($module.eq(k).attr('id').toUpperCase() === results[i]) {
                                module.push($module.eq(k)[0]);
                            }
                            // search titles
                            if ($settingsTitles.eq(k).text().toUpperCase().indexOf(val) !== -1) {
                                module.push($settingsTitles.eq(k).closest('.panel-group')[0]);
                            }
                            // search descriptions
                            if ($settingsDescriptions.eq(k).text().toUpperCase().indexOf(val) !== -1) {
                                module.push($settingsDescriptions.eq(k).closest('.panel-group')[0]);
                            }
                        }
                        module = jQuery(module);
                    }

                    let description = module.find('#description-text');
                    let title = module.find('a#title');

                    for (var j = 0; j < title.length; j++) {
                        if (title[j] !== undefined) {
                            let titleIndex = jQuery(title[j]).text().toUpperCase().indexOf(val);
                            let descriptionIndex = jQuery(description[j]).text().toUpperCase().indexOf(val);
                            if (descriptionIndex > -1) {
                                highlightSearch(description[j], val);
                            }
                            if (titleIndex > -1) {
                                highlightSearch(title[j], val);
                            }
                        }
                    }

                    if (module.length === 0) {
                        $settings.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().indexOf(results[i]);
                        }).closest('div.panel-group').show();
                    }
                    module.show().find('#searchId').hide();

                    let searchResults = $control.find('.panel-heading:visible');

                    if (searchResults.length === 1 && searchResults.parent().find('.panel-body.header-content').is(':empty')) {
                        searchResults[0].click();
                    }
                }
            }
            jQuery(this).focus();
        });
    };
    //----------------------------------------------------------------------------------------------
    updateUserIdNavExpanded(module: string, isExpanded: boolean) {
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        let request: any = {};
        if (userid) {
            if (module === 'settings') {
                userid.settingsnavexpanded = `${isExpanded}`;
                request.SettingsNavigationMenuVisible = isExpanded;
            } else {
                userid.reportsnavexpanded = `${isExpanded}`;
                request.ReportsNavigationMenuVisible = isExpanded;

            }
            sessionStorage.setItem('userid', JSON.stringify(userid));

            const webusersid = sessionStorage.getItem('webusersid');
            if (webusersid) {
                request.WebUserId = webusersid;
                FwAppData.apiMethod(true, 'PUT', `api/v1/userprofile/${webusersid}`, request, FwServices.defaultTimeout,
                    response => { },
                    ex => {
                        if (ex !== 'Forbidden') {
                            FwFunc.showError(ex)
                        }
                    }
                    , null);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    //saveForm(module, $form, closetab, navigationpath, $control, browseKeys, rowId, moduleName, getRows?) {
    //    var $tabpage, fields, ids, mode, isValid, $tab, request, controllername, controller;
    //    mode = $form.attr('data-mode');
    //    $tabpage = $form.parent();
    //    $tab = jQuery('#' + $tabpage.attr('data-tabid'));
    //    isValid = FwModule.validateForm($form);
    //    controllername = $form.attr('data-controller');
    //    controller = window[controllername];

    //    if (isValid) {
    //        ids = FwModule.getFormUniqueIds($form);
    //        fields = FwModule.getFormFields($form, false);
    //        if (typeof controller.apiurl !== 'undefined') {
    //            request = FwModule.getFormModel($form, false);
    //        } else {
    //            request = {
    //                module: module,
    //                mode: mode,
    //                ids: ids,
    //                fields: fields
    //            };
    //        }
    //        FwServices.module.method(request, module, 'Save', $form, response => {
    //            var $formfields, issubmodule, $browse;

    //            if (typeof controller.apiurl !== 'undefined' && browseKeys.length !== 0) {
    //                if (!closetab) {
    //                    var $body = $control.find('#' + moduleName + '.panel-body');

    //                    var html = [], $moduleRows;
    //                    html.push('<div class="panel-record">')
    //                    html.push('  <div class="panel panel-info container-fluid">');
    //                    html.push('    <div class="row-heading">');
    //                    for (var j = 0; j < browseKeys.length; j++) {
    //                        if (browseKeys.length === 1) {
    //                            html.push('      <label style="width:25%">' + browseKeys[j] + '</label>');
    //                            html.push('      <label style="width:25%">' + response[browseKeys[j]] + '</label>');
    //                            html.push('      <label style="width:25%"></label>');
    //                            html.push('      <label style="width:25%"></label>');
    //                        }
    //                        if (browseKeys.length === 2) {
    //                            html.push('      <label style="width:25%">' + browseKeys[j] + '</label>');
    //                            html.push('      <label style="width:25%">' + response[browseKeys[j]] + '</label>');
    //                        }
    //                        if (browseKeys.length === 3) {
    //                            html.push('      <label style="width:15%">' + browseKeys[j] + '</label>');
    //                            html.push('      <label style="width:15%">' + response[browseKeys[j]] + '</label>');
    //                        }
    //                    }
    //                    html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>');
    //                    html.push('    </div>');
    //                    html.push('  </div>');
    //                    html.push('  <div class="panel-body" style="display:none;" id="' + response[rowId] + '"></div>');
    //                    html.push('</div>');
    //                    $moduleRows = jQuery(html.join(''));
    //                    $moduleRows.data('recorddata', response);
    //                    $body.append($moduleRows);
    //                    //Refresh the browse window on saving a record.
    //                    $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
    //                    if ($browse.length > 0) {
    //                        FwBrowse.databind($browse);
    //                    }

    //                    //If form is submodule
    //                    //issubmodule = $form.parent().hasClass('submodule');
    //                    //if (!issubmodule) {
    //                    //    jQuery('#' + $form.parent().attr('data-tabid')).find('.caption').html(response.tabname);
    //                    //} else {
    //                    //    jQuery('#' + $form.parent().attr('data-tabid')).find('.form-caption').html(response.tabname);
    //                    //}

    //                    jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
    //                    if ($form.attr('data-mode') === 'NEW') {
    //                        $form.attr('data-mode', 'EDIT');
    //                        $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
    //                    } else {
    //                        $formfields = $form.data('fields');
    //                    }
    //                    FwFormField.loadForm($formfields, response);
    //                    $form.attr('data-modified', false);
    //                    if (typeof controller['afterLoad'] === 'function') {
    //                        controller['afterLoad']($form);
    //                    }
    //                    if (typeof controller['afterSave'] === 'function') {
    //                        controller['afterSave']($form);
    //                    }
    //                } else if (closetab) {
    //                    FwModule.closeFormTab($tab, $form);
    //                    if ((typeof navigationpath !== 'undefined') && (navigationpath !== '')) {
    //                        program.getModule(navigationpath);
    //                    }
    //                }
    //                FwNotification.renderNotification('SUCCESS', 'Record saved.');
    //            } else if (response.saved === true) {
    //                if (!closetab) {
    //                    //Refresh the browse window on saving a record.
    //                    $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
    //                    if ($browse.length > 0) {
    //                        FwBrowse.databind($browse);
    //                    }

    //                    //If form is submodule
    //                    issubmodule = $form.parent().hasClass('submodule');
    //                    if (!issubmodule) {
    //                        jQuery('#' + $form.parent().attr('data-tabid')).find('.caption').html(response.tabname);
    //                    } else {
    //                        jQuery('#' + $form.parent().attr('data-tabid')).find('.form-caption').html(response.tabname);
    //                    }

    //                    jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
    //                    if ($form.attr('data-mode') === 'NEW') {
    //                        $form.attr('data-mode', 'EDIT');
    //                        $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
    //                    } else {
    //                        $formfields = $form.data('fields');
    //                    }
    //                    FwFormField.loadForm($formfields, response.tables);
    //                    $form.attr('data-modified', false);
    //                    if (typeof controller['afterLoad'] === 'function') {
    //                        controller['afterLoad']($form);
    //                    }
    //                    if (typeof controller['afterSave'] === 'function') {
    //                        controller['afterSave']($form);
    //                    }
    //                } else if (closetab) {
    //                    FwModule.closeFormTab($tab, $form);
    //                    if ((typeof navigationpath !== 'undefined') && (navigationpath !== '')) {
    //                        program.getModule(navigationpath);
    //                    }
    //                }
    //                FwNotification.renderNotification('SUCCESS', 'Record saved.');
    //            } else if (response.saved == false) {
    //                if ((typeof response.message !== 'undefined') && (response.message != '')) {
    //                    FwNotification.renderNotification('ERROR', response.message);
    //                } else {
    //                    FwNotification.renderNotification('ERROR', 'There is an error on the form.');
    //                }
    //            }
    //            //emptyRows();
    //            //this.getRows();
    //        });
    //    }
    //};
    //----------------------------------------------------------------------------------------------
    getCaptions(screen) {
        const node = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Settings');
        //const node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');

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
            const settingsModules = FwApplicationTree.getChildrenByType(node, 'Module');
            for (let idx = 0; idx < settingsModules.length; idx++) {
                //let moduleName = settingsModules[idx].properties.controller.slice(0, -10);
                let moduleName = settingsModules[idx].caption;
                let $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                let $fwformfields = $form.find('.fwformfield[data-caption]');
                let $fwformsectionfields = $form.find('.fwform-section');
                for (let j = 0; j < $fwformfields.length; j++) {
                    let $field = $fwformfields.eq(j);
                    let caption = $field.attr('data-caption').toUpperCase();
                    if ($field.attr('data-type') === 'radio') {
                        let radioCaptions = $field.find('div');
                        for (let k = 0; k < radioCaptions.length; k++) {
                            let radioCaption = jQuery(radioCaptions[k]).attr('data-caption').toUpperCase()
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
                for (let k = 0; k < $fwformsectionfields.length; k++) {
                    let $section = $fwformsectionfields.eq(k);
                    let sectionCaption = $section.attr('data-caption').toUpperCase();
                    if (typeof screen.moduleCaptions[sectionCaption] === 'undefined') {
                        screen.moduleCaptions[sectionCaption] = {};
                    }
                    if (typeof screen.moduleCaptions[sectionCaption][moduleName] === 'undefined') {
                        screen.moduleCaptions[sectionCaption][moduleName] = [];
                    }
                    screen.moduleCaptions[sectionCaption][moduleName].push($section);
                }
            }
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    getRows($body, $control, apiurl, $modulecontainer, moduleName) {
        FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, response => {
            const browseKeys = [];
            const $browse = window[`${moduleName}Controller`].openBrowse();
            let browseData = [];
            let duplicateDatafields = {};
            let withoutDuplicates = [];

            let $form = jQuery(jQuery(`#tmpl-modules-${moduleName}Form`).html());
            const keys = $browse.find('.field');
            const rowId = jQuery(keys[0]).attr('data-browsedatafield');

            //append record search / legend
            if ($body.find('.search-bar').length <= 1) {
                $body.prepend(this.getRecordSearch());
                if ($browse.attr('data-hasinactive') !== 'true') {
                    $body.find('.search-bar .view-options').hide();
                }
            }
            const $legend = $browse.find('.legend');
            if ($legend.length > 0) {
                $body.find('.search-bar').after($legend);
            }

            for (var i = 1; i < keys.length; i++) {
                let Key;
                if (jQuery(keys[i]).attr('data-datafield')) {
                    Key = jQuery(keys[i]).attr('data-datafield');
                } else if (jQuery(keys[i]).attr('data-browsedatafield')) {
                    Key = jQuery(keys[i]).attr('data-browsedatafield');
                }
                const fieldData: any = {};
                if ($browse.find(`div[data-browsedatafield="${Key}"]`).data('cellcolor')) {
                    const cellColor = $browse.find(`div[data-browsedatafield="${Key}"]`).data('cellcolor');
                    fieldData['color'] = cellColor;
                }

                browseKeys.push(Key);
                if ($browse.find(`div[data-browsedatafield="${Key}"]`).data('caption') !== undefined) {
                    fieldData['caption'] = $browse.find(`div[data-browsedatafield="${Key}"]`).data('caption');
                } else {
                    fieldData['caption'] = Key;
                };

                fieldData['datatype'] = $browse.find(`div[data-browsedatafield="${Key}"]`).data('browsedatatype');
                fieldData['datafield'] = Key;
                browseData.push(fieldData);

                if (i === 1 && Key !== 'Inactive' || i === 2 && jQuery(keys[1]).attr('data-browsedatafield') === 'Inactive') {
                    for (let k = 0; k < response.length - 1; k++) {
                        for (let l = 0, sorted; l < response.length - 1; l++) {
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
                for (let j = 0; j < FwSettings.filter.length; j++) {
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

            for (let i = 0; i < response.length; i++) {
                response[i]['_Custom'].forEach((customField) => {
                    response[i][customField.FieldName] = customField.FieldValue
                });
                const html: Array<string> = [];
                html.push(`<div class="panel-record" id="${response[i][rowId]}">`);
                html.push(`  <div class="panel panel-info container-fluid">`);
                html.push(`    <div class="row-heading">`);
                html.push(`      <i class="material-icons record-selector">keyboard_arrow_down</i>`);

                for (let j = 0; j < browseData.length; j++) {
                    if (browseData[j]['caption'] === 'Inactive' && response[i][browseData[j]['caption']] === true) {
                        html[1] = '<div class="panel panel-info container-fluid" style="display:none;">';
                        html[2] = '<div class="inactive-panel row-heading" style="background-color:lightgray;">';
                    }
                    if (browseData[j]['caption'] !== 'Inactive' && browseData[j]['caption'] !== 'Color' && !browseData[j]['hidden']) {
                        html.push(`      <div style="width:100%;padding-left: inherit;">`);
                        html.push(`        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">`);
                        html.push(`          <label style="font-weight:800;">${browseData[j]['caption']}</label>`);
                        html.push(`        </div>`);

                        if (browseData[j]['datatype'] === 'checkbox') {
                            html.push(`        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="width:50px;">`);
                            if (response[i][browseData[j]['datafield']]) {
                                html.push(`<div class="checkboxwrapper"><input class="value" data-datafield="${browseData[j]['datafield']}" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" checked><label></label></div>`);
                            } else {
                                html.push(`<div class="checkboxwrapper"><input class="value" data-datafield="${browseData[j]['datafield']}" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;"><label></label></div>`);
                            }
                        } else {
                            if (browseData[j]['color'] && response[i][browseData[j]['color']] != null) {
                                html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow color" data-type="fieldrow" style="color:${response[i][browseData[j]['color']]};width:8em;white-space:nowrap;height: 0;display:flex;border-bottom: 20px solid transparent;border-top: 20px solid;">`);
                            } else {
                                html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">`);
                            }
                            html.push(`    <label data-datafield="${browseData[j]['datafield']}" style="color:#31708f">${response[i][browseData[j]['datafield']]}</label>`);
                        }
                        html.push(`        </div>`);
                        html.push(`      </div>`);
                        //if (browseKeys[j] === 'Inactive' && response[i][browseKeys[j]] === true) {
                        //    html.push('</div>');
                        //}
                    }
                }
                //html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>');
                html.push(`    </div>`);
                html.push(`  </div>`);
                html.push(`  <div class="panel-body data-panel" style="display:none;" id="${response[i][rowId]}" data-type="settings-row"></div>`);
                html.push(`</div>`);
                const $moduleRows = jQuery(html.join(''));
                $moduleRows.data('recorddata', response[i]);
                $moduleRows.data('browsedata', browseData);
                $body.append($moduleRows);
                $body.find('.recordSearch').focus();
            }

            $body.find('.recordSearch')
                .on('keypress', function (e) {
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
                });
            $control
                .on('click', '.row-heading', function (e) {
                    e.stopPropagation();
                    const recordData = jQuery(this).parent().parent().data('recorddata');
                    const $rowBody = $control.find('#' + recordData[moduleName + 'Id'] + '.panel-body');
                    let $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                    const controller = $form.data('controller');
                    if ($rowBody.is(':empty')) {
                        $form = (<any>window[controller]).openForm('EDIT');
                        $form.find('[data-type="RefreshMenuBarButton"]').remove();
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
        }, null, $modulecontainer);
    }
    //----------------------------------------------------------------------------------------------
    newRow($body, $control, apiurl, $modulecontainer, moduleName, $modules) {
        let $form = jQuery(jQuery(`#tmpl-modules-${moduleName}Form`).html());
        if ($body.is(':empty')) {
            this.getRows($body, $control, apiurl, $modulecontainer, moduleName);
        }

        if ($modules.find('.panel-collapse').css('display') === 'none') {
            $modules.find('.arrow-selector').html('keyboard_arrow_up');
            $modules.find('.panel-collapse').show("fast");
        }

        const caption = $control.find(`#${moduleName}`).attr('data-caption');
        const newRowHtml: Array<string> = [];
        if ($body.find('.new-row').length === 0) {
            newRowHtml.push('<div class="new-row">');
            newRowHtml.push('  <div class="panel-record">');
            newRowHtml.push('    <div class="panel panel-info container-fluid">');
            newRowHtml.push('      <div class="new-row-heading">');
            newRowHtml.push(`        <label style="width:100%">New ${caption}</label>`);
            newRowHtml.push('        <div class="pull-right close-new-row" style="padding-right:20px;"><i class="material-icons cancel">clear</i>Cancel</div>');
            newRowHtml.push('      </div>');
            newRowHtml.push('    </div>');
            newRowHtml.push('  </div>');
            newRowHtml.push('</div>');

            const controller = $form.data('controller');
            $form = (<any>window[controller]).openForm('NEW');
            $form.find('[data-type="RefreshMenuBarButton"]').remove();
            $form.data('caption', caption)
            const $wrappedForm = jQuery(`<div data-type="settings-row"></div>`);

            $wrappedForm.prepend($form)
            $body.prepend($wrappedForm);
            $body.prepend(jQuery(newRowHtml.join('')));
            $body.prepend($body.find('.legend'));
            $body.prepend($body.find('.search-bar'));
        }

        $body.on('click', '.close-new-row', e => {
            const $this = jQuery(e.currentTarget);
            e.stopPropagation();
            const newRow = $body.find('.new-row');
            const $form = newRow.next('div[data-type=settings-row]');
            newRow.remove();
            $form.remove();
        });

    }
    //----------------------------------------------------------------------------------------------
    renderModuleHtml($control, title, moduleName, description, menu, menuCaption, moduleId) {
        var html = [], $settingsPageModules, $rowBody, $modulecontainer, apiurl, $body, $form, browseKeys = [], rowId, filter = [], me = this;
        let showNew = false;
        let showDelete = false;
        let showEdit = false;
        let hasDownloadExcel = false;

        $modulecontainer = $control.find('#' + moduleName);
        const controllerName = moduleName + 'Controller';
        const module = <IModule>window[controllerName];
        if (typeof module !== 'undefined' && typeof module.apiurl === 'string') {
            apiurl = module.apiurl;
            $form = jQuery(jQuery(`#tmpl-modules-${moduleName}Form`).html());
            $body = $control.find(`#${moduleName}.panel-body`);
            const browseMenuOptions = FwModule.getDefaultBrowseMenuOptions($modulecontainer);
            if (typeof module.addBrowseMenuItems === 'function') {
                module.addBrowseMenuItems(browseMenuOptions);
            }

            let controller = moduleName + 'Controller';
            let nodeModule = null, nodeModuleActions = null;
            if (window[controller] !== null) {
                nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, window[controller].id);
            }
            if (nodeModule !== null && nodeModule.properties.visible === 'T') {
                nodeModuleActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return node.nodetype === 'ModuleActions';
                });
            }
            if (nodeModuleActions !== null && nodeModuleActions.properties.visible === 'T') {
                const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'New';
                });
                const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'Edit';
                });
                const nodeSave = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'Save';
                });
                const nodeDelete = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return node.nodetype === 'ModuleAction' && node.properties.action === 'Delete';
                });
                showNew = browseMenuOptions.hasNew && ((nodeNew !== null && nodeNew.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'));
                showEdit = browseMenuOptions.hasEdit && ((nodeEdit !== null && nodeEdit.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'));
                showDelete = browseMenuOptions.hasDelete && (nodeDelete !== null && nodeDelete.properties.visible === 'T');
                hasDownloadExcel = browseMenuOptions.hasDownloadExcel;

                html.push(`<div class="panel-group" id="${moduleName}" data-id="${moduleId}" data-navigation="${menuCaption}" data-caption="${title}" data-showDelete=${showDelete.toString()} data-showEdit="${showEdit.toString()}">`);
                html.push('  <div class="panel panel-primary">');
                html.push(`    <div data-toggle="collapse" data-target="${moduleName}" href="${moduleName}" class="panel-heading">`);
                html.push('      <div style="display:flex;max-width:none;">');
                html.push('        <i class="material-icons arrow-selector">keyboard_arrow_down</i>');
                html.push('        <h4 class="panel-title">');

                if (menu !== title) {
                    html.push(`        <a id="title" data-toggle="collapse">${menu} - ${title}</a>`);
                } else {
                    html.push(`        <a id="title" data-toggle="collapse">${title}</a>`);
                }

                html.push('        <div id="myDropdown" class="dropdown-content">');
                html.push('        <div class="flexcolumn">');
                if (showNew) {
                    html.push(`         <div class="flexrow new-row-menu" data-caption="${title}"><i class="material-icons">add</i>New Item</div>`);
                }
                html.push('          <div class="pop-out flexrow"><i class="material-icons">open_in_new</i>Pop Out Module</div>');

                if (hasDownloadExcel) {
                    html.push('          <div class="download-excel flexrow"><i class="material-icons">cloud_download</i>Download Excel</div>');
                    html.push('          <div class="upload-excel flexrow"><i class="material-icons">cloud_upload</i>Upload Excel</div>');
                }

                html.push('        </div>');
                html.push('        </div>');
                html.push('        <div class="panel-icons" style="margin-left:auto;">');
                if (showNew) {
                    html.push('          <i class="material-icons new-row-menu" title="Add New">add</i>');
                }
                html.push('          <i class="material-icons pop-out" title="Pop Out">open_in_new</i>');
                html.push('          <i class="material-icons refresh" title="Refresh">cached</i>');
                html.push('          <i class="material-icons heading-menu">more_vert</i>');
                html.push('        </div>');
                html.push('        </h4>');
                html.push('      </div>');
                //if (description === "") {
                //    html.push('      <small id="searchId" style="display:none;">' + moduleName + '</small>');
                //    html.push('      <small style="margin:0 0 0 32px;" id="description-text">' + moduleName + '</small>');
                //} else {
                //    html.push('      <small id="searchId" style="display:none;">' + moduleName + '</small>');
                //    html.push('      <small style="margin:0 0 0 32px;" id="description-text">' + description + '</small>');
                //}

                html.push(`      <small id="searchId" style="display:none;">${moduleName}</small>`);
                if (description) {
                    html.push(`      <div style="margin:0 0 0 32px;font-size:.9em;" id="description-text">${description}</div>`);
                }

                html.push('    </div>');
                html.push(`    <div class="panel-collapse collapse" style="display:none; "><div class="panel-body header-content" id="${moduleName}"></div></div>`);
                html.push('  </div>');
                html.push('</div>');
                $settingsPageModules = jQuery(html.join(''));

                $control.find('.well').append($settingsPageModules);

                $settingsPageModules.on('click', '.new-row-menu', e => {
                    e.stopPropagation();
                    const $this = jQuery(e.currentTarget);
                    if ($this.parent().find('.hidden').length === 0 && $this.closest('#myDropdown').length !== 0) {
                        $this.parent().find('div[data-mode="NEW"]').addClass('hidden').hide('fast');
                        $this.closest('#myDropdown').hide();
                    } else {
                        $this.parent().find('div[data-mode="NEW"]').removeClass('hidden').show('fast');
                        $this.closest('#myDropdown').hide();
                    }
                    $body = $control.find(`#${moduleName}.panel-body`);
                    this.newRow($body, $control, apiurl, $modulecontainer, moduleName, $settingsPageModules);
                });

                $settingsPageModules.on('click', '.view-options', e => {
                    e.stopPropagation();
                    const $this = jQuery(e.currentTarget);
                    if ($this.find('.show-inactive').is(':visible')) {
                        $this.closest('.panel').find('.inactive-panel').parent().show();
                        $this.closest('.view-options').find('.show-inactive').hide();
                        $this.closest('.view-options').find('.hide-inactive').show();
                        $this.closest('.view-options').find('.view-text').text('Hide Inactive');
                    } else {
                        $this.closest('.panel').find('.inactive-panel').parent().hide();
                        $this.closest('.view-options').find('.hide-inactive').hide();
                        $this.closest('.view-options').find('.show-inactive').show();
                        $this.closest('.view-options').find('.view-text').text('Show Inactive');
                    }
                });

                $settingsPageModules.on('click', '.pop-out', e => {
                    e.stopPropagation();
                    const $this = jQuery(e.currentTarget);
                    if ($this.closest('#myDropdown').length !== 0) {
                        $this.closest('#myDropdown').hide();
                    }
                    program.popOutTab('#/module/' + moduleName);
                });
                // Download Excel
                $settingsPageModules.on('click', '.download-excel', e => {
                    try {
                        e.stopPropagation();
                        const $this = jQuery(e.currentTarget);
                        const $browse = window[`${moduleName}Controller`].openBrowse();
                        $this.append($browse);
                        $browse.css('display', 'none');
                        FwBrowse.databind($browse)
                            .then(() => {
                                FwBrowse.downloadExcelWorkbook($browse, controller);
                            })
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                // Import Excel
                $settingsPageModules.on('click', '.upload-excel', e => {
                    e.stopPropagation();
                    const $this = jQuery(e.currentTarget);
                    const $browse = window[`${moduleName}Controller`].openBrowse();
                    $this.append($browse);
                    $browse.css('display', 'none');
                    try {
                        FwBrowse.importExcelFromBrowse($browse, controller);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });

                $settingsPageModules
                    .on('click', '.panel-heading', e => {
                        var browseData = [], browseKeys = [], $form;

                        const moduleName = $settingsPageModules.closest('.panel-group').attr('id');
                        const $browse = window[moduleName + 'Controller'].openBrowse();
                        const $modulecontainer = $control.find('#' + moduleName);
                        const apiurl = window[moduleName + 'Controller'].apiurl;
                        const $body = $control.find('#' + moduleName + '.panel-body');
                        const duplicateDatafields: any = {};
                        var withoutDuplicates = [];

                        if ($body.is(':empty')) {
                            //append record search / legend
                            if ($body.find('.search-bar').length <= 0) {
                                $body.append(this.getRecordSearch());
                                if ($browse.attr('data-hasinactive') !== 'true') {
                                    $body.find('.search-bar .view-options').hide();
                                }
                            }
                            if ($browse.find('.legend').length > 0) {
                                $body.append($browse.find('.legend'));
                            }

                            FwAppData.apiMethod(true, 'GET', apiurl, null, null, function onSuccess(response) {
                                $form = jQuery(jQuery(`#tmpl-modules-${moduleName}Form`).html());
                                const keys = $browse.find('.field');
                                const rowId = jQuery(keys[0]).attr('data-browsedatafield');

                                for (var i = 1; i < keys.length; i++) {
                                    let Key;
                                    if (jQuery(keys[i]).attr('data-datafield')) {
                                        Key = jQuery(keys[i]).attr('data-datafield');
                                    } else if (jQuery(keys[i]).attr('data-browsedatafield')) {
                                        Key = jQuery(keys[i]).attr('data-browsedatafield');
                                    }
                                    const fieldData: any = {};
                                    if ($browse.find(`div[data-browsedatafield="${Key}"]`).data('cellcolor')) {
                                        const cellColor = $browse.find(`div[data-browsedatafield="${Key}"]`).data('cellcolor');
                                        fieldData['color'] = cellColor;
                                    }
                                    browseKeys.push(Key);
                                    if ($browse.find(`div[data-browsedatafield="${Key}"]`).data('caption') !== undefined) {
                                        fieldData['caption'] = $browse.find(`div[data-browsedatafield="${Key}"]`).data('caption');
                                    } else {
                                        fieldData['caption'] = Key;
                                    };

                                    fieldData['datatype'] = $browse.find(`div[data-browsedatafield="${Key}"]`).data('browsedatatype');
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

                                if (FwSettings.filter.length > 0) {
                                    var uniqueFilters = [];
                                    for (let j = 0; j < FwSettings.filter.length; j++) {
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
                                // remove duplicated fields
                                browseData.forEach(function (browseField) {
                                    if (!duplicateDatafields[browseField.datafield]) {
                                        withoutDuplicates.push(browseField);
                                        duplicateDatafields[browseField.datafield] = true;
                                    }
                                });
                                browseData = withoutDuplicates;

                                for (let i = 0; i < response.length; i++) {
                                    const html: Array<string> = [];
                                    response[i]['_Custom'].forEach((customField) => {
                                        response[i][customField.FieldName] = customField.FieldValue
                                    });

                                    html.push(`<div class="panel-record" id="${response[i][rowId]}">`);
                                    html.push(`  <div class="panel panel-info container-fluid">`);
                                    html.push(`    <div class="row-heading">`);
                                    html.push(`      <i class="material-icons record-selector">keyboard_arrow_down</i>`);

                                    for (let j = 0; j < browseData.length; j++) {
                                        if (browseData[j]['caption'] === 'Inactive' && response[i][browseData[j]['caption']] === true) {
                                            html[1] = '<div class="panel panel-info container-fluid" style="display:none;">';
                                            html[2] = '<div class="inactive-panel row-heading" style="background-color:lightgray;">';
                                        }
                                        if (browseData[j]['caption'] !== 'Inactive' && browseData[j]['caption'] !== 'Color' && !browseData[j]['hidden']) {
                                            html.push('      <div style="width:100%;padding-left: inherit;">');
                                            html.push('        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">');
                                            html.push(`          <label style="font-weight:800;">${browseData[j]['caption']}</label>`);
                                            html.push('        </div>');

                                            if (browseData[j]['datatype'] === 'checkbox') {
                                                html.push(`        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="width:50px;">`);
                                                const checkbox = response[i][browseData[j]['datafield']];
                                                if (response[i][browseData[j]['datafield']]) {
                                                    html.push(`<div class="checkboxwrapper"><input class="value" data-datafield="${browseData[j]['datafield']}" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" checked><label></label></div>`);
                                                } else {
                                                    html.push(`<div class="checkboxwrapper"><input class="value" data-datafield="${browseData[j]['datafield']}" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;"><label></label></div>`);
                                                }
                                            } else {
                                                const color = response[i][browseData[j]['color']];
                                                if (browseData[j]['color'] && color !== '' && color != null) {
                                                    html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow color" data-type="fieldrow" style="color:${response[i][browseData[j]['color']]};width:8em;white-space:nowrap;height: 0;display:flex;border-bottom: 20px solid transparent;border-top: 20px solid;">`);
                                                } else {
                                                    html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">`);
                                                }
                                                html.push(`    <label data-datafield="${browseData[j]['datafield']}" style="color:#31708f">${response[i][browseData[j]['datafield']]}</label>`);
                                            }
                                            html.push(`        </div>`);
                                            html.push(`      </div>`);
                                            //if (browseKeys[j] === 'Inactive' && response[i][browseKeys[j]] === true) {
                                            //    html.push('</div>');
                                            //}
                                        }
                                    }
                                    //html.push('      <div class="pull-right save"><i class="material-icons">save</i>Save</div>');
                                    html.push('    </div>');
                                    html.push('  </div>');
                                    html.push(`  <div class="panel-body data-panel" style="display:none;" id="${response[i][rowId]}" data-type="settings-row"></div>`);
                                    html.push('</div>');
                                    const $moduleRows = jQuery(html.join(''));
                                    $moduleRows.data('recorddata', response[i]);
                                    $moduleRows.data('browsedata', browseData);
                                    $body.append($moduleRows);
                                    $body.find('.recordSearch').focus();
                                }

                                $body.find('.recordSearch').on('keypress', function (e) {
                                    if (e.which === 13) {
                                        let dataKeys = [];
                                        let query = jQuery.trim(this.value).toUpperCase();
                                        let matches = [];
                                        let $panelBody = jQuery(this).closest('.panel-body')
                                        for (let key in response[0]) {
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
                        let activeMenu = $control.find('.active-menu');
                        const $this: any = jQuery(this);
                        if ($this.parent().prev().css('display') === 'none') {
                            $this.parent().prev().css('display', 'block').addClass('active-menu');
                            jQuery(document).one('click', function closeMenu(e) {
                                if ($this.has(e.target).length === 0) {
                                    $this.parent().prev().removeClass('active-menu').css('display', 'none');
                                } else {
                                    $this.parent().prev().css('display', 'block');
                                }
                            })
                        } else {
                            $this.parent().prev().removeClass('active-menu').css('display', 'none');
                        }

                        //if (activeMenu.length > 0) {
                        //    activeMenu.removeClass('active-menu').hide();
                        //}
                    })
                    .on('click', '.refresh', e => {
                        e.stopPropagation();
                        const $body = $control.find(`#${moduleName}.panel-body`);
                        if ($body.find('.view-options .hide-inactive').length !== 0) {
                            $body.find('.view-options .hide-inactive').hide()
                            $body.find('.view-options .show-inactive').show();
                            $body.find('.view-options .view-text').text('Show Inactive');
                        }
                        if (!$body.is(':empty')) {
                            $body.empty();
                            this.getRows($body, $control, apiurl, $modulecontainer, moduleName);
                        }
                        if ($settingsPageModules.find('.panel-collapse').css('display') === 'none') {
                            $body.empty();
                            this.getRows($body, $control, apiurl, $modulecontainer, moduleName);
                            $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_up')
                            $settingsPageModules.find('.panel-collapse').show("fast");
                        }
                    });

                $control
                    .unbind().on('click', '.row-heading', e => {
                        e.stopPropagation();
                        const $this = jQuery(e.currentTarget);
                        const recordData = $this.parent().parent().data('recorddata');
                        const moduleName: any = $this.closest('div.panel-group')[0].id;
                        let $form = jQuery(jQuery(`#tmpl-modules-${moduleName}Form`).html());
                        const moduleId = jQuery($form.find('.fwformfield[data-isuniqueid="true"]')[0]).data('datafield');
                        const uniqueids: any = {};
                        uniqueids[moduleId] = recordData[moduleId];
                        const $rowBody = $this.closest(`#${recordData[moduleId]}.panel-record`).find(`#${recordData[moduleId]}.panel-body`);
                        // const $rowBody = $control.find(`#${recordData[moduleId]}.panel-body`);
                        const controller = $form.data('controller');

                        if ($rowBody.is(':empty')) {
                            $form = (<any>window[controller]).openForm('EDIT');
                            $form.find('[data-type="RefreshMenuBarButton"]').remove(); // remove refresh btn
                            $rowBody.append($form);
                            const $formSections = $form.find('.fwform-section-title');
                            $form.find('.highlighted').removeClass('highlighted');
                            $form.find('div[data-type="NewMenuBarButton"]').off();
                            if (jQuery(e.currentTarget).closest('.panel-group').attr('data-showDelete') === 'true') {
                                $form.find('div.fwmenu.default > .buttonbar').append('<div class="btn-delete" data-type="DeleteMenuBarButton"><i class="material-icons"></i><div class="btn-text">Delete</div></div>');
                            }

                            for (let key in recordData) {
                                for (let i = 0; i < this.filter.length; i++) {
                                    if (this.filter[i] === key) {
                                        const highlightField = $form.find(`[data-datafield="${key}"]`);
                                        const hightlightFieldTabId = highlightField.closest('.tabpage').attr('data-tabid');
                                        if ($form.find(`[data-datafield="${key}"]`).attr('data-type') === 'checkbox') {
                                            $form.find(`[data-datafield="${key}"] label`).addClass('highlighted');
                                        } else {
                                            highlightField.find('.fwformfield-caption').addClass('highlighted');
                                            highlightField.parents('.fwtabs .fwcontrol').find(`#${hightlightFieldTabId}`).addClass('highlighted');
                                        }
                                    }
                                };
                                const value = recordData[key];
                                const $field = $form.find(`[data-datafield="${key}"]`);
                                const displayfield = $field.attr('data-displayfield');
                                if ($field.length > 0) {
                                    if (typeof displayfield !== 'undefined' && typeof recordData[displayfield] !== 'undefined') {
                                        const text = recordData[displayfield];
                                        FwFormField.setValue($form, `[data-datafield="${key}"]`, value, text);
                                    } else {
                                        FwFormField.setValue($form, `[data-datafield="${key}"]`, value);
                                    }
                                }
                            }
                            for (let i = 0; i < $formSections.length; i++) {
                                for (let k = 0; k < this.sectionFilter.length; k++) {
                                    const sectionCaption = jQuery($formSections[i]).html();
                                    if (jQuery($formSections[i]).html() === me.sectionFilter[i]) {
                                        const startIndex = sectionCaption.toUpperCase().indexOf(this.searchValue);
                                        const endIndex = startIndex + this.searchValue.length;
                                        jQuery($formSections[i]).html(sectionCaption.substring(0, startIndex) + '<span class="highlighted">' + sectionCaption.substring(startIndex, endIndex) + '</span>' + sectionCaption.substring(endIndex));
                                    }
                                }

                            }
                            FwModule.loadForm(moduleName, $form);
                            if (jQuery(e.currentTarget).closest('.panel-group').attr('data-showEdit') === 'false') {
                                FwModule.setFormReadOnly($form);
                            }

                            FwControl.loadControls($form.find('.fwcontrol'));

                            //if ($form.find('.fwappimage')[0]) {
                            //    FwAppImage.getAppImages($form.find('.fwappimage'))  // - 12/16/19 J. Pace Moved into this scope to prvent undefined error in sortable library
                            //}

                            //if (typeof window[moduleName + 'Controller']['afterLoad'] === 'function') {  // - 01/07/2020 Jason H - afterLoad is already being called in FwModule.loadForm above. 
                            //    window[moduleName + 'Controller']['afterLoad']($form);
                            //}  
                        }


                        $form.data('afterLoadCustomFields', () => {
                            for (let key in recordData) {
                                let value;
                                for (let i = 0; i < this.filter.length; i++) {
                                    if (this.filter[i] === key) {
                                        $form.find(`[data-datafield="${key}"]`).find('.fwformfield-caption').css({ 'background': 'yellow' });
                                    }
                                };
                                if (key === '_Custom') {
                                    value = recordData[key][0]['FieldValue'];
                                    key = recordData[key][0]['FieldName'];
                                } else {
                                    value = recordData[key];
                                }
                                const $field = $form.find(`[data-datafield="${key}"]`);
                                const displayfield = $field.attr('data-displayfield');
                                if ($field.length > 0) {
                                    if (typeof displayfield !== 'undefined' && typeof recordData[displayfield] !== 'undefined') {
                                        const text = recordData[displayfield];
                                        FwFormField.setValue($form, `[data-datafield="${key}"]`, value, text);
                                    } else {
                                        FwFormField.setValue($form, `[data-datafield="${key}"]`, value);
                                    }
                                }
                            }
                        });

                        if ($rowBody.css('display') === 'none' || $rowBody.css('display') === undefined) {
                            $rowBody.parent().find('.record-selector').html('keyboard_arrow_up');
                            $rowBody.show("fast");
                        } else {
                            $rowBody.parent().find('.record-selector').html('keyboard_arrow_down');
                            $rowBody.hide("fast");
                        }
                    })
                    .on('click', '.btn[data-type="SaveMenuBarButton"]', e => {
                        const browsedata = jQuery(e.currentTarget).closest('.panel-record').data('browsedata');
                        const browsedatafields = [];
                        const record = jQuery(e.currentTarget).closest('.panel-record').find('.panel-info');
                        const $form = jQuery(e.currentTarget).closest('.fwform');
                        moduleName = jQuery(e.currentTarget).closest('.panel-group').attr('id');

                        // For new record without a panel
                        if ($form.attr('data-mode') === 'NEW') {
                            setTimeout(() => {
                                //const saveRes = await FwModule.saveForm(moduleName, $form, { closetab: false });                    // temporary solution. Need more reliable - J. Pace 9/16/19
                                const tempSaveResponse = $form.data('SaveFormAPIresponse');
                                if (tempSaveResponse) {
                                    this.getPanelForNew($control, $form, moduleName, tempSaveResponse);
                                }
                            }, 800)
                        }
                        // existing record with a save event
                        if (typeof browsedata !== 'undefined') {
                            for (let i = 0; i < browsedata.length; i++) {
                                browsedatafields.push(browsedata[i].datafield);
                            }
                            for (let i = 0; i < browsedatafields.length; i++) {
                                if (jQuery(e.currentTarget).closest('.panel-record').find('.panel-info').find(`label[data-datafield="${browsedatafields[i]}"]`)) {
                                    // check if field is valid or else may be a validation
                                    if ($form.find(`div[data-datafield="${browsedatafields[i]}"]`).length > 0) {
                                        if (browsedatafields[i] === 'Inactive') {
                                            if (FwFormField.getValueByDataField($form, browsedatafields[i])) {
                                                jQuery(e.currentTarget).closest('.panel-record').find('.row-heading').addClass('inactive-panel').css('background-color', 'lightgray');
                                            } else {
                                                jQuery(e.currentTarget).closest('.panel-record').find('.row-heading').removeClass('inactive-panel').css('background-color', '#d9edf7');
                                            }
                                        } else {
                                            jQuery(e.currentTarget).closest('.panel-record').find('.panel-info').find(`label[data-datafield="${browsedatafields[i]}"]`).text(FwFormField.getValueByDataField($form, browsedatafields[i]));
                                            jQuery(e.currentTarget).closest('.panel-record').find('.panel-info').find(`[data-datafield="${browsedatafields[i]}"]`).prop('checked', FwFormField.getValueByDataField($form, browsedatafields[i]));
                                            if (jQuery(e.currentTarget).closest('.panel-record').find('.panel-info').find(`label[data-datafield="${browsedatafields[i]}"]`).parent().hasClass('color')) {
                                                const newColor: any = $form.find('div[data-type="color"] input').val();
                                                jQuery(e.currentTarget).closest('.panel-record').find('.panel-info').find(`label[data-datafield="${browsedatafields[i]}"]`).parent().css('color', newColor)
                                            }
                                        }
                                    } else {
                                        const validationValue: any = $form.find(`div[data-displayfield="${browsedatafields[i]}"] input.fwformfield-text`).val()
                                        jQuery(e.currentTarget).closest('.panel-record').find('.panel-info').find(`label[data-datafield="${browsedatafields[i]}"]`).text(validationValue);
                                    }
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
                    let ids: any = {};
                    let $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Delete this record?');
                    let $yes = FwConfirmation.addButton($confirmation, 'Yes');
                    FwConfirmation.addButton($confirmation, 'No');
                    $yes.focus();
                    $yes.on('click', function () {
                        const controller = $form.data('controller');
                        ids = FwModule.getFormUniqueIds($form);
                        let request = {
                            module: (<any>window[controller]).Module,
                            ids: ids
                        };
                        try {
                            FwServices.module.method(request, (<any>window[controller]).Module, 'Delete', $form, function (response) {
                                $form = FwModule.getFormByUniqueIds(ids);
                                if ((typeof $form != 'undefined') && ($form.length > 0)) {
                                    $form.closest('.panel-record').remove();
                                }
                                FwNotification.renderNotification('SUCCESS', 'Record deleted.');
                            }, null);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                });
            }
        }

        return $settingsPageModules;
    }
    //----------------------------------------------------------------------------------------------
    getPanelForNew($control, $form, moduleName, saveData: any) {
        const $browse = window[`${moduleName}Controller`].openBrowse();
        const duplicateDatafields = {};
        const withoutDuplicates = [];
        let browseData = [];
        const browseKeys = [];
        const $body = $control.find(`#${moduleName}.panel-body`);
        const keys = $browse.find('.field');
        const rowId = jQuery(keys[0]).attr('data-browsedatafield');
        for (let i = 1; i < keys.length; i++) {
            let Key;
            if (jQuery(keys[i]).attr('data-datafield')) {
                Key = jQuery(keys[i]).attr('data-datafield');
            } else if (jQuery(keys[i]).attr('data-browsedatafield')) {
                Key = jQuery(keys[i]).attr('data-browsedatafield');
            }
            const fieldData: any = {};
            if ($browse.find(`div[data-browsedatafield="${Key}"]`).data('cellcolor')) {
                const cellColor = $browse.find(`div[data-browsedatafield="${Key}"]`).data('cellcolor');
                fieldData['color'] = cellColor;
            }

            browseKeys.push(Key);
            if ($browse.find(`div[data-browsedatafield="${Key}"]`).data('caption') !== undefined) {
                fieldData['caption'] = $browse.find(`div[data-browsedatafield="${Key}"]`).data('caption');
            } else {
                fieldData['caption'] = Key;
            };

            fieldData['datatype'] = $browse.find(`div[data-browsedatafield="${Key}"]`).data('browsedatatype');
            fieldData['datafield'] = Key;
            browseData.push(fieldData);

            //if (i === 1 && Key !== 'Inactive' || i === 2 && jQuery(keys[1]).attr('data-browsedatafield') === 'Inactive') {
            //    for (var k = 0; k < response.length - 1; k++) {
            //        for (var l = 0, sorted; l < response.length - 1; l++) {
            //            if (response[l][Key].toLowerCase() > response[l + 1][Key].toLowerCase()) {
            //                sorted = response[l + 1];
            //                response[l + 1] = response[l];
            //                response[l] = sorted;
            //            }
            //        }
            //    }
            //}
        };

        if (this.filter.length > 0) {
            const uniqueFilters = [];
            for (let i = 0; i < this.filter.length; i++) {
                if (uniqueFilters.indexOf(this.filter[i]) === -1) {
                    uniqueFilters.push(this.filter[i]);
                }
            }


            for (let i = 0; i < uniqueFilters.length; i++) {
                const filterField = $form.find(`div[data-datafield="${uniqueFilters[i]}"]`);
                if (filterField.length > 0 && filterField.attr('data-type') !== 'key') {
                    const filterData: any = {};
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
        if (this.customFilter.length > 0) {
            const uniqueCustomFilter = [];
            for (let i = 0; i < this.customFilter.length; i++) {
                if (uniqueCustomFilter.indexOf(this.customFilter[i]) === -1) {
                    uniqueCustomFilter.push(this.customFilter[i]);
                }
            }
            for (let i = 0; i < uniqueCustomFilter.length; i++) {
                if (uniqueCustomFilter[i].module == $form.data('controller').slice(0, -10)) {
                    browseData.push(uniqueCustomFilter[i]);
                    browseKeys.push(uniqueCustomFilter[i].datafield);
                }
            }
        }
        // remove duplicated fields
        browseData.forEach(browseField => {
            if (!duplicateDatafields[browseField.datafield]) {
                withoutDuplicates.push(browseField);
                duplicateDatafields[browseField.datafield] = true;
            }
        });
        browseData = withoutDuplicates;

        //response[i]['_Custom'].forEach((customField) => {
        //    response[i][customField.FieldName] = customField.FieldValue
        //});
        const html: Array<string> = [];
        let inactiverecord = false;
        html.push(`<div class="panel-record" id="${saveData[rowId]}">`);
        html.push(`  <div class="panel panel-info container-fluid">`);
        html.push(`    <div class="row-heading">`);
        html.push(`      <i class="material-icons record-selector">keyboard_arrow_up</i>`);

        for (let i = 0; i < browseData.length; i++) {
            if (browseData[i]['caption'] === 'Inactive' && saveData[browseData[i]['caption']] === true) {
                inactiverecord = true;
                html[1] = '<div class="panel panel-info container-fluid" style="display:none;">';
                html[2] = '<div class="inactive-panel row-heading" style="background-color:lightgray;">';
            }
            if (browseData[i]['caption'] !== 'Inactive' && browseData[i]['caption'] !== 'Color' && !browseData[i]['hidden']) {
                html.push(`      <div style="width:100%;padding-left: inherit;">`);
                html.push(`        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">`);
                html.push(`          <label style="font-weight:800;">${browseData[i]['caption']}</label>`);
                html.push(`        </div>`);

                if (browseData[i]['datatype'] === 'checkbox') {
                    html.push(`        <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow" style="width:50px;">`);
                    if (saveData[browseData[i]['datafield']]) {
                        html.push(`<div class="checkboxwrapper"><input class="value" data-datafield="${browseData[i]['datafield']}" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;" checked><label></label></div>`);
                    } else {
                        html.push(`<div class="checkboxwrapper"><input class="value" data-datafield="${browseData[i]['datafield']}" type="checkbox" disabled="disabled" style="box-sizing:border-box;pointer-events:none;"><label></label></div>`);
                    }
                } else {
                    if (browseData[i]['color'] && saveData[browseData[i]['color']] != null) {
                        html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow color" data-type="fieldrow" style="color:${saveData[browseData[i]['color']]};width:8em;white-space:nowrap;height: 0;display:flex;border-bottom: 20px solid transparent;border-top: 20px solid;">`);
                    } else {
                        html.push(`    <div class="fwcontrol fwcontainer fwform-fieldrow" data-type="fieldrow">`);
                    }
                    html.push(`    <label data-datafield="${browseData[i]['datafield']}" style="color:#31708f">${saveData[browseData[i]['datafield']]}</label>`);
                }
                html.push(`        </div>`);
                html.push(`      </div>`);
            }
        }
        html.push(`    </div>`);
        html.push(`  </div>`);
        html.push(`  <div class="panel-body data-panel" style="display:none;" id="${saveData[rowId]}" data-type="settings-row"></div>`);
        html.push(`</div>`);

        const $newPanel = jQuery(html.join(''));
        $newPanel.data('browsedata', browseData);
        $newPanel.data('recorddata', saveData);
        $body.find('.new-row').remove();
        $body.prepend($newPanel);
        $body.prepend($body.find('.legend'));
        $body.prepend($body.find('.search-bar'));

        const $rowBody = $body.find(`#${saveData[rowId]}.panel-body`);
        if (!inactiverecord) {
            $rowBody.prepend($form);
            if ($control.find(`#${moduleName}`).attr('data-showDelete') === 'true') {
                $form.find('div.fwmenu.default > .buttonbar').append('<div class="btn-delete" data-type="DeleteMenuBarButton"><i class="material-icons"></i><div class="btn-text">Delete</div></div>');
            }
            $rowBody.show("fast");
        } else {
            $rowBody.parent().find('.record-selector').html('keyboard_arrow_down');
            $form.remove();
        }
    }
    //----------------------------------------------------------------------------------------------
    getRecordSearch(): string {
        return `
            <div class="search-bar">
              <div class="view-options">
                <div style="display:flex;">
                  <div class="flexcolumn" style="max-width: 28px;">
                    <i class="material-icons show-inactive show-btn" title="Show All">visibility</i>
                    <i class="material-icons hide-inactive show-btn" title="Hide Inactive" style="display:none;">visibility_off</i>
                  </div>
                  <div class="flexcolumn">
                    <div class="view-text">Show Inactive</div>
                  </div>
                </div>
              </div>
              <div class="record-search">
                <span class="input-group-addon search"><i class="material-icons">search</i></span>
                <input type="text" class="form-control recordSearch" placeholder="Record Search" autofocus="">
              </div>
            </div>
       `;
    }
    //----------------------------------------------------------------------------------------------
    getHeaderView($control) {
        const $view = jQuery('<div class="menu-container" data-control="FwFileMenu" data-version="2" data-rendermode="template"><div class="menu"></div></div>');
        const nodeSettings = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Settings');
        const settings = (<any>window).Constants.Modules.Settings;
        this.generateDropDownModuleBtn($view, $control, 'All Settings ID', 'All Settings', null, null);
        for (let settingsCategoryKey in settings.children) {
            const settingsCategory = settings.children[settingsCategoryKey];
            //const nodeSettingsCategory = FwApplicationTree.getNodeById(FwApplicationTree.tree, settingsCategory.id);
            if (true /*nodeSettingsCategory !== null && nodeSettingsCategory.properties.visible === 'T'*/) {
                const dropDownMenuItems = [];
                for (let settingsKey in settingsCategory.children) {
                    const settingsModule = settingsCategory.children[settingsKey];
                    const nodeSettingsModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, settingsModule.id);
                    if (nodeSettingsModule !== null && nodeSettingsModule.properties.visible === 'T') {
                        dropDownMenuItems.push({ id: settingsModule.id, caption: settingsCategory.caption, modulenav: settingsModule.nav, imgurl: '', moduleName: settingsModule.caption });
                    }
                }
                if (dropDownMenuItems.length > 0) {
                    this.generateDropDownModuleBtn($view, $control, settingsCategory.id, settingsCategory.caption, '', dropDownMenuItems);
                }
            }
        }

        return $view;
    }
    //----------------------------------------------------------------------------------------------
    generateDropDownModuleBtn($menu, $control, securityid, caption, imgurl, subitems) {
        const version = $menu.closest('.fwfilemenu').attr('data-version');
        securityid = (typeof securityid === 'string') ? securityid : '';
        let $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                const btnHtml: Array<string> = [];
                btnHtml.push(`<div id="btnModule${securityid}" class="ddmodulebtn menu-tab" data-securityid="${securityid}" data-navigation="${caption}">`);
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw `FwSettings.generateDropDownModuleBtn: ${securityid} caption is not defined in translation`;
        }
        $modulebtn
            .on('click', e => {
                try {
                    const navigationCaption = $modulebtn.data('navigation');
                    const panels = $control.find('.panel-group');
                    if (navigationCaption === 'All Settings') {
                        const event = jQuery.Event('keyup');
                        event.which = 13;
                        $control.find('.selected').removeClass('selected');
                        $control.find('#settingsSearch').val('').trigger(event);
                        jQuery(e.currentTarget).addClass('selected');
                    } else if (navigationCaption != '') {
                        $control.find('.selected').removeClass('selected');
                        $control.find('#settingsSearch').val('')
                        jQuery(e.currentTarget).addClass('selected');
                        //if ($control.find('#' + moduleName + ' > div > div.panel-collapse').is(':hidden')) {
                        //    $control.find('#' + moduleName + ' > div > div.panel-heading').click();
                        //}
                        //jQuery('html, body').animate({
                        //    scrollTop: $control.find('#' + moduleName).offset().top + $control.find('.well').scrollTop()
                        //}, 1);
                        $control.find('.panel-group').hide();
                        $control.find(`.panel-group[data-navigation="${navigationCaption}"]`).show();
                        //for (let i = 0; i < panels.length; i++) {
                        //    if (jQuery(panels[i]).data('navigation') !== navigationCaption) {
                        //        jQuery(panels[i]).hide();
                        //    } else {
                        //        jQuery(panels[i]).show();
                        //    }
                        //}
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })

        $menu.find('.menu').append($modulebtn);
    };
    //----------------------------------------------------------------------------------------------
    generateStandardModuleBtn($menu, $control, securityid, caption, modulenav, imgurl, moduleName) {
        securityid = (typeof securityid === 'string') ? securityid : '';
        let $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                const btnId = 'btnModule' + securityid;
                const btnHtml: Array<string> = [];
                btnHtml.push(`<div id="${btnId}" class="modulebtn menu-tab" data-securityid="${securityid}">`);
                btnHtml.push('<div class="modulebtn-text">');
                btnHtml.push(caption);
                btnHtml.push('</div>');
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw `FwSettings.generateStandardModuleBtn: ${caption} caption is not defined in translation`;
        }

        $modulebtn
            .on('click', e => {
                try {
                    if (modulenav != '') {
                        const panels = $control.find('.panel-group');
                        $control.find('.selected').removeClass('selected');
                        $control.find('#settingsSearch').val('')
                        jQuery(e.currentTarget).addClass('selected');
                        for (let i = 0; i < panels.length; i++) {
                            if (jQuery(panels[i]).attr('id') !== moduleName) {
                                jQuery(panels[i]).hide();
                            } else {
                                jQuery(panels[i]).show();
                            }

                        }
                        if ($control.find(`#${moduleName} > div > div.panel-collapse`).is(':hidden')) {
                            $control.find(`#${moduleName} > div > div.panel-heading`).click();
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })

        $menu.find('.menu').append($modulebtn);
    };
    //----------------------------------------------------------------------------------------------
}

var FwSettings = new FwSettingsClass();