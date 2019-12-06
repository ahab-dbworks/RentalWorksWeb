echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will produce a new build of RentalWorksWeb or TrackitWorksWeb in the "build" directory.
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman, Mike Vermilion
rem Last modified: 11/26/2019
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


rem Exit if DwRentalWorksWebPath environment variable is not set
IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B

rem Exit if 7-zip is not installed
if not exist "c:\Program Files\7-Zip\7z.exe" ECHO 7-Zip is not installed
if not exist "c:\Program Files\7-Zip\7z.exe" set /p=Hit ENTER to exit
if not exist "c:\Program Files\7-Zip\7z.exe" exit /B

rem Prompt the user which system to build
set /p productname="Which system would you like to build: 1. RentalWorksWeb, 2. TrakitWorks? (default:1): "
IF "%productname%"=="" (
	set productname=RentalWorksWeb
) ELSE IF "%productname%"=="1" (
	set productname=RentalWorksWeb
) ELSE IF "%productname%"=="2" (
	set productname=TrakitWorksWeb
) ELSE (
	exit /B
)

rem Get the Build number from the user
FOR /F "tokens=*" %%i IN (%DwRentalWorksWebPath%\src\%productname%\version.txt) DO @set previousversionno=%%i
set /p fullversionno="Previous Version: %previousversionno%, New Version: "
for /f "tokens=1-3 delims=." %%i in ("%fullversionno%") do (
  set shortversionno=%%i.%%j.%%k
)
for /f "tokens=4 delims=." %%i in ("%fullversionno%") do (
  set buildno=%%i
)

rem Prompt the user if they want to commit and deploy to ftp
set /p commitandftp="Do you want to commit and FTP the build? (y/n default:n): "
IF NOT "%commitandftp%"=="y" set commitandftp=n

rem if posting to FTP, make sure the user/pass environment variables are set
IF "%commitandftp%"=="y" (
IF "%DwFtpUploadUser%"=="" ECHO Environment Variable DwFtpUploadUser is NOT defined
IF "%DwFtpUploadUser%"=="" set /p=Hit ENTER to exit
IF "%DwFtpUploadUser%"=="" exit /B

IF "%DwFtpUploadPassword%"=="" ECHO Environment Variable DwFtpUploadPassword is NOT defined
IF "%DwFtpUploadPassword%"=="" set /p=Hit ENTER to exit
IF "%DwFtpUploadPassword%"=="" exit /B
)

rem determine ZIP filename
setlocal ENABLEDELAYEDEXPANSION
set buildnoforzip=%fullversionno:.=_%
set zipfilename=%productname%_%buildnoforzip%.zip

rem Update the Build number in the version.txt files
echo | set /p buildnumber=>%DwRentalWorksWebPath%\src\%productname%\version.txt
echo | set /p buildnumber="%fullversionno%">>%DwRentalWorksWebPath%\src\%productname%\version.txt
echo | set /p buildnumber="%fullversionno%"> %DwRentalWorksWebPath%\src\RentalWorksWebApi\version.txt
echo | set /p buildnumber="%fullversionno%"> %DwRentalWorksWebPath%\src\RentalWorksWebApi\version-%productname%.txt

rem update AssemblyInfo.cs
cmd /c exit 91
set openBrack=%=exitcodeAscii%
rem echo %openBrack%
cmd /c exit 93
set closeBrack=%=exitcodeAscii%
rem echo %closeBrack%
set newassemblyline=%openBrack%assembly: AssemblyVersion("%fullversionno%")%closeBrack%
rem echo %newassemblyline%
set "file=%DwRentalWorksWebPath%\src\%productname%\Properties\AssemblyInfo.cs"
for /F "delims=" %%a in (%file%) do (
    set /A count+=1
    set "array[!count!]=%%a"
)
del %file%
for /L %%i in (1,1,%count%) do (
   rem set "line=!array[%%i]!"
   rem echo %line%>>%file%)
   echo !array[%%i]!|find "AssemblyVersion" >nul
   if errorlevel 1 (echo !array[%%i]!>>%file%) else (call echo !newassemblyline!>>%file%))

