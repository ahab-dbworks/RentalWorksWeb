routes.push({ pattern: /^module\/inventorysequenceutility$/, action: function (match: RegExpExecArray) { return InventorySequenceUtilityController.getModuleScreen(); } });

class InventorySequenceUtility {
    Module: string = 'InventorySequenceUtility';
    apiurl: string = 'api/v1/inventorysequenceutility';
    caption: string = Constants.Modules.Utilities.children.InventorySequenceUtility.caption;
    nav: string = Constants.Modules.Utilities.children.InventorySequenceUtility.nav;
    id: string = Constants.Modules.Utilities.children.InventorySequenceUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.afterLoad($form);
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

        FwFormField.loadItems($form.find('div[data-datafield="InventoryType"]'), [
            { value: 'R', caption: 'Rental', checked: 'checked' },
            { value: 'S', caption: 'Sales' },
            { value: 'L', caption: 'Labor' },
            { value: 'M', caption: 'Misc' },
        ]);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('div[data-datafield="InventoryType"]').on('change', e => {
            const $this = jQuery(e.currentTarget)
        })


        $form.find('[data-name="FloorGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                var buildingId = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
                var floorId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');

                var $spaceGridControl: any;
                $spaceGridControl = $form.find('[data-name="SpaceGrid"]');
                $spaceGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        BuildingId: buildingId,
                        FloorId: floorId
                    }
                })
                $spaceGridControl.data('beforesave', function (request) {
                    request.BuildingId = buildingId;
                    request.FloorId = floorId;
                });
                FwBrowse.search($spaceGridControl);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $form.find('[data-name="SpaceGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                var spaceId
                    , floorId
                    , buildingId;

                spaceId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
                floorId = jQuery($tr.find('.column > .field')[1]).attr('data-originalvalue');
                buildingId = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
                var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

                var $spaceRateGridControl: any;
                $spaceRateGridControl = $form.find('[data-name="SpaceRateGrid"]');
                $spaceRateGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        SpaceId: spaceId,
                        FloorId: floorId,
                        BuildingId: buildingId,
                        WarehouseId: warehouse.warehouseid
                    }
                })
                $spaceRateGridControl.data('beforesave', function (request) {
                    request.BuildingId = buildingId;
                    request.FloorId = floorId;
                    request.SpaceId = spaceId;
                });
                FwBrowse.search($spaceRateGridControl);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });


    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        $form.find('.itemid[data-displayfield="BarCode"] input').focus();
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'SpaceWarehouseRateGrid',
            gridSecurityId: '',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    RateId: FwFormField.getValueByDataField($form, 'RateId')
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        switch (datafield) {
            case 'ItemId':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateitem`);
                break;
            case 'InventoryId':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid,
                    TrackedBy: 'QUANTITY',
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
        }
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
       <div id="inventorysequenceutilityform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Inventory Sequence Utility" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="InventoryRetireUtilityController">
          <div id="inventorysequenceutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs"></div>
            <div class="tabpages">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Sequence Utility" style="max-width:700px">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="InventoryType"></div>
                </div>
                <div class="flexrow">
                  <div class="flexcoumn">
                    <div data-control="FwGrid" data-grid="" style="min-width:250px;max-width:700px;"></div>
                  </div>
                  <div class="flexcoumn">
                    <div data-control="FwGrid" data-grid="" style="min-width:250px;max-width:700px;"></div>
                  </div>
                  <div class="flexcoumn">
                    <div data-control="FwGrid" data-grid="" style="min-width:250px;max-width:700px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var InventorySequenceUtilityController = new InventorySequenceUtility();