echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will run concurrent automated tests in parallel on RentalWorksWeb
rem Indicate the start and stop number of tests to run as paramters to this batch file execution
rem Example: C:\Regression\src\JestTest\runRegressionTests.bat 1 15      -- this will run tests #1 through #15 in parallel
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 03/04/2020
rem --------------------------------------------------------------------------
rem
rem
rem do not supply a trailing backslash.  do not surround path in double-quotes
set webinstallpath=C:\inetpub\wwwroot\rentalworksweb
rem
rem
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
if not "%~1"=="" set starttest=%~1
if not "%~2"=="" set endtest=%~2

IF "%DwRentalWorksWebPath%"=="" (
   ECHO Environment Variable DwRentalWorksWebPath is NOT defined
   set /p=Hit ENTER to exit
   exit /B
)

if not exist "c:\Program Files\7-Zip\7z.exe" (
   ECHO 7-Zip is not installed
   set /p=Hit ENTER to exit
   exit /B
)

IF "%starttest%"=="" (
   ECHO Supply a starting test number as a command-line parameter
   set /p=Hit ENTER to exit
   exit /B
)
IF "%endtest%"=="" (
   ECHO Supply a ending test number as a command-line parameter
   set /p=Hit ENTER to exit
   exit /B
)

IF "%webinstallpath%"=="" (
   ECHO Web install path is not defined
   set /p=Hit ENTER to exit
   exit /B
)

IF "%DwRegressionTestEmail%"=="" (
   ECHO Warning: Environment Variable DwRegressionTestEmail is not defined.  Results will not be emailed.
)

call :getversion

set testrootpath=%DwRentalWorksWebPath%\src\JestTest
cd %testrootpath%\output
set zipfilename=RentalWorksWeb Regression Reports (%version%).zip
if exist "%zipfilename%" (del "%zipfilename%" /Q)

for /L %%i in (%starttest%,1,%endtest%) do (
   echo %%i
   echo Running test number %%i
   start %DwRentalWorksWebPath%\src\JestTest\runRegressionTest %%i
   timeout 2
)

rem wait here until all pdf files exist
:checkforpdfs
echo -----------------------------------------------
echo Checking for test results
set pdfsfound=T
for /L %%i in (%starttest%,1,%endtest%) do (
   if not exist "%%i - *.pdf" (
      echo test %%i still running
	  set pdfsfound=F
   )
)
echo -----------------------------------------------
if %pdfsfound%==T goto allpdfsfound
timeout /T 10 >nul
goto checkforpdfs

:allpdfsfound
"c:\Program Files\7-Zip\7z.exe" a "%zipfilename%" *%version%*.pdf
exit


:getversion
set "file=%webinstallpath%\version.txt"
for /F "delims=" %%a in (%file%) do (
    set version=%%a
)
echo Testing Version %version%
exit /b



