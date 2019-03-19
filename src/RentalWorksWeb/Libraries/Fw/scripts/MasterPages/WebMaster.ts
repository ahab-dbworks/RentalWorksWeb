class WebMaster {
    //---------------------------------------------------------------------------------
    getMasterView() {
        const $view = jQuery(`<div id="master" class="fwpage">
                                <div id="master-header"></div>
                                <div id="master-body"></div>
                                <div id="master-footer"></div>
                              </div>`);

        const applicationTheme = sessionStorage.getItem('applicationtheme');
        const masterHeader = $view.find('#master-header');
        masterHeader.append((applicationTheme === 'theme-classic') ? this.getHeaderClassic() : this.getHeaderView());


        program.setApplicationTheme(applicationTheme);

        // color nav header for non-default user location on app refresh. Event listener in RwMaster.buildOfficeLocation()
        const userLocation = JSON.parse(sessionStorage.getItem('location'));
        const defaultLocation = JSON.parse(sessionStorage.getItem('defaultlocation'));
        if (userLocation.location !== defaultLocation.location) {
            const nonDefaultStyles = { borderTop: `.3em solid ${userLocation.locationcolor}`, borderBottom: `.3em solid ${userLocation.locationcolor}` };
            masterHeader.find('div[data-control="FwFileMenu"]').css(nonDefaultStyles);
        } else {
            const defaultStyles = { borderTop: `transparent`, borderBottom: `1px solid #9E9E9E` };
            masterHeader.find('div[data-control="FwFileMenu"]').css(defaultStyles);
        }

        return $view;
    }
    //---------------------------------------------------------------------------------
    getHeaderClassic() {
        var $view = jQuery(`<div id="header">
                              <div id="headerRibbon" class="fwcontrol fwribbon" data-control="FwRibbon" data-version="1" data-rendermode="template">
                                <div class="dashboard"><img src="theme/images/icons/home.png" alt="Home" style="width:16px;height:16px;" /></div>
                                <div class="tabs"></div>
                                <div class="usercontrol"></div>
                                <div class="tabpages"></div>
                              </div>
                            </div>`);
    
        var $headerRibbon = $view.find('#headerRibbon');
        var $userControl  = $headerRibbon.find('.usercontrol');
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

        var $fwcontrols = $view.find('.fwcontrol');
        FwControl.renderRuntimeControls($fwcontrols);

        $view
            .on('click', '.dashboard', function() {
                try {
                    program.getModule('module/dashboard');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

        return $view;
    }
    //----------------------------------------------------------------------------------------------
    getUserControlClassic($userControl: JQuery) {
        const $user = jQuery(`<div id="username" class="item">${sessionStorage.getItem('userType')}: ${sessionStorage.getItem('fullname')}</div>`);
        $userControl.append($user);

        //var $notification = FwNotification.generateNotificationArea();
        //$userControl.append($notification);

        const $usersettings = jQuery(`<div id="usersettings" class="item"><div class="usersettingsicon"></div></div>`);
        $usersettings.on('click', function () {
            try {
                program.getModule('module/usersettings');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $userControl.append($usersettings);

        const $logoff = jQuery(`<div id="logoff" class="item">Logoff</div>`);
        $logoff.on('click', function () {
            try {
                program.getModule('logoff');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $userControl.append($logoff);
    }
    //----------------------------------------------------------------------------------------------
    getHeaderView() {
        const $view = jQuery(`<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>`);

        FwControl.renderRuntimeControls($view);
        $view.find('.logo').append(`<div class="bgothm">${program.name}</div>`);

        this.buildMainMenu($view);
        this.getUserControl($view);
        $view
            .on('click', '.bgothm', () => {
                try {
                    const homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;

                    if (homePagePath !== null && homePagePath !== '') {
                        program.getModule(homePagePath);
                    } else {
                        program.getModule('module/dashboard');
                    }
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            });

        return $view;
    }
    //----------------------------------------------------------------------------------------------
    buildMainMenu($context: JQuery) {

    }
    //----------------------------------------------------------------------------------------------
    getUserControl($context: JQuery) {

    }
    //----------------------------------------------------------------------------------------------
}