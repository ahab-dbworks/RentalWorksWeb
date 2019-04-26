/* Copyright 2005 - 2014 Annpoint, s.r.o.
 Use of this software is subject to license terms. 
 http://www.daypilot.org/
 */

if (typeof DayPilot === 'undefined') {
    var DayPilot = {};
}

if (typeof DayPilot.Global === 'undefined') {
    DayPilot.Global = {};
}

(function() {
    

    if (typeof DayPilot.$ !== 'undefined') {
        return;
    }

    DayPilot.$ = function(id) {
        return document.getElementById(id);
    };

    // mouse offset relative to the positioned element (FF) or relative to element that fired the event (IE)
    // deprecated, replaced by DayPilot.mo2
    // still being used in Month.js, replace carefully
    DayPilot.mo = function(t, ev) {
        ev = ev || window.event;

        if (ev.layerX) {  // Mozilla and others
            var coords = {x: ev.layerX, y: ev.layerY};
            if (!t) {
                return coords;
            }
            return coords;
            //while 

        }
        // this has to be first because IE9 supports layerX but it is not consistent with Mozilla
        if (ev.offsetX) { // IE
            return {x: ev.offsetX, y: ev.offsetY};
        }

        return null;
    };

    DayPilot.isKhtml = (navigator && navigator.userAgent && navigator.userAgent.indexOf("KHTML") !== -1);
    DayPilot.isIE = (navigator && navigator.userAgent && navigator.userAgent.indexOf("MSIE") !== -1);
    //DayPilot.isIE7 = (navigator && navigator.userAgent && navigator.userAgent.indexOf("MSIE 7") !== -1);
    //DayPilot.isIE8 = (navigator && navigator.userAgent && navigator.userAgent.indexOf("MSIE 8") !== -1);
    DayPilot.isIEQuirks = DayPilot.isIE && (document.compatMode && document.compatMode === "BackCompat");
    
    DayPilot.browser = {};
    //DayPilot.browser.ie9 = (navigator && navigator.userAgent && navigator.userAgent.indexOf("MSIE 9") !== -1);  // IE
    DayPilot.browser.ie8 = (function() {
        var div = document.createElement("div");
        div.innerHTML = "<!--[if IE 8]><i></i><![endif]-->";
        var result = (div.getElementsByTagName("i").length === 1);
        return result;
    })();
    DayPilot.browser.ie9 = (function() {
        var div = document.createElement("div");
        div.innerHTML = "<!--[if IE 9]><i></i><![endif]-->";
        var result = (div.getElementsByTagName("i").length === 1);
        return result;
    })();
    DayPilot.browser.ielt9 = (function() {
        var div = document.createElement("div");
        div.innerHTML = "<!--[if lt IE 9]><i></i><![endif]-->";
        var result = (div.getElementsByTagName("i").length === 1);
        return result;
    })();    
    
    
    DayPilot.touch = {};
    DayPilot.touch.start = window.navigator.msPointerEnabled ? "MSPointerDown" : "touchstart";
    DayPilot.touch.move = window.navigator.msPointerEnabled ? "MSPointerMove" : "touchmove";
    DayPilot.touch.end = window.navigator.msPointerEnabled ? "MSPointerUp" : "touchend";
    
    DayPilot.mo2 = function(target, ev) {
        ev = ev || window.event;

        // IE
        if (typeof (ev.offsetX) !== 'undefined') {

            var coords = {x: ev.offsetX + 1, y: ev.offsetY + 1};

            if (!target) {
                return coords;
            }

            var current = ev.srcElement;
            while (current && current !== target) {
                if (current.tagName !== 'SPAN') { // hack for DayPilotMonth/IE, hour info on the right side of an event
                    coords.x += current.offsetLeft;
                    if (current.offsetTop > 0) {  // hack for http://forums.daypilot.org/Topic.aspx/879/move_event_bug
                        coords.y += current.offsetTop - current.scrollTop;
                    }
                }

                current = current.offsetParent;
            }

            if (current) {
                return coords;
            }
            return null;
        }

        // FF
        if (typeof (ev.layerX) !== 'undefined') {

            var coords = {x: ev.layerX, y: ev.layerY, src: ev.target};

            if (!target) {
                return coords;
            }
            var current = ev.target;

            // find the positioned offsetParent, the layerX reference
            while (current && current.style.position !== 'absolute' && current.style.position !== 'relative') {
                current = current.parentNode;
                if (DayPilot.isKhtml) { // hack for KHTML (Safari and Google Chrome), used in DPC/event moving
                    coords.y += current.scrollTop;
                }
            }

            while (current && current !== target) {
                coords.x += current.offsetLeft;
                coords.y += current.offsetTop - current.scrollTop;
                current = current.offsetParent;
            }
            if (current) {
                return coords;
            }

            return null;
        }

        return null;
    };

    // mouse offset relative to the specified target
    DayPilot.mo3 = function(target, ev) {
        ev = ev || window.event;

        var page = DayPilot.page(ev);
        if (page) {
            var abs = DayPilot.abs(target);
            return {x: page.x - abs.x, y: page.y - abs.y};
        }

        return DayPilot.mo2(target, ev);
    };

    // mouse coords
    DayPilot.mc = function(ev) {
        if (ev.pageX || ev.pageY) {
            return {x: ev.pageX, y: ev.pageY};
        }
        return {
            x: ev.clientX + document.documentElement.scrollLeft,
            y: ev.clientY + document.documentElement.scrollTop
        };
    };

    DayPilot.Queue = function() {
        var q = this;
        this.items = [];
        this.index = 0;
        this.add = function(f) {
            q.items.push(f);
            q.testNext();
        };
        this.testNext = function() {
            var f = q.items[q.index];
            if (typeof f === 'function') {
                setTimeout(q.doNext);
            }
            else {
                q.clear();
                setTimeout(q.testNext, 100);
            }
        };
        this.doNext = function() {
            var f = q.items[q.index];
            if (typeof f === 'function') {
                f();
                q.index += 1;
            }
            q.testNext();
        };
        this.clear = function() {
            if (q.items.length === 0) {
                return;
            }
            q.items = [];
            q.index = 0;
        };
    };

    DayPilot.dynlist = function(array) {
        var result = {};

        result.each = function(f) {
            if (!f) {
                return;
            }
            for (var i = 0; i < array.length; i++) {
                var getItemProcessor = function(i) {
                    return function() {
                        f(i);
                    }
                }
                setTimeout(getItemProcessor(array[i]));
            }
        };

        result.seqEach = function(f) {
            if (!f) {
                return;
            }

            process(0);

            function process(i) {
                if (i >= array.length) {
                    return;
                }
                f(array[i]);

                setTimeout(process(i+1));
            }
        };

        return result;
    };

    DayPilot.list = function(array) {
        var list = [];

        list.each = function(f) {
            if (!f) {
                return;
            }
            for (var i = 0; i < this.length; i++) {
                f(list[i]);
            }
        };

        list.addProps = function(fields) {
            var result = DayPilot.list(this);
            if (fields) {
                for (var name in fields) {
                    result[name] = fields[name];
                }
            }
            return result;
        };

        list.map = function(callback, thisArg) {
            var T, A, k;
            if (this == null) {
                throw new TypeError(' this is null or not defined');
            }

            var O = Object(this);
            var len = O.length >>> 0;
            if (typeof callback !== 'function') {
                throw new TypeError(callback + ' is not a function');
            }
            if (arguments.length > 1) {
                T = thisArg;
            }
            A = DayPilot.list();
            k = 0;
            while (k < len) {
                var kValue, mappedValue;
                if (k in O) {
                    kValue = O[k];
                    mappedValue = callback.call(T, kValue, k, O);
                    A[k] = mappedValue;
                }
                k++;
            }
            return A;
        };

        /*
        if (!array || array.length === 0) {
            return list;
        }
        for (var i = 0; i < array.length; i++) {
            list.push(array[i]);
        }
        */
        if (DayPilot.isArray(array)) {
            for (var i = 0; i < array.length; i++) {
                list.push(array[i]);
            }
        }
        else if (typeof array === 'object') {
            list.push(array);
        }

        return list;
    };

    DayPilot.line = function (x1, y1, x2, y2, arrow) {
        var source = { x: x1, y: y1 };
        var target = { x: x2, y: y2, "deg": DayPilot.deg(x1, y1, x2, y2)};

        if (y1 < y2){
            var pom = y1;
            y1 = y2;
            y2 = pom;
            pom = x1;
            x1 = x2;
            x2 = pom;
        }

        var deg = DayPilot.deg(x1, y1, x2, y2);

        var width = (function() {
            var a = Math.abs(x1-x2);
            var b = Math.abs(y1-y2);
            return Math.sqrt(a*a + b*b ) ;
        })();

        var x = (function() {
            var a = Math.abs(x1-x2);
            var b = Math.abs(y1-y2);
            var sx = (x1+x2)/2 ;
            var sy = (y1+y2)/2 ;
            var width = Math.sqrt(a*a + b*b ) ;
            return sx - width/2;
        })();

        var y = (function() {
            return (y1+y2)/2 ;
        })();

        var div = document.createElement("div");
        div.setAttribute('style','border:1px solid black;width:'+width+'px;height:0px;-moz-transform:rotate('+deg+'deg);-webkit-transform:rotate('+deg+'deg);-ms-transform:rotate('+deg+'deg);transform:rotate('+deg+'deg);position:absolute;top:'+y+'px;left:'+x+'px;');

        var wrapper = document.createElement("div");
        wrapper.appendChild(div);

        if (arrow) {
            var width = 6;
            var top = target.y - width;
            var left = target.x - width;;
            //if (target.x < source.x) { left -= width; }
            var deg = deg;
            if (target.y > source.y) { deg -= 180; }
            var a = document.createElement("div");
            a.style.borderColor = "transparent black transparent transparent";
            a.style.borderWidth = width + "px";
            a.style.borderStyle = "solid";
            a.style.position = "absolute";
            a.style.left = left + "px";
            a.style.top = top + "px";
            a.style.transform = "rotate(" + deg + "deg)";
            wrapper.appendChild(a);
        }

        return wrapper;

    };

    DayPilot.deg = function(x1, y1, x2, y2) {
        var a = Math.abs(x1-x2);
        var b = Math.abs(y1-y2);
        var c;
        var sx = (x1+x2)/2 ;
        var sy = (y1+y2)/2 ;
        var width = Math.sqrt(a*a + b*b ) ;
        var x = sx - width/2;
        var y = sy;

        a = width / 2;

        c = Math.abs(sx-x);

        b = Math.sqrt(Math.abs(x1-x)*Math.abs(x1-x)+Math.abs(y1-y)*Math.abs(y1-y) );

        var cosb = (b*b - a*a - c*c) / (2*a*c);
        var rad = Math.acos(cosb);
        var deg = (rad*180)/Math.PI;

        return deg;
    };

    DayPilot.complete = function(f) {
        if (document.readyState === "complete") {
            f();
            return;
        }
        if (!DayPilot.complete.list) {
            DayPilot.complete.list = [];
            DayPilot.re(document, "readystatechange", function() {
                if (document.readyState === "complete") {
                    for (var i = 0; i < DayPilot.complete.list.length; i++) {
                        var d = DayPilot.complete.list[i];
                        d();
                    }
                    DayPilot.complete.list = [];
                }
            });
        }
        DayPilot.complete.list.push(f);
    };

    // returns pageX, pageY (calculated from clientX if pageX is not available)
    DayPilot.page = function(ev) {
        ev = ev || window.event;
        if (typeof ev.pageX !== 'undefined') {
            return {x: ev.pageX, y: ev.pageY};
        }
        if (typeof ev.clientX !== 'undefined') {
            return {
                x: ev.clientX + document.body.scrollLeft + document.documentElement.scrollLeft,
                y: ev.clientY + document.body.scrollTop + document.documentElement.scrollTop
            };
        }
        // shouldn't happen
        return null;
    };

    // absolute element position on page
    DayPilot.abs = function(element, visible) {
        if (!element) {
            return null;
        }

        var r = {
            x: element.offsetLeft,
            y: element.offsetTop,
            w: element.clientWidth,
            h: element.clientHeight,
            toString: function() {
                return "x:" + this.x + " y:" + this.y + " w:" + this.w + " h:" + this.h;
            }
        };

        if (element.getBoundingClientRect) {
            //var b = element.getBoundingClientRect();
            var b = null;
            try {
                b = element.getBoundingClientRect();
            } catch (e) {
                b = {top: element.offsetTop, left: element.offsetLeft};
            }
            ;
            r.x = b.left;
            r.y = b.top;

            var d = DayPilot.doc();
            r.x -= d.clientLeft || 0;
            r.y -= d.clientTop || 0;

            var pageOffset = DayPilot.pageOffset();
            r.x += pageOffset.x;
            r.y += pageOffset.y;

            if (visible) {
                // use diff, absOffsetBased is not as accurate
                var full = DayPilot.absOffsetBased(element, false);
                var visible = DayPilot.absOffsetBased(element, true);

                r.x += visible.x - full.x;
                r.y += visible.y - full.y;
                r.w = visible.w;
                r.h = visible.h;
            }

            return r;
        }
        else {
            return DayPilot.absOffsetBased(element, visible);
        }

    };

    DayPilot.isArray = function(o) {
        return Object.prototype.toString.call(o) === '[object Array]';
    };

    // old implementation of absolute position
    // problems with adjacent float and margin-left in IE7
    // still the best way to calculate the visible part of the element
    DayPilot.absOffsetBased = function(element, visible) {
        var r = {
            x: element.offsetLeft,
            y: element.offsetTop,
            w: element.clientWidth,
            h: element.clientHeight,
            toString: function() {
                return "x:" + this.x + " y:" + this.y + " w:" + this.w + " h:" + this.h;
            }
        };

        while (DayPilot.op(element)) {
            element = DayPilot.op(element);

            r.x -= element.scrollLeft;
            r.y -= element.scrollTop;

            if (visible) {  // calculates the visible part
                if (r.x < 0) {
                    r.w += r.x; // decrease width
                    r.x = 0;
                }

                if (r.y < 0) {
                    r.h += r.y; // decrease height
                    r.y = 0;
                }

                if (element.scrollLeft > 0 && r.x + r.w > element.clientWidth) {
                    r.w -= r.x + r.w - element.clientWidth;
                }

                if (element.scrollTop && r.y + r.h > element.clientHeight) {
                    r.h -= r.y + r.h - element.clientHeight;
                }
            }

            r.x += element.offsetLeft;
            r.y += element.offsetTop;

        }

        var pageOffset = DayPilot.pageOffset();
        r.x += pageOffset.x;
        r.y += pageOffset.y;

        return r;
    };
    
    // window dimensions
    DayPilot.wd = function() {
        var ieQuirks = DayPilot.isIEQuirks;
        
        // don't show the bubble outside of the visible window
        var windowHeight = document.documentElement.clientHeight;
        // fixing http://forums.daypilot.org/Topic.aspx/519/issue_with_bubble_in_ie
        if (ieQuirks) {
            windowHeight = document.body.clientHeight;
        }

        var windowWidth = document.documentElement.clientWidth;
        // fixing http://forums.daypilot.org/Topic.aspx/519/issue_with_bubble_in_ie
        if (ieQuirks) {
            windowWidth = document.body.clientWidth;
        }
        
        var scrollTop = (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop;
        var scrollLeft = (document.documentElement && document.documentElement.scrollLeft) || document.body.scrollLeft;
        
        var result = {};
        result.width = windowWidth;
        result.height = windowHeight;
        result.scrollTop = scrollTop;
        result.scrollLeft = scrollLeft;
        
        return result;
    };

    // offsetParent, safe access to prevent "Unspecified Error" in IE
    DayPilot.op = function(element) {
        try {
            return element.offsetParent;
        }
        catch (e) {
            return document.body;
        }
    };

    // distance of two points, works with x and y
    DayPilot.distance = function(point1, point2) {
        return Math.sqrt(Math.pow(point1.x - point2.x, 2) + Math.pow(point1.y - point2.y, 2));
    };

    // document element
    DayPilot.doc = function() {
        var de = document.documentElement;
        return (de && de.clientHeight) ? de : document.body;
    };

    DayPilot.pageOffset = function() {
        if (typeof pageXOffset !== 'undefined') {
            return {x: pageXOffset, y: pageYOffset};
        }
        var d = DayPilot.doc();
        return {x: d.scrollLeft, y: d.scrollTop};
    };

    // all children
    DayPilot.ac = function(e, children) {
        if (!children) {
            var children = [];
        }
        for (var i = 0; e.children && i < e.children.length; i++) {
            children.push(e.children[i]);
            DayPilot.ac(e.children[i], children);
        }

        return children;
    };

    DayPilot.indexOf = function(array, object) {
        if (!array || !array.length) {
            return -1;
        }
        for (var i = 0; i < array.length; i++) {
            if (array[i] === object) {
                return i;
            }
        }
        return -1;
    };

    DayPilot.contains = function(array, object) {
        //return object in array;
        return DayPilot.indexOf(array, object) !== -1;
    };

    // remove from array
    DayPilot.rfa = function(array, object) {
        var i = DayPilot.indexOf(array, object);
        if (i === -1) {
            return;
        }
        array.splice(i, 1);
    };
    
    DayPilot.sheet = function() {
        var style = document.createElement("style");
        style.setAttribute("type", "text/css");
        if (!style.styleSheet) {   // ie
            style.appendChild(document.createTextNode(""));
        }

        var h = document.head || document.getElementsByTagName('head')[0];
        h.appendChild(style);

        var oldStyle = !! style.styleSheet; // old ie

        var sheet = {};
        sheet.rules = [];
        sheet.commit = function() {
            try {
                if (oldStyle) {
                    style.styleSheet.cssText = this.rules.join("\n");
                }
            }
            catch (e) {
                //alert("Error registering the built-in stylesheet (IE stylesheet limit reached). Stylesheet count: " + document.styleSheets.length);
            }
        };

        sheet.add = function(selector, rules, index) {
            if (typeof index === 'undefined') {
                index = 0;
            }
            if (oldStyle) {
                this.rules.push(selector + "{" + rules + "\u007d");
                return;
            }
            if(style.sheet.insertRule) {
                style.sheet.insertRule(selector + "{" + rules + "\u007d", index);
            }
            else if (style.sheet.addRule) {
                style.sheet.addRule(selector, rules, index);
            }
            else {
                throw "No CSS registration method found";
            }
        };
        return sheet;
    };

    DayPilot.Debug = function(calendar) {
        var debug = this;
        
        this.printToBrowserConsole = false;
        this.messages = [];
        this._div = null;
        this.clear = function() {
            this.messages = [];
            if (debug._div) {
                debug._div.innerHTML = '';
            }
        };
        
        this.hide = function() {
            DayPilot.de(debug._div);
            debug._div = null;
        };
        
        this.show = function() {
            if (debug._div) {
                debug.hide();
            }
            
            var ref = calendar.nav.top;

            var div = document.createElement("div");
            div.style.position = "absolute";
            div.style.top = "0px";
            div.style.bottom = "0px";
            div.style.left = "0px";
            div.style.right = "0px";
            div.style.backgroundColor = "black";
            div.style.color = "#ccc";
            div.style.overflow = "auto";
            div.style.webkitUserSelect = 'auto';
            div.style.MozUserSelect = 'all';
            div.onclick = function() {
                debug.hide();
            };
            
            for(var i = 0; i < this.messages.length; i++) {
                var msg = debug.messages[i];
                
                var line = msg._toElement();
                div.appendChild(line);
            }
            
            this._div = div;
            ref.appendChild(div);
        };
        
        this.message = function(text, level) {  // levels: info, warning, error
            var msg = {};
            msg.time = new DayPilot.Date();
            msg.level = level || "debug";
            msg.text = text;
            msg._toElement = function() {
                var line = document.createElement("div");
                line.innerHTML =  msg.time + " (" + msg.level + "): " + msg.text;
                switch (msg.level) {
                    case "error":
                        line.style.color = "red";
                        break;
                    case "warning":
                        line.style.color = "orange";
                        break;
                    case "info":
                        line.style.color = "white";
                        break;
                    case "debug":
                        break;
                }
                return line;
            };
            
            this.messages.push(msg);
            
            if (this.printToBrowserConsole && typeof console !== 'undefined') {
                console.log(msg);
            }
        };
    };

    // register event
    DayPilot.re = function(el, ev, func) {
        if (!func) {
            return;
        }
        if (el.addEventListener) {
            el.addEventListener(ev, func, false);
        } else if (el.attachEvent) {
            var f = function(ev) {
                func.call(el, ev);
            }
            el.attachEvent("on" + ev, f);
        }
    };
    // unregister event
    DayPilot.ue = function(el, ev, func) {
        if (el.removeEventListener) {
            el.removeEventListener(ev, func, false);
        } else if (el.detachEvent) {
            el.detachEvent("on" + ev, func);
        }
    };
    // trim
    DayPilot.tr = function(stringToTrim) {
        if (!stringToTrim)
            return '';
        return stringToTrim.replace(/^\s+|\s+$/g, "");
    };
    // date sortable (DateTime.ToString("s"))
    DayPilot.ds = function(d) {
        return DayPilot.Date.toStringSortable(d);
    };
    // get style
    DayPilot.gs = function(el, styleProp) {
        var x = el;
        if (x.currentStyle)
            var y = x.currentStyle[styleProp];
        else if (window.getComputedStyle)
            var y = document.defaultView.getComputedStyle(x, null).getPropertyValue(styleProp);
        if (typeof (y) === 'undefined')
            y = '';
        return y;
    };
    // encode arguments
    DayPilot.ea = function(a) {
        var joined = "";
        for (var i = 0; i < a.length; i++) {
            if (a[i] || typeof (a[i]) === 'number') {
                if (a[i].isDayPilotDate) {
                    a[i] = a[i].toStringSortable();
                }
                else if (a[i].getFullYear) {
                    a[i] = DayPilot.ds(a[i]);
                }
                joined += encodeURIComponent(a[i]);
            }
            if (i + 1 < a.length) {
                joined += '&';
            }
        }
        return joined;
    };

    // html encode
    DayPilot.he = function(str) {
        var result = str.replace(/&/g, "&amp;");
        result = result.replace(/</g, "&lt;");
        result = result.replace(/>/g, "&gt;");
        result = result.replace(/"/g, "&quot;");
        return result;
    };

    // cellIndex
    DayPilot.ci = function(cell) {
        var i = cell.cellIndex;
        if (i && i > 0)
            return i;
        var tr = cell.parentNode;
        var len = tr.cells.length;
        for (i = 0; i < len; i++) {
            if (tr.cells[i] === cell)
                return i;
        }
        return null;
    };

    // make unselectable
    DayPilot.us = function(element) {
        if (element) {
            element.setAttribute("unselectable", "on");
            element.style.userSelect = 'none';
            element.style.MozUserSelect = 'none'; 
            element.style.KhtmlUserSelect = 'none';
            element.style.webkitUserSelect = 'none';
            for (var i = 0; i < element.childNodes.length; i++) {
                if (element.childNodes[i].nodeType === 1) {
                    DayPilot.us(element.childNodes[i]);
                }
            }
        }
    };

    // purge
    // thanks to http://javascript.crockford.com/memory/leak.html
    DayPilot.pu = function(d) {
        //var removed = [];
        //var start = new Date();
        var a = d.attributes, i, l, n;
        if (a) {
            l = a.length;
            for (i = 0; i < l; i += 1) {
                if (!a[i]) {
                    continue;
                }
                n = a[i].name;
                if (typeof d[n] === 'function') {
                    //DayPilot.log.push(d.tagName + "." + n);
                    //removed.push(n);
                    d[n] = null;
                }
            }
        }
        a = d.childNodes;
        if (a) {
            l = a.length;
            for (i = 0; i < l; i += 1) {
                var children = DayPilot.pu(d.childNodes[i]);
                //removed = removed.concat(children);
            }
        }
        //return removed;
    };

    // purge children
    DayPilot.puc = function(d) {
        var a = d.childNodes, i, l;
        if (a) {
            var l = a.length;
            for (i = 0; i < l; i += 1) {
                DayPilot.pu(d.childNodes[i]);
            }
        }
    };

    // delete element
    DayPilot.de = function(e) {
        if (!e) {
            return;
        }
        if (DayPilot.isArray(e)) {
            for (var i = 0; i < e.length; i++) {
                DayPilot.de(e[i]);
            }
            return;
        }
        /*
        if (!e.parentNode) {
            return;
        }*/
        e.parentNode && e.parentNode.removeChild(e);
    };

    // get row
    DayPilot.gr = function(cell) {
        var i = 0;
        var tr = cell.parentNode;
        while (tr.previousSibling) {
            tr = tr.previousSibling;
            if (tr.tagName === "TR") {
                i++;
            }
        }
        return i;
    };

    DayPilot.fade = function(element, step, end) {
        if (!element) {
            return;
        }

        var delay = 50;
        var visible = element.style.display !== 'none';
        var fadeIn = step > 0;
        var fadeOut = step < 0;

        if (step === 0) {
            return;
        }

        if (fadeIn) {
            element.status = "in";
        }
        else if (fadeOut) {
            element.status = "out";
        }

        if (fadeIn && !visible) {
            element.target = parseFloat(element.style.opacity);
            element.opacity = 0; // current, for IE
            element.style.opacity = 0;
            element.style.filter = "alpha(opacity=0)";
            element.style.display = '';
        }
        else if (fadeOut && !element.target) {
            element.target = element.style.opacity;
        }
        else {
            //var current = parseFloat(element.style.opacity);
            var current = element.opacity;
            var updated = Math.floor(10 * (current + step)) / 10;
            if (fadeIn && updated > element.target) {
                updated = element.target;
            }
            if (fadeOut && updated < 0) {
                updated = 0;
            }
            var ie = updated * 100;
            element.opacity = updated;
            element.style.opacity = updated;
            element.style.filter = "alpha(opacity=" + ie + ")";
        }
        if ((fadeIn && (element.opacity >= element.target || element.opacity >= 1)) || (fadeOut && element.opacity <= 0)) {
            element.target = null;
            if (fadeOut) {
                element.style.opacity = element.target;
                element.opacity = element.target;
                var filter = element.target ? "alpha(opacity=" + (element.target * 100) + ")" : null;
                element.style.filter = filter;
                element.style.display = 'none';
            }
            if (end && typeof end === 'function') {
                delete element.status;
                end();
            }
        }
        else {
            element.messageTimeout = setTimeout(function() {
                DayPilot.fade(element, step, end);
            }, delay);
        }
    };


    // vertical scrollbar width
    DayPilot.sw = function(element) {
        if (!element) {
            return 0;
        }
        return element.offsetWidth - element.clientWidth;
    };
    
    DayPilot.swa = function() {
        var div = document.createElement("div");
        div.style.position = "absolute";
        div.style.top = "-2000px";
        div.style.left = "-2000px";
        div.style.width = '200px';
        div.style.height = '100px';
        div.style.overflow = 'auto';
        
        var inner = document.createElement("div");
        inner.style.width = '300px';
        inner.style.height = '300px';
        div.appendChild(inner);

        document.body.appendChild(div);
        var sw = DayPilot.sw(div);
        document.body.removeChild(div);

        return sw;
    };

    // horizontal scrollbar height
    DayPilot.sh = function(element) {
        if (!element) {
            return 0;
        }
        return element.offsetHeight - element.clientHeight;
    };

    DayPilot.guid = function() {
        var S4 = function() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return ("" + S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    };

    // unique array members
    // works for strings and numbers only
    DayPilot.ua = function(array) {
        if (typeof array === "string" || typeof array === "number") {
            return [array];
        }
        var u = {}, a = [];
        for (var i = 0, l = array.length; i < l; ++i) {
            if (array[i] in u) {
                continue;
            }
            a.push(array[i]);
            u[array[i]] = 1;
        }
        return a;
    };

    // angular module
    DayPilot.am = function() {
        if (typeof angular === "undefined") {
            return null;
        }
        if (!DayPilot.am.cached) {
            DayPilot.am.cached = angular.module("daypilot", []);
        }
        return DayPilot.am.cached;
    };
    
    (function () {
        DayPilot.pop = wave;

        function wave(div, options) {
            var target = {
                w: div.offsetWidth,
                h: div.offsetHeight,
                x: parseInt(div.style.left),
                y: parseInt(div.style.top)
            };
            target.height = div.style.height;
            target.width = div.style.width;
            target.top = div.style.top;
            target.left = div.style.left;
            target.toString = function () { return "w: " + this.w + " h:" + this.h; };
            
            var config = {};
            config.finished = null;
            config.vertical = 'center';
            config.horizontal = 'center';
            
            if (options) {
                for (var name in options) {
                    config[name] = options[name];
                }
            }

            div.style.visibility = 'hidden';
            div.style.display = '';

            var animation = options.animation || "fast";

            var plan = createPlan(animation);

            plan.div = div;
            plan.i = 0;
            plan.target = target;
            plan.config = config;

            doStep(plan);
        }
        
        function createPlan(type) {

            var jump = function() {
                var plan = [];
                plan.time = 10;
                var last;

                var step = 0.08;
                last = 0.1;

                for (var i = last; i < 1.2; i += step) {
                    plan.push(i);
                    last = i;
                }

                step = 0.03;

                for (var i = last; i > 0.8; i -= step) {
                    plan.push(i);
                    last = i;
                }

                for (var i = last; i <= 1; i += step) {
                    plan.push(i);
                    last = i;
                }

                return plan;
            };

            var slow = function() {
                var plan = [];
                plan.time = 15;
                var last;

                var step = 0.04;
                last = 0.1;

                for (var i = last; i <= 1; i += step) {
                    plan.push(i);
                    last = i;
                }
                return plan;
            };

            var fast = function() {
                var plan = [];
                plan.time = 9;
                var last;

                var step = 0.04;
                last = 0.1;

                for (var i = last; i <= 1; i += step) {
                    plan.push(i);
                    last = i;
                }
                return plan;
            };
            
            var types = {
                "fast": fast,
                "slow": slow,
                "jump": jump
            };
            
            if (!types[type]) {
                type = "fast";
            }

            return types[type]();
        }

        function doStep(plan) {
            var div = plan.div;

            var pct = plan[plan.i];

            var height = pct * plan.target.h;
            var top;
            switch (plan.config.vertical) {
                case "center":
                    top = plan.target.y - (height - plan.target.h) / 2;
                    break;
                case "top":
                    top = plan.target.y;
                    break;
                case "bottom":
                    top = plan.target.y - (height - plan.target.h);
                    break;
                default:
                    throw "Unexpected 'vertical' value.";
            }

            var width = pct * plan.target.w;
            var left;
            
            switch (plan.config.horizontal) {
                case "left":
                    left = plan.target.x;
                    break;
                case "center":
                    left = plan.target.x - (width - plan.target.w) / 2;
                    break;
                case "right":
                    left = plan.target.x - (width - plan.target.w);
                    break;
                default:
                    throw "Unexpected 'horizontal' value.";
            }
            
            // TODO add scrollLeft
            var wd = DayPilot.wd();
            var bottom = (wd.height + wd.scrollTop) - (top + height);
            if (bottom < 0) {
                top += bottom;
            }
            
            var right = (wd.width) - (left + width);
            if (right < 0) {
                left += right;
            }

            div.style.height = height + "px";
            div.style.top = top + "px";

            div.style.width = width + "px";
            div.style.left = left + "px";

            //div.style.display = '';
            div.style.visibility = 'visible';

            plan.i++;

            if (plan.i < plan.length - 1) {
                setTimeout((function (plan) {
                    return function () {
                        doStep(plan);
                    };
                })(plan), plan.time);
            }
            else {
                // set the original dimensions
                div.style.width = plan.target.width;
                div.style.height = plan.target.height;
                // and position
                div.style.top = plan.target.top;
                div.style.left = plan.target.left;
                
                // callback
                if (typeof plan.config.finished === 'function') {
                    plan.config.finished();
                }
            }
        }


    })();

    DayPilot.Util = {};

    // object - DOM element or array of DOM elements
    DayPilot.Util.addClass = function(object, name) {
        if (!name) {
            return;
        }
        if (!object) {
            return;
        }
        if (DayPilot.isArray(object)) {
            for (var i = 0; i < object.length; i++) {
                DayPilot.Util.addClass(object[i], name);
            }
            return;
        }
        if (!object.className) {
            object.className = name;
            return;
        }
        var already = new RegExp("(^|\\s)" + name + "($|\\s)");
        if (!already.test(object.className)) {
            object.className = object.className + ' ' + name;
        }
    };

    DayPilot.Util.addClassToString = function(str, name) {
        if (!str) {
            return name;
        }
        var already = new RegExp("(^|\\s)" + name + "($|\\s)");
        if (!already.test(str)) {
            return str + ' ' + name;
        }
        else {
            return str;
        }
    };

    DayPilot.Util.removeClassFromString = function(str, name) {
        if (!str) {
            return "";
        }
        var already = new RegExp("(^|\\s)" + name + "($|\\s)");
        return str.replace(already, ' ').replace(/^\s\s*/, '').replace(/\s\s*$/, '');  // trim spaces
    };

    DayPilot.Util.removeClass = function(object, name) {
        if (!object) {
            return;
        }
        if (DayPilot.isArray(object)) {
            for (var i = 0; i < object.length; i++) {
                DayPilot.Util.removeClass(object[i], name);
            }
            return;
        }
        var already = new RegExp("(^|\\s)" + name + "($|\\s)");
        object.className = object.className.replace(already, ' ').replace(/^\s\s*/, '').replace(/\s\s*$/, '');  // trim spaces
    };

    DayPilot.Util.props = function(o) {
        var t = [];
        for (var a in o) {
            t.push(a);
            t.push(o[a]);
        }
        return t.join("-");
    };
    
    
    DayPilot.Util.propArray = function(props, name) {
        var result = [];
        if (!props || !props.length) {
            return result;
        }

        for (var i = 0; i < props.length; i++) {
            result.push(props[i][name]);
        }
        return result;
    };
    
    DayPilot.Util.updatePropsFromArray = function(props, name, array) {
        for (var i = 0; i < array.length; i++) {
            props[i][name] = array[i];
        }
    };    
    
    DayPilot.Util.copyProps = function(source, target, props) {
        if (!source) {
            return;
        }
        if (typeof props === 'undefined') {
            for (var name in source) {
                target[name] = source[name];
            }
        }
        else {
            for (var i = 0; i < props.length; i++) {
                var name = props[i];
                target[name] = source[name];
            }
        }
    };

    DayPilot.Util.createArrayCopy = function(source, itemProps) {
        if (!DayPilot.isArray(source)) {
            return [];
        }
        var list = [];
        for (var i = 0; i < source.length; i++) {
            var item = {};
            DayPilot.Util.copyProps(source[i], item, itemProps);
            list.push(item);
        }
        return list;
    };

    DayPilot.Util.avg = function(a, b) {
        return (a + b) / 2;
    };

    DayPilot.Util.div = function(parent, left, top, width, height) {
        var div = document.createElement("div");
        if (left || top || width || height) {
            if (width < 0) {
                left += width;
                width *= -1;
            }
            if (height < 0) {
                top += height;
                height *= -1;
            }
            div.style.position = "absolute";
            if (typeof left === "number") {
                div.style.left = left + "px";
            }
            if (typeof top === "number") {
                div.style.top = top + "px";
            }
            if (typeof width === "number") {
                div.style.width = width + "px";
            }
            if (typeof height === "number") {
                div.style.height = height + "px";
            }
        }
        if (parent) {
            parent.appendChild(div);
        }
        return div;
    };

    DayPilot.Util.overlaps = function(start1, end1, start2, end2) {
        start1 = start1.isDayPilotDate ? start1.getTime() : start1;
        start2 = start2.isDayPilotDate ? start2.getTime() : start2;
        end1 = end1.isDayPilotDate ? end1.getTime() : end1;
        end2 = end2.isDayPilotDate ? end2.getTime() : end2;

        return !(end1 <= start2 || start1 >= end2);
    };

    DayPilot.Util.mouseButton = function(ev) {
        var result = {};

        ev = ev || window.event;

        if (typeof ev.which === 'undefined') {
            switch (ev.button) {
                case 1:
                    result.left = true;
                    break;
                case 4:
                    result.middle = true;
                    break;
                case 2:
                    result.right = true;
                    break;
                case 0:
                    result.unknown = true;
                    break;
            }
        }
        else {
            switch (ev.which) {
                case 1:
                    result.left = true;
                    break;
                case 2:
                    result.middle = true;
                    break;
                case 3:
                    result.right = true;
                    break;
            }
        }
        return result;
    };

    DayPilot.Util.membersPlain = function(obj) {
        var members = DayPilot.Util.members(obj, 2);

        var transformArray = function(array) {
            for (var i = 0; i < array.length; i++) {
                var item = array[i];
                var name = item.name;
                if (item.obsolete) {
                    name += " (obsolete)";
                }
                if (item.noCssOnly) {
                    name += " (!cssOnly)";
                }
                if (item.aspnet) {
                    name += " (ASP.NET)";
                }
                if (item.mvc) {
                    name += "(MVC)";
                }
                array[i] = name;
            }
        };

        transformArray(members.events);
        transformArray(members.methods);
        transformArray(members.properties);

        return members;
    };

    DayPilot.Util.members = function(obj, maxLevel) {
        var events = [];
        var methods = [];
        var properties = [];

        var obsolete = (obj && obj.members) ? obj.members.obsolete : [];
        var noCssOnly = (obj && obj.members) ? obj.members.noCssOnly : [];
        var ignore = (obj && obj.members) ? obj.members.ignore : [];
        var ignoreFilter = (obj && obj.members && obj.members.ignoreFilter) ? obj.members.ignoreFilter : function() { return false; };

        for (var name in obj) {
            //var start = name.substring(0, 1);
            if (name.indexOf("$") === 0) {
                continue;
            }
            if (name.indexOf("_") === 0) {
                continue;
            }
            if (name.indexOf("number") === 0) {
                continue;
            }
            if (name.indexOf("is") === 0) {
                continue;
            }
            if (name === "v") {
                continue;
            }
            if (DayPilot.contains(ignore, name)) {
                continue;
            }
            if (ignoreFilter(name)) {
                continue;
            }
            if (name.indexOf("on") === 0) {
                events.push(name);
                continue;
            }
            if (typeof obj[name] === 'function') {
                methods.push(name);
                continue;
            }
            if (typeof obj[name] === 'object') {
                var o = obj[name];
                if (maxLevel === 0) {
                    properties.push(name);
                    continue;
                }
                if (o && o.nodeType > 0) {
                    properties.push(name);
                    continue;
                }
                if (o instanceof DayPilot.Bubble) {
                    properties.push(name);
                    continue;
                }
                if (o instanceof DayPilot.Date) {
                    properties.push(name);
                    continue;
                }
                if (o instanceof DayPilot.Menu) {
                    properties.push(name);
                    continue;
                }
                if (o instanceof DayPilot.Scheduler) {
                    properties.push(name);
                    continue;
                }
                if (DayPilot.isArray(o)) {
                    properties.push(name);
                    continue;
                }
                if (o === null) {
                    properties.push(name);
                }
                var ml = null;
                if (typeof maxLevel === "number") {
                    ml = maxLevel - 1;
                }
                var members = DayPilot.Util.members(o, ml);
                for (var i = 0; i < members.events.length; i++) {
                    events.push(name + "." + members.events[i].name);
                }
                for (var i = 0; i < members.methods.length; i++) {
                    methods.push(name + "." + members.methods[i].name);
                }
                for (var i = 0; i < members.properties.length; i++) {
                    properties.push(name + "." + members.properties[i].name);
                }
                continue;
            }
            properties.push(name);
        }

        events.sort();
        methods.sort();
        properties.sort();

        var transformArray = function(array) {
            for (var i = 0; i < array.length; i++) {
                var name = array[i];
                var item = {};
                item.name = name;
                array[i] = item;
                if (DayPilot.contains(obsolete, name)) {
                    item.obsolete = true;
                }
                if (DayPilot.contains(noCssOnly, name)) {
                    item.noCssOnly = true;
                }
                if (name.indexOf("CallBack") !== -1) {
                    item.aspnet = true;
                    item.mvc = true;
                }
                if (name.indexOf("PostBack") !== -1) {
                    item.aspnet = true;
                }
                if (name.indexOf("Notify") !== -1) {
                    item.aspnet = true;
                    item.mvc = true;
                }

            }
        };
        transformArray(events);
        transformArray(methods);
        transformArray(properties);

        return {
            "events": events,
            "methods": methods,
            "properties": properties
        };
    };

    DayPilot.Util.replaceCharAt = function(str, index, character) {
        return str.substr(0, index) + character + str.substr(index + character.length);
    };

    DayPilot.Areas = {};

    /**
     * Attach active areas to a target div.
     */
    DayPilot.Areas.attach = function (div, e, options) {
        var areas = options.areas;
        var allowed = options.allowed || function() { return true; };

        DayPilot.re(div, "mousemove", function(ev) {
            if (!div.active && allowed()) {
                DayPilot.Areas.showAreas(div, e, ev, areas);
            }
        });
        DayPilot.re(div, "mouseout", function(ev) {
            DayPilot.Areas.hideAreas(div, ev);
        });

        // permanently visible active areas
        areas = areasExtract(e, areas);
        for (var j = 0; j < areas.length; j++) {
            var area = areas[j];
            var v = area.v || "Visible";
            if (v !== "Visible") {
                continue;
            }
            //var r = calendar._createRowObject(row);
            var a = DayPilot.Areas.createArea(div, e, area);
            div.appendChild(a);
        }
    };

    /**
     * Extracts areas array from the source object, giving priority to a standalone areas object.
     * @param e
     * @param areas
     */
    var areasExtract = function(e, areas) {
        if (!DayPilot.isArray(areas)) {
            areas = e.areas;
            if (!areas) {
                if (e.cache) {
                    areas = e.cache.areas;
                }
                else if (e.data) {
                    areas = e.data.areas;
                }
            }
        }
        return areas;
    };


    DayPilot.Areas.showAreas = function(div, e, ev, areas) {
        if (DayPilot.Global.resizing) {
            return;
        }

        if (DayPilot.Global.moving) {
            return;
        }

        if (DayPilot.Areas.all && DayPilot.Areas.all.length > 0) {
            for (var i = 0; i < DayPilot.Areas.all.length; i++) {
                var d = DayPilot.Areas.all[i];
                if (d !== div) {
                    DayPilot.Areas.hideAreas(d, ev);
                }
            }
        }

        if (div.active) {
            return;
        }
        div.active = {};

        //var areas;
        if (!DayPilot.isArray(areas)) {
            areas = e.areas;
            if (!areas) {
                if (e.cache) {
                    areas = e.cache.areas;
                }
                else if (e.data) {
                    areas = e.data.areas;
                }
            }
        }
        
        /*
        if (!areas && e.cache && e.cache.areas) {
            areas = e.cache.areas;
        }
        
        if (!areas && e.data && e.data.areas) {
            areas = e.data.areas;
        }
        */
       
        if (!areas || areas.length === 0) {
            return;
        }

        if (div.areas && div.areas.length > 0) {
            return;
        }
        //if (typeof div.areas == 'undefined') {
        div.areas = [];
        //}

        for (var i = 0; i < areas.length; i++) {
            var area = areas[i];
            var v = area.v || "Visible";
            if (v !== 'Hover') {
                continue;
            }

            var a = DayPilot.Areas.createArea(div, e, area);

            div.areas.push(a);
            div.appendChild(a);

            DayPilot.Areas.all.push(div);
        }
        div.active.children = DayPilot.ac(div);
    };

    DayPilot.Areas.createArea = function(div, e, area) {

        var a = document.createElement("div");
        a.isActiveArea = true;
        a.setAttribute("unselectable", "on");
        var w = area.w || area.width;
        var h = area.h || area.height;
        var css = area.css || area.className;
        if (typeof area.style !== "undefined") {
            a.setAttribute("style", area.style);
        }
        a.style.position = "absolute";
        if (typeof w !== 'undefined') {
            a.style.width = w + "px";
        }
        if (typeof h !== 'undefined') {
            a.style.height = h + "px";
        }
        if (typeof area.right !== 'undefined') {
            a.style.right = area.right + "px";
        }
        if (typeof area.top !== 'undefined') {
            a.style.top = area.top + "px";
        }
        if (typeof area.left !== 'undefined') {
            a.style.left = area.left + "px";
        }
        if (typeof area.bottom !== 'undefined') {
            a.style.bottom = area.bottom + "px";
        }
        if (typeof area.html !== 'undefined' && area.html) {
            a.innerHTML = area.html;
        }
        if (css) {
            a.className = css;
        }
        if (area.backColor) {
            a.style.background = area.backColor;
        }
        if (area.action === "ResizeEnd" || area.action === "ResizeStart" || area.action === "Move") {
            if (e.calendar.isCalendar) {
                switch (area.action) {
                    case "ResizeEnd":
                        area.cursor = "s-resize";
                        area.dpBorder = "bottom";
                        break;
                    case "ResizeStart":
                        area.cursor = "n-resize";
                        area.dpBorder = "top";
                        break;
                    case "Move":
                        area.cursor = "move";
                        break;
                }
            }
            if (e.calendar.isScheduler || e.calendar.isMonth) {
                switch (area.action) {
                    case "ResizeEnd":
                        area.cursor = "e-resize";
                        area.dpBorder = "right";
                        break;
                    case "ResizeStart":
                        area.cursor = "w-resize";
                        area.dpBorder = "left";
                        break;
                    case "Move":
                        area.cursor = "move";
                        break;
                }
            }
            a.onmousemove = (function(div, e, area) {
                return function(ev) {
                    var ev = ev || window.event;
                    div.style.cursor = area.cursor;
                    if (area.dpBorder) {
                        div.dpBorder = area.dpBorder;
                    }
                    ev.cancelBubble = true;
                };
            })(div, e, area);
            a.onmouseout = (function(div, e, area) {
                return function(ev) {
                    div.style.cursor = '';
                };
            })(div, e, area);
        }
        if (area.action === "Move" && e.isEvent) {
            if (e.calendar.internal.touch) {
                var touchstart = (function(div, e, area) {
                    return function(ev) {
                        ev.cancelBubble = true;
                        var touch = e.calendar.internal.touch;
                        var t = ev.touches ? ev.touches[0] : ev;
                        var coords = {x: t.pageX, y: t.pageY }; 
                        // immediately
                        e.calendar.coords = touch.relativeCoords(ev);
                        touch.preventEventTap = true;
                        touch.startMoving(div, coords);
                    };
                })(div, e, area);
                DayPilot.re(a, DayPilot.touch.start, touchstart);
            }
        }
        if (area.action === "Move" && e.isRow) {
            if (e.calendar.internal.touch) {
                // TODO
            }
            /*
            a.onmousedown = (function(div, e, area) {
                return function(ev) {
                    rowmoving.row = row;
                    rowtools.createOverlay(row);
                };
            })(div, e, area);
            */
        }
        if (area.action === "Bubble" && e.isEvent) {
            a.onmousemove = (function(div, e, area) {
                return function(ev) {
                    if (e.calendar.bubble) {
                        e.calendar.bubble.showEvent(e);
                    }
                };
            })(div, e, area);
            a.onmouseout = (function(div, e, area) {
                return function(ev) {
                    if (typeof DayPilot.Bubble !== "undefined") {
                        //DayPilot.Bubble.hideActive();
                        if (e.calendar.bubble) {
                            e.calendar.bubble.hideOnMouseOut();
                        }
                    }

                };
            })(div, e, area);
        }
        if (area.action === "HoverMenu") {
            a.onmousemove = (function(div, e, area) {
                return function(ev) {
                    var m = area.menu;
                    if (typeof m === 'string') {
                        m = eval(m);
                    }
                    if (m && m.show) {
                        if (!m.visible) {
                            m.show(e);
                        }
                        else if (m.source && typeof m.source.id !== 'undefined' && m.source.id !== e.id) {
                            m.show(e);
                        }
                        m.cancelHideTimeout();
                    }
                };
            })(div, e, area);
            a.onmouseout = (function(div, e, area) {
                return function(ev) {
                    var m = area.menu;
                    if (typeof m === 'string') {
                        m = eval(m);
                    }
                    if (!m) {
                        return;
                    }
                    if (m.hideOnMouseOver) {
                        m.delayedHide();
                    }
                };
            })(div, e, area);
        }
        // prevent event moving
        a.onmousedown = (function(div, e, area) {
            return function(ev) {
                if (typeof area.onmousedown === 'function') {
                    area.onmousedown(ev);
                }

                if (area.action === "Move" && e.isRow) {
                    var row = e.$.row;
                    var rowtools = e.calendar.internal.rowtools;

                    rowtools.startMoving(row);
                }

                var cancel = true;
                
                if (cancel) {
                    if (area.action === "Move" || area.action === "ResizeEnd" || area.action === "ResizeStart") {
                        return;
                    }
                    ev = ev || window.event;
                    ev.cancelBubble = true;
                }
            };
        })(div, e, area);
        a.onclick = (function(div, e, area) {
            return function(ev) {
                var ev = ev || window.event;
                switch (area.action) {
                    case "JavaScript":
                        var f = area.js;
                        if (typeof f === 'string') {
                            f = eval("0, " + area.js);
                        }
                        if (typeof f === 'function') {
                            f.call(this, e);
                        }
                        break;
                    case "ContextMenu":
                        var m = area.menu;
                        if (typeof m === 'string') {
                            m = eval(m);
                        }
                        if (m && m.show) {
                            m.show(e);
                        }
                        break;
                    case "CallBack":
                        alert("callback not implemented yet, id: " + area.id);
                        break;
                }
                ev.cancelBubble = true;
            };
        })(div, e, area);


        return a;
    };

    DayPilot.Areas.all = [];

    DayPilot.Areas.hideAreas = function(div, ev) {
        if (!div) {
            return;
        }

        if (!div || !div.active) {
            return;
        }

        var active = div.active;
        var areas = div.areas;

        if (active && active.children) {
            var ev = ev || window.event;
            if (ev) {
                var target = ev.toElement || ev.relatedTarget;
                if (~DayPilot.indexOf(active.children, target)) {
                    return;
                }
            }
        }

        if (!areas || areas.length === 0) {
            div.active = null;
            return;
        }

        DayPilot.de(areas);
        /*
        for (var i = 0; i < areas.length; i++) {
            var a = areas[i];
            div.removeChild(a);
        }*/

        div.active = null;
        div.areas = [];

        DayPilot.rfa(DayPilot.Areas.all, div);

        active.children = null;
    };

    DayPilot.Areas.hideAll = function(ev) {
        if (!DayPilot.Areas.all || DayPilot.Areas.all.length === 0) {
            return;
        }
        for (var i = 0; i < DayPilot.Areas.all.length; i++) {
            DayPilot.Areas.hideAreas(DayPilot.Areas.all[i], ev);
        }

    };
    
    DayPilot.Action = function(calendar, action, params, data) {
        this.calendar = calendar;
        this.isAction = true;
        this.action = action;
        this.params = params;
        this.data = data;

        this.notify = function() {
            calendar.internal.invokeEvent("Immediate", this.action, this.params, this.data);
        };

        this.auto = function() {
            calendar.internal.invokeEvent("Notify", this.action, this.params, this.data);
        };

        this.queue = function() {
            calendar.queue.add(this);
        };

        this.toJSON = function() {
            var json = {};
            json.name = this.action;
            json.params = this.params;
            json.data = this.data;

            return json;
        };

    };

    DayPilot.Selection = function(start, end, resource, root) {
        this.menuType = 'selection';  // for menu
        this.start = start.isDayPilotDate ? start : new DayPilot.Date(start);
        this.end = end.isDayPilotDate ? end : new DayPilot.Date(end);
        this.resource = resource;
        this.root = root;

        this.toJSON = function(key) {
            var json = {};
            json.start = this.start;
            json.end = this.end;
            json.resource = this.resource;

            return json;
        };
    };

    DayPilot.Link = function(data, calendar) {
        this.isLink = true;
        this.data = data;
        this.calendar = calendar;

        this.to = function() {
            return this.data.to;
        };

        this.from = function() {
            return this.data.from;
        };

        this.type = function() {
            return this.data.type;
        };

        this.id = function() {
            return this.data.id;
        };

        this.toJSON = function() {
            var json = {};
            json.from = this.data.from;
            json.to = this.data.to;
            json.id = this.data.id;
            json.type = this.data.type;
            return json;
        };
    };

    DayPilot.Event = function(data, calendar, part) {
        var e = this;
        this.calendar = calendar;
        this.data = data ? data : {};
        this.part = part ? part : {};

        // backwards compatibility, still accepts id in "value" 
        if (typeof this.data.id === 'undefined') {
            this.data.id = this.data.value;
        }

        var copy = {};
        var synced = ["id", "text", "start", "end", "resource"];

        this.isEvent = true;

        // internal
        this.temp = function() {
            if (copy.dirty) {
                return copy;
            }
            for (var i = 0; i < synced.length; i++) {
                copy[synced[i]] = e.data[synced[i]];
            }
            copy.dirty = true;
            return copy;

        };

        // internal
        // copies data object
        // used when the original state of the data is needed (notified EventMove etc.)
        this.copy = function() {
            var result = {};
            DayPilot.Util.copyProps(e.data, result);
            return result;
            /*
            for (var i = 0; i < synced.length; i++) {
                result[synced[i]] = e.data[synced[i]];
            }
            return result;
            */
        };

        this.commit = function() {
            if (!copy.dirty) {
                return;
            }

            for (var i = 0; i < synced.length; i++) {
                e.data[synced[i]] = copy[synced[i]];
            }

            copy.dirty = false;
        };

        this.dirty = function() {
            return copy.dirty;
        };

        this.id = function(val) {
            if (typeof val === 'undefined') {
                return e.data.id;
            }
            else {
                this.temp().id = val;
            }
        };
        // obsolete, use id() instead
        this.value = function(val) {
            if (typeof val === 'undefined') {
                return e.id();
            }
            else {
                e.id(val);
            }
        };
        this.text = function(val) {
            if (typeof val === 'undefined') {
                return e.data.text;
            }
            else {
                this.temp().text = val;
                this.client.innerHTML(val); // update the HTML automatically
            }
        };
        this.start = function(val) {
            if (typeof val === 'undefined') {
                return new DayPilot.Date(e.data.start);
            }
            else {
                this.temp().start = new DayPilot.Date(val);
            }
        };
        this.end = function(val) {
            if (typeof val === 'undefined') {
                if (calendar && calendar.eventEndSpec == "Date") {
                    return new DayPilot.Date(e.data.end).getDatePart().addDays(1);
                }
                return new DayPilot.Date(e.data.end);
            }
            else {
                this.temp().end = new DayPilot.Date(val);
            }
        };
        this.partStart = function() {
            return new DayPilot.Date(this.part.start);
        };
        this.partEnd = function() {
            return new DayPilot.Date(this.part.end);
        };
        this.row = function() {
            return this.resource();
        };
        
        this.allday = function() {
            if (typeof val === 'undefined') {
                return e.data.allday;
            }
            else {
                this.temp().allday = val;
            }            
        };
        
        // backwards compatibility, 7.3
        this.isAllDay = this.allday;

        this.resource = function(val) {
            if (typeof val === 'undefined') {
                return e.data.resource;
            }
            else { // it's a resource id
                this.temp().resource = val;
            }
        };

        this.recurrent = function() {
            return e.data.recurrent;
        };
        this.recurrentMasterId = function() {
            return e.data.recurrentMasterId;
        };
        this.useBox = function() {
            return this.part.box;
        };
        this.staticBubbleHTML = function() {
            return this.bubbleHtml();
        };
        this.bubbleHtml = function() {
            if (e.cache) {
                return e.cache.bubbleHtml || e.data.bubbleHtml;
            }
            return e.data.bubbleHtml;
        };
        this.tag = function(field) {
            var values = e.data.tag;
            if (!values) {
                return null;
            }
            if (typeof field === 'undefined') {
                return e.data.tag;
            }
            var fields = e.calendar.tagFields;
            var index = -1;
            for (var i = 0; i < fields.length; i++) {
                if (field === fields[i])
                    index = i;
            }
            if (index === -1) {
                throw "Field name not found.";
            }
            return values[index];
        };

        this.client = {};
        this.client.innerHTML = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.html !== "undefined") {
                    return e.cache.html;
                }
                if (typeof e.data.html !== "undefined") {
                    return e.data.html;
                }
                return e.data.text;
            }
            else {
                e.data.html = val;
            }
        };
        
        this.client.html = this.client.innerHTML;
        
        this.client.header = function(val) {
            if (typeof val === 'undefined') {
                return e.data.header;
            }
            else {
                e.data.header = val;
            }
        };
        
        this.client.cssClass = function(val) {
            if (typeof val === 'undefined') {
                return e.data.cssClass;
            }
            else {
                e.data.cssClass = val;
            }
        };
        this.client.toolTip = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.toolTip !== "undefined") {
                    return e.cache.toolTip;
                }
                return typeof e.data.toolTip !== 'undefined' ? e.data.toolTip : e.data.text;
            }
            else {
                e.data.toolTip = val;
            }
        };

        this.client.backColor = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.backColor !== "undefined") {
                    return e.cache.backColor;
                }
                return typeof e.data.backColor !== "undefined" ? e.data.backColor : e.calendar.eventBackColor;
            }
            else {
                e.data.backColor = val;
            }
        };
