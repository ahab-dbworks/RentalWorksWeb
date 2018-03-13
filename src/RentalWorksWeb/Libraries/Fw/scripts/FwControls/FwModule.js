var FwModule = (function () {
    function FwModule() {
    }
    FwModule.getModuleControl = function (moduleControllerName) {
        var html, $view, $actionView;
        jQuery('html').removeClass('mobile').addClass("desktop");
        html = [];
        html.push('<div id="moduleMaster">');
        html.push('<div id="moduleMaster-body">');
        html.push('<div id="moduletabs" class="fwcontrol fwtabs" data-control="FwTabs" data-type="" data-version="1" data-rendermode="template">');
        html.push('<div class="tabs"></div>');
        html.push('<div class="tabpages"></div>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');
        $view = jQuery(html.join(''));
        $view.attr('data-module', moduleControllerName);
        FwControl.renderRuntimeControls($view.find('.fwcontrol'));
        return $view;
    };
    ;
    FwModule.openModuleTab = function ($object, caption, tabHasClose, tabType, setTabActive) {
        var $tabControl, newtabids, $fwcontrols, controller;
        $tabControl = jQuery('#moduletabs');
        if ($tabControl.find('.tab').length === 0) {
            jQuery('title').html(caption);
        }
        newtabids = FwTabs.addTab($tabControl, caption, tabHasClose, tabType, setTabActive);
        $tabControl.find('#' + newtabids.tabpageid).append($object);
        $fwcontrols = $object.find('.fwcontrol');
        FwControl.loadControls($fwcontrols);
        if ($object.hasClass('fwbrowse')) {
            var $searchbox = jQuery('.search input:visible');
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
    };
    ;
    FwModule.openSubModuleTab = function ($browse, $form) {
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
        }
        else {
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
    };
    ;
    FwModule.openFormTab = function ($form, $object, tabname, tabhasclose, tabtype, setactive) {
        var $formtabcontrol, tabids, $fwcontrols;
        $formtabcontrol = $form.find('div.fwtabs:first');
        tabids = FwTabs.addTab($formtabcontrol, tabname, tabhasclose, tabtype, setactive);
        $form.find('#' + tabids.tabpageid).append($object);
        $form.find('#' + tabids.tabid + ' .delete').on('click', function () {
            FwTabs.setActiveTab($form.find('#' + tabids.tabid).parent().parent().parent(), $form.find('#' + tabids.tabid).siblings(':first'));
        });
        $fwcontrols = $object.find('.fwcontrol');
        FwControl.loadControls($fwcontrols);
    };
    ;
    FwModule.openBrowse = function ($browse) {
        FwControl.renderRuntimeControls($browse.find('.fwcontrol').addBack());
        FwModule.addBrowseMenu($browse);
        return $browse;
    };
    ;
    FwModule.addBrowseMenu = function ($browse) {
        var controller, $menu, $new, $edit, $delete, $inactiveView, $activeView, $allView, $show, $vr, $submenubtn, $submenucolumn, $optiongroup, $excelxlsx, $excelxls, nodeModule, nodeBrowse, nodeBrowseMenuBar, nodeBrowseSubMenu, $submenubtn, $menubarbutton, nodeSubMenuGroup, $submenucolumn, $submenugroup, nodeSubMenuItem, $submenuitem, hasClickEvent, $editbtn;
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
                                                $submenucolumn = FwMenu.addSubMenuColumn($submenubtn);
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
                                                                    var webserviceurl, module, request;
                                                                    try {
                                                                        module = window[controller].Module;
                                                                        request = FwBrowse.getRequest($browse);
                                                                        request.saveas = FwTabs.getTabByElement($browse).attr('data-caption');
                                                                        request.module = module;
                                                                        webserviceurl = 'services.ashx?path=/module/' + module + '/ExportBrowseXLSX';
                                                                        FwAppData.jsonPost(true, webserviceurl, request, FwServices.defaultTimeout, function (response) {
                                                                            var win, $iframe;
                                                                            try {
                                                                                $iframe = jQuery('<iframe style="display:none;" />');
                                                                                jQuery('.application').append($iframe);
                                                                                $iframe.attr('src', response.downloadurl);
                                                                                setTimeout(function () {
                                                                                    $iframe.remove();
                                                                                }, 500);
                                                                            }
                                                                            catch (ex) {
                                                                                FwFunc.showError(ex);
                                                                            }
                                                                        }, null, null);
                                                                        FwNotification.renderNotification('INFO', 'Downloading Excel Workbook...');
                                                                    }
                                                                    catch (ex) {
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
                                        $menubarbutton = FwMenu.addStandardBtn($menu, nodeMenuBarItem.properties.caption);
                                        $menubarbutton.attr('data-type', 'NewMenuBarButton');
                                        $menubarbutton.on('click', function () {
                                            var $form, controller, $browse, issubmodule;
                                            try {
                                                $browse = jQuery(this).closest('.fwbrowse');
                                                controller = $browse.attr('data-controller');
                                                issubmodule = $browse.closest('.tabpage').hasClass('submodule');
                                                if (typeof window[controller] === 'undefined')
                                                    throw 'Missing javascript module: ' + controller;
                                                if (typeof window[controller].openForm !== 'function')
                                                    throw 'Missing javascript function: ' + controller + '.openForm';
                                                $form = window[controller].openForm('NEW');
                                                if (!issubmodule) {
                                                    FwModule.openModuleTab($form, 'New ' + $form.attr('data-caption'), true, 'FORM', true);
                                                }
                                                else {
                                                    FwModule.openSubModuleTab($browse, $form);
                                                }
                                            }
                                            catch (ex) {
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
                                                }
                                                else {
                                                    FwNotification.renderNotification('WARNING', 'Please select a row.');
                                                }
                                            }
                                            catch (ex) {
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
                                                    }
                                                    else {
                                                        FwNotification.renderNotification('WARNING', 'Please select a row.');
                                                    }
                                                }
                                                catch (ex) {
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
                                            if (typeof window[controller] === 'undefined')
                                                throw 'Missing javascript module: ' + controller;
                                            if (typeof window[controller]['deleteRecord'] === 'function') {
                                                window[controller]['deleteRecord']($browse);
                                            }
                                            else {
                                                FwModule['deleteRecord'](window[controller].Module, $browse);
                                            }
                                        });
                                        break;
                                }
                            }
                        }
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
                }
                catch (ex) {
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
                }
                catch (ex) {
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
                }
                catch (ex) {
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
    };
    ;
    FwModule.openForm = function ($form, mode) {
        var $fwcontrols, formid, $formTabControl, auditTabIds, $auditControl, controller, nodeModule, nodeForm, nodeTabs, nodeTab, $tabs, nodeField, $fields, nodeGrid, $grids, $tabcontrol, args;
        nodeModule = FwApplicationTree.getNodeByController($form.attr('data-controller'));
        args = {};
        nodeForm = FwApplicationTree.getChildByType(nodeModule, 'Form');
        if (typeof mode === 'string') {
            $form.attr('data-mode', mode);
        }
        else {
            $form.attr('data-mode', 'NEW');
        }
        formid = program.uniqueId(8);
        $fwcontrols = $form.find('.fwcontrol').addBack();
        FwControl.renderRuntimeControls($fwcontrols);
        FwControl.setIds($fwcontrols, formid);
        FwModule.addFormMenu($form);
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]'));
        $form.data('fields', $form.find('.fwformfield[data-isuniqueid!="true"]'));
        controller = $form.attr('data-controller');
        $form.attr('data-modified', 'false');
        if (typeof window[controller]['renderGrids'] === 'function') {
            window[controller]['renderGrids']($form);
        }
        $form.data('grids', $form.find('div[data-control="FwGrid"]'));
        if (typeof window[controller]['setFormProperties'] === 'function') {
            window[controller]['setFormProperties']($form);
        }
        if ($form.attr('data-hasaudit') === 'true') {
            $formTabControl = jQuery($form.find('.fwtabs'));
            auditTabIds = FwTabs.addTab($formTabControl, 'Audit', false, 'AUDIT', false);
            $auditControl = jQuery('<div data-control="FwAudit" class="fwcontrol fwaudit" data-version="1" data-rendermode="template"></div>');
            FwControl.renderRuntimeControls($auditControl.find('.fwcontrol').addBack());
            $formTabControl.find('#' + auditTabIds.tabpageid).append($auditControl);
            $formTabControl.find('#' + auditTabIds.tabid)
                .on('click', function (e) {
                if (typeof window[controller]['loadAudit'] === 'function') {
                    window[controller]['loadAudit']($form);
                }
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
            }
            else {
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
            var $this, value;
            $this = jQuery(this);
            value = FwFormField.getValue2($this);
            if (value != '') {
                $this.removeClass('error');
            }
        });
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
                idsmatch = false;
                if ((typeof args2.caption === 'string') && (typeof args2.datafield === 'string')) {
                    idsmatch = (node.id.indexOf('field-' + args2.caption.replace(/[^A-Za-z0-9]/g, '') + '-' + args2.datafield.replace(/[^A-Za-z0-9]/g, '')) !== -1);
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
    };
    ;
    FwModule.loadForm = function (module, $form) {
        var request;
        request = {
            module: module,
            ids: FwModule.getFormUniqueIds($form)
        };
        FwServices.module.method(request, module, 'Load', $form, function (response) {
            var controller, $fwchargefields, $formfields, $tabpage, $tab;
            try {
                $tabpage = $form.parent();
                $tab = jQuery('#' + $tabpage.attr('data-tabid'));
                controller = window[module + 'Controller'];
                if (typeof controller === 'undefined') {
                    throw module + 'Controller is not defined.';
                }
                if (typeof controller.apiurl === 'undefined' && typeof response.tables === 'undefined') {
                    throw 'Server did not return the data needed to load this form.';
                }
                if (typeof controller.apiurl !== 'undefined') {
                    controller = $form.attr('data-controller');
                    var tabname = (typeof response.tabname === 'string') ? response.tabname : (typeof response.RecordTitle === 'string') ? response.RecordTitle : 'Unknown';
                    $tab.find('.caption').html(tabname);
                    $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                    FwFormField.loadForm($formfields, response);
                    $fwchargefields = $form.find('div.fwcharge');
                    if (($fwchargefields.length > 0) && ($fwchargefields.eq(0).attr('data-template') != $form.find('div[data-datafield="' + $fwchargefields.eq(0).attr('data-boundfield') + '"] input').val())) {
                        FwCharge.rerenderRuntimeHtml($form, response);
                    }
                    if (typeof window[controller]['setFormProperties'] === 'function') {
                        window[controller]['setFormProperties']($form);
                    }
                    if (typeof window[controller]['afterLoad'] === 'function') {
                        window[controller]['afterLoad']($form);
                    }
                }
                else {
                    controller = $form.attr('data-controller');
                    $tab.find('.caption').html(response.tabname);
                    $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                    FwFormField.loadForm($formfields, response.tables);
                    $fwchargefields = $form.find('div.fwcharge');
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
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    ;
    FwModule.saveForm = function (module, $form, parameters) {
        var $tabpage, $tab, isValid, request, controllername, controller;
        $tabpage = $form.parent();
        $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        isValid = FwModule.validateForm($form);
        controllername = $form.attr('data-controller');
        controller = window[controllername];
        if (isValid === true) {
            if (typeof controller.apiurl !== 'undefined') {
                request = FwModule.getFormModel($form, false);
            }
            else {
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
                        $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]');
                        if ($browse.length > 0) {
                            FwBrowse.databind($browse);
                        }
                        var tabname = (typeof response.tabname === 'string') ? response.tabname : (typeof response.RecordTitle === 'string') ? response.RecordTitle : 'Unknown';
                        $tab.find('.caption').html(tabname);
                        $tab.find('.modified').html('');
                        if ($form.attr('data-mode') === 'NEW') {
                            $form.attr('data-mode', 'EDIT');
                            $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                        }
                        else {
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
                    }
                    else if (parameters.closetab) {
                        var issubmodule, $parenttab;
                        issubmodule = $tab.hasClass('submodule');
                        if (issubmodule === true) {
                            $parenttab = jQuery('#' + $tab.data('parenttabid'));
                        }
                        FwModule.beforeCloseForm($form);
                        FwModule.closeFormTab($tab);
                        if (typeof parameters.afterCloseForm === 'function') {
                            parameters.afterCloseForm();
                        }
                        if ((issubmodule === false) || (typeof parameters.closeparent === 'undefined')) {
                            if ((typeof parameters.navigationpath === 'string') && (parameters.navigationpath !== '')) {
                                program.getModule(parameters.navigationpath);
                            }
                        }
                        else if ((issubmodule === true) && (parameters.closeparent === true)) {
                            $parenttab.find('.delete').click();
                        }
                    }
                    FwNotification.renderNotification('SUCCESS', 'Record saved.');
                }
                else if (response.saved === true) {
                    if (parameters.closetab === false) {
                        $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]');
                        if ($browse.length > 0) {
                            FwBrowse.databind($browse);
                        }
                        $tab.find('.caption').html(response.tabname);
                        $tab.find('.modified').html('');
                        if ($form.attr('data-mode') === 'NEW') {
                            $form.attr('data-mode', 'EDIT');
                            $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                        }
                        else {
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
                    }
                    else if (parameters.closetab === true) {
                        var issubmodule, $parenttab;
                        issubmodule = $tab.hasClass('submodule');
                        if (issubmodule === true) {
                            $parenttab = jQuery('#' + $tab.data('parenttabid'));
                        }
                        FwModule.beforeCloseForm($form);
                        FwModule.closeFormTab($tab);
                        if (typeof parameters.afterCloseForm === 'function') {
                            parameters.afterCloseForm();
                        }
                        if ((issubmodule === false) || (typeof parameters.closeparent === 'undefined')) {
                            if ((typeof parameters.navigationpath === 'string') && (parameters.navigationpath !== '')) {
                                program.getModule(parameters.navigationpath);
                            }
                        }
                        else if ((issubmodule === true) && (parameters.closeparent === true)) {
                            $parenttab.find('.delete').click();
                        }
                    }
                    FwNotification.renderNotification('SUCCESS', 'Record saved.');
                }
                else if (response.saved == false) {
                    if ((typeof response.message !== 'undefined') && (response.message != '')) {
                        FwNotification.renderNotification('ERROR', response.message);
                    }
                    else {
                        FwNotification.renderNotification('ERROR', 'There is an error on the form.');
                    }
                }
            });
        }
    };
    ;
    FwModule.deleteRecord = function (module, $control) {
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
                            FwModule.closeFormTab($tab);
                        }
                        FwBrowse.databind($browse);
                    });
                });
            }
            else {
                FwNotification.renderNotification('WARNING', 'Please select a row.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    ;
    FwModule.addFormMenu = function ($form) {
        var controller, $menu, $save, $edit, $delete, nodeModule, nodeForm, nodeFormMenuBar, nodeFormSubMenu, $submenubtn, $menubarbutton, nodetype, nodeSubMenuGroup, $submenucolumn, $submenugroup, nodeSubMenuItem, $submenuitem, hasClickEvent;
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
                                                    $submenucolumn = FwMenu.addSubMenuColumn($submenubtn);
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
                                                    if (typeof window[controller] === 'undefined')
                                                        throw 'Missing javascript module: ' + controller;
                                                    if (typeof window[controller][method] === 'function') {
                                                        window[controller][method]($form, false);
                                                    }
                                                    else {
                                                        FwModule[method](window[controller].Module, $form, false);
                                                    }
                                                }
                                            }
                                            catch (ex) {
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
                                                }
                                                catch (ex) {
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
                                                }
                                                catch (ex) {
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
    };
    ;
    FwModule.beforeCloseForm = function ($form) {
        var $fwformfields;
        $fwformfields = typeof $form.data('fields') !== 'undefined' ? $form.data('fields') : jQuery([]);
        $fwformfields.each(function (index, element) {
            FwFormField.onRemove(jQuery(element));
        });
    };
    ;
    FwModule.closeForm = function ($form, $tab, navigationpath, afterCloseForm, closeParent) {
        var $tabcontrol, ismodified, hassubmodule, issubmodule, $confirmation, $save, $dontsave, $cancel, tabname, $parenttab;
        $tabcontrol = $tab.closest('.fwtabs');
        ismodified = $form.attr('data-modified');
        hassubmodule = (typeof $tab.data('subtabids') !== 'undefined') && ($tab.data('subtabids').length > 0);
        issubmodule = $tab.hasClass('submodule');
        if ($form.parent().data('type') === 'settings-row') {
            ismodified = 'settingspage';
        }
        if (issubmodule) {
            $parenttab = jQuery('#' + $tab.data('parenttabid'));
        }
        if (hassubmodule) {
            var $submoduletab, $submoduleform, subtabids;
            subtabids = $tab.data('subtabids');
            $submoduletab = jQuery('#' + subtabids[subtabids.length - 1]);
            $submoduleform = jQuery('#' + $submoduletab.attr('data-tabpageid')).find('.fwform');
            FwModule.closeForm($submoduleform, $submoduletab, navigationpath, null, true);
        }
        else {
            if (ismodified === 'true') {
                tabname = $tab.find('.caption').html();
                $confirmation = FwConfirmation.renderConfirmation('Close Tab', 'Want to save your changes to "' + tabname + '"?');
                $save = FwConfirmation.addButton($confirmation, 'Save');
                $dontsave = FwConfirmation.addButton($confirmation, 'Don\'t Save');
                $cancel = FwConfirmation.addButton($confirmation, 'Cancel');
                $save.on('click', function () {
                    var controller, isvalid;
                    controller = $form.attr('data-controller');
                    if (typeof window[controller] === 'undefined')
                        throw 'Missing javascript module controller: ' + controller;
                    if (typeof window[controller]['saveForm'] === 'function') {
                        window[controller]['saveForm']($form, { closetab: true, navigationpath: navigationpath, closeparent: closeParent, afterCloseForm: afterCloseForm });
                    }
                });
                $dontsave.on('click', function () {
                    FwModule.beforeCloseForm($form);
                    FwModule.closeFormTab($tab);
                    if (typeof afterCloseForm === 'function') {
                        afterCloseForm();
                    }
                    if ((!issubmodule) || (typeof closeParent === 'undefined')) {
                        if ((typeof navigationpath === 'string') && (navigationpath !== '')) {
                            program.getModule(navigationpath);
                        }
                    }
                    else if ((issubmodule) && (closeParent)) {
                        $parenttab.find('.delete').click();
                    }
                });
            }
            else {
                FwModule.beforeCloseForm($form);
                FwModule.closeFormTab($tab);
                if (typeof afterCloseForm === 'function') {
                    afterCloseForm();
                }
                if ((issubmodule) && (closeParent)) {
                    $parenttab.find('.delete').click();
                }
            }
        }
    };
    ;
    FwModule.closeFormTab = function ($tab) {
        var $browse, $form, $newTab, newTabType, tabIsActive, $tabcontrol, $tabpage, isSubModule;
        $tabcontrol = $tab.closest('.fwtabs');
        $tabpage = $tabcontrol.find('#' + $tab.attr('data-tabpageid'));
        isSubModule = $tab.hasClass('submodule');
        if (isSubModule) {
            var $parenttab, subtabids;
            $parenttab = jQuery('#' + $tab.data('parenttabid'));
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
                $tabpage = $tabcontrol.find('#' + $newTab.attr('data-tabpageid'));
                $browse = $tabpage.find('.fwbrowse[data-type="Browse"]');
                FwBrowse.databind($browse);
            }
        }
    };
    ;
    FwModule.loadAudit = function ($form, uniqueid) {
        if (FwSecurity.isUser()) {
            FwAudit.loadAudit($form, uniqueid);
        }
    };
    ;
    FwModule.getFormUniqueIds = function ($form) {
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
    };
    ;
    FwModule.getFormFields = function ($form, getAllFieldsOverride) {
        var $fwformfields, fields, field;
        fields = {};
        $fwformfields = typeof $form.data('fields') !== 'undefined' ? $form.data('fields') : jQuery([]);
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
            getAllFields = ($form.attr('data-mode') === 'NEW') || getAllFieldsOverride;
            if ((isValidDataField) && ((getAllFields) || (originalValue !== value))) {
                field = {
                    datafield: dataField,
                    value: value
                };
                fields[dataField] = field;
            }
        });
        return fields;
    };
    ;
    FwModule.getWebApiFields = function ($form, includeUnmodifiedFields) {
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
    };
    ;
    FwModule.getFormModel = function ($form, getAllFieldsOverride) {
        var uniqueids = FwModule.getFormUniqueIds($form);
        var fields = FwModule.getFormFields($form, getAllFieldsOverride);
        var request = {};
        for (var key in uniqueids) {
            request[key] = uniqueids[key].value;
        }
        for (var key in fields) {
            request[key] = fields[key].value;
        }
        return request;
    };
    ;
    FwModule.validateForm = function ($form) {
        var isvalid, $fields;
        isvalid = true;
        $fields = $form.data('fields');
        $fields.each(function (index) {
            var $field = jQuery(this);
            if (($field.attr('data-required') == 'true') && ($field.attr('data-enabled') == 'true')) {
                if ($field.find('.fwformfield-value').val() == '') {
                    isvalid = false;
                    $field.addClass('error');
                }
                else if ($field.find('.fwformfield-value').val() != '') {
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
    };
    ;
    FwModule.getData = function ($object, request, responseFunc, $elementToBlock, timeout) {
        var webserviceurl, controller, module, timeoutParam;
        controller = $object.attr('data-controller');
        module = window[controller].Module;
        request.module = module;
        webserviceurl = 'services.ashx?path=/module/' + module + '/GetData';
        if (typeof timeout !== 'number') {
            timeoutParam = null;
        }
        FwAppData.jsonPost(true, webserviceurl, request, timeoutParam, responseFunc, null, $elementToBlock);
    };
    ;
    FwModule.getData2 = function (module, request, responseFunc, $elementToBlock, timeout) {
        var webserviceurl, timeoutParam;
        request.module = module;
        webserviceurl = 'services.ashx?path=/module/' + module + '/GetData';
        if (typeof timeout !== 'number') {
            timeoutParam = null;
        }
        FwAppData.jsonPost(true, webserviceurl, request, timeoutParam, responseFunc, null, $elementToBlock);
    };
    ;
    FwModule.getFormByUniqueIds = function (uniqueidcollection) {
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
    };
    ;
    FwModule.checkDuplicate = function ($form, $fieldtocheck) {
        var $fields, request = {}, groupname, $field, datafield, value, type, table, runcheck = true, controller, required;
        controller = $form.attr('data-controller');
        if (typeof window[controller].Module !== 'undefined') {
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
                }
                else if (typeof typeof window[controller] !== 'undefined' && typeof typeof window[controller].apiurl === 'undefined' && request.table != table) {
                    runcheck = false;
                    FwNotification.renderNotification('ERROR', 'Fields are not part of the same table.');
                }
            });
            runcheck = runcheck && typeof controller.apiurl === 'undefined';
            if (runcheck) {
                FwServices.module.method(request, window[controller].Module, 'ValidateDuplicate', $form, function (response) {
                    try {
                        if ((typeof controller.apiurl === 'undefined' && response.duplicate == true) || (typeof controller.apiurl !== 'undefined' && response)) {
                            $fields.addClass('error');
                            FwNotification.renderNotification('ERROR', 'Duplicate ' + $fields.attr('data-caption') + '(s) are not allowed.');
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, function (errorMessage) {
                    try {
                        FwFunc.showError('MiddleTier: ' + errorMessage);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        }
    };
    ;
    FwModule.setFormReadOnly = function ($form) {
        var $fwformfields, $grids, $save;
        $form.attr('data-mode', 'READONLY');
        $fwformfields = $form.data('fields');
        FwFormField.disable($fwformfields);
        $grids = $form.data('grids');
        $grids.each(function (index, element) {
            var $this;
            $this = jQuery(element);
            FwBrowse.disableGrid($this);
        });
        $save = $form.find('div.btn[data-type="SaveMenuBarButton"]');
        $save.addClass('disabled');
        $save.off('click');
    };
    ;
    FwModule.loadFormFromTemplate = function (modulename) {
        var $control = jQuery(jQuery('#tmpl-modules-' + modulename + 'Form').html());
        return $control;
    };
    ;
    return FwModule;
}());
//# sourceMappingURL=FwModule.js.map