$ScriptPath = (Get-Variable MyInvocation).Value
$SolutionDir = Split-Path $ScriptPath.MyCommand.Path

Import-module webadministration
if (-Not(Test-Path 'IIS:\Sites\Default Web Site\rwfwjson')) {
	New-WebVirtualDirectory -Site "Default Web Site" -Name "rwfwjson" -PhysicalPath "$SolutionDir\lib\Fw" -Force
}

$mode = "dev"
if(!(Test-Path -Path "$SolutionDir\src\RentalWorksWeb\App_Data\Temp\Downloads" )){
	New-Item "src/RentalWorksWeb/App_Data/Temp/Downloads" -type directory
}
Copy-Item "src\RentalWorksAPI\Application.$mode.config" "src\RentalWorksAPI\Application.config"
Copy-Item "src\RentalWorksQuikScan\Application.$mode.config" "src\RentalWorksQuikScan\Application.config"
Copy-Item "src\RentalWorksQuikScan\Application.$mode.config" "src\RentalWorksQuikScan\ApplicationConfig.js"
Copy-Item "src\RentalWorksTest\Application.$mode.config" "src\RentalWorksTest\Application.config"
Copy-Item "src\RentalWorksWeb\Application.$mode.config" "src\RentalWorksWeb\Application.config"
Copy-Item "src\RentalWorksWeb\ApplicationConfig.$mode.js" "src\RentalWorksWeb\ApplicationConfig.js"

lib\Fw\lib\NuGet\nuget.exe restore RentalWorksWeb.sln

lib\Fw\lib\MSBuild\MSBuild.exe /p:Configuration=Debug RentalWorksWeb.sln

#setx path "%path%;C:\gitprojects\RentalWorksWeb\packages\Selenium.WebDriver.ChromeDriver.2.29.0"
#packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe C:\gitprojects\RentalWorksWeb\src\RentalWorksTest\RentalWorksTest.csproj