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
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const departmentId = FwFormField.getValueByDataField($form, 'DepartmentId');
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        request.uniqueids = {};
      
        switch (validationName) {
            case 'DealValidation':
                if (customerId != '') {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
            case 'OrderValidation':
                if (officeLocationId !== "") {
                    request.uniqueids.OfficeLocationId = officeLocationId;
                }
                if (departmentId !== "") {
                    request.uniqueids.DepartmentId = departmentId;
                }
                if (customerId !== "") {
                    request.uniqueids.CustomerId = customerId;
                }
                if (dealId !== "") {
                    request.uniqueids.DealId = dealId;
                }
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
};
var RwCrewSignInReportController: any = new RwCrewSignInReport();

