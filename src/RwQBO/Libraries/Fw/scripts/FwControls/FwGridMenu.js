var FwGridMenuClass = /** @class */ (function () {
    function FwGridMenuClass() {
    }
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.init = function ($control) {
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.getDesignerProperties = function (data_type) {
        var propId = { caption: 'ID', datatype: 'string', attribute: 'id', defaultvalue: FwControl.generateControlId('tabs'), visible: true, enabled: true };
        var propClass = { caption: 'CSS Class', datatype: 'string', attribute: 'class', defaultvalue: 'fwcontrol fwmenu', visible: false, enabled: false };
        var propDataControl = { caption: 'Control', datatype: 'string', attribute: 'data-control', defaultvalue: 'FwMenu', visible: true, enabled: false };
        var propDataType = { caption: 'Type', datatype: 'string', attribute: 'data-type', defaultvalue: data_type, visible: false, enabled: false };
        var propDataVersion = { caption: 'Version', datatype: 'string', attribute: 'data-version', defaultvalue: '1', visible: false, enabled: false };
        var propRenderMode = { caption: 'Render Mode', datatype: 'string', attribute: 'data-rendermode', defaultvalue: 'template', visible: false, enabled: false };
        var properties = [propId, propClass, propDataControl, propDataType, propDataVersion, propRenderMode];
        return properties;
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.renderDesignerHtml = function ($control) {
        $control.attr('data-rendermode', 'designer');
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.renderRuntimeHtml = function ($control) {
        $control.attr('data-rendermode', 'runtime');
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.renderTemplateHtml = function ($control) {
        $control.attr('data-rendermode', 'template');
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.getMenuControl = function (controltype) {
        var html, $menuObject;
        html = [];
        html.push('<div class="fwcontrol fwmenu ' + controltype + '" data-control="FwMenu">');
        html.push('</div>');
        $menuObject = jQuery(html.join(''));
        return $menuObject;
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addSubMenu = function ($control) {
        var $btn, html;
        html = [];
        html.push('<div class="submenubutton">');
        html.push('<div class="icon"><i class="material-icons">&#xE5D2;</i></div>'); //menu
        html.push('<div class="submenu"></div>');
        html.push('</div>');
        $btn = jQuery(html.join(''));
        $btn
            .on('click', '.icon', function (e) {
            var $this, $browse, maxZIndex;
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
        $control.append($btn);
        return $btn;
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addSubMenuColumn = function ($control) {
        var html, $column;
        html = [];
        html.push('<div class="submenu-column"></div>');
        $column = jQuery(html.join(''));
        $control.find('.submenu').append($column);
        return $column;
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addSubMenuGroup = function ($control, groupcaption, securityid) {
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
    FwGridMenuClass.prototype.addSubMenuBtn = function ($group, caption, securityid) {
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
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addCaption = function ($control, caption) {
        var $caption, html;
        html = [];
        html.push('<div class="menucaption">' + caption + '</div>');
        $caption = jQuery(html.join(''));
        $control.append($caption);
        return $caption;
    };
    ;
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addStandardBtn = function ($control, caption, securityid) {
        var $btn, btnHtml, btnId, id;
        $btn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                id = program.uniqueId(8);
                btnId = 'btn' + id;
                securityid = (typeof securityid === 'string') ? securityid : '';
                btnHtml = [];
                btnHtml.push('<div id="' + btnId + '" class="btn" tabindex="0" data-securityid="' + securityid + '">');
                //if ($control.hasClass('default')) {
                switch (caption) {
                    case 'New':
                        btnHtml.push('<i class="material-icons">&#xE145;</i>');
                        break; //add
                    case 'Edit':
                        btnHtml.push('<i class="material-icons">&#xE254;</i>');
                        break; //mode_edit
                    case 'Delete':
                        btnHtml.push('<i class="material-icons">&#xE872;</i>');
                        break; //delete
                    case 'Save':
                        btnHtml.push('<i class="material-icons">&#xE161;</i>');
                        break; //save
                }
                //}
                var $browse = $control.closest('[data-control="FwBrowse"]');
                if ($browse.attr('data-type') === 'Browse') {
                    btnHtml.push('  <div class="btn-text">' + caption + '</div>');
                }
                btnHtml.push('</div>');
                $btn = $btn.add(btnHtml.join(''));
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }
        else {
            throw 'FwGridMenu.addStandardBtn: ' + caption + ' caption is not defined in translation';
        }
        if ($control.find('.buttonbar').length === 0) {
            $control.append('<div class="buttonbar"></div>');
        }
        $control.find('.buttonbar').append($btn);
        return $btn;
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addViewBtn = function ($control, caption, subitems) {
        var $btn, btnHtml, btnId, id, $ddBtn, subitemFunc;
        id = program.uniqueId(8);
        btnId = 'btn' + id;
        btnHtml = [];
        btnHtml.push('<div id="' + btnId + '" class="ddviewbtn">');
        btnHtml.push('  <div class="ddviewbtn-caption">' + caption + ':</div>');
        btnHtml.push('  <div class="ddviewbtn-select" tabindex="0">');
        btnHtml.push('    <div class="ddviewbtn-select-value"></div>');
        btnHtml.push('    <i class="material-icons">&#xE5C5;</i>'); //arrow_drop_down
        btnHtml.push('    <div class="ddviewbtn-dropdown"></div>');
        btnHtml.push('  </div>');
        btnHtml.push('</div>');
        $btn = jQuery(btnHtml.join(''));
        for (var i = 0; i < subitems.length; i++) {
            $ddBtn = subitems[i];
            $ddBtn.on('click', function (e) {
                var $this;
                $this = jQuery(this);
                $btn.find('.ddviewbtn-select-value').empty().html($this.find('.ddviewbtn-dropdown-btn-caption').html());
                $this.siblings('.ddviewbtn-dropdown-btn.active').removeClass('active');
                $this.addClass('active');
            });
            $btn.find('.ddviewbtn-dropdown').append($ddBtn);
        }
        $btn.find('.ddviewbtn-select-value').html($btn.find('.ddviewbtn-dropdown-btn.active .ddviewbtn-dropdown-btn-caption').html());
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
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.generateDropDownViewBtn = function (caption, active) {
        var btnHtml, $ddBtn;
        btnHtml = [];
        btnHtml.push('<div class="ddviewbtn-dropdown-btn' + ((active) ? ' active' : '') + '">');
        btnHtml.push('<div class="ddviewbtn-dropdown-btn-caption">' + caption + '</div>');
        btnHtml.push('</div>');
        $ddBtn = jQuery(btnHtml.join(''));
        return $ddBtn;
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addVerticleSeparator = function ($control) {
        var html, $vr;
        html = [];
        html.push('<div class="vr"></div>');
        $vr = jQuery(html.join(''));
        $control.find('.buttonbar').append($vr);
    };
    //---------------------------------------------------------------------------------
    FwGridMenuClass.prototype.addCustomContent = function ($control, $content) {
        $control.find('.buttonbar').append($content);
    };
    return FwGridMenuClass;
}());
var FwGridMenu = new FwGridMenuClass();
//# sourceMappingURL=FwGridMenu.js.map