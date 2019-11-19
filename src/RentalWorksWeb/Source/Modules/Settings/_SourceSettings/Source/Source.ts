class Source {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Source';
        this.apiurl = 'api/v1/source';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Source', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form, $sourcetype, setFormProperties;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        setFormProperties = function ($form: any) {
            var $cbUseProxy, $txtProxy, $txtProxyPort, $txtProxyUsername, $txtProxyPassword, $cbArchiveFile, $txtArchivePath;

            $cbUseProxy = $form.find('div[data-datafield="UseProxy"]');
            $txtProxy = $form.find('div[data-datafield="Proxy"]');
            $txtProxyPort = $form.find('div[data-datafield="ProxyPort"]');
            $txtProxyUsername = $form.find('div[data-datafield="ProxyUserName"]');
            $txtProxyPassword = $form.find('div[data-datafield="ProxyPassword"]');
            $cbArchiveFile = $form.find('div[data-datafield="FtpArchive"]');
            $txtArchivePath = $form.find('div[data-datafield="FtpArchivePath"]');

            if (FwFormField.getValue2($cbUseProxy) === true) {
                FwFormField.enable($txtProxy);
                FwFormField.enable($txtProxyPort);
                FwFormField.enable($txtProxyUsername);
                FwFormField.enable($txtProxyPassword);
            } else {
                FwFormField.disable($txtProxy);
                FwFormField.disable($txtProxyPort);
                FwFormField.disable($txtProxyUsername);
                FwFormField.disable($txtProxyPassword);
            }

            if (FwFormField.getValue2($cbArchiveFile) === true) {
                FwFormField.enable($txtArchivePath);
            } else {
                FwFormField.disable($txtArchivePath);
            }
        };

        $sourcetype = $form.find('div[data-datafield="SourceType"]');
        FwFormField.loadItems($sourcetype, [
            { value: 'FTP', text: 'FTP' },
            { value: 'SOAP', text: 'SOAP' },
            { value: 'FILE', text: 'FILE' },
            { value: 'PATH', text: 'PATH' }
        ]);

        $form
            .on('change', 'div[data-datafield="SourceType"]', function () {
                var $source, $soapsection, $ftpsection;

                $ftpsection = $form.find('div.fwform-section[data-caption="FTP"]');
                $soapsection = $form.find('div.fwform-section[data-caption="SOAP"]');
                $source = $form.find('div[data-datafield="Source"]');

                $source.hide();
                $ftpsection.hide();
                $soapsection.hide();
                switch (jQuery(this).find('.fwformfield-value').val()) {
                    case 'PATH':
                        $source.show();
                        $source.find('.fwformfield-caption').html('Directory');
                        break;
                    case 'FILE':
                        $source.show();
                        $source.find('.fwformfield-caption').html('File Name');
                        break;
                    case 'SOAP':
                        $source.show();
                        $source.find('.fwformfield-caption').html('URL');
                        $soapsection.show();
                        break;
                    case 'FTP':
                        $ftpsection.show();
                        break;
                }
            })
            .on('change', 'div[data-datafield="FtpArchive"]', function () {
                setFormProperties($form);
            })
            .on('change', 'div[data-datafield="UseProxy"]', function () {
                setFormProperties($form);
            })
            ;

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="SourceId"] input').val(uniqueids.SourceId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
        var $sourcetype, $source, $soapsection, $ftpsection;

        $ftpsection = $form.find('div.fwform-section[data-caption="FTP"]');
        $soapsection = $form.find('div.fwform-section[data-caption="SOAP"]');
        $source = $form.find('div[data-datafield="Source"]');
        $sourcetype = $form.find('div[data-datafield="SourceType"]');
        FwFormField.disable($sourcetype);

        switch ($sourcetype.find('.fwformfield-value').val()) {
            case 'PATH':
                $source.show();
                $source.find('.fwformfield-caption').html('Directory');
                break;
            case 'FILE':
                $source.show();
                $source.find('.fwformfield-caption').html('File Name');
                break;
            case 'SOAP':
                $source.show();
                $source.find('.fwformfield-caption').html('URL');
                $soapsection.show();
                break;
            case 'FTP':
                $ftpsection.show();
                break;
        }
    }
}

var SourceController = new Source();