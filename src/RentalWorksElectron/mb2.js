const path = require('path');
const url = require('url');

var cdvapp = {};
//------------------------------------------------------------------------
cdvapp.error = function(ex) {
    if (typeof ex === 'object') {
        alert(ex.message);
    } else if (typeof ex === 'string') {
        setTimeout(function() {
            //document.getElementById('index-loadingmessage').innerHTML = ex + '\n\n-Long press the screen and press Main Menu.\n\n-If that option is not available because you set the Start in Browser mode, then force quit the app by pressing the Apple Home button once, then twice, then swipe away the app.';
            document.getElementById('cdvapploading').style.display = 'none';
            document.getElementById('index-loadingmessage').innerHTML = ex + '<br><br>Long press screen (iOS) or back button (Android)';
        }, 1);
        //alert(ex + '\n\n-Long press the screen and press Main Menu.\n\n-If that option is not available because you set the Start in Browser mode, then force quit the app by pressing the Apple Home button once, then twice, then swipe away the app.');
    }
};
//------------------------------------------------------------------------
cdvapp.initialize = function() {
    try {
        //document.addEventListener('deviceready', cdvapp.onDeviceReady, false);
		cdvapp.onDeviceReady();
    } catch(ex) {
        cdvapp.error(ex);
    }
};
//------------------------------------------------------------------------
cdvapp.ajaxGet = function(url, onsuccess, onerror) {
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.timeout = 20000;
    xmlhttp.ontimeout = function() {
        //cdvapp.error('Timed out loading: ' + url);
        cdvapp.error('Unable to connect to web server.\n\n(' + url + ')');
    }
    xmlhttp.onreadystatechange = function() {
        switch(xmlhttp.readyState) {
            case 0:
                document.getElementById('index-loadingmessage').innerHTML = 'Preparing to Connect...';
                break;
            case 1:
                document.getElementById('index-loadingmessage').innerHTML = 'Opening Connection...';
                break;
            case 2:
                document.getElementById('index-loadingmessage').innerHTML = 'Header Received';
                break;
            case 3:
                document.getElementById('index-loadingmessage').innerHTML = 'Downloading';
                break;
            case 4:
                document.getElementById('index-loadingmessage').innerHTML = 'Done';
                if (xmlhttp.status == 200) {
                    onsuccess(xmlhttp.responseText);
                } else {
                    cdvapp.error('<div style="color:#ff0000;" onclick="alert(\'' + url + '\')">Unable to connect.<br><br>' + xmlhttp.status + ' '  + xmlhttp.statusText + '</div>');
                }
                break;
        
        }
//        if (xmlhttp.status == 200) {
//            onsuccess(xmlhttp.responseText);
//        } else {
//            //onerror(xmlhttp.status.toString());
//            if (xmlhttp.readyState > 0 && xmlhttp.readyState < 4) {
//                cdvapp.error(xmlhttp.status + ' - Unable to connect to web server.<br><br>(' + url + ')');
//                xmlhttp.abort();
//            }
//        }
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
        function(ex) {
            cdvapp.error(ex);
        }
    );
};
//------------------------------------------------------------------------
cdvapp.onDeviceReady = function() {
    var el_cdvapploading, el_loadingmessage, el_version, el_head, el_cdvapp, el_appscripts, nocachestr, app_head_url, app_body_url, app_scripts_url, script_url, app_version_url, version, versionstr, applicationName;
    
    nocachestr = '?cache=' + new Date().getTime();
    //screen.lockOrientation('portrait-primary');
    //StatusBar.overlaysWebView(false);
    //StatusBar.backgroundColorByHexString('#000000');
    //StatusBar.styleLightContent();
    el_head           = document.getElementById('head');
    el_cdvapp         = document.getElementById('cdvapp');
    el_appscripts     = document.getElementById('appscripts');
    el_cdvapploading  = document.getElementById('cdvapploading');
    el_loadingmessage = document.getElementById('index-loadingmessage');
    el_version        = document.getElementById('index-version');
    
    // get the settings url
    function onGetSettingsUrlSuccess(settings) {
        applicationName            = settings[0];
        cdvapp.appbaseurl          = settings[1];
        cdvapp.fwvirtualdirectory  = settings[2];
        cdvapp.appvirtualdirectory = settings[3];
        cdvapp.localbaseurl   = window.location.pathname;
        app_version_url = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'version.txt' + nocachestr;
        app_scripts_url = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_scripts.json' + nocachestr;
        el_loadingmessage.innerHTML = 'Connecting...';
        cdvapp.loadUrl(app_version_url, function(version) {
            var websiteNeedsUpgrade, minWebVersion;
            
            websiteNeedsUpgrade = false;
            if (applicationName === 'RentalWorks') {
                minWebVersion       = '2015.1.2.0';
                websiteNeedsUpgrade = cdvapp.needsUpgrade(version, minWebVersion);
                if (websiteNeedsUpgrade) {
                    el_cdvapploading.style.display = 'none';
                    el_loadingmessage.innerHTML = 'Website update required.';
                    el_version.innerHTML        = 'Long press the screen for more options...';
                    setTimeout(function() {
                        alert('This app requires a new version of RentalWorks QuikScan to be installed on your web server.\n\nMinimum version is: ' + minWebVersion + '\nCurrent version is: ' + version + '\n\nPlease contact your IT staff and/or Database Works (rentalworks@dbworks.com) for assistance. As of iOS 9, the way to exit this app is to double-click the Home button to see recently used apps, then swipe up on this app.');
                    }, 100);
                }
            } else if (applicationName === 'GateWorks') {
                minWebVersion = '2015.1.2.0';
                cdvapp.needsUpgrade(version, minWebVersion)
                if (websiteNeedsUpgrade) {
                    el_cdvapploading.style.display = 'none';
                    el_loadingmessage.innerHTML = 'Website update required.';
                    el_version.innerHTML        = 'Long press the screen for more options...';
                    setTimeout(function() {
                        alert('This app requires a new version of GateWorks Mobile to be installed on your web server.\n\nMinimum version is: ' + minWebVersion + '\nCurrent version is: ' + version + '\n\nPlease contact your IT staff and/or Database Works (gateworks@dbworks.com) for assistance. As of iOS 9, the way to exit this app is to double-click the Home button to see recently used apps, then swipe up on this app.');
                    }, 100);
                }
            }
            if (!websiteNeedsUpgrade) {
                el_loadingmessage.innerHTML = 'Loading HTML...';
                el_version.innerHTML        = 'Version: ' + version;
                versionstr = '?cache=' + version;
//                if (cdvapp.appvirtualdirectory === '') {
//                    app_head_url    = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_head.htm' + versionstr;
//                    app_body_url    = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_body.htm' + versionstr;
//                } else {
                    app_head_url    = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_head.htm' + nocachestr;
                    app_body_url    = cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_body.htm' + nocachestr;
//                }
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
//                            if (cdvapp.appvirtualdirectory === '') {
//                               cdvapp.scripturls.push(cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_startupscript.js' + versionstr);
//                            } else {
                                cdvapp.scripturls.push(cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'app_startupscript.js' + nocachestr);
//                            }
                            cdvapp.scripturls.push(cdvapp.appbaseurl + cdvapp.appvirtualdirectory + 'ApplicationConfig.js' + nocachestr);
                            for (var i = 0; i < scripts.length; i++) {
                                script_url = scripts[i].src.replace(/\[appbaseurl\]/g, cdvapp.appbaseurl);
                                script_url = script_url.replace(/\[fwvirtualdirectory\]/g, cdvapp.fwvirtualdirectory);
                                script_url = script_url.replace(/\[appvirtualdirectory\]/g, cdvapp.appvirtualdirectory);
                                script_url = script_url + nocachestr;
                                cdvapp.scripturls.push(script_url);
                            }
                            cdvapp.loadscripts(cdvapp.appbaseurl, nocachestr);
                        }, cdvapp.error);
                    }, cdvapp.error);
                }, cdvapp.error);
            }
        }, cdvapp.error);
    };
	onGetSettingsUrlSuccess([
		"RentalWorks QuikScan", 
		'http://192.168.0.13/', 
		'fwjson/', 
		'qs2015/']);
};
//------------------------------------------------------------------------
cdvapp.loadscripts = function(appbaseurl, nocachestr) {
    var el_loadingmessage, script;
    
    el_loadingmessage = document.getElementById('index-loadingmessage');
    script = document.createElement('script');
    if ((cdvapp.scripturls.length > 0) && (cdvapp.scriptloadindex < cdvapp.scripturls.length)) {
        //el_loadingmessage.innerHTML = 'Loading: ' + cdvapp.scripturls[cdvapp.scriptloadindex];
        el_loadingmessage.innerHTML = 'Loading:<br/>' + cdvapp.scripturls[cdvapp.scriptloadindex].replace(appbaseurl, '').replace(nocachestr, '');
        document.body.appendChild(script);
        script.type = 'text/javascript';
        script.onload = function() {
            cdvapp.loadscripts(appbaseurl, nocachestr);
        };
        script.onerror = function() {
            alert("Unable to load: " + script.src);
        };
        script.src  = cdvapp.scripturls[cdvapp.scriptloadindex];
        cdvapp.scriptloadindex++;
    }  else {
        script = document.createElement('script');
        script.type = 'text/javascript';
        script.innerHTML = "applicationConfig.appbaseurl = '" + appbaseurl + "';"
        document.body.appendChild(script);
        document.getElementById('cdvapploading').style.display = 'none';
        document.getElementById('cdvapploadinginfo').style.display = 'none';
        document.getElementById('cdvapp').style.display = 'block';
    }
}
//------------------------------------------------------------------------
cdvapp.needsUpgrade = function(version, minVersion) {
    var versionArray, minVersionArray, vMajor, vMinor, vBuild, vRevision, minVmajor, minVMinor, minVBuild, minVRevision, needsUpgrade;
    
    versionArray    = version.split('.');
    vMajor          = parseInt(versionArray[0]);
    vMinor          = parseInt(versionArray[1]);
    vBuild          = parseInt(versionArray[2]);
    vRevision       = parseInt(versionArray[3]);
    minVersionArray = minVersion.split('.');
    minVMajor       = parseInt(minVersionArray[0]);
    minVMinor       = parseInt(minVersionArray[1]);
    minVBuild       = parseInt(minVersionArray[2]);
    minVRevision    = parseInt(minVersionArray[3]);
    needsUpgrade    = (vMajor < minVMajor) ||
                      ((vMajor == minVMajor) && (vMinor < minVMinor)) ||
                      ((vMajor == minVMajor) && (vMinor == minVMinor) && (vBuild < minVBuild)) ||
                      ((vMajor == minVMajor) && (vMinor == minVMinor) && (vBuild == minVBuild) && (vRevision < minVRevision));
    
    return needsUpgrade;
};
//------------------------------------------------------------------------
setTimeout(function() {
    cdvapp.initialize();
}, 500);