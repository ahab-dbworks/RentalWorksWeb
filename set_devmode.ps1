$myWindowsID=[System.Security.Principal.WindowsIdentity]::GetCurrent()
$myWindowsPrincipal=new-object System.Security.Principal.WindowsPrincipal($myWindowsID)
 
# Get the security principal for the Administrator role
$adminRole=[System.Security.Principal.WindowsBuiltInRole]::Administrator
 
# Check to see if we are currently running "as Administrator"
if ($myWindowsPrincipal.IsInRole($adminRole))
   {
   # We are running "as Administrator" - so change the title and background color to indicate this
   $Host.UI.RawUI.WindowTitle = $myInvocation.MyCommand.Definition + "(Elevated)"
   $Host.UI.RawUI.BackgroundColor = "DarkBlue"
   clear-host
   }
else
   {
   # We are not running "as Administrator" - so relaunch as administrator
   
   # Create a new process object that starts PowerShell
   $newProcess = new-object System.Diagnostics.ProcessStartInfo "PowerShell";
   
   # Specify the current script path and name as a parameter
   $newProcess.Arguments = $myInvocation.MyCommand.Definition;
   
   # Indicate that the process should be elevated
   $newProcess.Verb = "runas";
   
   # Start the new process
   [System.Diagnostics.Process]::Start($newProcess);
   
   # Exit from the current, unelevated, process
   exit
   }
 
# Run your code that needs to be elevated here

$ScriptPath = (Get-Variable MyInvocation).Value
$SolutionDir = Split-Path $ScriptPath.MyCommand.Path
New-WebVirtualDirectory -Site "Default Web Site" -Name "rwfwjson" -PhysicalPath "$SolutionDir\lib\Fw" 


$mode = "dev"
if(!(Test-Path -Path "src/RentalWorksWeb/App_Data/Temp/Downloads" )){
	New-Item "src/RentalWorksWeb/App_Data/Temp/Downloads" -type directory
}
Copy-Item "src/RentalWorksAPI/Application.$mode.config" "src/RentalWorksAPI/Application.config"
Copy-Item "src/RentalWorksQuikScan/Application.$mode.config" "src/RentalWorksQuikScan/Application.config"
Copy-Item "src/RentalWorksQuikScan/Application.$mode.config" "src/RentalWorksQuikScan/ApplicationConfig.js"
Copy-Item "src/RentalWorksTest/Application.$mode.config" "src/RentalWorksTest/Application.config"
Copy-Item "src/RentalWorksWeb/Application.$mode.config" "src/RentalWorksWeb/Application.config"
Copy-Item "src/RentalWorksWeb/ApplicationConfig.$mode.js" "src/RentalWorksWeb/ApplicationConfig.js"

lib\Fw\lib\NuGet\nuget.exe restore RentalWorksWeb.sln

lib\Fw\lib\MSBuild\MSBuild.exe /p:Configuration=Debug RentalWorksWeb.sln