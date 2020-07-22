QuikPick = {};
//----------------------------------------------------------------------------------------------
QuikPick.getQuikPickScreen = function() {
    var viewModel = {
        captionPageTitle:        RwLanguages.translate('QuikPick')
        //captionPageSubTitle:     RwLanguages.translate('')
    };
    viewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-QuikPick').html(), viewModel);
    var screen = {};
    screen.$view = FwMobileMasterController.getMasterView(viewModel);

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    var $search   = screen.$view.find('.qp-search');
    var $neworder = screen.$view.find('.qp-newquote');

    $search.find('#searchcontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'New',
                orientation: 'right',
                icon:        '&#xE145;', //add
                state:       0,
                buttonclick: function () {
                    $search.hide();
                    $neworder.showscreen();
                }
            }
        ]
    });
    $search.find('#quotesearch').fwmobilesearch({
        service:   'QuikPick',
        method:    'QuoteSearch',
        searchModes: [
            { value: 'QUOTE',       caption: 'Quote/Order No' },
            { value: 'DESCRIPTION', caption: 'Description' }
        ],
        cacheItemTemplate: false,
        itemTemplate: function(model) {
            var html = [];
            html.push('<div class="record order">');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Desc:</div>');
            html.push('    <div class="value desc">{{orderdesc}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Order:</div>');
            html.push('    <div class="value orderno">{{orderno}}</div>');
            html.push('    <div class="caption dynamic">Date:</div>');
            html.push('    <div class="value date">{{orderdate}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Deal:</div>');
            html.push('    <div class="value deal">{{deal}}</div>');
            html.push('  </div>');
            html.push('  <div class="row">');
            html.push('    <div class="caption fixed">Start Date:</div>');
            html.push('    <div class="value startdate">{{estrentfrom}}</div>');
            html.push('    <div class="caption dynamic">End Date:</div>');
            html.push('    <div class="value enddate">{{estrentto}}</div>');
            html.push('  </div>');
            html.push('</div>');
            html = html.join('\n');
            return html;
        },
        recordClick: function(recorddata) {
            try {
                screen.loadquote(recorddata.orderid, recorddata.orderno, recorddata.orderdesc, recorddata.ordertype, recorddata.warehouseid);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }
    });
    $search.showscreen = function() {
        $search.show();
        $search.find('#quotesearch').fwmobilesearch('search');
        program.setScanTarget('.qp-search .fwmobilesearch .searchbox');
    };

    $neworder.find('#newquotecontrol').fwmobilemodulecontrol({
        buttons: [
            {
                caption:     'Back',
                orientation: 'left',
                icon:        '&#xE5CB;', //arrow_back
                state:       0,
                buttonclick: function () {
                    try {
                        $search.show();
                        $neworder.hidescreen();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            },
            {
                caption:     'Continue',
                orientation: 'right',
                icon:        '&#xE5CC;', //arrow_forward
                state:       0,
                buttonclick: function () {
                    try {
                        $neworder.createquoteorder();
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            }
        ]
    });
    $neworder
        .on('change', 'div[data-datafield="selecttype"] .fwformfield-value', function() {
            var $this = jQuery(this);
            var $deal = $neworder.find('div[data-datafield="deal"]');
            if ($this.val() == 'Q') {
                $deal.attr('data-required', false);
            } else {
                $deal.attr('data-required', true);
            }
        })
        .on('change', 'div[data-datafield="startdate"] input', function() {
            var $this = jQuery(this);
            var $enddate = $neworder.find('div[data-datafield="enddate"] input');
            if ($this.val() > $enddate.val()) {
                $enddate.val($this.val()).change();
            }
        })
        .on('change', 'div[data-datafield="enddate"] input', function() {
            var $this = jQuery(this);
            var $startdate = $neworder.find('div[data-datafield="startdate"] input');
            if ($startdate.val() > $this.val()) {
                $this.val($startdate.val()).change();
            }
        })
    ;
    $neworder.showscreen = function() {
        $neworder.show();
        var applicationOptions = program.getApplicationOptions();

        var html = [];
        html.push('<div class="qp-newquote-title">New Quote/Order</div>');
        html.push('<div class="qp-newquote-fields">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Select QuikPick Type" data-type="select" data-datafield="selecttype" data-required="true" />');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Deal" data-type="select" data-datafield="deal" />');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Description" data-type="text" data-datafield="description" data-required="true" />');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Estimated Start Date" data-type="date" data-datafield="startdate" data-required="true" />');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Estimated End Date" data-type="date" data-datafield="enddate" />');
        html.push('  </div>');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption="Rate" data-type="select" data-datafield="rate" />');
        html.push('  </div>');
        html.push('</div>');
        var $newbody = jQuery(html.join(''));

        FwControl.renderRuntimeControls($newbody.find('.fwcontrol'));

        FwFormField.loadItems($newbody.find('div[data-datafield="selecttype"]'), [
            {value:'Q',     text:'Quote'},
            {value:'O',     text:'Order'}
        ], true);

        RwServices.callMethod("QuikPick", "GetDeals", {}, function(response) {
            var dealvalues = []
            for (var i = 0; i < response.deals.length; i++) {
                dealvalues.push({value: response.deals[i].dealid, text: response.deals[i].deal});
            }
            FwFormField.loadItems($newbody.find('div[data-datafield="deal"]'), dealvalues, false);
        });

        var ratevalues = [];
        ratevalues.push({value:'DAILY',    text:'Daily Rate'});
        ratevalues.push({value:'WEEKLY',   text:'Weekly Rate'});
        ratevalues.push({value:'MONTHLY',  text:'Monthly Rate'});
        if ((typeof applicationOptions.tieredpricing !== 'undefined') && (applicationOptions.tieredpricing.enabled)) ratevalues.push({value:'TIEREDWK', text:'Tiered Week Rate'});
        FwFormField.loadItems($newbody.find('div[data-datafield="rate"]'), ratevalues, true);

        FwFormField.setValue($newbody, 'div[data-datafield="startdate"]', FwFunc.getDate());

        $neworder.find('#newquoteform').append($newbody);
    };
    $neworder.hidescreen = function() {
        $neworder.hide();
        $neworder.find('#newquoteform').empty();
    };
    $neworder.createquoteorder = function() {
        if ($neworder.validate()) {
            var request = {
                orderdesc:   FwFormField.getValue($neworder, 'div[data-datafield="description"]'),
                estrentfrom: FwFormField.getValue($neworder, 'div[data-datafield="startdate"]'),
                estfromtime: '',
                estrentto:   FwFormField.getValue($neworder, 'div[data-datafield="enddate"]'),
                esttotime:   '',
                dealid:      FwFormField.getValue($neworder, 'div[data-datafield="deal"]'),
                ratetype:    FwFormField.getValue($neworder, 'div[data-datafield="rate"]'),
                ordertype:   FwFormField.getValue($neworder, 'div[data-datafield="selecttype"]')
            };
            RwServices.callMethod("QuikPick", "NewQuote", request, function(response) {
                try {
                    $neworder.hidescreen();
                    screen.loadquote(response.order.orderid, response.order.orderno, response.order.orderdesc, response.order.ordertype, response.order.warehouseid);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
        }
    };
    $neworder.validate = function() {
        var $fields, isvalid = true;
        $fields = $neworder.find('.fwformfield');
        $fields.each(function(index) {
            var $field = jQuery(this);
            $field.removeClass('error');

            if (($field.attr('data-required') == 'true') && ($field.attr('data-enabled') == 'true')) {
                if ($field.find('.fwformfield-value').val() == '') {
                    isvalid = false;
                    $field.addClass('error');
                }
            }
            if ($field.hasClass('error')) {
                isvalid = false;
            }
        })

        return isvalid;
    };

    screen.loadquote = function(orderid, orderno, description, ordertype, warehouseid) {
        var quoteScreen_properties = {
            orderid:     orderid,
            orderno:     orderno,
            description: description,
            ordertype:   ordertype,
            warehouseid: warehouseid
        };
        program.pushScreen(RwQuote.getQuoteScreen({}, quoteScreen_properties));
    };

    screen.load = function () {
        program.onScanBarcode = function (barcode, barcodeType) {
            try {
                screen.scanCode(barcode);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }
        $search.showscreen();
    };

    screen.unload = function () {
        program.onScanBarcode = null
        $search.showscreen();
    };

    screen.scanCode = (barcode, barcodeType) => {
        $search.find('#quotesearch').fwmobilesearch('setsearchmode', 'QUOTE');
        $search.find('#quotesearch').fwmobilesearch('setSearchText', barcode, true);
    }

    return screen;
};