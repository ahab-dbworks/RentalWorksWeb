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
            const inventoryType = FwFormField.getValueByDataField($form, 'InventoryType');
            let type;
            switch (inventoryType) {
                case 'R':
                    type = 'Rental';
                    break;
                case 'S':
                    type = 'Sales';
                    break;
                case 'L':
                    type = 'Labor';
                    break;
                case 'M':
                    type = 'Misc';
                    break;
            }

            const $inventoryTypeGrid = $form.find('[data-name="InventoryTypeGrid"]');
            $inventoryTypeGrid.data('ondatabind', function (request) {
                request.uniqueids = {
                    type: true,
                }
            })
            FwBrowse.search($inventoryTypeGrid);

            const $categoryGrid = $form.find('[data-name="CategoryGrid"]');
            $inventoryTypeGrid.data('ondatabind', function (request) {
                request.uniqueids = {
                    type: true,
                }
            })
            FwBrowse.search($categoryGrid);

            const $subCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
            $inventoryTypeGrid.data('ondatabind', function (request) {
                request.uniqueids = {
                    type: true,
                }
            })
            FwBrowse.search($subCategoryGrid);
        });
        // ----------

        //$form.find('[data-name="CategoryGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
        //    try {
        //        var buildingId = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
        //        var floorId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');

        //        const $categoryGrid = $form.find('[data-name="CategoryGrid"]');

        //        $spaceGridControl.data('ondatabind', function (request) {
        //            request.uniqueids = {
        //                BuildingId: buildingId,
        //                FloorId: floorId
        //            }
        //        })
        //        $spaceGridControl.data('beforesave', function (request) {
        //            request.BuildingId = buildingId;
        //            request.FloorId = floorId;
        //        });
        //        FwBrowse.search($spaceGridControl);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});

        //$form.find('[data-name="InventoryTypeGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
        //    try {
        //        //var spaceId
        //        //    , floorId
        //        //    , buildingId;

        //        //spaceId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
        //        //floorId = jQuery($tr.find('.column > .field')[1]).attr('data-originalvalue');
        //        //buildingId = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
        //        //var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        //        const $inventoryTypeGrid = $form.find('[data-name="InventoryTypeGrid"]');
        //        $inventoryTypeGrid.data('ondatabind', function (request) {
        //            request.uniqueids = {
        //                SpaceId: spaceId,
        //                FloorId: floorId,
        //                BuildingId: buildingId,
        //                WarehouseId: warehouse.warehouseid
        //            }
        //        })
        //        $inventoryTypeGrid.data('beforesave', function (request) {
        //            request.BuildingId = buildingId;
        //            request.FloorId = floorId;
        //            request.SpaceId = spaceId;
        //        });
        //        FwBrowse.search($inventoryTypeGrid);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const $inventoryTypeGrid = $form.find('[data-name="InventoryTypeGrid"]');
        FwBrowse.search($inventoryTypeGrid);
        const $categoryGrid = $form.find('[data-name="CategoryGrid"]');
        FwBrowse.search($categoryGrid);
        const $subCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        FwBrowse.search($subCategoryGrid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'InventoryTypeGrid',
            gridSecurityId: '',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    InventoryTypeId: FwFormField.getValueByDataField($form, 'InventoryTypeId'),
                    Rental: true,
                };
            },
            beforeSave: (request: any) => {
                request.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'CategoryGrid',
            gridSecurityId: 'pWsHOgp1o7Obw',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CategoryId: FwFormField.getValueByDataField($form, 'CategoryId'),
                    Rental: true,
                };
            },
            beforeSave: (request: any) => {
                request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SubCategoryGrid',
            gridSecurityId: 'vHMa0l5PUysXo',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CategoryId: FwFormField.getValueByDataField($form, 'CategoryId'),
                    Rental: true,
                };
            },
            beforeSave: (request: any) => {
                request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            }
        });
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
                  <div class="flexcolumn">
                    <div data-control="FwGrid" data-grid="InventoryTypeGrid" style="min-width:240px;max-width:400px;"></div>
                  </div>
                  <div class="flexcolumn">
                    <div data-control="FwGrid" data-grid="CategoryGrid" style="min-width:240px;max-width:400px;"></div>
                  </div>
                  <div class="flexcolumn">
                    <div data-control="FwGrid" data-grid="SubCategoryGrid" style="min-width:240px;max-width:400px;"></div>
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