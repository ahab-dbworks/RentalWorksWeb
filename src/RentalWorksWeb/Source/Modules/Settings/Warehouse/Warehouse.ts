class Warehouse {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Warehouse';
        this.apiurl = 'api/v1/warehouse';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Warehouse', false, 'BROWSE', true);
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

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

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

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val(uniqueids.WarehouseId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $warehouseDepartmentGrid: any;
        var $warehouseDepartmentGridControl: any;
        var $warehouseInventoryTypeGrid: any;
        var $warehouseInventoryTypeGridControl: any;
        var $warehouseAvailabilityHourGrid: any;
        var $warehouseAvailabilityHourGridControl: any;
        var $warehouseDepartmentUserGrid: any;
        var $warehouseDepartmentUserGridControl: any;
        var $warehouseOfficeLocationGrid: any;
        var $warehouseOfficeLocationGridControl: any;
        var $warehouseQuikLocateApproverGrid: any;
        var $warehouseQuikLocateApproverGridControl: any;

        $warehouseDepartmentGrid = $form.find('div[data-grid="WarehouseDepartmentGrid"]');
        $warehouseDepartmentGridControl = jQuery(jQuery('#tmpl-grids-WarehouseDepartmentGridBrowse').html());
        $warehouseDepartmentGrid.empty().append($warehouseDepartmentGridControl);
        $warehouseDepartmentGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseDepartmentGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseDepartmentGridControl);
        FwBrowse.renderRuntimeHtml($warehouseDepartmentGridControl);

        $warehouseInventoryTypeGrid = $form.find('div[data-grid="WarehouseInventoryTypeGrid"]');
        $warehouseInventoryTypeGridControl = jQuery(jQuery('#tmpl-grids-WarehouseInventoryTypeGridBrowse').html());
        $warehouseInventoryTypeGrid.empty().append($warehouseInventoryTypeGridControl);
        $warehouseInventoryTypeGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseInventoryTypeGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseInventoryTypeGridControl);
        FwBrowse.renderRuntimeHtml($warehouseInventoryTypeGridControl);

        $warehouseAvailabilityHourGrid = $form.find('div[data-grid="WarehouseAvailabilityHourGrid"]');
        $warehouseAvailabilityHourGridControl = jQuery(jQuery('#tmpl-grids-WarehouseAvailabilityHourGridBrowse').html());
        $warehouseAvailabilityHourGrid.empty().append($warehouseAvailabilityHourGridControl);
        $warehouseAvailabilityHourGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseAvailabilityHourGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseAvailabilityHourGridControl);
        FwBrowse.renderRuntimeHtml($warehouseAvailabilityHourGridControl);

        $warehouseDepartmentUserGrid = $form.find('div[data-grid="WarehouseDepartmentUserGrid"]');
        $warehouseDepartmentUserGridControl = jQuery(jQuery('#tmpl-grids-WarehouseDepartmentUserGridBrowse').html());
        $warehouseDepartmentUserGrid.empty().append($warehouseDepartmentUserGridControl);
        $warehouseDepartmentUserGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseDepartmentUserGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseDepartmentUserGridControl);
        FwBrowse.renderRuntimeHtml($warehouseDepartmentUserGridControl);

        $warehouseOfficeLocationGrid = $form.find('div[data-grid="WarehouseOfficeLocationGrid"]');
        $warehouseOfficeLocationGridControl = jQuery(jQuery('#tmpl-grids-WarehouseOfficeLocationGridBrowse').html());
        $warehouseOfficeLocationGrid.empty().append($warehouseOfficeLocationGridControl);
        $warehouseOfficeLocationGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseOfficeLocationGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseOfficeLocationGridControl);
        FwBrowse.renderRuntimeHtml($warehouseOfficeLocationGridControl);

        $warehouseQuikLocateApproverGrid = $form.find('div[data-grid="WarehouseQuikLocateApproverGrid"]');
        $warehouseQuikLocateApproverGridControl = jQuery(jQuery('#tmpl-grids-WarehouseQuikLocateApproverGridBrowse').html());
        $warehouseQuikLocateApproverGrid.empty().append($warehouseQuikLocateApproverGridControl);
        $warehouseQuikLocateApproverGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                WarehouseId: $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
            };
        });
        $warehouseQuikLocateApproverGridControl.data('beforesave', function (request) {
            request.WarehouseId = $form.find('div.fwformfield[data-datafield="WarehouseId"] input').val()
        });
        FwBrowse.init($warehouseQuikLocateApproverGridControl);
        FwBrowse.renderRuntimeHtml($warehouseQuikLocateApproverGridControl);
    }

    afterLoad($form: any) {
        var $warehouseDepartmentGrid: any;
        var $warehouseInventoryTypeGrid: any;
        var $warehouseAvailabilityHourGrid: any;
        var $warehouseDepartmentUserGrid: any;
        var $warehouseOfficeLocationGrid: any;
        var $warehouseQuikLocateApproverGrid: any;

        $warehouseDepartmentGrid = $form.find('[data-name="WarehouseDepartmentGrid"]');
        FwBrowse.search($warehouseDepartmentGrid);

        $warehouseInventoryTypeGrid = $form.find('[data-name="WarehouseInventoryTypeGrid"]');
        FwBrowse.search($warehouseInventoryTypeGrid);

        $warehouseAvailabilityHourGrid = $form.find('[data-name="WarehouseAvailabilityHourGrid"]');
        FwBrowse.search($warehouseAvailabilityHourGrid);

        $warehouseDepartmentUserGrid = $form.find('[data-name="WarehouseDepartmentUserGrid"]');
        FwBrowse.search($warehouseDepartmentUserGrid);

        $warehouseOfficeLocationGrid = $form.find('[data-name="WarehouseOfficeLocationGrid"]');
        FwBrowse.search($warehouseOfficeLocationGrid);

        $warehouseQuikLocateApproverGrid = $form.find('[data-name="WarehouseQuikLocateApproverGrid"]');
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

        if (FwFormField.getValue($form, 'div[data-datafield="TaxCountry"]') === 'U') {
            $form.find('.ustax').show();
            $form.find('.catax').hide();
        }
        if (FwFormField.getValue($form, 'div[data-datafield="TaxCountry"]') === 'C') {
            $form.find('.catax').show();
            $form.find('.ustax').hide();
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
}

var WarehouseController = new Warehouse();