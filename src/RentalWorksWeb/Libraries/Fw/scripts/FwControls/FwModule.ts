﻿class FwModule {
    //----------------------------------------------------------------------------------------------
    static getModuleControl(moduleControllerName: string) {
        var html, $view, $actionView;
        jQuery('html').removeClass('mobile').addClass("desktop");
        html = [];
        html.push('<div id="moduleMaster">');
        html.push('  <div id="moduleMaster-body">');
        html.push('    <div id="moduletabs" class="fwcontrol fwtabs" data-control="FwTabs" data-type="" data-version="1" data-rendermode="template">');
        html.push('      <div class="tabs"></div>');
        html.push('      <div class="newtabbutton"></div>');
        html.push('      <div class="tabpages"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');

        $view = jQuery(html.join(''));
        $view.attr('data-module', moduleControllerName);

        FwControl.renderRuntimeControls($view.find('.fwcontrol'));

        return $view;
    }
    //----------------------------------------------------------------------------------------------
    static openModuleTab($object: JQuery, caption: string, tabHasClose: boolean, tabType: string, setTabActive: boolean) {
        let $tabControl, newtabids, $fwcontrols, controller;

        $tabControl = jQuery('#moduletabs');
        if ($tabControl.find('.tab').length === 0) {
            jQuery('title').html(caption);
        }
        newtabids = FwTabs.addTab($tabControl, caption, tabHasClose, tabType, setTabActive);
        $tabControl.find(`#${newtabids.tabpageid}`).append($object);

        $fwcontrols = $object.find('.fwcontrol');
        FwControl.loadControls($fwcontrols);
        // START CLOSE TAB
        if ($tabControl.find('div[data-tabtype="FORM"]').length > 2) {
            let iconHtml: Array<string> = [], $closeTabButton;
            $tabControl.find('.closetabbutton').html('');
            iconHtml.push(`<div class="closetab">
                            <i class="material-icons">more_horiz</i>
                            <div style="display:none;" class="close-dialog">
                              <div class="leave-active">
                                <span>Close All but Current Tab </span>
                              </div>
                              <div class="close-all">
                                <span>Close All Tabs</span>
                              </div>
                            </div>
                          </div>`);
            $closeTabButton = jQuery(iconHtml.join(''));
            $tabControl.find('.closetabbutton:first').append($closeTabButton);
        } else {
            $tabControl.find('.closetabbutton').html('');
        }
        //Toggle button dialog
        $tabControl.find('.closetab').click(() => {
            $object.find('.close-dialog').html('');
            $tabControl.find('.close-dialog:first').toggle();
        });
        function closeModifiedForms() {
            let $bodyContainer, $openForms, $modifiedForms, $form, $tab, $activeTabId, $tabId;
            $activeTabId = jQuery('body').data('activeTabId');
            $bodyContainer = jQuery('#master-body');
            $modifiedForms = $bodyContainer.find('div[data-modified="true"]');

            for (let i = 0; i < $modifiedForms.length; i++) {
                let $tabId = jQuery($modifiedForms[i]).closest('div[data-type="tabpage"]').attr('id');
                if ($tabId === $activeTabId) {
                    $modifiedForms.splice(i, 1);
                }
            }

            if ($modifiedForms) {
                $form = jQuery($modifiedForms[0]);
                $tab = jQuery(`#${$form.parent().attr('data-tabid')}`);
                $tabId = $tab.attr('data-tabpageid');

                if ($tabId !== $activeTabId) {
                    $tab.click();
                    FwModule.closeForm($form, $tab);
                    $modifiedForms.splice(1);
                }
            }
            $openForms = $bodyContainer.find('div[data-type="form"]');
            if ($openForms.length < 2) {
                $tabControl.find('.closetabbutton').html('');
            }
        }
        // Close all tabs but active tab
        $tabControl.find('.leave-active').click(() => {
            let $bodyContainer, $openForms, $modifiedForms, $unmodifiedForms, $form, $tab, $activeTab, $activeTabId, $tabId;

            jQuery('body').off('click');
            jQuery('body').click(e => {
                if (e.target.className === 'fwconfirmation-button default') {
                    if (e.target.innerHTML !== 'Cancel') {
                        closeModifiedForms();
                    }
                }
            });

            $activeTab = $tabControl.find('div[data-tabtype="FORM"].tab.active');
            $activeTabId = $activeTab.attr('data-tabpageid');
            jQuery('body').data('activeTabId', $activeTabId);
            $bodyContainer = jQuery('#master-body');
            $bodyContainer = jQuery('#master-body');
            $modifiedForms = $bodyContainer.find('div[data-modified="true"]');
            $unmodifiedForms = $bodyContainer.find('div[data-modified="false"]');

            for (let i = 0; i < $unmodifiedForms.length; i++) {
                $form = jQuery($unmodifiedForms[i]);
                $tab = jQuery(`#${$form.parent().attr('data-tabid')}`);
                $tabId = $tab.attr('data-tabpageid');
                if ($tabId !== $activeTabId) {
                    $tab.click();
                    FwModule.closeForm($form, $tab);
                }
            }
            if ($modifiedForms.length >= 1) {
                closeModifiedForms();
            }
            $openForms = $bodyContainer.find('div[data-type="form"]');
            if ($openForms.length < 2) {
                $tabControl.find('.closetabbutton').html('');
                jQuery('body').off('click');
            }
        });
        // Close all tabs
        $tabControl.find('.close-all').click(() => {
            let $rootTab, $bodyContainer, $openForms, moduleNav, controller;
            $rootTab = $tabControl.find('div[data-tabpageid="tabpage1"]');
            $bodyContainer = jQuery('#master-body');
            $openForms = $bodyContainer.find('div[data-type="form"]');
            controller = $rootTab.closest('#moduleMaster').attr('data-module');
            moduleNav = window[`${controller}`].nav;
            program.getModule(moduleNav);

            if ($openForms.length < 2) {
                $tabControl.find('.closetabbutton').html('');
            }
        });
        // END CLOSE TAB

        if ($object.hasClass('fwbrowse')) {
            if ($object.attr('data-newtab') == 'true') {
                let html = [];
                html.push(`<div class="addnewtab">
                            <i class="material-icons">add</i>
                          </div>`);
                let $newTabButton = jQuery(html.join(''));
                $tabControl.find('.newtabbutton').append($newTabButton);
                $newTabButton.on('click', e => {
                    $object.find('.buttonbar [data-type="NewMenuBarButton"]').click();
                });
            }

            let $searchbox = jQuery('.search input:visible');
            if ($searchbox.length > 0) {
                $searchbox.eq(0).focus();
            }
            if (typeof window[$object.attr('data-controller')] !== 'undefined') {
                controller = window[$object.attr('data-controller')];
                if (typeof controller.onLoadBrowse === 'function') {
                    controller.onLoadBrowse($object);
                }
            }
        }
        else if ($object.hasClass('fwform')) {
            if (typeof window[$object.attr('data-controller')] !== 'undefined') {
                controller = window[$object.attr('data-controller')];
                if (typeof controller.onLoadForm === 'function') {
                    controller.onLoadForm($object);
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    static openSubModuleTab($browse: JQuery, $form: JQuery) {
        var $parentform, $tabControl, newtabids, controller, $newtab, $parenttab;

        $parentform = $browse.closest('.fwform');

        $tabControl = jQuery('#moduletabs');

        newtabids = FwTabs.addTab($tabControl, 'New ' + $form.attr('data-caption'), true, 'FORM', true);
        $tabControl.find('#' + newtabids.tabpageid).append($form);

        $newtab = $tabControl.find('#' + newtabids.tabid);
        $newtab.addClass('submodule').remove();

        $parenttab = $tabControl.find('#' + $parentform.closest('.tabpage').attr('data-tabid'));
        $parenttab.after($newtab);

        if (typeof $parenttab.data('subtabids') === 'undefined') {
            $parenttab.data('subtabids', [newtabids.tabid]);
        } else {
            $parenttab.data('subtabids').push(newtabids.tabid);
        }
        $newtab.data('parenttabid', $parenttab.attr('id'));

        FwControl.loadControls($form.find('.fwcontrol'));

        if (typeof window[$form.attr('data-controller')] !== 'undefined') {
            controller = window[$form.attr('data-controller')];
            if (typeof controller.onLoadForm === 'function') {
                controller.onLoadForm($form);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    static openFormTab($form: JQuery, $object: JQuery, tabname: string, tabhasclose: boolean, tabtype: string, setactive: boolean) {
        var $formtabcontrol, tabids, $fwcontrols;
        $formtabcontrol = $form.find('div.fwtabs:first');
        tabids = FwTabs.addTab($formtabcontrol, tabname, tabhasclose, tabtype, setactive);
        $form.find('#' + tabids.tabpageid).append($object);
        $form.find('#' + tabids.tabid + ' .delete').on('click', function () {
            FwTabs.setActiveTab($form.find('#' + tabids.tabid).parent().parent().parent(), $form.find('#' + tabids.tabid).siblings(':first'));
        });

        $fwcontrols = $object.find('.fwcontrol');
        FwControl.loadControls($fwcontrols);
    }
    //----------------------------------------------------------------------------------------------
    static openBrowse($browse: JQuery) {
        let controller = $browse.attr('data-controller');

        if (sessionStorage.getItem('customForms') !== null) {
            let customForms = JSON.parse(sessionStorage.getItem('customForms'));
            var baseForm = controller.replace('Controller', 'Browse');
            customForms = customForms.filter(a => a.BaseForm == baseForm);
            if (customForms.length > 0) {
                $browse = jQuery(jQuery(`#tmpl-custom-${baseForm}`)[0].innerHTML);
            }
        }

        FwControl.renderRuntimeControls($browse.find('.fwcontrol').addBack());
        FwModule.addBrowseMenu($browse);

        let fields = FwModule.loadFormFromTemplate($browse.data('name')).find('.fwformfield');
        let findFields = [];
        let textComparisonFields = [
            { value: 'Contains', text: 'Contains' },
            { value: 'StartsWith', text: 'Starts With' },
            { value: 'EndsWith', text: 'Ends With' },
            { value: 'Equals', text: 'Equals' },
            { value: 'DoesNotContain', text: 'Does Not Contain' },
            { value: 'DoesNotEqual', text: 'Does Not Equal' }
        ];
        let numericComparisonFields = [
            { value: 'Equals', text: '=' },
            { value: 'GreaterThan', text: '>' },
            { value: 'GreaterThanEqual', text: '≥' },
            { value: 'LessThan', text: '<' },
            { value: 'LessThanEqual', text: '≤' },
            { value: 'NotEqual', text: '≠' },
        ];
        let booleanComparisonFields = [
            { value: 'Equals', text: 'Equals' },
            { value: 'DoesNotEqual', text: 'Does Not Equal' }
        ];
        let dateComparisonFields = [
            { value: 'Equals', text: 'Equals' },
            { value: 'PriorTo', text: 'Prior To' },
            { value: 'PriorToEquals', text: 'Prior To or Equals' },
            { value: 'LaterThan', text: 'Later Than' },
            { value: 'LaterThanEquals', text: 'Later Than or Equals' },
            { value: 'NotEqual', text: 'Does Not Equal' },
        ];

        FwAppData.apiMethod(true, 'GET', window[controller].apiurl + '/emptyobject', null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response._Fields.length; i++) {
                findFields.push({
                    'value': response._Fields[i].Name,
                    'text': response._Fields[i].Name,
                    'type': response._Fields[i].DataType
                })
            }
            window['FwFormField_select'].loadItems($browse.find('.datafieldselect'), findFields, false);
            window['FwFormField_select'].loadItems($browse.find('.andor'), [{ value: 'And', text: 'And' }, { value: 'Or', text: 'Or' }], true);
            $browse.find('.datafieldselect').on('change', function () {
                let datatype = jQuery(this).find(':selected').data('type');
                switch (datatype) {
                    case 'Text':
                        window['FwFormField_select'].loadItems($browse.find('.datafieldcomparison'), textComparisonFields, true);
                        break;
                    case 'Number':
                        window['FwFormField_select'].loadItems($browse.find('.datafieldcomparison'), numericComparisonFields, true);
                        break;
                    case 'Checkbox':
                        window['FwFormField_select'].loadItems($browse.find('.datafieldcomparison'), booleanComparisonFields, true);
                        break;
                    case 'Date':
                        window['FwFormField_select'].loadItems($browse.find('.datafieldcomparison'), dateComparisonFields, true);
                        break;
                }
            })
        }, function onError(response) {
            FwFunc.showError(response);
        }, null);

        $browse.find('.add-query').on('click', function cloneRow() {
            let newRow = jQuery(this).closest('.queryrow').clone();

            newRow.find('.datafieldselect').on('change', function () {
                let datatype = jQuery(this).find(':selected').data('type');
                switch (datatype) {
                    case 'text':
                        window['FwFormField_select'].loadItems(newRow.find('.datafieldcomparison'), textComparisonFields, true);
                        break;
                    case 'number':
                        window['FwFormField_select'].loadItems(newRow.find('.datafieldcomparison'), numericComparisonFields, true);
                        break;
                    case 'checkbox':
                        window['FwFormField_select'].loadItems(newRow.find('.datafieldcomparison'), booleanComparisonFields, true);
                        break;
                    case 'date':
                        window['FwFormField_select'].loadItems(newRow.find('.datafieldcomparison'), dateComparisonFields, true);
                        break;
                }
            });
            newRow.find('.delete-query').on('click', function () {
                if (newRow.find('.add-query').css('visibility') === 'visible') {
                    newRow.prev().find('.add-query').css('visibility', 'visible');
                }
                if (newRow.find('.andor').css('visibility') === 'hidden') {
                    newRow.next().find('.andor').css('visibility', 'hidden');
                }
                if ($browse.find('.query').find('.queryrow').length === 2) {
                    newRow.next().find('.delete-query').removeAttr('style').css('visibility', 'hidden');
                }

                newRow.remove();
            }).css('visibility', 'visible');
            newRow.find('.andor').css('visibility', 'visible');
            newRow.find('.add-query').on('click', cloneRow);
            newRow.find('input').val('')
            newRow.appendTo($browse.find('.query'));
            if ($browse.find('.query').find('.queryrow').length > 1) {
                jQuery($browse.find('.query').find('.queryrow')[0]).find('.delete-query').on('click', function () {
                    jQuery(this).closest('.queryrow').next().find('.andor').css('visibility', 'hidden');
                    jQuery(this).closest('.queryrow').remove();
                }).css('visibility', 'visible');
            }
            jQuery(this).css('cursor', 'default');
            jQuery(this).css('visibility', 'hidden');
        })

        $browse.find('.search').on('click', function () {
            let request = FwBrowse.getRequest($browse);
            request.searchfieldoperators.unshift('like');
            request.searchfields.unshift(FwFormField.getValue2($browse.find('div[data-datafield="Datafield"]')));
            request.searchfieldvalues.unshift(FwFormField.getValue2($browse.find('div[data-datafield="DatafieldQuery"]')));
            request.searchseparators.unshift(',');

            FwServices.module.method(request, request.module, 'Browse', $browse, function (response) {
                try {
                    FwBrowse.beforeDataBindCallBack($browse, request, response);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })

        })

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    static addBrowseMenu($browse: JQuery) {
        var controller, $menu, $new, $edit, $delete, $inactiveView, $activeView, $allView, $show, $vr, $submenubtn, $submenucolumn, $optiongroup, $excelxlsx, $excelxls,
            nodeModule, nodeBrowse, nodeBrowseMenuBar, nodeBrowseSubMenu, $submenubtn, $menubarbutton, nodeSubMenuGroup, $submenucolumn, $submenugroup,
            nodeSubMenuItem, $submenuitem, hasClickEvent, $editbtn;

        controller = $browse.attr('data-controller');
        $menu = FwMenu.getMenuControl('default');

        nodeModule = FwApplicationTree.getNodeByController(controller);
        if (nodeModule !== null) {
            nodeBrowse = FwApplicationTree.getChildByType(nodeModule, 'Browse');
            if (nodeBrowse !== null) {
                nodeBrowseMenuBar = FwApplicationTree.getChildByType(nodeBrowse, 'MenuBar');
                if (nodeBrowseMenuBar !== null) {
                    if (nodeBrowseMenuBar.properties.visible === 'T') {
                        $browse.find('.fwbrowse-menu').append($menu);
                        nodeBrowseMenuBar.children.push({
                            id: '303DF0E5-1410-4894-8379-6D2995132DA9',
                            properties: {
                                caption: 'Find',
                                nodetype: 'FindMenuBarButton',
                                visible: 'T'
                            }
                        })
                        for (var menubaritemno = 0; menubaritemno < nodeBrowseMenuBar.children.length; menubaritemno++) {
                            var nodeMenuBarItem = nodeBrowseMenuBar.children[menubaritemno];
                            if (nodeMenuBarItem.properties.visible === 'T') {
                                switch (FwApplicationTree.getNodeType(nodeMenuBarItem)) {
                                    case 'SubMenu':
                                        nodeBrowseSubMenu = nodeMenuBarItem;
                                        $submenubtn = FwMenu.addSubMenu($menu);
                                        for (var submenuoptionno = 0; submenuoptionno < nodeBrowseSubMenu.children.length; submenuoptionno++) {
                                            nodeSubMenuGroup = nodeBrowseSubMenu.children[submenuoptionno];
                                            if (nodeSubMenuGroup.properties.visible === 'T') {
                                                $submenucolumn = FwMenu.addSubMenuColumn($submenubtn)
                                                $submenugroup = FwMenu.addSubMenuGroup($submenucolumn, nodeSubMenuGroup.properties.caption, nodeSubMenuGroup.id);
                                                for (var submenuitemno = 0; submenuitemno < nodeSubMenuGroup.children.length; submenuitemno++) {
                                                    nodeSubMenuItem = nodeSubMenuGroup.children[submenuitemno];
                                                    if (nodeSubMenuItem.properties.visible === 'T') {
                                                        switch (FwApplicationTree.getNodeType(nodeSubMenuItem)) {
                                                            case 'SubMenuItem':
                                                                $submenuitem = FwMenu.addSubMenuBtn($submenugroup, nodeSubMenuItem.properties.caption, nodeSubMenuItem.id);
                                                                hasClickEvent = ((typeof controller === 'string') &&
                                                                    (controller.length > 0) &&
                                                                    (typeof FwApplicationTree.clickEvents !== 'undefined') &&
                                                                    (typeof FwApplicationTree.clickEvents['{' + nodeSubMenuItem.id + '}'] === 'function'));
                                                                if (hasClickEvent) {
                                                                    $submenuitem.on('click', FwApplicationTree.clickEvents['{' + nodeSubMenuItem.id + '}']);
                                                                }
                                                                break;
                                                            case 'DownloadExcelSubMenuItem':
                                                                $submenuitem = FwMenu.addSubMenuBtn($submenugroup, nodeSubMenuItem.properties.caption, nodeSubMenuItem.id);
                                                                $submenuitem.on('click', function () {
                                                                    try {
                                                                        let $confirmation, $yes, $no, totalNumberofRows, totalNumberofRowsStr, totalPages, pageSize, userDefinedNumberofRows;
                                                                        let module = window[controller].Module;
                                                                        let apiurl = window[controller].apiurl;
                                                                        let request = FwBrowse.getRequest($browse);

                                                                        $confirmation = FwConfirmation.renderConfirmation('Download Excel Workbook', '');
                                                                        $confirmation.find('.fwconfirmationbox').css('width', '450px');
                                                                        totalNumberofRows = FwBrowse.getTotalRowCount($browse);
                                                                        totalNumberofRowsStr = totalNumberofRows.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");


                                                                        let html = [];
                                                                        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                                                                        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                                                        html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield all-records" data-caption="Download all ${totalNumberofRowsStr} Records" data-datafield="" style="float:left;width:100px;"></div>`);
                                                                        html.push('  </div>');
                                                                        html.push(' <div class="formrow" style="width:100%;display:flex;align-content:flex-start; align-items:center">');
                                                                        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                                                                        html.push(`    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield user-defined-records" data-caption="" data-datafield="" style="float:left;width:30px;"></div>`);
                                                                        html.push('  </div>');
                                                                        html.push('  <span style="margin:22px 0px 0px 0px;">First</span>');
                                                                        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin:0px 0px 0px 0px;">');
                                                                        html.push('    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield user-defined-records-input" data-caption="" data-datafield="" style="width:80px;float:left;margin:0px 0px 0px 0px;"></div>');
                                                                        html.push('  </div>');
                                                                        html.push('  <span style="margin:22px 0px 0px 0px;">Records</span>');
                                                                        html.push(' </div>');
                                                                        html.push('</div>');

                                                                        FwConfirmation.addControls($confirmation, html.join(''));
                                                                        $yes = FwConfirmation.addButton($confirmation, 'Download', false);
                                                                        $no = FwConfirmation.addButton($confirmation, 'Cancel');

                                                                        $confirmation.find('.user-defined-records-input input').val(request.pagesize);
                                                                        $confirmation.find('.all-records input').prop('checked', true);
                                                                        userDefinedNumberofRows = +$confirmation.find('.user-defined-records input').val();

                                                                        $confirmation.find('.all-records input').on('change', function () {
                                                                            var $this = jQuery(this);
                                                                            if ($this.prop('checked') === true) {
                                                                                $confirmation.find('.user-defined-records input').prop('checked', false);
                                                                            }
                                                                            else {
                                                                                $confirmation.find('.user-defined-records input').prop('checked', true);
                                                                            }
                                                                        });

                                                                        $confirmation.find('.user-defined-records input').on('change', function () {
                                                                            var $this = jQuery(this);
                                                                            if ($this.prop('checked') === true) {
                                                                                $confirmation.find('.all-records input').prop('checked', false);
                                                                            }
                                                                            else {
                                                                                $confirmation.find('.all-records input').prop('checked', true);
                                                                            }
                                                                        });

                                                                        $confirmation.find('.user-defined-records-input input').keypress(function () {
                                                                            $confirmation.find('.user-defined-records input').prop('checked', true);
                                                                            $confirmation.find('.all-records input').prop('checked', false);
                                                                        });

                                                                        $yes.on('click', () => {
                                                                            if ($confirmation.find('.all-records input').prop('checked') === true) {
                                                                                userDefinedNumberofRows = totalNumberofRows;
                                                                            } else {
                                                                                userDefinedNumberofRows = +$confirmation.find('.user-defined-records-input input').val();
                                                                            }

                                                                            request.pagesize = userDefinedNumberofRows

                                                                            FwAppData.apiMethod(true, 'POST', `${apiurl}/exportexcelxlsx/${module}`, request, FwServices.defaultTimeout, function (response) {
                                                                                try {
                                                                                    //let $iframe = jQuery('<iframe style="display:none;" />');
                                                                                    //jQuery('.application').append($iframe);
                                                                                    //$iframe.attr('src', `${applicationConfig.apiurl}${response.downloadUrl}`);
                                                                                    //setTimeout(function () {
                                                                                    //    $iframe.remove();
                                                                                    //}, 500);

                                                                                    window.location.assign(applicationConfig.apiurl + response.downloadUrl);
                                                                                } catch (ex) {
                                                                                    FwFunc.showError(ex);
                                                                                }
                                                                            }, null, null);
                                                                            FwConfirmation.destroyConfirmation($confirmation);

                                                                            FwNotification.renderNotification('INFO', 'Downloading Excel Workbook...');
                                                                        });
                                                                    } catch (ex) {
                                                                        FwFunc.showError(ex);
                                                                    }
                                                                });
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 'MenuBarButton':
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.on('click', FwApplicationTree.clickEvents['{' + nodeMenuBarItem.id + '}']);
                                        break;
                                    case 'NewMenuBarButton':
                                        $browse.attr('data-newtab', 'true');
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.attr('data-type', 'NewMenuBarButton');
                                        $menubarbutton.on('click', function () {
                                            var $form, controller, $browse, issubmodule;
                                            try {
                                                $browse = jQuery(this).closest('.fwbrowse');
                                                $browse.attr('data-newtab', 'true');
                                                controller = $browse.attr('data-controller');
                                                issubmodule = $browse.closest('.tabpage').hasClass('submodule');
                                                if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                                                if (typeof window[controller].openForm !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                                                $form = window[controller].openForm('NEW');
                                                if (!issubmodule) {
                                                    FwModule.openModuleTab($form, 'New ' + $form.attr('data-caption'), true, 'FORM', true);
                                                } else {
                                                    FwModule.openSubModuleTab($browse, $form);
                                                }
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                        break;
                                    case 'EditMenuBarButton':
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.attr('data-type', 'EditMenuBarButton');
                                        $menubarbutton.on('click', function (event) {
                                            var $selectedRow, $browse;
                                            try {
                                                $browse = jQuery(this).closest('.fwbrowse');
                                                $selectedRow = $browse.find('tr.selected');
                                                if ($selectedRow.length > 0) {
                                                    $selectedRow.dblclick();
                                                } else {
                                                    FwNotification.renderNotification('WARNING', 'Please select a row.');
                                                }
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                        break;
                                    case 'ViewMenuBarButton':
                                        $editbtn = FwApplicationTree.getChildByType(nodeBrowseMenuBar, 'EditMenuBarButton');
                                        if ($editbtn === null) {
                                            $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                            $menubarbutton.attr('data-type', 'ViewMenuBarButton');
                                            $menubarbutton.on('click', function (event) {
                                                var $selectedRow, $browse;
                                                try {
                                                    $browse = jQuery(this).closest('.fwbrowse');
                                                    $selectedRow = $browse.find('tr.selected');
                                                    if ($selectedRow.length > 0) {
                                                        $selectedRow.dblclick();
                                                    } else {
                                                        FwNotification.renderNotification('WARNING', 'Please select a row.');
                                                    }
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
                                        }
                                        break;
                                    case 'DeleteMenuBarButton':
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.attr('data-type', 'DeleteMenuBarButton');
                                        $menubarbutton.on('click', function () {
                                            var $browse;
                                            $browse = jQuery(this).closest('.fwbrowse');
                                            controller = $browse.attr('data-controller');
                                            if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                                            if (typeof window[controller]['deleteRecord'] === 'function') {
                                                window[controller]['deleteRecord']($browse);
                                            } else {
                                                FwModule['deleteRecord'](window[controller].Module, $browse);
                                            }
                                        });
                                        break;
                                    case 'FindMenuBarButton':
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $browse = $menubarbutton.closest('.fwbrowse');

                                        $menubarbutton.attr('data-type', 'FindMenuBarButton');

                                        $menubarbutton.on('click', function (e) {
                                            controller = $browse.attr('data-controller');
                                            let maxZIndex;
                                            let $this = jQuery(this);
                                            e.preventDefault();

                                            if (!$this.hasClass('active')) {
                                                maxZIndex = FwFunc.getMaxZ('*');
                                                $this.find('.findbutton-dropdown').css('z-index', maxZIndex + 1);
                                                $this.addClass('active');

                                                jQuery(document).on('click', function closeMenu(e: any) {
                                                    if ($menubarbutton.has(e.target).length === 0 && !jQuery(e.target).hasClass('delete-query')) {
                                                        $this.removeClass('active');
                                                        $this.find('.findbutton-dropdown').css('z-index', '0');
                                                        jQuery(document).off('click');
                                                    }
                                                });
                                            }
                                        });

                                        $menubarbutton.append(`
                                        <div class="findbutton-dropdown">
                                            <div class="query">
                                                <div class="flexrow queryrow" style="align-items: center;">
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield andor" data-caption="" data-datafield="AndOr" style="flex:1 1 auto;"></div>
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield datafieldselect" data-caption="Datafield" data-datafield="Datafield" style="flex:1 1 auto;"></div>
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield datafieldcomparison" data-caption="" data-datafield="DatafieldComparison" style="flex:1 1 auto;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="DatafieldQuery" style="flex:1 1 auto;"></div>
                                                    <i class="material-icons delete-query">delete_outline</i>
                                                    <i class="material-icons add-query">add_circle_outline</i>
                                                </div>
                                            </div>
                                            <div class="flexrow queryrow">
                                                <div class="find fwformcontrol search" data-type="button" style="flex:0 1 50px;margin:15px 15px 10px 10px;margin-left:auto;">Apply</div>
                                            </div>
                                        </div>`);
                                        FwControl.renderRuntimeHtml($menubarbutton.find('.fwcontrol'));
                                        break;
                                }
                            }
                        }
                        nodeBrowseMenuBar.children.splice(nodeBrowseMenuBar.children.length - 1, 1);
                    }
                }
            }
        }
        if ((typeof controller === 'string') && (controller.length > 0) && (typeof window[controller]['addBrowseMenuItems'] === 'function')) {
            var $menuobj = window[controller]['addBrowseMenuItems']($menu, $browse);
            if (typeof $menuobj !== 'undefined') {
                $menu = $menuobj;
            }
        }

        if ((typeof $browse.attr('data-hasinactive') === 'string') && ($browse.attr('data-hasinactive') === 'true')) {
            if (typeof $browse.attr('data-activeinactiveview') === 'undefined') {
                $browse.attr('data-activeinactiveview', 'active');
            }
            if (!($browse.attr('data-activeinactiveview') === 'active' || $browse.attr('data-activeinactiveview') === 'inactive' || $browse.attr('data-activeinactiveview') === 'all')) {
                throw 'On the Browse template for ' + controller + ' the attribute data-activeinactiveview must be active, inactive, or all';
            }
            FwMenu.addVerticleSeparator($menu);

            $activeView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('Active'), true);
            $activeView.on('click', function () {
                var $fwbrowse;
                try {
                    $fwbrowse = jQuery(this).closest('.fwbrowse');
                    $fwbrowse.attr('data-activeinactiveview', 'active');
                    FwBrowse.search($fwbrowse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $inactiveView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('Inactive'), false);
            $inactiveView.on('click', function () {
                var $fwbrowse;
                try {
                    $fwbrowse = jQuery(this).closest('.fwbrowse');
                    $fwbrowse.attr('data-activeinactiveview', 'inactive');
                    FwBrowse.search($fwbrowse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            $allView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('All'), false);
            $allView.on('click', function () {
                var $fwbrowse;
                try {
                    $fwbrowse = jQuery(this).closest('.fwbrowse');
                    $fwbrowse.attr('data-activeinactiveview', 'all');
                    FwBrowse.search($fwbrowse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            var viewitems = [];
            viewitems.push($activeView);
            viewitems.push($inactiveView);
            viewitems.push($allView);
            $show = FwMenu.addViewBtn($menu, FwLanguages.translate('Show'), viewitems);
        }
        FwControl.renderRuntimeControls($menu.find('.fwcontrol').addBack());
    }
    //----------------------------------------------------------------------------------------------
    static openForm($form: JQuery, mode: string) {
        var $fwcontrols, formid, $formTabControl, auditTabIds, $auditControl, controller,
            nodeModule, nodeForm, nodeTabs, nodeTab, $tabs, nodeField, $fields, nodeGrid, $grids, $tabcontrol, args;

        controller = $form.attr('data-controller');

        if (sessionStorage.getItem('customForms') !== null) {
            let customForms = JSON.parse(sessionStorage.getItem('customForms'));
            var baseForm = controller.replace('Controller', 'Form');
            customForms = customForms.filter(a => a.BaseForm == baseForm);
            if (customForms.length > 0) {
                $form = jQuery(jQuery(`#tmpl-custom-${baseForm}`)[0].innerHTML);
            }
        }

        nodeModule = FwApplicationTree.getNodeByController($form.attr('data-controller'));
        args = {};
        nodeForm = FwApplicationTree.getChildByType(nodeModule, 'Form');

        if (typeof mode === 'string') {
            $form.attr('data-mode', mode);
        } else {
            $form.attr('data-mode', 'NEW');
        }
        formid = program.uniqueId(8);

        $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        FwControl.setIds($fwcontrols, formid);

        FwModule.addFormMenu($form);

        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));

        $form.attr('data-modified', 'false');
        if (typeof window[controller]['renderGrids'] === 'function') {
            window[controller]['renderGrids']($form);
        }
        $form.data('grids', $form.find('div[data-control="FwGrid"]'));
        if (typeof window[controller]['setFormProperties'] === 'function') {
            window[controller]['setFormProperties']($form);
        }

        if (typeof window[controller]['addButtonMenu'] === 'function') {
            window[controller]['addButtonMenu']($form);
        }


        //if (sessionStorage.getItem('customFields') !== null) {
        //    let customFields = JSON.parse(sessionStorage.getItem('customFields'))
        //    if (customFields !== null && typeof customFields.length === 'number' && customFields.length > 0) {
        //        let hasCustomFields = false;
        //        for (var i = 0; i < customFields.length; i++) {
        //            if (controller.slice(0, -10) === customFields[i]) {
        //                FwModule.loadCustomFields($form, customFields[i]);
        //            }
        //        }
        //    }
        //}   -- Jason Hoang 12/10/2018 removed custom fields tab

        //add Audit tab to all forms
        let $keys = $form.find('.fwformfield[data-type="key"]');
        if ($keys.length !== 0) {
            $formTabControl = jQuery($form.find('.fwtabs'));
            auditTabIds = FwTabs.addTab($formTabControl, 'Audit', false, 'AUDIT', false);
            $auditControl = jQuery(jQuery('#tmpl-grids-AuditHistoryGridBrowse').html());
            $auditControl.data('ondatabind', function (request) {
                request.uniqueids = {};
                for (let i = 0; i < 2; i++) {
                    let uniqueIdValue = jQuery($keys[i]).find('input').val();
                    if (typeof uniqueIdValue !== 'undefined') {
                        switch (i) {
                            case 0:
                                request.uniqueids.UniqueId1 = uniqueIdValue;
                                break;
                            case 1:
                                request.uniqueids.UniqueId2 = uniqueIdValue;
                                break;
                            case 2:
                                request.uniqueids.UniqueId3 = uniqueIdValue;
                                break;
                        }
                    } else {
                        break;
                    }
                }
            });
            FwBrowse.init($auditControl);
            FwBrowse.renderRuntimeHtml($auditControl);

            $formTabControl.find('#' + auditTabIds.tabpageid).append($auditControl);
            $formTabControl.find('#' + auditTabIds.tabid)
                .addClass('audittab')
                .on('click', e => {
                    if ($form.attr('data-mode') !== 'NEW') {
                        FwBrowse.search($auditControl);
                    };
                });
        }


        $form
            .on('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]', function (event) {
                var fields, $tab, $tabpage;
                event.stopPropagation();

                $tabpage = $form.parent();
                $tab = jQuery('#' + $tabpage.attr('data-tabid'));
                fields = FwModule.getFormFields($form, false);
                if (Object.keys(fields).length > 0) {
                    $tab.find('.modified').html('*');
                    $form.attr('data-modified', 'true');
                    $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                } else {
                    $tab.find('.modified').html('');
                    $form.attr('data-modified', 'false');
                    $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
                }
            })
            .on('change', '.fwformfield[data-noduplicate="true"]', function () {
                var $this;
                $this = jQuery(this);
                FwModule.checkDuplicate($form, $this);
            })
            .on('change', '.fwformfield[data-required="true"].error', function () {
                var $this, value, errorTab;
                $this = jQuery(this);
                value = FwFormField.getValue2($this);
                errorTab = $this.closest('.tabpage').attr('data-tabid');
                if (value != '') {
                    $this.removeClass('error');
                    if ($this.closest('.tabpage.active').has('.error').length === 0) {
                        $this.parents('.fwcontrol .fwtabs').find('#' + errorTab).removeClass('error');
                    }
                }
            })
            ;

        // hide tabs based on security tree
        $tabcontrol = $form.find('.fwtabs');
        if ($tabcontrol.length > 1) {
            $tabcontrol = $tabcontrol.eq(0);
        }
        $tabs = $tabcontrol.find('div[data-type="tab"]');
        $tabs.each(function (index, element) {
            var $tab, caption, args;
            $tab = jQuery(element);
            args = {};
            args.caption = $tab.attr('data-caption');
            nodeTab = FwApplicationTree.getNodeByFuncRecursive(nodeForm, args, function (node, args2) {
                var istab, captionsmatch;
                istab = (node.properties.nodetype === 'Tab');
                captionsmatch = (node.properties.caption === args2.caption);
                return (istab && captionsmatch);
            });
            if ((nodeTab !== null) && (nodeTab.properties.visible === 'F')) {
                FwTabs.removeTab($tab);
            }
        });
        $tabs = $tabcontrol.find('div[data-type="tab"]');
        if ($tabs.length > 0) {
            FwTabs.setActiveTab($tabcontrol, $tabs.eq(0));
        }

        // hide fields based on security tree
        $fields = $form.find('div[data-datafield]');
        $fields.each(function (index, element) {
            var $field, args;
            $field = jQuery(element);
            args = {};
            args.caption = $field.attr('data-caption');
            args.datafield = $field.attr('data-datafield');
            nodeField = FwApplicationTree.getNodeByFuncRecursive(nodeForm, args, function (node, args2) {
                var isfield, captionsmatch, idsmatch;
                isfield = (node.properties.nodetype === 'Field');
                //captionsmatch = (node.properties.caption  === args2.caption);
                idsmatch = false;
                if ((typeof args2.caption === 'string') && (typeof args2.datafield === 'string')) {
                    idsmatch = (node.id.indexOf('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, '')) !== -1);
                    //console.log('---------');
                    //console.log(node.id);
                    //console.log('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, ''));
                    //console.log(node.id.indexOf('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, '')));
                }
                return (isfield && idsmatch);
            });
            if (nodeField !== null) {
                if (nodeField.properties.visible === 'F') {
                    $field.remove();
                }
                if (nodeField.properties.editable === 'F') {
                    FwFormField.disable($field);
                }
            }
        });

        // hide grids based on security tree
        $grids = $form.data('grids');
        $grids.each(function (index, element) {
            var $grid, args, nodeGridButton;
            $grid = jQuery(element);
            args = {};
            args.grid = $grid.attr('data-grid');
            nodeGrid = FwApplicationTree.getNodeByFuncRecursive(nodeForm, args, function (node, args2) {
                var isgrid, gridnamesmatch;
                isgrid = (node.properties.nodetype === 'FormGrid');
                gridnamesmatch = (node.properties.grid === args2.grid);
                return (isgrid && gridnamesmatch);
            });
            if (nodeGrid !== null) {
                if (nodeGrid.properties.visible === 'F') {
                    $grid.empty().css('visibility', 'hidden');
                }
                for (var gridBtnIndex = 0; gridBtnIndex < nodeGrid.children.length; gridBtnIndex++) {
                    nodeGridButton = nodeGrid.children[gridBtnIndex];
                    if (nodeGridButton.properties.visible === 'F') {
                        switch (nodeGridButton.properties.nodetype) {
                            case 'NewMenuBarButton':
                                $grid.attr('data-security-new', 'F');
                                break;
                            case 'EditMenuBarButton':
                                $grid.attr('data-security-edit', 'F');
                                break;
                            case 'DeleteMenuBarButton':
                                $grid.attr('data-security-delete', 'F');
                                break;
                        }
                    }
                }
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    static loadForm(module: string, $form: JQuery) {
        var request;
        request = {
            module: module,
            ids: FwModule.getFormUniqueIds($form)
        }
        FwServices.module.method(request, module, 'Load', $form, function (response) {
            try {
                FwModule.afterLoadForm(module, $form, response);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    static loadForm2(httpMethod: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, request: any, module: string, $form: JQuery) {
        FwAppData.apiMethod(true, httpMethod, url, request, FwServices.defaultTimeout,
            function (response) { // onSuccess
                try {
                    FwModule.afterLoadForm(module, $form, response);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            },
            null, $form);
    }
    //----------------------------------------------------------------------------------------------
    static afterLoadForm(module: string, $form: JQuery, response: any) {
        let $tabpage = $form.parent();
        let $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        let controller = window[module + 'Controller'];
        if (typeof controller === 'undefined') {
            throw module + 'Controller is not defined.'
        }
        if (typeof controller.apiurl === 'undefined' && typeof response.tables === 'undefined') {
            throw 'Server did not return the data needed to load this form.';
        }
        if (typeof controller.apiurl !== 'undefined') {
            controller = $form.attr('data-controller');

            var tabname = (typeof response.tabname === 'string') ? response.tabname : (typeof response.RecordTitle === 'string') ? response.RecordTitle : 'Unknown';
            $tab.find('.caption').html(tabname);

            let $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
            FwFormField.loadForm($formfields, response);

            let $fwchargefields = $form.find('div.fwcharge');
            if (($fwchargefields.length > 0) && ($fwchargefields.eq(0).attr('data-template') != $form.find('div[data-datafield="' + $fwchargefields.eq(0).attr('data-boundfield') + '"] input').val())) {
                FwCharge.rerenderRuntimeHtml($form, response);
            }

            if (typeof window[controller]['setFormProperties'] === 'function') {
                window[controller]['setFormProperties']($form);
            }
            if (typeof window[controller]['afterLoad'] === 'function') {
                window[controller]['afterLoad']($form);
            }
        } else {
            //$form.attr('data-mode', 'EDIT');
            controller = $form.attr('data-controller');

            $tab.find('.caption').html(response.tabname);

            let $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
            FwFormField.loadForm($formfields, response.tables);

            let $fwchargefields = $form.find('div.fwcharge');
            if (($fwchargefields.length > 0) && ($fwchargefields.eq(0).attr('data-template') != $form.find('div[data-datafield="' + $fwchargefields.eq(0).attr('data-boundfield') + '"] input').val())) {
                FwCharge.rerenderRuntimeHtml($form, response.tables);
            }

            if (typeof window[controller]['setFormProperties'] === 'function') {
                window[controller]['setFormProperties']($form);
            }
            if (typeof window[controller]['afterLoad'] === 'function') {
                window[controller]['afterLoad']($form);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    static saveForm(module: string, $form: JQuery, parameters: { closetab?: boolean; afterCloseForm?: Function; closeparent?: boolean; navigationpath?: string; refreshRootTab?: boolean }) {
        var $tabpage, $tab, isValid, request, controllername, controller;
        $tabpage = $form.parent();
        $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        isValid = FwModule.validateForm($form);
        controllername = $form.attr('data-controller');
        controller = window[controllername];

        if (isValid === true) {
            if (typeof controller.apiurl !== 'undefined') {
                request = FwModule.getFormModel($form, false);
            } else {
                request = {
                    module: module,
                    mode: $form.attr('data-mode'),
                    ids: FwModule.getFormUniqueIds($form),
                    fields: FwModule.getFormFields($form, false)
                };
            }
            FwServices.module.method(request, module, 'Save', $form, function (response) {
                var $formfields, $browse;

                if (typeof controller.apiurl !== 'undefined') {
                    if (parameters.closetab === false) {
                        //Refresh the browse window on saving a record.
                        $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
                        if ($browse.length > 0) {
                            FwBrowse.databind($browse);
                        }

                        var tabname = (typeof response.tabname === 'string') ? response.tabname : (typeof response.RecordTitle === 'string') ? response.RecordTitle : 'Unknown';
                        $tab.find('.caption').html(tabname);
                        $tab.find('.modified').html('');

                        if ($form.attr('data-mode') === 'NEW') {
                            $form.attr('data-mode', 'EDIT');
                            $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                            $form.find('.submenu-btn').css({ 'pointer-events': 'auto', 'color': '' });
                        } else {
                            $formfields = $form.data('fields');
                        }
                        FwFormField.loadForm($formfields, response);
                        $form.attr('data-modified', 'false');
                        if (typeof controller['afterLoad'] === 'function') {
                            controller['afterLoad']($form);
                        }
                        if (typeof controller['afterSave'] === 'function') {
                            controller['afterSave']($form);
                        }
                    } else if (parameters.closetab) {
                        var issubmodule, $parenttab;
                        issubmodule = $tab.hasClass('submodule');
                        if (issubmodule === true) {
                            $parenttab = jQuery('#' + $tab.data('parenttabid'));
                        }
                        FwModule.beforeCloseForm($form);
                        FwModule.closeFormTab($tab, parameters.refreshRootTab);
                        if (typeof parameters.afterCloseForm === 'function') {
                            parameters.afterCloseForm();
                        }
                        if ((issubmodule === false) || (typeof parameters.closeparent === 'undefined')) {
                            if ((typeof parameters.navigationpath === 'string') && (parameters.navigationpath !== '')) {
                                program.getModule(parameters.navigationpath);
                            }
                        } else if ((issubmodule === true) && (parameters.closeparent === true)) {
                            $parenttab.find('.delete').click();
                        }
                    }
                    $form.find('.error').removeClass('error')
                    FwNotification.renderNotification('SUCCESS', 'Record saved.');
                } else if (response.saved === true) {
                    if (parameters.closetab === false) {
                        //Refresh the browse window on saving a record.
                        $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
                        if ($browse.length > 0) {
                            FwBrowse.databind($browse);
                        }

                        $tab.find('.caption').html(response.tabname);
                        $tab.find('.modified').html('');
                        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled')

                        if ($form.attr('data-mode') === 'NEW') {
                            $form.attr('data-mode', 'EDIT');
                            $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                        } else {
                            $formfields = $form.data('fields');
                        }
                        FwFormField.loadForm($formfields, response.tables);
                        $form.attr('data-modified', 'false');
                        if (typeof controller['afterLoad'] === 'function') {
                            controller['afterLoad']($form);
                        }
                        if (typeof controller['afterSave'] === 'function') {
                            controller['afterSave']($form);
                        }
                    } else if (parameters.closetab === true) {
                        var issubmodule, $parenttab;
                        issubmodule = $tab.hasClass('submodule');
                        if (issubmodule === true) {
                            $parenttab = jQuery('#' + $tab.data('parenttabid'));
                        }
                        FwModule.beforeCloseForm($form);
                        FwModule.closeFormTab($tab, parameters.refreshRootTab);
                        if (typeof parameters.afterCloseForm === 'function') {
                            parameters.afterCloseForm();
                        }
                        if ((issubmodule === false) || (typeof parameters.closeparent === 'undefined')) {
                            if ((typeof parameters.navigationpath === 'string') && (parameters.navigationpath !== '')) {
                                program.getModule(parameters.navigationpath);
                            }
                        } else if ((issubmodule === true) && (parameters.closeparent === true)) {
                            $parenttab.find('.delete').click();
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
    }
    //----------------------------------------------------------------------------------------------
    static deleteRecord(module: string, $control: JQuery) {
        var controller, method, $browse, ids, $selectedRow, $form, $tab, request;
        try {
            $browse = $control;
            $selectedRow = $browse.find('tr.selected');
            if ($selectedRow.length > 0) {
                ids = {};
                var $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Are you sure you want to delete this record?');
                var $yes = FwConfirmation.addButton($confirmation, 'Yes');
                var $no = FwConfirmation.addButton($confirmation, 'No');

                $yes.on('click', function () {
                    controller = $browse.attr('data-controller');
                    ids = FwBrowse.getRowFormUniqueIds($browse, $selectedRow);
                    request = {
                        module: window[controller].Module,
                        ids: ids
                    };
                    FwServices.module.method(request, window[controller].Module, 'Delete', $browse, function (response) {
                        $form = FwModule.getFormByUniqueIds(ids);
                        if ((typeof $form != 'undefined') && ($form.length > 0)) {
                            $tab = jQuery('#' + $form.closest('div.tabpage').attr('data-tabid'));
                            FwModule.closeFormTab($tab, true);
                        }
                        FwBrowse.databind($browse);
                    });
                });
            } else {
                FwNotification.renderNotification('WARNING', 'Please select a row.');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    static addFormMenu($form: JQuery) {
        var controller, $menu, $save, $edit, $delete,
            nodeModule, nodeForm, nodeFormMenuBar, nodeFormSubMenu, $submenubtn, $menubarbutton, nodetype,
            nodeSubMenuGroup, $submenucolumn, $submenugroup, nodeSubMenuItem, $submenuitem, hasClickEvent;

        controller = $form.attr('data-controller');
        $menu = FwMenu.getMenuControl('default');

        nodeModule = FwApplicationTree.getNodeByController(controller);
        if (nodeModule !== null) {
            nodeForm = FwApplicationTree.getChildByType(nodeModule, 'Form');
            if (nodeForm !== null) {
                nodeFormMenuBar = FwApplicationTree.getChildByType(nodeForm, 'MenuBar');
                if (nodeFormMenuBar !== null) {
                    if (nodeFormMenuBar.properties.visible === 'T') {
                        $form.find('.fwform-menu').append($menu);
                        for (var menubaritemno = 0; menubaritemno < nodeFormMenuBar.children.length; menubaritemno++) {
                            var nodeMenuBarItem = nodeFormMenuBar.children[menubaritemno];
                            if (nodeMenuBarItem.properties.visible === 'T') {
                                nodetype = FwApplicationTree.getNodeType(nodeMenuBarItem);
                                switch (nodetype) {
                                    case 'SubMenu':
                                        nodeFormSubMenu = nodeMenuBarItem;
                                        if (nodeFormSubMenu !== null) {
                                            $submenubtn = FwMenu.addSubMenu($menu);
                                            for (var submenuoptionno = 0; submenuoptionno < nodeFormSubMenu.children.length; submenuoptionno++) {
                                                nodeSubMenuGroup = nodeFormSubMenu.children[submenuoptionno];
                                                if (nodeSubMenuGroup.properties.visible === 'T') {
                                                    $submenucolumn = FwMenu.addSubMenuColumn($submenubtn)
                                                    $submenugroup = FwMenu.addSubMenuGroup($submenucolumn, nodeSubMenuGroup.properties.caption, nodeSubMenuGroup.id);
                                                    for (var submenuitemno = 0; submenuitemno < nodeSubMenuGroup.children.length; submenuitemno++) {
                                                        nodeSubMenuItem = nodeSubMenuGroup.children[submenuitemno];
                                                        if (nodeSubMenuItem.properties.visible === 'T') {
                                                            $submenuitem = FwMenu.addSubMenuBtn($submenugroup, nodeSubMenuItem.properties.caption, nodeSubMenuItem.id);
                                                            hasClickEvent = ((typeof controller === 'string') &&
                                                                (controller.length > 0) &&
                                                                (typeof FwApplicationTree.clickEvents !== 'undefined') &&
                                                                (typeof FwApplicationTree.clickEvents['{' + nodeSubMenuItem.id + '}'] === 'function'));
                                                            if (hasClickEvent) {
                                                                $submenuitem.on('click', FwApplicationTree.clickEvents['{' + nodeSubMenuItem.id + '}']);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 'MenuBarButton':
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.on('click', FwApplicationTree.clickEvents['{' + nodeMenuBarItem.id + '}']);
                                        break;
                                    case 'SaveMenuBarButton':
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.attr('data-type', 'SaveMenuBarButton');
                                        $menubarbutton.addClass('disabled');
                                        $menubarbutton.on('click', function (event) {
                                            var method, ismodified;
                                            try {
                                                method = 'saveForm';
                                                ismodified = $form.attr('data-modified');
                                                if (ismodified === 'true') {
                                                    if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                                                    if (typeof window[controller][method] === 'function') {
                                                        window[controller][method]($form, { closetab: false });
                                                    } else {
                                                        FwModule[method](window[controller].Module, $form, { closetab: false });
                                                    }
                                                }
                                            } catch (ex) {
                                                FwFunc.showError(ex);
                                            }
                                        });
                                        break;
                                    case 'PrevMenuBarButton':
                                        if (nodeMenuBarItem.properties.visible === 'T') {
                                            var $prev = FwMenu.addStandardBtn($menu, '<', nodeMenuBarItem.id);
                                            $prev.attr('data-type', 'PrevButton');
                                            $prev.on('click', function () {
                                                var $this, $browse, $tab, $selectedrow;
                                                try {
                                                    $this = jQuery(this);
                                                    $browse = $this.closest('.tabpages').find('[data-tabtype="BROWSE"] .fwbrowse');
                                                    $tab = FwTabs.getTabByElement($this);
                                                    FwBrowse.openPrevRow($browse, $tab, $form);
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
                                        }
                                        break;
                                    case 'NextMenuBarButton':
                                        if (nodeMenuBarItem.properties.visible === 'T') {
                                            var $next = FwMenu.addStandardBtn($menu, '>', nodeMenuBarItem.id);
                                            $next.attr('data-type', 'NextButton');
                                            $next.on('click', function () {
                                                var $this, $browse, $tab, $selectedrow;
                                                try {
                                                    $this = jQuery(this);
                                                    $browse = $this.closest('.tabpages').find('[data-tabtype="BROWSE"] .fwbrowse');
                                                    $tab = FwTabs.getTabByElement($this);
                                                    FwBrowse.openNextRow($browse, $tab, $form);
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        if ((typeof $form.attr('data-controller') != 'undefined') && (typeof window[$form.attr('data-controller')]['addFormMenuItems'] === 'function')) {
            var $menuobj = window[$form.attr('data-controller')]['addFormMenuItems']($menu, $form);
            if (typeof $menuobj !== 'undefined') {
                $menu = $menuobj;
            }
        }

        FwControl.renderRuntimeControls($menu.find('.fwcontrol').addBack());
    }
    //----------------------------------------------------------------------------------------------
    static beforeCloseForm($form: JQuery) {
        var $fwformfields;

        $fwformfields = typeof $form.data('fields') !== 'undefined' ? $form.data('fields') : jQuery([]);
        $fwformfields.each(function (index, element) {
            FwFormField.onRemove(jQuery(element));
        });
    }
    //----------------------------------------------------------------------------------------------
    static closeForm($form: JQuery, $tab: JQuery, navigationpath?: string, afterCloseForm?: Function, closeParent?: boolean) {
        var $tabcontrol, ismodified, hassubmodule, issubmodule, $confirmation, $save, $dontsave, $cancel, tabname, $parenttab;
        $tabcontrol = $tab.closest('.fwtabs');
        ismodified = $form.attr('data-modified');
        hassubmodule = (typeof $tab.data('subtabids') !== 'undefined') && ($tab.data('subtabids').length > 0);
        issubmodule = $tab.hasClass('submodule')

        if (issubmodule) {
            $parenttab = jQuery('#' + $tab.data('parenttabid'));
        }

        if (hassubmodule) {
            var $submoduletab, $submoduleform, subtabids;
            subtabids = $tab.data('subtabids');
            $submoduletab = jQuery('#' + subtabids[subtabids.length - 1]);
            $submoduleform = jQuery('#' + $submoduletab.attr('data-tabpageid')).find('.fwform');
            FwModule.closeForm($submoduleform, $submoduletab, navigationpath, null, true);
        } else {
            if (ismodified === 'true') {
                if ($form.parent().data('type') === 'settings-row') {
                    tabname = $form.data('caption')
                } else {
                    tabname = $tab.find('.caption').html();
                }
                $confirmation = FwConfirmation.renderConfirmation('Close Tab', 'Want to save your changes to "' + tabname + '"?');
                $save = FwConfirmation.addButton($confirmation, 'Save');
                $dontsave = FwConfirmation.addButton($confirmation, 'Don\'t Save');
                if ($form.parent().data('type') !== 'settings-row') { $cancel = FwConfirmation.addButton($confirmation, 'Cancel'); }

                $save.on('click', function () {
                    var controller, isvalid;
                    controller = $form.attr('data-controller');
                    if (typeof window[controller] === 'undefined') throw 'Missing javascript module controller: ' + controller;
                    if (typeof window[controller]['saveForm'] === 'function') {
                        window[controller]['saveForm']($form, { closetab: true, navigationpath: navigationpath, closeparent: closeParent, afterCloseForm: afterCloseForm, refreshRootTab: true });
                    }
                });
                $dontsave.on('click', function () {
                    FwModule.beforeCloseForm($form);
                    FwModule.closeFormTab($tab, false);
                    if (typeof afterCloseForm === 'function') {
                        afterCloseForm();
                    }
                    if ((!issubmodule) || (typeof closeParent === 'undefined')) {
                        if ((typeof navigationpath === 'string') && (navigationpath !== '')) {
                            program.getModule(navigationpath);
                        }
                    } else if ((issubmodule) && (closeParent)) {
                        $parenttab.find('.delete').click();
                    }
                });
            } else {
                FwModule.beforeCloseForm($form);
                FwModule.closeFormTab($tab, false);
                if (typeof afterCloseForm === 'function') {
                    afterCloseForm();
                }
                if ((issubmodule) && (closeParent)) {
                    $parenttab.find('.delete').click();
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    static closeFormTab($tab: JQuery, refreshRootTab?: boolean) {
        var $browse, $newTab, newTabType, tabIsActive, $tabcontrol, $tabpage, isSubModule;
        $tabcontrol = $tab.closest('.fwtabs');
        $tabpage = $tabcontrol.find('#' + $tab.attr('data-tabpageid'));
        isSubModule = $tab.hasClass('submodule');

        if (isSubModule) {
            var $parenttab, subtabids;
            $parenttab = jQuery(`#${$tab.data('parenttabid')}`);
            if ($parenttab.length > 0) {
                subtabids = $parenttab.data('subtabids');
                subtabids = jQuery.grep(subtabids, function (value) {
                    return value != $tab.attr('id');
                });
                $parenttab.data('subtabids', subtabids);
            }
        }

        $newTab = (($tab.next().length > 0) ? $tab.next() : $tab.prev());
        tabIsActive = $tab.hasClass('active');
        FwTabs.removeTab($tab);
        if (tabIsActive) {
            FwTabs.setActiveTab($tabcontrol, $newTab);
            newTabType = $newTab.attr('data-tabtype');
            if (newTabType === 'BROWSE') {
                $tabpage = $tabcontrol.find(`#${$newTab.attr('data-tabpageid')}`);
                $browse = $tabpage.find('.fwbrowse[data-type="Browse"]');
                if (refreshRootTab) {
                    FwBrowse.databind($browse);
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    static loadAudit($form: JQuery, uniqueid: string) {
        if (FwSecurity.isUser()) {
            FwAudit.loadAudit($form, uniqueid);
        }
    }
    //----------------------------------------------------------------------------------------------
    static getFormUniqueIds($form: JQuery) {
        var $uniqueIdFields, uniqueids, uniqueid;

        uniqueids = {};
        $uniqueIdFields = typeof $form.data('uniqueids') !== 'undefined' ? $form.data('uniqueids') : jQuery([]);
        $uniqueIdFields.each(function (index, element) {
            var $uniqueIdField, $input, value, datafield;

            $uniqueIdField = jQuery(element);
            $input = $uniqueIdField.find('input');
            value = $input.val();
            datafield = $uniqueIdField.attr('data-datafield');
            uniqueid = {
                datafield: datafield,
                value: value
            };
            uniqueids[datafield] = uniqueid;
        });

        return uniqueids;
    }
    //----------------------------------------------------------------------------------------------
    static getFormFields($form: JQuery, getAllFieldsOverride: boolean) {
        var $fwformfields, fields, field, displayField;

        fields = {};
        $fwformfields = typeof $form.data('fields') !== 'undefined' ? $form.data('fields') : jQuery([]);
        $fwformfields.each(function (index, element) {
            var $fwformfield, originalValue, dataField, value, isValidDataField, getAllFields, isBlank, isCalculatedField,
                validationDisplayField, validationDisplayValue;

            $fwformfield = jQuery(element);
            originalValue = $fwformfield.attr('data-originalvalue');
            dataField = $fwformfield.attr('data-datafield');
            if (typeof dataField === 'undefined') {
                var formCaption = typeof $form.attr('data-caption') !== 'undefined' ? $form.attr('data-caption') : 'Unknown';
                console.log('On Form: "' + formCaption + ' ", the attribute data-datafield is required on the fwformfield with the following html: ' + jQuery('div').append($fwformfield).html());
                throw 'Attribute data-datafield is missing on fwformfield element.';
            }
            value = FwFormField.getValue2($fwformfield);
            let value2 = value
            if (typeof value === 'number' || typeof value === 'boolean') {
                value2 = value.toString();
            } else if (typeof value === 'object') {
                value2 = JSON.stringify(value);
            }

            isBlank = (dataField === '');
            isCalculatedField = (dataField[0] === '#') && (dataField[1] === '.');
            isValidDataField = (!isBlank) && (!isCalculatedField);
            getAllFields = ($form.attr('data-mode') === 'NEW') || getAllFieldsOverride;

            if ((isValidDataField) && ((getAllFields) || (originalValue !== value2))) {
                if ($fwformfield.data('customfield') !== undefined && $fwformfield.data('customfield') === true) {
                    field = {
                        FieldName: dataField,
                        FieldValue: value
                    }
                    if (typeof fields._Custom === 'undefined') {
                        fields._Custom = [];
                    }
                    fields._Custom.push(field);
                } else {
                    field = {
                        datafield: dataField,
                        value: value
                    };
                    fields[dataField] = field;
                }

                if ($fwformfield.attr('data-type') === 'validation') {
                    validationDisplayField = $fwformfield.attr('data-displayfield');
                    if (validationDisplayField != dataField) {
                        validationDisplayValue = FwFormField.getText2($fwformfield);
                        displayField = {
                            datafield: validationDisplayField,
                            value: validationDisplayValue
                        }
                        fields[validationDisplayField] = displayField;
                    }
                }
            }
        });

        return fields;
    }
    //----------------------------------------------------------------------------------------------
    static getWebApiFields($form: JQuery, includeUnmodifiedFields: boolean) {
        var fields = {};
        var $uniqueids = typeof $form.data('uniqueids') !== 'undefined' ? $form.data('uniqueids') : jQuery([]);
        var $fwformfields = typeof $form.data('fields') !== 'undefined' ? $form.data('fields') : jQuery([]);
        $fwformfields = $fwformfields.add($uniqueids);
        $fwformfields.each(function (index, element) {
            var $fwformfield, originalValue, dataField, value, isValidDataField, getAllFields, isBlank, isCalculatedField;

            $fwformfield = jQuery(element);
            originalValue = $fwformfield.attr('data-originalvalue');
            dataField = $fwformfield.attr('data-datafield');
            if (typeof dataField === 'undefined') {
                var formCaption = typeof $form.attr('data-caption') !== 'undefined' ? $form.attr('data-caption') : 'Unknown';
                console.log('On Form: "' + formCaption + ' ", the attribute data-datafield is required on the fwformfield with the following html: ' + jQuery('div').append($fwformfield).html());
                throw 'Attribute data-datafield is missing on fwformfield element.';
            }
            value = FwFormField.getValue2($fwformfield);

            isBlank = (dataField === '');
            isCalculatedField = (dataField[0] === '#') && (dataField[1] === '.');
            isValidDataField = (!isBlank) && (!isCalculatedField);
            getAllFields = ($form.attr('data-mode') === 'NEW') || includeUnmodifiedFields;

            if ((isValidDataField) && ((getAllFields) || (originalValue !== value))) {
                fields[dataField] = value;
            }
        });

        return fields;
    }
    //----------------------------------------------------------------------------------------------
    static getFormModel($form: JQuery, getAllFieldsOverride: boolean) {
        let uniqueids = FwModule.getFormUniqueIds($form);
        let fields = FwModule.getFormFields($form, getAllFieldsOverride);
        let request = {};
        for (let key in uniqueids) {
            request[key] = uniqueids[key].value;
        }
        for (let key in fields) {
            if (key === '_Custom') {
                request[key] = fields[key];
            } else {
                request[key] = fields[key].value;
            }
        }
        if (typeof $form.data('beforesave') === 'function') {
            $form.data('beforesave')(request);
        }
        return request;
    }
    //----------------------------------------------------------------------------------------------
    static validateForm($form: JQuery) {
        var isvalid, $fields;

        isvalid = true;

        if ($form.parent().data('type') === 'settings-row') {
            $form.data('fields', $form.find('.fwformfield'))
        }

        $fields = $form.data('fields');

        $fields.each(function (index) {
            var $field = jQuery(this);

            if (($field.attr('data-required') == 'true') && ($field.attr('data-enabled') == 'true')) {
                if ($field.find('.fwformfield-value').val() == '') {
                    var errorTab = $field.closest('.tabpage').attr('data-tabid');
                    isvalid = false;
                    $field.addClass('error');
                    $field.parents('.fwcontrol .fwtabs').find('#' + errorTab).addClass('error');
                } else if ($field.find('.fwformfield-value').val() != '') {
                    $field.removeClass('error');
                }
            }
            if (($field.attr('data-noduplicate') == 'true') && ($field.hasClass('error'))) {
                isvalid = false;
            }
            if ($field.hasClass('error')) {
                isvalid = false;
            }
            if (isvalid) {
                $field.removeClass('error');
            }
        });

        if (!isvalid) {
            FwNotification.renderNotification('ERROR', 'Please resolve the error(s) on the form.');
        }

        return isvalid;
    }
    //----------------------------------------------------------------------------------------------
    static getData($object: JQuery, request: any, responseFunc: Function, $elementToBlock: JQuery, timeout?: number) {
        var webserviceurl, controller, module, timeoutParam;
        controller = $object.attr('data-controller');
        module = window[controller].Module;
        request.module = module;
        webserviceurl = 'services.ashx?path=/module/' + module + '/GetData';
        if (typeof timeout !== 'number') {
            timeoutParam = null;
        }
        FwAppData.jsonPost(true, webserviceurl, request, timeoutParam, responseFunc, null, $elementToBlock);
    }
    //----------------------------------------------------------------------------------------------
    static getData2(module: string, request: any, responseFunc: Function, $elementToBlock: JQuery, timeout?: number) {
        var webserviceurl, timeoutParam;
        request.module = module;
        webserviceurl = 'services.ashx?path=/module/' + module + '/GetData';
        if (typeof timeout !== 'number') {
            timeoutParam = null;
        }
        FwAppData.jsonPost(true, webserviceurl, request, timeoutParam, responseFunc, null, $elementToBlock);
    }
    //----------------------------------------------------------------------------------------------
    static getFormByUniqueIds(uniqueidcollection: any) {
        var $forms, $form, result;
        result = jQuery();
        $forms = jQuery('.fwform');
        $forms.each(function (index, element) {
            var tabid, formuniqueids, didFindAllUniqueIds;
            $form = jQuery(element);
            formuniqueids = FwModule.getFormUniqueIds($form);
            didFindAllUniqueIds = false;
            for (var formuniqueid in formuniqueids) {
                var didFindUniqueId = false;
                for (var formuniqueidname in uniqueidcollection) {
                    if ((formuniqueidname === formuniqueid) && (uniqueidcollection[formuniqueidname].value === formuniqueids[formuniqueid].value)) {
                        didFindUniqueId = true;
                        break;
                    }
                }
                didFindAllUniqueIds = (didFindAllUniqueIds || didFindUniqueId);
            }
            if (didFindAllUniqueIds) {
                result = $form;
                return false;
            }
        });
        return result;
    }
    //----------------------------------------------------------------------------------------------
    static checkDuplicate($form: JQuery, $fieldtocheck: JQuery) {
        var $fields, request: any = {}, groupname, $field, datafield, value, type, table, runcheck = true, controller, required;
        controller = $form.attr('data-controller');
        if (typeof window[controller].Module !== 'undefined') {
            //($fieldtocheck.find('input.fwformfield-value').val() != '') &&
            //($fieldtocheck.find('input.fwformfield-value').val().toUpperCase() != $fieldtocheck.attr('data-originalvalue').toUpperCase())) {     //2015-12-21 MY: removed logic to support non required duplicategroup fields.
            request.module = window[controller].Module;
            request.fields = {};
            request.table = $fieldtocheck.attr('data-datafield').split('.')[0];
            $fields = $fieldtocheck;
            if ((typeof $fieldtocheck.attr('data-duplicategroup') !== 'undefined') && ($fieldtocheck.attr('data-duplicategroup') != '')) {
                groupname = $fieldtocheck.attr('data-duplicategroup');
                $fields = $form.find('div[data-duplicategroup="' + groupname + '"]');
            }
            $fields.each(function (index, element) {
                $field = jQuery(element);
                $field.removeClass('error');
                datafield = $field.attr('data-datafield');
                table = datafield.split('.')[0];
                value = FwFormField.getValue2($field);
                type = $field.attr('data-type');
                required = $field.attr('data-required');
                request.fields[datafield] = { datafield: datafield, value: value, type: type };

                if ((required == 'true') && (value == '')) {
                    runcheck = false;
                } else if (typeof typeof window[controller] !== 'undefined' && typeof typeof window[controller].apiurl === 'undefined' && request.table != table) {
                    runcheck = false;
                    FwNotification.renderNotification('ERROR', 'Fields are not part of the same table.');
                }
            });
            runcheck = runcheck && typeof controller.apiurl === 'undefined';
            if (runcheck) {
                FwServices.module.method(request, window[controller].Module, 'ValidateDuplicate', $form,
                    // onSuccess
                    function (response) {
                        try {
                            if ((typeof controller.apiurl === 'undefined' && response.duplicate == true) || (typeof controller.apiurl !== 'undefined' && response)) {
                                $fields.addClass('error');
                                FwNotification.renderNotification('ERROR', 'Duplicate ' + $fields.attr('data-caption') + '(s) are not allowed.');
                            }
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                    // onError
                    , function (errorMessage) {
                        try {
                            FwFunc.showError('MiddleTier: ' + errorMessage);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                );
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    static setFormReadOnly($form: JQuery) {
        var $fwformfields, $grids, $save;

        $form.attr('data-mode', 'READONLY');
        $fwformfields = $form.data('fields');
        FwFormField.disable($fwformfields);
        $grids = $form.data('grids');
        $grids.each(function (index, element) {
            var $grid = jQuery(element).find('div[data-control="FwBrowse"][data-type="Grid"]');
            FwBrowse.disableGrid($grid);
        });

        $save = $form.find('div.btn[data-type="SaveMenuBarButton"]');
        $save.addClass('disabled');
        $save.off('click');
    }
    //----------------------------------------------------------------------------------------------
    static loadFormFromTemplate(modulename: string) {
        var $control = jQuery(jQuery('#tmpl-modules-' + modulename + 'Form').html());
        return $control;
    }
    //----------------------------------------------------------------------------------------------
    static refreshForm($form: JQuery, controller: any) {
        const uniqueIds = FwModule.getFormUniqueIds($form);
        let newUniqueIds = {};
        setTimeout(() => {
            for (let key in uniqueIds) {
                newUniqueIds[key] = uniqueIds[key].value
                const $newForm = controller.loadForm(newUniqueIds);
                $form.parent().empty().append($newForm);
            }
        }, 0)
    };
    //----------------------------------------------------------------------------------------------
    static loadCustomFields($form, customModuleName) {
        var customHtml = [];
        var $formTabControl = jQuery($form.find('.fwtabs'));
        var customTabIds = FwTabs.addTab($formTabControl, 'Custom Fields', false, 'CUSTOM', false);

        FwAppData.apiMethod(true, 'GET', 'api/v1/customfield', null, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                customHtml.push('<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Custom Fields">')
                for (var j = 0; j < response.length; j++) {
                    if (customModuleName === response[j].ModuleName) {
                        customHtml.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        if (response[j].FieldSizeInPixels > 0) {
                            customHtml.push('<div data-control="FwFormField" data-customfield="true" data-type="' + response[j].ControlType + '" class="fwcontrol fwformfield" data-caption="' + response[j].FieldName + '" data-datafield="' + response[j].FieldName + '" data-digits="' + response[j].FloatDecimalDigits + '" style="width:' + response[j].FieldSizeInPixels + 'px;float:left;" data-digitsoptional="false"></div>');
                        } else {
                            customHtml.push('<div data-control="FwFormField" data-customfield="true" data-type="' + response[j].ControlType + '" class="fwcontrol fwformfield" data-caption="' + response[j].FieldName + '" data-datafield="' + response[j].FieldName + '" data-digits="' + response[j].FloatDecimalDigits + '" data-digitsoptional="false"></div>');
                        }
                        customHtml.push('</div>');
                    }
                }
                customHtml.push('</div>');

                var $customControl = jQuery(customHtml.join(''));
                FwControl.renderRuntimeControls($customControl.find('.fwcontrol').addBack());

                $formTabControl.find('#' + customTabIds.tabpageid).append($customControl);

                $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
                if (typeof $form.data('afterLoadCustomFields') !== 'undefined' && typeof $form.data('afterLoadCustomFields') === 'function') {
                    $form.data('afterLoadCustomFields')();
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
}