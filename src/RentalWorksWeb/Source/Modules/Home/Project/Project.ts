routes.push({ pattern: /^module\/project$/, action: function (match: RegExpExecArray) { return ProjectController.getModuleScreen(); } });
routes.push({ pattern: /^module\/project\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return ProjectController.getModuleScreen(filter); } });

class Project {
    Module: string = 'Project';
    apiurl: string = 'api/v1/project';
    caption: string = Constants.Modules.Home.Project.caption;
	nav: string = Constants.Modules.Home.Project.nav;
	id: string = Constants.Modules.Home.Project.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;

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
    };

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        return $browse;
    }

    addBrowseMenuItems($menuObject) {
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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

        $form.find('div[data-datafield="PickTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStartTime"]').attr('data-required', false);
        $form.find('div[data-datafield="EstimatedStopTime"]').attr('data-required', false);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ProjectId"] input').val(uniqueids.ProjectId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form) {
        const $projectContactGrid = $form.find('div[data-grid="ProjectContactGrid"]');
        const $projectContactGridControl = FwBrowse.loadGridFromTemplate('ProjectContactGrid');
        $projectContactGrid.empty().append($projectContactGridControl);
        $projectContactGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
            };
        });
        $projectContactGridControl.data('beforesave', request => {
            request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($projectContactGridControl);
        FwBrowse.renderRuntimeHtml($projectContactGridControl);

        const $poApproverGrid = $form.find('div[data-grid="POApproverGrid"]');
        const $poApproverGridControl = FwBrowse.loadGridFromTemplate('POApproverGrid');
        $poApproverGrid.empty().append($poApproverGridControl);
        $poApproverGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
            };
        });
        $poApproverGridControl.data('beforesave', request => {
            request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
        });
        FwBrowse.init($poApproverGridControl);
        FwBrowse.renderRuntimeHtml($poApproverGridControl);

        const $projectNoteGrid = $form.find('div[data-grid="ProjectNoteGrid"]');
        const $projectNoteGridControl = FwBrowse.loadGridFromTemplate('ProjectNoteGrid');
        $projectNoteGrid.empty().append($projectNoteGridControl);
        $projectNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ProjectId: FwFormField.getValueByDataField($form, 'ProjectId')
            };
        });
        $projectNoteGridControl.data('beforesave', request => {
            request.ProjectId = FwFormField.getValueByDataField($form, 'ProjectId');
        });
        FwBrowse.init($projectNoteGridControl);
        FwBrowse.renderRuntimeHtml($projectNoteGridControl);
    }

    afterLoad($form: any) {
        const $projectContactGrid = $form.find('[data-name="ProjectContactGrid"]');
        FwBrowse.search($projectContactGrid);

        const $poApproverGrid = $form.find('[data-name="POApproverGrid"]');
        FwBrowse.search($poApproverGrid);

        const $projectNoteGrid = $form.find('[data-name="ProjectNoteGrid"]');
        FwBrowse.search($projectNoteGrid);
    }
}

FwApplicationTree.clickEvents[Constants.Modules.Home.Project.form.menuItems.CreateQuote.id] = function () {
    try {
        const $form = jQuery(this).closest('.fwform');
        const projectId = FwFormField.getValueByDataField($form, 'ProjectId');

        if (projectId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            var $confirmation, $yes, $no;

            $confirmation = FwConfirmation.renderConfirmation('Create Quote', '');
            $confirmation.find('.fwconfirmationbox').css('width', '450px');
            var html = [];
            html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
            html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
            html.push('    <div>Create a Quote?</div>');
            html.push('  </div>');
            html.push('</div>');

            FwConfirmation.addControls($confirmation, html.join(''));

            $yes = FwConfirmation.addButton($confirmation, 'Create Quote', false);
            $no = FwConfirmation.addButton($confirmation, 'Cancel');

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

                    FwModule.refreshForm($form, ProjectController);
                }, null, $confirmationbox);
            }
        }
    } 
    catch (ex) {
        FwFunc.showError(ex);
    }
};

var ProjectController = new Project();