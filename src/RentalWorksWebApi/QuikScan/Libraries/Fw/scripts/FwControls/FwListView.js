var FwListView = {};
//---------------------------------------------------------------------------------
FwListView.search = function($control, request, funcsearch) {
    request.pageno   = +$control.attr('data-pageno');
    request.pagesize = +$control.attr('data-pagesize');
    $control.data('request', request);
    $control.data('funcsearch', funcsearch);
    funcsearch(request);


    //var _throttleTimer = null;
    //var _throttleDelay = 100;
    //var $window = jQuery(window);
    //var $document = jQuery(document);
    //$window
    //    .off('scroll', ScrollHandler)
    //    .on('scroll', ScrollHandler);
    //function ScrollHandler(e) {
    ////throttle event:
    //    clearTimeout(_throttleTimer);
    //    _throttleTimer = setTimeout(function () {
    //        console.log('scroll');

    //        //do work
    //        if ($window.scrollTop() + $window.height() > $document.height() - 100) {
    //            $control.find('.btnmore').click();
    //        }

    //    }, _throttleDelay);
    //}
}
//---------------------------------------------------------------------------------
FwListView.page = function($control) {
    var request = $control.data('request');
    request.pageno   = +$control.attr('data-pageno');
    request.pagesize = +$control.attr('data-pagesize');
    $control.data('request', request);
    var funcsearch = $control.data('funcsearch');
    funcsearch(request);
}
//---------------------------------------------------------------------------------
FwListView.clear = function($control) {
    $control.find('.items').empty();
};
//---------------------------------------------------------------------------------
FwListView.load = function($control, request, dt, itemtemplate, itemmodel) {
    var html, headerhtml, rowhtml, pagerhtml;
    html = [];
    html.push('<div class="items">');
    if ($control.find('.items').length > 0) {
        html.push($control.find('.items').html());
    }
    if (dt !== null) {
        $control.attr('data-totalpages', dt.TotalPages);
        for (var rowno = 0; rowno < dt.Rows.length; rowno++) {
            //var itemmodel = {};
            //for (var colno = 0; colno < dt.Columns.length; colno++) {
            //    itemmodel['caption' + dt.Columns[colno].DataField] = dt.Columns[colno].Name;
            //}
            for (var colname in dt.ColumnIndex) {
                itemmodel['value' + colname] = dt.Rows[rowno][dt.ColumnIndex[colname]]
            }
            rowhtml = Mustache.render(itemtemplate, itemmodel);
            html.push(rowhtml);
        }
    }
    html.push('</div>');
    //html.push('<div style="background-color:#000000;text-align:center;">Page ' + dt.PageNo + ' of ' + dt.TotalPages + ' (' + dt.TotalRows + ' items)</div>');
    if (dt.PageNo < dt.TotalPages) {
        html.push('<div style="background-color:#000000;text-align:center;">' + dt.PageNo * dt.PageSize + ' of ' + dt.TotalRows + ' items</div>');
        var remaining = dt.TotalRows - (dt.PageNo * dt.PageSize);
        if (dt.PageSize >= remaining) {
            html.push('<div style="text-align:center;font-size:18px;background-color:#aaaaaa;padding:13px 0;color:#000000;"><span class="btnmore">Load last ' + remaining + ' records...</span></div>');
        } else {
            html.push('<div style="text-align:center;font-size:18px;background-color:#aaaaaa;padding:13px 0;color:#000000;"><span class="btnmore">Load next ' + dt.PageSize + ' records...</span></div>');
        }
    } else {
        html.push('<div style="background-color:#000000;text-align:center;">' + dt.TotalRows + ' items</div>');
    }
    //html.push('<div>');
    //html.push('<span class="btnfirstpage">-First-</span>');
    //html.push('<span class="btnprevpage">-Prev-</span>');
    //html.push('<span class="btnnextpage">-Next-</span>');
    //html.push('<span class="btnlastpage">-Last-</span>');
    //html.push('<div>');
    $control.html(html.join('\n'));
    $control
        .off('click', '.btnmore')
        .on('click', '.btnmore', function() {
            var pageno     = +$control.attr('data-pageno') + 1;
            var totalpages = +$control.attr('data-totalpages');
            if (pageno <= totalpages) {
                $control.attr('data-pageno', pageno);
                FwListView.page($control);
            }
        })
        
        .off('click', '.btnfirstpage')
        .on('click', '.btnfirstpage', function() {
            var pageno = +$control.attr('data-pageno');
            if (pageno != 1) {
                $control.attr('data-pageno', 1);
                FwListView.page($control);
            }
        })

        .off('click', '.btnprevpage')
        .on('click', '.btnprevpage', function() {
            var pageno = +$control.attr('data-pageno') - 1;
            if (pageno >= 1) {
                $control.attr('data-pageno', pageno);
                FwListView.page($control);
            }
        })

        .off('click', '.btnnextpage')
        .on('click', '.btnnextpage', function() {
            var pageno     = +$control.attr('data-pageno') + 1;
            var totalpages = +$control.attr('data-totalpages');
            if (pageno <= totalpages) {
                $control.attr('data-pageno', pageno);
                FwListView.page($control);
            }
        })

        .off('click', '.btnlastpage')
        .on('click', '.btnlastpage', function() {
            var currentpageno = +$control.attr('data-pageno');
            var pageno        = +$control.attr('data-totalpages');
            if (currentpageno != pageno) {
                $control.attr('data-pageno', pageno);
                FwListView.page($control);
            }
        })
    ;
}
//---------------------------------------------------------------------------------