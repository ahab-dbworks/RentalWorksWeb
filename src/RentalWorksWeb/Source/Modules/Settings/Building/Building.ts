class RwBuilding {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Building';
        this.apiurl = 'api/v1/building';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Building', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        this.events($form);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BuildingId"] input').val(uniqueids.BuildingId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    events($form: JQuery): void {

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

    renderGrids($form: any) {
        var $floorGrid: any;
        var $floorGridControl: any;
        var $spaceGrid: any;
        var $spaceGridControl: any;
        var $spaceRateGrid: any;
        var $spaceRateGridControl: any;

        $floorGrid = $form.find('div[data-grid="FloorGrid"]');
        $floorGridControl = jQuery(jQuery('#tmpl-grids-FloorGridBrowse').html());
        $floorGrid.empty().append($floorGridControl);
        $floorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                BuildingId: $form.find('div.fwformfield[data-datafield="BuildingId"] input').val()
            };
        })
        $floorGridControl.data('beforesave', function (request) {
            request.BuildingId = FwFormField.getValueByDataField($form, 'BuildingId');
        });
        FwBrowse.init($floorGridControl);
        FwBrowse.renderRuntimeHtml($floorGridControl);

        $spaceGrid = $form.find('div[data-grid="SpaceGrid"]');
        $spaceGridControl = jQuery(jQuery('#tmpl-grids-SpaceGridBrowse').html());
        $spaceGrid.empty().append($spaceGridControl);
        $spaceGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                BuildingId: $form.find('div.fwformfield[data-datafield="BuildingId"] input').val()
            };
        })
        FwBrowse.init($spaceGridControl);
        FwBrowse.renderRuntimeHtml($spaceGridControl);

        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        $spaceRateGrid = $form.find('div[data-grid="SpaceRateGrid"]');
        $spaceRateGridControl = jQuery(jQuery('#tmpl-grids-SpaceRateGridBrowse').html());
        $spaceRateGrid.empty().append($spaceRateGridControl);
        $spaceRateGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                BuildingId: $form.find('div.fwformfield[data-datafield="BuildingId"] input').val(),
                WarehouseId: warehouse.warehouseid
            };
        })
        FwBrowse.init($spaceRateGridControl);
        FwBrowse.renderRuntimeHtml($spaceRateGridControl);


    }

    afterLoad($form: any) {
        var $floorGrid: any;
        var $spaceGrid: any;
        var $spaceRateGrid: any;

        $floorGrid = $form.find('[data-name="FloorGrid"]');
        FwBrowse.search($floorGrid);

        $spaceGrid = $form.find('[data-name="SpaceGrid"]');
        FwBrowse.search($spaceGrid);

        $spaceRateGrid = $form.find('[data-name="SpaceRateGrid"]');
        FwBrowse.search($spaceRateGrid);

    }


}

(<any>window).BuildingController = new RwBuilding();