class Warehouse {
    Module: string = 'Warehouse';
    apiurl: string = 'api/v1/warehouse';
    caption: string = Constants.Modules.Settings.children.WarehouseSettings.children.Warehouse.caption;
    nav: string = Constants.Modules.Settings.children.WarehouseSettings.children.Warehouse.nav;
    id: string = Constants.Modules.Settings.children.WarehouseSettings.children.Warehouse.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            // Dashboard search
            if (typeof filter !== 'undefined') {
                const datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                };
                filter.datafield = datafields.join('')
                const parsedSearch = filter.search.replace(/%20/g, " ").replace(/%2f/g, '/');
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(parsedSearch);
            }
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

        if (FwFormField.getValueByDataField($form, 'TaxCountry') === 'U') {
            $form.find('.ustax').show();
            $form.find('.catax').hide();
        }
        if (FwFormField.getValueByDataField($form, 'TaxCountry') === 'C') {
            $form.find('.catax').show();
            $form.find('.ustax').hide();
        }

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val(uniqueids.WarehouseId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'WarehouseDepartmentGrid',
            gridSecurityId: 'BlB26FHHFsaQx',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
            },
            beforeSave: (request: any) => {
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId')
            }
        });
        // ----------
         FwBrowse.renderGrid({
            nameGrid: 'WarehouseInventoryTypeGrid',
            gridSecurityId: 'HRLS0W2gCu4lD',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
            },
            beforeSave: (request: any) => {
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId')
            }
        });
        // ----------
        //FwBrowse.renderGrid({
        //    nameGrid: 'WarehouseAvailabilityHourGrid',
        //    gridSecurityId: '1iBtCdzhTkio4',
        //    moduleSecurityId: this.id,
        //    $form: $form,
        //    pageSize: 10,
        //    onDataBind: (request: any) => {
        //        request.uniqueids = {
        //            WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
        //        };
        //    },
        //    beforeSave: (request: any) => {
        //        request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId')
        //    }
        //});
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'WarehouseDepartmentUserGrid',
            gridSecurityId: 'BlB26FHHFsaQx',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
            },
            beforeSave: (request: any) => {
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'WarehouseOfficeLocationGrid',
            gridSecurityId: 'B1kMAlpwQNPLG',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                };
            },
            beforeSave: (request: any) => {
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'WarehouseQuikLocateApproverGrid',
            gridSecurityId: 'IBGJoUXyFbKmm',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
            };
            },
            beforeSave: (request: any) => {
                request.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            }
        });
        // ----------
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $warehouseDepartmentGrid = $form.find('[data-name="WarehouseDepartmentGrid"]');
        FwBrowse.search($warehouseDepartmentGrid);

        const $warehouseInventoryTypeGrid = $form.find('[data-name="WarehouseInventoryTypeGrid"]');
        FwBrowse.search($warehouseInventoryTypeGrid);

        const $warehouseAvailabilityHourGrid = $form.find('[data-name="WarehouseAvailabilityHourGrid"]');
        FwBrowse.search($warehouseAvailabilityHourGrid);

        const $warehouseDepartmentUserGrid = $form.find('[data-name="WarehouseDepartmentUserGrid"]');
        FwBrowse.search($warehouseDepartmentUserGrid);

        const $warehouseOfficeLocationGrid = $form.find('[data-name="WarehouseOfficeLocationGrid"]');
        FwBrowse.search($warehouseOfficeLocationGrid);

        const $warehouseQuikLocateApproverGrid = $form.find('[data-name="WarehouseQuikLocateApproverGrid"]');
        FwBrowse.search($warehouseQuikLocateApproverGrid);

        if (FwFormField.getValue($form, 'div[data-datafield="AssignBarCodesBy"]') === 'S') {
            $form.find('.singlerange').show();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="AssignBarCodesBy"]') === 'C') {
            $form.find('.warehousedepartment').show();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="AssignBarCodesBy"]') === 'D') {
            $form.find('.warehouseinventorytype').show();
        }

        if ($form.find('[data-datafield="MarkupSales"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="SalesMarkupPercent"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="SalesMarkupPercent"]'))
        }
        if ($form.find('[data-datafield="MarkupParts"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="PartsMarkupPercent"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="PartsMarkupPercent"]'))
        }
        if ($form.find('[data-datafield="MarkupReplacementCost"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="ReplacementCostMarkupPercent"]'))
        } else {
            FwFormField.disable($form.find('[data-datafield="ReplacementCostMarkupPercent"]'))
        }
        if ($form.find('[data-datafield="CalculateDefaultRentalRates"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('[data-datafield="RentalDailyRatePercentOfReplacementCost"]'));
            FwFormField.enable($form.find('[data-datafield="RentalWeeklyRateMultipleOfDailyRate"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="RentalDailyRatePercentOfReplacementCost"]'));
            FwFormField.disable($form.find('[data-datafield="RentalWeeklyRateMultipleOfDailyRate"]'));
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('[data-datafield="AssignBarCodesBy"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            $form.find('.singlerange').hide();
            $form.find('.warehousedepartment').hide();
            $form.find('.warehouseinventorytype').hide();

            if ($this.val() === 'S') {
                $form.find('.singlerange').show();
            }
            if ($this.val() === 'C') {
                $form.find('.warehousedepartment').show();
            }
            if ($this.val() === 'D') {
                $form.find('.warehouseinventorytype').show();
            }
        });

        $form.find('[data-datafield="MarkupSales"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="SalesMarkupPercent"]'))
            }
            else {
                FwFormField.disable($form.find('[data-datafield="SalesMarkupPercent"]'))
            }
        });

        $form.find('[data-datafield="MarkupParts"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="PartsMarkupPercent"]'))
            }
            else {
                FwFormField.disable($form.find('[data-datafield="PartsMarkupPercent"]'))
            }
        });

        $form.find('[data-datafield="MarkupReplacementCost"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ReplacementCostMarkupPercent"]'))
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ReplacementCostMarkupPercent"]'))
            }
        });

        $form.find('[data-datafield="CalculateDefaultRentalRates"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="RentalDailyRatePercentOfReplacementCost"]'))
                FwFormField.enable($form.find('[data-datafield="RentalWeeklyRateMultipleOfDailyRate"]'))
            }
            else {
                FwFormField.disable($form.find('[data-datafield="RentalDailyRatePercentOfReplacementCost"]'))
                FwFormField.disable($form.find('[data-datafield="RentalWeeklyRateMultipleOfDailyRate"]'))
            }
        });

        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
            $form.find('.ustax').hide();
            $form.find('.catax').hide();

            if ($tr.find('.field[data-browsedatafield="TaxCountry"]').attr('data-originalvalue') === 'U') {
                $form.find('.ustax').show();
            };

            if ($tr.find('.field[data-browsedatafield="TaxCountry"]').attr('data-originalvalue') === 'C') {
                FwFormField.setValueByDataField($form, 'RentalTaxRate2', $tr.find('.field[data-browsedatafield="RentalTaxRate2"]').attr('data-originalvalue'));
                FwFormField.setValueByDataField($form, 'SalesTaxRate2', $tr.find('.field[data-browsedatafield="SalesTaxRate2"]').attr('data-originalvalue'));
                FwFormField.setValueByDataField($form, 'LaborTaxRate2', $tr.find('.field[data-browsedatafield="LaborTaxRate2"]').attr('data-originalvalue'));
                $form.find('.catax').show();
            };
        });
    }
}

var WarehouseController = new Warehouse();