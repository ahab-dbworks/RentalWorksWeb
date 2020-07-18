class FwMenuClass {
    //---------------------------------------------------------------------------------
    upgrade($control: JQuery): void {
        var properties, i, data_type;
        //sync properties
        data_type = $control.attr('data-type');
        properties = this.getDesignerProperties(data_type);
        for (i = 0; i < properties.length; i++) {
            if (typeof $control.attr(properties[i].attribute) === 'undefined') {
                $control.attr(properties[i].attribute, properties[i].defaultvalue);
            }
        }
    }
    //---------------------------------------------------------------------------------
    init($control: JQuery): void {
        //FwMenu.upgrade($control);
    };
    //---------------------------------------------------------------------------------
    getDesignerProperties(data_type: string): any[] {
        var properties = [], propId, propClass, propDataControl, propDataType, propRenderMode, propDataVersion, propDataCaption, propDataEnabled, propDataOriginalValue, propDataImageUrl, propDataField, propDataFieldCount, propFieldCount;
        propId = { caption: 'ID', datatype: 'string', attribute: 'id', defaultvalue: FwControl.generateControlId('tabs'), visible: true, enabled: true };
        propClass = { caption: 'CSS Class', datatype: 'string', attribute: 'class', defaultvalue: 'fwcontrol fwmenu', visible: false, enabled: false };
        propDataControl = { caption: 'Control', datatype: 'string', attribute: 'data-control', defaultvalue: 'FwMenu', visible: true, enabled: false };
        propDataType = { caption: 'Type', datatype: 'string', attribute: 'data-type', defaultvalue: data_type, visible: false, enabled: false };
        propDataVersion = { caption: 'Version', datatype: 'string', attribute: 'data-version', defaultvalue: '1', visible: false, enabled: false };
        propRenderMode = { caption: 'Render Mode', datatype: 'string', attribute: 'data-rendermode', defaultvalue: 'template', visible: false, enabled: false };

        properties = [propId, propClass, propDataControl, propDataType, propDataVersion, propRenderMode];

        return properties;
    };
    //---------------------------------------------------------------------------------
    renderDesignerHtml($control: JQuery): void {
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'designer');
    };
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control: JQuery): void {
        var html, $usercontrolChildren;

        $control.attr('data-rendermode', 'runtime');
    };
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control: JQuery): void {
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
    };
    //---------------------------------------------------------------------------------
    getMenuControl(controltype: string): JQuery {
        var html, $menuObject;

        html = [];
        html.push('<div class="fwcontrol fwmenu ' + controltype + '" data-control="FwMenu" data-visible="true">');
        html.push('<div class="buttonbar"></div>');
        html.push('</div>');
        $menuObject = jQuery(html.join(''));

        return $menuObject;
    };
    //---------------------------------------------------------------------------------
    addSubMenu($control: JQuery): JQuery {
        var $btn, html;

        html = [];
        html.push('<div class="submenubutton" data-visible="true">');
        html.push('<div class="icon"><i class="material-icons">&#xE5D2;</i></div>'); //menu
        html.push('<div class="submenu"></div>');
        html.push('</div>');

        $btn = jQuery(html.join(''));

        $btn
            .on('click', '.icon', function (e) {
                var $this, maxZIndex;
                $this = jQuery(this);
                e.preventDefault();
                if (!$this.parent().hasClass('active')) {
                    maxZIndex = FwFunc.getMaxZ('*');
                    $this.parent().find('.submenu').css('z-index', maxZIndex + 1);
                    $this.parent().addClass('active');

                    jQuery(document).one('click', function closeMenu(e) {
                        if ($this.parent().has(e.target).length === 0) {
                            $this.parent().removeClass('active');
                            $this.parent().find('.submenu').css('z-index', '0');
                        } else if ($this.parent().hasClass('active')) {
                            jQuery(document).one('click', closeMenu);
                        }
                    });
                } else {
                    $this.parent().removeClass('active');
                    $this.parent().find('.submenu').css('z-index', '0');
                }
            })
            .on('click', '.submenu-btn', function () {
                if ($btn.hasClass('active')) {
                    $btn.removeClass('active');
                }
            })
            ;

        $control.prepend($btn);

        return $btn;
    };
    //---------------------------------------------------------------------------------
    addSubMenuColumn($control: JQuery): JQuery {
        var html, $column;

        html = [];
        html.push('<div class="submenu-column" data-visible="true"></div>');

        $column = jQuery(html.join(''));

        $control.find('.submenu').append($column);

        return $column;
    };
    //---------------------------------------------------------------------------------
    addSubMenuGroup($control: JQuery, groupcaption: string, securityid?: string): JQuery {
        var html, $group;

        securityid = (typeof securityid === 'string') ? securityid : '';
        html = [];
        html.push('<div class="submenu-group" data-securityid="' + securityid + '" data-visible="true">');
        html.push('<div class="caption">' + groupcaption + '</div>');
        html.push('<div class="body"></div>');
        html.push('</div>');

        $group = jQuery(html.join(''));

        $control.append($group);

        return $group;
    };
    //---------------------------------------------------------------------------------
    addSubMenuBtn($group: JQuery, caption: string, securityid?: string): JQuery {
        var html, $btn;

        securityid = (typeof securityid === 'string') ? securityid : '';
        html = [];
        html.push('<div class="submenu-btn" data-securityid="' + securityid + '" data-visible="true">');
        html.push('<div class="caption">' + caption + '</div>');
        html.push('</div>');

        $btn = jQuery(html.join(''));

        $group.find('.body').append($btn);

        return $btn;
    };
    //---------------------------------------------------------------------------------
    addSubMenuItem($group: JQuery, caption: string, securityid: string, onclick: (event: JQuery.ClickEvent) => void): JQuery {
        const $btn = this.addSubMenuBtn($group, caption, securityid);
        $btn.on('click', onclick);
        return $btn;
    };
    //---------------------------------------------------------------------------------
    addStandardBtn($control: JQuery, caption: string, securityid?: string): JQuery {
        let $btn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                const id = program.uniqueId(8);
                const btnId = `btn${id}`;
                securityid = (typeof securityid === 'string') ? securityid : '';
                const btnHtml: Array<string> = [];
                btnHtml.push(`<div id="${btnId}" class="btn" tabindex="0" data-securityid="${securityid}" data-visible="true">`);
                if ($control.hasClass('default')) {
                    switch (caption) {
                        case 'New': btnHtml.push('<i class="material-icons">&#xE145;</i>'); break; //add
                        case 'Edit': btnHtml.push('<i class="material-icons">&#xE254;</i>'); break; //mode_edit
                        case 'Delete': btnHtml.push('<i class="material-icons">&#xE872;</i>'); break; //delete
                        case 'Save': btnHtml.push('<i class="material-icons">&#xE161;</i>'); break; //save
                        case 'Refresh': btnHtml.push('<i class="material-icons">refresh</i>'); break; //find
                        case 'Find': btnHtml.push('<i class="material-icons">search</i>'); break; //find
                    }
                }
                const addedClass = caption.toLowerCase().replace(/ /g, '');
                btnHtml.push(`  <div class="btn-text ${addedClass}-btn">${caption}</div>`);
                btnHtml.push('</div>');
                $btn = $btn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwMenu.addStandardBtn: ' + caption + ' caption is not defined in translation';
        }

        $control.find('.buttonbar').append($btn);

        return $btn;
    }
    //---------------------------------------------------------------------------------
    addViewBtn($control: JQuery, caption: string, subitems: JQuery[], allowMultiple?: boolean, filterField?: string): JQuery {
        var $btn, btnHtml, btnId, id;
        id = program.uniqueId(8);
        btnId = 'btn' + id;
        btnHtml = [];
        btnHtml.push(`<div id="${btnId}" class="ddviewbtn" data-visible="true">`);
        btnHtml.push(`  <div class="ddviewbtn-caption">${caption}:</div>`);
        btnHtml.push(`  <div class="ddviewbtn-select ${allowMultiple ? ' multifilter' : ''}" tabindex="0">`);
        btnHtml.push('    <div class="ddviewbtn-select-value"></div>');
        btnHtml.push('    <i class="material-icons">&#xE5C5;</i>'); //arrow_drop_down
        btnHtml.push('    <div class="ddviewbtn-dropdown"></div>')
        btnHtml.push('  </div>');
        btnHtml.push('</div>');

        $btn = jQuery(btnHtml.join(''));

        const controller = $control.closest('.fwbrowse').attr('data-controller');
        let selectedFilterValues: Array<string> = [];
        for (var i = 0; i < subitems.length; i++) {
            let $ddBtn = subitems[i];
            if (allowMultiple) {
                $ddBtn.prepend(`<input type="checkbox">`);

                if (typeof filterField !== 'undefined') {
                    if (typeof (<any>window)[controller].ActiveViewFields[filterField] == 'undefined') {
                        (<any>window)[controller].ActiveViewFields[filterField] = ["ALL"];
                    }
                }
            }

            $ddBtn.on('click', e => {
                const $this = jQuery(e.currentTarget);
                const $browse = $this.closest('.fwbrowse');
                let caption = $this.find('.ddviewbtn-dropdown-btn-caption').html();
                if (allowMultiple) {
                    e.stopPropagation();
                    const value = $this.attr('data-value');
                    let fields = (<any>window)[controller].ActiveViewFields[filterField];
                    if (value === "ALL") $this.addClass('select-all-filters');
                    let isSelectAllFilters: boolean = $this.hasClass('select-all-filters');
                    let isChecked: boolean = $this.find('input[type="checkbox"]').prop('checked');

                    //toggle checkboxes
                    if (jQuery(e.target).attr('type') != 'checkbox') {
                        $this.find('input[type="checkbox"]').prop('checked', !isChecked);
                        isChecked = !isChecked; //updates bool val after toggling checkbox
                    }

                    //check all checkboxes if "ALL" is checked & set caption & update ActiveViewFields
                    let indexOfAll = fields.indexOf("ALL");
                    if (isSelectAllFilters) {
                        const $filterOptions = jQuery($this).siblings().find('input[type="checkbox"]');
                        if (isChecked) {
                            if ($filterOptions.length === 1) { //if there is only one other option
                                $filterOptions.prop('checked', false);
                            } else {
                                $filterOptions.prop('checked', true);
                            }
                            selectedFilterValues = ["All"];
                            fields = ["ALL"];
                            caption = "All";
                        } else {
                            selectedFilterValues = [];
                            fields = [];
                            jQuery($this).siblings().find('input[type="checkbox"]').prop('checked', false);
                            caption = '';
                        }
                    } else {
                        jQuery($this).siblings('.select-all-filters').find('input[type="checkbox"]').prop('checked', false);
                        if (indexOfAll != -1) {
                            selectedFilterValues = [];
                            fields = [];
                            const checkedFilters = $this.siblings().find('input[type="checkbox"]:checked');
                            for (let i = 0; i < checkedFilters.length; i++) {
                                let filterCaption = jQuery(checkedFilters[i]).siblings('.ddviewbtn-dropdown-btn-caption').html();
                                let filterValue = jQuery(checkedFilters[i]).parents('.ddviewbtn-dropdown-btn').attr('data-value');
                                selectedFilterValues.push(filterCaption);
                                fields.push(filterValue);
                            }
                        }
                        if (isChecked) {
                            if (selectedFilterValues.indexOf(caption) == -1) {
                                selectedFilterValues.push(caption);
                                fields.push(value);
                            }
                        } else {
                            selectedFilterValues = selectedFilterValues.filter(val => val !== caption);
                            fields = fields.filter(val => val !== value);
                        }

                        if (selectedFilterValues.length <= 3) {
                            caption = selectedFilterValues.join(', ');
                        } else {
                            let firstThreeFilters = selectedFilterValues.slice(0, 3);
                            caption = `${firstThreeFilters.join(", ")} + ${selectedFilterValues.length - 3} more`;
                        }
                    }

                    (<any>window)[controller].ActiveViewFields[filterField] = fields;

                    const request: any = {};
                    request.WebUserId = JSON.parse(sessionStorage.getItem('userid')).webusersid;
                    request.ModuleName = (<any>window)[controller].Module;
                    request.ActiveViewFields = JSON.stringify((<any>window)[controller].ActiveViewFields);
                    request.OfficeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;

                    if (typeof (<any>window)[controller].ActiveViewFieldsId == 'undefined') {
                        FwAppData.apiMethod(true, 'POST', `api/v1/browseactiveviewfields/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            (<any>window)[controller].ActiveViewFieldsId = response.Id;
                        }, ex => FwFunc.showError(ex), $browse);
                    } else {
                        request.Id = (<any>window)[controller].ActiveViewFieldsId;
                        FwAppData.apiMethod(true, 'PUT', `api/v1/browseactiveviewfields/${request.Id}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        }, ex => FwFunc.showError(ex), $browse);
                    }
                }

                $btn.find('.ddviewbtn-select-value').empty().html(caption);
                $this.siblings('.ddviewbtn-dropdown-btn.active').removeClass('active');
                $this.addClass('active');
                FwBrowse.search($browse);
            });

            $btn.find('.ddviewbtn-dropdown').append($ddBtn);
        }

        //set caption and check checkboxes upon loading
        if (typeof filterField !== 'undefined' && typeof (<any>window)[controller].ActiveViewFields[filterField] !== 'undefined') {
            for (let i = 0; i < (<any>window)[controller].ActiveViewFields[filterField].length; i++) {
                const $this = (<any>window)[controller].ActiveViewFields[filterField][i];
                let $ddbtn = $btn.find(`[data-value="${$this}"]`);
                //To account for changes in location
                if (filterField == 'LocationId' && $ddbtn.length == 0) {
                    const loc = JSON.parse(sessionStorage.getItem('location')).locationid;
                    (<any>window)[controller].ActiveViewFieldsId = undefined;
                    $ddbtn = $btn.find(`[data-value="${loc}"]`);
                    (<any>window)[controller].ActiveViewFields[filterField][i] = loc;
                }
                //To account for changes in warehouse
                if (filterField == 'WarehouseId' && $ddbtn.length == 0) {
                    const wh = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
                    (<any>window)[controller].ActiveViewFieldsId = undefined;
                    $ddbtn = $btn.find(`[data-value="${wh}"]`);
                    (<any>window)[controller].ActiveViewFields[filterField] = (<any>window)[controller].ActiveViewFields[filterField].filter(e => { return e != $this });
                    (<any>window)[controller].ActiveViewFields[filterField][i] = wh;
                }

                const caption = $ddbtn.find(`.ddviewbtn-dropdown-btn-caption`).html();
                if ($this == 'ALL') {
                    $ddbtn.addClass('select-all-filters');
                    if ($btn.find('input').length === 2) {
                        $btn.find(`div[data-value="${$this}"] input`).prop('checked', true);
                    } else {
                        $btn.find('input').prop('checked', true);
                    }
                    selectedFilterValues = ["All"];
                } else {
                    $ddbtn.find('input').prop('checked', true);
                    selectedFilterValues.push(caption);
                }
            }

            let filterCaption: string;
            if (selectedFilterValues.length <= 3) {
                filterCaption = selectedFilterValues.join(', ');
            } else {
                let firstThreeFilters = selectedFilterValues.slice(0, 3);
                filterCaption = `${firstThreeFilters.join(", ")} + ${selectedFilterValues.length - 3} more`;
            }
            $btn.find('.ddviewbtn-select-value').empty().html(filterCaption);
        } else {
            $btn.find('.ddviewbtn-select-value').html($btn.find('.ddviewbtn-dropdown-btn.active .ddviewbtn-dropdown-btn-caption').html());
        }

        $btn.on('click', '.ddviewbtn-select', function (e) {
            var $this, maxZIndex;
            $this = jQuery(this);
            e.preventDefault();
            if (!$this.hasClass('active')) {
                maxZIndex = FwFunc.getMaxZ('*');
                $this.find('.ddviewbtn-dropdown').css('z-index', maxZIndex + 1);
                $this.addClass('active');
                jQuery(document).one('click', function closeMenu(e) {
                    if (($this.has(e.target).length === 0) && ($this.parent().has(e.target).length === 0)) {
                        $this.removeClass('active');
                        $this.find('.ddviewbtn-dropdown').css('z-index', '0');
                    } else if ($this.hasClass('active')) {
                        jQuery(document).one('click', closeMenu);
                    }
                });
            } else {
                $this.removeClass('active');
                $this.find('.ddviewbtn-dropdown').css('z-index', '0');
            }
        });

        $control.find('.buttonbar').append($btn);

        return $btn;
    };
    //---------------------------------------------------------------------------------
    generateDropDownViewBtn(caption: string, active: boolean, value?: string): JQuery {
        var btnHtml, $ddBtn;
        btnHtml = [];
        if (typeof value !== 'undefined') {
            btnHtml.push(`<div class="ddviewbtn-dropdown-btn ${active ? ' active' : ''}" data-value="${value}">`);
        } else {
            btnHtml.push(`<div class="ddviewbtn-dropdown-btn ${active ? ' active' : ''}">`);
        }
        btnHtml.push(`<div class="ddviewbtn-dropdown-btn-caption">${caption}</div>`);
        btnHtml.push(`</div>`);

        $ddBtn = jQuery(btnHtml.join(''));

        return $ddBtn
    };
    //---------------------------------------------------------------------------------
    addVerticleSeparator($control: JQuery): void {
        var html, $vr;
        html = [];
        html.push('<div class="vr"></div>');
        $vr = jQuery(html.join(''));
        $control.find('.buttonbar').append($vr);
    };
    //---------------------------------------------------------------------------------
    addCustomContent($control: JQuery, $content: JQuery): void {
        $control.find('.buttonbar').append($content);
    };
    //---------------------------------------------------------------------------------
    generateButtonMenuOption(caption: string): JQuery {
        let $btnMenuOption, html;
        html = [];
        html.push(`<div class="btnmenuoption" data-type="btnmenuoption">${caption}</div>`);
        $btnMenuOption = jQuery(html.join(''));
        return $btnMenuOption;
    };
    //----------------------------------------------------------------------------------------------
    addButtonMenuOptions($control: JQuery, menuOptions: JQuery[]): JQuery {
        const caption = $control.attr('data-caption');
        const html = [];
        html.push(`<div class="fwformcontrol btnmenu">${caption}</div>`);
        html.push(`<div class="icon-wrapper"><i class="material-icons btnmenudd">&#xE5C5;</i></div>`);
        html.push(`<div class="btnmenuoptions"></div>`);
        $control.append(html.join(''))
            .find('.btnmenuoptions')
            .append(menuOptions);

        $control.on('click', '.btnmenudd', e => {
            e.stopPropagation();
            $control.find('.btnmenuoptions').toggle();

            jQuery(document).one('click', function closeMenu(e) {
                $control.find('.btnmenuoptions').toggle();
            });
        });

        return $control;
    };
    //----------------------------------------------------------------------------------------------
    applyFormSecurity(options: IAddFormMenuOptions, moduleSecurityId: string): void {
        let $form = options.$menu.closest('.fwform');
        const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, moduleSecurityId);
        if (nodeModule === null) {
            console.error(`Unable to find Module node in security tree for security id: ${moduleSecurityId}`);
            options.$menu.attr('data-visible', 'false').hide();
            return;
        } else {
            options.$menu.attr('data-visible', 'true').show();
        }
        const nodeActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleActions');
        });
        if (nodeActions !== null) {
            const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'New');
            });
            const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'Edit');
            });
            const nodeSave = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'Save');
            });
            let hasSave: boolean = false;

            const mode = $form.attr('data-mode');
            hasSave = ((options.hasSave && nodeNew !== null && nodeNew.properties.visible === 'T' && mode === 'NEW') ||
                (options.hasSave && nodeEdit !== null && nodeEdit.properties.visible === 'T') && mode === 'EDIT' ||
                (nodeSave !== null && nodeSave.properties.visible === 'T') && (mode === 'NEW' || mode === 'EDIT'));

            // Save
            if (hasSave) {
                const $btnSave = options.$menu.find('.btn[data-type="SaveMenuBarButton"]');
                $btnSave.attr('data-visible', 'true').show();
            } else {
                const $btnSave = options.$menu.find('.btn[data-type="SaveMenuBarButton"]');
                $btnSave.attr('data-visible', 'false').hide();
                if (options.hasSave) {
                    FwModule.setFormReadOnly(options.$form);
                }
            }
        }
        const nodeOptions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleOptions' || node.nodetype === 'ControlOptions');
        });
        options.$menu.find('.submenubutton [data-securityid]').show(); // first enable all the menu items in case they are hidden
        if (nodeOptions !== null) {
            for (let i = 0; i < nodeOptions.children.length; i++) {
                let nodeOption = nodeOptions.children[i];
                const optionIsVisible = (nodeOption.properties.visible === 'T');
                options.$menu.find(`.submenubutton [data-securityid="${nodeOption.id}"]`).attr('data-visible', optionIsVisible.toString()).toggle(optionIsVisible);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    applyBrowseSecurity(options: IAddBrowseMenuOptions, moduleSecurityId: string): void {
        let $browse = options.$menu.closest('.fwbrowse');
        const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, moduleSecurityId);
        if (nodeModule === null) {
            console.error(`Unable to find Module node in security tree for security id: ${moduleSecurityId}`);
            options.$menu.attr('data-visible', 'false').hide();
            return;
        } else {
            options.$menu.attr('data-visible', 'true').show();
        }
        const nodeActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleActions');
        });
        if (nodeActions !== null) {
            let nodeView = null;
            nodeView = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'View');
            });
            const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'New');
            });
            const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'Edit');
            });
            const nodeSave = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'Save');
            });
            const nodeDelete = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ModuleAction' && node.properties.action === 'Delete');
            });
            let hasView: boolean = false;
            let hasNew: boolean = false;
            let hasEdit: boolean = false;
            let hasDelete: boolean = false;
            hasView = options.hasView && (nodeView !== null && nodeView.properties.visible === 'T' && (nodeEdit === null || nodeEdit.properties.visible === 'F'));
            hasNew = options.hasNew && ((nodeNew !== null && nodeNew.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'));
            hasEdit = options.hasEdit && ((nodeEdit !== null && nodeEdit.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'));
            hasDelete = options.hasDelete && (nodeDelete !== null && nodeDelete.properties.visible === 'T');

            // View
            if (hasView) {
                const $btnView = options.$menu.find('.btn[data-type="ViewMenuBarButton"]');
                $btnView.attr('data-visible', 'true').show();
            }
            else {
                const $btnView = options.$menu.find('.btn[data-type="ViewMenuBarButton"]');
                $btnView.attr('data-visible', 'false').hide();
            }

            // New
            if (hasNew) {
                const $btnNew = options.$menu.find('.btn[data-type="NewMenuBarButton"]');
                $btnNew.attr('data-visible', 'true').show();
                $browse.attr('data-newtab', 'true');
            } else {
                const $btnNew = options.$menu.find('.btn[data-type="NewMenuBarButton"]');
                $btnNew.attr('data-visible', 'false').hide();
                $browse.attr('data-newtab', 'false');
            }

            // Edit
            if (hasEdit) {
                const $btnEdit = options.$menu.find('.btn[data-type="EditMenuBarButton"]');
                $btnEdit.attr('data-visible', 'true').show();
            } else {
                const $btnEdit = options.$menu.find('.btn[data-type="EditMenuBarButton"]');
                $btnEdit.attr('data-visible', 'false').hide();
            }

            // Delete
            if (hasDelete) {
                const $btnDelete = options.$menu.find('.btn[data-type="DeleteMenuBarButton"]');
                $btnDelete.attr('data-visible', 'true').show();
            } else {
                const $btnDelete = options.$menu.find('.btn[data-type="DeleteMenuBarButton"]');
                $btnDelete.attr('data-visible', 'false').hide();
            }
        }
        const nodeOptions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleOptions' || node.nodetype === 'ControlOptions');
        });
        options.$menu.find('.submenubutton [data-securityid]').show(); // first enable all the menu items in case they are hidden
        if (nodeOptions !== null) {
            for (let i = 0; i < nodeOptions.children.length; i++) {
                let nodeOption = nodeOptions.children[i];
                const optionIsVisible = (nodeOption.properties.visible === 'T');
                options.$menu.find(`.submenubutton [data-securityid="${nodeOption.id}"]`).attr('data-visible', optionIsVisible.toString()).toggle(optionIsVisible);
            }
        }
    }

    //----------------------------------------------------------------------------------------------
    applyGridSecurity(options: IAddGridMenuOptions, moduleSecurityId: string): void {
        let $browse = options.$menu.closest('.fwbrowse');
        const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, moduleSecurityId);
        if (nodeModule === null) {
            console.error(`Unable to find Module node in security tree for security id: ${moduleSecurityId}`);
            options.$menu.attr('data-visible', 'false').hide();
            return;
        } else {
            options.$menu.attr('data-visible', 'true').show();
        }
        const nodeActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
            return (node.nodetype === 'ControlActions' || node.nodetype === 'ModuleActions');
        });
        if (nodeActions !== null) {
            const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlNew') || (node.nodetype === 'ModuleAction' && node.properties.action === 'New');
            });
            const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlEdit') || (node.nodetype === 'ModuleAction' && node.properties.action === 'Edit');
            });
            const nodeSave = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlSave') || (node.nodetype === 'ModuleAction' && node.properties.action === 'Save');
            });
            const nodeDelete = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlDelete') || (node.nodetype === 'ModuleAction' && node.properties.action === 'Delete');
            });
            let hasNew: boolean = false;
            let hasEdit: boolean = false;
            let hasDelete: boolean = false;
            hasNew = options.hasNew && ((nodeNew !== null && nodeNew.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'));
            hasEdit = options.hasEdit && ((nodeEdit !== null && nodeEdit.properties.visible === 'T') || (nodeSave !== null && nodeSave.properties.visible === 'T'));
            hasDelete = options.hasDelete && (nodeDelete !== null && nodeDelete.properties.visible === 'T');

            // New
            if (hasNew) {
                const $btnNew = options.$menu.find('.btn[data-type="NewMenuBarButton"]');
                $btnNew.attr('data-visible', 'true').show();
                $browse.attr('data-newtab', 'true');
            } else {
                const $btnNew = options.$menu.find('.btn[data-type="NewMenuBarButton"]');
                $btnNew.attr('data-visible', 'false').hide();
                $browse.attr('data-newtab', 'false');
            }

            // Edit
            if (hasEdit) {
                const $btnEdit = options.$menu.find('.btn[data-type="EditMenuBarButton"]');
                $btnEdit.attr('data-visible', 'true').show();
            } else {
                const $btnEdit = options.$menu.find('.btn[data-type="EditMenuBarButton"]');
                $btnEdit.attr('data-visible', 'false').hide();
            }

            // Delete
            if (hasDelete) {
                const $btnDelete = options.$menu.find('.btn[data-type="DeleteMenuBarButton"]');
                $btnDelete.attr('data-visible', 'true').show();
            } else {
                const $btnDelete = options.$menu.find('.btn[data-type="DeleteMenuBarButton"]');
                $btnDelete.attr('data-visible', 'false').hide();
            }
            options.$browse.attr('data-deleteoption', hasDelete.toString());
        }
        const nodeOptions = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
            return (node.nodetype === 'ModuleOptions' || node.nodetype === 'ControlOptions');
        });
        options.$menu.find('.submenubutton [data-securityid]').show(); // first enable all the menu items in case they are hidden
        if (nodeOptions !== null) {
            for (let i = 0; i < nodeOptions.children.length; i++) {
                let nodeOption = nodeOptions.children[i];
                const optionIsVisible = (nodeOption.properties.visible === 'T');
                options.$menu.find(`.submenubutton [data-securityid="${nodeOption.id}"]`).attr('data-visible', optionIsVisible.toString()).toggle(optionIsVisible);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    cleanupMenu($menu: JQuery) {
        const $submenus = $menu.find('.submenu');
        for (let submenuno = 0; submenuno < $submenus.length; submenuno++) {
            const $submenu = $submenus.eq(submenuno);
            const $submenugroups = $submenu.find('.submenu-group');
            for (let submenugroupno = 0; submenugroupno < $submenugroups.length; submenugroupno++) {
                const $submenugroup = $submenugroups.eq(submenugroupno);
                const hasChildren = $submenugroup.children('.body').eq(0).children('[data-visible="true"]').length > 0;
                $submenugroup.attr('data-visible', hasChildren.toString()).toggle(hasChildren);
            }
            const $submenucolumns = $submenu.find('.submenu-column');
            for (let submenucolno = 0; submenucolno < $submenucolumns.length; submenucolno++) {
                const $submenucol = $submenucolumns.eq(submenucolno);
                const hasChildren = $submenucol.children('[data-visible="true"]').eq(0).length > 0;
                $submenucol.attr('data-visible', hasChildren.toString()).toggle(hasChildren);
            }
            if ($submenu.children('[data-visible="true"]').length === 0) {
                const $submenubutton = $submenu.closest('.submenubutton');
                $submenubutton.addClass('nohover');
                $submenubutton.off('click', '.icon');
                $submenubutton.find('.icon i')
                    .css({
                        color: '#aaaaaa'
                    });
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuButtons(options: IAddBrowseMenuOptions) {
        if (typeof options.hasView === 'undefined') {
            options.hasView = true;
        }
        if (typeof options.hasNew === 'undefined') {
            options.hasNew = true;
        }
        if (typeof options.hasEdit === 'undefined') {
            options.hasEdit = true;
        }
        if (typeof options.hasDelete === 'undefined') {
            options.hasDelete = true;
        }
        if (typeof options.hasFind === 'undefined') {
            options.hasFind = true;
        }
        if (typeof options.hasDownloadExcel === 'undefined') {
            options.hasDownloadExcel = true;
        }
        if (typeof options.hasCustomize === 'undefined') {
            options.hasCustomize = true;
        }
        //if (typeof buttons.hasExportExcel) {
        //    //check the security tree
        //    const nodeExportExcel
        //    FwMenu.addSubMenuItem(options.$groupExport, 'Download Excel Workbook (*.xlsx)', this.exportExcelSecurityId, (e: JQuery.ClickEvent) => {
        //        try {
        //            const $form = options.$browse.closest('.fwform');
        //            $form
        //            FwBrowse.downloadExcelWorkbook(options.$browse, options.$browse.attr('data-controller');
        //        }
        //        catch (ex) {
        //            FwFunc.showError(ex);
        //        }
        //    });
        //}
        if (typeof options.hasNew === 'boolean' && options.hasNew) {
            options.$browse.attr('data-newtab', 'true');
            const $menubarbutton = FwMenu.addStandardBtn(options.$menu, 'New');
            $menubarbutton.attr('data-type', 'NewMenuBarButton');
            $menubarbutton.on('click', function () {
                try {
                    //options.$browse.attr('data-newtab', 'true');
                    const controller = options.$browse.attr('data-controller');
                    const issubmodule = options.$browse.closest('.tabpage').hasClass('submodule');
                    if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                    if (typeof (<any>window[controller]).openForm !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
                    const $form = (<any>window[controller]).openForm('NEW');
                    if (!issubmodule) {
                        FwModule.openModuleTab($form, 'New ' + $form.attr('data-caption'), true, 'FORM', true);
                    } else {
                        FwModule.openSubModuleTab(options.$browse, $form);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if (typeof options.hasEdit === 'boolean' && options.hasEdit) {
            const $menubarbutton = FwMenu.addStandardBtn(options.$menu, 'View');
            $menubarbutton.attr('data-type', 'ViewMenuBarButton');
            $menubarbutton.on('click', function (event) {
                try {
                    const $browse = jQuery(this).closest('.fwbrowse');
                    const $selectedRow = $browse.find('tr.selected');
                    if ($selectedRow.length > 0) {
                        $selectedRow.dblclick();
                    } else {
                        FwNotification.renderNotification('WARNING', 'Please select a row.');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

            const $menubarbutton2 = FwMenu.addStandardBtn(options.$menu, 'Edit');
            $menubarbutton2.attr('data-type', 'EditMenuBarButton');
            $menubarbutton2.on('click', function (event) {
                try {
                    const $selectedRow = options.$browse.find('tr.selected');
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
        if (typeof options.hasDelete === 'boolean' && options.hasDelete) {
            const $menubarbutton = FwMenu.addStandardBtn(options.$menu, 'Delete');
            $menubarbutton.attr('data-type', 'DeleteMenuBarButton');
            $menubarbutton.on('click', function () {
                try {
                    const controller = options.$browse.attr('data-controller');
                    if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                    if (typeof window[controller]['deleteRecord'] === 'function') {
                        window[controller]['deleteRecord'](options.$browse);
                    } else {
                        FwModule['deleteRecord']((<any>window[controller]).Module, options.$browse);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if (typeof options.hasFind === 'boolean' && options.hasFind) {
            let loaded = false;
            const $menubarbutton = FwMenu.addStandardBtn(options.$menu, 'Find');

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
                let maxZIndex;
                let $this = jQuery(this);
                const controller = options.$browse.attr('data-controller');
                e.preventDefault();
                if (!loaded) {
                    FwAppData.apiMethod(true, 'GET', (<any>window[controller]).apiurl + '/emptyobject', null, FwServices.defaultTimeout, function onSuccess(response) {
                        let dateField = options.$browse.find('.datequery');
                        let textField = options.$browse.find('.textquery');
                        let booleanField = options.$browse.find('.booleanquery');
                        FwControl.renderRuntimeHtml($menubarbutton.find('.fwcontrol'));

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
                        window['FwFormField_select'].loadItems(options.$browse.find('.datafieldselect'), findFields, false);
                        window['FwFormField_select'].loadItems(options.$browse.find('.andor'), [{ value: 'and', text: 'And' }, { value: 'or', text: 'Or' }], true);
                        window['FwFormField_select'].loadItems(booleanField, [{ value: 'T', text: 'true' }, { value: 'F', text: 'false' }], true);
                        options.$browse.find('.datafieldselect').on('change', function () {
                            let datatype = jQuery(this).find(':selected').data('type');
                            dateField.hide();
                            textField.hide();
                            booleanField.hide();
                            switch (datatype) {
                                case 'Text':
                                    textField.show();
                                    window['FwFormField_select'].loadItems(jQuery(options.$browse.find('.datafieldcomparison')[0]), textComparisonFields, true);
                                    break;
                                case 'Integer':
                                case 'Decimal':
                                case 'Float':
                                    textField.show();
                                    window['FwFormField_select'].loadItems(jQuery(options.$browse.find('.datafieldcomparison')[0]), numericComparisonFields, true);
                                    break;
                                case 'True/False':
                                case 'Boolean':
                                    booleanField.show();
                                    window['FwFormField_select'].loadItems(jQuery(options.$browse.find('.datafieldcomparison')[0]), booleanComparisonFields, true);
                                    break;
                                case 'Date':
                                    dateField.show();
                                    window['FwFormField_select'].loadItems(jQuery(options.$browse.find('.datafieldcomparison')[0]), dateComparisonFields, true);
                                    break;
                            }
                        })

                        if (chartFilters) {
                            for (let i = 0; i < chartFilters.length; i++) {
                                let valueField;
                                const data = chartFilters[i];
                                const field = data.datafield.toUpperCase();
                                const type = data.type;
                                const $queryRow = options.$browse.find('.query .queryrow:last');
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
                            options.$browse.find('.querysearch').click();
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

            const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
            if (chartFilters) $menubarbutton.click();


            options.$browse.find('.add-query').on('click', function cloneRow() {
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
                    if (options.$browse.find('.query').find('.queryrow').length === 2 && $newRow.next().length !== 0) {
                        $newRow.next().find('.delete-query').removeAttr('style').css('visibility', 'hidden');
                    }
                    if (options.$browse.find('.query').find('.queryrow').length === 2 && $newRow.prev().length !== 0) {
                        $newRow.prev().find('.delete-query').removeAttr('style').css('visibility', 'hidden');
                    }
                    $newRow.remove();
                }).css('visibility', 'visible');
                $newRow.find('.andor').css('visibility', 'visible');
                $newRow.find('.add-query').on('click', cloneRow);
                $newRow.find('input').val('')
                $newRow.appendTo(options.$browse.find('.query'));
                if (options.$browse.find('.query').find('.queryrow').length > 1) {
                    jQuery(options.$browse.find('.query').find('.queryrow')[0]).find('.delete-query').on('click', function () {
                        jQuery(this).closest('.queryrow').next().find('.andor').css('visibility', 'hidden');
                        jQuery(this).closest('.queryrow').remove();
                    }).css('visibility', 'visible');
                }
                jQuery(this).css('cursor', 'default');
                jQuery(this).css('visibility', 'hidden');
            })

            options.$browse.find('.querysearch').on('click', function (e) {
                options.$browse.removeData('advancedsearchrequest');
                let request = FwBrowse.getRequest(options.$browse);
                let advancedSearch: any = {};
                let queryRows = options.$browse.find('.query').find('.queryrow');
                let $find = jQuery(this).closest('.btn');
                advancedSearch.searchfieldoperators = [];
                advancedSearch.searchfieldtypes = [];
                advancedSearch.searchfields = [];
                advancedSearch.searchfieldvalues = [];
                advancedSearch.searchcondition = [];
                advancedSearch.searchseparators = [];
                advancedSearch.searchconjunctions = [];

                //adds container for read-only fields from "Find"
                const $browsemenu = options.$browse.find('.fwbrowse-menu');

                let $searchFields;
                if ($browsemenu.find('.search-wrapper').length === 0) {
                    $searchFields = jQuery(`<div class="fwcontrol fwmenu default flexrow search-wrapper">
                                                <div class="read-only-searchfields flexcolumn"></div>
                                            </div>`);
                    $browsemenu.append($searchFields);
                } else {
                    $browsemenu.find('.read-only-searchfields').empty();
                    $searchFields = $browsemenu.find('.search-wrapper');
                }

                const $clearFilters = jQuery(`<div class="flexcolumn" style="max-width:200px; margin-top:1em;">
                                                    <div class="fwformcontrol clear-filters" data-type="button">Clear All Filters</div>
                                                </div>`);
                $clearFilters.on('click', e => {
                    $browsemenu.find('.search-wrapper').remove();
                    options.$browse.removeData('advancedsearchrequest');
                    const $queryRows = $menubarbutton.find('.query');
                    $queryRows.find('.queryrow:not(:first-of-type)').remove();
                    $queryRows.find('.queryrow .delete-query').css('visibility', 'hidden');
                    $queryRows.find('.queryrow .add-query').css('visibility', 'visible');
                    $queryRows.find('.queryrow .find-field select, .queryrow .find-field input').val('');
                    FwBrowse.search(options.$browse);
                });

                if (!$browsemenu.find('.clear-filters').length) $browsemenu.find('.search-wrapper').append($clearFilters);

                for (var i = 0; i < queryRows.length; i++) {
                    let valuefield, altsearchfield;
                    const comparisonText = jQuery(queryRows[i]).find('.datafieldcomparison').find(':selected').text();
                    const comparisonfield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="DatafieldComparison"]'));
                    const datafield = FwFormField.getValue2(jQuery(queryRows[i]).find('div[data-datafield="Datafield"]'));

                    const $browseDataField = options.$browse.find(`thead [data-browsedatafield="${datafield}"]`);
                    if (typeof $browseDataField.attr('data-searchfield') !== 'undefined') {
                        altsearchfield = $browseDataField.attr('data-searchfield');
                    }

                    let type = jQuery(queryRows[i]).find('.datafieldselect').find(':selected').data('type');
                    if (datafield != '') {
                        advancedSearch.searchfieldtypes.push(jQuery(queryRows[i]).find('.datafieldselect').find(':selected').data('type'));
                        if (typeof altsearchfield !== 'undefined') {
                            advancedSearch.searchfields.push(altsearchfield);
                        } else {
                            advancedSearch.searchfields.push(datafield);
                        }
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
                        $searchFields.find('.read-only-searchfields').append($readOnlyField);
                    }
                    if (i === 0) {
                        advancedSearch.searchconjunctions.push(' ');
                    } else {
                        advancedSearch.searchconjunctions.push(FwFormField.getValue2(jQuery(queryRows[i]).find('.andor')));
                    }
                }

                options.$browse.data('advancedsearchrequest', advancedSearch);

                request.searchfieldoperators = request.searchfieldoperators.concat(advancedSearch.searchfieldoperators);
                request.searchfields = request.searchfields.concat(advancedSearch.searchfields);
                request.searchfieldtypes = request.searchfieldtypes.concat(advancedSearch.searchfieldtypes);
                request.searchfieldvalues = request.searchfieldvalues.concat(advancedSearch.searchfieldvalues);
                request.searchseparators = request.searchseparators.concat(advancedSearch.searchseparators);
                request.searchconjunctions = request.searchconjunctions.concat(advancedSearch.searchconjunctions);

                FwServices.module.method(request, request.module, 'Browse', options.$browse, function (response) {
                    try {
                        FwBrowse.beforeDataBindCallBack(options.$browse, request, response);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })

                $find.removeClass('active');
                $find.find('.findbutton-dropdown').css('z-index', '0');
                jQuery(document).off('click');

                e.stopPropagation();
            });
        }
        if (options.hasDownloadExcel) {
            const gridSecurityId = options.$browse.data('secid');
            const gridName = options.$browse.data('name');
            FwMenu.addSubMenuItem(options.$groupExport, 'Download Excel Workbook (*.xlsx)', gridSecurityId, (e: JQuery.ClickEvent) => {
                try {
                    FwBrowse.downloadExcelWorkbook(options.$browse, `${gridName}Controller`);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            FwMenu.addSubMenuItem(options.$groupExport, 'Import data from Excel', gridSecurityId, (e: JQuery.ClickEvent) => {
                try {
                    FwBrowse.importExcelFromBrowse(options.$browse, `${gridName}Controller`);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }

        if (options.hasCustomize) {
            const gridSecurityId = options.$browse.data('secid');
            const name = options.$browse.data('name');
            const type = options.$browse.data('type');
            FwMenu.addSubMenuItem(options.$groupOptions, 'Customize', gridSecurityId, (e: JQuery.ClickEvent) => {
                try {
                    FwBrowse.customizeColumns(options.$browse, name, type);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuButtons(options: IAddFormMenuOptions) {
        if (options.$form.attr('data-mode') === 'NEW' || options.$form.attr('data-mode') === 'EDIT') {
            const $menubarbutton = FwMenu.addStandardBtn(options.$menu, 'Save');
            $menubarbutton.attr('data-type', 'SaveMenuBarButton');
            $menubarbutton.addClass('disabled');
            $menubarbutton.on('click', function (event) {
                try {
                    const method = 'saveForm';
                    const ismodified = options.$form.attr('data-modified');
                    if (ismodified === 'true') {
                        const controller = options.$form.attr('data-controller');
                        if (typeof window[controller] === 'undefined') throw 'Missing javascript module: ' + controller;
                        if (typeof window[controller][method] === 'function') {
                            (<any>window)[controller][method](options.$form, { closetab: false });
                        } else {
                            FwModule[method]((<any>window)[controller].Module, options.$form, { closetab: false });
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if (typeof options.hasPrevious === 'boolean' && options.hasPrevious) {
            var $prev = FwMenu.addStandardBtn(options.$menu, '<');
            $prev.attr('data-type', 'PrevButton');
            $prev.on('click', function () {
                var $this, $browse, $tab, $selectedrow;
                try {
                    $this = jQuery(this);
                    $browse = $this.closest('.tabpages').find('[data-tabtype="BROWSE"] .fwbrowse');
                    $tab = FwTabs.getTabByElement($this);
                    FwBrowse.openPrevRow($browse, $tab, options.$form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
        if (typeof options.hasNext === 'boolean' && options.hasNext) {
            var $next = FwMenu.addStandardBtn(options.$menu, '>');
            $next.attr('data-type', 'NextButton');
            $next.on('click', function () {
                var $this, $browse, $tab, $selectedrow;
                try {
                    $this = jQuery(this);
                    $browse = $this.closest('.tabpages').find('[data-tabtype="BROWSE"] .fwbrowse');
                    $tab = FwTabs.getTabByElement($this);
                    FwBrowse.openNextRow($browse, $tab, options.$form);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    }
    //----------------------------------------------------------------------------------------------
}

var FwMenu = new FwMenuClass();

interface IAddFormMenuOptions {
    $form: JQuery
    $menu: JQuery
    $subMenu: JQuery
    $colOptions: JQuery
    $groupOptions: JQuery
    hasPrevious?: boolean
    hasNext?: boolean
    hasSave?: boolean
}

interface IAddBrowseMenuOptions {
    $browse: JQuery
    $menu: JQuery
    $subMenu: JQuery
    $colOptions: JQuery
    $groupOptions: JQuery
    $colExport: JQuery
    $groupExport: JQuery
    hasView?: boolean
    hasNew?: boolean
    hasEdit?: boolean
    hasDelete?: boolean
    hasFind?: boolean
    hasDownloadExcel?: boolean
    hasInactive?: boolean
    hasCustomize?: boolean;
}

interface IAddGridMenuOptions {
    $browse: JQuery;
    $menu: JQuery;
    $subMenu: JQuery;
    $colActions: JQuery;
    $groupActions: JQuery;
    //$colExport: JQuery;
    $groupExport: JQuery;
    gridSecurityId?: string;
    hasDelete?: boolean;
    hasEdit?: boolean;
    hasNew?: boolean;
    hasDownloadExcel?: boolean;
}