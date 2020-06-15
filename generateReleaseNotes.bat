echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will generate only the release notes
rem --------------------------------------------------------------------------
rem Author:        Mike Vermilion
rem Last modified: 6/12/2020
rem --------------------------------------------------------------------------

rem Exit if DwRentalWorksWebPath environment variable is not set
IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B

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

IF "%fullversionno%"=="%previousversionno%" (
  echo New Version matches previous version, please enter previous version no 
  set /p previousversionno="Previous %productname%Web Version: " 
)

rem Generate Release Notes Markdown file
cd %DwRentalWorksWebPath%\build
call npx gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=v%fullversionno%.md -t %tagprefix%/v%fullversionno%..%tagprefix%/v%previousversionno% -c ..\config.grenrc

rem Convert Release Notes Markdown file to PDF
cd %DwRentalWorksWebPath%
call npx md-to-pdf build\v%fullversionno%.md
set pdffilename=v%fullversionno%.pdf
start build\v%fullversionno%.pdf
