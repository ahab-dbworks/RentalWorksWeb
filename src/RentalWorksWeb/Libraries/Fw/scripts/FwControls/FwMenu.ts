class FwMenuClass {
    //---------------------------------------------------------------------------------
    upgrade($control) {
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
    init($control) {
        FwMenu.upgrade($control);

    };
    //---------------------------------------------------------------------------------
    getDesignerProperties(data_type) {
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
    renderDesignerHtml($control) {
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'designer');
    };
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html, $usercontrolChildren;

        $control.attr('data-rendermode', 'runtime');
    };
    //---------------------------------------------------------------------------------
    renderTemplateHtml($control) {
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
    };
    //---------------------------------------------------------------------------------
    getMenuControl(controltype) {
        var html, $menuObject;

        html = [];
        html.push('<div class="fwcontrol fwmenu ' + controltype + '" data-control="FwMenu">');
        html.push('<div class="buttonbar"></div>');
        html.push('</div>');
        $menuObject = jQuery(html.join(''));

        return $menuObject;
    };
    //---------------------------------------------------------------------------------
    addSubMenu($control) {
        var $btn, html;

        html = [];
        html.push('<div class="submenubutton">');
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
    addSubMenuColumn($control) {
        var html, $column;

        html = [];
        html.push('<div class="submenu-column"></div>');

        $column = jQuery(html.join(''));

        $control.find('.submenu').append($column);

        return $column;
    };
    //---------------------------------------------------------------------------------
    addSubMenuGroup($control, groupcaption, securityid) {
        var html, $group;

        securityid = (typeof securityid === 'string') ? securityid : '';
        html = [];
        html.push('<div class="submenu-group" data-securityid="' + securityid + '">');
        html.push('<div class="caption">' + groupcaption + '</div>');
        html.push('<div class="body"></div>');
        html.push('</div>');

        $group = jQuery(html.join(''));

        $control.append($group);

        return $group;
    };
    //---------------------------------------------------------------------------------
    addSubMenuBtn($group, caption, securityid) {
        var html, $btn;

        securityid = (typeof securityid === 'string') ? securityid : '';
        html = [];
        html.push('<div class="submenu-btn" data-securityid="' + securityid + '">');
        html.push('<div class="caption">' + caption + '</div>');
        html.push('</div>');

        $btn = jQuery(html.join(''));

        $group.find('.body').append($btn);

        return $btn;
    };
    //---------------------------------------------------------------------------------
    addStandardBtn($control, caption, securityid) {
        var $btn, btnHtml, btnId, id;
        $btn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                id = program.uniqueId(8);
                btnId = 'btn' + id;
                securityid = (typeof securityid === 'string') ? securityid : '';
                btnHtml = [];
                btnHtml.push('<div id="' + btnId + '" class="btn" tabindex="0" data-securityid="' + securityid + '">');
                if ($control.hasClass('default')) {
                    switch (caption) {
                        case 'New': btnHtml.push('<i class="material-icons">&#xE145;</i>'); break; //add
                        case 'Edit': btnHtml.push('<i class="material-icons">&#xE254;</i>'); break; //mode_edit
                        case 'Delete': btnHtml.push('<i class="material-icons">&#xE872;</i>'); break; //delete
                        case 'Save': btnHtml.push('<i class="material-icons">&#xE161;</i>'); break; //save
                        case 'Find': btnHtml.push('<i class="material-icons">search</i>'); break; //find
                    }
                }
                btnHtml.push('  <div class="btn-text">' + caption + '</div>');
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
    };
    //---------------------------------------------------------------------------------
    addViewBtn($control, caption, subitems, allowMultiple?: boolean, filterField?: string) {
        var $btn, btnHtml, btnId, id, $ddBtn;
        id = program.uniqueId(8);
        btnId = 'btn' + id;
        btnHtml = [];
        btnHtml.push(`<div id="${btnId}" class="ddviewbtn">`);
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
            $ddBtn = subitems[i];
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
                        if (isChecked) {
                            jQuery($this).siblings().find('input[type="checkbox"]').prop('checked', true);
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

                    let request = {
                        WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid
                        , ModuleName: (<any>window)[controller].Module
                        , ActiveViewFields: JSON.stringify((<any>window)[controller].ActiveViewFields)
                        , OfficeLocationId: JSON.parse(sessionStorage.getItem('location')).locationid
                    };

                    if (typeof (<any>window)[controller].ActiveViewFieldsId == 'undefined') {
                        FwAppData.apiMethod(true, 'POST', `api/v1/browseactiveviewfields/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            (<any>window)[controller].ActiveViewFieldsId = response.Id;
                        }, null, null);
                    } else {
                        request["Id"] = (<any>window)[controller].ActiveViewFieldsId;
                        FwAppData.apiMethod(true, 'POST', `api/v1/browseactiveviewfields/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        }, null, null);
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
                const caption = $ddbtn.find(`.ddviewbtn-dropdown-btn-caption`).html();
                if ($this == 'ALL') {
                    $ddbtn.addClass('select-all-filters');
                    $btn.find('input').prop('checked', true);
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
    generateDropDownViewBtn(caption, active, value?) {
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
    addVerticleSeparator($control) {
        var html, $vr;
        html = [];
        html.push('<div class="vr"></div>');
        $vr = jQuery(html.join(''));
        $control.find('.buttonbar').append($vr);
    };
    //---------------------------------------------------------------------------------
    addCustomContent($control, $content) {
        $control.find('.buttonbar').append($content);
    };
    //---------------------------------------------------------------------------------
    generateButtonMenuOption(caption) {
        let $btnMenuOption, html;
        html = [];
        html.push(`<div class="btnmenuoption" data-type="btnmenuoption">${caption}</div>`);
        $btnMenuOption = jQuery(html.join(''));
        return $btnMenuOption;
    };
    //----------------------------------------------------------------------------------------------
    addButtonMenuOptions($control, menuOptions) {
        let caption = $control.attr('data-caption');
        let html = [];
        html.push(`<div class="fwformcontrol btnmenu">${caption}`);
        html.push('  <i class="material-icons btnmenudd" style="vertical-align:middle">&#xE5C5;</i>');
        html.push('</div>');
        html.push('<div class="btnmenuoptions"></div>');
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
}

var FwMenu: any = new FwMenuClass();