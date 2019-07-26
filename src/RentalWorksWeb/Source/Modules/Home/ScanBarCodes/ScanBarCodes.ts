
class ScanBarCodes {
    Module: string = 'ScanBarCodes';
    //caption: string = Constants.Modules.Home.AssignBarCodes.caption;
    //nav: string = Constants.Modules.Home.AssignBarCodes.nav;
    //id: string = Constants.Modules.Home.AssignBarCodes.id;
    caption: 'Scan Bar Codes';
    nav: 'module/scanbarcodes';
    id: 'C8683D4F-70C1-40CD-967A-0891B14664E8';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Scan Bar Codes', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.events($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PhysicalInventoryId', parentmoduleinfo.PhysicalInventoryId);
            FwFormField.setValueByDataField($form, 'PhysicalInventoryNumber', parentmoduleinfo.PhysicalInventoryNumber);
            FwFormField.setValueByDataField($form, 'ScheduleDate', parentmoduleinfo.ScheduleDate);
            FwFormField.setValueByDataField($form, 'Description', parentmoduleinfo.Description);
            FwFormField.setValueByDataField($form, 'OfficeLocationId', parentmoduleinfo.OfficeLocationId, parentmoduleinfo.OfficeLocation);
            FwFormField.setValueByDataField($form, 'WarehouseId', parentmoduleinfo.WarehouseId, parentmoduleinfo.Warehouse);
        }
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        //BarCode input
        $form.find('[data-datafield="BarCode"] input').on('keydown', e => {
            if (e.which === 13) {
                $form.find('.error-msg').html('');
                const physicalInventoryId = FwFormField.getValueByDataField($form, 'PhysicalInventoryId');
                const barCode = FwFormField.getValueByDataField($form, 'BarCode');
                const request: any = {};
                request.PhysicalInventoryId = physicalInventoryId;
                request.BarCode = barCode;
                FwAppData.apiMethod(true, 'POST', 'api/v1/physicalinventory/countbarcode', request, FwServices.defaultTimeout,
                    response => {
                        if (response.success) {
                            $form.find('[data-datafield="BarCode"] input').select();
                            FwFormField.setValueByDataField($form, 'ICode', response.ICode);
                            FwFormField.setValueByDataField($form, 'InventoryDescription', response.Description);
                        } else {
                            $form.find('.error-msg').html(`<div><span>${response.msg}</span></div>`);
                            $form.find('[data-datafield="BarCode"] input').select();
                            FwFormField.setValueByDataField($form, 'ICode', '');
                            FwFormField.setValueByDataField($form, 'InventoryDescription', '');
                        }
                    },
                    ex => FwFunc.showError(ex)
                    , $form);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="scanbarcodesform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-caption="Scan Bar Codes" data-hasaudit="false" data-controller="ScanBarCodesController">
  <div class="flexpage">
      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Physical Inventory">
        <div class="flexrow">
          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Physical Inventory" data-datafield="PhysicalInventoryId" style="display:none;"></div>
          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Physical Inventory No." data-datafield="PhysicalInventoryNumber" data-enabled="false" style="flex:1"></div>
          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="ScheduleDate" data-enabled="false" style="flex:1"></div>
          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:3"></div>
          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-enabled="false" style="float:left;width:250px;"></div>
          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield warehouse" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-enabled="false" style="float:left;width:250px;"></div>
        </div>
        </div>
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Scan Bar Codes to count Inventory" style="flex:1 1 750px;">
          <div class="flexrow">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code" data-datafield="BarCode" style="flex:1 1 300px;"></div>
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="flex:1 1 300px;" data-enabled="false"></div>
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="InventoryDescription" style="flex:1 1 400px;" data-enabled="false"></div>
          </div>
          <div class="flexrow"><div class="error-msg" style="margin-top:8px;"></div></div>
        </div>
      </div>
</div>
`;
    }
}
var ScanBarCodesController = new ScanBarCodes();