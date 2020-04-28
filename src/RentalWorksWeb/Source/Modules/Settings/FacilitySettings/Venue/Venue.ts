routes.push({ pattern: /^module\/venue$/, action: function (match: RegExpExecArray) { return VenueController.getModuleScreen(); } });
class Venue {
    Module: string = 'Venue';
    apiurl: string = 'api/v1/venue';
    caption: string = Constants.Modules.Settings.children.FacilitySettings.children.Venue.caption;
    nav: string = Constants.Modules.Settings.children.FacilitySettings.children.Venue.nav;
    id: string = Constants.Modules.Settings.children.FacilitySettings.children.Venue.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
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
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="VenueId"] input').val(uniqueids.VenueId);
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
                const venueId = FwFormField.getValueByDataField($form, 'VenueId');
                const floorId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');

                const $spaceGridControl = $form.find('[data-name="SpaceGrid"]');
                $spaceGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        BuildingId: venueId,
                        FloorId: floorId
                    }
                })
                $spaceGridControl.data('beforesave', request => {
                    request.BuildingId = venueId;
                    request.FloorId = floorId;
                });
                FwBrowse.search($spaceGridControl);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //$form.find('[data-name="SpaceGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
        //    try {
        //        const spaceId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
        //        const floorId = jQuery($tr.find('.column > .field')[1]).attr('data-originalvalue');
        //        const venueId = FwFormField.getValueByDataField($form, 'VenueId');
        //        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;

        //        const $spaceRateGridControl = $form.find('[data-name="SpaceRateGrid"]');
        //        $spaceRateGridControl.data('ondatabind', request => {
        //            request.uniqueids = {
        //                SpaceId: spaceId,
        //                FloorId: floorId,
        //                BuildingId: venueId,
        //                WarehouseId: warehouseId
        //            }
        //        })
        //        $spaceRateGridControl.data('beforesave', request => {
        //            request.BuildingId = venueId;
        //            request.FloorId = floorId;
        //            request.SpaceId = spaceId;
        //        });
        //        FwBrowse.search($spaceRateGridControl);
        //    } catch (ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'FloorGrid',
            gridSecurityId: 'LrybQVClgY6f',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    BuildingId: FwFormField.getValueByDataField($form, 'VenueId'),
                };
            },
            beforeSave: (request: any) => {
                request.BuildingId = FwFormField.getValueByDataField($form, 'VenueId');
            },
        });

        FwBrowse.renderGrid({
            nameGrid: 'SpaceGrid',
            gridSecurityId: 'DgWXultjwPXkU',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    BuildingId: FwFormField.getValueByDataField($form, 'VenueId'),
                };
            },
            //beforeSave: (request: any) => {
            //    request.BuildingId = FwFormField.getValueByDataField($form, 'VenueId');
            //},
        });

        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        //FwBrowse.renderGrid({
        //    nameGrid: 'SpaceRateGrid',
        //    gridSecurityId: 'iWPadFxStXkcL',
        //    moduleSecurityId: this.id,
        //    $form: $form,
        //    onDataBind: (request: any) => {
        //        request.uniqueids = {
        //            BuildingId: FwFormField.getValueByDataField($form, 'VenueId'),
        //            WarehouseId: warehouseId,
        //        };
        //    },
        //    //beforeSave: (request: any) => {
        //    //    request.BuildingId = FwFormField.getValueByDataField($form, 'VenueId');
        //    //},
        //});
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $floorGrid = $form.find('[data-name="FloorGrid"]');
        FwBrowse.search($floorGrid);

        const $spaceGrid = $form.find('[data-name="SpaceGrid"]');
        FwBrowse.search($spaceGrid);

        //const $spaceRateGrid = $form.find('[data-name="SpaceRateGrid"]');
        //FwBrowse.search($spaceRateGrid);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
        }
    }
}
//----------------------------------------------------------------------------------------------
var VenueController = new Venue();