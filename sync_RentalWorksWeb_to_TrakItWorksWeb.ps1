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
    If (-not(Test-Path Env:DwRentalWorksWebPath)) { throw "Please set System Environment Variable 'DwRentalWorksWebPath' to the repostory path of RentalWorksWeb for example: C:\gitprojects\GaterWorksWeb" }
    If (-not(Test-Path "$Env:DwRentalWorksWebPath" -PathType Container)) { throw "The System Environment Variable 'DwRentalWorksWebPath' is set to a directory that does not exist: $Env:DwRentalWorksWebPath" }

    # sync RentalWorksWeb to TrakItWorksWeb
    robocopy "$Env:DwRentalWorksWebPath\src\RentalWorksWeb\Source" "$Env:DwRentalWorksWebPath\src\TrakItWorksWeb\Libraries\RentalWorksWeb" /mir
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
    