routes.push({ pattern: /^module\/project$/, action: function (match: RegExpExecArray) { return ProjectController.getModuleScreen(); } });
routes.push({ pattern: /^module\/project\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return ProjectController.getModuleScreen(filter); } });

class Project {
    Module: string = 'Project';
    apiurl: string = 'api/v1/project';
    caption: string = Constants.Modules.Agent.children.Project.caption;
    nav: string = Constants.Modules.Agent.children.Project.nav;
    id: string = Constants.Modules.Agent.children.Project.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //-----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);

        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //-----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Quote', 'X5qRQcu9TBn8Z', (e: JQuery.ClickEvent) => {
            try {
                this.createQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //-----------------------------------------------------------------------------------------------
    getModuleScreen = (filter?: { datafield: string, search: string }) => {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        let $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            // Dashboard search
            if (typeof filter !== 'undefined') {
                let datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                let parsedSearch = filter.search.replace(/%20/g, " ");
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(parsedSearch);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //-----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        return $browse;
    }
    //-----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('[data-datafield="Status"]'), [
            { value: 'NEW', text: 'NEW' },
            { value: 'ACTIVE', text: 'ACTIVE' },
            { value: 'CLOSED', text: 'CLOSED' }
        ], true);

        if (mode === 'NEW') {
            const office = JSON.parse(sessionStorage.getItem('location')),
                warehouse = JSON.parse(sessionStorage.getItem('warehouse')),
                today = FwFunc.getDate(),
                usersid = sessionStorage.getItem('usersid'),
                name = sessionStorage.getItem('name'),
                department = JSON.parse(sessionStorage.getItem('department'));

            FwFormField.setValue($form, 'div[data-datafield="ProjectManagerId"]', usersid, name);
            FwFormField.setValue($form, 'div[data-datafield="AgentId"]', usersid, name);
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStartDate', today);
            FwFormField.setValueByDataField($form, 'EstimatedStopDate', today);
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
            FwFormField.setValueByDataField($form, 'Status', 'NEW');
            FwFormField.setValueByDataField($form, 'StatusDate', today);
            FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

            $form.find('.activityCheckboxes div > input').prop('checked', true);
        }

        $form.find('div[data-datafield="PickTime"]').attr('data-required', 'false');
        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', 'false');
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', 'false');

        return $form;
    }
    //-----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ProjectId"] input').val(uniqueids.ProjectId);

        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'ProjectDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${uniqueids.ProjectId}/document`;
            },
            gridSecurityId: 'xTTNkaom7t5q',
            moduleSecurityId: this.id,
            parentFormDataFields: 'ProjectId',
            uniqueid1Name: 'ProjectId',
            getUniqueid1Value: () => uniqueids.ProjectId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //-----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //-----------------------------------------------------------------------------------------------
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        switch (datafield) {
            case 'DealId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'AgentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateagent`);
                break;
            case 'ProjectManagerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprojectmanager`);
                break;
            case 'OutsideSalesRepresentativeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesalesrepresentative`);
                break;
        }
    }
    //--------------------------------------------------------------------------------------------
    renderGrids($form) {
        //Project Contact Grid
        //const $projectContactGrid = $form.find('div[data-grid="ProjectContactGrid"]');
        //const $projectContactGridControl = FwBrowse.loadGridFromTemplate('ProjectContactGrid');
        //$projectContactGrid.empty().append($projectContactGridControl);
        //$projectContactGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
        //    };
        //});
        //$projectContactGridControl.data('beforesave', request => {
        //    request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        //});
        //FwBrowse.init($projectContactGridControl);
        //FwBrowse.renderRuntimeHtml($projectContactGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'ProjectContactGrid',
            gridSecurityId: 'ZvjyLW5OM5s1X',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
                };
            },
            beforeSave: (request: any) => {
                request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
        //-----
        //PO Approver Grid
        //const $poApproverGrid = $form.find('div[data-grid="POApproverGrid"]');
        //const $poApproverGridControl = FwBrowse.loadGridFromTemplate('POApproverGrid');
        //$poApproverGrid.empty().append($poApproverGridControl);
        //$poApproverGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
        //    };
        //});
        //$poApproverGridControl.data('beforesave', request => {
        //    request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
        //});
        //FwBrowse.init($poApproverGridControl);
        //FwBrowse.renderRuntimeHtml($poApproverGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'POApproverGrid',
            gridSecurityId: 'kaGlUrLG9GjN',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
                };
            },
            beforeSave: (request: any) => {
                request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
            }
        });
        //-----
        //Project Note Grid
        //const $projectNoteGrid = $form.find('div[data-grid="ProjectNoteGrid"]');
        //const $projectNoteGridControl = FwBrowse.loadGridFromTemplate('ProjectNoteGrid');
        //$projectNoteGrid.empty().append($projectNoteGridControl);
        //$projectNoteGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
        //    };
        //});
        //$projectNoteGridControl.data('beforesave', request => {
        //    request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
        //});
        //FwBrowse.init($projectNoteGridControl);
        //FwBrowse.renderRuntimeHtml($projectNoteGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'ProjectNoteGrid',
            gridSecurityId: 'tR09bf745p0YU',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
                };
            },
            beforeSave: (request: any) => {
                request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
            }
        });
    }
    //-----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //const $projectContactGrid = $form.find('[data-name="ProjectContactGrid"]');
        //FwBrowse.search($projectContactGrid);

        //const $poApproverGrid = $form.find('[data-name="POApproverGrid"]');
        //FwBrowse.search($poApproverGrid);

        //const $projectNoteGrid = $form.find('[data-name="ProjectNoteGrid"]');
        //FwBrowse.search($projectNoteGrid);

        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;
            if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        try {
                            const $gridcontrol = jQuery($gridControls[i]);
                            FwBrowse.search($gridcontrol);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }

                const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });
    }
    //-----------------------------------------------------------------------------------------------
    createQuote($form: any) {
        const projectId = FwFormField.getValueByDataField($form, 'ProjectId');

        if (projectId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            let $confirmation = FwConfirmation.renderConfirmation('Create Quote', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            var html = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Create a Quote?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            let $yes = FwConfirmation.addButton($confirmation, 'Create Quote', false);
            let $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', createQuote);
            var $confirmationbox = jQuery('.fwconfirmationbox');
            function createQuote() {
                FwAppData.apiMethod(true, 'POST', "api/v1/project/createquote/" + projectId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Created.');
                    FwConfirmation.destroyConfirmation($confirmation);
                    let uniqueids: any = {};
                    uniqueids.QuoteId = response.QuoteId;
                    var $quoteform = QuoteController.loadForm(uniqueids);
                    FwModule.openModuleTab($quoteform, "", true, 'FORM', true);

                    FwModule.refreshForm($form);
                }, null, $confirmationbox);
            }
        }
    }
    //-----------------------------------------------------------------------------------------------
}

var ProjectController = new Project();