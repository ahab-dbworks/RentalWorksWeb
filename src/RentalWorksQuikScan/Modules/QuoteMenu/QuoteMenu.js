﻿RwQuoteMenu = {};
//----------------------------------------------------------------------------------------------
RwQuoteMenu.getQuoteMenuScreen = function(viewModel, properties) {
    var screen, combinedViewModel, $search, $addnew, $records, $neworder;
    combinedViewModel = jQuery.extend({
        captionPageTitle:        RwLanguages.translate('QuikPick'),
        captionPageSubTitle:     RwLanguages.translate('')
    }, viewModel);
    combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-quoteMenu').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    screen.$view = FwMobileMasterController.getMasterView(combinedViewModel);

    $fwcontrols = screen.$view.find('.fwcontrol');
    FwControl.renderRuntimeControls($fwcontrols);

    $search   = screen.$view.find('.qm-search');
    $neworder = screen.$view.find('.qm-newquote');

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
        service:   'QuoteMenu',
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
                screen.loadquote(recorddata.orderid, recorddata.orderno, recorddata.orderdesc, recorddata.ordertype);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }
    });
    $search.showscreen = function() {
        $search.show();
        $search.find('#quotesearch').fwmobilesearch('search');
        program.setScanTarget('.qm-search .fwmobilesearch .searchbox');
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
            var $this, $deal;

            $this = jQuery(this);
            $deal = $neworder.find('div[data-datafield="deal"]');
            if ($this.val() == 'Q') {
                $deal.attr('data-required', false);
            } else {
                $deal.attr('data-required', true);
            }
        })
        .on('change', 'div[data-datafield="startdate"] input', function() {
            var $this, $enddate;

            $this = jQuery(this);
            $enddate = $neworder.find('div[data-datafield="enddate"] input');
            if ($this.val() > $enddate.val()) {
                $enddate.val($this.val()).change();
            }
        })
        .on('change', 'div[data-datafield="enddate"] input', function() {
            var $this, $startdate;

            $this = jQuery(this);
            $startdate = $neworder.find('div[data-datafield="startdate"] input');
            if ($startdate.val() > $this.val()) {
                $this.val($startdate.val()).change();
            }
        })
    ;
    $neworder.showscreen = function() {
        var html = [], $newbody, applicationOptions, ratevalues = [];
        $neworder.show();
        applicationOptions = program.getApplicationOptions();

        html.push('<div class="qm-newquote-title">New Quote/Order</div>');
        html.push('<div class="qm-newquote-fields">');
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
        $newbody = jQuery(html.join(''));

        FwControl.renderRuntimeControls($newbody.find('.fwcontrol'));

        FwFormField.loadItems($newbody.find('div[data-datafield="selecttype"]'), [
            {value:'Q',     text:'Quote'},
            {value:'O',     text:'Order'}
        ], true);

        RwServices.callMethod("QuoteMenu", "GetDeals", {}, function(response) {
            var dealvalues = []
            for (var i = 0; i < response.deals.length; i++) {
                dealvalues.push({value: response.deals[i].dealid, text: response.deals[i].deal});
            }
            FwFormField.loadItems($newbody.find('div[data-datafield="deal"]'), dealvalues, false);
        });

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
        var request;
        if ($neworder.validate()) {
            request = {
                orderdesc:   FwFormField.getValue($neworder, 'div[data-datafield="description"]'),
                estrentfrom: FwFormField.getValue($neworder, 'div[data-datafield="startdate"]'),
                estfromtime: '',
                estrentto:   FwFormField.getValue($neworder, 'div[data-datafield="enddate"]'),
                esttotime:   '',
                dealid:      FwFormField.getValue($neworder, 'div[data-datafield="deal"]'),
                ratetype:    FwFormField.getValue($neworder, 'div[data-datafield="rate"]'),
                ordertype:   FwFormField.getValue($neworder, 'div[data-datafield="selecttype"]')
            };
            RwServices.callMethod("QuoteMenu", "NewQuote", request, function(response) {
                try {
                    $neworder.hidescreen();
                    screen.loadquote(response.order.orderid, response.order.orderno, response.order.orderdesc, '');
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

            if (($field.attr('data-required') == 'true') && ($field.attr('data-enabled') == 'true')) {
                if ($field.find('.fwformfield-value').val() == '') {
                    isvalid = false;
                    $field.addClass('error');
                }
            }
            if ($field.hasClass('error')) {
                isvalid = false;
            }
            if (isvalid) {
                $field.removeClass('error');
            }
        })

        return isvalid;
    };

    screen.loadquote = function(orderid, orderno, description, ordertype) {
        var quoteScreen_properties;
        quoteScreen_properties = {
            orderid:     orderid,
            orderno:     orderno,
            description: description,
            ordertype:   ordertype
        };
        program.pushScreen(RwQuote.getQuoteScreen({}, quoteScreen_properties));
    };

    screen.load = function() {
        $search.showscreen();
    };

    return screen;
};