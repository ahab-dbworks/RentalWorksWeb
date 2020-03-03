echo off
rem --------------------------------------------------------------------------
rem Purpose:       
rem This batch file will run concurrent automated tests in parallel on RentalWorksWeb
rem Indicate the start and stop number of tests to run as paramters to this batch file execution
rem Example: C:\Regression\src\JestTest\runRegressionTestsParallel.bat 1 15      -- this will run tests 1 through 15 in parallel
rem Example: C:\Regression\src\JestTest\runRegressionTestsParallel.bat           -- this will run all tests in serial 
rem --------------------------------------------------------------------------
rem Author:        Justin Hoffman
rem Last modified: 03/03/2020
rem --------------------------------------------------------------------------
rem
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
rem NO MODIFICATIONS BEYOND THIS POINT
rem --------------------------------------------------------------------------
rem --------------------------------------------------------------------------
set starttest=0
set endtest=0
if not "%~1"=="" set starttest=%~1
if not "%~2"=="" set endtest=%~2

IF "%DwRentalWorksWebPath%"=="" ECHO Environment Variable DwRentalWorksWebPath is NOT defined
IF "%DwRentalWorksWebPath%"=="" set /p=Hit ENTER to exit
IF "%DwRentalWorksWebPath%"=="" exit /B
IF "%DwRegressionTestEmail%"=="" ECHO Warning: Environment Variable DwRegressionTestEmail is not defined.  Results will not be emailed.

if %starttest%==0 (
   echo Running all tests
   start %DwRentalWorksWebPath%\src\JestTest\runRegressionTests
)
if not %starttest%==0 (
   for /L %%i in (%starttest%,1,%endtest%) do (
      echo %%i
      echo Running test number %%i
      start %DwRentalWorksWebPath%\src\JestTest\runRegressionTests %%i
      timeout 2
   )
)
