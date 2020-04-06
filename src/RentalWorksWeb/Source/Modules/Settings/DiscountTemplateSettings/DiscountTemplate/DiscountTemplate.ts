class DiscountTemplate {
    Module: string = 'DiscountTemplate';
    apiurl: string = 'api/v1/discounttemplate';
    caption: string = Constants.Modules.Settings.children.DiscountTemplateSettings.children.DiscountTemplate.caption;
    nav: string = Constants.Modules.Settings.children.DiscountTemplateSettings.children.DiscountTemplate.nav;
    id: string = Constants.Modules.Settings.children.DiscountTemplateSettings.children.DiscountTemplate.id;
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
            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
        }
        return $form;
    }
    //-------------------------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DiscountTemplateId"] input').val(uniqueids.DiscountTemplateId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //-------------------------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        //Discount Item Rental Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemRentalGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'Add All Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.addAllItems($form, 'Rental');
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                    RecType: "R",
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                request.RecType = "R";
            }
        });
        //-------------------------------------------------------------------------------------------------------------
        //Discount Item Sales Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemSalesGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'Add All Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.addAllItems($form, 'Sales');
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                    RecType: "S",
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                request.RecType = "S";
            }
        });
        //-------------------------------------------------------------------------------------------------------------
        //Discount Item Labor Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemLaborGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'Add All Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.addAllItems($form, 'Labor');
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                    RecType: "L",
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                request.RecType = "L";
            }
        });

        //-------------------------------------------------------------------------------------------------------------
        //Discount Item Misc Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemMiscGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'Add All Items', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.addAllItems($form, 'Misc');
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                    RecType: "M",
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                request.RecType = "M";
            }
        });
        //-------------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $discountItemControl = $form.find('[data-name="DiscountItemGrid"]');
        FwBrowse.search($discountItemControl);

        const $discountItemRentalControl = $form.find('[data-name="DiscountItemRentalGrid"]');
        FwBrowse.search($discountItemRentalControl);

        const $discountItemSalesControl = $form.find('[data-name="DiscountItemSalesGrid"]');
        FwBrowse.search($discountItemSalesControl);

        const $discountItemLaborControl = $form.find('[data-name="DiscountItemLaborGrid"]');
        FwBrowse.search($discountItemLaborControl);

        const $discountItemMiscControl = $form.find('[data-name="DiscountItemMiscGrid"]');
        FwBrowse.search($discountItemMiscControl);

        //const rentalDays = parseFloat(FwFormField.getValueByDataField($form, 'RentalDaysPerWeek'));
        //const rentalDecimals = rentalDays.toFixed(3);
        //FwFormField.setValueByDataField($form, 'RentalDaysPerWeek', rentalDecimals);

        //const spaceDays = parseFloat(FwFormField.getValueByDataField($form, 'SpaceDaysPerWeek'));
        //const spaceDecimals = spaceDays.toFixed(3);
        //FwFormField.setValueByDataField($form, 'SpaceDaysPerWeek', spaceDecimals);
    }
    //-------------------------------------------------------------------------------------------------------------
    addAllItems($form: JQuery, recType: string) {
        let $confirmation;
        const discountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        const $grid = $form.find(`[data-name="DiscountItem${recType}Grid"]`);
        $confirmation = FwConfirmation.renderConfirmation('Add All Items',
            `Add ALL ${recType} items to this Discount Template? This cannot be undone.`);
        const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        FwConfirmation.addButton($confirmation, 'No');
        $yes.on('click', () => {
            const request: any = {
                DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid,
                RecType: recType.charAt(0)
            };

            FwAppData.apiMethod(true, 'POST', `api/v1/discounttemplate/addallitems`, request, FwServices.defaultTimeout,
                response => {
                    try {
                        FwBrowse.search($grid);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, ex => FwFunc.showError(ex), $grid, discountTemplateId);
        });
    }
    //-------------------------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
        }
    }
}

var DiscountTemplateController = new DiscountTemplate();