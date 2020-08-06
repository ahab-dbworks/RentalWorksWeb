var Constants = {
    Modules: {
        Mobile: {
            id: 'Mobile',
            caption: 'QuikScan',
            nodetype: 'Category',
            children: {
                //RFIDCheckIn: {caption: 'RFID Check-In', visible: false, id: 'a8eGjTCPFhcv', nav: 'rfidcheckin', iconurl: "theme/images/icons/128/rfidstaging.001.png", htmlcaption: 'RFID<br>Check-In', applicationoptions: 'rfid', usertype: 'USER', nodetype: 'Module'},
                //RFIDStaging: {caption: 'RFID Staging', visible: false, id: 'hAqBNCg82m2U', nav: 'rfidstaging', iconurl: "theme/images/icons/128/rfidstaging.001.png", htmlcaption: 'RFID<br>Staging', applicationoptions: 'rfid', usertype: 'USER', nodetype: 'Module'},
                Staging: {caption: 'Staging', visible: true, id: 'GRHqRAwrYYk0', nav: 'staging', iconurl: "theme/images/icons/128/staging.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                CheckIn: {caption: 'Check-In', visible: true, id: 'S2kfsHmsu1oO', nav: 'order/checkinmenu', iconurl: "theme/images/icons/128/checkin.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                ReceiveOnSet: {caption: 'Receive On Set', visible: true, id: 'FQzkXOWEfgTO', nav: 'receiveonset', iconurl: "theme/images/icons/128/receiveset.png", htmlcaption: '', applicationoptions: 'production', usertype: 'USER', nodetype: 'Module'},
                AssetSetLocation: {caption: 'Item Set Location', visible: true, id: 'L8b4IMFGH6Vy', nav: 'assetsetlocation', iconurl: "theme/images/icons/128/setlocation.png", htmlcaption: '', applicationoptions: 'production', usertype: 'USER', nodetype: 'Module'},
                //Exchange: {caption: 'Exchange', visible: false, id: '6Ow8BBaVymuo', nav: 'utilities/exchange', iconurl: "theme/images/icons/128/exchange.001.png", htmlcaption: '', applicationoptions:'', usertype: '', nodetype: 'Module'},
                POReceive: {caption: 'PO Receive', visible: true, id: 'jWEmoA2ducSl', nav: 'inventory/subreceive', iconurl: "theme/images/icons/128/subreceive.png", htmlcaption: 'PO<br>Receive', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                POReturn: {caption: 'PO Return', visible: true, id: 'eXB6m15leORx', nav: 'inventory/subreturn', iconurl: "theme/images/icons/128/subreturn.png", htmlcaption: 'PO<br>Return', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                ItemStatus: {caption: 'Item Status', visible: true, id: 'RCPFdx5BHa6s', nav: 'order/itemstatus', iconurl: "theme/images/icons/128/orderstatus.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                QC: {caption: 'QC', visible: true, id: 'BJvIbGLdfA7m', nav: 'inventory/qc', iconurl: "theme/images/icons/128/qc.png", htmlcaption: '', applicationoptions:'', usertype: 'USER', nodetype: 'Module'},
                TransferOut: {caption: 'Transfer Out', visible: true, id: 'mm8fUVFspz1x', nav: 'transferout', iconurl: "theme/images/icons/128/transferout.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                TransferIn: {caption: 'Transfer In', visible: true, id: 'zMaxj2QXfuaA', nav: 'order/transferin', iconurl: "theme/images/icons/128/transferin.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                Repair: {caption: 'Repair', visible: true, id: 'etbWAOdN0p1J', nav: 'inventory/repairmenu', iconurl: "theme/images/icons/128/repair.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                AssetDisposition: {caption: 'Asset Disposition', visible: true, id: '2KbMXwEKkQe2', nav: 'inventory/assetdisposition', iconurl: "theme/images/icons/128/assetdisposition.001.png", htmlcaption: '', applicationoptions: 'production', usertype: 'USER', nodetype: 'Module'},
                PackageTruck: {caption: 'Package Truck', visible: true, id: 'W4sg9KXex7L0', nav: 'order/packagetruck', iconurl: "theme/images/icons/128/package-truck.001.png", htmlcaption: '', applicationoptions: 'packagetruck', usertype: 'USER', nodetype: 'Module'},
                QuikPick: {caption: 'QuikPick', visible: true, id: 'fM17eHF85Uen', nav: 'quikpick', iconurl: "theme/images/icons/128/quikpick.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                TimeLog: {caption: 'Time Log', visible: true, id: 'JqwwOdl6rQPa', nav: 'timelog', iconurl: "theme/images/icons/128/timelog.png", htmlcaption: '', applicationoptions: 'crew', usertype: 'USER,CREW', nodetype: 'Module'},
                FillContainer: {caption: 'Fill Container', visible: true, id: 't4YTMXX0TFgw', nav: 'order/fillcontainer', iconurl: "theme/images/icons/128/fillcontainer.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                InventoryWebImage: {caption: 'Inventory Web Image', visible: true, id: 'blvusPl8F6yl', nav: 'inventory/inventorywebimage', iconurl: "theme/images/icons/128/webimage.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                PhysicalInventory: {caption: 'Physical Inventory', visible: true, id: 'aO5jb7MLcSX2', nav: 'physicalinventory', iconurl: "theme/images/icons/128/physicalinv.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                MoveBCLocation: {caption: 'Move To Aisle/Shelf', visible: true, id: 'gQERbwRwFOhN', nav: 'inventory/movebclocation', iconurl: "theme/images/icons/128/moveto.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                AssignItems: {caption: 'Assign Items', visible: true, id: 'zlz0ompN1E8D', nav: 'assignitems', iconurl: "theme/images/icons/128/assignitems.001.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'},
                BarcodeLabel: {caption: 'Barcode Label', visible: true, id: '0TLuX86CwIz4', nav: 'barcodelabel', iconurl: "theme/images/icons/128/barcodelabel.001.png", htmlcaption: '', applicationoptions: '', usertype: 'USER', nodetype: 'Module'}
            }
        }
    
    }
}