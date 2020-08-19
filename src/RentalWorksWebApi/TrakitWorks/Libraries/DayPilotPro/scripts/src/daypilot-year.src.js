/* Copyright © 2005 - 2014 Annpoint, s.r.o.
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
    

    if (typeof DayPilot.Year !== 'undefined') {
        return;
    }
	
    DayPilot.Year = function(id, options) {
        this.v = '1338';
        var This = this;
        
        this.startDate = new DayPilot.Date();
        this.cssOnly = true;
        this.cssClassPrefix = null;
        this.columns = 4;
        this.cellWidth = 20;
        this.showWeekNumbers = false;
        
        this._navigators = [];
        
        this._prepare = function() {
            this.root.dp = this;
            this._start = new DayPilot.Date(this.startDate.toString("yyyy-01-01", "en-us"));
        };
        
        this._prefixCssClass = function(part) {
            if (this.cssClassPrefix) {
                return this.cssClassPrefix + part;
            }
            else {
                return "";
            }
        };
        
        this._weekNumberWidth = function() {
            if (this.showWeekNumbers) {
                return this.cellWidth;
            }
            return 0;
        };        
        
        this._draw = function() {
            this._navigators = [];
            
            var columns = this.columns;
            var rows = 12 / columns;
            
            this.root.className = this._prefixCssClass('_main');
            this.root.style.width = columns * (this.cellWidth * 7 + this._weekNumberWidth()) + 'px';
            
            for (var y = 0; y < rows; y++) {
                for (var x = 0; x < columns; x++) {
                    var m = y * columns + x;
                    var id = id + "_nav_" + m;
                    
                    var div = document.createElement("div");
                    div.id = id;
                    div.style.cssFloat = 'left';
                    
                    this.root.appendChild(div);
                    
                    var navigator = new DayPilot.Navigator(id);
                    navigator.startDate = this._start.addMonths(m);
                    navigator.cssOnly = this.cssOnly;
                    navigator.cssClassPrefix = this.cssClassPrefix;
                    navigator.cellWidth = this.cellWidth;
                    navigator.showWeekNumbers = this.showWeekNumbers;
                    navigator.internal.showLinks = {};
                    navigator.selectMode = 'none';
                    navigator.timeRangeSelectedHandling = "JavaScript";
                    navigator.onTimeRangeSelected = this._onTimeRangeSelected;
                    navigator.init();
                    navigator.root.className = '';  // clear the top style
                    
                    this._navigators.push(navigator);
                }
                var line = document.createElement("div");
                line.style.clear = 'left';
                this.root.appendChild(line);
            }
        };
        
        this._onTimeRangeSelected = function(args) {
            alert("clicked: " + args.day);
        };
        
        this.init = function() {
            this.root = document.getElementById(id);
            
            if (this.root.dp) { // already initialized
                return;
            }
            
            this._prepare();
            this._draw();
        };


    };

	
})();