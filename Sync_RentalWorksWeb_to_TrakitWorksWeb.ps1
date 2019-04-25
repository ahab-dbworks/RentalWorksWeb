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
        
        # Modules: Administator
        'Modules\Administrator\Control',
        'Modules\Administrator\CustomForm',
        'Modules\Administrator\CustomField',
        'Modules\Administrator\DuplicateRule',
        'Modules\Administrator\EmailHistory',
        #'Modules\Administrator\Group',
        'Modules\Administrator\Hotfix',
        'Modules\Administrator\Integration',
        'Modules\Administrator\Reports', 
        'Modules\Administrator\Settings'
        #'Modules\Administrator\User'
        
        # Modules: Settings
        "Modules\Settings\Attribute",
        "Modules\Settings\ContactTitle",
        "Modules\Settings\Country",
        "Modules\Settings\Currency",
        "Modules\Settings\DealClassification",
        "Modules\Settings\DealStatus",
        "Modules\Settings\DealType",
        "Modules\Settings\Department",
        "Modules\Settings\InventoryCondition",
        "Modules\Settings\InventoryRank",
        "Modules\Settings\InventoryType",
        "Modules\Settings\OfficeLocation",
        "Modules\Settings\OrderType",
        "Modules\Settings\OrganizationType",
        "Modules\Settings\POClassification",
        "Modules\Settings\POType",
        "Modules\Settings\RepairItemStatus",
        "Modules\Settings\RentalCategory",
        "Modules\Settings\ShipVia",
        "Modules\Settings\State",
        "Modules\Settings\Unit",
        "Modules\Settings\Warehouse",

        # Reports
        'Modules\Reports\DealOutstandingItemsReport',
        'Modules\Reports\LateReturnDueBackReport',
        'Modules\Reports\QuoteReport',
        'Modules\Reports\OrderReport',
        'Modules\Reports\OutContract',
        'Modules\Reports\PickListReport',
        'Modules\Reports\RentalInventoryAttributesReport',
        'Modules\Reports\RentalInventoryCatalogReport',
        'Modules\Reports\RentalInventoryChangeReport',
        'Modules\Reports\RentalInventoryPurchaseHistoryReport',
        'Modules\Reports\RentalInventoryValueReport',
        'Modules\Reports\RetiredRentalInventoryReport',
        'Modules\Reports\UnretiredRentalInventoryReport'

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
        'Grids\VendorTaxOptionGrid',
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
        'Grids\RentalInventoryWarehouseGrid',
        'Grids\RepairReleaseGrid',
        'Grids\ReportSettingsGrid',
        'Grids\StagedItemGrid',
        'Grids\StageHoldingItemGrid',
        'Grids\StageQuantityItemGrid',
        'Grids\VendorNoteGrid'
    )
    foreach ($module in $modules) {
        $source = "$Env:DwRentalWorksWebPath\src\RentalWorksWeb\Source\" + $module
        $destination = "$Env:DwRentalWorksWebPath\src\TrakitWorksWeb\Source\" + $module
        robocopy "$source" "$destination" /mir /xf *.js *.js.map
    }
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