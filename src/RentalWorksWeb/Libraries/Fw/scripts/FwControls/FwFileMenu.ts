﻿class FwFileMenu {
    //---------------------------------------------------------------------------------
    static init($control) {
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
                .on('click', '.usermenu', function() {
                    var $usercontrols, maxZIndex;
                    $usercontrols = jQuery(this).parent();
                    if (!$usercontrols.hasClass('active')) {
                        maxZIndex = FwFunc.getMaxZ('*');
                        $usercontrols.find('.user-dropdown').css('z-index', maxZIndex+1);
                        $usercontrols.addClass('active');

                        jQuery(document).one('click', function closeMenu(e) {
                            if (($usercontrols.has(e.target).length === 0)) {
                                $usercontrols.removeClass('active');
                                $usercontrols.find('.user-dropdown').css('z-index', '0');
                            } else if ($usercontrols.hasClass('active')) {
                                jQuery(document).one('click', closeMenu);
                            }
                        });
                    } else {
                        $usercontrols.removeClass('active');
                        $usercontrols.find('.user-dropdown').css('z-index', '0');
                    }
                })
            ;
        }
    }
    //---------------------------------------------------------------------------------
    static renderRuntimeHtml($control) {
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
    static renderUserControl($control, usertype, username) {
        var html: string[] = [];
        html.push(`<div class="user" title="User Type: ${usertype}">${username}</div>`);
        html.push('<i class="material-icons usermenu">&#xE7FD;</i>'); //person
        html.push('<div class="user-dropdown">');
        html.push('  <div class="menuitems"></div>');
        html.push('  <div class="staticinfo">');
        html.push(`    <div class="copyright">&copy; ${new Date().getFullYear().toString()} Database Works, Inc.</div>`);
        html.push(`    <div class="version">${applicationConfig.version}</div>`);
        html.push('  </div>');
        html.push('</div>');
        var htmlString = html.join('\n');
        $control.find('.user-controls').append(htmlString);
    }
    //---------------------------------------------------------------------------------
    static addMenuItemToUserControl($view: JQuery, menuitem: FileMenu.UserControlMenuItem) {
        var $menuitems = $view.find('.user-dropdown .menuitems');
        var $menuitem = jQuery(`<div class="item ${menuitem.cssclass}">${menuitem.caption}</div>`);
        $menuitems.append($menuitem);
        $menuitem.on('click', menuitem.click);
    }
    //---------------------------------------------------------------------------------
    static removeMenuItemFromUserControl($view: JQuery, cssclass: string) {
        var $menuitems = $view.find(`.user-dropdown .menuitems .${cssclass}`).remove();
    }
    //---------------------------------------------------------------------------------
    static addLinkToUserControl($view, link: FileMenu.UserControlLink) {
        
    }
    //---------------------------------------------------------------------------------
    static removeLinkFromUserControl($view: JQuery, cssclass: string) {

    }
    //---------------------------------------------------------------------------------
    static addMenu = function($control, caption) {
        var $menu, fileMenuHtml;
        try {
            fileMenuHtml = [];
            fileMenuHtml.push('<div class="file-menu" data-caption="' + caption + '">');
                fileMenuHtml.push('<div class="caption">'+ caption + '</div>');
                fileMenuHtml.push('<div class="menu"></div>');
            fileMenuHtml.push('</div>');
            $menu = jQuery(fileMenuHtml.join(''));
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
    static generateStandardModuleBtn($menu, securityid, caption, modulenav, imgurl) {
        var $modulebtn, btnHtml, btnId, version;
        securityid = (typeof securityid === 'string') ? securityid : '';
        $modulebtn = jQuery();
        if ((caption !== '') && (typeof caption !== 'undefined')) {
            try {
                btnId = 'btnModule' + securityid;
                btnHtml = [];
                btnHtml.push('<div id="' + btnId + '" class="modulebtn" data-securityid="' + securityid + '">');
                    btnHtml.push('<div class="modulebtn-text">');
                        btnHtml.push(caption);
                    btnHtml.push('</div>');
                btnHtml.push('</div>');
                $modulebtn = $modulebtn.add(btnHtml.join(''));
            } catch (ex) {
                FwFunc.showError(ex);
            }
        } else {
            throw 'FwRibbon.generateStandardModuleBtn: ' + caption + ' caption is not defined in translation';
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
    static generateDropDownModuleBtn($menu, securityid, caption, imgurl, subitems) {
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
            throw 'FwRibbon.generateDropDownModuleBtn: ' + securityid + ' caption is not defined in translation';
        }

        $menu.find('.menu').append($modulebtn);
    }
    //---------------------------------------------------------------------------------
}

namespace FileMenu {
    export class UserControlMenuItem {
        caption: string = '';
        cssclass: string = '';
        click: (evt: JQuery.Event<HTMLElement, null>) => void;
    }

    export class UserControlLink {
        caption: string = '';
        cssclass: string = '';
        click: (evt: JQuery.Event<HTMLElement, null>) => void;
        html: string = '';
    }
}

