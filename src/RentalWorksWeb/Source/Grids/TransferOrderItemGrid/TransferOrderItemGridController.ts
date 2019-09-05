﻿class TransferOrderItemGrid {
     Module: string = 'TransferOrderItemGrid';
     apiurl: string = 'api/v1/orderitem';

     generateRow($control, $generatedtr) {
         const $form = $control.closest('.fwform');
         $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
             const fromWarehouseId = FwFormField.getValueByDataField($form, 'FromWarehouseId');
             const toWarehouseId = FwFormField.getValueByDataField($form, 'ToWarehouseId');
             $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
             $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
             //$generatedtr.find('.field[data-browsedatafield="WarehouseId"]').attr('data-originalvalue', fromWarehouseId);
             //$generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"]').attr('data-originalvalue', toWarehouseId);
             $generatedtr.find('.field[data-browsedatafield="WarehouseId"]').text(FwFormField.getTextByDataField($form, 'FromWarehouseId'));
             $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"]').text(FwFormField.getTextByDataField($form, 'ToWarehouseId'));

             //adds warehouse values
             const beforeSave = $control.data('beforesave');
             if (typeof beforeSave == 'function') {
                 $control.data('beforesave', request => {
                     beforeSave(request);
                     request.WarehouseId = fromWarehouseId;
                     request.ReturnToWarehouseId = toWarehouseId;
                 });
             }
         });
     }

     beforeValidateItem = function ($browse, $grid, request, datafield, $tr) {
         const availFor = $grid.find('.active [data-grid="TransferOrderItemGrid"]');

         if (availFor.hasClass('R')) {
             request.uniqueids = {
                 AvailFor: 'R'
             };
         } else if (availFor.hasClass('S')) {
             request.uniqueids = {
                 AvailFor: 'S'
             };
         }
     };
}
//-----------------------------------------------------------------------------------------------------
//QuikSearch
FwApplicationTree.clickEvents[Constants.Grids.TransferOrderItemGrid.menuItems.QuikSearch.id] = function (event: JQuery.ClickEvent) {
    try {
        const $form = jQuery(event.currentTarget).closest('.fwform');

        if ($form.attr('data-mode') === 'NEW') {
            TransferOrderController.saveForm($form, { closetab: false });
            return;
        }

        const transferId = FwFormField.getValueByDataField($form, 'TransferId');
        if (transferId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            let search = new SearchInterface();
            search.renderSearchPopup($form, transferId, 'Transfer');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
////Refresh Availability
//FwApplicationTree.clickEvents[Constants.Grids.TransferOrderItemGrid.menuItems.RefreshAvailability.id] = function (e: JQuery.ClickEvent) {
//    const $transferOrderItemGrid = jQuery(this).closest('[data-name="TransferOrderItemGrid"]');
//    let recType;
//    recType = jQuery(this).closest('[data-grid="TransferOrderItemGrid"]');
//    if (recType.hasClass('R')) {
//        recType = 'R';
//    } else if (recType.hasClass('S')) {
//        recType = 'S';
//    }
//    //else if (recType.hasClass('L')) {
//    //    recType = 'L';
//    //} else if (recType.hasClass('M')) {
//    //    recType = 'M';
//    //} else if (recType.hasClass('P')) {
//    //    recType = 'P';
//    //} else if (recType.hasClass('A')) {
//    //    recType = '';
//    //} else if (recType.hasClass('RS')) {
//    //    recType = 'RS'
//    //}

//    const pageNumber = $transferOrderItemGrid.attr('data-pageno');
//    const onDataBind = $transferOrderItemGrid.data('ondatabind');
//    if (typeof onDataBind == 'function') {
//        $transferOrderItemGrid.data('ondatabind', function (request) {
//            onDataBind(request);
//            request.uniqueids.RefreshAvailability = true;
//            request.pageno = parseInt(pageNumber);
//        });
//    }

//    FwBrowse.search($transferOrderItemGrid);
//    $transferOrderItemGrid.attr('data-pageno', pageNumber);
//    //resets ondatabind
//    $transferOrderItemGrid.data('ondatabind', onDataBind);

//    jQuery(document).trigger('click');
//}
////----------------------------------------------------------------------------------------------
//Copy Template
FwApplicationTree.clickEvents[Constants.Grids.TransferOrderItemGrid.menuItems.CopyTemplate.id] = function (e: JQuery.ClickEvent) {
    const $form = jQuery(this).closest('.fwform');
    const $grid = jQuery(this).closest('[data-name="TransferOrderItemGrid"]');
    let recType;
    recType = jQuery(this).closest('[data-grid="TransferOrderItemGrid"]');
    if (recType.hasClass('R')) {
        recType = 'R';
    } else if (recType.hasClass('S')) {
        recType = 'S';
    } else if (recType.hasClass('L')) {
        recType = 'L';
    } else if (recType.hasClass('M')) {
        recType = 'M';
    } else if (recType.hasClass('P')) {
        recType = 'P';
    } else if (recType.hasClass('A')) {
        recType = '';
    } else if (recType.hasClass('RS')) {
        recType = 'RS'
    }
    let module = $form.attr('data-controller').replace('Controller', '');
    const HTML: Array<string> = [];
    HTML.push(
        `<div class="fwcontrol fwcontainer fwform popup template-popup" data-control="FwContainer" data-type="form">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:5px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:10px;">Copy Template to Transfer</div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Add to Transfer</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
    );

    const addTemplateBrowse = () => {
        let $browse = FwBrowse.loadBrowseFromTemplate('Template');
        $browse.attr('data-hasmultirowselect', 'true');
        $browse = FwModule.openBrowse($browse);
        $browse.find('.fwbrowse-menu').hide();

        $browse.data('ondatabind', function (request) {
            request.pagesize = 20;
            request.orderby = "Description asc";
        });
        return $browse;
    }
    const $popupHtml = HTML.join('');
    const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
    FwPopup.showPopup($popup);
    const $templateBrowse = addTemplateBrowse();
    $popup.find('.container').append($templateBrowse);
    const $templatePopup = $popup.find('.template-popup');
    $popup.find('.close-modal').one('click', e => {
        FwPopup.destroyPopup($popup);
        jQuery(document).find('.fwpopup').off('click');
        jQuery(document).off('keydown');
    });

    $popup.on('click', '.select-items', e => {
        const $selectedCheckBoxes = $popup.find('[data-control="FwGrid"] tbody .cbselectrow:checked');
        let templateIds: Array<string> = [];
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let $this = jQuery($selectedCheckBoxes[i]);
            let id;
            id = $this.closest('tr').find('div[data-browsedatafield="TemplateId"]').attr('data-originalvalue');
            templateIds.push(id);
        };

        let request: any = {};
        request = {
            TemplateIds: templateIds
            , RecType: recType
            , OrderId: FwFormField.getValueByDataField($form, `TransferId`)
        }

        FwAppData.apiMethod(true, 'POST', `api/v1/order/copytemplate`, request, FwServices.defaultTimeout, function onSuccess(response) {
            $popup.find('.close-modal').click();
            FwBrowse.search($grid);
        }, null, $templatePopup);

    });

    FwBrowse.search($templateBrowse);
};
//----------------------------------------------------------------------------------------------
var TransferOrderItemGridController = new TransferOrderItemGrid();