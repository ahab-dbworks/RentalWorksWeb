echo --------------------------------------------------------------------------
echo Downloading: RentalWorksWeb
echo --------------------------------------------------------------------------

@echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will download the requested ZIP file for RentalWorksWeb
rem --------------------------------------------------------------------------
rem Author:        Mike and Scott
rem Last modified: 03/05/2020
rem --------------------------------------------------------------------------
rem
rem


rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
set workingdirectory=C:\temp
set updatesfilename=updates.txt
set ftpcommandfilename=ftp.txt

if not exist "c:\Program Files\7-Zip\7z.exe" (ECHO 7-Zip is not installed)
if not exist "c:\Program Files\7-Zip\7z.exe" (exit /B)

rem Determine the version number to upgrade to
setlocal
set buildno=LATEST
if not "%1"=="" (set buildno=%1)
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
      if "!fileextension!"=="zip" (set zipupdatefile=!updatefile!)
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
if exist RentalWorks\ (rmdir RentalWorks /S /Q)
mkdir RentalWorks
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

rem use 7-zip to extract the contents of the ZIP file, delete the ZIP file
"c:\Program Files\7-Zip\7z.exe" x %zipfilename% -o%workingdirectory%\RentalWorks -r -y
del %zipfilename%

rem --------------------------------------------------------------------------
rem WEB
rem --------------------------------------------------------------------------
rem delete any downloaded files that should not be here
cd %workingdirectory%\RentalWorks\RentalWorksQuikScan
if exist ApplicationConfig.js (del ApplicationConfig.js)
if exist Application.config (del Application.config)

cd %workingdirectory%\RentalWorks\RentalWorksWeb
if exist App_Data\ (rmdir App_Data /S /Q)
if exist ApplicationConfig.js (del ApplicationConfig.js)

cd %workingdirectory%\RentalWorks\RentalWorksWebApi
if exist appsettings.json (del appsettings.json)

cd C:\inetpub\wwwroot





