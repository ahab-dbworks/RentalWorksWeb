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

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
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
    afterLoad($form: any, response: any) {
        const $floorGrid = $form.find('[data-name="FloorGrid"]');
        FwBrowse.search($floorGrid);

        const $spaceGrid = $form.find('[data-name="SpaceGrid"]');
        FwBrowse.search($spaceGrid);

        //const $spaceRateGrid = $form.find('[data-name="SpaceRateGrid"]');
        //FwBrowse.search($spaceRateGrid);

        this.applyTaxOptions($form, response);
    }
    //----------------------------------------------------------------------------------------------
    applyTaxOptions($form: JQuery, response: any) {
        const $tax1Fields = $form.find('[data-taxfield="1"]');

        const updateCaption = ($fields, taxName, count) => {
            for (let i = 0; i < $fields.length; i++) {
                const $field = jQuery($fields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const taxRateName = `${taxType}TaxRate${count}`;
                    const taxRatePercentage = response[taxRateName];
                    if (typeof taxRatePercentage == 'number') {
                        const caption = taxName + ` (${taxRatePercentage.toFixed(3) + '%'})`;
                        $field.find('.fwformfield-caption').text(caption);
                    }
                }
            }

            const $billingTabTaxFields = $form.find(`[data-datafield="RentalTaxRate${count}"], [data-datafield="SalesTaxRate${count}"], [data-datafield="LaborTaxRate${count}"]`);
            for (let i = 0; i < $billingTabTaxFields.length; i++) {
                const $field = jQuery($billingTabTaxFields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const newCaption = `${taxType} ${taxName}`;
                    $field.find('.fwformfield-caption').text(newCaption);
                }
                $field.show();
            }
        }

        const tax1Name = response.Tax1Name;
        if (tax1Name != "") {
            updateCaption($tax1Fields, tax1Name, 1);
        }

        const $tax2Fields = $form.find('[data-taxfield="2"]');
        //const tax2Name = response.Tax2Name;
        const tax2Name: string = 'NY';
        if (tax2Name != "") {
            $tax2Fields.show();
            updateCaption($tax2Fields, tax2Name, 2);
        } else {
            $tax2Fields.hide();
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'TaxOptionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxoption`);
                break;
        }
    }
}
//----------------------------------------------------------------------------------------------
var VenueController = new Venue();