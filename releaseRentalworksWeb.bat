echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will produce a new build of RentalWorksWeb or TrackitWorksWeb in the "build" directory.
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman, Mike Vermilion
rem Last modified: 12/13/2019
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
set /p productname="Which system would you like to build: 1. RentalWorks, 2. TrakitWorks? (default:1): "
IF "%productname%"=="1" (
	set productname=RentalWorks
) ELSE IF "%productname%"=="2" (
	set productname=TrakitWorks
) ELSE (
	set productname=RentalWorks
)
echo Building %productname%Web


if "%productname%"=="RentalWorks" (
    set tagprefix=web
) else if "%productname%"=="TrakitWorks" (
    set tagprefix=tw
)
rem Get the Web Build number from the user
FOR /F "tokens=*" %%i IN (%DwRentalWorksWebPath%\src\RentalWorksWebApi\version-%productname%Web.txt) DO @set previousversionno=%%i
echo Previous %productname%Web Version: %previousversionno%
set /p fullversionno=".....New %productname%Web Version: "
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
  IF "%fullversionno%"=="%previousversionno%" (
    echo New Version matches previous version, please enter previous version no 
    set /p previousversionno="Previous %productname%Web Version: " 
  )
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
set zipfilename=%productname%Web_%buildnoforzip%.zip

rem Update the version.txt files
echo | set /p buildnumber="%fullversionno%">%DwRentalWorksWebPath%\src\%productname%Web\version.txt
echo | set /p buildnumber="%fullversionno%">%DwRentalWorksWebPath%\src\RentalWorksWebApi\QuikScan\version.txt
echo | set /p buildnumber="%fullversionno%">%DwRentalWorksWebPath%\src\RentalWorksWebApi\version.txt
echo | set /p buildnumber="%fullversionno%">%DwRentalWorksWebPath%\src\RentalWorksWebApi\version-%productname%Web.txt


rem update AssemblyInfo.cs
cmd /c exit 91
set openBrack=%=exitcodeAscii%
rem echo %openBrack%
cmd /c exit 93
set closeBrack=%=exitcodeAscii%
rem echo %closeBrack%

rem Update RentalWorksWeb AssemblyInfo.cs
set newassemblyline=%openBrack%assembly: AssemblyVersion("%fullversionno%")%closeBrack%
set file=%DwRentalWorksWebPath%\src\%productname%Web\Properties\AssemblyInfo.cs
set count=0
for /F "delims=" %%a in (%file%) do (
    set /A count+=1
    set "array[!count!]=%%a"
)
del %file%
for /L %%i in (1,1,%count%) do (
   echo !array[%%i]!|find "AssemblyVersion" >nul
   if errorlevel 1 (echo !array[%%i]!>>%file%) else (call echo !newassemblyline!>>%file%)
)

rem We need to commit the version files and Tag the repo here because other commits may come in while we Build
IF "%commitandftp%"=="y" (
    rem rem command-line Git push in the modified version and assemply files
    cd %DwRentalWorksWebPath%

    git config --global gc.auto 0
    git add "src/%productname%Web/version.txt"
    git add "src/%productname%Web/Properties/AssemblyInfo.cs"
    git add "src/RentalWorksWebApi/QuikScan/version.txt"
    git add "src/RentalWorksWebApi/version.txt"
    git add "src/RentalWorksWebApi/version-%productname%Web.txt"
    git commit -m "%tagprefix%: %fullversionno%"
    git push
    git tag %tagprefix%/v%fullversionno%
    git push origin %tagprefix%/v%fullversionno%

    rem copy the document header image to the build directory 
    cd %DwRentalWorksWebPath%
    if not exist build\ mkdir build
    copy releasedocumentlogo.png build /y

    call npm i

    rem command-line gren make Build Release Document for all issues between the previous version's tag and this current tag
    cd %DwRentalWorksWebPath%\build
    rem call gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=v%fullversionno%.md -t %tagprefix%/v%fullversionno% -c ..\config.grenrc
    call npx gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=v%fullversionno%.md -t %tagprefix%/v%fullversionno%..%tagprefix%/v%previousversionno% -c ..\config.grenrc

    rem produce a PDF of the MD file
    cd %DwRentalWorksWebPath%
    call npx md-to-pdf build\v%fullversionno%.md
    set pdffilename=v%fullversionno%.pdf
    start build\v%fullversionno%.pdf

    rem Need to use curl to publish the PDF file to ZenDesk as a new "article"
    rem curl https://dbworks.zendesk.com/api/v2/help_center/sections/{id}/articles.json \
    rem   -d '{"article": {"title": "RentalWorksWeb v2019.1.1.XX", "body": "RentalWorksWeb v2019.1.1.XX has been released", "locale": "en-us" }, "notify_subscribers": false}' \
    rem   -v -u {email_address}:{password} -X POST -H "Content-Type: application/json"
    rem Note: "section" will be something like RentalWorksWeb > Release Documents
    rem Note: need to research how to attach documents
)

