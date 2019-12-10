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
        //const $discountItemRentalGrid = $form.find('div[data-grid="DiscountItemRentalGrid"]');
        //const $discountItemRentalGridControl = FwBrowse.loadGridFromTemplate('DiscountItemRentalGrid');
        //$discountItemRentalGrid.empty().append($discountItemRentalGridControl);
        //$discountItemRentalGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
        //        RecType: "R"
        //    };
        //})
        //$discountItemRentalGridControl.data('beforesave', request => {
        //    request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        //    request.RecType = "R";
        //})
        //FwBrowse.init($discountItemRentalGridControl);
        //FwBrowse.renderRuntimeHtml($discountItemRentalGridControl);

        //Discount Item Rental Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemRentalGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10, 
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
                    //eg ,                    RecType = "R"
                    //jh - RecType is needed on all of these dataBind and beforeSave methods
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                //request.RecType = "R";
            }
        });
        //-------------------------------------------------------------------------------------------------------------

        //const $discountItemSalesGrid = $form.find('div[data-grid="DiscountItemSalesGrid"]');
        //const $discountItemSalesGridControl = FwBrowse.loadGridFromTemplate('DiscountItemSalesGrid');
        //$discountItemSalesGrid.empty().append($discountItemSalesGridControl);
        //$discountItemSalesGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
        //        RecType: "S"
        //    };
        //})
        //$discountItemSalesGridControl.data('beforesave', request => {
        //    request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        //    request.RecType = "S";
        //})
        //FwBrowse.init($discountItemSalesGridControl);
        //FwBrowse.renderRuntimeHtml($discountItemSalesGridControl);

        //FwBrowse.renderRuntimeHtml($discountItemRentalGridControl);

        //Discount Item Sales Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemSalesGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId')
                    //,                    RecType = "S"
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                //request.RecType = "S";
            }
        });        //-------------------------------------------------------------------------------------------------------------
        //const $discountItemLaborGrid = $form.find('div[data-grid="DiscountItemLaborGrid"]');
        //const $discountItemLaborGridControl = FwBrowse.loadGridFromTemplate('DiscountItemLaborGrid');
        //$discountItemLaborGrid.empty().append($discountItemLaborGridControl);
        //$discountItemLaborGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
        //        RecType: "L"
        //    };
        //})
        //$discountItemLaborGridControl.data('beforesave', request => {
        //    request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        //    request.RecType = "L";
        //})
        //FwBrowse.init($discountItemLaborGridControl);
        //FwBrowse.renderRuntimeHtml($discountItemLaborGridControl);
        
        //Discount Item Labor Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemLaborGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId')
                    //,                    RecType = "L"
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                //request.RecType = "L";
            }
        });

        //-------------------------------------------------------------------------------------------------------------
        //const $discountItemMiscGrid = $form.find('div[data-grid="DiscountItemMiscGrid"]');
        //const $discountItemMiscGridControl = FwBrowse.loadGridFromTemplate('DiscountItemMiscGrid');
        //$discountItemMiscGrid.empty().append($discountItemMiscGridControl);
        //$discountItemMiscGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId'),
        //        RecType: "M"
        //    };
        //})
        //$discountItemMiscGridControl.data('beforesave', request => {
        //    request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
        //    request.RecType = "M";
        //})
        //FwBrowse.init($discountItemMiscGridControl);
        //FwBrowse.renderRuntimeHtml($discountItemMiscGridControl);

        //Discount Item Misc Grid
        FwBrowse.renderGrid({
            nameGrid: 'DiscountItemMiscGrid',
            gridSecurityId: 'UMKuETy6vOLA',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DiscountTemplateId: FwFormField.getValueByDataField($form, 'DiscountTemplateId')
                    //,                    RecType = "M"
                };
            }, 
            beforeSave: (request: any) => {
                request.DiscountTemplateId = FwFormField.getValueByDataField($form, 'DiscountTemplateId');
                //request.RecType = "M";
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
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
        }
    }
}

var DiscountTemplateController = new DiscountTemplate();