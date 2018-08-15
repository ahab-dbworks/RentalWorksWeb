routes.push({ pattern: /^module\/crewsigninreport$/, action: function (match: RegExpExecArray) { return RwCrewSignInReportController.getModuleScreen(); } });

class RwCrewSignInReport {
    Module: string = 'CrewSignInReport';
    ModuleOptions: any = {
        ReportOptions: {
            HasDownloadExcel: true
        }
    };

    constructor() {
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $form;
        screen            = {};
        screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm() {
        var $form;
    
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function(request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };

        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid,  department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const customerValue = jQuery($grid.find('[data-validationname="CustomerValidation"] input')).val();
        const officeValue = jQuery($grid.find('[data-validationname="OfficeLocationValidation"] input')).val();
        const departmentValue = jQuery($grid.find('[data-validationname="DepartmentValidation"] input')).val();
        const dealValue = jQuery($grid.find('[data-validationname="DealValidation"] input')).val();
      
        switch (validationName) {
            case 'DealValidation':
                if (customerValue != '') {
                    request.uniqueids = {
                        CustomerId: customerValue,
                    };
                }
                break;
            case 'OrderValidation':
                if (officeValue !== "") {
                    request.uniqueids = {
                        OfficeLocationId: officeValue,
                    };
                }
                if (departmentValue !== "") {
                    request.uniqueids.DepartmentId = departmentValue;
                }
                if (customerValue !== "") {
                    request.uniqueids.CustomerId = customerValue;
                }
                if (dealValue !== "") {
                    request.uniqueid.DealIds = dealValue;
                }
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
};
var RwCrewSignInReportController: any = new RwCrewSignInReport();

