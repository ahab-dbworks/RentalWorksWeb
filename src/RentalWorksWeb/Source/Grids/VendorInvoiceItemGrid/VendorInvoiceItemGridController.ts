class VendorInvoiceItemGrid {
    Module: string = 'VendorInvoiceItemGrid';
    apiurl: string = 'api/v1/vendorinvoiceitem';
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const orderId = FwBrowse.getValueByDataField($control, $tr, 'OrderId');
            if (orderId != '') {
                const $browsecontextmenu = $tr.find('.browsecontextmenu');
                $browsecontextmenu.data('contextmenuoptions', $tr => {
                    //View Corresponding Deal Invoices
                    FwContextMenu.addMenuItem($browsecontextmenu, `View Corresponding Deal Invoices`, () => {
                        try {
                            this.renderInvoicePopup($control, $tr);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    $browsecontextmenu.find('.viewcorrespondingdealinvoicesoption').css('white-space', 'break-spaces');
                });
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'GlAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateglaccount`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    renderInvoicePopup($control: JQuery, $tr: JQuery) {
        let HTML: Array<string> = [], $popupHtml, $popup;
        HTML.push(
            `<div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
            <div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form" style="margin-top:2.5em;">
                <div class="wideflexrow">
                   <div data-control="FwGrid" data-grid="VendorInvoiceItemCorrespondingDealInvoicesGrid" data-securitycaption=""></div>
                 </div>
            </div>`);
        $popupHtml = HTML.join('');
        $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
        FwPopup.showPopup($popup);

        const $grid = FwBrowse.renderGrid({
            nameGrid: `VendorInvoiceItemCorrespondingDealInvoicesGrid`,
            gridSecurityId: '06qm2k830e7D',
            moduleSecurityId: '06qm2k830e7D',
            $form: $popup,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwBrowse.getValueByDataField($control, $tr, 'OrderId'),
                    BillingStartDate: FwBrowse.getValueByDataField($control, $tr, 'PoItemBillingStartDate'),
                    BillingEndDate: FwBrowse.getValueByDataField($control, $tr, 'PoItemBillingEndDate')
                };
            }
        });

        FwBrowse.search($grid);

        // Close modal
        $popup.find('.close-modal').one('click', e => {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var VendorInvoiceItemGridController = new VendorInvoiceItemGrid();