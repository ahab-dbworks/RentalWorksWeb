if "%1"=="" (
  ECHO Missing Client name as parameter
  xit /B
)
if "%2"=="" (
  ECHO Missing Web site name as parameter
  exit /B
)
echo --------------------------------------------------------------------------
echo Upgrading: %1 
echo --------------------------------------------------------------------------
@echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will download the requested ZIP file for RentalWorksWeb and deploy it to the directories hosting the Web and Api files
rem --------------------------------------------------------------------------
rem Author:        Mike and Scott
rem Last modified: 03/05/2020
rem --------------------------------------------------------------------------
rem Set the actual paths here for Web and API running directories (do not supply trailing "\")
set workingdirectory=C:\temp
set webpath=C:\inetpub\wwwroot\%1\%2
set apipath=C:\inetpub\wwwroot\%1\%2api

rem if %webpath%==C:\Temp\RentalWorks\xxxweb (
rem   ECHO Need to configure install location for Web)
rem   exit /B
rem )
rem 
rem if %apipath%==C:\Temp\RentalWorks\xxxapi (
rem   ECHO Need to configure install location for API
rem   exit /B
rem )

if not exist %apipath%\ (
  ECHO Cannot find install location for API -- %apipath%
  exit /B
)

if not exist %webpath%\ (
  ECHO Cannot find install location for Web -- %webpath%
  exit /B 
)

rem --------------------------------------------------------------------------
rem WEB
rem --------------------------------------------------------------------------
rem stop the Application Pool before updating Rentalworks Web, restart it again after applying the new Rentalworks Web files
%systemroot%\system32\inetsrv\appcmd stop apppool /apppool.name:"%1"
cd %webpath%
rem remove all directories except .well-known and App_Data
forfiles /p %webpath% /c "cmd /c if @isdir==TRUE if not @file==\".well-known\" if not @file==\"App_Data\" rd @file /S /Q"
rem delete all files except ApplicationConfig.js
forfiles /p %webpath% /c "cmd /c if not @isdir==TRUE if not @file==\"ApplicationConfig.js\" del @file"
rem copy all files from the downloaded web directoy to the target web directory
echo xcopy: %workingdirectory%\RentalWorks\RentalWorksWeb\*.* %webpath%
xcopy %workingdirectory%\RentalWorks\RentalWorksWeb\*.* %webpath% /e

rem --------------------------------------------------------------------------
rem API
rem --------------------------------------------------------------------------
rem stop the Application Pool before updating API, restart it again after applying the new API files
%systemroot%\system32\inetsrv\appcmd stop apppool /apppool.name:"%1api"
rem delete all except directories that start with "." and appsettings.json
cd %apipath%
rem remove all directories except .well-known and .local-chromium
forfiles /p %apipath% /c "cmd /c if @isdir==TRUE if not @file==\".well-known\" if not @file==\".local-chromium\" rd @file /S /Q"
rem delete all files except appsettings.json
forfiles /p %apipath% /c "cmd /c if not @isdir==TRUE if not @file==\"appsettings.json\" del @file"
rem copy all files from the downloaded api directoy to the target api directory
xcopy %workingdirectory%\RentalWorks\RentalWorksWebApi\*.* %apipath% /e

rem cd %workingdirectory%
rem if exist %workingdirectory%\RentalWorks\ (
rem   echo Deleting 
rem   rmdir RentalWorksWeb /S /Q
rem )

rem grant "modify" permissions for the IIS User to the temp\downloads directory for users to be able to make Excel files
if exist %apipath%\wwwroot\temp\downloads (  
  echo Granting permission on: %apipath%\wwwroot\temp\downloads to IIS_IUSRS 
  icacls %apipath%\wwwroot\temp\downloads /grant IIS_IUSRS:M
)

rem start the Application Pool before updating Rentalworks Web, restart it again after applying the new Rentalworks Web files
%systemroot%\system32\inetsrv\appcmd start apppool /apppool.name:"%1"

rem start the Application Pool before updating Rentalworks API, restart it again after applying the new Rentalworks API files
%systemroot%\system32\inetsrv\appcmd start apppool /apppool.name:"%1api"

cd C:\inetpub\wwwroot
