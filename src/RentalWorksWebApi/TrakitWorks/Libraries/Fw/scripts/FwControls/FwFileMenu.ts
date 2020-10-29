class FwFileMenuClass {
    //---------------------------------------------------------------------------------
    init($control) {
        if ($control.attr('data-version') == '1') {
            $control
                .on('click', '.file-menu', function(e) {
                    var $this, maxZIndex;
                    $this = jQuery(this);
                    e.preventDefault();
                    if (!$this.hasClass('active')) {
                        maxZIndex = FwFunc.getMaxZ('*');
                        $this.find('.menu').css('z-index', maxZIndex+1);
                        $this.addClass('active');

                        jQuery(document).one('click', function closeMenu(e) {
                            if (($this.parent().has(e.target).length === 0)) {
                                $this.parent().find('.file-menu').removeClass('active');
                                $this.parent().find('.file-menu .menu').css('z-index', '0');
                            } else if ($this.hasClass('active')) {
                                jQuery(document).one('click', closeMenu);
                            }
                        });
                    } else {
                        $this.removeClass('active');
                        $this.find('.menu').css('z-index', '0');
                    }
                })
                .on('mouseover', '.file-menu', function() {
                    var $this, maxZIndex;
                    $this = jQuery(this);
                    if ($this.parent().find('.file-menu').hasClass('active') && !$this.hasClass('active')) {
                        $this.parent().find('.file-menu').removeClass('active');
                        $this.parent().find('.file-menu .menu').css('z-index', '0');

                        maxZIndex = FwFunc.getMaxZ('*');
                        $this.find('.menu').css('z-index', maxZIndex+1);
                        $this.addClass('active');
                    }
                })
            ;
        } else if ($control.attr('data-version') == '2') {
            $control
                .on('click', '.appmenu', function(e) {
                    var $this, maxZIndex;
                    $this = jQuery(this);
                    e.preventDefault();
                    if (!$this.parent().hasClass('active')) {
                        maxZIndex = FwFunc.getMaxZ('*');
                        $this.parent().find('.file-menu-dropdown').css('z-index', maxZIndex+1);
                        $this.parent().addClass('active');

                        jQuery(document).one('click', function closeMenu(e) {
                            if ($this.parent().has(e.target).length === 0) {
                                $this.parent().removeClass('active');
                                $this.parent().find('.file-menu-dropdown').css('z-index', '0');
                            } else if ($this.parent().hasClass('active')) {
                                jQuery(document).one('click', closeMenu);
                            }
                        });
                    } else {
                        $this.parent().removeClass('active');
                        $this.parent().find('.file-menu-dropdown').css('z-index', '0');
                    }
                })
                .on('click', '.ddmodulebtn .ddmodulebtn-caption', function() {
                    var $ddmodulebtn;
                    $ddmodulebtn = jQuery(this).parent();
                    if (!$ddmodulebtn.hasClass('active')) {
                        $ddmodulebtn.addClass('active');
                        $ddmodulebtn.find('.ddmodulebtn-dropdown').slideToggle(300);
                    } else {
                        $ddmodulebtn.removeClass('active');
                        $ddmodulebtn.find('.ddmodulebtn-dropdown').slideToggle(300);
                    }
                })
                .on('click', '.modulebtn, .ddmodulebtn-dropdown-btn', function() {
                    if ($control.find('.file-menus-wrapper').hasClass('active')) {
                        $control.find('.file-menus-wrapper').removeClass('active');
                        $control.find('.file-menus-wrapper .ddmodulebtn.active .ddmodulebtn-dropdown').slideToggle(10)
                        $control.find('.file-menus-wrapper .ddmodulebtn.active').removeClass('active');
                    }
                })
                $control.on('click', '.usermenu', (event: JQuery.ClickEvent) => {
                    try {
                        var $usercontrol = jQuery('.usercontrol');
                        if (!$usercontrol.hasClass('active')) {
                            var maxZIndex = FwFunc.getMaxZ('*');
                            $usercontrol.find('.user-dropdown').css('z-index', maxZIndex + 1);
                            $usercontrol.addClass('active');

                            jQuery(document).one('click', function closeMenu(e: JQuery.ClickEvent) {
                                try {
                                    if (($usercontrol.has(<Element>e.target).length === 0)) {
                                        $usercontrol.removeClass('active');
                                        $usercontrol.find('.user-dropdown').css('z-index', '0');
                                    } else if ($usercontrol.hasClass('active')) {
                                        jQuery(document).one('click', closeMenu);
                                    }
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        } else {
                            $usercontrol.removeClass('active');
                            $usercontrol.find('.user-dropdown').css('z-index', '0');
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            ;
        }
    }
    //---------------------------------------------------------------------------------
    renderRuntimeHtml($control) {
        var html;
        $control.attr('data-rendermode', 'runtime');
        html = [];
        html.push('<div class="file-menus-wrapper"></div>');
        html.push('<div class="user-controls"></div>');

        $control.html(html.join(''));

        if ($control.attr('data-version') == '2') {
            $control.find('.file-menus-wrapper').after('<div class="logo"></div>');
            $control.find('.file-menus-wrapper').append('<i class="material-icons appmenu">&#xE5D2;</i>'); //menu
            $control.find('.file-menus-wrapper').append('<div class="file-menu-dropdown"></div>');
        }
    }
    //---------------------------------------------------------------------------------
    UserControl_render($view: JQuery): JQuery {
        var html: string[] = [];
        html.push('<div class="usercontrol">');
        html.push('  <div class="systembar"></div>')
        html.push('  <i class="material-icons usermenu">&#xE7FD;</i>'); //person
        html.push('  <div class="user-dropdown">');
        html.push('    <div class="menuitems"></div>');
        html.push('    <div class="staticinfo">');
        html.push(`      <div class="copyright">&copy; ${new Date().getFullYear().toString()} Database Works, Inc.</div>`);
        html.push(`      <div class="version">${applicationConfig.version}</div>`);
        html.push('    </div>');
        html.push('  </div>');
        html.push('</div>');
        var htmlString = html.join('\n');
        var $control = jQuery(htmlString);

        $control.on('click', '.menuitem', (e) => {
            try {
                var $usercontrol = this.UserControl_getUserControl();
                this.UserControl_hideDropDownMenu($usercontrol);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $view.find('.user-controls').append($control);
        
        return $control;
    }
    //---------------------------------------------------------------------------------
    UserControl_getUserControl(): JQuery {
        return jQuery('.usercontrol');
    }
    //---------------------------------------------------------------------------------
    UserControl_getDropDownMenuItems($usercontrol?: JQuery): JQuery {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        return $usercontrol.find('.user-dropdown .menuitems');
    }
    //---------------------------------------------------------------------------------
    UserControl_getDropDownMenuItem(id: string, $usercontrol?: JQuery): JQuery {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        return this.UserControl_getDropDownMenuItems($usercontrol).find(`.menuitem[data-id="${id}"]`);
    }
    //---------------------------------------------------------------------------------
    UserControl_addDropDownMenuItem(id: string, $control: JQuery, $usercontrol?: JQuery) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        var $menuitem = $control.attr('data-id', id).addClass('menuitem'); 
        this.UserControl_getDropDownMenuItems($usercontrol).append($menuitem);
    }
    //---------------------------------------------------------------------------------
    UserControl_setMenuItemHtml($usercontrol: JQuery, id: string, html: string) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        this.UserControl_getDropDownMenuItem(id, $usercontrol).html(html);
    }
    //---------------------------------------------------------------------------------
    UserControl_removeMenuItem($usercontrol: JQuery, id: string) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        this.UserControl_getDropDownMenuItem(id, $usercontrol).remove();
    }
    //---------------------------------------------------------------------------------
    UserControl_toggleMenuItem(id: string, isVisible?: boolean, $usercontrol?: JQuery) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        var $menuitem = this.UserControl_getDropDownMenuItem(id, $usercontrol);
        if (typeof isVisible === 'undefined') {
            isVisible = !$menuitem.is(':visible');
        }
        $menuitem.toggle(isVisible);
    }
    //---------------------------------------------------------------------------------
    UserControl_hideDropDownMenu($usercontrol?: JQuery) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        $usercontrol.removeClass('active');
        $usercontrol.find('.user-dropdown').css('z-index', '0');
    }
    //---------------------------------------------------------------------------------
    UserControl_getSystemBar($usercontrol?: JQuery): JQuery {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        return $usercontrol.find('.systembar');
    }
    //---------------------------------------------------------------------------------
    UserControl_getSystemBarControl(id: string, $usercontrol?: JQuery): JQuery {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        return $usercontrol.find(`.systembarcontrol[data-id=${id}]`);
    }
    //---------------------------------------------------------------------------------
    UserControl_addSystemBarControl(id: string, $control: JQuery, $usercontrol?: JQuery) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        var $systembarcontrol = $control.attr('data-id', id).addClass('systembarcontrol');
        this.UserControl_getSystemBar($usercontrol).append($systembarcontrol);
    }
    //---------------------------------------------------------------------------------
    UserControl_setSystemBarControlHtml(id: string, html: string, $usercontrol?: JQuery) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        this.UserControl_getSystemBarControl(id, $usercontrol).html(html);
    }
    //---------------------------------------------------------------------------------
    UserControl_removeSystemBarControl(id: string, $usercontrol?: JQuery) {
        $usercontrol = (typeof $usercontrol !== 'undefined') ? $usercontrol : this.UserControl_getUserControl();
        var $systembarcontrol = this.UserControl_getSystemBarControl(id, $usercontrol).remove();
    }
    //---------------------------------------------------------------------------------
    addMenu($control, caption): JQuery {
        try {
            var fileMenuHtml = [];
            fileMenuHtml.push(`<div class="file-menu" data-caption="${caption}">`);
            fileMenuHtml.push(`  <div class="caption">${caption}</div>`);
            fileMenuHtml.push('  <div class="menu"></div>');
            fileMenuHtml.push('</div>');
            var $menu = jQuery(fileMenuHtml.join('\n'));
            if ($control.attr('data-version') == '1') {
                $control.find('.file-menus-wrapper').append($menu);
            } else if ($control.attr('data-version') == '2') {
                $control.find('.file-menu-dropdown').append($menu);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return $menu;
    }
    //---------------------------------------------------------------------------------
    generateStandardModuleBtn($menu, securityid, caption, modulenav, imgurl) {
        var $modulebtn, btnHtml, btnId, version;
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnId = 'btnModule' + securityid;
                btnHtml = [];
                btnHtml.push(`<div id="${btnId}" class="modulebtn" data-securityid="${securityid}">`);
                btnHtml.push(`  <div class="modulebtn-text">${caption}</div>`);
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwFileMenu.generateStandardModuleBtn: ' + caption + ' caption is not defined in translation';
        }

        $modulebtn
            .on('click', function() {
                try {
                    if (modulenav != '') {
                        program.getModule(modulenav);
                        //program.navigate(modulenav);
                    } else {
                        FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;

        $menu.find('.menu').append($modulebtn);
    }
    //---------------------------------------------------------------------------------
    generateDropDownModuleBtn($menu, securityid, caption, imgurl, subitems) {
        var $modulebtn, btnHtml, subitemHtml, $subitem, version;
    
        version    = $menu.closest('.fwfilemenu').attr('data-version');
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
                jQuery.each(subitems, function(index, value) {
                    $subitem = jQuery(subitemHtml.join(''));
                    $subitem.attr('data-securityid', subitems[index].id);
                    $subitem.find('.ddmodulebtn-dropdown-btn-text').html(value.caption);

                    $subitem.on('click', function() {
                        try {
                            if (value.modulenav) {
                                program.getModule(value.modulenav);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                            }
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
            throw 'FwFileMenu.generateDropDownModuleBtn: ' + securityid + ' caption is not defined in translation';
        }

        $menu.find('.menu').append($modulebtn);
    }
    //---------------------------------------------------------------------------------
}
var FwFileMenu = new FwFileMenuClass();

//namespace FileMenu {
//    export class UserControlMenuItem {
//        id: string = '';
//        caption: string = '';
//        cssclass: string = '';
//        click: (evt: JQuery.Event<HTMLElement, null>) => void;
//    }

//    export class UserControlLink {
//        caption: string = '';
//        cssclass: string = '';
//        click: (evt: JQuery.Event<HTMLElement, null>) => void;
//        html: string = '';
//    }
//}