rem delete any old build files
cd %DwRentalWorksWebPath%\build
if exist %productname%Web\ (rmdir %productname%Web /S /Q)
if exist %productname%WebApi\ (rmdir %productname%WebApi /S /Q)
if exist %zipfilename% (del %zipfilename%)

rem build the API 
cd %DwRentalWorksWebPath%\src\RentalWorksWebApi
call npm i
call npm run publish

if "%productname%"=="RentalWorks" (
    set webpath=%DwRentalWorksWebPath%\build\RentalWorksWeb\
    set webapipath=%DwRentalWorksWebPath%\build\RentalWorksWebApi\
    set jestsrcpath=%DwRentalWorksWebPath%\src\JestTest\src\
) else if "%productname%"=="TrakitWorks" (
    move %DwRentalWorksWebPath%\build\RentalWorksWebApi %DwRentalWorksWebPath%\build\TrakitWorksWebApi
    set webpath=%DwRentalWorksWebPath%\build\TrakitWorksWeb\
    set webapipath=%DwRentalWorksWebPath%\build\TrakitWorksWebApi\
    set jestsrcpath=%DwRentalWorksWebPath%\src\JestTest\src\
)
del "%webapipath%version-RentalWorksWeb.txt"
del "%webapipath%version-TrakitWorksWeb.txt"

rem make the ZIP deliverable
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %webpath%
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %webapipath%
"c:\Program Files\7-Zip\7z.exe" a %DwRentalWorksWebPath%\build\%zipfilename% %jestsrcpath%ts\
cd %DwRentalWorksWebPath%\build

rem delete the work files
if exist %productname%Web\ (rmdir %productname%Web /S /Q)
if exist %productname%WebApi\ (rmdir %productname%WebApi /S /Q)

rem copy the ZIP delivable to "history" sub-directory
if not exist history\ mkdir history
copy %zipfilename% history

set ftpcommandfilename=ftp.txt
IF "%commitandftp%"=="y" (
    rem Create FTP command file to upload the zip
    setlocal DISABLEDELAYEDEXPANSION
    cd %DwRentalWorksWebPath%\build
    echo open ftp.dbworks.com>%ftpcommandfilename%
    echo %DwFtpUploadUser%>>%ftpcommandfilename%
    echo %DwFtpUploadPassword%>>%ftpcommandfilename%
    echo cd Update>>%ftpcommandfilename%
    echo cd %productname%Web>>%ftpcommandfilename%
    echo cd %shortversionno%>>%ftpcommandfilename%
    echo cd preview>>%ftpcommandfilename%
    echo put %pdffilename%>>%ftpcommandfilename%
    echo put %zipfilename%>>%ftpcommandfilename%
    echo quit>>%ftpcommandfilename%
    rem Run the FTP command using the command file created above, delete the command file
    ftp -s:%ftpcommandfilename% -v
    del %ftpcommandfilename%
)
cd %DwRentalWorksWebPath%
