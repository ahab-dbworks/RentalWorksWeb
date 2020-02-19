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
        let isCustomTemplate = false;
        let $customBrowse;
        if (sessionStorage.getItem('customForms') !== null) {
            const controller = $browse.attr('data-controller');
            const baseForm = controller.replace('Controller', 'Browse');
            const customForms = JSON.parse(sessionStorage.getItem('customForms')).filter(a => a.BaseForm == baseForm);
            if (customForms.length > 0) {
                $customBrowse = jQuery(jQuery(`#tmpl-custom-${baseForm}`)[0].innerHTML);
                if ($customBrowse.length > 0) {
                    isCustomTemplate = true;
                    const attributes = $browse.each(function () { // replacing attributes/classes previously assigned to $browse when using a $customBrowse template
                        jQuery.each(this.attributes, function () {
                            // this.attributes is not a plain object, but an array of attribute nodes, which contain both the name and value
                            if (this.specified) {
                                $customBrowse.attr(this.name, this.value)
                            }
                        });
                    });
                } else {
                    throw new Error(`${baseForm} custom template not found.`)
                }
            }
        }
        if (isCustomTemplate) {
            $browse = $customBrowse
        }
        FwControl.renderRuntimeControls($browse.find('.fwcontrol').addBack());
        const options: IAddBrowseMenuOptions = this.getDefaultBrowseMenuOptions($browse);
        FwModule.addBrowseMenu(options);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    static getDefaultBrowseMenuOptions($browse: JQuery): IAddBrowseMenuOptions {
        const $menu = FwMenu.getMenuControl('default');
        $browse.find('.fwbrowse-menu').append($menu);
        const $subMenu = FwMenu.addSubMenu($menu);
        const $colOptions = FwMenu.addSubMenuColumn($subMenu);
        const $groupOptions = FwMenu.addSubMenuGroup($colOptions, 'Options');
        const $colExport = FwMenu.addSubMenuColumn($subMenu);
        const $groupExport = FwMenu.addSubMenuGroup($colExport, 'Export');
        const options: IAddBrowseMenuOptions = {
            $browse,
            $menu: $menu,
            $subMenu: $subMenu,
            $colOptions: $colOptions,
            $groupOptions: $groupOptions,
            $colExport: $colExport,
            $groupExport: $groupExport,
            hasNew: true,
            hasEdit: true,
            hasDelete: true,
            hasFind: true,
            hasDownloadExcel: true,
            hasInactive: true
        };
        return options;
    }
    //----------------------------------------------------------------------------------------------
    static addBrowseMenu(options: IAddBrowseMenuOptions) {
        const controller = options.$browse.attr('data-controller');

        if ((typeof controller === 'string') && (controller.length > 0) && (typeof window[controller].addBrowseMenuItems === 'function')) {
            window[controller].addBrowseMenuItems(options);
        }
        else {
            FwMenu.addBrowseMenuButtons(options);
        }

        // add the Active/Inactive dropdown to the browse menu
        if (typeof options.$browse !== null) {
            if (typeof options.$browse.attr('data-hasinactive') === 'string') {
                options.hasInactive = (options.$browse.attr('data-hasinactive') === 'true');
                //console.warn(`HasInactive for browse control: ${options.$browse.attr('data-controller')} should be updated to define hasinactive in the TypeScript code.`);
            } else {
                // mv 2019-12-12 Not sure if this is needed, just adding this in case some legacy code needs this
                options.$browse.attr('data-hasinactive', options.hasInactive.toString());
            }
        }
        if (options.hasInactive) {
            if (typeof options.$browse.attr('data-activeinactiveview') === 'undefined') {
                options.$browse.attr('data-activeinactiveview', 'active');
            }
            if (!(options.$browse.attr('data-activeinactiveview') === 'active' || options.$browse.attr('data-activeinactiveview') === 'inactive' || options.$browse.attr('data-activeinactiveview') === 'all')) {
                throw 'On the Browse template for ' + controller + ' the attribute data-activeinactiveview must be active, inactive, or all';
            }
            FwMenu.addVerticleSeparator(options.$menu);

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
            let viewitems: JQuery[] = [];
            viewitems.push($activeView, $inactiveView, $allView);
            const $show = FwMenu.addViewBtn(options.$menu, FwLanguages.translate('Show'), viewitems);
        }

        FwMenu.applyBrowseSecurity(options, (<any>window)[controller].id);
        FwMenu.cleanupMenu(options.$menu);


        FwControl.renderRuntimeControls(options.$menu.find('.fwcontrol').addBack());
    }
    //----------------------------------------------------------------------------------------------
    static openForm($form: JQuery, mode: string) {
        var $fwcontrols, formid, $formTabControl, auditTabIds, controller,
            nodeModule = null, nodeTabs, nodeTab, $tabs, nodeField, $fields, nodeControl, $grids, $tabcontrol, args;

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

        //nodeModule = FwApplicationTree.getNodeByController($form.attr('data-controller'));
        if (window[controller] !== null) {
            nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, (<any>window)[controller].id);
        }
        args = {};

        if (typeof mode === 'string') {
            $form.attr('data-mode', mode);
        } else {
            $form.attr('data-mode', 'NEW');
        }
        formid = program.uniqueId(8);

        $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        FwControl.setIds($fwcontrols, formid);

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
                                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Description" data-datafield="CustomFormId" data-displayfield="CustomFormDescription" data-validationname="CustomFormValidation" data-validationpeek="true" data-enabled="false" data-formreadonly="true" style="flex:1 1 300px;"></div>
                              </div>
                          </div>
                    </div>`);
            FwControl.renderRuntimeControls($customFormFields.find('.fwcontrol'));
            $formTabControl.find(`#${customformTabIds.tabpageid}`).append($customFormFields);
            FwFormField.setValueByDataField($form, 'CustomFormBaseForm', customform.BaseForm);
            FwFormField.setValueByDataField($form, 'CustomFormId', customform.CustomFormId, customform.Description);
        }

        //add Audit tab to all forms
        const nodeAuditGrid = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'xepjGBf0rdL');
        if (nodeAuditGrid !== null && nodeAuditGrid.properties.visible === 'T') {
            let $keys = $form.find('.fwformfield[data-type="key"]');
            if ($keys.length !== 0) {
                auditTabIds = FwTabs.addTab($formTabControl, 'Audit', false, 'AUDIT', false);
                const moduleControllerName = $form.data('controller');
                if (moduleControllerName !== undefined) {
                    const moduleController = (<any>window)[moduleControllerName];
                    if (moduleController !== undefined) {
                        const moduleSecurityId = moduleController.id;
                        if (moduleSecurityId !== undefined) {
                            const $auditControl = FwBrowse.renderGrid({
                                moduleSecurityId: moduleSecurityId,
                                $form: $form,
                                nameGrid: 'AuditHistoryGrid',
                                gridSecurityId: 'xepjGBf0rdL',
                                pageSize: 10,
                                onDataBind: request => {
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
                                }
                            });
                            $formTabControl.find('#' + auditTabIds.tabpageid).append($auditControl);
                            $formTabControl.find('#' + auditTabIds.tabid)
                                .addClass('audittab')
                                .on('click', e => {
                                    if ($form.attr('data-mode') !== 'NEW') {
                                        FwBrowse.search($auditControl);
                                    };
                                });
                        }
                    }
                }
            }
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
                if (value != '' && !$this.hasClass('dev-err')) {
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
        //$tabs.each(function (index, element) {
        //    var $tab, caption, args;
        //    $tab = jQuery(element);
        //    args = {};
        //    args.caption = $tab.attr('data-caption');
        //    nodeTab = FwApplicationTree.getNodeByFuncRecursive(nodeForm, args, function (node, args2) {
        //        var istab, captionsmatch;
        //        istab = (node.nodetype === 'Tab');
        //        captionsmatch = (node.caption === args2.caption);
        //        return (istab && captionsmatch);
        //    });
        //    if ((nodeTab !== null) && (nodeTab.properties.visible === 'F')) {
        //        FwTabs.removeTab($tab);
        //    }
        //});
        $tabs = $tabcontrol.find('div[data-type="tab"]');
        if ($tabs.length > 0) {
            FwTabs.setActiveTab($tabcontrol, $tabs.eq(0));
        }

        // hide fields based on security tree
        //$fields = $form.find('div[data-datafield]');
        //$fields.each(function (index, element) {
        //    var $field, args;
        //    $field = jQuery(element);
        //    args = {};
        //    args.caption = $field.attr('data-caption');
        //    args.datafield = $field.attr('data-datafield');
        //    nodeField = FwApplicationTree.getNodeByFuncRecursive(nodeForm, args, function (node, args2) {
        //        var isfield, captionsmatch, idsmatch;
        //        isfield = (node.nodetype === 'Field');
        //        //captionsmatch = (node.caption  === args2.caption);
        //        idsmatch = false;
        //        if ((typeof args2.caption === 'string') && (typeof args2.datafield === 'string')) {
        //            idsmatch = (node.id.indexOf('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, '')) !== -1);
        //            //console.log('---------');
        //            //console.log(node.id);
        //            //console.log('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, ''));
        //            //console.log(node.id.indexOf('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, '')));
        //        }
        //        return (isfield && idsmatch);
        //    });
        //    if (nodeField !== null) {
        //        if (nodeField.properties.visible === 'F') {
        //            $field.remove();
        //        }
        //        if (nodeField.properties.editable === 'F') {
        //            FwFormField.disable($field);
        //        }
        //    }
        //});

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

        FwModule.addFormMenu($form);

        // hide grids based on security tree
        const nodeControls = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args2: any) => {
            return node.nodetype === 'Controls';
        });
        const $securitycontrols = $form.find('[data-secid]');
        $securitycontrols.each(function (index, element) {
            const $securitycontrol = jQuery(element);
            const securityid = $securitycontrol.attr('data-secid');
            const nodeSecurityControl = FwApplicationTree.getNodeById(nodeControls, securityid);
            if (nodeSecurityControl === null || nodeSecurityControl.properties.visible === 'F') {
                $securitycontrol.empty().css({
                    'visibility': 'hidden'
                });
            }
            if (nodeSecurityControl !== null && $securitycontrol.attr('data-control') === 'FwGrid') {
                const $grid = $securitycontrol;
                const nodeGridActions = FwApplicationTree.getNodeByFuncRecursive(nodeSecurityControl, {}, (node: any, args2: any) => {
                    return (node.nodetype === 'ControlActions');
                });
                const nodeGridEdit = FwApplicationTree.getNodeByFuncRecursive(nodeGridActions, {}, (node: any, args2: any) => {
                    return node.nodetype === 'ControlAction' && node.properties.action === 'ControlEdit';
                });
                if (nodeGridEdit.properties.visible === 'F') {
                    $grid.children('[data-control="FwBrowse"]').attr('data-security-new', 'F');
                    $grid.children('[data-control="FwBrowse"]').attr('data-security-edit', 'F');
                }
                const nodeGridDelete = FwApplicationTree.getNodeByFuncRecursive(nodeGridActions, {}, (node: any, args2: any) => {
                    return node.nodetype === 'ControlAction' && node.properties.action === 'ControlDelete';
                });
                if (nodeGridDelete.properties.visible === 'F') {
                    $grid.children('[data-control="FwBrowse"]').attr('data-security-delete', 'F');
                }
            }
        });

        $grids = $form.data('grids');
        $grids.each(function (index, element) {
            var $grid, args, nodeGridButton;
            $grid = jQuery(element);
            args = {};
            args.grid = $grid.attr('data-grid');
            nodeControl = FwApplicationTree.getNodeByFuncRecursive(nodeControls, args, function (node, args2) {
                var isgrid, gridnamesmatch;
                isgrid = (node.nodetype === 'Control' && node.properties.controltype === 'Grid');
                gridnamesmatch = (node.properties.control === args2.grid);
                return (isgrid && gridnamesmatch);
            });
            if (nodeControl !== null) {
                if (nodeControl.properties.visible === 'F') {
                    //$grid.empty().css('visibility', 'hidden');
                    $grid.remove();
                }
                const nodeGridActions = FwApplicationTree.getNodeByFuncRecursive(nodeControl, {}, (node: any, args2: any) => {
                    return (node.nodetype === 'ControlActions');
                });
                const nodeGridEdit = FwApplicationTree.getNodeByFuncRecursive(nodeGridActions, {}, (node: any, args2: any) => {
                    return node.nodetype === 'ControlAction' && node.properties.action === 'ControlEdit';
                });
                if (nodeGridEdit.properties.visible === 'F') {
                    $grid.children('[data-control="FwBrowse"]').attr('data-security-new', 'F');
                    $grid.children('[data-control="FwBrowse"]').attr('data-security-edit', 'F');
                }
                const nodeGridDelete = FwApplicationTree.getNodeByFuncRecursive(nodeGridActions, {}, (node: any, args2: any) => {
                    return node.nodetype === 'ControlAction' && node.properties.action === 'ControlDelete';
                });
                if (nodeGridDelete.properties.visible === 'F') {
                    $grid.children('[data-control="FwBrowse"]').attr('data-security-delete', 'F');
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

        // work around for when devs add fields in the afterLoad
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield:not([data-isuniqueid="true"])'));
        $form.data('grids', $form.find('div[data-control="FwGrid"]'));
        if ($form.data('mode') !== undefined && $form.data('mode') === 'READONLY') {
            FwModule.setFormReadOnly($form);
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
                        $form.closest('.fwpopupbox').find('.popuptitle').html(tabname); // If popout
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
                        FwModule.closeFormTab($tab, $form, parameters.refreshRootTab);
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
                        FwModule.closeFormTab($tab, $form, parameters.refreshRootTab);
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
                            FwModule.closeFormTab($tab, $form, true);
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
    static addFormMenuBarButton($menu: JQuery, caption: string, onclick: (e: JQuery.ClickEvent) => void) {
        const $menubarbutton = FwMenu.addStandardBtn($menu, caption);
        $menubarbutton.on('click', onclick);
    }
    //----------------------------------------------------------------------------------------------
    static addFormMenu($form: JQuery) {
        const controller = $form.attr('data-controller');
        const $menu = FwMenu.getMenuControl('default');
        $form.find('.fwform-menu').append($menu);
        const $subMenu = FwMenu.addSubMenu($menu);
        const $colOptions = FwMenu.addSubMenuColumn($subMenu);
        const $groupOptions = FwMenu.addSubMenuGroup($colOptions, 'Options');

        const options: IAddFormMenuOptions = {
            $form: $form,
            $menu: $menu,
            $subMenu: $subMenu,
            $colOptions: $colOptions,
            $groupOptions: $groupOptions,
            hasSave: true,
            hasNext: false,
            hasPrevious: false
        };
        //if (typeof $form.data('addFormMenuItems') === 'function') {
        //    $form.data('addFormMenuItems')(options);
        //}
        if ((typeof $form.attr('data-controller') != 'undefined') && (typeof window[$form.attr('data-controller')]['addFormMenuItems'] === 'function')) {
            window[$form.attr('data-controller')]['addFormMenuItems'](options);
        }
        else {
            FwMenu.addFormMenuButtons(options);
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

        const moduleSecurityId = '';
        if (typeof (<any>window)[controller].id === 'string' && (<any>window)[controller].id.length > 0) {
            FwMenu.applyFormSecurity(options, (<any>window)[controller].id);
        }
        FwMenu.cleanupMenu($menu);
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
        const issubmodule = $tab.hasClass('submodule')
        if (issubmodule) {
            var $parenttab = jQuery(`#${$tab.data('parenttabid')}`);
        }

        const hassubmodule = (typeof $tab.data('subtabids') !== 'undefined') && ($tab.data('subtabids').length > 0);
        if (hassubmodule) {
            const subtabids = $tab.data('subtabids');
            const $submoduletab = jQuery(`#${subtabids[subtabids.length - 1]}`);
            const $submoduleform = jQuery(`#${$submoduletab.attr('data-tabpageid')}`).find('.fwform');
            FwModule.closeForm($submoduleform, $submoduletab, navigationpath, null, true);
        } else {
            const ismodified = $form.attr('data-modified');
            if (ismodified === 'true') {
                let tabname;
                if ($form.parent().data('type') === 'settings-row') {
                    tabname = $form.data('caption')
                } else {
                    tabname = $tab.find('.caption').html();
                }
                const $confirmation = FwConfirmation.renderConfirmation('Close Tab', `Want to save your changes to ${tabname}?`);
                const $save = FwConfirmation.addButton($confirmation, 'Save');
                const $dontsave = FwConfirmation.addButton($confirmation, 'Don\'t Save');
                const $cancel = FwConfirmation.addButton($confirmation, 'Cancel');
                $save.focus();
                $save.on('click', function () {
                    const controller = $form.attr('data-controller');
                    if (typeof window[controller] === 'undefined') throw `Missing javascript module controller: ${controller}`;
                    if (typeof window[controller]['saveForm'] === 'function') {
                        window[controller]['saveForm']($form, { closetab: true, navigationpath: navigationpath, closeparent: closeParent, afterCloseForm: afterCloseForm, refreshRootTab: true });
                    }
                });
                // hotkey support for confirmation buttons
                $confirmation.on('keyup', e => {
                    e.preventDefault();
                    if (e.which === 83) { // 's'
                        $save.click();
                    }
                    if (e.which === 78 || e.which === 68) { // 'n'(78) or 'd'(68)
                        $dontsave.click();
                    }
                    if (e.which === 67) { // 'c'
                        if ($cancel) {
                            $cancel.click();
                        }
                    }
                })

                $dontsave.on('click', e => {
                    FwModule.beforeCloseForm($form);
                    FwModule.closeFormTab($tab, $form, false);
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
                FwModule.closeFormTab($tab, $form, false);
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
    static closeFormTab($tab: JQuery, $form: JQuery, refreshRootTab?: boolean) {
        if ($form.parent().data('type') === 'settings-row') {
            $form.attr('data-modified', 'false')
        } else {
            const $tabcontrol = $tab.closest('.fwtabs');
            let $tabpage = $tabcontrol.find('#' + $tab.attr('data-tabpageid'));

            const isSubModule = $tab.hasClass('submodule');
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

            const $newTab = (($tab.next().length > 0) ? $tab.next() : $tab.prev());
            const tabIsActive = $tab.hasClass('active');
            FwTabs.removeTab($tab);

            // remove 'elipsis menu' if less than 2 forms open
            const $openForms = jQuery('#master-body').find('div[data-type="form"]');
            if ($openForms.length < 2) {
                jQuery('#moduletabs').find('.closetabbutton').html('');
            }

            if (tabIsActive) {
                FwTabs.setActiveTab($tabcontrol, $newTab);
                const newTabType = $newTab.attr('data-tabtype');
                if (newTabType === 'BROWSE') {
                    $tabpage = $tabcontrol.find(`#${$newTab.attr('data-tabpageid')}`);
                    const $browse = $tabpage.find('.fwbrowse[data-type="Browse"]');
                    if (refreshRootTab) {
                        FwBrowse.databind($browse);
                    }
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
                dataField = '';
            }
            //if (typeof dataField === 'undefined') {
            //    var formCaption = typeof $form.attr('data-caption') !== 'undefined' ? $form.attr('data-caption') : 'Unknown';
            //    console.log('On Form: "' + formCaption + ' ", the attribute data-datafield is required on the fwformfield with the following html: ' + jQuery('div').append($fwformfield).html());
            //    throw 'Attribute data-datafield is missing on fwformfield element.';
            //}
            value = FwFormField.getValue2($fwformfield);
            let value2 = value
            if (typeof value === 'number' || typeof value === 'boolean') {
                value2 = value.toString();
            } else if (typeof value === 'object') {
                value2 = JSON.stringify(value);
            }

            isBlank = (dataField === '');
            isCalculatedField = (!isBlank) && (dataField[0] === '#') && (dataField[1] === '.');
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
        let isValid = true;

        if ($form.parent().data('type') === 'settings-row') {
            $form.data('fields', $form.find('.fwformfield'))
        }

        const $fields = $form.find('.fwformfield:not([data-isuniqueid="true"])');

        $fields.each(function (index) {
            var $field = jQuery(this);

            if (($field.attr('data-required') == 'true') && ($field.attr('data-enabled') == 'true')) {
                if ($field.find('.fwformfield-value').val() == '') {
                    var errorTab = $field.closest('.tabpage').attr('data-tabid');
                    isValid = false;
                    $field.addClass('error');
                    $field.parents('.fwcontrol .fwtabs').find('#' + errorTab).addClass('error');
                } else if ($field.find('.fwformfield-value').val() != '' && !$field.hasClass('dev-err')) {
                    $field.removeClass('error');
                }
            }
            if (($field.attr('data-noduplicate') == 'true') && ($field.hasClass('error'))) {
                isValid = false;
            }
            if ($field.hasClass('error')) {
                isValid = false;
            }
            if (isValid) {
                $field.removeClass('error');
            }
        });

        if (!isValid) {
            FwNotification.renderNotification('ERROR', 'Please resolve the error(s) on the form.');
        } else {
            $form.find('[data-type="tab"].error').removeClass('error');
        }

        return isValid;
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
            if (typeof window[controller]['loadForm'] === 'function') {
                const $newForm = (<any>window[controller]).loadForm(newUniqueIds);
                $form.html($newForm);
                const $fwcontrols = $newForm.find('.fwcontrol');
                FwControl.loadControls($fwcontrols);
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

interface IModule {
    Module: string
    apiurl: string
    caption: string
    nav: string
    id: string
    getModuleScreen: () => IModuleScreen
    addBrowseMenuItems?: (options: IAddBrowseMenuOptions) => void
    [key: string]: any
}

interface IModuleScreen {
    $view?: JQuery
    viewModel?: any
    properties?: any
    load?: () => void
    unload?: () => void
    [key: string]: any
}

