routes.push({ pattern: /^module\/createpicklist$/, action: function (match) { return CreatePickListController.getModuleScreen(); } });
class CreatePickList {
    constructor() {
        this.Module = 'CreatePickList';
        this.caption = Constants.Modules.Home.children.CreatePickList.caption;
        this.nav = Constants.Modules.Home.children.CreatePickList.nav;
        this.id = Constants.Modules.Home.children.CreatePickList.id;
    }
    getModuleScreen() {
        var screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        var $form = this.openForm('EDIT', null);
        screen.load = function () {
            FwModule.openModuleTab($form, 'Create Pick List', false, 'FORM', true);
        };
        screen.unload = function () {
        };
        return screen;
    }
    openForm(mode, parentmoduleinfo) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');
        var $optionToggle = $form.find('.optiontoggle');
        $form.find('.options').toggle();
        $optionToggle.on('click', function () {
            $form.find('.options').toggle();
        });
        if (typeof parentmoduleinfo !== 'undefined') {
            $form.find('div[data-datafield="OrderId"] input').val(parentmoduleinfo.OrderId);
        }
        $form.find('.defaultoptions input').prop('checked', true);
        return $form;
    }
    renderGrids($form) {
        const $pickListUtilityGridControl = FwBrowse.renderGrid({
            nameGrid: 'PickListUtilityGrid',
            gridSecurityId: 'DOnlknWuWfYS',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    SessionId: FwFormField.getValueByDataField($form, 'OrderId')
                };
            }
        });
        $form.find('.applyoptions').on('click', function () {
            var miscfields = CreatePickListController.getOptions($form);
            $pickListUtilityGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    SessionId: FwFormField.getValueByDataField($form, 'OrderId')
                };
                request.miscfields = miscfields;
            });
            FwBrowse.search($pickListUtilityGridControl);
        });
        $form.find('.selectall').on('click', function () {
            var request = {};
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                SessionId: FwFormField.getValueByDataField($form, 'OrderId')
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/selectall', request, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwBrowse.search($pickListUtilityGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
        $form.find('.selectnone').on('click', function () {
            var request = {};
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                SessionId: FwFormField.getValueByDataField($form, 'OrderId')
            };
            FwAppData.apiMethod(true, 'POST', 'api/v1/picklistutilityitem/selectnone', request, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwBrowse.search($pickListUtilityGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
    }
    getOptions($form) {
        var miscfields = {};
        var $options = $form.find('.option');
        var optionName, optionValue, optionType;
        $options.each(function () {
            optionName = jQuery(this).attr('data-datafield');
            optionType = jQuery(this).attr('data-type');
            if (optionType == "checkbox") {
                optionValue = jQuery(this).find('input:checked').val();
                if (optionValue == "on") {
                    optionValue = true;
                }
                else {
                    optionValue = false;
                }
            }
            else {
                optionValue = jQuery(this).find('input').val();
            }
            if (!(optionType == "date" && optionValue == "")) {
                miscfields[optionName] = optionValue;
            }
        });
        return miscfields;
    }
    afterLoad($form) {
        var $pickListUtilityGrid;
        $pickListUtilityGrid = $form.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    getFormTemplate() {
        return `
        <div id="createpicklistform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Create Pick List" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="CreatePickListController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-saveorder="1" data-caption="" data-datafield="OrderId"></div>
          <div id="createpicklistform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
            </div>
            <div class="tabpages">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Create Pick List">
                  <div class="formrow">
                    <div class="formcolumn summaryview" style="width:100%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="PickListUtilityGrid" data-securitycaption="Pick List Utility"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="optiontoggle fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                    <div class="createpicklist fwformcontrol" data-type="button" style="float:right;">Create Pick List</div>
                  </div>
                  <br /><br />
                  <div class="formrow options">
                    <div class="formcolumn" style="width:20%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items by Status">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items Not Yet Staged" data-datafield="ItemsNotYetStaged"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items Staged" data-datafield="ItemsStaged"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items Out" data-datafield="ItemsOut"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Pick Date Range">
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield option" data-caption="From" data-datafield="PickDateFrom"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield option" data-caption="To" data-datafield="PickDateTo"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:40%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items to Include">
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Rental Items" data-datafield="RentalItems"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Sales Items" data-datafield="SaleItems"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Vendor Items" data-datafield="VendorItems"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Labor Items" data-datafield="LaborItems"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield option" data-caption="Warehouse" data-validationname="WarehouseValidation" data-datafield="WarehouseId" data-displayfield="Warehouse" style="width:50%;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield defaultoptions" data-caption="All" data-datafield=""></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Complete/Kit Main Items" data-datafield="CompleteKitMain"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Complete/Kit Accessories" data-datafield="CompleteKitAccessories"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Complete/Kit Options" data-datafield="CompleteKitOptions"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Stand-Alone Items" data-datafield="StandAloneItems"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items on Other Pick Lists" data-datafield="ItemsOnOtherPickLists"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Reduce Qty of Items Already Picked" data-datafield="ReduceQuantityAlreadyPicked"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:40%;">
                      <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Types">
                   
                      </div>-->
                      <br /><br />
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option" data-caption="Summarize By I-Code" data-datafield="SummarizeByICode"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option" data-caption="Summarize Complete/Kit Accessories/Options" data-datafield="SummarizeCompleteKitItems"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option" data-caption="Show Complete/Kit Accessories/Options in Their Own Inventory Types/Categories" data-datafield="HonorCompleteKitItemTypes"></div>
                      <br /><br />
                      <div class="applyoptions fwformcontrol" data-type="button" style="float:left;">Apply</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}
var CreatePickListController = new CreatePickList();
//# sourceMappingURL=CreatePickList.js.map