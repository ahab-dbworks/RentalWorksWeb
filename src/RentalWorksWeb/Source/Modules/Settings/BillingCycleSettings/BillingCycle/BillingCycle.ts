class BillingCycle {
    Module: string = 'BillingCycle';
    apiurl: string = 'api/v1/billingcycle';
    caption: string = Constants.Modules.Settings.children.BillingCycleSettings.children.BillingCycle.caption;
    nav: string = Constants.Modules.Settings.children.BillingCycleSettings.children.BillingCycle.nav;
    id: string = Constants.Modules.Settings.children.BillingCycleSettings.children.BillingCycle.id;
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

        $form.find('div[data-datafield="BillingCycleType"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);

            if ($this.prop('checked') === true && $this.val() === "EVENTS") {
                $form.find(".eventstab").show();
                FwFormField.disable($form.find('[data-datafield="ProrateMonthly"]'));
            } else if ($this.prop('checked') === true && ($this.val() === "MONTHLY" || $this.val() === "CALMONTH")) {
                FwFormField.enable($form.find('[data-datafield="ProrateMonthly"]'));
            } else {
                FwFormField.disable($form.find('[data-datafield="ProrateMonthly"]'));
                $form.find(".eventstab").hide();
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BillingCycleId"] input').val(uniqueids.BillingCycleId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        //const $billingCycleEventsGrid = $form.find('div[data-grid="BillingCycleEventsGrid"]');
        //const $billingCycleEventsGridControl = FwBrowse.loadGridFromTemplate('BillingCycleEventsGrid');
        //$billingCycleEventsGrid.empty().append($billingCycleEventsGridControl);
        //$billingCycleEventsGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        BillingCycleId: FwFormField.getValueByDataField($form, 'BillingCycleId')
        //    };
        //});
        //FwBrowse.init($billingCycleEventsGridControl);
        //FwBrowse.renderRuntimeHtml($billingCycleEventsGridControl);
    //-------------
        FwBrowse.renderGrid({
            nameGrid: 'BillingCycleEventsGrid',
            gridSecurityId: 'KSA8EsXjcrt',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    BillingCycleId: FwFormField.getValueByDataField($form, 'BillingCycleId')
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $billingCycleEventsGrid = $form.find('[data-name="BillingCycleEventsGrid"]');
        FwBrowse.search($billingCycleEventsGrid);

        const radioType = FwFormField.getValueByDataField($form, 'BillingCycleType');
        if (radioType === "EVENTS") {
            $form.find(".eventstab").show();
        } else if (radioType === "MONTHLY" || radioType === "CALMONTH") {
            FwFormField.enable($form.find('[data-datafield="ProrateMonthly"]'));
        } else {
            $form.find(".eventstab").hide();
        }
    }
}
//----------------------------------------------------------------------------------------------
var BillingCycleController = new BillingCycle();