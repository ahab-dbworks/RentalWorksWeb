//---------------------------------------------------------------------------------
var FwBasePages = {};
//---------------------------------------------------------------------------------
FwBasePages.getDefaultScreen = function(viewModel) {
    var html = [];
    html.push('<div class="default-page">');
    html.push('  <div class="default-container">');
    html.push('    <div class="programlogo">');
    html.push('      <img id="programlogo" src="" alt="program logo" />')
    html.push('    </div>');
    html.push('    <div class="default-buttons">');
    html.push('      <div class="default-button btnLogin">Sign In to {{captionProgramTitle}}</div>');
    html.push('    </div>');
    html.push('  </div>');
    html.push('  <div id="master-footer">');
    html.push('    <div id="copyright">© {{valueYear}} <span id="dbworkslink">Database Works</span>.&nbsp;All Rights Reserved.</div>');
    html.push('    <div id="version">v{{valueVersion}}</div>');
    html.push('  </div>');
    html.push('</div>');

    html = html.join('');
    html = Mustache.render(html, viewModel);

    var screen = {};
    screen.$view = jQuery(html);

    screen.$view.on('click', '#dbworkslink', function() {
        try {
            window.location.href = 'http://www.dbworks.com';
        } catch (ex) {
            FwFunc.showError(ex);
        }
    });

    program.setApplicationTheme('theme-material');

    return screen;
};
//---------------------------------------------------------------------------------
FwBasePages.getLoginScreen = function(viewModel) {
    var html, screen, $loginscreen;
    screen = {};
    html = [];

    if (!viewModel.OktaEnabled) {
        html.push('<div class="login-page">');
        html.push('  <div class="login-container">');
        html.push('    <div class="programlogo">');
        html.push('      <img id="programlogo" src="" alt="program logo" />')
        html.push('    </div>');
        html.push('    <div class="login-fields">');
        html.push('      <div class="login-field" data-id="AQ0NDgwPBwg">');
        html.push('        <input id="email" class="login-field-value" type="text" autocapitalize="none" />');
        html.push('        <label class="login-field-caption" for="email">Username / Email</label>');
        html.push('      </div>');
        html.push('      <div class="login-field" data-id="BwcODw4HBw8">');
        html.push('        <input id="password" class="login-field-value" type="password" />');
        html.push('        <label class="login-field-caption" for="password">Password</label>');
        html.push('      </div>');
        html.push('    </div>');
        html.push('    <div class="errormessage"></div>');
        html.push('    <div class="login-buttons">');
        html.push('      <div class="login-button btnLogin" data-id="BA4JDgMACgA">{{captionBtnLogin}}</div>');
        html.push('      <div class="login-button btnCancel">{{captionBtnCancel}}</div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div id="master-footer">');
        html.push('    <div id="copyright">© {{valueYear}} <span id="dbworkslink">Database Works</span>.&nbsp;All Rights Reserved.</div>');
        html.push('    <div id="version">v{{valueVersion}}</div>');
        html.push('  </div>');
        html.push('</div>');
    } else {
        html.push('  <div id="okta-login-container"></div>');
    }

    html = html.join('');
    html = Mustache.render(html, viewModel);
    $loginscreen = jQuery(html);

    screen.$view = $loginscreen;
    
    if (viewModel.valueEmail !== '') {
        screen.$view.find('#email').val(viewModel.valueEmail);
        screen.$view.find('#email').siblings().addClass('active');
    }

    screen.$view
        .on('keypress', '#email', function(e) {
            e = e || window.event;
            var charCode = e.which || e.keyCode;
            switch(charCode) {
                case 13:
                    if (screen.$view.find('#password').val() === '') {
                        screen.$view.find('#password').select();
                    } else {
                        screen.$view.find('.btnLogin').click();
                    }
                    break;
            }
        })
        .on('keypress', '#password', function(e) {
            e = e || window.event;
            var charCode = e.which || e.keyCode;
            switch(charCode) {
                case 13:
                    if (screen.$view.find('#email').val() === '') {
                        screen.$view.find('#email').focus();
                    } else {
                        screen.$view.find('.btnLogin').click();
                    }
                    break;
            }
        })
        .on('focus', '.login-field-value', function() {
            var $this = jQuery(this);
            $this.siblings().addClass('active');
        })
        .on('blur', '.login-field-value', function() {
            var $this = jQuery(this);
            if ($this.val() === '') {
                $this.siblings().removeClass('active');
            }
        })
        .on('click', '#dbworkslink', function() {
            try {
                window.location.href = 'http://www.dbworks.com';
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    ;

    return screen;
};
//---------------------------------------------------------------------------------
FwBasePages.getMobileLoginScreen = function(viewModel) {
    var html, screen, $loginscreen, $fwcontrols;
    screen = {};
    html = [];

    html.push('<div class="fwpage" data-control="FwContainer">');
    html.push('  <div class="mobileloginpage">');
    html.push('    <div class="programlogo"></div>');
    html.push('    <div class="mobilelogin-fields">');
    html.push('      <div class="mobilelogin-email" data-id="AQ0NDgwPBwg">');
    html.push('        <div class="mobilelogin-email-caption">Username / Email</div>');
    html.push('        <input class="mobilelogin-email-value" type="text" value="{{valueEmail}}" autocapitalize="none" />');
    html.push('      </div>');
    html.push('      <div class="mobilelogin-password" data-id="BwcODw4HBw8">');
    html.push('        <div class="mobilelogin-password-caption">Password</div>');
    html.push('        <input class="mobilelogin-password-value" type="password" />');
    html.push('      </div>');
    html.push('    </div>');
    html.push('    <div class="mobilelogin-buttons">');
    html.push('      <div id="mobilelogin-btnLogin" data-id="BA4JDgMACgA">{{captionBtnLogin}}</div>');
    html.push('    </div>');
    html.push('    <div class="mobilelogin-footer"></div>');
    html.push('  </div>');
    html.push('</div>');
    html = html.join('');
    html = Mustache.render(html, viewModel);
    $loginscreen = jQuery(html);

    $fwcontrols = $loginscreen.find('.fwcontrol').addBack();
    FwControl.renderRuntimeControls($fwcontrols);

    screen.$view = $loginscreen;

    screen.$view
        .on('click', '.mobilelogin-email, .mobilelogin-password', function() {
            var $this = jQuery(this);
            $this.find('input').focus();
        })
        .on('keypress', '.mobilelogin-email-value', function(e) {
            e = e || window.event;
            var charCode = e.which || e.keyCode;
            switch(charCode) {
                case 13:
                    screen.$view.find('.mobilelogin-password-value').select();
                    break;
            }
        })
        .on('keypress', '.mobilelogin-password-value', function(e) {
            e = e || window.event;
            var charCode = e.which || e.keyCode;
            switch(charCode) {
                case 13:
                    if (screen.$view.find('.mobilelogin-email-value').val() === '') {
                        screen.$view.find('.mobilelogin-email-value').focus();
                    } else {
                        screen.$view.find('#mobilelogin-btnLogin').click();
                    }
                    break;
            }
        })
    ;

    return screen;
};
//---------------------------------------------------------------------------------
FwBasePages.getAboutScreen = function(viewModel) {
    var html, screen;
    screen = {};
    html = [];

    html.push('<div class="fwpage">');
    html.push('  <div class="about">');
    html.push('    <div class="programlogo">');
    html.push('      <img id="programlogo" src="" alt="program logo" />')
    html.push('    </div>');
    html.push('  </div>');
    html.push('</div>');
    html = html.join('');
    html = Mustache.render(html, viewModel);

    screen.$view = jQuery(html);

    return screen;
};
//---------------------------------------------------------------------------------
FwBasePages.getSupportScreen = function() {

};
//---------------------------------------------------------------------------------
FwBasePages.getPasswordRecoveryScreen = function() {

};
//---------------------------------------------------------------------------------
