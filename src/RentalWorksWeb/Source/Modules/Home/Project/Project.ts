routes.push({ pattern: /^module\/project$/, action: function (match: RegExpExecArray) { return ProjectController.getModuleScreen(); } });
routes.push({ pattern: /^module\/project\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return ProjectController.getModuleScreen(filter); } });

class Project {
    Module: string = 'Project';
    apiurl: string = 'api/v1/project';
    caption: string = 'Project';
    ActiveView: string = 'ALL';

    getModuleScreen = (filter?: { datafield: string, search: string }) => {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    addBrowseMenuItems($menuObject) {
        var self = this;
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        var $locationView;
        $locationView = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };


    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('[data-datafield="Status"]'), [
            { value: 'NEW', text: 'NEW' },
            { value: 'ACTIVE', text: 'ACTIVE' },
            { value: 'CLOSED', text: 'CLOSED' }
        ], true);

        if (mode === 'NEW') {
            const office = JSON.parse(sessionStorage.getItem('location')),
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

    afterLoad($form: any) {

    }
}

var ProjectController = new Project();