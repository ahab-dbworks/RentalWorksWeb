var FwMenuClass = (function () {
    function FwMenuClass() {
    }
    FwMenuClass.prototype.upgrade = function ($control) {
        var properties, i, data_type;
        data_type = $control.attr('data-type');
        properties = this.getDesignerProperties(data_type);
        for (i = 0; i < properties.length; i++) {
            if (typeof $control.attr(properties[i].attribute) === 'undefined') {
                $control.attr(properties[i].attribute, properties[i].defaultvalue);
            }
        }
    };
    FwMenuClass.prototype.init = function ($control) {
        FwMenu.upgrade($control);
    };
    ;
    FwMenuClass.prototype.getDesignerProperties = function (data_type) {
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
    ;
    FwMenuClass.prototype.renderDesignerHtml = function ($control) {
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'designer');
    };
    ;
    FwMenuClass.prototype.renderRuntimeHtml = function ($control) {
        var html, $usercontrolChildren;
        $control.attr('data-rendermode', 'runtime');
    };
    ;
    FwMenuClass.prototype.renderTemplateHtml = function ($control) {
        var data_type, data_rendermode, html;
        data_type = $control.attr('data-type');
        data_rendermode = $control.attr('data-rendermode');
        $control.attr('data-rendermode', 'template');
    };
    ;
    FwMenuClass.prototype.getMenuControl = function (controltype) {
        var html, $menuObject;
        html = [];
        html.push('<div class="fwcontrol fwmenu ' + controltype + '" data-control="FwMenu">');
        html.push('<div class="buttonbar"></div>');
        html.push('</div>');
        $menuObject = jQuery(html.join(''));
        return $menuObject;
    };
    ;
    FwMenuClass.prototype.addSubMenu = function ($control) {
        var $btn, html;
        html = [];
        html.push('<div class="submenubutton">');
        html.push('<div class="icon"><i class="material-icons">&#xE5D2;</i></div>');
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
                    }
                    else if ($this.parent().hasClass('active')) {
                        jQuery(document).one('click', closeMenu);
                    }
                });
            }
            else {
                $this.parent().removeClass('active');
                $this.parent().find('.submenu').css('z-index', '0');
            }
        })
            .on('click', '.submenu-btn', function () {
            if ($btn.hasClass('active')) {
                $btn.removeClass('active');
            }
        });
        $control.prepend($btn);
        return $btn;
    };
    ;
    FwMenuClass.prototype.addSubMenuColumn = function ($control) {
        var html, $column;
        html = [];
        html.push('<div class="submenu-column"></div>');
        $column = jQuery(html.join(''));
        $control.find('.submenu').append($column);
        return $column;
    };
    ;
    FwMenuClass.prototype.addSubMenuGroup = function ($control, groupcaption, securityid) {
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
    ;
    FwMenuClass.prototype.addSubMenuBtn = function ($group, caption, securityid) {
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
    ;
    FwMenuClass.prototype.addStandardBtn = function ($control, caption, securityid) {
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
                        case 'New':
                            btnHtml.push('<i class="material-icons">&#xE145;</i>');
                            break;
                        case 'Edit':
                            btnHtml.push('<i class="material-icons">&#xE254;</i>');
                            break;
                        case 'Delete':
                            btnHtml.push('<i class="material-icons">&#xE872;</i>');
                            break;
                        case 'Save':
                            btnHtml.push('<i class="material-icons">&#xE161;</i>');
                            break;
                        case 'Find':
                            btnHtml.push('<i class="material-icons">search</i>');
                            break;
                    }
                }
                btnHtml.push('  <div class="btn-text">' + caption + '</div>');
                btnHtml.push('</div>');
                $btn = $btn.add(btnHtml.join(''));
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }
        else {
            throw 'FwMenu.addStandardBtn: ' + caption + ' caption is not defined in translation';
        }
        $control.find('.buttonbar').append($btn);
        return $btn;
    };
    ;
    FwMenuClass.prototype.addViewBtn = function ($control, caption, subitems, allowMultiple, filterField) {
        var $btn, btnHtml, btnId, id, $ddBtn;
        id = program.uniqueId(8);
        btnId = 'btn' + id;
        btnHtml = [];
        btnHtml.push("<div id=\"" + btnId + "\" class=\"ddviewbtn\">");
        btnHtml.push("  <div class=\"ddviewbtn-caption\">" + caption + ":</div>");
        btnHtml.push("  <div class=\"ddviewbtn-select " + (allowMultiple ? ' multifilter' : '') + "\" tabindex=\"0\">");
        btnHtml.push('    <div class="ddviewbtn-select-value"></div>');
        btnHtml.push('    <i class="material-icons">&#xE5C5;</i>');
        btnHtml.push('    <div class="ddviewbtn-dropdown"></div>');
        btnHtml.push('  </div>');
        btnHtml.push('</div>');
        $btn = jQuery(btnHtml.join(''));
        var controller = $control.closest('.fwbrowse').attr('data-controller');
        var selectedFilterValues = [];
        for (var i = 0; i < subitems.length; i++) {
            $ddBtn = subitems[i];
            if (allowMultiple) {
                $ddBtn.prepend("<input type=\"checkbox\">");
                if (typeof filterField !== 'undefined') {
                    if (typeof window[controller].ActiveViewFields[filterField] == 'undefined') {
                        window[controller].ActiveViewFields[filterField] = ["ALL"];
                    }
                }
            }
            $ddBtn.on('click', function (e) {
                var $this = jQuery(e.currentTarget);
                var $browse = $this.closest('.fwbrowse');
                var caption = $this.find('.ddviewbtn-dropdown-btn-caption').html();
                if (allowMultiple) {
                    e.stopPropagation();
                    var value_1 = $this.attr('data-value');
                    var fields = window[controller].ActiveViewFields[filterField];
                    if (value_1 === "ALL")
                        $this.addClass('select-all-filters');
                    var isSelectAllFilters = $this.hasClass('select-all-filters');
                    var isChecked = $this.find('input[type="checkbox"]').prop('checked');
                    if (jQuery(e.target).attr('type') != 'checkbox') {
                        $this.find('input[type="checkbox"]').prop('checked', !isChecked);
                        isChecked = !isChecked;
                    }
                    var indexOfAll = fields.indexOf("ALL");
                    if (isSelectAllFilters) {
                        if (isChecked) {
                            jQuery($this).siblings().find('input[type="checkbox"]').prop('checked', true);
                            selectedFilterValues = ["All"];
                            fields = ["ALL"];
                        }
                        else {
                            selectedFilterValues = [];
                            fields = [];
                            var checkedFilters = $this.siblings().find('input[type="checkbox"]:checked');
                            for (var i_1 = 0; i_1 < checkedFilters.length; i_1++) {
                                var filterCaption = jQuery(checkedFilters[i_1]).siblings('.ddviewbtn-dropdown-btn-caption').html();
                                var filterValue = jQuery(checkedFilters[i_1]).parents('.ddviewbtn-dropdown-btn').attr('data-value');
                                selectedFilterValues.push(filterCaption);
                                fields.push(filterValue);
                            }
                        }
                    }
                    else {
                        jQuery($this).siblings('.select-all-filters').find('input[type="checkbox"]').prop('checked', false);
                        if (indexOfAll != -1) {
                            selectedFilterValues = [];
                            fields = [];
                            var checkedFilters = $this.siblings().find('input[type="checkbox"]:checked');
                            for (var i_2 = 0; i_2 < checkedFilters.length; i_2++) {
                                var filterCaption = jQuery(checkedFilters[i_2]).siblings('.ddviewbtn-dropdown-btn-caption').html();
                                var filterValue = jQuery(checkedFilters[i_2]).parents('.ddviewbtn-dropdown-btn').attr('data-value');
                                selectedFilterValues.push(filterCaption);
                                fields.push(filterValue);
                            }
                        }
                        if (isChecked) {
                            if (selectedFilterValues.indexOf(caption) == -1) {
                                selectedFilterValues.push(caption);
                                fields.push(value_1);
                            }
                        }
                        else if (isChecked === false) {
                            selectedFilterValues = selectedFilterValues.filter(function (val) { return val !== caption; });
                            fields = fields.filter(function (val) { return val !== value_1; });
                        }
                        if (selectedFilterValues.length <= 3) {
                            caption = selectedFilterValues.join(', ');
                        }
                        else {
                            var firstThreeFilters = selectedFilterValues.slice(0, 3);
                            caption = firstThreeFilters.join(", ") + " + " + (selectedFilterValues.length - 3) + " more";
                        }
                    }
                    window[controller].ActiveViewFields[filterField] = fields;
                    var request = {
                        WebUserId: JSON.parse(sessionStorage.getItem('userid')).webusersid,
                        ModuleName: window[controller].Module,
                        ActiveViewFields: JSON.stringify(window[controller].ActiveViewFields)
                    };
                    if (typeof window[controller].ActiveViewFieldsId == 'undefined') {
                        FwAppData.apiMethod(true, 'POST', "api/v1/browseactiveviewfields/", request, FwServices.defaultTimeout, function onSuccess(response) {
                            window[controller].ActiveViewFieldsId = response.Id;
                        }, null, null);
                    }
                    else {
                        request["Id"] = window[controller].ActiveViewFieldsId;
                        FwAppData.apiMethod(true, 'POST', "api/v1/browseactiveviewfields/", request, FwServices.defaultTimeout, function onSuccess(response) {
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
        if (typeof filterField !== 'undefined' && typeof window[controller].ActiveViewFields[filterField] !== 'undefined') {
            for (var i_3 = 0; i_3 < window[controller].ActiveViewFields[filterField].length; i_3++) {
                var $this = window[controller].ActiveViewFields[filterField][i_3];
                var $ddbtn = $btn.find("[data-value=\"" + $this + "\"]");
                var caption_1 = $ddbtn.find(".ddviewbtn-dropdown-btn-caption").html();
                if ($this == 'ALL') {
                    $ddbtn.addClass('select-all-filters');
                    $btn.find('input').prop('checked', true);
                    selectedFilterValues = ["All"];
                }
                else {
                    $ddbtn.find('input').prop('checked', true);
                    selectedFilterValues.push(caption_1);
                }
            }
            var filterCaption = void 0;
            if (selectedFilterValues.length <= 3) {
                filterCaption = selectedFilterValues.join(', ');
            }
            else {
                var firstThreeFilters = selectedFilterValues.slice(0, 3);
                filterCaption = firstThreeFilters.join(", ") + " + " + (selectedFilterValues.length - 3) + " more";
            }
            $btn.find('.ddviewbtn-select-value').empty().html(filterCaption);
        }
        else {
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
                    }
                    else if ($this.hasClass('active')) {
                        jQuery(document).one('click', closeMenu);
                    }
                });
            }
            else {
                $this.removeClass('active');
                $this.find('.ddviewbtn-dropdown').css('z-index', '0');
            }
        });
        $control.find('.buttonbar').append($btn);
        return $btn;
    };
    ;
    FwMenuClass.prototype.generateDropDownViewBtn = function (caption, active, value) {
        var btnHtml, $ddBtn;
        btnHtml = [];
        if (typeof value !== 'undefined') {
            btnHtml.push("<div class=\"ddviewbtn-dropdown-btn " + (active ? ' active' : '') + "\" data-value=\"" + value + "\">");
        }
        else {
            btnHtml.push("<div class=\"ddviewbtn-dropdown-btn " + (active ? ' active' : '') + "\">");
        }
        btnHtml.push("<div class=\"ddviewbtn-dropdown-btn-caption\">" + caption + "</div>");
        btnHtml.push("</div>");
        $ddBtn = jQuery(btnHtml.join(''));
        return $ddBtn;
    };
    ;
    FwMenuClass.prototype.addVerticleSeparator = function ($control) {
        var html, $vr;
        html = [];
        html.push('<div class="vr"></div>');
        $vr = jQuery(html.join(''));
        $control.find('.buttonbar').append($vr);
    };
    ;
    FwMenuClass.prototype.addCustomContent = function ($control, $content) {
        $control.find('.buttonbar').append($content);
    };
    ;
    FwMenuClass.prototype.generateButtonMenuOption = function (caption) {
        var $btnMenuOption, html;
        html = [];
        html.push("<div class=\"btnmenuoption\" data-type=\"btnmenuoption\">" + caption + "</div>");
        $btnMenuOption = jQuery(html.join(''));
        return $btnMenuOption;
    };
    ;
    FwMenuClass.prototype.addButtonMenuOptions = function ($control, menuOptions) {
        var caption = $control.attr('data-caption');
        var html = [];
        html.push("<div class=\"fwformcontrol btnmenu\">" + caption);
        html.push('  <i class="material-icons btnmenudd" style="vertical-align:middle">&#xE5C5;</i>');
        html.push('</div>');
        html.push('<div class="btnmenuoptions"></div>');
        $control.append(html.join(''))
            .find('.btnmenuoptions')
            .append(menuOptions);
        $control.on('click', '.btnmenudd', function (e) {
            e.stopPropagation();
            $control.find('.btnmenuoptions').toggle();
            jQuery(document).one('click', function closeMenu(e) {
                $control.find('.btnmenuoptions').toggle();
            });
        });
        return $control;
    };
    ;
    return FwMenuClass;
}());
var FwMenu = new FwMenuClass();
//# sourceMappingURL=FwMenu.js.map