class POType {
    Module: string = 'POType';
    apiurl: string = 'api/v1/potype';
    caption: string = Constants.Modules.Settings.children.POSettings.children.POType.caption;
    nav: string = Constants.Modules.Settings.children.POSettings.children.POType.nav;
    id: string = Constants.Modules.Settings.children.POSettings.children.POType.id;
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

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val(uniqueids.PoTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        //----------
        //const $orderTypeCoverLetterGrid = $form.find('div[data-grid="OrderTypeCoverLetterGrid"]');
        //const $orderTypeCoverLetterGridControl = FwBrowse.loadGridFromTemplate('OrderTypeCoverLetterGrid');
        //$orderTypeCoverLetterGrid.empty().append($orderTypeCoverLetterGridControl);
        //$orderTypeCoverLetterGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
        //    };
        //})
        //$orderTypeCoverLetterGridControl.data('beforesave', request => {
        //    request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        //});
        //FwBrowse.init($orderTypeCoverLetterGridControl);
        //FwBrowse.renderRuntimeHtml($orderTypeCoverLetterGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeCoverLetterGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
                };
            },
            //(cannot add New)
            //beforeSave: (request: any) => {
            //    request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
            //}
        });
        //----------
        //const $orderTypeTermsAndConditionsGrid = $form.find('div[data-grid="OrderTypeTermsAndConditionsGrid"]');
        //const $orderTypeTermsAndConditionsGridControl = FwBrowse.loadGridFromTemplate('OrderTypeTermsAndConditionsGrid');
        //$orderTypeTermsAndConditionsGrid.empty().append($orderTypeTermsAndConditionsGridControl);
        //$orderTypeTermsAndConditionsGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
        //    };
        //})
        //$orderTypeTermsAndConditionsGridControl.data('beforesave', request => {
        //    request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        //});
        //FwBrowse.init($orderTypeTermsAndConditionsGridControl);
        //FwBrowse.renderRuntimeHtml($orderTypeTermsAndConditionsGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeTermsAndConditionsGrid',
            gridSecurityId: 'acguZNBoT1XC',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
                };
            },
            //(cannot add New)
            //beforeSave: (request: any) => {
            //    request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
            //}
        });
        //----------
        //const $orderTypeActivityDatesGrid = $form.find('div[data-grid="OrderTypeActivityDatesGrid"]');
        //const $orderTypeActivityDatesGridControl = FwBrowse.loadGridFromTemplate('OrderTypeActivityDatesGrid');
        //$orderTypeActivityDatesGrid.empty().append($orderTypeActivityDatesGridControl);
        //$orderTypeActivityDatesGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
        //    };
        //})
        //$orderTypeActivityDatesGridControl.data('beforesave', request => {
        //    request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        //});
        //FwBrowse.init($orderTypeActivityDatesGridControl);
        //FwBrowse.renderRuntimeHtml($orderTypeActivityDatesGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeActivityDatesGrid',
            gridSecurityId: 'oMijD9WAL6Bl',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasDelete = true;
                options.hasEdit = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'PoTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
            }
        });
    }

    afterLoad($form: any) {
        const $orderTypeCoverLetterGrid = $form.find('[data-name="OrderTypeCoverLetterGrid"]');
        FwBrowse.search($orderTypeCoverLetterGrid);

        const $orderTypeTermsAndConditionsGrid = $form.find('[data-name="OrderTypeTermsAndConditionsGrid"]');
        FwBrowse.search($orderTypeTermsAndConditionsGrid);

        const $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
        FwBrowse.search($orderTypeActivityDatesGrid);

        $orderTypeActivityDatesGrid.find('.eventType').remove();
    }
}

var POTypeController = new POType();