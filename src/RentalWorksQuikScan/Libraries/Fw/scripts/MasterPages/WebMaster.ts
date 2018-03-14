class WebMaster {
    //---------------------------------------------------------------------------------
    getMasterView() {
        var $view, $headerView, applicationtheme, html = [];
        applicationtheme = sessionStorage.getItem('applicationtheme');

        html.push('<div id="master" class="fwpage">');
        html.push('  <div id="master-header"></div>');
        html.push('  <div id="master-body"></div>');
        html.push('  <div id="master-footer"></div>');
        html.push('</div>');
        $view = jQuery(html.join(''));

        if (applicationtheme === 'theme-classic') {
            $headerView = this.getHeaderClassic();
        } else {
            $headerView = this.getHeaderView();
        }
        $view.find('#master-header').append($headerView);

        jQuery('html').addClass(applicationtheme);

        return $view;
    };
    //---------------------------------------------------------------------------------
    getHeaderClassic() {
        var $view, $headerRibbon, $userControl, $fwcontrols, html = [];

        html.push('<div id="header">');
        html.push('  <div id="headerRibbon" class="fwcontrol fwribbon" data-control="FwRibbon" data-version="1" data-rendermode="template">');
        html.push('    <div class="dashboard">');
        html.push('      <img src="theme/images/icons/home.png" alt="Home" style="width:16px;height:16px;" />');
        html.push('    </div>');
        html.push('    <div class="tabs"></div>');
        html.push('    <div class="usercontrol"></div>');
        html.push('    <div class="tabpages"></div>');
        html.push('  </div>');
        html.push('</div>');

        $view = jQuery(html.join(''));
    
        $headerRibbon       = $view.find('#headerRibbon');
        $userControl        = $headerRibbon.find('.usercontrol');
        this.getUserControlClassic($userControl);
    
        var nodeApplications, nodeApplication=null, baseiconurl, $tabpage, ribbonItem, dropDownMenuItems, caption;
        nodeApplications = FwApplicationTree.getMyTree();
        for (var appno = 0; appno < nodeApplications.children.length; appno++) {
            if (nodeApplications.children[appno].id === FwApplicationTree.currentApplicationId) {
                nodeApplication = nodeApplications.children[appno];
            }
        }
        if (nodeApplication === null) {
            sessionStorage.clear();
            window.location.reload(true);
        }
        baseiconurl = 'theme/images/icons/home/';
        for (var lv1childno = 0; lv1childno < nodeApplication.children.length; lv1childno++) {
            var nodeLv1MenuItem = nodeApplication.children[lv1childno];
            if (nodeLv1MenuItem.properties.visible === 'T') {
                switch(nodeLv1MenuItem.properties.nodetype) {
                    case 'Lv1ModuleMenu':
                        $tabpage = FwRibbon.addTab($headerRibbon, nodeLv1MenuItem.properties.caption)
                        for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                            var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                            if (nodeLv2MenuItem.properties.visible === 'T') {
                                switch(nodeLv2MenuItem.properties.nodetype) {
                                    case 'Lv2ModuleMenu':
                                        dropDownMenuItems = [];
                                        for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                            var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                            if (nodeLv3MenuItem.properties.visible === 'T') {
                                                dropDownMenuItems.push({id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl});
                                            }
                                        }
                                        caption = (typeof nodeLv2MenuItem.properties.htmlcaption === 'string') ? nodeLv2MenuItem.properties.htmlcaption : nodeLv2MenuItem.properties.caption;
                                        ribbonItem = FwRibbon.generateDropDownModuleBtn(nodeLv2MenuItem.id, caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                        $tabpage.append(ribbonItem);
                                        break;
                                    case 'Module':
                                        caption = (typeof nodeLv2MenuItem.properties.htmlcaption === 'string') ? nodeLv2MenuItem.properties.htmlcaption : nodeLv2MenuItem.properties.caption;
                                        ribbonItem = FwRibbon.generateStandardModuleBtn(nodeLv2MenuItem.id, caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                        $tabpage.append(ribbonItem);
                                        break;
                                    case 'Report':
                                        caption = (typeof nodeLv2MenuItem.properties.htmlcaption === 'string') ? nodeLv2MenuItem.properties.htmlcaption : nodeLv2MenuItem.properties.caption;
                                        ribbonItem = FwRibbon.generateStandardModuleBtn(nodeLv2MenuItem.id, caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                        $tabpage.append(ribbonItem);
                                        break;
                                }
                            }
                        }
                        break;
                }
            }
        }

        $fwcontrols = $view.find('.fwcontrol');
        FwControl.init($fwcontrols);
        FwControl.renderRuntimeHtml($fwcontrols);

        $view
            .on('click', '.dashboard', function() {
                try {
                    program.navigate('home');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
        ;

        return $view;
    };
    //----------------------------------------------------------------------------------------------
    getUserControlClassic($userControl: JQuery) {
        var $user, $logoff, $notification, $usersettings;

        $user = jQuery('<div id="username" class="item">' + sessionStorage.getItem('userType') + ': ' + sessionStorage.getItem('fullname') + '</div>');
        $userControl.append($user);

        //$notification = FwNotification.generateNotificationArea();
        //$userControl.append($notification);

        $usersettings = jQuery('<div id="usersettings" class="item"><div class="usersettingsicon"></div></div>');
        $usersettings.on('click', function() {
            try {
                program.getModule('module/usersettings');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $userControl.append($usersettings);

        $logoff = jQuery('<div id="logoff" class="item">Logoff</div>');
        $logoff.on('click', function() { 
            try {
                program.navigate('logoff');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $userControl.append($logoff);
    };
    //----------------------------------------------------------------------------------------------
    getHeaderView() {
        var $view;

        $view = jQuery('<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>');

        FwControl.renderRuntimeControls($view);

        $view.find('.logo').append(`<div class="bgothm">${program.name}</div>`);

        var nodeSystem, nodeApplication, baseiconurl, $menu, ribbonItem, dropDownMenuItems, caption;
        nodeSystem = FwApplicationTree.getMyTree();
        for (var appno = 0; appno < nodeSystem.children.length; appno++) {
            if (nodeSystem.children[appno].id === FwApplicationTree.currentApplicationId) {
                nodeApplication = nodeSystem.children[appno];
            }
        }
        if (nodeApplication === null) {
            sessionStorage.clear();
            window.location.reload(true);
        }
        baseiconurl = 'theme/images/icons/home/';
        for (var lv1childno = 0; lv1childno < nodeApplication.children.length; lv1childno++) {
            var nodeLv1MenuItem = nodeApplication.children[lv1childno];
            if (nodeLv1MenuItem.properties.visible === 'T') {
                switch(nodeLv1MenuItem.properties.nodetype) {
                    case 'Lv1ModuleMenu':
                        $menu = FwFileMenu.addMenu($view, nodeLv1MenuItem.properties.caption)
                        for (var lv2childno = 0; lv2childno < nodeLv1MenuItem.children.length; lv2childno++) {
                            var nodeLv2MenuItem = nodeLv1MenuItem.children[lv2childno];
                            if (nodeLv2MenuItem.properties.visible === 'T') {
                                switch(nodeLv2MenuItem.properties.nodetype) {
                                    case 'Lv2ModuleMenu':
                                        dropDownMenuItems = [];
                                        for (var lv3childno = 0; lv3childno < nodeLv2MenuItem.children.length; lv3childno++) {
                                            var nodeLv3MenuItem = nodeLv2MenuItem.children[lv3childno];
                                            if (nodeLv3MenuItem.properties.visible === 'T') {
                                                dropDownMenuItems.push({id: nodeLv3MenuItem.id, caption: nodeLv3MenuItem.properties.caption, modulenav: nodeLv3MenuItem.properties.modulenav, imgurl: nodeLv3MenuItem.properties.iconurl});
                                            }
                                        }
                                        FwFileMenu.generateDropDownModuleBtn($menu, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.iconurl, dropDownMenuItems);
                                        break;
                                    case 'Module':
                                        FwFileMenu.generateStandardModuleBtn($menu, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                        break;
                                    case 'Report':
                                        FwFileMenu.generateStandardModuleBtn($menu, nodeLv2MenuItem.id, nodeLv2MenuItem.properties.caption, nodeLv2MenuItem.properties.modulenav, nodeLv2MenuItem.properties.iconurl);
                                        break;
                                }
                            }
                        }
                        break;
                }
            }
        }

        this.getUserControl($view);
        $view
            .on('click', '.bgothm', function () {
                try { program.navigate('home'); } catch (ex) { FwFunc.showError(ex); }
            })
        ;

        return $view;
    };
    //----------------------------------------------------------------------------------------------
    getUserControl($context: JQuery) {

    }
    //----------------------------------------------------------------------------------------------
}