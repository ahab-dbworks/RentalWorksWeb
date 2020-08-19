class RwHome {
    Module: string = 'RwHome';
    //----------------------------------------------------------------------------------------------
    getHomeScreen() {
        //var applicationOptions = program.getApplicationOptions();
        const screen: any = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());

        jQuery('title').html(Constants.appCaption);

        screen.load = async () => {
            if (typeof window.firstLoadCompleted === 'undefined') {
                // Get ActiveViewFields
                const responseGetActiveViewFields = await FwAjax.callWebApi<BrowseRequest, any>({
                    httpMethod: 'POST',
                    url: `${applicationConfig.apiurl}api/v1/browseactiveviewfields/browse`,
                    $elementToBlock: jQuery('body'),
                    data: {
                        uniqueids: {
                            WebUserId: sessionStorage.getItem('webusersid')
                        }
                    }
                });
                const moduleNameIndex = responseGetActiveViewFields.ColumnIndex.ModuleName;
                const activeViewFieldsIndex = responseGetActiveViewFields.ColumnIndex.ActiveViewFields;
                const idIndex = responseGetActiveViewFields.ColumnIndex.Id;
                for (let i = 0; i < responseGetActiveViewFields.Rows.length; i++) {
                    let controller = `${responseGetActiveViewFields.Rows[i][moduleNameIndex]}Controller`;
                    if (typeof window[controller] !== 'undefined') {
                        window[controller].ActiveViewFields = JSON.parse(responseGetActiveViewFields.Rows[i][activeViewFieldsIndex]);
                        window[controller].ActiveViewFieldsId = responseGetActiveViewFields.Rows[i][idIndex];
                    }
                }

                window.firstLoadCompleted = true;

                this.addSystemUpdateNotification(jQuery('#fw-app-header'));
                this.addDuplicateCustomFormAlerts(jQuery('#fw-app-header'));
            }

            const redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(() => {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            }
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    addDuplicateCustomFormAlerts($control) {
        if (sessionStorage['duplicateforms']) {
            const $alertContainer = jQuery('<div class="alert-container"></div>')
            jQuery($control).append($alertContainer);
            const duplicateForms = JSON.parse(sessionStorage.getItem('duplicateforms'));
            for (let i = 0; i < duplicateForms.length; i++) {
                const customForm = duplicateForms[i];
                $alertContainer.append(`<div class="duplicate-form-alert">
                                            <span>Duplicate Custom Forms defined for the ${customForm['BaseForm']}: ${customForm['Desc1']} and ${customForm['Desc2']}</span>        
                                            <i class="material-icons">clear</i>
                                        </div>`);
            }

            $alertContainer.on('click', '.duplicate-form-alert i', e => {
                const $this = jQuery(e.currentTarget);
                const $alert = $this.parents('.duplicate-form-alert');
                $alert.remove();
            });

            sessionStorage.removeItem('duplicateforms');
        }
    }
    //----------------------------------------------------------------------------------------------
    addSystemUpdateNotification($control) {
        const isWebAdmin = JSON.parse(sessionStorage.getItem('userid')).webadministrator;
        if (isWebAdmin === 'true') {
            FwAjax.callWebApi<any, any>({
                httpMethod: 'POST',
                url: `${applicationConfig.apiurl}api/v1/systemupdate/availableversions`,
                data: {
                    CurrentVersion: sessionStorage.getItem('serverVersion'),
                    OnlyIncludeNewerVersions: true
                }
            }).then((response) => {
                if (response.Versions.length) {
                    const $sysUpdateContainer = jQuery(`<div class="system-update-container">
                                                            <div class="system-update-notification">
                                                                <span>Version ${response.Versions[0].Version} is now available.  Access the System Update module to install.</span>        
                                                                <i class="material-icons">clear</i>
                                                            </div>
                                                        </div>`)
                    jQuery($control).append($sysUpdateContainer);

                    $sysUpdateContainer.on('click', '.system-update-notification i', e => {
                        e.stopPropagation();
                        $sysUpdateContainer.remove();
                    });

                    $sysUpdateContainer.on('click', e => {
                        program.navigate('module/update');
                        $sysUpdateContainer.remove();
                    });
                }
            });
        }
    }
    //----------------------------------------------------------------------------------------------

};

var RwHomeController = new RwHome();