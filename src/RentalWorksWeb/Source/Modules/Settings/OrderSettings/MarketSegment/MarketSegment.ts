//routes.push({ pattern: /^module\/marketsegment$/, action: function (match: RegExpExecArray) { return MarketSegmentController.getModuleScreen(); } });

class MarketSegment {
    Module: string = 'MarketSegment';
    apiurl: string = 'api/v1/marketsegment';
    caption: string = Constants.Modules.Settings.children.OrderSettings.children.MarketSegment.caption;
    nav: string = Constants.Modules.Settings.children.OrderSettings.children.MarketSegment.nav;
    id: string = Constants.Modules.Settings.children.OrderSettings.children.MarketSegment.id;
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
        $form.find('div.fwformfield[data-datafield="MarketSegmentId"] input').val(uniqueids.MarketSegmentId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    renderGrids($form: any) {
        //const marketSegmentJobGrid = $form.find('div[data-grid="MarketSegmentJobGrid"]');
        //const $marketSegmentJobGridControl = FwBrowse.loadGridFromTemplate('MarketSegmentJobGrid');
        //marketSegmentJobGrid.empty().append($marketSegmentJobGridControl);
        //$marketSegmentJobGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        MarketSegmentId: FwFormField.getValueByDataField($form, 'MarketSegmentId')
        //    }
        //});
        //$marketSegmentJobGridControl.data('beforesave', request => {
        //    request.MarketSegmentId = FwFormField.getValueByDataField($form, 'MarketSegmentId');
        //})
        //FwBrowse.init($marketSegmentJobGridControl);
        //FwBrowse.renderRuntimeHtml($marketSegmentJobGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'MarketSegmentJobGrid',
            gridSecurityId: 'OWZGrnUnJHon',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    MarketSegmentId: FwFormField.getValueByDataField($form, 'MarketSegmentId'),
                };
            },
            beforeSave: (request: any) => {
                request.MarketSegmentId = FwFormField.getValueByDataField($form, 'MarketSegmentId');
            },
        });
    }


    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
        const $marketSegmentJobGrid = $form.find('[data-name="MarketSegmentJobGrid"]');
        FwBrowse.search($marketSegmentJobGrid);
    }
    //--------------------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
    switch (datafield) {
        case 'MarketTypeId':
            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemarkettype`);
            break;
    }
}
}

var MarketSegmentController = new MarketSegment();