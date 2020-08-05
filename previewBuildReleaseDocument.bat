echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will produce a Preview Build Release Document in the "build" directory.
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman, Mike Vermilion
rem Last modified: 03/10/2020
rem --------------------------------------------------------------------------

rem Exit if DwRentalWorksWebPath environment variable is not set
IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B


rem Prompt the user which system to build
set /p productname="Which system document would you like to preview?: 1. RentalWorks, 2. TrakitWorks? (default:1): "
IF "%productname%"=="1" (
	set productname=RentalWorks
) ELSE IF "%productname%"=="2" (
	set productname=TrakitWorks
) ELSE (
	set productname=RentalWorks
)

if "%productname%"=="RentalWorks" (
    set tagprefix=web
) else if "%productname%"=="TrakitWorks" (
    set tagprefix=tw
)

rem Get the Web Build number from the user
FOR /F "tokens=*" %%i IN (%DwRentalWorksWebPath%\src\RentalWorksWebApi\version-%productname%Web.txt) DO @set previousversionno=%%i

rem command-line Git push in the temporary vNext tag
cd %DwRentalWorksWebPath%
git tag %tagprefix%/vNext
git push origin %tagprefix%/vNext

rem copy the document header image to the build directory 
cd %DwRentalWorksWebPath%
if not exist build\ mkdir build
copy releasedocumentlogo.png build /y

rem command-line gren make Build Release Document for all issues between the previous version's tag and this current tag
cd %DwRentalWorksWebPath%\build
if exist vNext.md (del vNext.md)
if exist vNext.pdf (del vNext.pdf)
call gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=vNext.md -t %tagprefix%/vNext..%tagprefix%/v%previousversionno% -c ..\config.grenrc

rem produce a PDF of the MD file
cd %DwRentalWorksWebPath%
call md-to-pdf build\vNext.md
start build\vNext.pdf

cd %DwRentalWorksWebPath%

rem delete the local tag
git tag -d %tagprefix%/vNext

rem delete the remote tag
git push --delete origin %tagprefix%/vNext
