clear
try {
    $enableLogging = $true

    # starting logging
    if ($enableLogging) {
        $VerbosePreference = "Continue"
        $LogPath = Split-Path $MyInvocation.MyCommand.Path
        $LogPathName = Join-Path -Path $LogPath -ChildPath "$($MyInvocation.MyCommand.Name).log"
        Start-Transcript $LogPathName
    }

    # validate environment variables and directories exist
    If (-not(Test-Path Env:DwRentalWorksWebPath)) { throw "Please set System Environment Variable 'DwRentalWorksWebPath' to the repostory path of RentalWorksWeb for example: C:\gitprojects\RentalWorksWeb" }
    # If (-not(Test-Path Env:DwFwPath))           { throw "Please set System Environment Variable 'DwFwPath' to the repostory path of Fw for example C:\gitprojects\Fw" }
    If (-not(Test-Path "$Env:DwRentalWorksWebPath" -PathType Container)) { throw "The System Environment Variable 'DwRentalWorksWebPath' is set to a directory that does not exist: $Env:DwRentalWorksWebPath" }
    # If (-not(Test-Path "$Env:DwFwPath" -PathType Container))           { throw "The System Environment Variable 'DwFwPath' is set to a directory that does not exist: $Env:DwFwPath" }

    # sync Source files from RentalWorksWeb
    $modules = @(
    
        # Modules: Base
        'Modules\AaBaseModules\CheckInBase',
        'Modules\AaBaseModules\OrderStatusBase',
        'Modules\AaBaseModules\StagingCheckoutBase', 
        
        # Modules: Administator
        'Modules\Administrator\Alert',
        'Modules\Administrator\CustomForm',
        'Modules\Administrator\CustomField',
        'Modules\Administrator\CustomReportLayout',
        'Modules\Administrator\DuplicateRule',
        'Modules\Administrator\EmailHistory',
        'Modules\Administrator\Group',
        'Modules\Administrator\Hotfix',
        'Modules\Administrator\Reports', 
        'Modules\Administrator\Settings'

        # Modules: Warehouse
        'Modules\Warehouse\AssignBarCodes',
        'Modules\Warehouse\CheckIn',
        'Modules\Warehouse\Exchange',
        'Modules\Warehouse\OrderStatus',
        'Modules\Warehouse\ReceiveFromVendor',
        'Modules\Warehouse\ReturnToVendor',
        'Modules\Warehouse\StagingCheckout',
        'Modules\Home\SuspendedSession',
        
        # Modules: Settings
        "Modules\Settings\AddressSettings\Country",
        "Modules\Settings\AddressSettings\State",
        "Modules\Settings\CompanyDepartmentSettings\Department",
        "Modules\Settings\ContactSettings\ContactTitle",
        "Modules\Settings\CurrencySettings\Currency",
        "Modules\Settings\DealSettings\DealClassification",
        "Modules\Settings\DealSettings\DealStatus",
        "Modules\Settings\DealSettings\DealType",
        "Modules\Settings\InventorySettings\Attribute",
        "Modules\Settings\InventorySettings\InventoryCondition",
        "Modules\Settings\InventorySettings\InventoryRank",
        "Modules\Settings\InventorySettings\InventoryType",
        "Modules\Settings\InventorySettings\RentalCategory",
        "Modules\Settings\InventorySettings\Unit",
        "Modules\Settings\OfficeLocationSettings\OfficeLocation",
        "Modules\Settings\OrderSettings\OrderType",
        "Modules\Settings\POSettings\POClassification",
        "Modules\Settings\POSettings\POType",
        "Modules\Settings\RepairSettings\RepairItemStatus",        
        "Modules\Settings\ShipViaSettings\ShipVia",
        "Modules\Settings\VendorSettings\OrganizationType",
        "Modules\Settings\WarehouseSettings\Warehouse",

        # Modules: Utilities
        "Modules\Utilities\QuikActivityCalendar",

        # Reports
        'Modules\Reports\DealOutstandingItemsReport',
        'Modules\Reports\LateReturnsReport',,
        'Modules\Reports\OrderReport',
        'Modules\Reports\OrderStatusDetailReport',
        'Modules\Reports\OrderStatusSummaryReport',
        'Modules\Reports\OutContractReport',
        'Modules\Reports\PickListReport',
        'Modules\Reports\PurchaseOrderReturnListReport',
        'Modules\Reports\QuoteReport'
        'Modules\Reports\RentalInventoryAttributesReport',
        'Modules\Reports\RentalInventoryCatalogReport',
        'Modules\Reports\RentalInventoryChangeReport',
        'Modules\Reports\RentalInventoryPurchaseHistoryReport',
        'Modules\Reports\RentalInventoryValueReport',
        'Modules\Reports\RetiredRentalInventoryReport',
        'Modules\Reports\ReturnListReport',
        'Modules\Reports\UnretiredRentalInventoryReport',

        # Grids
        'Grids\AttributeValueGrid',
        'Grids\AuditHistoryGrid',
        'Grids\CheckedInItemGrid',
        'Grids\CheckedOutItemGrid',
        'Grids\CheckInExceptionGrid',
        'Grids\CheckInOrderGrid',
        'Grids\CheckInQuantityItemsGrid',
        'Grids\CheckInSwapGrid',
        'Grids\CheckOutPendingItemGrid',
        #'Grids\CompanyContactGrid',
        #'Grids\ContactCompanyGrid',
        'Grids\ContactNoteGrid',
        'Grids\ContactPersonalEventGrid',
        'Grids\ContainerWarehouseGrid',
        'Grids\ContractDetailGrid',
        'Grids\ContractExchangeItemGrid',
        'Grids\ContractSummaryGrid',
        #'Grids\DealNoteGrid',
        'Grids\DealShipperGrid',
        'Grids\ExchangeItemGrid',
        'Grids\InventoryAttributeValueGrid',
        'Grids\InventoryAvailabilityGrid',
        'Grids\InventoryCompatibilityGrid',
        'Grids\InventoryCompleteKitGrid',
        'Grids\InventoryConsignmentGrid',
        'Grids\InventoryQcGrid',
        'Grids\InventorySubstituteGrid',
        'Grids\InventoryVendorGrid',
        'Grids\InventoryWarehouseStagingGrid',
        'Grids\ItemAttributeValueGrid',
        'Grids\ItemQcGrid',
        'Grids\OrderContactGrid',
        #'Grids\OrderItemGrid',
        #'Grids\OrderNoteGrid',
        'Grids\OrderStatusHistoryGrid',
        'Grids\OrderStatusRentalDetailGrid',
        'Grids\OrderStatusSalesDetailGrid',
        'Grids\OrderStatusSummaryGrid',
        'Grids\PickListItemGrid',
        'Grids\POReceiveBarCodeGrid',
        'Grids\POReceiveItemGrid',
        'Grids\POReturnBarCodeGrid',
        'Grids\POReturnItemGrid',
        'Grids\QuikActivityGrid',
        #'Grids\RentalInventoryWarehouseGrid',
        'Grids\RepairReleaseGrid',
        'Grids\ReportSettingsGrid',
        'Grids\StagedItemGrid',
        'Grids\StageHoldingItemGrid',
        'Grids\StageQuantityItemGrid',
        'Grids\VendorNoteGrid'
        #'Grids\VendorTaxOption' -> this is new in TrakitWorksWeb and seems to replace CompanyTaxOptionGrid used by Vendor in RentalWorksWeb
    )
    echo 'Syncing RentalWorksWeb to TrakitWorksWeb...'
    foreach ($module in $modules) {
        $source = "$Env:DwRentalWorksWebPath\src\RentalWorksWeb\Source\$module"
        $destination = "$Env:DwRentalWorksWebPath\src\RentalWorksWebApi\TrakitWorks\Source\$module"
        robocopy "$source" "$destination" /mir /xf *.js *.js.map _SyncLog.txt | Out-Null
        if (($LastExitCode -eq 1) -or ($LastExitCode -eq 3) -or ($LastExitCode -eq 5) -or ($LastExitCode -eq 7) -or ($LastExitCode -eq 9) -or ($LastExitCode -eq 11) -or ($LastExitCode -eq 13) -or ($LastExitCode -eq 15))
        {
            $date = Get-Date
            Set-Content -Path "$destination\_SyncLog.txt" -Value "Last Synced on: $date`r`nSource Path: RentalWorksWeb\src\RentalWorksWeb\Source\$module"
            Write-Host "Updated: $module"
        }
        elseif ($LASTEXITCODE -eq 16)
        {
            Write-Host "Failed to sync: $module"
        }
    }

    echo 'Compiling TypeScript...'
    cd "$Env:DwRentalWorksWebPath\src\RentalWorksWebApi\TrakitWorks"
    npx tsc

    echo ''
    echo ''
    echo ''
    echo 'Make sure you manually update the security tree and Constants to include any new menu items that have been added.'
    echo 'Also rebuild TrakitWorksWeb'
    echo ''
    echo ''
}
catch {
    # log the error that got thrown
    Write-Error  $_

    #show a .NET messagebox with the error
    [System.Windows.Forms.MessageBox]::Show($_);
}
finally {
    # stop logging
    if ($enableLogging) {
        Stop-Transcript
    }
}