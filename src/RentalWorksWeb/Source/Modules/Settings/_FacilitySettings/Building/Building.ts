class Building {
    Module: string = 'Building';
    apiurl: string = 'api/v1/building';
    caption: string = Constants.Modules.Settings.children.FacilitySettings.children.Building.caption;
    nav: string = Constants.Modules.Settings.children.FacilitySettings.children.Building.nav;
    id: string = Constants.Modules.Settings.children.FacilitySettings.children.Building.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        this.events($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BuildingId"] input').val(uniqueids.BuildingId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        //floorgrid
        //const $floorGrid = $form.find('div[data-grid="FloorGrid"]');
        //const $floorGridControl = FwBrowse.loadGridFromTemplate('FloorGrid');
        //$floorGrid.empty().append($floorGridControl);
        //$floorGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        BuildingId: FwFormField.getValueByDataField($form, 'BuildingId')
        //    };
        //})
        //$floorGridControl.data('beforesave', request => {
        //    request.BuildingId = FwFormField.getValueByDataField($form, 'BuildingId');
        //});
        //FwBrowse.init($floorGridControl);
        //FwBrowse.renderRuntimeHtml($floorGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'FloorGrid',
            gridSecurityId: 'LrybQVClgY6f',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    BuildingId: FwFormField.getValueByDataField($form, 'BuildingId')
                };
            },
            beforeSave: (request: any) => {
                request.BuildingId = FwFormField.getValueByDataField($form, 'BuildingId');
            },
        });

        // Space Grid
        //const $spaceGrid = $form.find('div[data-grid="SpaceGrid"]');
        //const $spaceGridControl = FwBrowse.loadGridFromTemplate('SpaceGrid');
        //$spaceGrid.empty().append($spaceGridControl);
        //$spaceGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        BuildingId: FwFormField.getValueByDataField($form, 'BuildingId')
        //    };
        //})
        //FwBrowse.init($spaceGridControl);
        //FwBrowse.renderRuntimeHtml($spaceGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'SpaceGrid',
            gridSecurityId: 'DgWXultjwPXkU',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    BuildingId: FwFormField.getValueByDataField($form, 'BuildingId')
                };
            },
            //beforeSave: (request: any) => {
            //    request.BuildingId = FwFormField.getValueByDataField($form, 'BuildingId');
            //},
        });

        //space rate grid
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        //const $spaceRateGrid = $form.find('div[data-grid="SpaceRateGrid"]');
        //const $spaceRateGridControl = FwBrowse.loadGridFromTemplate('SpaceRateGrid');
        //$spaceRateGrid.empty().append($spaceRateGridControl);
        //$spaceRateGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        BuildingId: FwFormField.getValueByDataField($form, 'BuildingId'),
        //        WarehouseId: warehouse.warehouseid
        //    };
        //})
        //FwBrowse.init($spaceRateGridControl);
        //FwBrowse.renderRuntimeHtml($spaceRateGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'SpaceRateGrid',
            gridSecurityId: 'iWPadFxStXkcL',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    BuildingId: FwFormField.getValueByDataField($form, 'BuildingId'),
                    WarehouseId: warehouse.warehouseid
                };
            },
            //beforeSave: (request: any) => {
            //    request.BuildingId = FwFormField.getValueByDataField($form, 'BuildingId');
            //},
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $floorGrid = $form.find('[data-name="FloorGrid"]');
        FwBrowse.search($floorGrid);

        const $spaceGrid = $form.find('[data-name="SpaceGrid"]');
        FwBrowse.search($spaceGrid);

        const $spaceRateGrid = $form.find('[data-name="SpaceRateGrid"]');
        FwBrowse.search($spaceRateGrid);
    }
}
//----------------------------------------------------------------------------------------------
var BuildingController = new Building();