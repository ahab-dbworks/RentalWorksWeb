echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will download the requested ZIP file for RentalWorksWeb and deploy it to the directories hosting the Web and Api files
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 08/13/2019
rem --------------------------------------------------------------------------
rem
rem
rem --------------------------------------------------------------------------
rem Set the name of the Application Pool that the API runs in
set apppoolname=XXAppPoolNameHereXX
rem --------------------------------------------------------------------------
rem Set the paths for Web and API files (do not supply trailing "\")
set webpath=C:\Temp\web            
set apipath=C:\Temp\api
rem --------------------------------------------------------------------------



rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
set workingdirectory=C:\temp
set ftpcommandfilename=ftp.txt
rem prompt for the build number to download and install
set /p buildno="Build Number (ie. 2019.1.1.15): "


rem determine the name of the ZIP file to download
setlocal ENABLEDELAYEDEXPANSION
set buildnoforzip=%buildno:.=_%
set zipfilename=RentalWorksWeb_%buildnoforzip%.zip

rem delete any old files in the working directory (c:\temp)
cd %workingdirectory%
rmdir RentalWorksWeb /S /Q
rmdir RentalWorksWebApi /S /Q
del %ftpcommandfilename%
del %zipfilename%

rem Create FTP command file to download the zip
echo open ftp.dbworks.com>%ftpcommandfilename%
echo update>>%ftpcommandfilename%
echo update>>%ftpcommandfilename%
echo cd rentalworksweb>>%ftpcommandfilename%
echo cd 2019.1.1>>%ftpcommandfilename%
echo get %zipfilename%>>%ftpcommandfilename%
echo quit>>%ftpcommandfilename%

rem Run the FTP command using the command file created above, delete the command file
ftp -s:%ftpcommandfilename% -v
del %ftpcommandfilename%

rem use 7-zip to extract the contents of the ZIP file, delete the ZIP file
"c:\Program Files\7-Zip\7z.exe" x %zipfilename%
del %zipfilename%

rem --------------------------------------------------------------------------
rem WEB
rem --------------------------------------------------------------------------
rem delete any downloaded files that should not be here
cd %workingdirectory%\RentalWorksWeb
rmdir App_Data /S /Q
del ApplicationConfig.js
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
del appsettings.json
rem delete all except directories that start with "." and appsettings.json
cd %apipath%
rem remove all directories except .well-known and .local-chromium
forfiles /p %apipath% /c "cmd /c if @isdir==TRUE if not @file==\".well-known\" if not @file==\".local-chromium\" rd @file /S /Q"
rem delete all files except appsettings.json
forfiles /p %apipath% /c "cmd /c if not @isdir==TRUE if not @file==\"appsettings.json\" del @file"
rem copy all files from the downloaded api directoy to the target api directory
xcopy %workingdirectory%\RentalWorksWebApi\*.* %apipath% /e

cd %workingdirectory%
rmdir RentalWorksWeb /S /Q
rmdir RentalWorksWebApi /S /Q


rem grant "modify" permissions for the IIS User to the temp\downloads directory for users to be able to make Excel files
icacls %apipath%\wwwroot\temp\downloads /grant IIS_IUSRS:M

rem recycle the Application Pool
%systemroot%\system32\inetsrv\appcmd recycle apppool /apppool.name:"%apppoolname%"