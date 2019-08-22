echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will produce a new build of RentalWorksWeb in the "build" directory.
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 08/13/2019
rem --------------------------------------------------------------------------
rem
rem
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------




rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------


IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" exit /B


IF "%DwFtpUploadUser%"=="" ECHO Environment Variable DwFtpUploadUser is NOT defined
IF "%DwFtpUploadUser%"=="" exit /B

IF "%DwFtpUploadPassword%"=="" ECHO Environment Variable DwFtpUploadPassword is NOT defined
IF "%DwFtpUploadPassword%"=="" exit /B

if not exist "c:\Program Files\7-Zip\7z.exe" ECHO 7 Zip is not installed
if not exist "c:\Program Files\7-Zip\7z.exe" exit /B


rem Get the Build number from the user
set /p buildno="Build Number (ie. 2019.1.1.15): "

rem determine ZIP filename
setlocal ENABLEDELAYEDEXPANSION
set buildnoforzip=%buildno:.=_%
set zipfilename=RentalWorksWeb_%buildnoforzip%.zip
rem echo %zipfilename%

rem delete any old build files
cd %DwRentalWorksWebPath%\build
if exist RentalWorksWeb\ (rmdir RentalWorksWeb /S /Q)
if exist RentalWorksWebApi\ (rmdir RentalWorksWebApi /S /Q)
if exist %zipfilename% (del %zipfilename%)


rem Update the Build number in the version.txt files
echo | set /p buildnumber=%buildno%> %DwRentalWorksWebPath%\src\RentalWorksWeb\version.txt
echo | set /p buildnumber=%buildno%> %DwRentalWorksWebPath%\src\RentalWorksWebApi\version.txt
echo | set /p buildnumber=%buildno%> %DwRentalWorksWebPath%\src\RentalWorksWeb\version-RentalWorksWeb.txt


rem build Web
dotnet restore %DwRentalWorksWebPath%\RentalWorksWeb.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" %DwRentalWorksWebPath%\RentalWorksWeb.sln /t:Rebuild /p:Configuration="Release Web" /p:Platform="any cpu" /p:ReferencePath=%DwRentalWorksWebPath%\packages 


rem build the API 
cd %DwRentalWorksWebPath%\src\RentalWorksWebApi
call npm run publish


rem RUN REGRESSION TEST SCRIPTS HERE


rem make the ZIP deliverable
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %DwRentalWorksWebPath%\build\RentalWorksWeb\
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %DwRentalWorksWebPath%\build\RentalWorksWebApi\
cd %DwRentalWorksWebPath%\build


rem delete the work files
if exist RentalWorksWeb\ (rmdir RentalWorksWeb /S /Q)
if exist RentalWorksWebApi\ (rmdir RentalWorksWebApi /S /Q)

rem copy the ZIP delivable to "history" sub-directory
copy %zipfilename% history

rem Create FTP command file to upload the zip
setlocal DISABLEDELAYEDEXPANSION
cd %DwRentalWorksWebPath%\build
set ftpcommandfilename=ftp.txt
echo open ftp.dbworks.com>%ftpcommandfilename%
echo %DwFtpUploadUser%>>%ftpcommandfilename%
echo %DwFtpUploadPassword%>>%ftpcommandfilename%
echo cd update>>%ftpcommandfilename%
echo cd rentalworksweb>>%ftpcommandfilename%
echo cd 2019.1.1>>%ftpcommandfilename%
echo put %zipfilename%>>%ftpcommandfilename%
echo quit>>%ftpcommandfilename%

rem Run the FTP command using the command file created above, delete the command file
ftp -s:%ftpcommandfilename% -v
del %ftpcommandfilename%




rem command-line Git push in the modified version and assemply files
rem ??


rem command-line Git make Release and Tag
rem ??


rem command-line gren make Build Release Document
rem cd %DwRentalWorksWebPath%\build
rem gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=v2019.1.1.xx.md -D issues -m --group-by label



