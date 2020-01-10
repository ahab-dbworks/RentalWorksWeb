echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will download the requested ZIP file for RentalWorksWeb and deploy it to the directories hosting the Web and Api files
rem 
rem Usagw:       
rem upgradeRentalWorksWeb <version>
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 01/10/2020
rem --------------------------------------------------------------------------
rem
rem
rem --------------------------------------------------------------------------
rem Set the name of the Application Pool that the API runs in
set apppoolname=XXAppPoolNameHereXX
rem --------------------------------------------------------------------------
rem Set the actual paths here for Web and API running directories (do not supply trailing "\")
set webpath=C:\Temp\xxxweb
set apipath=C:\Temp\xxxapi
rem --------------------------------------------------------------------------



rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
set workingdirectory=C:\temp
set updatesfilename=updates.txt
set ftpcommandfilename=ftp.txt

if not exist "c:\Program Files\7-Zip\7z.exe" ECHO 7-Zip is not installed
if not exist "c:\Program Files\7-Zip\7z.exe" set /p=Hit ENTER to exit
if not exist "c:\Program Files\7-Zip\7z.exe" exit /B

if %apppoolname%==XXAppPoolNameHereXX ECHO Need to configure the name of the Application Pool
if %apppoolname%==XXAppPoolNameHereXX set /p=Hit ENTER to exit
if %apppoolname%==XXAppPoolNameHereXX exit /B

if %webpath%==C:\Temp\xxxweb ECHO Need to configure install location for Web
if %webpath%==C:\Temp\xxxweb set /p=Hit ENTER to exit
if %webpath%==C:\Temp\xxxweb exit /B

if %apipath%==C:\Temp\xxxapi ECHO Need to configure install location for API
if %apipath%==C:\Temp\xxxapi set /p=Hit ENTER to exit
if %apipath%==C:\Temp\xxxapi exit /B

if not exist %apipath%\ ECHO Cannot find install location for API -- %apipath%
if not exist %apipath%\ set /p=Hit ENTER to exit
if not exist %apipath%\ exit /B

if not exist %webpath%\ ECHO Cannot find install location for Web -- %webpath%
if not exist %webpath%\ set /p=Hit ENTER to exit
if not exist %webpath%\ exit /B

rem Determine the version number to upgrade to
setlocal
set buildno=LATEST
if not "%1"=="" set buildno=%1
set updatesfile=%workingdirectory%\%updatesfilename%
if "%buildno%"=="LATEST" (
   if exist !updatesfile! (del !updatesfile!)
   set zipupdatefile=none
   rem Create FTP command file to download the zip
   echo open ftp.dbworks.com>%ftpcommandfilename%
   echo update>>%ftpcommandfilename%
   echo update>>%ftpcommandfilename%
   echo cd rentalworksweb>>%ftpcommandfilename%
   echo cd 2019.1.2>>%ftpcommandfilename%
   echo ls * %updatesfile%>>%ftpcommandfilename%
   echo quit>>%ftpcommandfilename%
   ftp -s:%ftpcommandfilename% -v
   del %ftpcommandfilename%
   setlocal ENABLEDELAYEDEXPANSION
   for /F "delims=" %%a in (%updatesfile%) do (
	  set updatefile=%%a
	  set fileextension=!updatefile:~-3!
      if "!fileextension!"=="zip" set zipupdatefile=!updatefile!
	  )
   if exist !updatesfile! (del !updatesfile!)
   set buildno=!zipupdatefile:~15,11!
   set buildno=!buildno:_=.!
)
echo Upgrading to Version %buildno%


rem determine the name of the ZIP file to download
setlocal ENABLEDELAYEDEXPANSION
set buildnoforzip=%buildno:.=_%
set zipfilename=RentalWorksWeb_%buildnoforzip%.zip

rem delete any old files in the working directory (c:\temp)
cd %workingdirectory%
if exist RentalWorksWeb\ (rmdir RentalWorksWeb /S /Q)
if exist RentalWorksWebApi\ (rmdir RentalWorksWebApi /S /Q)
if exist RentalWorksQuikScan\ (rmdir RentalWorksQuikScan /S /Q)
if exist %ftpcommandfilename% (del %ftpcommandfilename%)
if exist %zipfilename% (del %zipfilename%)

rem Create FTP command file to download the zip
echo open ftp.dbworks.com>%ftpcommandfilename%
echo update>>%ftpcommandfilename%
echo update>>%ftpcommandfilename%
echo cd rentalworksweb>>%ftpcommandfilename%
echo cd 2019.1.2>>%ftpcommandfilename%
echo get %zipfilename%>>%ftpcommandfilename%
echo quit>>%ftpcommandfilename%

rem Run the FTP command using the command file created above, delete the command file
ftp -s:%ftpcommandfilename% -v
del %ftpcommandfilename%

if not exist %zipfilename% ECHO Could not download %zipfilename%
if not exist %zipfilename% set /p=Hit ENTER to exit
if not exist %zipfilename% exit /B

rem use 7-zip to extract the contents of the ZIP file, delete the ZIP file
"c:\Program Files\7-Zip\7z.exe" x %zipfilename%
del %zipfilename%

rem --------------------------------------------------------------------------
rem WEB
rem --------------------------------------------------------------------------
rem delete any downloaded files that should not be here
cd %workingdirectory%\RentalWorksWeb
if exist App_Data\ (rmdir App_Data /S /Q)
if exist ApplicationConfig.js (del ApplicationConfig.js)
rem delete any old installations in the target "web" directory
cd %webpath%
rem remove all directories except .well-known and App_Data
forfiles /p %webpath% /c "cmd /c if @isdir==TRUE if not @file==\".well-known\" if not @file==\"App_Data\" rd @file /S /Q"
rem delete all files except ApplicationConfig.js
forfiles /p %webpath% /c "cmd /c if not @isdir==TRUE if not @file==\"ApplicationConfig.js\" del @file"
rem copy all files from the downloaded web directoy to the target web directory
xcopy %workingdirectory%\RentalWorksWeb\*.* %webpath% /e


rem --------------------------------------------------------------------------
rem API
rem --------------------------------------------------------------------------
rem delete any downloaded files that should not be here
cd %workingdirectory%\RentalWorksWebApi 
if exist appsettings.json (del appsettings.json)

rem recycle the Application Pool (before updating API)
%systemroot%\system32\inetsrv\appcmd recycle apppool /apppool.name:"%apppoolname%"

rem delete all except directories that start with "." and appsettings.json
cd %apipath%
rem remove all directories except .well-known and .local-chromium
forfiles /p %apipath% /c "cmd /c if @isdir==TRUE if not @file==\".well-known\" if not @file==\".local-chromium\" rd @file /S /Q"
rem delete all files except appsettings.json
forfiles /p %apipath% /c "cmd /c if not @isdir==TRUE if not @file==\"appsettings.json\" del @file"
rem copy all files from the downloaded api directoy to the target api directory
xcopy %workingdirectory%\RentalWorksWebApi\*.* %apipath% /e

cd %workingdirectory%
if exist RentalWorksWeb\ (rmdir RentalWorksWeb /S /Q)
if exist RentalWorksWebApi\ (rmdir RentalWorksWebApi /S /Q)


rem grant "modify" permissions for the IIS User to the temp\downloads directory for users to be able to make Excel files
icacls %apipath%\wwwroot\temp\downloads /grant IIS_IUSRS:M

rem recycle the Application Pool (after updating API)
%systemroot%\system32\inetsrv\appcmd recycle apppool /apppool.name:"%apppoolname%"