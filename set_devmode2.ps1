$SolutionDir = (Resolve-Path .\).Path

Import-module webadministration
if (-Not(Test-Path 'IIS:\Sites\Default Web Site\rwfwjson')) {
	New-WebVirtualDirectory -Site "Default Web Site" -Name "rwfwjson" -PhysicalPath "$SolutionDir\lib\Fw\src\Fw.Json\content" -Force
}

$mode = "dev"
if(!(Test-Path -Path "$SolutionDir\src\RentalWorksWeb\App_Data\Temp\Downloads" )){
	New-Item "src/RentalWorksWeb/App_Data/Temp/Downloads" -type directory
}
Copy-Item "src\RentalWorksAPI\Application.$mode.config" "src\RentalWorksAPI\Application.config"
Copy-Item "src\RentalWorksQuikScan\Application.$mode.config" "src\RentalWorksQuikScan\Application.config"
Copy-Item "src\RentalWorksQuikScan\ApplicationConfig.$mode.js" "src\RentalWorksQuikScan\ApplicationConfig.js"
Copy-Item "src\RentalWorksTest\Application.$mode.config" "src\RentalWorksTest\Application.config"
Copy-Item "src\RentalWorksWeb\Application.$mode.config" "src\RentalWorksWeb\Application.config"
Copy-Item "src\RentalWorksWeb\ApplicationConfig.$mode.js" "src\RentalWorksWeb\ApplicationConfig.js"

lib\Fw\lib\NuGet\nuget.exe restore RentalWorksWeb.sln
