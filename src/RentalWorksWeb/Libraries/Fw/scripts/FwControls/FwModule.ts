class FwModule {
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
            $tabControl.find('.closetabbutton').html('');
            const iconHtml: Array<string> = [];
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
            const $closeTabButton = jQuery(iconHtml.join(''));
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
        if (sessionStorage.getItem('customForms') !== null) {
            const controller = $browse.attr('data-controller');
            const baseForm = controller.replace('Controller', 'Browse');
            const customForms = JSON.parse(sessionStorage.getItem('customForms')).filter(a => a.BaseForm == baseForm);
            if (customForms.length > 0) {
                $browse = jQuery(jQuery(`#tmpl-custom-${baseForm}`)[0].innerHTML);
            }
        }

        FwControl.renderRuntimeControls($browse.find('.fwcontrol').addBack());
        FwModule.addBrowseMenu($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    static addBrowseMenu($browse: JQuery) {
        var controller, $menu, $edit, $vr, $submenubtn, $submenucolumn, nodeModule, nodeBrowse, nodeBrowseMenuBar, nodeBrowseSubMenu, $submenubtn, $menubarbutton, nodeSubMenuGroup, $submenucolumn, $submenugroup, nodeSubMenuItem, hasClickEvent, $editbtn;

        controller = $browse.attr('data-controller');
        $menu = FwMenu.getMenuControl('default');

        nodeModule = FwApplicationTree.getNodeByController(controller);
        if (nodeModule !== null) {
            nodeBrowse = FwApplicationTree.getChildByType(nodeModule, 'Browse');
            if (nodeBrowse !== null) {
                nodeBrowseMenuBar = FwApplicationTree.getChildByType(nodeBrowse, 'MenuBar');
                nodeBrowseMenuBar = this.addFindMenuButton($browse, nodeBrowseMenuBar);
                if (nodeBrowseMenuBar !== null) {
                    if (nodeBrowseMenuBar.properties.visible === 'T') {
                        $browse.find('.fwbrowse-menu').append($menu);
                        for (var menubaritemno = 0; menubaritemno < nodeBrowseMenuBar.children.length; menubaritemno++) {
                            var nodeMenuBarItem = nodeBrowseMenuBar.children[menubaritemno];
                            if (nodeMenuBarItem.properties.visible === 'T') {
                                switch (FwApplicationTree.getNodeType(nodeMenuBarItem)) {
                                    case 'SubMenu':
                                        nodeBrowseSubMenu = nodeMenuBarItem;
                                        $submenubtn = FwMenu.addSubMenu($menu);
                                        for (let submenuoptionno = 0; submenuoptionno < nodeBrowseSubMenu.children.length; submenuoptionno++) {
                                            nodeSubMenuGroup = nodeBrowseSubMenu.children[submenuoptionno];
                                            if (nodeSubMenuGroup.properties.visible === 'T') {
                                                $submenucolumn = FwMenu.addSubMenuColumn($submenubtn)
                                                $submenugroup = FwMenu.addSubMenuGroup($submenucolumn, nodeSubMenuGroup.properties.caption, nodeSubMenuGroup.id);
                                                for (let submenuitemno = 0; submenuitemno < nodeSubMenuGroup.children.length; submenuitemno++) {
                                                    nodeSubMenuItem = nodeSubMenuGroup.children[submenuitemno];
                                                    if (nodeSubMenuItem.properties.visible === 'T') {
                                                        switch (FwApplicationTree.getNodeType(nodeSubMenuItem)) {
                                                            case 'SubMenuItem':
                                                                const $submenuitem = FwMenu.addSubMenuBtn($submenugroup, nodeSubMenuItem.properties.caption, nodeSubMenuItem.id);
                                                                hasClickEvent = ((typeof controller === 'string') &&
                                                                    (controller.length > 0) &&
                                                                    (typeof FwApplicationTree.clickEvents !== 'undefined') &&
                                                                    (typeof FwApplicationTree.clickEvents[`{${nodeSubMenuItem.id}}`] === 'function'));
                                                                if (hasClickEvent) {
                                                                    $submenuitem.on('click', FwApplicationTree.clickEvents[`{${nodeSubMenuItem.id}}`]);
                                                                }
                                                                break;
                                                            case 'DownloadExcelSubMenuItem':
                                                                const $excelSubMenuItem = FwMenu.addSubMenuBtn($submenugroup, nodeSubMenuItem.properties.caption, nodeSubMenuItem.id);
                                                                $excelSubMenuItem.on('click', () => {
                                                                    try {
                                                                        FwBrowse.downloadExcelWorkbook($browse, controller);
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
                                                if (typeof (<any>window[controller]).openForm !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                                                $form = (<any>window[controller]).openForm('NEW');
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
                                                FwModule['deleteRecord']((<any>window[controller]).Module, $browse);
                                            }
                                        });
                                        break;
                                    case 'FindMenuBarButton':
                                        let loaded = false;
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $browse = $menubarbutton.closest('.fwbrowse');

                                        $menubarbutton.append(`
                                        <div class="findbutton-dropdown">
                                            <div class="query">
                                                <div class="queryrow">
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield find-field andor" data-caption="" data-datafield="AndOr" style="flex:1 1 auto;"></div>
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield find-field datafieldselect" data-caption="Data Field" data-datafield="Datafield" style="flex:1 1 auto;"></div>
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield find-field datafieldcomparison" data-caption="" data-datafield="DatafieldComparison" style="flex:1 1 150px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield find-field textquery" data-caption="" data-datafield="DatafieldQuery" style="flex:1 1 200px;"></div>
                                                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield find-field datequery" data-enabled="true" data-caption="" data-datafield="DateFieldQuery" style="flex:1 1 200px;display:none;"></div>
                                                    <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield find-field booleanquery" data-caption="" data-datafield="BooleanFieldQuery" style="flex:1 1 200px;display:none;"></div>
                                                    <i class="material-icons delete-query">delete_outline</i>
                                                    <i class="material-icons add-query">add_circle_outline</i>
                                                </div>
                                            </div>
                                            <div class="flexrow queryrow">
                                                <div class="find fwformcontrol querysearch" data-type="button" style="flex:0 1 50px;margin:15px 15px 10px 10px;margin-left:auto;">Apply</div>
                                            </div>
                                        </div>`);
                                        FwControl.renderRuntimeHtml($menubarbutton.find('.fwcontrol'));

                                        $menubarbutton.attr('data-type', 'FindMenuBarButton');
                                        let findFields = [];
                                        let textComparisonFields = [
                                            { value: 'like', text: 'Contains' },
                                            { value: 'startswith', text: 'Starts With' },
                                            { value: 'endswith', text: 'Ends With' },
                                            { value: '=', text: 'Equals' },
                                            { value: 'doesnotcontain', text: 'Does Not Contain' },
                                            { value: '<>', text: 'Does Not Equal' }
                                        ];
                                        let numericComparisonFields = [
                                            { value: '=', text: '=' },
                                            { value: '>', text: '>' },
                                            { value: '>=', text: '≥' },
                                            { value: '<', text: '<' },
                                            { value: '<=', text: '≤' },
                                            { value: '<>', text: '≠' },
                                        ];
                                        let booleanComparisonFields = [
                                            { value: '=', text: 'Equals' },
                                            { value: '<>', text: 'Does Not Equal' }
                                        ];
                                        let dateComparisonFields = [
                                            { value: '=', text: 'Equals' },
                                            { value: '<', text: 'Prior To' },
                                            { value: '<=', text: 'Prior To or Equals' },
                                            { value: '>', text: 'Later Than' },
                                            { value: '>=', text: 'Later Than or Equals' },
                                            { value: '<>    ', text: 'Does Not Equal' },
                                        ];


                                        $menubarbutton.on('click', function (e) {
                                            controller = $browse.attr('data-controller');
                                            let maxZIndex;
                                            let $this = jQuery(this);
                                            e.preventDefault();
                                            if (!loaded) {
                                                FwAppData.apiMethod(true, 'GET', (<any>window[controller]).apiurl + '/emptyobject', null, FwServices.defaultTimeout, function onSuccess(response) {
                                                    let dateField = $browse.find('.datequery');
                                                    let textField = $browse.find('.textquery');
                                                    let booleanField = $browse.find('.booleanquery');

                                                    for (var j = 0; j < response._Custom.length; j++) {
                                                        findFields.push({
                                                            'value': response._Custom[j].FieldName,
                                                            'text': response._Custom[j].FieldName,
                                                            'type': response._Custom[j].FieldType,
                                                            'filterval': (response._Custom[j].FieldName).toUpperCase()
                                                        })
                                                    }

                                                    for (var i = 0; i < response._Fields.length; i++) {
                                                        findFields.push({
                                                            'value': response._Fields[i].Name,
                                                            'text': response._Fields[i].Name,
                                                            'type': response._Fields[i].DataType,
                                                            'filterval': (response._Fields[i].Name).toUpperCase()
                                                        })
                                                    }
                                                    findFields.sort(function (a, b) { return (a.text > b.text) ? 1 : ((b.text > a.text) ? -1 : 0); });
                                                    window['FwFormField_select'].loadItems($browse.find('.datafieldselect'), findFields, false);
                                                    window['FwFormField_select'].loadItems($browse.find('.andor'), [{ value: 'and', text: 'And' }, { value: 'or', text: 'Or' }], true);
                                                    window['FwFormField_select'].loadItems(booleanField, [{ value: 'T', text: 'true' }, { value: 'F', text: 'false' }], true);
                                                    $browse.find('.datafieldselect').on('change', function () {
                                                        let datatype = jQuery(this).find(':selected').data('type');
                                                        dateField.hide();
                                                        textField.hide();
                                                        booleanField.hide();
                                                        switch (datatype) {
                                                            case 'Text':
                                                                textField.show();
                                                                window['FwFormField_select'].loadItems(jQuery($browse.find('.datafieldcomparison')[0]), textComparisonFields, true);
                                                                break;
                                                            case 'Integer':
                                                            case 'Decimal':
                                                            case 'Float':
                                                                textField.show();
                                                                window['FwFormField_select'].loadItems(jQuery($browse.find('.datafieldcomparison')[0]), numericComparisonFields, true);
                                                                break;
                                                            case 'True/False':
                                                            case 'Boolean':
                                                                booleanField.show();
                                                                window['FwFormField_select'].loadItems(jQuery($browse.find('.datafieldcomparison')[0]), booleanComparisonFields, true);
                                                                break;
                                                            case 'Date':
                                                                dateField.show();
                                                                window['FwFormField_select'].loadItems(jQuery($browse.find('.datafieldcomparison')[0]), dateComparisonFields, true);
                                                                break;
                                                        }
                                                    })

                                                    if (chartFilters) {
                                                        for (let i = 0; i < chartFilters.length; i++) {
                                                            let valueField;
                                                            const data = chartFilters[i];
                                                            const field = data.datafield.toUpperCase();
                                                            const type = data.type;
                                                            const $queryRow = $browse.find('.query .queryrow:last');
                                                            $queryRow.find(`[data-datafield="Datafield"] [data-filterval="${field}"]`).prop('selected', true).change();

                                                            const $comparisionField = $queryRow.find(`[data-datafield="DatafieldComparison"]`);
                                                            switch (type) {
                                                                case 'field':
                                                                    FwFormField.setValue2($comparisionField, '=');
                                                                    valueField = 'DatafieldQuery';
                                                                    break;
                                                                case 'fromdate':
                                                                    FwFormField.setValue2($comparisionField, '>=');
                                                                    valueField = 'DateFieldQuery';
                                                                    break;
                                                                case 'todate':
                                                                    FwFormField.setValue2($comparisionField, '<=');
                                                                    valueField = 'DateFieldQuery';
                                                                    break;
                                                            }

                                                            FwFormField.setValue2($queryRow.find(`[data-datafield="${valueField}"]`), data.value);
                                                            if ((i + 1) < chartFilters.length) {
                                                                $queryRow.find('.add-query').click();
                                                            }
                                                        }
                                                        $browse.find('.querysearch').click();
                                                        sessionStorage.removeItem('chartfilter');
                                                    }

                                                }, function onError(response) {
                                                    FwFunc.showError(response);
                                                }, null);

                                                loaded = true
                                            }

                                            if (!$this.hasClass('active')) {
                                                maxZIndex = FwFunc.getMaxZ('*');
                                                $this.find('.findbutton-dropdown').css('z-index', maxZIndex + 1);
                                                $this.addClass('active');

                                                jQuery(document).on('click', function closeMenu(e: any) {
                                                    let target = jQuery(e.target);
                                                    if ($menubarbutton.has(e.target).length === 0 && !jQuery(e.target).hasClass('delete-query') && target.parent().prop('tagName') !== 'TR' && !target.hasClass('year') && !target.hasClass('month') && jQuery(document.body).find('.datepicker').has(e.target).length === 0) {
                                                        $this.removeClass('active');
                                                        $this.find('.findbutton-dropdown').css('z-index', '0');
                                                        jQuery(document).off('click');
                                                    }
                                                });
                                            }
                                        });

                                        var chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
                                        if (chartFilters) $menubarbutton.click();

                                        $browse.find('.add-query').on('click', function cloneRow() {
                                            let $newRow = jQuery(this).closest('.queryrow').clone();
                                            FwControl.renderRuntimeHtml($newRow.find('.fwcontrol'));
                                            window['FwFormField_select'].loadItems($newRow.find('.datafieldselect'), findFields, false);
                                            window['FwFormField_select'].loadItems($newRow.find('.andor'), [{ value: 'and', text: 'And' }, { value: 'or', text: 'Or' }], true);
                                            window['FwFormField_select'].loadItems($newRow.find('.booleanquery'), [{ value: 'T', text: 'true' }, { value: 'F', text: 'false' }], true);
                                            let dateField = $newRow.find('.datequery');
                                            let textField = $newRow.find('.textquery');
                                            let booleanField = $newRow.find('.booleanquery');

                                            $newRow.find('.datafieldselect').on('change', function () {
                                                let datatype = jQuery(this).find(':selected').data('type');
                                                dateField.hide();
                                                textField.hide();
                                                booleanField.hide();
                                                switch (datatype) {
                                                    case 'Text':
                                                        textField.show();
                                                        window['FwFormField_select'].loadItems($newRow.find('.datafieldcomparison'), textComparisonFields, true);
                                                        break;
                                                    case 'Integer':
                                                    case 'Decimal':
                                                    case 'Float':
                                                        textField.show();
                                                        window['FwFormField_select'].loadItems($newRow.find('.datafieldcomparison'), numericComparisonFields, true);
                                                        break;
                                                    case 'True/False':
                                                    case 'Boolean':
                                                        booleanField.show();
                                                        window['FwFormField_select'].loadItems($newRow.find('.datafieldcomparison'), booleanComparisonFields, true);
                                                        break;
                                                    case 'Date':
                                                        dateField.show();
                                                        window['FwFormField_select'].loadItems($newRow.find('.datafieldcomparison'), dateComparisonFields, true);
                                                        break;
                                                }
                                            });
                                            $newRow.find('.delete-query').on('click', function () {
                                                if ($newRow.find('.add-query').css('visibility') === 'visible') {
                                                    $newRow.prev().find('.add-query').css('visibility', 'visible');
                                                }
                                                if ($newRow.find('.andor').css('visibility') === 'hidden') {
                                                    $newRow.next().find('.andor').css('visibility', 'hidden');
                                                }
                                                if ($browse.find('.query').find('.queryrow').length === 2 && $newRow.next().length !== 0) {
                                                    $newRow.next().find('.delete-query').removeAttr('style').css('visibility', 'hidden');
                                                }
                                                if ($browse.find('.query').find('.queryrow').length === 2 && $newRow.prev().length !== 0) {
                                                    $newRow.prev().find('.delete-query').removeAttr('style').css('visibility', 'hidden');
                                                }
                                                $newRow.remove();
                                            }).css('visibility', 'visible');
                                            $newRow.find('.andor').css('visibility', 'visible');
                                            $newRow.find('.add-query').on('click', cloneRow);
                                            $newRow.find('input').val('')
                                            $newRow.appendTo($browse.find('.query'));
                                            if ($browse.find('.query').find('.queryrow').length > 1) {
                                                jQuery($browse.find('.query').find('.queryrow')[0]).find('.delete-query').on('click', function () {
                                                    jQuery(this).closest('.queryrow').next().find('.andor').css('visibility', 'hidden');
                                                    jQuery(this).closest('.queryrow').remove();
                                                }).css('visibility', 'visible');
                                            }
                                            jQuery(this).css('cursor', 'default');
                                            jQuery(this).css('visibility', 'hidden');
                                        })

                                        $browse.find('.querysearch').on('click', function (e) {
                                            $browse.removeData('advancedsearchrequest')
                                            let request = FwBrowse.getRequest($browse);
                                            let advancedSearch: any = {};
                                            let queryRows = $browse.find('.query').find('.queryrow');
                                            let $find = jQuery(this).closest('.btn');
                                            advancedSearch.searchfieldoperators = [];
                                            advancedSearch.searchfieldtypes = [];
                                            advancedSearch.searchfields = [];
                                            advancedSearch.searchfieldvalues = [];
                                            advancedSearch.searchcondition = [];
                                            advancedSearch.searchseparators = [];
                                            advancedSearch.searchconjunctions = [];

                                            //adds container for read-only fields from "Find"
                                            const $browsemenu = $browse.find('.fwbrowse-menu');
                                            let $searchFields;
                                            if ($browsemenu.find('.read-only-searchfields').length === 0) {
                                                $searchFields = jQuery(`<div class="fwcontrol fwmenu default read-only-searchfields flexcolumn" style="padding-bottom:10px;"></div>`);
                                                $browsemenu.append($searchFields);
                                            } else {
                                                $searchFields = $browsemenu.find('.read-only-searchfields').empty();
                                            }

                                            for (var i = 0; i < queryRows.length; i++) {
                                                let valuefield;
                                                const comparisonText = jQuery(queryRows[i]).find('.datafieldcomparison').find(':selected').text();
                                                const comparisonfield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="DatafieldComparison"]'));
                                                const datafield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="Datafield"]'));
                                                let type = jQuery(queryRows[i]).find('.datafieldselect').find(':selected').data('type');
                                                if (datafield != '') {
                                                    advancedSearch.searchfieldtypes.push(jQuery(queryRows[i]).find('.datafieldselect').find(':selected').data('type'));
                                                    advancedSearch.searchfields.push(datafield);
                                                    switch (type) {
                                                        case 'True/False':
                                                        case 'Boolean':
                                                            valuefield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="BooleanFieldQuery"]'));
                                                            if (valuefield === 'F' && comparisonfield === '=') {
                                                                advancedSearch.searchfieldvalues.push('T');
                                                                advancedSearch.searchfieldoperators.push('<>');
                                                            } else if (valuefield === 'F' && comparisonfield === '<>') {
                                                                advancedSearch.searchfieldvalues.push('T');
                                                                advancedSearch.searchfieldoperators.push('=');
                                                            } else {
                                                                advancedSearch.searchfieldvalues.push(valuefield);
                                                                advancedSearch.searchfieldoperators.push(comparisonfield);
                                                            }
                                                            valuefield = (valuefield == 'F' ? 'FALSE' : 'TRUE');
                                                            break;
                                                        case 'Date':
                                                            valuefield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="DateFieldQuery"]'));
                                                            advancedSearch.searchfieldvalues.push(valuefield);
                                                            advancedSearch.searchfieldoperators.push(comparisonfield);
                                                            break;
                                                        default:
                                                            valuefield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="DatafieldQuery"]'));
                                                            advancedSearch.searchfieldvalues.push(valuefield);
                                                            advancedSearch.searchfieldoperators.push(comparisonfield);
                                                            break;
                                                    }
                                                    advancedSearch.searchseparators.push(',');

                                                    //adds read-only fields
                                                    const $readOnlyField = jQuery(`<div class="searchfield-row" style="display:flex;">
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Datafield" data-enabled="false" style="flex:0 0 200px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Comparison" data-enabled="false" style="flex:0 0 200px;"></div>
                                                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Value" data-enabled="false" style="flex:0 0 200px;"></div>
                                                    </div>`);
                                                    FwControl.renderRuntimeHtml($readOnlyField.find('.fwformfield'));
                                                    FwFormField.setValue2($readOnlyField.find('[data-datafield="Datafield"]'), datafield, datafield);
                                                    FwFormField.setValue2($readOnlyField.find('[data-datafield="Comparison"]'), comparisonText, comparisonText);
                                                    FwFormField.setValue2($readOnlyField.find('[data-datafield="Value"]'), valuefield, valuefield);
                                                    $searchFields.append($readOnlyField);
                                                }
                                                if (i === 0) {
                                                    advancedSearch.searchconjunctions.push(' ');
                                                } else {
                                                    advancedSearch.searchconjunctions.push(FwFormField.getValue2(jQuery(queryRows[i]).find('.andor')));
                                                }
                                            }

                                            $browse.data('advancedsearchrequest', advancedSearch);

                                            request.searchfieldoperators = request.searchfieldoperators.concat(advancedSearch.searchfieldoperators);
                                            request.searchfields = request.searchfields.concat(advancedSearch.searchfields);
                                            request.searchfieldtypes = request.searchfieldtypes.concat(advancedSearch.searchfieldtypes);
                                            request.searchfieldvalues = request.searchfieldvalues.concat(advancedSearch.searchfieldvalues);
                                            request.searchseparators = request.searchseparators.concat(advancedSearch.searchseparators);
                                            request.searchconjunctions = request.searchconjunctions.concat(advancedSearch.searchconjunctions);

                                            FwServices.module.method(request, request.module, 'Browse', $browse, function (response) {
                                                try {
                                                    FwBrowse.beforeDataBindCallBack($browse, request, response);
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            })

                                            $find.removeClass('active');
                                            $find.find('.findbutton-dropdown').css('z-index', '0');
                                            jQuery(document).off('click');

                                            e.stopPropagation();
                                        })
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

            const $activeView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('Active'), true);
            $activeView.on('click', function () {
                try {
                    const $fwbrowse = jQuery(this).closest('.fwbrowse');
                    $fwbrowse.attr('data-activeinactiveview', 'active');
                    FwBrowse.search($fwbrowse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            const $inactiveView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('Inactive'), false);
            $inactiveView.on('click', function () {
                try {
                    const $fwbrowse = jQuery(this).closest('.fwbrowse');
                    $fwbrowse.attr('data-activeinactiveview', 'inactive');
                    FwBrowse.search($fwbrowse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            const $allView = FwMenu.generateDropDownViewBtn(FwLanguages.translate('All'), false);
            $allView.on('click', function () {
                try {
                    const $fwbrowse = jQuery(this).closest('.fwbrowse');
                    $fwbrowse.attr('data-activeinactiveview', 'all');
                    FwBrowse.search($fwbrowse);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            const viewitems: Array<string> = [];
            viewitems.push($activeView, $inactiveView, $allView);
            const $show = FwMenu.addViewBtn($menu, FwLanguages.translate('Show'), viewitems);
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
                $form.data('customformdata', customForms[0]);
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
        $form.data('fields', $form.find('.fwformfield:not([data-isuniqueid="true"])'));

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

        $formTabControl = $form.find('.fwtabs');
        if (typeof $form.data('customformdata') !== 'undefined') {
            //add custom form info tab
            const customform = $form.data('customformdata');
            const customformTabIds = FwTabs.addTab($formTabControl, 'Custom Form', false, 'CUSTOMFORM', false);
            const $customformTab = $formTabControl.find(`#${customformTabIds.tabid}`);
            FwTabs.setTabColor($customformTab, 'yellow');
            let $customFormFields = jQuery(`
                    <div class="flexpage">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Custom Form" style="max-width:900px;">
                              <div class="flexrow">
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Base Form" data-datafield="CustomFormBaseForm" data-enabled="false" data-formreadonly="true" style="flex:1 1 200px;"></div>
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Description" data-datafield="CustomFormId" data-displayfield="Description" data-validationname="CustomFormValidation" data-validationpeek="true" data-enabled="false" data-formreadonly="true" style="flex:1 1 300px;"></div>
                              </div>
                          </div>
                    </div>`);
            FwControl.renderRuntimeControls($customFormFields.find('.fwcontrol'));
            $formTabControl.find(`#${customformTabIds.tabpageid}`).append($customFormFields);
            FwFormField.setValueByDataField($form, 'CustomFormBaseForm', customform.BaseForm);
            FwFormField.setValueByDataField($form, 'CustomFormId', customform.CustomFormId, customform.Description);
        }

        //add Audit tab to all forms
        let $keys = $form.find('.fwformfield[data-type="key"]');
        if ($keys.length !== 0) {
            auditTabIds = FwTabs.addTab($formTabControl, 'Audit', false, 'AUDIT', false);
            $auditControl = jQuery(FwBrowse.loadGridFromTemplate('AuditHistoryGrid'));
            $auditControl.data('ondatabind', function (request) {
                const apiurl = (<any>window[controller]).apiurl;
                const sliceIndex = apiurl.lastIndexOf('/');
                const moduleName = apiurl.slice(sliceIndex + 1);
                request.uniqueids = {};
                request.uniqueids.ModuleName = moduleName;
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
            .on('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])', function (event) {
                var fields, $tab, $tabpage;
                event.stopPropagation();

                $tabpage = $form.parent();
                $tab = jQuery('#' + $tabpage.attr('data-tabid'));
                fields = FwModule.getFormFields($form, false);
                if (Object.keys(fields).length > 0) {
                    $tab.find('.modified').html('*');
                    $form.attr('data-modified', 'true');
                    $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                    $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');
                } else {
                    $tab.find('.modified').html('');
                    $form.attr('data-modified', 'false');
                    $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
                    $form.find('.btn[data-type="RefreshMenuBarButton"]').removeClass('disabled');
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
                if (value != '' && !$this.hasClass('date-validation')) {
                    $this.removeClass('error');
                    if ($this.closest('.tabpage.active').has('.error').length === 0) {
                        $this.parents('.fwcontrol .fwtabs').find('#' + errorTab).removeClass('error');
                    }
                }
                setTimeout(() => { FwModule.validateForm($form); }, 500); // some fields with .error with values assigned as the result of another validation were not being triggered
            })
            .on('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)', function (e) {
                e.stopPropagation();
                const $this = jQuery(this);
                const fieldName = $this.attr('data-datafield');
                const value = FwFormField.getValue2($this);
                const text = FwFormField.getText2($this);
                const $formfields = $form.find(`[data-datafield="${fieldName}"]`);
                if ($formfields.length > 1) {
                    for (let i = 0; i < $formfields.length; i++) {
                        FwFormField.setValue2(jQuery($formfields[i]), value, text);
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
                window[controller]['afterLoad']($form, response);
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
                window[controller]['afterLoad']($form, response);
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
                let $formfields, $browse;
                $form.data('SaveFormAPIresponse', response); // meant to be temporary solution for settings page save of new record. Data used to populate new record panel for form - J. Pace
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
                        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
                        $form.find('.btn[data-type="RefreshMenuBarButton"]').removeClass('disabled');
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
                            controller['afterLoad']($form, response);
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
                        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
                        $form.find('.btn[data-type="RefreshMenuBarButton"]').removeClass('disabled');

                        if ($form.attr('data-mode') === 'NEW') {
                            $form.attr('data-mode', 'EDIT');
                            $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                        } else {
                            $formfields = $form.data('fields');
                        }
                        FwFormField.loadForm($formfields, response.tables);
                        $form.attr('data-modified', 'false');
                        if (typeof controller['afterLoad'] === 'function') {
                            controller['afterLoad']($form, response);
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
        try {
            const $browse = $control;
            const $selectedRow = $browse.find('tr.selected');
            if ($selectedRow.length > 0) {
                const $confirmation = FwConfirmation.renderConfirmation('Delete Record', 'Are you sure you want to delete this record?');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes');
                const $no = FwConfirmation.addButton($confirmation, 'No');
                $yes.focus();
                $yes.on('click', function () {
                    const controller = $browse.attr('data-controller');
                    const ids = FwBrowse.getRowFormUniqueIds($browse, $selectedRow);
                    const request: any = {
                        module: (<any>window[controller]).Module,
                        ids: ids
                    };
                    FwServices.module.method(request, (<any>window[controller]).Module, 'Delete', $browse, function (response) {
                        const $form = FwModule.getFormByUniqueIds(ids);
                        if ((typeof $form != 'undefined') && ($form.length > 0)) {
                            const $tab = jQuery(`#${$form.closest('div.tabpage').attr('data-tabid')}`);
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
            const nodeBrowse = FwApplicationTree.getChildByType(nodeModule, 'Browse');
            let hasBrowseNew = false;
            let hasBrowseEdit = false;
            const hasBrowse = (nodeBrowse !== null);
            if (hasBrowse) {
                const nodeBrowseNewButton = FwApplicationTree.getChildrenByType(nodeBrowse, 'NewMenuBarButton');
                const nodeBrowseEditButton = FwApplicationTree.getChildrenByType(nodeBrowse, 'EditMenuBarButton');
                hasBrowseNew = (nodeBrowseNewButton !== null && nodeBrowseNewButton.length > 0) ? (nodeBrowseNewButton[0].properties.visible === 'T') : false;
                hasBrowseEdit = (nodeBrowseEditButton !== null && nodeBrowseEditButton.length > 0) ? (nodeBrowseEditButton[0].properties.visible === 'T') : false;
            }
            nodeForm = FwApplicationTree.getChildByType(nodeModule, 'Form');
            if (nodeForm !== null) {
                nodeFormMenuBar = FwApplicationTree.getChildByType(nodeForm, 'MenuBar');
                if (nodeFormMenuBar !== null) {
                    if (nodeFormMenuBar.properties.visible === 'T') {
                        $form.find('.fwform-menu').append($menu);
                        for (let menubaritemno = 0; menubaritemno < nodeFormMenuBar.children.length; menubaritemno++) {
                            var nodeMenuBarItem = nodeFormMenuBar.children[menubaritemno];
                            if (nodeMenuBarItem.properties.visible === 'T') {
                                nodetype = FwApplicationTree.getNodeType(nodeMenuBarItem);
                                switch (nodetype) {
                                    case 'SubMenu':
                                        nodeFormSubMenu = nodeMenuBarItem;
                                        if (nodeFormSubMenu !== null) {
                                            $submenubtn = FwMenu.addSubMenu($menu);
                                            for (let submenuoptionno = 0; submenuoptionno < nodeFormSubMenu.children.length; submenuoptionno++) {
                                                nodeSubMenuGroup = nodeFormSubMenu.children[submenuoptionno];
                                                if (nodeSubMenuGroup.properties.visible === 'T') {
                                                    $submenucolumn = FwMenu.addSubMenuColumn($submenubtn)
                                                    $submenugroup = FwMenu.addSubMenuGroup($submenucolumn, nodeSubMenuGroup.properties.caption, nodeSubMenuGroup.id);
                                                    for (let submenuitemno = 0; submenuitemno < nodeSubMenuGroup.children.length; submenuitemno++) {
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
                                        if (($form.attr('data-mode') === 'NEW' && (hasBrowseNew || !hasBrowse)) || ($form.attr('data-mode') === 'EDIT' && (hasBrowseEdit || !hasBrowse))) {
                                            $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                            $menubarbutton.attr('data-type', 'SaveMenuBarButton');
                                            $menubarbutton.addClass('disabled');
                                            $menubarbutton.on('click', function (event) {
                                                try {
                                                    const method = 'saveForm';
                                                    const ismodified = $form.attr('data-modified');
                                                    if (ismodified === 'true') {
                                                        if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                                                        if (typeof window[controller][method] === 'function') {
                                                            (<any>window)[controller][method]($form, { closetab: false });
                                                        } else {
                                                            FwModule[method]((<any>window)[controller].Module, $form, { closetab: false });
                                                        }
                                                    }
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
                                        }
                                        break;
                                    case 'PrevMenuBarButton':
                                        if (nodeMenuBarItem.properties.visible === 'T') {
                                            const $prev = FwMenu.addStandardBtn($menu, '<', nodeMenuBarItem.id);
                                            $prev.attr('data-type', 'PrevButton');
                                            $prev.on('click', function () {
                                                try {
                                                    const $this = jQuery(this);
                                                    const $browse = $this.closest('.tabpages').find('[data-tabtype="BROWSE"] .fwbrowse');
                                                    const $tab = FwTabs.getTabByElement($this);
                                                    FwBrowse.openPrevRow($browse, $tab, $form);
                                                } catch (ex) {
                                                    FwFunc.showError(ex);
                                                }
                                            });
                                        }
                                        break;
                                    case 'NextMenuBarButton':
                                        if (nodeMenuBarItem.properties.visible === 'T') {
                                            const $next = FwMenu.addStandardBtn($menu, '>', nodeMenuBarItem.id);
                                            $next.attr('data-type', 'NextButton');
                                            $next.on('click', function () {
                                                try {
                                                    const $this = jQuery(this);
                                                    const $browse = $this.closest('.tabpages').find('[data-tabtype="BROWSE"] .fwbrowse');
                                                    const $tab = FwTabs.getTabByElement($this);
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
                        // Refresh form button
                        if (typeof (<any>window[controller])['loadForm'] === 'function') {
                            const $refreshmenubarbutton = FwMenu.addStandardBtn($menu, 'Refresh');
                            $refreshmenubarbutton.attr('data-type', 'RefreshMenuBarButton');
                            if ($form.attr('data-mode') === 'NEW') {
                                $refreshmenubarbutton.addClass('disabled');
                            } else {
                                $refreshmenubarbutton.removeClass('disabled');
                            }
                            $refreshmenubarbutton.on('click', e => {
                                try {
                                    const $this = jQuery(e.currentTarget);
                                    const ismodified = $form.attr('data-modified');
                                    if (ismodified !== 'true' && $form.attr('data-mode') !== 'NEW' && !$this.hasClass('disabled')) {
                                        FwModule.refreshForm($form)
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                    }
                }
            }
        }
        if ((typeof $form.attr('data-controller') != 'undefined') && (typeof window[$form.attr('data-controller')]['addFormMenuItems'] === 'function')) {
            const $menuobj = window[$form.attr('data-controller')]['addFormMenuItems']($menu, $form);
            if (typeof $menuobj !== 'undefined') {
                $menu = $menuobj;
            }
        }

        FwControl.renderRuntimeControls($menu);
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
                $save.focus();
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
                validationDisplayField, validationDisplayValue, datatype;

            $fwformfield = jQuery(element);
            datatype = $fwformfield.attr('data-type');
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
                    switch (datatype) {
                        case "text":
                            datatype = "Text";
                            break;
                        case "date":
                            datatype = "Date";
                            break;
                        case "checkbox":
                            datatype = "True/False";
                            break;
                        case "number":
                            datatype = "Integer";
                            break;
                        case "decimal":
                            datatype = "Float";
                            break;
                    }
                    field = {
                        FieldName: dataField,
                        FieldType: datatype,
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
        } else {
            $form.find('[data-type="tab"].error').removeClass('error');
        }

        return isvalid;
    }
    //----------------------------------------------------------------------------------------------
    static getData($object: JQuery, request: any, responseFunc: Function, $elementToBlock: JQuery, timeout?: number) {
        var webserviceurl, controller, module, timeoutParam;
        controller = $object.attr('data-controller');
        module = (<any>window)[controller].Module;
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
        if (typeof (<any>window)[controller].Module !== 'undefined') {
            //($fieldtocheck.find('input.fwformfield-value').val() != '') &&
            //($fieldtocheck.find('input.fwformfield-value').val().toUpperCase() != $fieldtocheck.attr('data-originalvalue').toUpperCase())) {     //2015-12-21 MY: removed logic to support non required duplicategroup fields.
            request.module = (<any>window)[controller].Module;
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
                } else if (typeof typeof (<any>window)[controller] !== 'undefined' && typeof typeof (<any>window)[controller].apiurl === 'undefined' && request.table != table) {
                    runcheck = false;
                    FwNotification.renderNotification('ERROR', 'Fields are not part of the same table.');
                }
            });
            runcheck = runcheck && typeof controller.apiurl === 'undefined';
            if (runcheck) {
                FwServices.module.method(request, (<any>window)[controller].Module, 'ValidateDuplicate', $form,
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
        $form.attr('data-mode', 'READONLY');
        const $fwformfields = $form.data('fields');
        FwFormField.disable($fwformfields);
        const $grids = $form.data('grids');
        $grids.each(function (index, element) {
            const $grid = jQuery(element).find('div[data-control="FwBrowse"][data-type="Grid"]');
            FwBrowse.disableGrid($grid);
        });

        const $save = $form.find('div.btn[data-type="SaveMenuBarButton"]');
        $save.addClass('disabled');
        $save.off('click');
    }
    //----------------------------------------------------------------------------------------------
    static loadFormFromTemplate(modulename: string) {
        var $control = jQuery(jQuery('#tmpl-modules-' + modulename + 'Form').html());
        return $control;
    }
    //----------------------------------------------------------------------------------------------
    static refreshForm($form: JQuery) {
        const uniqueIds = FwModule.getFormUniqueIds($form);
        const newUniqueIds: any = {};
        setTimeout(() => {
            for (let key in uniqueIds) {
                newUniqueIds[key] = uniqueIds[key].value
            }
            const controller = $form.data('controller');
            if (controller) {
                const $newForm = (<any>window[controller]).loadForm(newUniqueIds);
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
    static addFindMenuButton($browse, browseMenuBar) {
        if ($browse.data('hasfind') !== false) {
            browseMenuBar.children.push({
                properties: {
                    caption: 'Find',
                    nodetype: 'FindMenuBarButton',
                    visible: 'T'
                }
            })
        }
        return browseMenuBar;
    }
    //----------------------------------------------------------------------------------------------
}

interface IFwFormField {
    initControl?($control: JQuery<HTMLElement>): void;
    disable($control: JQuery<HTMLElement>): void;
    enable($control: JQuery<HTMLElement>): void;
    getText2?($fwformfield: JQuery<HTMLElement>): string;
    getValue2($fwformfield: JQuery<HTMLElement>): any;
    loadForm($fwformfield: JQuery<HTMLElement>, table: string, field: string, value: any, text: string): void;
    loadItems($control: JQuery<HTMLElement>, items: any, hideEmptyItem: boolean): void;
    renderDesignerHtml($control: JQuery<HTMLElement>, html: string[]): void;
    renderRuntimeHtml($control: JQuery<HTMLElement>, html: string[]): void;
    setValue($fwformfield: JQuery<HTMLElement>, value: any, text: string, firechangeevent: boolean): void;
}