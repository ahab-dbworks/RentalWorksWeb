routes.push({
    pattern: /^reports\/outcontractreport/, action: function (match) {
        return RwOutContractReportController.getModuleScreen();
    }
});
let templateOutContractReportFrontEnd = `
    <div class="fwcontrol fwcontainer fwform fwreport outcontractreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Charge Processing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwChargeProcessingController">
        <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
                <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
            </div>
            <div class="tabpages">
                <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
                    <div class="formpage">
                        <div class="formcolumn">
                            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract">
                                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield contractid" data-caption="Contract" data-datafield="contractid" data-validationname="ContractValidation"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
`;
class RwOutContractReportClass extends FwWebApiReport {
    constructor() {
        super('OutContractReport', 'api/v1/outcontractreport', templateOutContractReportFrontEnd);
    }
    getModuleScreen() {
        let screen = {};
        screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        let $form = this.openForm();
        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
            $form.find('.contractid').data('calldatabind', function (request, callback) {
            });
        };
        screen.unload = function () {
        };
        return screen;
    }
    openForm() {
        let $form = this.getFrontEnd();
        return $form;
    }
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        this.loadLists($form);
    }
    loadLists($form) {
    }
}
var RwOutContractReportController = new RwOutContractReportClass();
//# sourceMappingURL=RwOutContractReportController.js.map