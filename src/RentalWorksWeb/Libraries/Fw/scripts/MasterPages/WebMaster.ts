class WebMaster {
    private headerOptions: HeaderOptions = {
        title: 'Application Title',
        userControls: {
            links: [
                { title: 'Sign Off', navigation: 'logoff' }
            ]
        }
    };
    //---------------------------------------------------------------------------------
    getMasterView() {
        let $appmaster = jQuery('<div>')
            .attr('id', 'fw-app');

        let $appheader = this._renderHeader($appmaster, {
                title:        this._getAppTitle(),
                userControls: this.getUserControls()
            })
            .appendTo($appmaster);

        let $appmenu = this._renderMenu($appmaster, {
                menu: this.buildMainMenu()
            })
            .appendTo($appmaster);
        let $appbody = jQuery('<div>')
            .attr('id', 'fw-app-body')
            .appendTo($appmaster);

        program.setApplicationTheme(sessionStorage.getItem('applicationtheme'));
        this.events($appmaster);

        jQuery(window).resize((e) => {
            var $usercontrols = $appmaster.data('header').find('.app-usercontrols');
            if (((window.innerWidth <= 1239) && (!$usercontrols.hasClass('minified'))) || ((window.innerWidth > 1239) && ($usercontrols.hasClass('minified')))) {
                this._closeMainMenu($appmaster);
                $usercontrols.remove();
                this._renderUserControls($appmaster.data('header').find('.header-wrapper'), this.getUserControls());

                this.events($appmaster);
            }
        });

        return $appmaster;
    }
    //----------------------------------------------------------------------------------------------
    private _renderHeader($appmaster: JQuery, options: HeaderOptions): JQuery {
        let $appheader = jQuery('<div>')
            .attr('id', 'fw-app-header')
            .data('options', options);

        $appmaster.data('header', $appheader);

        let $headerwrapper = jQuery('<div>')
            .addClass('header-wrapper')
            .appendTo($appheader);

        var $appmenubutton = jQuery('<div>')
            .addClass('app-menu-button')
            .attr('tabindex', '-1')
            .appendTo($headerwrapper);

        var $menuicon = jQuery('<i>')
            .addClass('material-icons main')
            .html('menu')
            .appendTo($appmenubutton)
            .on('click', (e) => {
                this._openMainMenu($appmaster);
            });

        var $closemenuicon = jQuery('<i>')
            .addClass('material-icons close')
            .html('close')
            .css('display', 'none')
            .appendTo($appmenubutton)
            .on('click', (e) => {
                this._closeMainMenu($appmaster);
            });

        if (options.title) {
            //var title = (typeof options.title == 'string') ? options.title : options.title();
            var $title = jQuery('<div>')
                .addClass('app-title')
                //.html(title)
                .appendTo($headerwrapper);
            if (typeof options.title === 'string')
                $title.html(options.title)
            else
                $title.append(options.title)
        }

        if (options.userControls) {
            this._renderUserControls($headerwrapper, ((options.userControls === true) ? this.headerOptions.userControls : options.userControls) as MenuUserControls);
        }

        return $appheader;
    }
    //---------------------------------------------------------------------------------
    private _renderUserControls($menu: JQuery, userControls: MenuUserControls) {
        var me = this;

        var $usercontrols = jQuery('<div>')
            .addClass('app-usercontrols')
            .appendTo($menu);

        if (window.innerWidth <= 1239) $usercontrols.addClass('minified');

        if ($usercontrols.hasClass('minified')) {
            var $usermenu = jQuery('<div>')
                .addClass('app-usermenu')
                .attr('tabindex', '-1')
                .appendTo($usercontrols)
                .on('focus', (e) => {
                    me._openMenuTray($menu, $usermenu);
                })
                .on('focusout', (e) => {
                    setTimeout(function() {
                        if (document.hasFocus() && !jQuery.contains($usermenu[0], e.currentTarget) && e) {
                            me._closeMenuTray($menu, $usermenu);
                        }
                    }, 0);
                });

            var $menuicon = jQuery('<i>')
                .addClass('material-icons main')
                .html('account_box')
                .appendTo($usermenu);

            var $closemenuicon = jQuery('<i>')
                .addClass('material-icons close')
                .html('close')
                .css('display', 'none')
                .appendTo($usermenu)
                .on('click', (e) => {
                    e.stopPropagation();
                    $usermenu.blur();
                });

            var $menutray = jQuery('<div>')
                .addClass('app-menu-tray')
                .appendTo($usermenu);

            if (userControls.controls) {
                var $usercontrolcontainer = jQuery('<div>')
                    .addClass('container')
                    .appendTo($menutray);
                for (let usercontrol of userControls.controls) {
                    usercontrol.control.addClass('usercontrol').appendTo($usercontrolcontainer);
                }
            }

            if (userControls.bookmarks) {
                var $bookmarkcontainer = jQuery('<div>')
                    .addClass('container')
                    .appendTo($menutray);
                var $bookmarktitle = jQuery('<div>')
                    .addClass('bookmarkcaption')
                    .html('Favorites')
                    .appendTo($bookmarkcontainer);
                var $bookmarkicons = jQuery('<div>')
                    .addClass('bookmarkicons')
                    .appendTo($bookmarkcontainer);
                var $bookmarktitles = jQuery('<div>')
                    .addClass('bookmarktitles')
                    .appendTo($bookmarkcontainer);
                for (let bookmark of userControls.bookmarks) {
                    var $bookmark = jQuery('<div>')
                        .addClass('bookmark')
                        .data('bookmark', bookmark)
                        //.appendTo($usercontrols)
                        .on('click', (e) => {
                            if (jQuery(e.currentTarget).data('bookmark').navigation !== '') {
                                program.getModule(jQuery(e.currentTarget).data('bookmark').navigation);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                            }
                        });

                    if (bookmark.type === 'system') {
                        $bookmark.appendTo($bookmarkicons);
                        jQuery('<i>')
                            .addClass('material-icons')
                            .html(bookmark.icon)
                            .attr('title', bookmark.title)
                            .appendTo($bookmark);
                    } else if (bookmark.type === 'userdefined') {
                        $bookmark.appendTo($bookmarktitles);
                        jQuery('<i>')
                            .addClass('material-icons')
                            .html('star')
                            .appendTo($bookmark);
                        jQuery('<div>')
                            .addClass('bookmark-title')
                            .html(bookmark.title)
                            .appendTo($bookmark);
                        $bookmark.attr('title', bookmark.title);
                    }
                }
            }

            if (userControls.links) {
                for (let link of userControls.links) {
                    var $link = jQuery('<div>')
                        .addClass('link')
                        .html(link.title)
                        .data('link', link)
                        .appendTo($menutray)
                        .on('click', (e) => {
                            e.stopPropagation();
                            $usermenu.blur();
                            if (jQuery(e.currentTarget).data('link').navigation !== '') {
                                program.getModule(jQuery(e.currentTarget).data('link').navigation);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                            }
                        });
                }
            }

            var $staticinfo = jQuery('<div>')
                .addClass('staticinfo')
                .appendTo($menutray);
            var $copyright = jQuery('<div>')
                .addClass('copyright')
                .html(`&copy; ${new Date().getFullYear().toString()} Database Works, Inc.`)
                .appendTo($staticinfo);
            var $version = jQuery('<div>')
                .addClass('version')
                .html(`${applicationConfig.version}`)
                .appendTo($staticinfo);
        } else {
            if (userControls.controls) {
                for (let usercontrol of userControls.controls) {
                    usercontrol.control.addClass('usercontrol').appendTo($usercontrols);
                }
            }

            var $usermenu = jQuery('<div>')
                .addClass('app-usermenu')
                .attr('tabindex', '-1')
                .appendTo($usercontrols)
                .on('focus', (e) => {
                    me._openMenuTray($menu, $usermenu);
                })
                .on('focusout', (e) => {
                    setTimeout(function() {
                        if (document.hasFocus() && !jQuery.contains($usermenu[0], e.currentTarget) && e) {
                            me._closeMenuTray($menu, $usermenu);
                        }
                    }, 0);
                });

            var $menuicon = jQuery('<i>')
                .addClass('material-icons main')
                .html('account_box')
                .appendTo($usermenu);

            var $closemenuicon = jQuery('<i>')
                .addClass('material-icons close')
                .html('close')
                .css('display', 'none')
                .appendTo($usermenu)
                .on('click', (e) => {
                    e.stopPropagation();
                    $usermenu.blur();
                });

            var $menutray = jQuery('<div>')
                .addClass('app-menu-tray')
                .appendTo($usermenu);

            var $bookmarkcontainer = jQuery('<div>')
                .addClass('container')
                .appendTo($menutray);
            var $bookmarktitle = jQuery('<div>')
                .addClass('bookmarkcaption')
                .html('Favorites')
                .appendTo($bookmarkcontainer);
            var $bookmarkicons = jQuery('<div>')
                .addClass('bookmarkicons')
                .appendTo($bookmarkcontainer);
            var $bookmarktitles = jQuery('<div>')
                .addClass('bookmarktitles')
                .appendTo($bookmarkcontainer);
            for (let bookmark of userControls.bookmarks) {
                var $bookmark = jQuery('<div>')
                    .addClass('bookmark')
                    .data('bookmark', bookmark)
                    //.appendTo($usercontrols)
                    .on('click', (e) => {
                        if (jQuery(e.currentTarget).data('bookmark').navigation !== '') {
                            program.getModule(jQuery(e.currentTarget).data('bookmark').navigation);
                        } else {
                            FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                        }
                    });

                if (bookmark.type === 'system') {
                    $bookmark.appendTo($bookmarkicons);
                    jQuery('<i>')
                        .addClass('material-icons')
                        .html(bookmark.icon)
                        .attr('title', bookmark.title)
                        .appendTo($bookmark);
                } else if (bookmark.type === 'userdefined') {
                    $bookmark.appendTo($bookmarktitles);
                    jQuery('<i>')
                        .addClass('material-icons')
                        .html('star')
                        .appendTo($bookmark);
                    jQuery('<div>')
                        .addClass('bookmark-title')
                        .html(bookmark.title)
                        .appendTo($bookmark);
                    $bookmark.attr('title', bookmark.title);
                }
            }

            if (userControls.links) {
                for (let link of userControls.links) {
                    var $link = jQuery('<div>')
                        .addClass('link')
                        .html(link.title)
                        .data('link', link)
                        .appendTo($menutray)
                        .on('click', (e) => {
                            e.stopPropagation();
                            $usermenu.blur();
                            if (jQuery(e.currentTarget).data('link').navigation !== '') {
                                program.getModule(jQuery(e.currentTarget).data('link').navigation);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                            }
                        });
                }
            }

            var $staticinfo = jQuery('<div>')
                .addClass('staticinfo')
                .appendTo($menutray);
            var $copyright = jQuery('<div>')
                .addClass('copyright')
                .html(`&copy; ${new Date().getFullYear().toString()} Database Works, Inc.`)
                .appendTo($staticinfo);
            var $version = jQuery('<div>')
                .addClass('version')
                .html(`${applicationConfig.version}`)
                .appendTo($staticinfo);
        }
    }
    //---------------------------------------------------------------------------------
    private _renderMenu($appmaster: JQuery, options: MenuOptions): JQuery {
        var me = this;
        let $appmenu = jQuery('<div>')
            .attr('id', 'fw-app-menu')
            .attr('tabindex', '-1')
            .attr('pinned', '')
            .data('options', options)
            .on('focusout', (e) => {
                setTimeout(function() {
                    if (document.hasFocus() && !jQuery.contains($appmenu[0], e.currentTarget) && e) {
                        me._closeMainMenu($appmaster);
                    }
                }, 0);
            });

        $appmaster.data('menu', $appmenu);

        var $menutray = jQuery('<div>')
            .addClass('app-menu-tray tray')
            .appendTo($appmenu);

        for (let menuobject of options.menu) {
            if ((menuobject as MenuCategory).modules) {
                var $category = jQuery('<div>')
                    .addClass('menu-lv1object')
                    .appendTo($menutray)
                    .on('click', (e) => {
                        if (jQuery(e.currentTarget).hasClass('selected')) {
                            jQuery(e.currentTarget).removeClass('selected');
                        } else if (!$appmenu.hasClass('active')) {
                            jQuery(e.currentTarget).siblings().removeClass('selected');
                            jQuery(e.currentTarget).addClass('selected');
                        }
                    })
                    .on('mouseenter', (e) => {
                        if ($appmenu.hasClass('active')) {
                            jQuery(e.currentTarget).siblings().removeClass('hovered');
                            jQuery(e.currentTarget).addClass('hovered');
                        }
                    });

                var $categoryicon = jQuery('<i>')
                    .addClass('material-icons')
                    .html(menuobject.icon)
                    .attr('title', menuobject.title)
                    .appendTo($category);

                var $collapseabletray = jQuery('<div>')
                    .addClass('lv1object-tray')
                    .appendTo($category);

                var $categorytitle = jQuery('<div>')
                    .addClass('menu-lv1object-title')
                    .html(menuobject.title)
                    .appendTo($collapseabletray);

                var $categoryarrow = jQuery('<i>')
                    .addClass('material-icons')
                    .html('chevron_right')
                    .appendTo($collapseabletray);

                var $moduletray = jQuery('<div>')
                    .addClass('module-tray')
                    .appendTo($category);

                for (let module of (menuobject as MenuCategory).modules) {
                    var $module = jQuery('<div>')
                        .addClass('module')
                        .html(module.title)
                        .data('module', module)
                        .appendTo($moduletray)
                        .on('click', (e) => {
                            e.stopPropagation();
                            $appmenu.blur();
                            if (jQuery(e.currentTarget).data('module').navigation !== '') {
                                program.navigate(jQuery(e.currentTarget).data('module').navigation);
                            } else {
                                FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                            }
                        });
                }
            } else if ((menuobject as MenuModule).navigation) {
                var $lv1module = jQuery('<div>')
                    .addClass('menu-lv1object')
                    .data('module', menuobject)
                    .appendTo($menutray)
                    .on('click', (e) => {
                        e.stopPropagation();
                        $appmenu.blur();
                        if (jQuery(e.currentTarget).data('module').navigation !== '') {
                            program.navigate(jQuery(e.currentTarget).data('module').navigation);
                        } else {
                            FwNotification.renderNotification('ERROR', 'Module navigation not set up.');
                        }
                    })
                    .on('mouseenter', (e) => {
                        if ($appmenu.hasClass('active')) {
                            jQuery(e.currentTarget).siblings().removeClass('hovered');
                            jQuery(e.currentTarget).addClass('hovered');
                        }
                    });

                var $lv1moduleicon = jQuery('<i>')
                    .addClass('material-icons')
                    .html(menuobject.icon)
                    .attr('title', menuobject.title)
                    .appendTo($lv1module);

                var $collapseabletray = jQuery('<div>')
                    .addClass('lv1object-tray')
                    .appendTo($lv1module);

                var $lv1moduletitle = jQuery('<div>')
                    .addClass('menu-lv1object-title')
                    .html(menuobject.title)
                    .appendTo($collapseabletray);
            }
        }

        var $auxtray = jQuery('<div>')
            .addClass('auxiliary-tray tray')
            .appendTo($appmenu);

        var $togglebuttons = jQuery('<div>')
            .addClass('toggle-buttons')
            .appendTo($auxtray);

        var $pintoggle = jQuery('<div>')
            .addClass('toggle-button pin-unpin')
            .attr('title', 'Pin/Unpin Menu')
            .appendTo($togglebuttons)
            .on('mouseenter', (e) => {
                $menutray.find('.menu-lv1object').removeClass('hovered');
            })
            .on('click', (e) => {
                if ($appmenu[0].hasAttribute('pinned')) {
                    $appmenu.removeAttr('pinned');
                } else {
                    $appmenu.attr('pinned', '');
                }
            });

        var $pintoggleicon = jQuery('<i>')
            .addClass('material-icons pin-unpin-icon')
            .html('push_pin')
            .appendTo($pintoggle);

        return $appmenu;
    }
    //---------------------------------------------------------------------------------
    private _getAppTitle(): JQuery {
        const isTraining = sessionStorage.getItem('istraining');
        let trainingEl = '';
        if (isTraining !== null && isTraining === 'true') {
            trainingEl = `<span style="background-color:#4caf50;padding:2px 4px 2px 4px;margin-left:5px;">Training</span>`;
        }

        var $title = jQuery('<span>')
            .addClass('bgothm')
            .html(`${program.title}${trainingEl}`)

        $title.on('click', (e) => {
            try {
                if (sessionStorage.getItem('homePage') !== null) {
                    const homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;
                    if (homePagePath !== null && homePagePath !== '') {
                        program.getModule(homePagePath);
                    } else {
                        program.getModule('module/dashboard');
                    }
                } else {
                    program.getModule('module/dashboard');
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        return $title;
    }
    //---------------------------------------------------------------------------------
    private _openMenuTray($menu: JQuery, $appmenu: JQuery) {
        $appmenu.addClass('active');
        $appmenu.find('.main').css('display', 'none');
        $appmenu.find('.close').css('display', '');
    }
    //---------------------------------------------------------------------------------
    private _closeMenuTray($menu: JQuery, $appmenu: JQuery) {
        $appmenu.removeClass('active');
        $appmenu.find('.main').css('display', '');
        $appmenu.find('.close').css('display', 'none');
    }
    //----------------------------------------------------------------------------------------------
    private _openMainMenu($appmaster: JQuery) {
        var $header = $appmaster.data('header');
        var $menu   = $appmaster.data('menu');

        $header.find('.app-menu-button').addClass('active');
        $header.find('.app-menu-button .main').css('display', 'none');
        $header.find('.app-menu-button .close').css('display', '');
        $menu.addClass('active').focus();
    }
    //----------------------------------------------------------------------------------------------
    private _closeMainMenu($appmaster: JQuery) {
        var $header = $appmaster.data('header');
        var $menu   = $appmaster.data('menu');

        $header.find('.app-menu-button').removeClass('active');
        $header.find('.app-menu-button .main').css('display', '');
        $header.find('.app-menu-button .close').css('display', 'none');
        $menu.removeClass('active');

        $menu.find('.menu-lv1object').removeClass('hovered selected');
    }
    //----------------------------------------------------------------------------------------------
    buildMainMenu(): (MenuCategory | MenuModule)[] {
        return null;
    }
    //----------------------------------------------------------------------------------------------
    getUserControls(): MenuUserControls {
        return null;
    }
    //----------------------------------------------------------------------------------------------
    events($appmaster: JQuery) { }
    //----------------------------------------------------------------------------------------------
}

interface HeaderOptions {
    title?: string | JQuery;
    userControls?: boolean | MenuUserControls;
}

interface MenuUserControls {
    links?: UserControlLink[];
    bookmarks?: UserControlBookmark[];
    controls?: UserControlControl[];
}

interface UserControlLink {
    title: string;
    navigation: string;
}

interface UserControlBookmark {
    title?: string;
    icon?: string;
    navigation: string;
    securityid?: string;
    type: 'system'|'userdefined';
}

interface UserControlControl {
    control: JQuery;
}

interface MenuOptions {
    menu: (MenuCategory | MenuModule)[];
}

interface MenuModule {
    title: string;
    icon: string;
    navigation: string;
    securityid: string;
}

interface MenuCategory {
    title: string;
    icon: string;
    modules?: MenuCategoryModule[];
}

interface MenuCategoryModule {
    title: string;
    navigation: string;
    securityid: string;
}