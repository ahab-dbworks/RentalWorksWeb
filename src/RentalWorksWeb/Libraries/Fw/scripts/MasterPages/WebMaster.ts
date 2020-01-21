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

        masterHeader.append(this.getHeaderView());
        program.setApplicationTheme(applicationTheme);

        // color nav header for non-default user location on app refresh. Event listener in RwMaster.buildOfficeLocation()
        if (sessionStorage.getItem('location') !== null && sessionStorage.getItem('defaultlocation') !== null) {
            const userLocation = JSON.parse(sessionStorage.getItem('location'));
            const defaultLocation = JSON.parse(sessionStorage.getItem('defaultlocation'));
            if (userLocation.location !== defaultLocation.location) {
                const nonDefaultStyles = { borderTop: `.3em solid ${userLocation.locationcolor}`, borderBottom: `.3em solid ${userLocation.locationcolor}` };
                masterHeader.find('div[data-control="FwFileMenu"]').css(nonDefaultStyles);
            } else {
                const defaultStyles = { borderTop: `transparent`, borderBottom: `1px solid #9E9E9E` };
                masterHeader.find('div[data-control="FwFileMenu"]').css(defaultStyles);
            }
        }

        return $view;
    }
    //----------------------------------------------------------------------------------------------
    getHeaderView() {
        const $view = jQuery(`<div class="fwcontrol fwfilemenu" data-control="FwFileMenu" data-version="2" data-rendermode="template"></div>`);

        FwControl.renderRuntimeControls($view);
        const isTraining = sessionStorage.getItem('istraining');
        let trainingEl;
        if (isTraining === 'true') {
            trainingEl = `<span style="color:#0b0d0b;background-color:#07b907;border-radius:4px;padding:2px 4px 2px 4px;">TRAINING</span>`;
        } else {
            trainingEl = '';
        }

        $view.find('.logo').append(`<span class="bgothm">${program.title}${trainingEl}</span>`);

        this.buildMainMenu($view);
        this.getUserControl($view);
        $view
            .on('click', '.bgothm', () => {
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