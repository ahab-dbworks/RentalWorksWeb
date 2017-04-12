var cdvapp = {};
//------------------------------------------------------------------------
cdvapp.error = function(ex) {
    alert(JSON.stringify(ex));
};
//------------------------------------------------------------------------
cdvapp.initialize = function() {
    try {
        window.addEventListener('load', cdvapp.load);
    } catch(ex) {
        cdvapp.error(ex);
    }
};
//------------------------------------------------------------------------
cdvapp.ajaxGet = function(url, onsuccess, onerror) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState === 4 && xmlhttp.status === 200) {
            onsuccess(xmlhttp.responseText);
        } else {
            //onerror(xmlhttp.status.toString());
        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
};
//------------------------------------------------------------------------
cdvapp.loadUrl = function(url, onsuccess, onerror) {
    cdvapp.ajaxGet(url,
        function(response) {
            var html;
            html = response.replace(/\[appbaseurl\]/g, cdvapp.appbaseurl);
            onsuccess(html);
        },
        function(error) {
            onerror(error);
        }
    );
};
//------------------------------------------------------------------------
cdvapp.bindEvents = function() {
    
};
//------------------------------------------------------------------------
cdvapp.load = function() {
    var el_loadingmessage, el_head, el_cdvapp, el_appscripts, nocache, nocachestr, app_head_url, app_body_url, app_scripts_url, app_script_url, app_version_url, version, versionstr;
    
    nocache = true;
    nocachestr = '?cache=' + new Date().getTime();
    nocachestr = '';
    el_level1     = document.getElementById('level1');
    el_level2     = document.getElementById('level2');
    el_level3     = document.getElementById('level3');
    el_head       = document.getElementById('head');
    el_cdvapp     = document.getElementById('cdvapp');
    el_appscripts = document.getElementById('appscripts');
    el_loadingmessage = document.getElementById('index-loadingmessage');
    cdvapp.appbaseurl          = applicationConfig.appbaseurl;
    cdvapp.fwvirtualdirectory  = applicationConfig.fwvirtualdirectory;
    cdvapp.appvirtualdirectory = applicationConfig.appvirtualdirectory;
    cdvapp.localbaseurl   = window.location.pathhname;
    app_version_url = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'version.txt' + nocachestr;
    cdvapp.loadUrl(app_version_url, function(version) {
        el_loadingmessage.innerHTML = 'Loading HTML...';
        document.getElementById('index-version').innerHTML = 'Version: ' + version;
        versionstr = '?cache=' + version;
        if (cdvapp.appvirtualdirectory === '') {
            app_head_url    = 'app_head.htm' + versionstr;
            app_body_url    = 'app_body.htm' + versionstr;
        } else {
            app_head_url    = 'app_head.htm' + nocachestr;
            app_body_url    = 'app_body.htm' + nocachestr;
        }
        app_scripts_url = 'app_scripts.json' + nocachestr;
        cdvapp.loadUrl(app_head_url, function(htmlhead) {
            cdvapp.loadUrl(app_body_url, function(htmlbody) {
                cdvapp.ajaxGet(app_scripts_url, function(response) {
                    htmlhead = htmlhead.replace(/\[fwvirtualdirectory\]/g, cdvapp.fwvirtualdirectory);
                    htmlhead = htmlhead.replace(/\[appvirtualdirectory\]/g, cdvapp.appvirtualdirectory);
                    el_head.innerHTML = htmlhead;
                    htmlbody = htmlbody.replace(/\[fwvirtualdirectory\]/g, cdvapp.fwvirtualdirectory);
                    htmlbody = htmlbody.replace(/\[appvirtualdirectory\]/g, cdvapp.appvirtualdirectory);
                    el_cdvapp.innerHTML = htmlbody;
                    cdvapp.scripturls = [];
                    cdvapp.scriptloadindex = 0;
                    var scripts = JSON.parse(response).scripts;
                    for (var i = 0; i < scripts.length; i++) {
                        app_script_url = scripts[i].src.replace(/\[appbaseurl\]/g, cdvapp.appbaseurl);
                        app_script_url = app_script_url.replace(/\[fwvirtualdirectory\]/g, cdvapp.fwvirtualdirectory);
                        app_script_url = app_script_url.replace(/\[appvirtualdirectory\]/g, cdvapp.appvirtualdirectory);
                        app_script_url = app_script_url + nocachestr;
                        cdvapp.scripturls.push(app_script_url);
                    }
                    cdvapp.loadscripts();
                }, cdvapp.error);
            }, cdvapp.error);
        }, cdvapp.error);
    }, cdvapp.error);
};
//------------------------------------------------------------------------
cdvapp.loadscripts = function(){
    var el_loadingmessage, script;
    
    el_loadingmessage = document.getElementById('index-loadingmessage');
    script = document.createElement('script');
    if ((cdvapp.scripturls.length > 0) && (cdvapp.scriptloadindex < cdvapp.scripturls.length)) {
        el_loadingmessage.innerHTML = 'Loading scripts...';
        script.src  = cdvapp.scripturls[cdvapp.scriptloadindex];
        cdvapp.scriptloadindex++;
        console.log('Loading: ' + script.src);
        script.type = 'text/javascript';
        //real browsers
        script.onload = cdvapp.loadscripts;
        //Internet explorer
        script.onreadystatechange = function() {
            if (this.readyState === 'complete') {
                cdvapp.loadscripts();
            }
        };
        document.body.appendChild(script);
    }  else {
        document.getElementById('cdvapploadinginfo').style.display = 'none';
        document.getElementById('cdvapploading').style.display = 'none';
        document.getElementById('cdvapp').style.display = 'block';
    }
};
//------------------------------------------------------------------------
cdvapp.initialize();