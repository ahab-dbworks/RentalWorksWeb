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

    # sync the Settings modules from RentalWorksWeb
    $settingsmodules = @( 
        "Attribute",
        #"AttributeValue",
        "BillingCycle",
        "ContactTitle",
        "Country",
        "Currency",
        "DealClassification",
        "DealStatus",
        "DealType",
        "Department",
        #"FacilityType",
        "InventoryCondition",
        "InventoryRank",
        "InventoryType",
        #"LaborType",
        #"MiscType",
        "OfficeLocation",
        "OrderType",
        "OrganizationType",
        #"PaymentTerms",
        "POClassification",
        "POType",
        "RepairItemStatus",
        "RentalCategory",
        "ShipVia",
        "State",
        #"TaxOption",
        "Unit",
        "Warehouse"
    )
    foreach ($settingsmodule in $settingsmodules) {
        $source = "$Env:DwRentalWorksWebPath\src\RentalWorksWeb\Source\Modules\Settings\" + $settingsmodule
        $destination = "$Env:DwRentalWorksWebPath\src\TrakitWorksWeb\Source\Modules\Settings\" + $settingsmodule
        robocopy "$source" "$destination" /mir
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