class CountQuantityInventory {
    Module: string = 'CountQuantityInventory';
    caption: string = Constants.Modules.Home.CountQuantityInventory.caption;
    nav: string = Constants.Modules.Home.CountQuantityInventory.nav;
    id: string = Constants.Modules.Home.CountQuantityInventory.id;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Count Quantity Inventory', false, 'FORM', true);
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
    events($form) { }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="countquantityform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-caption="Count Quantity Inventory" data-hasaudit="false" data-controller="CountQuantityInventoryController">
          <div class="flexpage">
            <div class="flexrow">
              <div class="flexcolumn" style="flex:1 1 575px;">
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
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Apply Counted Inventory Quantities" style="flex:1 1 750px;">
                  <div class="flexrow">
                    <div>GRID</div>
                  </div>
                  <div class="flexrow"><div class="error-msg" style="margin-top:8px;"></div></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        `;
    }
}
var CountQuantityInventoryController = new CountQuantityInventory();