/*
        this.client.backColor = function(val) {
            if (typeof val === 'undefined') {
                //return typeof e.data.backColor !== "undefined" ? e.data.backColor : e.calendar.eventBackColor;
                return typeof e.data.backColor !== "undefined" ? e.data.backColor : e.calendar.eventBackColor;
            }
            else {
                e.data.backColor = val;
            }
        };
*/
        this.client.borderColor = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.borderColor !== "undefined") {
                    return e.cache.borderColor;
                }
                return typeof e.data.borderColor !== "undefined" ? e.data.borderColor : e.calendar.eventBorderColor;
            }
            else {
                e.data.borderColor = val;
            }
        };

        this.client.barColor = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.barColor !== "undefined") {
                    return e.cache.barColor;
                }
                return typeof e.data.barColor !== "undefined" ? e.data.barColor : e.calendar.durationBarColor;
            }
            else {
                e.data.barColor = val;
            }
        };

        this.client.barVisible = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.barHidden !== "undefined") {
                    return !e.cache.barHidden;
                }
                return e.calendar.durationBarVisible && !e.data.barHidden;
            }
            else {
                e.data.barHidden = !val;
            }
        };

        this.client.contextMenu = function(val) {
            if (typeof val === 'undefined') {
                if (e.oContextMenu) {
                    return e.oContextMenu;
                }
                return (e.data.contextMenu) ? eval(e.data.contextMenu) : null;  // might want to return the default context menu in the future
            }
            else {
                e.oContextMenu = val;
            }
        };

        this.client.moveEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.moveDisabled !== "undefined") {
                    return !e.cache.moveDisabled;
                }
                return e.calendar.eventMoveHandling !== 'Disabled' && !e.data.moveDisabled;
            }
            else {
                e.data.moveDisabled = !val;
            }
        };

        this.client.resizeEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.resizeDisabled !== "undefined") {
                    return !e.cache.resizeDisabled;
                }
                return e.calendar.eventResizeHandling !== 'Disabled' && !e.data.resizeDisabled;
            }
            else {
                e.data.resizeDisabled = !val;
            }
        };

        this.client.rightClickEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.rightClickDisabled !== "undefined") {
                    return !e.cache.rightClickDisabled;
                }
                return e.calendar.rightClickHandling !== 'Disabled' && !e.data.rightClickDisabled;
            }
            else {
                e.data.rightClickDisabled = !val;
            }
        };

        this.client.clickEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.clickDisabled !== "undefined") {
                    return !e.cache.clickDisabled;
                }
                return e.calendar.clickHandling !== 'Disabled' && !e.data.clickDisabled;
            }
            else {
                e.data.clickDisabled = !val;
            }
        };

        this.client.deleteEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.deleteDisabled !== "undefined") {
                    return !e.cache.deleteDisabled;
                }
                return e.calendar.eventDeleteHandling !== 'Disabled' && !e.data.deleteDisabled;
            }
            else {
                e.data.deleteDisabled = !val;
            }
        };
        
        this.client.doubleClickEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.doubleClickDisabled !== "undefined") {
                    return !e.cache.doubleClickDisabled;
                }
                return e.calendar.eventDoubleClickHandling !== 'Disabled' && !e.data.doubleClickDisabled;
            }
            else {
                e.data.doubleClickDisabled = !val;
            }
        };

        this.client.deleteClickEnabled = function(val) {
            if (typeof val === 'undefined') {
                if (e.cache && typeof e.cache.deleteDisabled !== "undefined") {
                    return !e.cache.deleteDisabled;
                }
                return e.calendar.eventDeleteHandling !== 'Disabled' && !e.data.deleteDisabled;
            }
            else {
                e.data.deleteDisabled = !val;
            }
        };

        this.toJSON = function(key) {
            var json = {};
            json.value = this.id(); // still sending it with the old name
            json.id = this.id();
            json.text = this.text();
            json.start = this.start().toJSON();
            json.end = this.end().toJSON();
            json.resource = this.resource();
            json.isAllDay = false;
            json.recurrentMasterId = this.recurrentMasterId();
            json.tag = {};

            if (e.calendar.tagFields) {
                var fields = e.calendar.tagFields;
                for (var i = 0; i < fields.length; i++) {
                    json.tag[fields[i]] = this.tag(fields[i]);
                }
            }

            return json;
        };
    };

    /**
     * A simple wrapper around task data.
     * @param data
     * @constructor
     */
    DayPilot.Task = function(data, calendar) {
        if (!data) {
            throw "Trying to initialize DayPilot.Task with null data parameter";
        }

        var e = this;

        var event = null; // reference to DayPilot.Event received from the Scheduler

        if (data instanceof DayPilot.Event) {
            event = data;
            this.data = data.data.task;
        }
        else if (data instanceof DayPilot.Task) {
            return data; // don't create a new object
        }
        else if (data.isTaskWrapper) {
            this.data = data.data;
        }
        else {
            this.data = data;
        }

        var copy = {};
        var synced = ["id", "text", "start", "end", "complete", "type"];

        this.isTask = true;
        this.calendar = calendar;

        // internal
        this.temp = function() {
            if (copy.dirty) {
                return copy;
            }
            for (var i = 0; i < synced.length; i++) {
                copy[synced[i]] = e.data[synced[i]];
            }
            copy.dirty = true;
            return copy;

        };

        // internal
        // copies data object
        // used when the original state of the data is needed (notified EventMove etc.)
        this.copy = function() {
            var result = {};
            DayPilot.Util.copyProps(e.data, result);
            return result;
        };

        this.commit = function() {
            if (!copy.dirty) {
                return;
            }

            for (var i = 0; i < synced.length; i++) {
                e.data[synced[i]] = copy[synced[i]];
            }

            copy.dirty = false;
        };

        this.dirty = function() {
            return copy.dirty;
        };

        this.id = function(val) {
            if (typeof val === 'undefined') {
                return e.data.id;
            }
            else {
                this.temp().id = val;
            }
        };
        this.text = function(val) {
            if (typeof val === 'undefined') {
                return e.data.text;
            }
            else {
                this.temp().text = val;
                this.client.innerHTML(val); // update the HTML automatically
            }
        };
        this.start = function(val) {
            if (typeof val === 'undefined') {
                return new DayPilot.Date(e.data.start);
            }
            else {
                this.temp().start = new DayPilot.Date(val);
            }
        };
        this.end = function(val) {
            if (typeof val === 'undefined') {
                if (calendar && calendar.eventEndSpec === "Date") {
                    return new DayPilot.Date(e.data.end).getDatePart().addDays(1);
                }
                return new DayPilot.Date(e.data.end);
            }
            else {
                this.temp().end = new DayPilot.Date(val);
            }
        };
        this.type = function(val) {
            if (typeof val === 'undefined') {
                if (event) {
                    return event.data.type;
                }
                return e.data.type;
            }
            else {
                this.temp().type = val;
            }
        };
        this.complete = function(val) {
            if (typeof val === 'undefined') {
                if (!e.data.complete) {
                    return 0;
                }
                return e.data.complete;
            }
            else {
                this.temp().complete = val;
            }
        };
        this.children = function() {
            var list = [];
            list.add = function(data) {
                var task = new DayPilot.Task(data);
                if (!this.data.children) {
                    this.data.children = [];
                }
                this.children.push(task.data);
            };
            for(var i = 0; this.data.children && i < this.data.children.length; i++) {
                list.push(new DayPilot.Task(this.data.children[i], calendar));
            }

            return list;
        };

        this.toJSON = function(key) {
            var json = {};
            json.id = this.id();
            json.text = this.text();
            json.start = this.start().toJSON();
            json.end = this.end().toJSON();
            json.type = this.type();
            json.tags = {};

            DayPilot.Util.copyProps(this.data.tags, json.tags);

            return json;
        }

        this.row = {};
        var row = this.row;

        row.expanded = function(val) {
            if (typeof val === 'undefined') {
                if (!e.data.row) {
                    return true;
                }
                return !e.data.row.collapsed;
            }
            else {
                if (!e.data.row) {
                    e.data.row = {};
                }
                if (!!e.data.row.collapsed  !== !val) {
                    calendar.internal.rowObjectForTaskData(e.data).toggle();
                }
                e.data.row.collapsed = !val;

                /*
                if (calendar) {
                    calendar.update();
                }
                */
            }
        };

        row.expand = function() {
            row.expanded(true);
        };

        row.collapse = function() {
            row.expanded(false);
        };

        row.toggle = function() {
            row.expanded(!row.expanded());
        };

    };


    /* JSON objects */
    /*
    DayPilot.EventData = function(e) {
        this.value = function() {
            return id;
        };
        this.tag = function() {
            return null;
        };
        this.start = function() {
            return new Date(0);
        };
        this.end = function() {
            return new Date(duration * 1000);
        };
        this.text = function() {
            return text;
        };
        this.isAllDay = function() {
            return false;
        };
        this.allday = this.isAllDay;
    };
*/


    /* XMLHttpRequest */
    
    DayPilot.request = function(url, callback, postData, errorCallback) {
        var req = DayPilot.createXmlHttp();
        if (!req) {
            return;
        }

        req.open("POST", url, true);
        req.setRequestHeader('Content-type', 'text/plain');
        req.onreadystatechange = function() {
            if (req.readyState !== 4)
                return;
            if (req.status !== 200 && req.status !== 304) {
                if (errorCallback) {
                    errorCallback(req);
                }
                else {
                    if (window.console) { console.log('HTTP error ' + req.status); }
                }
                return;
            }
            callback(req);
        };
        if (req.readyState === 4) {
            return;
        }
        if (typeof postData === 'object') {
            postData = DayPilot.JSON.stringify(postData);
        }
        req.send(postData);
    };

    DayPilot.ajax = function(object) {
        var req = DayPilot.createXmlHttp();
        if (!req) {
            return;
        }
        
        var method = object.method || "GET";
        var success = object.success || function() {};
        var error = object.error || function() {};
        var data = object.data;
        var url = object.url;

        req.open(method, url, true);
        req.setRequestHeader('Content-type', 'text/plain');
        req.onreadystatechange = function() {
            if (req.readyState !== 4)
                return;
            if (req.status !== 200 && req.status !== 304) {
                if (error) {
                    var args = {};
                    args.request = req;
                    error(args);
                }
                else {
                    if (window.console) { console.log('HTTP error ' + req.status); }
                }
                return;
            }
            var args = {};
            args.request = req;
            success(args);
        };
        if (req.readyState === 4) {
            return;
        }
        if (typeof data === 'object') {
            data = DayPilot.JSON.stringify(postData);
        }
        req.send(data);
    };

    DayPilot.createXmlHttp = function() {
        var xmlHttp;
        try {
            xmlHttp = new XMLHttpRequest();
        }
        catch (e) {
            try {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (e) {
            }
        }
        return xmlHttp;
    };


    DayPilot.TimeSpan = function(ticks) {
        var day = 1000*60*60*24;
        var hour = 1000*60*60;
        var minute = 1000*60;
        var second = 1000;

        this.ticks = ticks;

        this.toString = function(pattern) {
            if (!pattern) {
                return this.days() + "." + this.hours() + ":" + this.minutes() + ":" + this.seconds() + "." + this.milliseconds();
            }

            var minutes = this.minutes();
            minutes = (minutes < 10 ? "0" : "") + minutes;

            // dumb replacement
            var result = pattern;
            result = result.replace("mm", minutes);
            result = result.replace("m", this.minutes());
            result = result.replace("H", this.hours());
            result = result.replace("h", this.hours());
            result = result.replace("d", this.days());
            result = result.replace("s", this.seconds());
            return result;
        };

        this.totalHours = function() {
            return ticks / hour;
        };

        this.totalDays = function() {
            return ticks / day;
        };

        this.totalHours = function() {
            return ticks / hour;
        };

        this.totalMinutes = function() {
            return ticks / minute;
        };

        this.totalSeconds = function() {
            return ticks / seconds;
        };

        this.days = function() {
            return Math.floor(this.totalDays());
        };

        this.hours = function() {
            var hourPartTicks = ticks - this.days()*day;
            return Math.floor(hourPartTicks/hour);
        };

        this.minutes = function() {
            var minutePartTicks = ticks - Math.floor(this.totalHours()) * hour;
            return Math.floor(minutePartTicks/minute);
        };

        this.seconds = function() {
            var secondPartTicks = ticks - Math.floor(this.totalMinutes()) * minute;
            return Math.floor(secondPartTicks/second);
        };

        this.milliseconds = function() {
            return ticks % second;
        };

    };

    /* Date utils */

    // DayPilot.Date class
    /* Constructor signatures:
     
     -- new DayPilot.Date(date, isLocal)
     date - JavaScript Date object
     isLocal - true if the local time should be taken from date, otherwise GMT base is used
     
     -- new DayPilot.Date() - returns now, using local date
     
     -- new DayPilot.Date(string)
     string - date in ISO 8601 format, e.g. 2009-01-01T00:00:00
     
     */
    DayPilot.Date = function(date, isLocal) {

        if (typeof date === 'undefined') {  // date not set, use NOW
            this.isDayPilotDate = true; // allow class detection
            this.d = DayPilot.Date.fromLocal();
            this.ticks = this.d.getTime();
            this.value = this.toStringSortable();
            return;
        }

        if (date.isDayPilotDate) { // it's already DayPilot.Date object, return it (no copy)
            return date;
        }

        var cache = DayPilot.Date.Cache.Ctor;
        if (cache[date]) {
            return cache[date];
        }

        if (typeof date === "string") {
            var result = DayPilot.Date.fromStringSortable(date);
            cache[date] = result;
            return result;
        }
        
        if (typeof date === "number") {
            return new DayPilot.Date(new Date(date));
        }

        if (!date.getFullYear) {  // it's not a date object, fail
            throw "date parameter is not a Date object: " + date;
        }

        if (isLocal) {  // if the date passed should be read as local date
            this.isDayPilotDate = true; // allow class detection
            this.d = DayPilot.Date.fromLocal(date);
            this.ticks = this.d.getTime();
        }
        else {  // should be read as GMT
            this.isDayPilotDate = true; // allow class detection
            this.d = date;
            this.ticks = this.d.getTime();
        }
        this.value = this.toStringSortable();
    };

    DayPilot.Date.Cache = {};
    DayPilot.Date.Cache.Parsing = {};
    DayPilot.Date.Cache.Ctor = {};

/*
    DayPilot.Date.prototype.toJSON = function() {
        return this.value;
    };
*/
    DayPilot.Date.prototype.addDays = function(days) {
        return new DayPilot.Date(DayPilot.Date.addDays(this.d, days));
    };

    DayPilot.Date.prototype.addHours = function(hours) {
        return this.addTime(hours * 60 * 60 * 1000);
    };

    DayPilot.Date.prototype.addMilliseconds = function(millis) {
        return this.addTime(millis);
    };

    DayPilot.Date.prototype.addMinutes = function(minutes) {
        return this.addTime(minutes * 60 * 1000);
    };

    DayPilot.Date.prototype.addMonths = function(months) {
        return new DayPilot.Date(DayPilot.Date.addMonths(this.d, months));
    };

    DayPilot.Date.prototype.addSeconds = function(seconds) {
        return this.addTime(seconds * 1000);
    };

    DayPilot.Date.prototype.addTime = function(ticks) {
        return new DayPilot.Date(DayPilot.Date.addTime(this.d, ticks));
    };

    DayPilot.Date.prototype.addYears = function(years) {
        var n = this.clone();
        n.d.setUTCFullYear(this.getYear() + years);
        return n;
    };

    DayPilot.Date.prototype.clone = function() {
        return new DayPilot.Date(DayPilot.Date.clone(this.d));
    };

    DayPilot.Date.prototype.dayOfWeek = function() {
        return this.d.getUTCDay();
    };
    
    DayPilot.Date.prototype.getDayOfWeek = function() {
        return this.d.getUTCDay();
    };

    DayPilot.Date.prototype.daysInMonth = function() {
        return DayPilot.Date.daysInMonth(this.d);
    };

    DayPilot.Date.prototype.dayOfYear = function() {
        return Math.ceil((this.getDatePart().getTime() - this.firstDayOfYear().getTime()) / 86400000) + 1;
    };

    DayPilot.Date.prototype.equals = function(another) {
        if (another === null) {
            return false;
        }
        if (another.isDayPilotDate) {
            return DayPilot.Date.equals(this.d, another.d);
        }
        else {
            throw "The parameter must be a DayPilot.Date object (DayPilot.Date.equals())";
        }
    };

    DayPilot.Date.prototype.firstDayOfMonth = function() {
        var utc = DayPilot.Date.firstDayOfMonth(this.getYear(), this.getMonth() + 1);
        return new DayPilot.Date(utc);
    };

    DayPilot.Date.prototype.firstDayOfYear = function() {
        var year = this.getYear();
        var d = new Date();
        d.setUTCFullYear(year, 0, 1);
        d.setUTCHours(0);
        d.setUTCMinutes(0);
        d.setUTCSeconds(0);
        d.setUTCMilliseconds(0);
        return new DayPilot.Date(d);
    };

    DayPilot.Date.prototype.firstDayOfWeek = function(weekStarts) {
        var utc = DayPilot.Date.firstDayOfWeek(this.d, weekStarts);
        return new DayPilot.Date(utc);
    };

    DayPilot.Date.prototype.getDay = function() {
        return this.d.getUTCDate();
    };

    DayPilot.Date.prototype.getDatePart = function() {
        return new DayPilot.Date(DayPilot.Date.getDate(this.d));
    };

    DayPilot.Date.prototype.getYear = function() {
        return this.d.getUTCFullYear();
    };

    DayPilot.Date.prototype.getHours = function() {
        return this.d.getUTCHours();
    };

    DayPilot.Date.prototype.getMilliseconds = function() {
        return this.d.getUTCMilliseconds();
    };

    DayPilot.Date.prototype.getMinutes = function() {
        return this.d.getUTCMinutes();
    };

    DayPilot.Date.prototype.getMonth = function() {
        return this.d.getUTCMonth();
    };

    DayPilot.Date.prototype.getSeconds = function() {
        return this.d.getUTCSeconds();
    };

    DayPilot.Date.prototype.getTotalTicks = function() {
        return this.getTime();
    };

    // undocumented
    DayPilot.Date.prototype.getTime = function() {
        /*
         if (typeof this.ticks !== 'number') {
         throw "Uninitialized DayPilot.Date (internal error)";
         }*/
        return this.ticks;
    };

    DayPilot.Date.prototype.getTimePart = function() {
        return DayPilot.Date.getTime(this.d);
    };

    DayPilot.Date.prototype.lastDayOfMonth = function() {
        var utc = DayPilot.Date.lastDayOfMonth(this.getYear(), this.getMonth() + 1);
        return new DayPilot.Date(utc);
    };

    DayPilot.Date.prototype.weekNumber = function() {
        var first = this.firstDayOfYear();
        var days = (this.getTime() - first.getTime()) / 86400000;
        return Math.ceil((days + first.dayOfWeek() + 1) / 7);
    };

    DayPilot.Date.prototype.local = function() {
        if (typeof this.offset === 'undefined') {
            return new DayPilot.Date(this.d);
        }
        return this.addMinutes(this.offset);
    };

    // ISO 8601
    DayPilot.Date.prototype.weekNumberISO = function() {
        var thursdayFlag = false;
        var dayOfYear = this.dayOfYear();

        var startWeekDayOfYear = this.firstDayOfYear().dayOfWeek();
        var endWeekDayOfYear = this.firstDayOfYear().addYears(1).addDays(-1).dayOfWeek();
        //int startWeekDayOfYear = new DateTime(date.getYear(), 1, 1).getDayOfWeekOrdinal();
        //int endWeekDayOfYear = new DateTime(date.getYear(), 12, 31).getDayOfWeekOrdinal();

        if (startWeekDayOfYear === 0) {
            startWeekDayOfYear = 7;
        }
        if (endWeekDayOfYear === 0) {
            endWeekDayOfYear = 7;
        }

        var daysInFirstWeek = 8 - (startWeekDayOfYear);

        if (startWeekDayOfYear === 4 || endWeekDayOfYear === 4) {
            thursdayFlag = true;
        }

        var fullWeeks = Math.ceil((dayOfYear - (daysInFirstWeek)) / 7.0);

        var weekNumber = fullWeeks;

        if (daysInFirstWeek >= 4) {
            weekNumber = weekNumber + 1;
        }

        if (weekNumber > 52 && !thursdayFlag) {
            weekNumber = 1;
        }

        if (weekNumber === 0) {
            weekNumber = this.firstDayOfYear().addDays(-1).weekNumberISO(); //weekNrISO8601(new DateTime(date.getYear() - 1, 12, 31));
        }

        return weekNumber;

    };

    DayPilot.Date.prototype.toDateLocal = function() {
        return DayPilot.Date.toLocal(this.d);
    };

    DayPilot.Date.prototype.toJSON = function() {
        return this.value;
    };

    // formatting and languages needed here
    DayPilot.Date.prototype.toString = function(pattern, locale) {
        if (typeof pattern === 'undefined') {
            return this.toStringSortable();
        }
        return new Pattern(pattern, locale).print(this);
    };

    DayPilot.Date.prototype.toStringSortable = function() {
        return DayPilot.Date.toStringSortable(this.d);
    };

    /* static functions, return DayPilot.Date object */


    // returns null if parsing was not successful
    DayPilot.Date.parse = function(str, pattern) {
        var p = new Pattern(pattern);
        return p.parse(str);
    };

    DayPilot.Date.fromStringSortable = function(string) {
        var result;
        //var datetime = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})(?:(\+|-)(\d{2}):(\d{2}))?$/;
        //var date = /^(\d{4})-(\d{2})-(\d{2})$/;

        //var isValidDateTime = datetime.test(string);
        //var isValidDate = date.test(string);
        //var isValid = isValidDateTime || isValidDate;

        // 2010-01-01  --- 10
        // 
        // 2010-01-01T00:00:00  --- 19
        // 2010-01-01T00:00:00+00:00   --- 25

        if (!string) {
            throw "Can't create DayPilot.Date from empty string";
        }

        var len = string.length;
        var date = len === 10;
        var datetime = len = 19;
        var datetimetz = len === 25;

        if (!date && !datetime && !datetimetz) {
            throw "Invalid string format (use '2010-01-01', '2010-01-01T00:00:00', or '2010-01-01T00:00:00+00:00'.";
        }

        if (DayPilot.Date.Cache.Parsing[string]) {
            return DayPilot.Date.Cache.Parsing[string];
        }

        var year = string.substring(0, 4);
        var month = string.substring(5, 7);
        var day = string.substring(8, 10);

        var d = new Date();
        d.setUTCFullYear(year, month - 1, day);

        if (date) {
            d.setUTCHours(0);
            d.setUTCMinutes(0);
            d.setUTCSeconds(0);
            d.setUTCMilliseconds(0);
            result = new DayPilot.Date(d);
            DayPilot.Date.Cache.Parsing[string] = result;
            return result;
        }

        var hours = string.substring(11, 13);
        var minutes = string.substring(14, 16);
        var seconds = string.substring(17, 19);

        d.setUTCHours(hours);
        d.setUTCMinutes(minutes);
        d.setUTCSeconds(seconds);
        d.setUTCMilliseconds(0);
        var result = new DayPilot.Date(d);

        if (datetime) {
            DayPilot.Date.Cache.Parsing[string] = result;
            return result;
        }

        var tzdir = string[20];
        var tzhours = string.substring(21, 23);
        var tzminutes = string.substring(24);
        var tzoffset = tzhours * 60 + tzminutes;
        if (tzdir === "-") {
            tzoffset = -tzoffset;
        }
        result = result.addMinutes(-tzoffset); // get UTC base
        result.offset = offset; // store offset

        DayPilot.Date.Cache.Parsing[string] = result;
        return result;
    };

    /* internal functions, all operate with GMT base of the date object 
     (except of DayPilot.Date.fromLocal()) */

    DayPilot.Date.addDays = function(date, days) {
        var d = new Date();
        d.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
        return d;
    };

    DayPilot.Date.addMinutes = function(date, minutes) {
        var d = new Date();
        d.setTime(date.getTime() + minutes * 60 * 1000);
        return d;
    };

    DayPilot.Date.addMonths = function(date, months) {
        if (months === 0)
            return date;

        var y = date.getUTCFullYear();
        var m = date.getUTCMonth() + 1;

        if (months > 0) {
            while (months >= 12) {
                months -= 12;
                y++;
            }
            if (months > 12 - m) {
                y++;
                m = months - (12 - m);
            }
            else {
                m += months;
            }
        }
        else {
            while (months <= -12) {
                months += 12;
                y--;
            }
            if (m <= months) {  // 
                y--;
                m = 12 - (months + m);
            }
            else {
                m = m + months;
            }
        }

        var d = DayPilot.Date.clone(date);
        d.setUTCFullYear(y);
        d.setUTCMonth(m - 1);

        return d;
    };

    DayPilot.Date.addTime = function(date, time) {
        var d = new Date();
        d.setTime(date.getTime() + time);
        return d;
    };

    DayPilot.Date.clone = function(original) {
        var d = new Date();
        return DayPilot.Date.dateFromTicks(original.getTime());
    };


    // rename candidate: diffDays
    DayPilot.Date.daysDiff = function(first, second) {
        if (first.getTime() > second.getTime()) {
            return null;
        }

        var i = 0;
        var fDay = DayPilot.Date.getDate(first);
        var sDay = DayPilot.Date.getDate(second);

        while (fDay < sDay) {
            fDay = DayPilot.Date.addDays(fDay, 1);
            i++;
        }

        return i;
    };

    DayPilot.Date.daysInMonth = function(year, month) {  // accepts also: function(date)
        if (year.getUTCFullYear) { // it's a date object
            month = year.getUTCMonth() + 1;
            year = year.getUTCFullYear();
        }

        var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        if (month !== 2)
            return m[month - 1];
        if (year % 4 !== 0)
            return m[1];
        if (year % 100 === 0 && year % 400 !== 0)
            return m[1];
        return m[1] + 1;
    };

    DayPilot.Date.daysSpan = function(first, second) {
        if (first.getTime() === second.getTime()) {
            return 0;
        }

        var diff = DayPilot.Date.daysDiff(first, second);

        if (DayPilot.Date.equals(second, DayPilot.Date.getDate(second))) {
            diff--;
        }

        return diff;
    };

    DayPilot.Date.diff = function(first, second) { // = first - second
        if (!(first && second && first.getTime && second.getTime)) {
            throw "Both compared objects must be Date objects (DayPilot.Date.diff).";
        }

        return first.getTime() - second.getTime();
    };

    DayPilot.Date.equals = function(first, second) {
        return first.getTime() === second.getTime();
    };

    DayPilot.Date.fromLocal = function(localDate) {
        if (!localDate) {
            localDate = new Date();
        }

        var d = new Date();
        d.setUTCFullYear(localDate.getFullYear(), localDate.getMonth(), localDate.getDate());
        d.setUTCHours(localDate.getHours());
        d.setUTCMinutes(localDate.getMinutes());
        d.setUTCSeconds(localDate.getSeconds());
        d.setUTCMilliseconds(localDate.getMilliseconds());
        return d;
    };

    DayPilot.Date.firstDayOfMonth = function(year, month) {
        var d = new Date();
        d.setUTCFullYear(year, month - 1, 1);
        d.setUTCHours(0);
        d.setUTCMinutes(0);
        d.setUTCSeconds(0);
        d.setUTCMilliseconds(0);
        return d;
    };

    DayPilot.Date.firstDayOfWeek = function(d, weekStarts) {
        var weekStarts = weekStarts || 0;
        var day = d.getUTCDay();
        while (day !== weekStarts) {
            d = DayPilot.Date.addDays(d, -1);
            day = d.getUTCDay();
        }
        return d;
    };


    // rename candidate: fromTicks
    DayPilot.Date.dateFromTicks = function(ticks) {
        var d = new Date();
        d.setTime(ticks);
        return d;
    };

    // rename candidate: getDatePart
    DayPilot.Date.getDate = function(original) {
        var d = DayPilot.Date.clone(original);
        d.setUTCHours(0);
        d.setUTCMinutes(0);
        d.setUTCSeconds(0);
        d.setUTCMilliseconds(0);
        return d;
    };

    DayPilot.Date.getStart = function(year, month, weekStarts) {  // gets the first days of week where the first day of month occurs
        var fdom = DayPilot.Date.firstDayOfMonth(year, month);
        var d = DayPilot.Date.firstDayOfWeek(fdom, weekStarts);
        return d;
    };

    // rename candidate: getTimePart
    DayPilot.Date.getTime = function(original) {
        var date = DayPilot.Date.getDate(original);

        return DayPilot.Date.diff(original, date);
    };

    // rename candidate: toHourString
    DayPilot.Date.hours = function(date, use12) {

        var minute = date.getUTCMinutes();
        if (minute < 10)
            minute = "0" + minute;


        var hour = date.getUTCHours();
        //if (hour < 10) hour = "0" + hour;

        if (use12) {
            var am = hour < 12;
            var hour = hour % 12;
            if (hour === 0) {
                hour = 12;
            }
            var suffix = am ? "AM" : "PM";
            return hour + ':' + minute + ' ' + suffix;
        }
        else {
            return hour + ':' + minute;
        }
    };

    DayPilot.Date.lastDayOfMonth = function(year, month) {
        var d = DayPilot.Date.firstDayOfMonth(year, month);
        var length = DayPilot.Date.daysInMonth(year, month);
        d.setUTCDate(length);
        return d;
    };

    DayPilot.Date.max = function(first, second) {
        if (first.getTime() > second.getTime()) {
            return first;
        }
        else {
            return second;
        }
    };

    DayPilot.Date.min = function(first, second) {
        if (first.getTime() < second.getTime()) {
            return first;
        }
        else {
            return second;
        }
    };

    DayPilot.Date.today = function() {
        var relative = new Date();
        var d = new Date();
        d.setUTCFullYear(relative.getFullYear());
        d.setUTCMonth(relative.getMonth());
        d.setUTCDate(relative.getDate());

        return d;
    };

    DayPilot.Date.toLocal = function(date) {
        if (!date) {
            date = new Date();
        }

        var d = new Date();
        d.setFullYear(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate());
        d.setHours(date.getUTCHours());
        d.setMinutes(date.getUTCMinutes());
        d.setSeconds(date.getUTCSeconds());
        d.setMilliseconds(date.getUTCMilliseconds());
        return d;
    };


    DayPilot.Date.toStringSortable = function(date) {
        if (date.isDayPilotDate) {
            return date.toStringSortable();
        }

        var d = date;
        var second = d.getUTCSeconds();
        if (second < 10)
            second = "0" + second;
        var minute = d.getUTCMinutes();
        if (minute < 10)
            minute = "0" + minute;
        var hour = d.getUTCHours();
        if (hour < 10)
            hour = "0" + hour;
        var day = d.getUTCDate();
        if (day < 10)
            day = "0" + day;
        var month = d.getUTCMonth() + 1;
        if (month < 10)
            month = "0" + month;
        var year = d.getUTCFullYear();

        if (year <= 0) {
            throw "The minimum year supported is 1.";
        }
        if (year < 10) {
            year = "000" + year;
        }
        else if (year < 100) {
            year = "00" + year;
        }
        else if (year < 1000) {
            year = "0" + year;
        }

        return year + "-" + month + "-" + day + 'T' + hour + ":" + minute + ":" + second;
    };

    var Pattern = function(pattern, locale) {
        if (typeof locale === "string") {
            locale = DayPilot.Locale.find(locale);
        }
        var locale = locale || DayPilot.Locale.US;
        var all = [
            {"seq": "yyyy", "expr": "[0-9]{4,4\u007d", "str": function(d) {
                    return d.getYear();
                }},
            {"seq": "MMMM", "expr": "[a-z]*", "str": function(d) {
                    var r = locale.monthNames[d.getMonth()];
                    return r;
                }},
            {"seq": "MMM", "expr": "[a-z]*", "str": function(d) {
                    var r = locale.monthNamesShort[d.getMonth()];
                    return r;
                }},
            {"seq": "MM", "expr": "[0-9]{2,2\u007d", "str": function(d) {
                    var r = d.getMonth() + 1;
                    return r < 10 ? "0" + r : r;
                }},
            {"seq": "M", "expr": "[0-9]{1,2\u007d", "str": function(d) {
                    var r = d.getMonth() + 1;
                    return r;
                }},
            {"seq": "dddd", "expr": "[a-z]*", "str": function(d) {
                    var r = locale.dayNames[d.getDayOfWeek()];
                    return r;
                }},
            {"seq": "ddd", "expr": "[a-z]*", "str": function(d) {
                    var r = locale.dayNamesShort[d.getDayOfWeek()];
                    return r;
                }},
            {"seq": "dd", "expr": "[0-9]{2,2\u007d", "str": function(d) {
                    var r = d.getDay();
                    return r < 10 ? "0" + r : r;
                }},
            {"seq": "d", "expr": "[0-9]{1,2\u007d", "str": function(d) {
                    var r = d.getDay();
                    return r;
                }},
            {"seq": "m", "expr": "[0-9]{1,2\u007d", "str": function(d) {
                    var r = d.getMinutes();
                    return r;
                }},
            {"seq": "mm", "expr": "[0-9]{2,2\u007d", "str": function(d) {
                    var r = d.getMinutes();
                    return r < 10 ? "0" + r : r;
                }},
            {"seq": "H", "expr": "[0-9]{1,2\u007d", "str": function(d) {
                    var r = d.getHours();
                    return r;
                }},
            {"seq": "HH", "expr": "[0-9]{2,2\u007d", "str": function(d) {
                    var r = d.getHours();
                    return r < 10 ? "0" + r : r;
                }},
            {"seq": "h", "expr": "[0-9]{1,2\u007d", "str": function(d) {
                    var hour = d.getHours();
                    var hour = hour % 12;
                    if (hour === 0) {
                        hour = 12;
                    }
                    return hour;
                }},
            {"seq": "hh", "expr": "[0-9]{2,2\u007d", "str": function(d) {
                    var hour = d.getHours();
                    var hour = hour % 12;
                    if (hour === 0) {
                        hour = 12;
                    }
                    var r = hour;
                    return r < 10 ? "0" + r : r;
                }},
            {"seq": "tt", "expr": "(AM|PM)", "str": function(d) {
                    var hour = d.getHours();
                    var am = hour < 12;
                    return am ? "AM" : "PM";
                }},
            {"seq": "s", "expr": "[0-9]{1,2\u007d", "str": function(d) {
                    var r = d.getSeconds();
                    return r;
                }},
            {"seq": "ss", "expr": "[0-9]{2,2\u007d", "str": function(d) {
                    var r = d.getSeconds();
                    return r < 10 ? "0" + r : r;
                }}
        ];

        var escapeRegex = function(text) {
            return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
        };

        this.init = function() {
            this.year = this.findSequence("yyyy");
            this.month = this.findSequence("MM") || this.findSequence("M");
            this.day = this.findSequence("dd") || this.findSequence("d");

            this.hours = this.findSequence("HH") || this.findSequence("H");
            this.minutes = this.findSequence("mm") || this.findSequence("m");
            this.seconds = this.findSequence("ss") || this.findSequence("s");
        };

        this.findSequence = function(seq) {

            var index = pattern.indexOf(seq);
            if (index === -1) {
                return null;
            }
            return {
                "findValue": function(input) {
                    var prepared = escapeRegex(pattern);
                    for (var i = 0; i < all.length; i++) {
                        var len = all[i].length;
                        var pick = (seq === all[i].seq);
                        //var expr = "";
                        var expr = all[i].expr;
                        if (pick) {
                            expr = "(" + expr + ")";
                        }
                        prepared = prepared.replace(all[i].seq, expr);
                    }
                    
                    try {
                        var r = new RegExp(prepared);
                        var array = r.exec(input);
                        if (!array) {
                            return null;
                        }
                        return parseInt(array[1]);
                    }
                    catch (e) {
                        throw "unable to create regex from: " + prepared;
                    }
                }
            };
        };

        this.print = function(date) {
            // always recompiles the pattern

            var find = function(t) {
                for (var i = 0; i < all.length; i++) {
                    if (all[i].seq === t) {
                        return all[i];
                    }
                }
                return null;
            };

            var eos = pattern.length <= 0;
            var pos = 0;
            var components = [];

            while (!eos) {
                var rem = pattern.substring(pos);
                var matches = /(.)\1*/.exec(rem);
                if (matches && matches.length > 0) {
                    var match = matches[0];
                    var q = find(match);
                    if (q) {
                        components.push(q);
                    }
                    else {
                        components.push(match);
                    }
                    pos += match.length;
                    eos = pattern.length <= pos;
                }
                else {
                    eos = true;
                }
            }

            // resolve placeholders
            for (var i = 0; i < components.length; i++) {
                var c = components[i];
                if (typeof c !== 'string') {
                    components[i] = c.str(date);
                }
            }

            return components.join("");
        };



        this.parse = function(input) {

            var year = this.year.findValue(input);
            if (!year) {
                return null; // unparseable
            }

            var month = this.month.findValue(input);
            var day = this.day.findValue(input);

            var hours = this.hours ? this.hours.findValue(input) : 0;
            var minutes = this.minutes ? this.minutes.findValue(input) : 0;
            var seconds = this.seconds ? this.seconds.findValue(input) : 0;

            var d = new Date();
            d.setUTCFullYear(year, month - 1, day);
            d.setUTCHours(hours);
            d.setUTCMinutes(minutes);
            d.setUTCSeconds(seconds);
            d.setUTCMilliseconds(0);

            return new DayPilot.Date(d);
        };

        this.init();

    };

    DayPilot.Action = function(calendar, action, params, data) {
        this.calendar = calendar;
        this.isAction = true;
        this.action = action;
        this.params = params;
        this.data = data;

        this.notify = function() {
            calendar.invokeEvent("Immediate", this.action, this.params, this.data);
        };

        this.auto = function() {
            calendar.invokeEvent("Notify", this.action, this.params, this.data);
        };

        this.queue = function() {
            calendar.queue.add(this);
        };


        this.toJSON = function() {
            var json = {};
            json.name = this.action;
            json.params = this.params;
            json.data = this.data;

            return json;
        };

    };

    DayPilot.Locale = function(id, config) {
        this.id = id;
        this.dayNames = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        this.dayNamesShort = ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"];
        this.monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        this.monthNamesShort  = ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
        this.datePattern = "M/d/yyyy";
        this.timePattern = "H:mm";
        this.dateTimePattern = "M/d/yyyy H:mm";
        this.timeFormat = "Clock12Hours";
        this.weekStarts = 0; // Sunday

        if (config) {
            for (var name in config) {
                this[name] = config[name];
            }
        }
    };

    DayPilot.Locale.all = {};

    DayPilot.Locale.find = function(id) {
        if (!id) {
            return null;
        }
        var normalized = id.toLowerCase();
        if (normalized.length > 2) {
            normalized = DayPilot.Util.replaceCharAt(normalized, 2, '-');
        }
        return DayPilot.Locale.all[normalized];
    };
    
    DayPilot.Locale.register = function(locale) {
        DayPilot.Locale.all[locale.id] = locale;
    };

    DayPilot.Locale.register(new DayPilot.Locale('ca-es', {'dayNames':['diumenge','dilluns','dimarts','dimecres','dijous','divendres','dissabte'],'dayNamesShort':['dg','dl','dt','dc','dj','dv','ds'],'monthNames':['gener','febrer','març','abril','maig','juny','juliol','agost','setembre','octubre','novembre','desembre',''],'monthNamesShort':['gen.','febr.','març','abr.','maig','juny','jul.','ag.','set.','oct.','nov.','des.',''],'timePattern':'H:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('cs-cz', {'dayNames':['neděle','pondělí','úterý','středa','čtvrtek','pátek','sobota'],'dayNamesShort':['ne','po','út','st','čt','pá','so'],'monthNames':['leden','únor','březen','duben','květen','červen','červenec','srpen','září','říjen','listopad','prosinec',''],'monthNamesShort':['I','II','III','IV','V','VI','VII','VIII','IX','X','XI','XII',''],'timePattern':'H:mm','datePattern':'d. M. yyyy','dateTimePattern':'d. M. yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('da-dk', {'dayNames':['søndag','mandag','tirsdag','onsdag','torsdag','fredag','lørdag'],'dayNamesShort':['sø','ma','ti','on','to','fr','lø'],'monthNames':['januar','februar','marts','april','maj','juni','juli','august','september','oktober','november','december',''],'monthNamesShort':['jan','feb','mar','apr','maj','jun','jul','aug','sep','okt','nov','dec',''],'timePattern':'HH:mm','datePattern':'dd-MM-yyyy','dateTimePattern':'dd-MM-yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('de-at', {'dayNames':['Sonntag','Montag','Dienstag','Mittwoch','Donnerstag','Freitag','Samstag'],'dayNamesShort':['So','Mo','Di','Mi','Do','Fr','Sa'],'monthNames':['Jänner','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember',''],'monthNamesShort':['Jän','Feb','Mär','Apr','Mai','Jun','Jul','Aug','Sep','Okt','Nov','Dez',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('de-ch', {'dayNames':['Sonntag','Montag','Dienstag','Mittwoch','Donnerstag','Freitag','Samstag'],'dayNamesShort':['So','Mo','Di','Mi','Do','Fr','Sa'],'monthNames':['Januar','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember',''],'monthNamesShort':['Jan','Feb','Mrz','Apr','Mai','Jun','Jul','Aug','Sep','Okt','Nov','Dez',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('de-de', {'dayNames':['Sonntag','Montag','Dienstag','Mittwoch','Donnerstag','Freitag','Samstag'],'dayNamesShort':['So','Mo','Di','Mi','Do','Fr','Sa'],'monthNames':['Januar','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember',''],'monthNamesShort':['Jan','Feb','Mrz','Apr','Mai','Jun','Jul','Aug','Sep','Okt','Nov','Dez',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('de-lu', {'dayNames':['Sonntag','Montag','Dienstag','Mittwoch','Donnerstag','Freitag','Samstag'],'dayNamesShort':['So','Mo','Di','Mi','Do','Fr','Sa'],'monthNames':['Januar','Februar','März','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember',''],'monthNamesShort':['Jan','Feb','Mrz','Apr','Mai','Jun','Jul','Aug','Sep','Okt','Nov','Dez',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('en-au', {'dayNames':['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'],'dayNamesShort':['Su','Mo','Tu','We','Th','Fr','Sa'],'monthNames':['January','February','March','April','May','June','July','August','September','October','November','December',''],'monthNamesShort':['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec',''],'timePattern':'h:mm tt','datePattern':'d/MM/yyyy','dateTimePattern':'d/MM/yyyy h:mm tt','timeFormat':'Clock12Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('en-ca', {'dayNames':['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'],'dayNamesShort':['Su','Mo','Tu','We','Th','Fr','Sa'],'monthNames':['January','February','March','April','May','June','July','August','September','October','November','December',''],'monthNamesShort':['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec',''],'timePattern':'h:mm tt','datePattern':'yyyy-MM-dd','dateTimePattern':'yyyy-MM-dd h:mm tt','timeFormat':'Clock12Hours','weekStarts':0}));
    DayPilot.Locale.register(new DayPilot.Locale('en-gb', {'dayNames':['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'],'dayNamesShort':['Su','Mo','Tu','We','Th','Fr','Sa'],'monthNames':['January','February','March','April','May','June','July','August','September','October','November','December',''],'monthNamesShort':['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec',''],'timePattern':'HH:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('en-us', {'dayNames':['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'],'dayNamesShort':['Su','Mo','Tu','We','Th','Fr','Sa'],'monthNames':['January','February','March','April','May','June','July','August','September','October','November','December',''],'monthNamesShort':['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec',''],'timePattern':'h:mm tt','datePattern':'M/d/yyyy','dateTimePattern':'M/d/yyyy h:mm tt','timeFormat':'Clock12Hours','weekStarts':0}));
    DayPilot.Locale.register(new DayPilot.Locale('es-es', {'dayNames':['domingo','lunes','martes','miércoles','jueves','viernes','sábado'],'dayNamesShort':['D','L','M','X','J','V','S'],'monthNames':['enero','febrero','marzo','abril','mayo','junio','julio','agosto','septiembre','octubre','noviembre','diciembre',''],'monthNamesShort':['ene.','feb.','mar.','abr.','may.','jun.','jul.','ago.','sep.','oct.','nov.','dic.',''],'timePattern':'H:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('es-mx', {'dayNames':['domingo','lunes','martes','miércoles','jueves','viernes','sábado'],'dayNamesShort':['do.','lu.','ma.','mi.','ju.','vi.','sá.'],'monthNames':['enero','febrero','marzo','abril','mayo','junio','julio','agosto','septiembre','octubre','noviembre','diciembre',''],'monthNamesShort':['ene.','feb.','mar.','abr.','may.','jun.','jul.','ago.','sep.','oct.','nov.','dic.',''],'timePattern':'hh:mm tt','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy hh:mm tt','timeFormat':'Clock12Hours','weekStarts':0}));
    DayPilot.Locale.register(new DayPilot.Locale('eu-es', {'dayNames':['igandea','astelehena','asteartea','asteazkena','osteguna','ostirala','larunbata'],'dayNamesShort':['ig','al','as','az','og','or','lr'],'monthNames':['urtarrila','otsaila','martxoa','apirila','maiatza','ekaina','uztaila','abuztua','iraila','urria','azaroa','abendua',''],'monthNamesShort':['urt.','ots.','mar.','api.','mai.','eka.','uzt.','abu.','ira.','urr.','aza.','abe.',''],'timePattern':'H:mm','datePattern':'yyyy/MM/dd','dateTimePattern':'yyyy/MM/dd H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('fi-fi', {'dayNames':['sunnuntai','maanantai','tiistai','keskiviikko','torstai','perjantai','lauantai'],'dayNamesShort':['su','ma','ti','ke','to','pe','la'],'monthNames':['tammikuu','helmikuu','maaliskuu','huhtikuu','toukokuu','kesäkuu','heinäkuu','elokuu','syyskuu','lokakuu','marraskuu','joulukuu',''],'monthNamesShort':['tammi','helmi','maalis','huhti','touko','kesä','heinä','elo','syys','loka','marras','joulu',''],'timePattern':'H:mm','datePattern':'d.M.yyyy','dateTimePattern':'d.M.yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('fr-be', {'dayNames':['dimanche','lundi','mardi','mercredi','jeudi','vendredi','samedi'],'dayNamesShort':['di','lu','ma','me','je','ve','sa'],'monthNames':['janvier','février','mars','avril','mai','juin','juillet','août','septembre','octobre','novembre','décembre',''],'monthNamesShort':['janv.','févr.','mars','avr.','mai','juin','juil.','août','sept.','oct.','nov.','déc.',''],'timePattern':'HH:mm','datePattern':'dd-MM-yy','dateTimePattern':'dd-MM-yy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('fr-ch', {'dayNames':['dimanche','lundi','mardi','mercredi','jeudi','vendredi','samedi'],'dayNamesShort':['di','lu','ma','me','je','ve','sa'],'monthNames':['janvier','février','mars','avril','mai','juin','juillet','août','septembre','octobre','novembre','décembre',''],'monthNamesShort':['janv.','févr.','mars','avr.','mai','juin','juil.','août','sept.','oct.','nov.','déc.',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('fr-fr', {'dayNames':['dimanche','lundi','mardi','mercredi','jeudi','vendredi','samedi'],'dayNamesShort':['di','lu','ma','me','je','ve','sa'],'monthNames':['janvier','février','mars','avril','mai','juin','juillet','août','septembre','octobre','novembre','décembre',''],'monthNamesShort':['janv.','févr.','mars','avr.','mai','juin','juil.','août','sept.','oct.','nov.','déc.',''],'timePattern':'HH:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('fr-lu', {'dayNames':['dimanche','lundi','mardi','mercredi','jeudi','vendredi','samedi'],'dayNamesShort':['di','lu','ma','me','je','ve','sa'],'monthNames':['janvier','février','mars','avril','mai','juin','juillet','août','septembre','octobre','novembre','décembre',''],'monthNamesShort':['janv.','févr.','mars','avr.','mai','juin','juil.','août','sept.','oct.','nov.','déc.',''],'timePattern':'HH:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('gl-es', {'dayNames':['domingo','luns','martes','mércores','xoves','venres','sábado'],'dayNamesShort':['do','lu','ma','mé','xo','ve','sá'],'monthNames':['xaneiro','febreiro','marzo','abril','maio','xuño','xullo','agosto','setembro','outubro','novembro','decembro',''],'monthNamesShort':['xan','feb','mar','abr','maio','xuño','xul','ago','set','out','nov','dec',''],'timePattern':'H:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('it-it', {'dayNames':['domenica','lunedì','martedì','mercoledì','giovedì','venerdì','sabato'],'dayNamesShort':['do','lu','ma','me','gi','ve','sa'],'monthNames':['gennaio','febbraio','marzo','aprile','maggio','giugno','luglio','agosto','settembre','ottobre','novembre','dicembre',''],'monthNamesShort':['gen','feb','mar','apr','mag','giu','lug','ago','set','ott','nov','dic',''],'timePattern':'HH:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('it-ch', {'dayNames':['domenica','lunedì','martedì','mercoledì','giovedì','venerdì','sabato'],'dayNamesShort':['do','lu','ma','me','gi','ve','sa'],'monthNames':['gennaio','febbraio','marzo','aprile','maggio','giugno','luglio','agosto','settembre','ottobre','novembre','dicembre',''],'monthNamesShort':['gen','feb','mar','apr','mag','giu','lug','ago','set','ott','nov','dic',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('ja-jp', {'dayNames':['日曜日','月曜日','火曜日','水曜日','木曜日','金曜日','土曜日'],'dayNamesShort':['日','月','火','水','木','金','土'],'monthNames':['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月',''],'monthNamesShort':['1','2','3','4','5','6','7','8','9','10','11','12',''],'timePattern':'H:mm','datePattern':'yyyy/MM/dd','dateTimePattern':'yyyy/MM/dd H:mm','timeFormat':'Clock24Hours','weekStarts':0}));
    DayPilot.Locale.register(new DayPilot.Locale('nb-no', {'dayNames':['søndag','mandag','tirsdag','onsdag','torsdag','fredag','lørdag'],'dayNamesShort':['sø','ma','ti','on','to','fr','lø'],'monthNames':['januar','februar','mars','april','mai','juni','juli','august','september','oktober','november','desember',''],'monthNamesShort':['jan','feb','mar','apr','mai','jun','jul','aug','sep','okt','nov','des',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('nl-nl', {'dayNames':['zondag','maandag','dinsdag','woensdag','donderdag','vrijdag','zaterdag'],'dayNamesShort':['zo','ma','di','wo','do','vr','za'],'monthNames':['januari','februari','maart','april','mei','juni','juli','augustus','september','oktober','november','december',''],'monthNamesShort':['jan','feb','mrt','apr','mei','jun','jul','aug','sep','okt','nov','dec',''],'timePattern':'HH:mm','datePattern':'d-M-yyyy','dateTimePattern':'d-M-yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('nl-be', {'dayNames':['zondag','maandag','dinsdag','woensdag','donderdag','vrijdag','zaterdag'],'dayNamesShort':['zo','ma','di','wo','do','vr','za'],'monthNames':['januari','februari','maart','april','mei','juni','juli','augustus','september','oktober','november','december',''],'monthNamesShort':['jan','feb','mrt','apr','mei','jun','jul','aug','sep','okt','nov','dec',''],'timePattern':'H:mm','datePattern':'d/MM/yyyy','dateTimePattern':'d/MM/yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('nn-no', {'dayNames':['søndag','måndag','tysdag','onsdag','torsdag','fredag','laurdag'],'dayNamesShort':['sø','må','ty','on','to','fr','la'],'monthNames':['januar','februar','mars','april','mai','juni','juli','august','september','oktober','november','desember',''],'monthNamesShort':['jan','feb','mar','apr','mai','jun','jul','aug','sep','okt','nov','des',''],'timePattern':'HH:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('pt-br', {'dayNames':['domingo','segunda-feira','terça-feira','quarta-feira','quinta-feira','sexta-feira','sábado'],'dayNamesShort':['D','S','T','Q','Q','S','S'],'monthNames':['janeiro','fevereiro','março','abril','maio','junho','julho','agosto','setembro','outubro','novembro','dezembro',''],'monthNamesShort':['jan','fev','mar','abr','mai','jun','jul','ago','set','out','nov','dez',''],'timePattern':'HH:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':0}));
    DayPilot.Locale.register(new DayPilot.Locale('pl-pl', {'dayNames':['niedziela','poniedziałek','wtorek','środa','czwartek','piątek','sobota'],'dayNamesShort':['N','Pn','Wt','Śr','Cz','Pt','So'],'monthNames':['styczeń','luty','marzec','kwiecień','maj','czerwiec','lipiec','sierpień','wrzesień','październik','listopad','grudzień',''],'monthNamesShort':['sty','lut','mar','kwi','maj','cze','lip','sie','wrz','paź','lis','gru',''],'timePattern':'HH:mm','datePattern':'yyyy-MM-dd','dateTimePattern':'yyyy-MM-dd HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('pt-pt', {'dayNames':['domingo','segunda-feira','terça-feira','quarta-feira','quinta-feira','sexta-feira','sábado'],'dayNamesShort':['D','S','T','Q','Q','S','S'],'monthNames':['janeiro','fevereiro','março','abril','maio','junho','julho','agosto','setembro','outubro','novembro','dezembro',''],'monthNamesShort':['jan','fev','mar','abr','mai','jun','jul','ago','set','out','nov','dez',''],'timePattern':'HH:mm','datePattern':'dd/MM/yyyy','dateTimePattern':'dd/MM/yyyy HH:mm','timeFormat':'Clock24Hours','weekStarts':0}));
    DayPilot.Locale.register(new DayPilot.Locale('ru-ru', {'dayNames':['воскресенье','понедельник','вторник','среда','четверг','пятница','суббота'],'dayNamesShort':['Вс','Пн','Вт','Ср','Чт','Пт','Сб'],'monthNames':['Январь','Февраль','Март','Апрель','Май','Июнь','Июль','Август','Сентябрь','Октябрь','Ноябрь','Декабрь',''],'monthNamesShort':['янв','фев','мар','апр','май','июн','июл','авг','сен','окт','ноя','дек',''],'timePattern':'H:mm','datePattern':'dd.MM.yyyy','dateTimePattern':'dd.MM.yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('sk-sk', {'dayNames':['nedeľa','pondelok','utorok','streda','štvrtok','piatok','sobota'],'dayNamesShort':['ne','po','ut','st','št','pi','so'],'monthNames':['január','február','marec','apríl','máj','jún','júl','august','september','október','november','december',''],'monthNamesShort':['1','2','3','4','5','6','7','8','9','10','11','12',''],'timePattern':'H:mm','datePattern':'d.M.yyyy','dateTimePattern':'d.M.yyyy H:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('sv-se', {'dayNames':['söndag','måndag','tisdag','onsdag','torsdag','fredag','lördag'],'dayNamesShort':['sö','må','ti','on','to','fr','lö'],'monthNames':['januari','februari','mars','april','maj','juni','juli','augusti','september','oktober','november','december',''],'monthNamesShort':['jan','feb','mar','apr','maj','jun','jul','aug','sep','okt','nov','dec',''],'timePattern':'HH:mm','datePattern':'yyyy-MM-dd','dateTimePattern':'yyyy-MM-dd HH:mm','timeFormat':'Clock24Hours','weekStarts':1}));
    DayPilot.Locale.register(new DayPilot.Locale('zh-cn', {'dayNames':['星期日','星期一','星期二','星期三','星期四','星期五','星期六'],'dayNamesShort':['日','一','二','三','四','五','六'],'monthNames':['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月',''],'monthNamesShort':['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月',''],'timePattern':'H:mm','datePattern':'yyyy/M/d','dateTimePattern':'yyyy/M/d H:mm','timeFormat':'Clock24Hours','weekStarts':1}));

    DayPilot.Locale.US = DayPilot.Locale.find("en-us");

    DayPilot.Switcher = function () {

        var This = this;

        this.views = [];
        this.switchers = [];
        this.navigator = {};
        
        this.selectedClass = null;

        this.active = null;

        this.day = new DayPilot.Date();

        this.navigator.updateMode = function (mode) {
            var control = This.navigator.control;
            if (!control) {
                return;
            }
            control.selectMode = mode;
            control.select(This.day);
        };

        this.addView = function (spec, options) {
            var element;
            if (typeof spec === 'string') {
                element = document.getElementById(spec);
                if (!element) {
                	throw "Element not found: " + spec;
                }
            }
            else {  // DayPilot object, DOM element
                element = spec;
            }
            
            var control = element;

            var view = {};
            view.isView = true;
            view.id = control.id;
            view.control = control;
            view.options = options || {};
            view.hide = function () {
                if (control.hide) {
                    control.hide();
                }
                else if (control.nav && control.nav.top) {
                    control.nav.top.style.display = 'none';
                }
                else {
                    control.style.display = 'none';
                }
            };
            view.sendNavigate = function(date) {
                var serverBased = (function() {
                    if (control.backendUrl) {  // ASP.NET MVC, Java
                        return true;
                    }
                    if (typeof WebForm_DoCallback === 'function' && control.uniqueID) {  // ASP.NET WebForms
                        return true;
                    }
                    return false;
                })();
                if (serverBased) {
                    if (control.commandCallBack) {
                        control.commandCallBack("navigate", { "day": date });
                    }
                }
            };
            view.show = function () {
                This._hideViews();
                if (control.show) {
                    control.show();
                }
                else if (control.nav && control.nav.top) {
                    control.nav.top.style.display = '';
                }
                else {
                    control.style.display = '';
                }
            };
            view.selectMode = function () { // for navigator
                if (view.options.navigatorSelectMode) {
                    return view.options.navigatorSelectMode;
                }
                    
                if (control.isCalendar) {
                    switch (control.viewType) {
                        case "Day":
                            return "day";
                        case "Week":
                            return "week";
                        case "WorkWeek":
                            return "week";
                        default:
                            return "day";
                    }
                }
                else if (control.isMonth) {
                    switch (control.viewType) {
                        case "Month":
                            return "month";
                        case "Weeks":
                            return "week";
                        default:
                            return "day";
                    }
                }
                return "day";
            };

            this.views.push(view);
            
            return view;
        };

        this.addButton = function (id, control) {
            var element;
            if (typeof id === 'string') {
                element = document.getElementById(id);
                if (!element) {
                	throw "Element not found: " + id;
                }
            }
            else {
                element = id;
            }

            var view = this._findViewByControl(control);
            if (!view) {
                view = this.addView(control);
            }

            var switcher = {};
            switcher.isSwitcher = true;
            switcher.element = element;
            switcher.id = element.id;
            switcher.view = view;
            switcher.onClick = function (ev) {

                This.show(switcher);
                This._select(switcher);

                ev = ev || window.event;
                if (ev) {
                    ev.preventDefault ? ev.preventDefault() : ev.returnValue = false;
                }
                
            };

            DayPilot.re(element, 'click', switcher.onClick);
            
            this.switchers.push(switcher);
            
            return switcher;
        };

        this.select = function(id) {
            var switcher = this._findSwitcherById(id);
            if (switcher) {
                switcher.onClick();
            }
            else if (this.switchers.length > 0) {
                this.switchers[0].onClick();
            }
        };
        
        this._findSwitcherById = function(id) {
            for (var i = 0; i < this.switchers.length; i++) {
                var switcher = this.switchers[i];
                if (switcher.id === id) {
                    return switcher;
                }
            }
            return null;
        };
        
        this._select = function(switcher) {
            if (!this.selectedClass) {
                return;
            }
            
            for (var i = 0; i < this.switchers.length; i++) {
                var s = this.switchers[i];
                DayPilot.Util.removeClass(s.element, this.selectedClass);
            }
            DayPilot.Util.addClass(switcher.element, this.selectedClass);
        };

        this.addNavigator = function (control) {
            //this.navigator = {};
            This.navigator.control = control;

            control.timeRangeSelectedHandling = "JavaScript";
            control.onTimeRangeSelected = function() {
                var start, end, day;
                if (control.api === 1) {
                    start = arguments[0];
                    end = arguments[1];
                    day = arguments[2];
                }
                else {
                    var args = arguments[0];
                    start = args.start;
                    end = args.end;
                    day = args.day;
                }
                This.day = day;
                This.active.sendNavigate(This.day);
                if (This.onTimeRangeSelected) {
                    var args = {};
                    args.start = start;
                    args.end = end;
                    args.day = day;
                    args.target = This.active.control;
                    This.onTimeRangeSelected(args);
                }
                //This.active.control.commandCallBack("navigate", { "day": This.day });
            };
        };

        this.show = function (el) {
            var view, switcher;
            if (el.isSwitcher) {
                switcher = el;
                view = switcher.view;
            }
            else {
                view = el.isView ? el : this._findViewByControl(el);
                if (this.active === view) {
                    return;
                }
            }
            
            if (This.onSelect) {
                var args = {};
                //args.switcher = switcher;
                args.source = switcher ? switcher.element : null;
                args.target = view.control;
                
                This.onSelect(args);
                // TODO add preventDefault
            }
            
            this.active = view;
            view.show();

            var mode = view.selectMode();
            This.navigator.updateMode(mode);

            This.active.sendNavigate(this.day);
        };

        this._findViewByControl = function (control) {
            for (var i = 0; i < this.views.length; i++) {
                if (this.views[i].control === control) {
                    return this.views[i];
                }
            }
            return null;
        };

        this._hideViews = function () {
            //var controls = [dp_day, dp_week, dp_month];
            for (var i = 0; i < this.views.length; i++) {
                this.views[i].hide();
            }
        };
    };

    // register the default theme
    (function() {
        if (DayPilot.Global.defaultCss) {
            return;
        }

        var sheet = DayPilot.sheet();

        // bubble
        sheet.add(".bubble_default_main", "cursor: default;");
        sheet.add(".bubble_default_main_inner", 'border-radius: 5px;font-size: 12px;padding: 4px;color: #666;background: #eeeeee; background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");border: 1px solid #ccc;-moz-border-radius: 5px;-webkit-border-radius: 5px;border-radius: 5px;-moz-box-shadow:0px 2px 3px rgba(000,000,000,0.3),inset 0px 0px 2px rgba(255,255,255,0.8);-webkit-box-shadow:0px 2px 3px rgba(000,000,000,0.3),inset 0px 0px 2px rgba(255,255,255,0.8);box-shadow:0px 2px 3px rgba(000,000,000,0.3),inset 0px 0px 2px rgba(255,255,255,0.8);');

        // calendar
        sheet.add(".calendar_default_main", "border: 1px solid #999; font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 12px;");
        sheet.add(".calendar_default_rowheader_inner,.calendar_default_cornerright_inner,.calendar_default_corner_inner,.calendar_default_colheader_inner,.calendar_default_alldayheader_inner", "color: #666;background: #eee;");
        sheet.add(".calendar_default_cornerright_inner", "position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;	border-bottom: 1px solid #999;");
        sheet.add(".calendar_default_rowheader_inner", "font-size: 16pt;text-align: right; position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;border-right: 1px solid #999;border-bottom: 1px solid #999;");
        sheet.add(".calendar_default_corner_inner", "position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;border-right: 1px solid #999;border-bottom: 1px solid #999;");
        sheet.add(".calendar_default_rowheader_minutes", "font-size:10px;vertical-align: super;padding-left: 2px;padding-right: 2px;");
        sheet.add(".calendar_default_colheader_inner", "text-align: center; position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;border-right: 1px solid #999;border-bottom: 1px solid #999;");
        sheet.add(".calendar_default_cell_inner", "position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;border-right: 1px solid #ddd;border-bottom: 1px solid #ddd; background: #f9f9f9;");
        sheet.add(".calendar_default_cell_business .calendar_default_cell_inner", "background: #fff");
        sheet.add(".calendar_default_alldayheader_inner", "text-align: center;position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;border-right: 1px solid #999;border-bottom: 1px solid #999;");
        sheet.add(".calendar_default_message", "opacity: 0.9;filter: alpha(opacity=90);	padding: 10px; color: #ffffff;background: #ffa216;");
        sheet.add(".calendar_default_alldayevent_inner,.calendar_default_event_inner", 'color: #666; border: 1px solid #999;'); // border-top: 4px solid #1066a8;
        sheet.add(".calendar_default_event_bar", "top: 0px;bottom: 0px;left: 0px;width: 4px;background-color: #9dc8e8;");
        sheet.add(".calendar_default_event_bar_inner", "position: absolute;width: 4px;background-color: #1066a8;");
        sheet.add(".calendar_default_alldayevent_inner,.calendar_default_event_inner", 'background: #fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".calendar_default_selected .calendar_default_event_inner", "background: #ddd;");
        sheet.add(".calendar_default_alldayevent_inner", "position: absolute;top: 2px;bottom: 2px;left: 2px;right: 2px;padding: 2px;margin-right: 1px;font-size: 12px;");
        sheet.add(".calendar_default_event_withheader .calendar_default_event_inner", "padding-top: 15px;");
        sheet.add(".calendar_default_event", "cursor: default;");
        sheet.add(".calendar_default_event_inner", "position: absolute;overflow: hidden;top: 0px;bottom: 0px;left: 0px;right: 0px;padding: 2px 2px 2px 6px;font-size: 12px;");
        sheet.add(".calendar_default_shadow_inner", "background-color: #666666;	opacity: 0.5;filter: alpha(opacity=50);height: 100%;");
        sheet.add(".calendar_default_event_delete", "background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAYAAACprHcmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjExR/NCNwAAAI5JREFUKFNtkLERgCAMRbmzdK8s4gAUlhYOYEHJEJYOYOEwDmGBPxC4kOPfvePy84MGR0RJ2N1A8H3N6DATwSQ57m2ql8NBG+AEM7D+UW+wjdfUPgerYNgB5gOLRHqhcasg84C2QxPMtrUhSqQIhg7ypy9VM2EUZPI/4rQ7rGxqo9sadTegw+UdjeDLAKUfhbaQUVPIfJYAAAAASUVORK5CYII=) center center no-repeat; opacity: 0.6; -ms-filter:'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; cursor: pointer;");
        sheet.add(".calendar_default_event_delete:hover", "opacity: 1;-ms-filter: none;");
        sheet.add(".calendar_default_scroll_up", "background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAB3RJTUUH2wESDiYcrhwCiQAAAAlwSFlzAAAuIwAALiMBeKU/dgAAAARnQU1BAACxjwv8YQUAAACcSURBVHjaY2AgF9wWsTW6yGMlhi7OhC7AyMDQzMnBXIpFHAFuCtuaMTP+P8nA8P/b1x//FfW/HHuF1UQmxv+NUP1c3OxMVVhNvCVi683E8H8LXOY/w9+fTH81tF8fv4NiIpBRj+YoZtZ/LDUoJmKYhsVUpv0MDiyMDP96sIYV0FS2/8z9ICaLlOhvS4b/jC//MzC8xBG0vJeF7GQBlK0xdiUzCtsAAAAASUVORK5CYII=);");
        sheet.add(".calendar_default_scroll_down", "background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAALiMAAC4jAXilP3YAAACqSURBVChTY7wpam3L9J+xmQEP+PGPKZZxP4MDi4zI78uMDIwa2NT+Z2DYovrmiC+TI8OBP/8ZmEqwGvif4e8vxr+FIDkmEKH25vBWBgbG0+iK/zEwLtF+ffwOXCGI8Y+BoRFFIdC030x/WmBiYBNhpgLdswNJ8RSYaSgmgk39z1gPUfj/29ef/9rwhQTDHRHbrbdEbLvRFcGthkkAra/9/uMvhkK8piNLAgCRpTnNn4AEmAAAAABJRU5ErkJggg==);");

        sheet.add(".calendar_default_now", "background-color: red;");
        sheet.add(".calendar_default_now:before", "content: ''; top: -5px; border-width: 5px; border-color: transparent transparent transparent red; border-style: solid; width: 0px; height:0px; position: absolute; -moz-transform: scale(.9999);");

        // menu
        sheet.add(".menu_default_main", "font-family: Tahoma, Arial, Helvetica, Sans-Serif;font-size: 12px;border: 1px solid #dddddd;background-color: white;padding: 0px;cursor: default;background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAABCAIAAABG0om7AAAAKXRFWHRDcmVhdGlvbiBUaW1lAHBvIDEwIDUgMjAxMCAyMjozMzo1OSArMDEwMGzy7+IAAAAHdElNRQfaBQoUJAesj4VUAAAACXBIWXMAAA7DAAAOwwHHb6hkAAAABGdBTUEAALGPC/xhBQAAABVJREFUeNpj/P//PwO1weMnT2RlZAAYuwX/4oA3BgAAAABJRU5ErkJggg==);background-repeat: repeat-y;xborder-radius: 5px;-moz-box-shadow:0px 2px 3px rgba(000,000,000,0.3),inset 0px 0px 2px rgba(255,255,255,0.8);-webkit-box-shadow:0px 2px 3px rgba(000,000,000,0.3),inset 0px 0px 2px rgba(255,255,255,0.8);box-shadow:0px 2px 3px rgba(000,000,000,0.3),inset 0px 0px 2px rgba(255,255,255,0.8);");
        sheet.add(".menu_default_title", "background-color: #f2f2f2;border-bottom: 1px solid gray;padding: 4px 4px 4px 37px;");
        sheet.add(".menu_default_main a", "padding: 2px 2px 2px 35px;color: black;text-decoration: none;cursor: default;");
        sheet.add(".menu_default_main a img", "margin-left: 6px;margin-top: 2px;");
        sheet.add(".menu_default_main a span", "display: block;height: 20px;line-height: 20px; overflow:hidden;padding-left: 2px;padding-right: 20px;");
        sheet.add(".menu_default_main a:hover", 'background: #eeeeee;background: -webkit-gradient(linear, left top, left bottom, from(#efefef), to(#e6e6e6));background: -webkit-linear-gradient(top, #efefef 0%, #e6e6e6);background: -moz-linear-gradient(top, #efefef 0%, #e6e6e6);background: -ms-linear-gradient(top, #efefef 0%, #e6e6e6);background: -o-linear-gradient(top, #efefef 0%, #e6e6e6);background: linear-gradient(top, #efefef 0%, #e6e6e6);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#efefef", endColorStr="#e6e6e6");');
        sheet.add(".menu_default_main div div", "border-top: 1px solid #dddddd;margin-top: 2px;margin-bottom: 2px;margin-left: 28px;");

        // month
        sheet.add(".month_default_main", "border: 1px solid #aaa;font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 12px;color: #666;");
        sheet.add(".month_default_cell_inner", "border-right: 1px solid #ddd;border-bottom: 1px solid #ddd;position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;background-color: #f9f9f9;");
        sheet.add(".month_default_cell_business .month_default_cell_inner", "background-color: #fff;");
        sheet.add(".month_default_cell_header", "text-align: right;padding-right: 2px;");
        sheet.add(".month_default_header_inner", 'text-align: center; vertical-align: middle;position: absolute;top: 0px;left: 0px;bottom: 0px;right: 0px;border-right: 1px solid #999;border-bottom: 1px solid #999;cursor: default;color: #666;background: #eee;');
        sheet.add(".month_default_message", 'padding: 10px;opacity: 0.9;filter: alpha(opacity=90);color: #ffffff;background: #ffa216;background: -webkit-gradient(linear, left top, left bottom, from(#ffa216), to(#ff8400));background: -webkit-linear-gradient(top, #ffa216 0%, #ff8400);background: -moz-linear-gradient(top, #ffa216 0%, #ff8400);background: -ms-linear-gradient(top, #ffa216 0%, #ff8400);background: -o-linear-gradient(top, #ffa216 0%, #ff8400);background: linear-gradient(top, #ffa216 0%, #ff8400);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffa216", endColorStr="#ff8400");');
        sheet.add(".month_default_event_inner", 'position: absolute;top: 0px;bottom: 0px;left: 1px;right: 1px;overflow:hidden;padding: 2px;padding-left: 5px;font-size: 12px;color: #666;background: #fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");border: 1px solid #999;border-radius: 0px;');
        sheet.add(".month_default_event_continueright .month_default_event_inner", "border-top-right-radius: 0px;border-bottom-right-radius: 0px;border-right-style: dotted;");
        sheet.add(".month_default_event_continueleft .month_default_event_inner", "border-top-left-radius: 0px;border-bottom-left-radius: 0px;border-left-style: dotted;");
        sheet.add(".month_default_event_hover .month_default_event_inner", 'background: #fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#e8e8e8));background: -webkit-linear-gradient(top, #ffffff 0%, #e8e8e8);background: -moz-linear-gradient(top, #ffffff 0%, #e8e8e8);background: -ms-linear-gradient(top, #ffffff 0%, #e8e8e8);background: -o-linear-gradient(top, #ffffff 0%, #e8e8e8);background: linear-gradient(top, #ffffff 0%, #e8e8e8);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#e8e8e8");');
        sheet.add(".month_default_selected .month_default_event_inner, .month_default_event_hover.month_default_selected .month_default_event_inner", "background: #ddd;");
        sheet.add(".month_default_shadow_inner", "background-color: #666666;opacity: 0.5;filter: alpha(opacity=50);height: 100%;");
        sheet.add(".month_default_event_delete", "background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAYAAACprHcmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjExR/NCNwAAAI5JREFUKFNtkLERgCAMRbmzdK8s4gAUlhYOYEHJEJYOYOEwDmGBPxC4kOPfvePy84MGR0RJ2N1A8H3N6DATwSQ57m2ql8NBG+AEM7D+UW+wjdfUPgerYNgB5gOLRHqhcasg84C2QxPMtrUhSqQIhg7ypy9VM2EUZPI/4rQ7rGxqo9sadTegw+UdjeDLAKUfhbaQUVPIfJYAAAAASUVORK5CYII=) center center no-repeat; opacity: 0.6; -ms-filter:'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)';cursor: pointer;");
        sheet.add(".month_default_event_delete:hover", "opacity: 1;-ms-filter: none;");

        // navigator
        sheet.add(".navigator_default_main", "border-left: 1px solid #A0A0A0;border-right: 1px solid #A0A0A0;border-bottom: 1px solid #A0A0A0;background-color: white;color: #000000;");
        sheet.add(".navigator_default_month", "font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 11px;");
        sheet.add(".navigator_default_day", "color: black;");
        sheet.add(".navigator_default_weekend", "background-color: #f0f0f0;");
        sheet.add(".navigator_default_dayheader", "color: black;");
        sheet.add(".navigator_default_line", "border-bottom: 1px solid #A0A0A0;");
        sheet.add(".navigator_default_dayother", "color: gray;");
        sheet.add(".navigator_default_todaybox", "border: 1px solid red;");
        sheet.add(".navigator_default_select, .navigator_default_weekend.navigator_default_select", "background-color: #FFE794;");
        sheet.add(".navigator_default_title, .navigator_default_titleleft, .navigator_default_titleright", 'border-top: 1px solid #A0A0A0;color: #666;background: #eee;background: -webkit-gradient(linear, left top, left bottom, from(#eeeeee), to(#dddddd));background: -webkit-linear-gradient(top, #eeeeee 0%, #dddddd);background: -moz-linear-gradient(top, #eeeeee 0%, #dddddd);background: -ms-linear-gradient(top, #eeeeee 0%, #dddddd);background: -o-linear-gradient(top, #eeeeee 0%, #dddddd);background: linear-gradient(top, #eeeeee 0%, #dddddd);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#eeeeee", endColorStr="#dddddd");');
        sheet.add(".navigator_default_busy", "font-weight: bold;");

        // scheduler
        sheet.add(".scheduler_default_selected .scheduler_default_event_inner", "background: #ddd;");
        sheet.add(".scheduler_default_main", "border: 1px solid #aaa;font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 12px;");
        sheet.add(".scheduler_default_timeheader", "cursor: default;color: #666;");
        sheet.add(".scheduler_default_message", "opacity: 0.9;filter: alpha(opacity=90);padding: 10px; color: #ffffff;background: #ffa216;");
        sheet.add(".scheduler_default_timeheadergroup,.scheduler_default_timeheadercol", "color: #666;background: #eee;");
        sheet.add(".scheduler_default_rowheader,.scheduler_default_corner", "color: #666;background: #eee;");
        sheet.add(".scheduler_default_rowheader.scheduler_default_rowheader_selected", "background-color: #aaa;background-image: -webkit-gradient(linear, 0 100%, 100% 0,color-stop(.25, rgba(255, 255, 255, .2)), color-stop(.25, transparent),	color-stop(.5, transparent), color-stop(.5, rgba(255, 255, 255, .2)), color-stop(.75, rgba(255, 255, 255, .2)), color-stop(.75, transparent), to(transparent));background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -moz-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -ms-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);-webkit-background-size: 20px 20px;-moz-background-size: 20px 20px;background-size: 20px 20px;");
        sheet.add(".scheduler_default_rowheader_inner", "position: absolute;left: 0px;right: 0px;top: 0px;bottom: 0px;border-right: 1px solid #eee;padding: 2px;");
        sheet.add(".scheduler_default_timeheadergroup, .scheduler_default_timeheadercol", "text-align: center;");
        sheet.add(".scheduler_default_timeheadergroup_inner", "position: absolute;left: 0px;right: 0px;top: 0px;bottom: 0px;border-right: 1px solid #aaa;border-bottom: 1px solid #aaa;");
        sheet.add(".scheduler_default_timeheadercol_inner", "position: absolute;left: 0px;right: 0px;top: 0px;bottom: 0px;border-right: 1px solid #aaa;");
        sheet.add(".scheduler_default_divider, .scheduler_default_splitter", "background-color: #aaa;");
        sheet.add(".scheduler_default_divider_horizontal", "background-color: #aaa;");
        sheet.add(".scheduler_default_matrix_vertical_line", "background-color: #eee;");
        sheet.add(".scheduler_default_matrix_vertical_break", "background-color: #000;");
        sheet.add(".scheduler_default_matrix_horizontal_line", "background-color: #eee;");
        sheet.add(".scheduler_default_resourcedivider", "background-color: #aaa;");
        sheet.add(".scheduler_default_shadow_inner", "background-color: #666666;opacity: 0.5;filter: alpha(opacity=50);height: 100%;");
        sheet.add(".scheduler_default_event", "font-size:12px;color:#666;");
        sheet.add(".scheduler_default_event_inner", "position:absolute;top:0px;left:0px;right:0px;bottom:0px;padding:5px 2px 2px 2px;overflow:hidden;border:1px solid #ccc;");
        sheet.add(".scheduler_default_event_bar", "top:0px;left:0px;right:0px;height:4px;background-color:#9dc8e8;");
        sheet.add(".scheduler_default_event_bar_inner", "position:absolute;height:4px;background-color:#1066a8;");
        sheet.add(".scheduler_default_event_inner", 'background:#fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".scheduler_default_event_float_inner", "padding:6px 2px 2px 8px;"); // space for arrow
        sheet.add(".scheduler_default_event_float_inner:after", 'content:"";border-color: transparent #666 transparent transparent;border-style:solid;border-width:5px;width:0;height:0;position:absolute;top:8px;left:-4px;');
        sheet.add(".scheduler_default_columnheader_inner", "font-weight: bold;");
        sheet.add(".scheduler_default_columnheader_splitter", "background-color: #666;opacity: 0.5;filter: alpha(opacity=50);");
        sheet.add(".scheduler_default_columnheader_cell_inner", "padding: 2px;");
        sheet.add(".scheduler_default_cell", "background-color: #f9f9f9;");
        sheet.add(".scheduler_default_cell.scheduler_default_cell_business", "background-color: #fff;");
        sheet.add(".scheduler_default_cell.scheduler_default_cell_business.scheduler_default_cell_selected,.scheduler_default_cell.scheduler_default_cell_selected", "background-color: #ccc;background-image: -webkit-gradient(linear, 0 100%, 100% 0,	color-stop(.25, rgba(255, 255, 255, .2)), color-stop(.25, transparent),	color-stop(.5, transparent), color-stop(.5, rgba(255, 255, 255, .2)), color-stop(.75, rgba(255, 255, 255, .2)), color-stop(.75, transparent), to(transparent));background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -moz-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -ms-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);-webkit-background-size: 20px 20px;-moz-background-size: 20px 20px;background-size: 20px 20px;");
        sheet.add(".scheduler_default_tree_image_no_children", "background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAAKXRFWHRDcmVhdGlvbiBUaW1lAHDhIDMwIEkgMjAwOSAwODo0NjozMSArMDEwMClDkt4AAAAHdElNRQfZAR4HLzEyzsCJAAAACXBIWXMAAA7CAAAOwgEVKEqAAAAABGdBTUEAALGPC/xhBQAAADBJREFUeNpjrK6s5uTl/P75OybJ0NLW8h8bAIozgeSxAaA4E1A7VjmgOL31MeLxHwCeXUT0WkFMKAAAAABJRU5ErkJggg==);");
        sheet.add(".scheduler_default_tree_image_expand", "background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAAKXRFWHRDcmVhdGlvbiBUaW1lAHDhIDMwIEkgMjAwOSAwODo0NjozMSArMDEwMClDkt4AAAAHdElNRQfZAR4HLyUoFBT0AAAACXBIWXMAAA7CAAAOwgEVKEqAAAAABGdBTUEAALGPC/xhBQAAAFJJREFUeNpjrK6s5uTl/P75OybJ0NLW8h8bAIozgeRhgJGREc4GijMBtTNgA0BxFog+uA4IA2gmUJwFog/IgUhAGBB9KPYhA3T74Jog+hjx+A8A1KRQ+AN5vcwAAAAASUVORK5CYII=);");
        sheet.add(".scheduler_default_tree_image_collapse", "background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAAKXRFWHRDcmVhdGlvbiBUaW1lAHDhIDMwIEkgMjAwOSAwODo0NjozMSArMDEwMClDkt4AAAAHdElNRQfZAR4HLxB+p9DXAAAACXBIWXMAAA7CAAAOwgEVKEqAAAAABGdBTUEAALGPC/xhBQAAAENJREFUeNpjrK6s5uTl/P75OybJ0NLW8h8bAIozgeSxAaA4E1A7VjmgOAtEHyMjI7IE0EygOAtEH5CDqY9c+xjx+A8ANndK9WaZlP4AAAAASUVORK5CYII=);");
        sheet.add(".scheduler_default_event_move_left", 'box-sizing: border-box; padding:2px;border:1px solid #ccc;background:#fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".scheduler_default_event_move_right", 'box-sizing: border-box; padding:2px;border:1px solid #ccc;background:#fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".scheduler_default_event_delete", "background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAYAAACprHcmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjExR/NCNwAAAI5JREFUKFNtkLERgCAMRbmzdK8s4gAUlhYOYEHJEJYOYOEwDmGBPxC4kOPfvePy84MGR0RJ2N1A8H3N6DATwSQ57m2ql8NBG+AEM7D+UW+wjdfUPgerYNgB5gOLRHqhcasg84C2QxPMtrUhSqQIhg7ypy9VM2EUZPI/4rQ7rGxqo9sadTegw+UdjeDLAKUfhbaQUVPIfJYAAAAASUVORK5CYII=) center center no-repeat; opacity: 0.6; -ms-filter:'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)';cursor: pointer;");
        sheet.add(".scheduler_default_event_delete:hover", "opacity: 1;-ms-filter: none;");

        sheet.add(".scheduler_default_rowmove_handle", "background-repeat: no-repeat; background-position: center center; background-color: #ccc; background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAQAAAAKCAYAAACT+/8OAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjExR/NCNwAAAClJREFUGFdj+P//P4O9vX2Bg4NDP4gNFgBytgPxebgAMsYuQGMz/jMAAFsTZDPYJlDHAAAAAElFTkSuQmCC); cursor: move;");
        sheet.add(".scheduler_default_rowmove_source", "background-color: black; opacity: 0.2;");
        sheet.add(".scheduler_default_rowmove_position_before, .scheduler_default_rowmove_position_after", "background-color: #999; height: 2px;");
        sheet.add(".scheduler_default_rowmove_position_child", "margin-left: 10px; background-color: #999; height: 2px;");
        sheet.add(".scheduler_default_rowmove_position_child:before", "content: '+'; color: #999; position: absolute; top: -8px; left: -10px;");
        sheet.add(".scheduler_default_rowmove_position_forbidden", "background-color: red; height: 2px; margin-left: 10px;");
        sheet.add(".scheduler_default_rowmove_position_forbidden:before", "content: 'x'; color: red; position: absolute; top: -8px; left: -10px;");

        sheet.add(".scheduler_default_link_horizontal", "border-bottom-style: solid; border-bottom-color: red");
        sheet.add(".scheduler_default_link_vertical", "border-right-style: solid; border-right-color: red");
        sheet.add(".scheduler_default_link_arrow_right:before", "content: ''; border-width: 6px; border-color: transparent transparent transparent red; border-style: solid; width: 0px; height:0px; position: absolute;");
        sheet.add(".scheduler_default_link_arrow_left:before", "content: ''; border-width: 6px; border-color: transparent red transparent transparent; border-style: solid; width: 0px; height:0px; position: absolute;");
        sheet.add(".scheduler_default_link_arrow_down:before", "content: ''; border-width: 6px; border-color: red transparent transparent transparent; border-style: solid; width: 0px; height:0px; position: absolute;");

        sheet.add(".scheduler_default_shadow_overlap .scheduler_default_shadow_inner", "background-color: red;");
        sheet.add(".scheduler_default_block", "background-color: gray; opacity: 0.5; filter: alpha(opacity=50);");

        sheet.add(".scheduler_default_event_group", "box-sizing: border-box; font-size:12px; color:#666; padding:4px 2px 2px 2px; overflow:hidden; border:1px solid #ccc; background-color: #fff;");

        sheet.add(".scheduler_default_header_icon", "box-sizing: border-box; border: 1px solid #aaa; background-color: #f5f5f5; color: #000;");
        sheet.add(".scheduler_default_header_icon:hover", "background-color: #ccc;");
        sheet.add(".scheduler_default_header_icon_hide:before", "content: '\\00AB';");
        sheet.add(".scheduler_default_header_icon_show:before", "content: '\\00BB';");

        sheet.add(".scheduler_default_row_new .scheduler_default_rowheader_inner", "cursor: text; background-position: 0px 5px; background-repeat: no-repeat; background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAABUSURBVChTY0ACslAaK2CC0iCQDMSlECYmQFYIAl1AjFUxukIQwKoYm0IQwFCMSyEIaEJpMMClcD4Qp0CYEIBNIUzRPzAPCtAVYlWEDgyAGIdTGBgAbqEJYyjqa3oAAAAASUVORK5CYII=);");
        sheet.add(".scheduler_default_row_new .scheduler_default_rowheader_inner:hover", "background: white;");
        sheet.add(".scheduler_default_rowheader textarea", "padding: 3px;");
        sheet.add(".scheduler_default_rowheader_scroll", "cursor: default;");

        // gantt
        sheet.add(".gantt_default_selected .gantt_default_event_inner", "background: #ddd;");
        sheet.add(".gantt_default_main", "border: 1px solid #aaa;font-family: Tahoma, Arial, Helvetica, sans-serif; font-size: 12px;");
        sheet.add(".gantt_default_timeheader", "cursor: default;color: #666;");
        sheet.add(".gantt_default_message", "opacity: 0.9;filter: alpha(opacity=90);padding: 10px; color: #ffffff;background: #ffa216;");
        sheet.add(".gantt_default_timeheadergroup,.gantt_default_timeheadercol", "color: #666;background: #eee;");
        sheet.add(".gantt_default_rowheader,.gantt_default_corner", "color: #666;background: #eee;");
        sheet.add(".gantt_default_rowheader.gantt_default_rowheader_selected", "background-color: #aaa;background-image: -webkit-gradient(linear, 0 100%, 100% 0,color-stop(.25, rgba(255, 255, 255, .2)), color-stop(.25, transparent),	color-stop(.5, transparent), color-stop(.5, rgba(255, 255, 255, .2)), color-stop(.75, rgba(255, 255, 255, .2)), color-stop(.75, transparent), to(transparent));background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -moz-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -ms-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);-webkit-background-size: 20px 20px;-moz-background-size: 20px 20px;background-size: 20px 20px;");
        sheet.add(".gantt_default_rowheader_inner", "position: absolute;left: 0px;right: 0px;top: 0px;bottom: 0px;border-right: 1px solid #eee;padding: 2px;");
        sheet.add(".gantt_default_timeheadergroup, .gantt_default_timeheadercol", "text-align: center;");
        sheet.add(".gantt_default_timeheadergroup_inner", "position: absolute;left: 0px;right: 0px;top: 0px;bottom: 0px;border-right: 1px solid #aaa;border-bottom: 1px solid #aaa;");
        sheet.add(".gantt_default_timeheadercol_inner", "position: absolute;left: 0px;right: 0px;top: 0px;bottom: 0px;border-right: 1px solid #aaa;");
        sheet.add(".gantt_default_divider, .gantt_default_splitter", "background-color: #aaa;");
        sheet.add(".gantt_default_divider_horizontal", "background-color: #aaa;");
        sheet.add(".gantt_default_matrix_vertical_line", "background-color: #eee;");
        sheet.add(".gantt_default_matrix_vertical_break", "background-color: #000;");
        sheet.add(".gantt_default_matrix_horizontal_line", "background-color: #eee;");
        sheet.add(".gantt_default_resourcedivider", "background-color: #aaa;");
        sheet.add(".gantt_default_shadow_inner", "background-color: #666666;opacity: 0.5;filter: alpha(opacity=50);height: 100%;");
        sheet.add(".gantt_default_event", "font-size:12px;color:#666;");
        sheet.add(".gantt_default_event_inner", "position:absolute;top:0px;left:0px;right:0px;bottom:0px;padding:5px 2px 2px 2px;overflow:hidden;border:1px solid #ccc;");
        sheet.add(".gantt_default_event_bar", "top:0px;left:0px;right:0px;height:4px;background-color:#9dc8e8;");
        sheet.add(".gantt_default_event_bar_inner", "position:absolute;height:4px;background-color:#1066a8;");
        sheet.add(".gantt_default_event_inner", 'background:#fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".gantt_default_event_float_inner", "padding:6px 2px 2px 8px;"); // space for arrow
        sheet.add(".gantt_default_event_float_inner:after", 'content:"";border-color: transparent #666 transparent transparent;border-style:solid;border-width:5px;width:0;height:0;position:absolute;top:8px;left:-4px;');
        sheet.add(".gantt_default_columnheader_inner", "font-weight: bold;");
        sheet.add(".gantt_default_columnheader_splitter", "background-color: #666;opacity: 0.5;filter: alpha(opacity=50);");
        sheet.add(".gantt_default_columnheader_cell_inner", "padding: 2px;");
        sheet.add(".gantt_default_cell", "background-color: #f9f9f9;");
        sheet.add(".gantt_default_cell.gantt_default_cell_business", "background-color: #fff;");
        sheet.add(".gantt_default_cell.gantt_default_cell_business.gantt_default_cell_selected,.gantt_default_cell.gantt_default_cell_selected", "background-color: #ccc;background-image: -webkit-gradient(linear, 0 100%, 100% 0,	color-stop(.25, rgba(255, 255, 255, .2)), color-stop(.25, transparent),	color-stop(.5, transparent), color-stop(.5, rgba(255, 255, 255, .2)), color-stop(.75, rgba(255, 255, 255, .2)), color-stop(.75, transparent), to(transparent));background-image: -webkit-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -moz-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -ms-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: -o-linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);background-image: linear-gradient(45deg, rgba(255, 255, 255, .2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, .2) 50%, rgba(255, 255, 255, .2) 75%, transparent 75%, transparent);-webkit-background-size: 20px 20px;-moz-background-size: 20px 20px;background-size: 20px 20px;");
        sheet.add(".gantt_default_tree_image_no_children", "background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAAKXRFWHRDcmVhdGlvbiBUaW1lAHDhIDMwIEkgMjAwOSAwODo0NjozMSArMDEwMClDkt4AAAAHdElNRQfZAR4HLzEyzsCJAAAACXBIWXMAAA7CAAAOwgEVKEqAAAAABGdBTUEAALGPC/xhBQAAADBJREFUeNpjrK6s5uTl/P75OybJ0NLW8h8bAIozgeSxAaA4E1A7VjmgOL31MeLxHwCeXUT0WkFMKAAAAABJRU5ErkJggg==);");
        sheet.add(".gantt_default_tree_image_expand", "background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAAKXRFWHRDcmVhdGlvbiBUaW1lAHDhIDMwIEkgMjAwOSAwODo0NjozMSArMDEwMClDkt4AAAAHdElNRQfZAR4HLyUoFBT0AAAACXBIWXMAAA7CAAAOwgEVKEqAAAAABGdBTUEAALGPC/xhBQAAAFJJREFUeNpjrK6s5uTl/P75OybJ0NLW8h8bAIozgeRhgJGREc4GijMBtTNgA0BxFog+uA4IA2gmUJwFog/IgUhAGBB9KPYhA3T74Jog+hjx+A8A1KRQ+AN5vcwAAAAASUVORK5CYII=);");
        sheet.add(".gantt_default_tree_image_collapse", "background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAkAAAAJCAIAAABv85FHAAAAKXRFWHRDcmVhdGlvbiBUaW1lAHDhIDMwIEkgMjAwOSAwODo0NjozMSArMDEwMClDkt4AAAAHdElNRQfZAR4HLxB+p9DXAAAACXBIWXMAAA7CAAAOwgEVKEqAAAAABGdBTUEAALGPC/xhBQAAAENJREFUeNpjrK6s5uTl/P75OybJ0NLW8h8bAIozgeSxAaA4E1A7VjmgOAtEHyMjI7IE0EygOAtEH5CDqY9c+xjx+A8ANndK9WaZlP4AAAAASUVORK5CYII=);");
        sheet.add(".gantt_default_event_move_left", 'box-sizing: border-box; padding:2px;border:1px solid #ccc;background:#fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".gantt_default_event_move_right", 'box-sizing: border-box; padding:2px;border:1px solid #ccc;background:#fff;background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#eeeeee));background: -webkit-linear-gradient(top, #ffffff 0%, #eeeeee);background: -moz-linear-gradient(top, #ffffff 0%, #eeeeee);background: -ms-linear-gradient(top, #ffffff 0%, #eeeeee);background: -o-linear-gradient(top, #ffffff 0%, #eeeeee);background: linear-gradient(top, #ffffff 0%, #eeeeee);filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr="#ffffff", endColorStr="#eeeeee");');
        sheet.add(".gantt_default_event_delete", "background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAsAAAALCAYAAACprHcmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjExR/NCNwAAAI5JREFUKFNtkLERgCAMRbmzdK8s4gAUlhYOYEHJEJYOYOEwDmGBPxC4kOPfvePy84MGR0RJ2N1A8H3N6DATwSQ57m2ql8NBG+AEM7D+UW+wjdfUPgerYNgB5gOLRHqhcasg84C2QxPMtrUhSqQIhg7ypy9VM2EUZPI/4rQ7rGxqo9sadTegw+UdjeDLAKUfhbaQUVPIfJYAAAAASUVORK5CYII=) center center no-repeat; opacity: 0.6; -ms-filter:'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)';cursor: pointer;");
        sheet.add(".gantt_default_event_delete:hover", "opacity: 1;-ms-filter: none;");

        sheet.add(".gantt_default_rowmove_handle", "background-repeat: no-repeat; background-position: center center; background-color: #ccc; background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAQAAAAKCAYAAACT+/8OAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjExR/NCNwAAAClJREFUGFdj+P//P4O9vX2Bg4NDP4gNFgBytgPxebgAMsYuQGMz/jMAAFsTZDPYJlDHAAAAAElFTkSuQmCC); cursor: move;");
        sheet.add(".gantt_default_rowmove_source", "background-color: black; opacity: 0.2;");
        sheet.add(".gantt_default_rowmove_position_before, .gantt_default_rowmove_position_after", "background-color: #999; height: 2px;");
        sheet.add(".gantt_default_rowmove_position_child", "margin-left: 10px; background-color: #999; height: 2px;");
        sheet.add(".gantt_default_rowmove_position_child:before", "content: '+'; color: #999; position: absolute; top: -8px; left: -10px;");
        sheet.add(".gantt_default_rowmove_position_forbidden", "background-color: red; height: 2px; margin-left: 10px;");
        sheet.add(".gantt_default_rowmove_position_forbidden:before", "content: 'x'; color: red; position: absolute; top: -8px; left: -10px;");

        sheet.add(".gantt_default_task_group .gantt_default_event_inner", "position:absolute;top:5px;left:0px;right:0px;bottom:6px;overflow:hidden; background: blue; filter: none; border: 0px none;");
        sheet.add(".gantt_default_task_group.gantt_default_event:before", "content:''; border-color: transparent transparent transparent blue; border-style: solid; border-width: 6px; position: absolute; bottom: 0px;");
        sheet.add(".gantt_default_task_group.gantt_default_event:after", "content:''; border-color: transparent blue transparent transparent; border-style: solid; border-width: 6px; position: absolute; bottom: 0px; right: 0px;");

        sheet.add(".gantt_default_task_milestone .gantt_default_event_inner", "position:absolute;top:16%;left:16%;right:16%;bottom:16%; background: green; border: 0px none; -webkit-transform: rotate(45deg);-moz-transform: rotate(45deg);-ms-transform: rotate(45deg);-o-transform: rotate(45deg); transform: rotate(45deg); filter: none;");
        sheet.add(".gantt_default_browser_ie8 .gantt_default_task_milestone .gantt_default_event_inner", "-ms-filter: \"progid:DXImageTransform.Microsoft.Matrix(SizingMethod='auto expand', M11=0.7071067811865476, M12=-0.7071067811865475, M21=0.7071067811865475, M22=0.7071067811865476);\"");

        sheet.add(".gantt_default_event_left", "white-space: nowrap; padding-top: 5px; color: #666; cursor: default;");
        sheet.add(".gantt_default_event_right", "white-space: nowrap; padding-top: 5px; color: #666; cursor: default;");

        sheet.add(".gantt_default_link_horizontal", "border-bottom-style: solid; border-bottom-color: red;");
        sheet.add(".gantt_default_link_vertical", "border-right-style: solid; border-right-color: red;");
        sheet.add(".gantt_default_link_arrow_right:before", "content: ''; border-width: 6px; border-color: transparent transparent transparent red; border-style: solid; width: 0px; height:0px; position: absolute;");
        sheet.add(".gantt_default_link_arrow_left:before", "content: ''; border-width: 6px; border-color: transparent red transparent transparent; border-style: solid; width: 0px; height:0px; position: absolute;");
        sheet.add(".gantt_default_link_arrow_down:before", "content: ''; border-width: 6px; border-color: red transparent transparent transparent; border-style: solid; width: 0px; height:0px; position: absolute;");

        sheet.add(".gantt_default_shadow_overlap .gantt_default_shadow_inner", "background-color: red;");
        sheet.add(".gantt_default_block", "background-color: gray; opacity: 0.5; filter: alpha(opacity=50);");

        sheet.add(".gantt_default_link_hover", "box-shadow: 0px 0px 2px 2px rgba(255, 0, 0, 0.3)");

        sheet.add(".gantt_default_header_icon", "box-sizing: border-box; border: 1px solid #aaa; background-color: #f5f5f5; color: #000;");
        sheet.add(".gantt_default_header_icon:hover", "background-color: #ccc;");
        sheet.add(".gantt_default_header_icon_hide:before", "content: '\\00AB';");
        sheet.add(".gantt_default_header_icon_show:before", "content: '\\00BB';");

        sheet.add(".gantt_default_row_new .gantt_default_rowheader_inner", "cursor: text; background-position: 0 50%; background-repeat: no-repeat; background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAABUSURBVChTY0ACslAaK2CC0iCQDMSlECYmQFYIAl1AjFUxukIQwKoYm0IQwFCMSyEIaEJpMMClcD4Qp0CYEIBNIUzRPzAPCtAVYlWEDgyAGIdTGBgAbqEJYyjqa3oAAAAASUVORK5CYII=);");
        sheet.add(".gantt_default_row_new .gantt_default_rowheader_inner:hover", "background: white;");
        sheet.add(".gantt_default_rowheader textarea", "padding: 5px;");
        sheet.add(".gantt_default_rowheader_scroll", "cursor: default;");

        sheet.commit();

        DayPilot.Global.defaultCss = true;
    })();


    var Splitter = function(id) {
        var This = this;

        this.id = id;
        //this.count = 3;
        this.widths = [];
        this.titles = [];
        this.height = null;
        //this.height = 20;
        this.splitterWidth = 3;
        //this.color = "#000000";
        //this.opacity = 60;
        //this.padding = '0px 2px 0px 2px';
        this.css = {};
        this.css.title = null;
        this.css.titleInner = null;
        this.css.splitter = null;

        // internal
        this.blocks = [];
        this.drag = {};

        // callback
        this.updated = function() {};
        this.updating = function() {};

        this.init = function() {
            var div;

            if (!id) {
                throw "error: id not provided";
            }
            else if (typeof id === 'string') {
                div = document.getElementById(id);
            }
            else if (id.appendChild) {
                div = id;
            }
            else {
                throw "error: invalid object provided";
            }

            this.div = div;
            this.blocks = [];

            for (var i = 0; i < this.widths.length; i++) {
                var s = document.createElement("div");
                s.style.display = "inline-block";
                if (This.height !== null) {
                    s.style.height = This.height + "px";
                }
                else {
                    s.style.height = "100%";
                }
                s.style.width = (this.widths[i] - this.splitterWidth) + "px";
                s.style.overflow = 'hidden';
                s.style.verticalAlign = "top";
                s.style.position = "relative";
                s.setAttribute("unselectable", "on");
                s.className = this.css.title;
                div.appendChild(s);

                var inner = document.createElement("div");
                inner.innerHTML = this.titles[i];
                inner.setAttribute("unselectable", "on");
                inner.className = this.css.titleInner;
                s.appendChild(inner);
                
                var handle = document.createElement("div");
                handle.style.display = "inline-block";
                
                //handle.style.top = "0px";
                //handle.style.left = "0px";
                //handle.style.float = "left";
                //handle.style.height = this.height + "px";
                if (This.height !== null) {
                    handle.style.height = This.height + "px";
                }
                else {
                    handle.style.height = "100%";
                }
                handle.style.width = this.splitterWidth + "px";
                handle.style.position = "relative";

                handle.appendChild(document.createElement("div"));
                /*
                handle.style.backgroundColor = this.color;
                if (this.opacity >= 0 && this.opacity <= 100) {
                    handle.style.opacity = this.opacity / 100;
                    handle.style.filter = "alpha(opacity=" + this.opacity + ")";
                }*/
                handle.style.cursor = "col-resize";
                handle.setAttribute("unselectable", "on");
                handle.className = this.css.splitter;

                var data = {};
                data.index = i;
                data.width = this.widths[i];

                handle.data = data;

                handle.onmousedown = function(ev) {
                    This.drag.start = DayPilot.page(ev);
                    This.drag.data = this.data;
                    This.div.style.cursor = "col-resize";
                    //document.body.style.cursor = "col-resize";
                    ev = ev || window.event;
                    ev.preventDefault ? ev.preventDefault() : ev.returnValue = false;
                };

                div.appendChild(handle);

                var block = {};
                block.section = s;
                block.handle = handle;
                this.blocks.push(block);
            }

            this.registerGlobalHandlers();
        }; // Init

        // resets the initial value
        this.updateWidths = function() {
            for (var i = 0; i < this.blocks.length; i++) {
                var block = this.blocks[i];
                var width = this.widths[i];
                block.handle.data.width = width;

                this._updateWidth(i);
            }
        };

        this._updateWidth = function(i) {
            var block = this.blocks[i];
            var width = this.widths[i];
            block.section.style.width = (width - this.splitterWidth) + "px";
        };

        this.totalWidth = function() {
            var t = 0;
            for (var i = 0; i < this.widths.length; i++) {
                t += this.widths[i];
            }
            return t;
        };

        this.gMouseMove = function(ev) {
            if (!This.drag.start) {
                return;
            }

            var data = This.drag.data;

            var now = DayPilot.page(ev);
            var delta = now.x - This.drag.start.x;
            var i = data.index;

            This.widths[i] = Math.max(5, data.width + delta);
            This._updateWidth(i);

            // callback
            var params = {};
            params.widths = this.widths;
            params.index = data.index;

            This.updating(params);
        };

        this.gMouseUp = function(ev) {
            if (!This.drag.start) {
                return;
            }
            This.drag.start = null;
            document.body.style.cursor = "";
            This.div.style.cursor = "";

            var data = This.drag.data;
            data.width = This.widths[data.index];

            // callback
            var params = {};
            params.widths = this.widths;
            params.index = data.index;

            This.updated(params);

        };

        this.registerGlobalHandlers = function() {
            DayPilot.re(document, 'mousemove', this.gMouseMove);
            DayPilot.re(document, 'mouseup', this.gMouseUp);
        };
    };

    DayPilot.Splitter = Splitter;    
    
})();

/* JSON */
// thanks to http://www.json.org/js.html


// declares DayPilot.JSON.stringify()
DayPilot.JSON = {};

(function() {
    function f(n) {
        return n < 10 ? '0' + n : n;
    }

    if (typeof Date.prototype.toJSON2 !== 'function') {

        Date.prototype.toJSON2 = function(key) {
            return this.getUTCFullYear() + '-' +
                    f(this.getUTCMonth() + 1) + '-' +
                    f(this.getUTCDate()) + 'T' +
                    f(this.getUTCHours()) + ':' +
                    f(this.getUTCMinutes()) + ':' +
                    f(this.getUTCSeconds()) + '';
        };

        String.prototype.toJSON =
                Number.prototype.toJSON =
                Boolean.prototype.toJSON = function(key) {
            return this.valueOf();
        };
    }

    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
            escapeable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
            gap,
            indent,
            meta = {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"': '\\"',
        '\\': '\\\\'
    },
    rep;

    function quote(string) {
        escapeable.lastIndex = 0;
        return escapeable.test(string) ?
                '"' + string.replace(escapeable, function(a) {
            var c = meta[a];
            if (typeof c === 'string') {
                return c;
            }
            return '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
        }) + '"' :
                '"' + string + '"';
    }

    function str(key, holder) {
        var i,
                k,
                v,
                length,
                mind = gap,
                partial,
                value = holder[key];
        if (value && typeof value === 'object' && typeof value.toJSON2 === 'function') {
            value = value.toJSON2(key);
        }
        else if (value && typeof value === 'object' && typeof value.toJSON === 'function' && !value.ignoreToJSON) {
            value = value.toJSON(key);
        }
        if (typeof rep === 'function') {
            value = rep.call(holder, key, value);
        }
        switch (typeof value) {
            case 'string':
                return quote(value);
            case 'number':
                return isFinite(value) ? String(value) : 'null';
            case 'boolean':
            case 'null':
                return String(value);
            case 'object':
                if (!value) {
                    return 'null';
                }
                gap += indent;
                partial = [];
                if (typeof value.length === 'number' &&
                        !value.propertyIsEnumerable('length')) {
                    length = value.length;
                    for (i = 0; i < length; i += 1) {
                        partial[i] = str(i, value) || 'null';
                    }
                    v = partial.length === 0 ? '[]' :
                            gap ? '[\n' + gap +
                            partial.join(',\n' + gap) + '\n' +
                            mind + ']' :
                            '[' + partial.join(',') + ']';
                    gap = mind;
                    return v;
                }
                if (rep && typeof rep === 'object') {
                    length = rep.length;
                    for (i = 0; i < length; i += 1) {
                        k = rep[i];
                        if (typeof k === 'string') {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ': ' : ':') + v);
                            }
                        }
                    }
                } else {
                    for (k in value) {
                        if (Object.hasOwnProperty.call(value, k)) {
                            v = str(k, value);
                            if (v) {
                                partial.push(quote(k) + (gap ? ': ' : ':') + v);
                            }
                        }
                    }
                }
                v = (partial.length === 0) ? '{\u007D' :
                        gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' +
                        mind + '\u007D' : '{' + partial.join(',') + '\u007D';
                gap = mind;
                return v;
        }
    }

    if (typeof DayPilot.JSON.stringify !== 'function') {
        DayPilot.JSON.stringify = function(value, replacer, space) {
            var i;
            gap = '';
            indent = '';
            if (typeof space === 'number') {
                for (i = 0; i < space; i += 1) {
                    indent += ' ';
                }
            } else if (typeof space === 'string') {
                indent = space;
            }
            rep = replacer;
            if (replacer && typeof replacer !== 'function' && (typeof replacer !== 'object' || typeof replacer.length !== 'number')) {
                throw new Error('JSON.stringify');
            }
            return str('', {'': value});
        };
    }

    if (typeof Sys !== 'undefined' && Sys.Application && Sys.Application.notifyScriptLoaded) {
        Sys.Application.notifyScriptLoaded();
    }

})();
