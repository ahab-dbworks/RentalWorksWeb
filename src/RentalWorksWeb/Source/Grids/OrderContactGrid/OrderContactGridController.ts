﻿class OrderContactGrid {
    Module: string = 'OrderContactGrid';
    apiurl: string = 'api/v1/ordercontact';

    generateRow($control, $generatedtr) {
        let $form;
        $form = $control.closest('.fwform');

        $generatedtr.find('div[data-browsedatafield="ContactId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.value').val($tr.find('.field[data-browsedatafield="ContactTitleId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="CompanyId"] input').val($form.find('div[data-datafield="DealId"]').attr('data-originalvalue'));
        });
    };

    addLegend($control) {
        let $form;
        $form = $control.closest('.fwform');

        const controller = $form.attr('data-controller');
        switch (controller) {
            case 'QuoteController':
                FwBrowse.addLegend($control, 'Quoted For', '#00c400');
                break;
            case 'PurchaseOrderController':
                FwBrowse.addLegend($control, 'Ordered From', '#00c400');
                break;
            default:
                FwBrowse.addLegend($control, 'Ordered By', '#00c400');
                break;
        }
    }
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontact`);
                break;
            case 'ContactTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontacttitle`);
                break;
        }
    }
}

var OrderContactGridController = new OrderContactGrid();
//----------------------------------------------------------------------------------------------