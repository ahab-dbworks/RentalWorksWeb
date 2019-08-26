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

if not exist "c:\Program Files\7-Zip\7z.exe" ECHO 7-Zip is not installed
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


rem update AssemblyInfo.cs
cmd /c exit 91
set openBrack=%=exitcodeAscii%
rem echo %openBrack%
cmd /c exit 93
set closeBrack=%=exitcodeAscii%
rem echo %closeBrack%
set newassemblyline=%openBrack%AssemblyVersion("%buildno%")%closeBrack%
rem echo %newassemblyline%
set "file=%DwRentalWorksWebPath%\src\RentalWorksWeb\Properties\AssemblyInfo.cs"
for /F "delims=" %%a in (%file%) do (
    set /A count+=1
    set "array[!count!]=%%a"
)
rem for /L %%i in (1,1,%count%) do echo !array[%%i]!
del %file%
for /L %%i in (1,1,%count%) do (
   rem set "line=!array[%%i]!"
   rem echo %line%>>%file%)
   echo !array[%%i]!|find "AssemblyVersion" >nul
   if errorlevel 1 (echo !array[%%i]!>>%file%) else (call echo !newassemblyline!>>%file%))

rem command-line Git push in the modified version and assemply files
cd %DwRentalWorksWebPath%
git config --global gc.auto 0
git add "src/RentalWorksWeb/version.txt"
git add "src/RentalWorksWebApi/version.txt"
git add "src/RentalWorksWeb/version-RentalWorksWeb.txt"
git add "src/RentalWorksWeb/Properties/AssemblyInfo.cs"
git commit -m "web: %buildno%"
git push
git tag web/v%buildno%
git push origin web/v%buildno%

rem command-line gren make Build Release Document
cd %DwRentalWorksWebPath%\build
gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=v%buildno%.md -D issues -m --group-by label


rem build Web
dotnet restore %DwRentalWorksWebPath%\RentalWorksWeb.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" %DwRentalWorksWebPath%\RentalWorksWeb.sln /t:Rebuild /p:Configuration="Release Web" /p:Platform="any cpu" /p:ReferencePath=%DwRentalWorksWebPath%\packages 


rem build the API 
cd %DwRentalWorksWebPath%\src\RentalWorksWebApi
call npm run publish

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



