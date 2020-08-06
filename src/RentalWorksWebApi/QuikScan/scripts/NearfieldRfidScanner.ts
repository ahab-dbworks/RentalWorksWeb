class NearfieldRfidScannerClass {
    private isEnabled: boolean = false;
    private isListening: boolean = false;
    private onScanUid: (uid: string, uidType: string) => void;
    private lastUid: string = '';
    private ignoreLastUidUntil: Date = new Date();
    
    init(onScanUid: (uid: string, uidType: string) => void): void {
        if (program.hasHfRfidApplicationOption) {
            this.onScanUid = onScanUid;
        }
    }
    
    // This will enable the scanner and listen for a scan and then become disabled after a successful scan.  Programmer must re-enable the scanner after a succesful scan if they want it back on.
    enable() {
        if (program.hasHfRfidApplicationOption) {
            if (!this.isEnabled && typeof (<any>window).DTDevices !== 'undefined' && typeof (<any>window).DTDevices.rfInitWithFieldGain === 'function') {
                DTDevices.rfInitWithFieldGain('ISO15', -1000,
                    // success
                    () => {
                        try {
                            this.isEnabled = true;
                            //FwNotification.renderNotification('SUCCESS', 'Enabled nearfield scanner.');
                            this.startListening();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    },
                    // error
                    () => {
                        try {
                            this.isEnabled = false;
                            //FwNotification.renderNotification('ERROR', 'Can\'t enable nearfield scanner.');
                            this.stopListening();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                );
            }
        }
    }

    disable() {
        this.isEnabled = false;
        this.stopListening();
    }

    private startListening(): void {
        if (program.hasHfRfidApplicationOption) {
            if (!this.isListening && typeof (<any>window).DTDevices !== 'undefined' && typeof (<any>window).DTDevices.rfInitWithFieldGain === 'function') {
                //FwNotification.renderNotification('INFO', 'Start Listening for Nearfield Scans.');
                DTDevices.registerListener('rfCardDetected', 'rfCardDetected_applicationjs', 
                    // success
                    (uid, uidType) => {
                        try {
                            this.isEnabled = false;
                            //FwNotification.renderNotification('INFO', `this.lastUid === '': ${this.lastUid === ''}`);
                            //FwNotification.renderNotification('INFO', `uid !== this.lastUid: ${uid !== this.lastUid}`);
                            //FwNotification.renderNotification('INFO', `uid !== this.lastUid && new Date() > this.ignoreLastUidUntil: ${uid !== this.lastUid && new Date() > this.ignoreLastUidUntil}`);
                            if (this.lastUid === '' || uid !== this.lastUid || (uid === this.lastUid && new Date() > this.ignoreLastUidUntil)) {
                                this.lastUid = uid;
                                let ignoreDate = new Date();
                                ignoreDate.setSeconds(ignoreDate.getSeconds() + 5);
                                this.ignoreLastUidUntil = ignoreDate;
                                //FwNotification.renderNotification('INFO', uid);
                                try {
                                    this.onScanUid(uid, uidType);
                                } catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            }
                            this.enable();
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                );
            }
        }
    }

    private stopListening(): void {
        if (program.hasHfRfidApplicationOption) {
            if (this.isListening && typeof window.DTDevices !== 'undefined') {
                this.isListening = false;
                window.DTDevices.unregisterListener('barcodeData', 'barcodeData_assetdisposition');
            }
        }
    }
}
var NearfieldRfidScanner = new NearfieldRfidScannerClass();