rem We need to commit the version files and Tag the repo here because other commits may come in while we Build
IF "%commitandftp%"=="y" (
    rem rem command-line Git push in the modified version and assemply files
    cd %DwRentalWorksWebPath%
    git config --global gc.auto 0
    git add "src/%productname%/version.txt"
    git add "src/%productname%/Properties/AssemblyInfo.cs"
    git add "src/RentalWorksWebApi/version.txt"
    git add "src/RentalWorksWebApi/version-%productname%.txt"
    git commit -m "web: %fullversionno%"
    git push
    git tag web/v%fullversionno%
    git push origin web/v%fullversionno%

    rem command-line gren make Build Release Document
    cd %DwRentalWorksWebPath%
    call gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=build/v%fullversionno%.md -t web/v%fullversionno% -c config.grenrc
    start build/v%fullversionno%.md

    rem produce a PDF of the MD file
    cd %DwRentalWorksWebPath%
    call md-to-pdf build\v%fullversionno%.md

    rem Need to use curl to publish the PDF file to ZenDesk as a new "article"
    rem curl https://dbworks.zendesk.com/api/v2/help_center/sections/{id}/articles.json \
    rem   -d '{"article": {"title": "RentalWorksWeb v2019.1.1.XX", "body": "RentalWorksWeb v2019.1.1.XX has been released", "locale": "en-us" }, "notify_subscribers": false}' \
    rem   -v -u {email_address}:{password} -X POST -H "Content-Type: application/json"
    rem Note: "section" will be something like RentalWorksWeb > Release Documents
    rem Note: need to research how to attach documents
)

rem delete any old build files
cd %DwRentalWorksWebPath%\build
if exist %productname%\ (rmdir %productname% /S /Q)
if exist %productname%Api\ (rmdir %productname%Api /S /Q)
if exist %zipfilename% (del %zipfilename%)

rem build Web
dotnet restore %DwRentalWorksWebPath%\RentalWorksWeb.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" %DwRentalWorksWebPath%\RentalWorksWeb.sln /t:Rebuild /p:Configuration="Release %productname%" /p:Platform="any cpu" /p:ReferencePath=%DwRentalWorksWebPath%\packages 

rem build the API 
cd %DwRentalWorksWebPath%\src\RentalWorksWebApi
call npm i
call npm run publish
if "%productname%"=="RentalWorksWeb" (
	set webapipath=%DwRentalWorksWebPath%\build\RentalWorksWebApi\
) else if "%productname%"=="TrakitWorksWeb" (
    MOVE %DwRentalWorksWebPath%\build\RentalWorksWebApi %DwRentalWorksWebPath%\build\TrakitWorksWebApi
	set webapipath=%DwRentalWorksWebPath%\build\TrakitWorksWebApi\
)

rem make the ZIP deliverable
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %DwRentalWorksWebPath%\build\%productname%\
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %webapipath%
cd %DwRentalWorksWebPath%\build

rem delete the work files
if exist %productname%\ (rmdir %productname% /S /Q)
if exist %productname%Api\ (rmdir %productname%Api /S /Q)

rem copy the ZIP delivable to "history" sub-directory
copy %zipfilename% history

IF "%commitandftp%"=="y" (
    rem Create FTP command file to upload the zip
    setlocal DISABLEDELAYEDEXPANSION
    cd %DwRentalWorksWebPath%\build
    set ftpcommandfilename=ftp.txt
    echo open ftp.dbworks.com>%ftpcommandfilename%
    echo %DwFtpUploadUser%>>%ftpcommandfilename%
    echo %DwFtpUploadPassword%>>%ftpcommandfilename%
    echo cd update>>%ftpcommandfilename%
    echo cd %productname%>>%ftpcommandfilename%
    echo cd %shortversionno%>>%ftpcommandfilename%
    echo put %zipfilename%>>%ftpcommandfilename%
    echo put v%fullversionno%.pdf>>%ftpcommandfilename%
    echo quit>>%ftpcommandfilename%

    rem Run the FTP command using the command file created above, delete the command file
    ftp -s:%ftpcommandfilename% -v
    del %ftpcommandfilename%
)
rem pause
