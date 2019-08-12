rem !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
rem WARNING: HARD-CODED PATHS IN USE (WIP)
rem !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

rem delete any old build files
cd C:\project\RentalWorks\RentalWorksWeb\build
rmdir RentalWorksWeb /S /Q
rmdir RentalWorksWebApi /S /Q
del *.zip


rem Get the Build number from the user
set /p buildno="Build Number (ie. 2019.1.1.15): "


rem Update the Build number in the version.txt files
echo | set /p buildnumber=%buildno%> C:\project\RentalWorks\RentalWorksWeb\src\RentalWorksWeb\version.txt
echo | set /p buildnumber=%buildno%> C:\project\RentalWorks\RentalWorksWeb\src\RentalWorksWebApi\version.txt
echo | set /p buildnumber=%buildno%> C:\project\RentalWorks\RentalWorksWeb\src\RentalWorksWeb\version-RentalWorksWeb.txt


rem build Web
dotnet restore C:\project\RentalWorks\RentalWorksWeb\RentalWorksWeb.sln
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\msbuild.exe" C:\project\RentalWorks\RentalWorksWeb\RentalWorksWeb.sln     /t:Rebuild   /p:Configuration="Release Web" /p:Platform="any cpu" /p:ReferencePath=C:\project\RentalWorks\RentalWorksWeb\packages 


rem build the API 
cd C:\project\RentalWorks\RentalWorksWeb\src\RentalWorksWebApi
call npm run publish


rem RUN REGRESSION TEST SCRIPTS HERE


rem make the ZIP deliverable
setlocal ENABLEDELAYEDEXPANSION
set buildnoforzip=%buildno:.=_%
set zipfilename=RentalWorksWeb_%buildnoforzip%.zip
echo %zipfilename%
"c:\Program Files\7-Zip\7z.exe" a C:\project\RentalWorks\RentalWorksWeb\build\%zipfilename% C:\project\RentalWorks\RentalWorksWeb\build\RentalWorksWeb\
"c:\Program Files\7-Zip\7z.exe" a C:\project\RentalWorks\RentalWorksWeb\build\%zipfilename% C:\project\RentalWorks\RentalWorksWeb\build\RentalWorksWebApi\
cd C:\project\RentalWorks\RentalWorksWeb\build


rem delete the work files
rmdir RentalWorksWeb /S /Q
rmdir RentalWorksWebApi /S /Q


rem copy the ZIP delivable to "history" sub-directory
copy %zipfilename% history



rem command-line Git push in the modified version and assemply files
rem command-line Git make Release and Tag
rem command-line gren make Build Release Document
rem gren changelog --token=4f42c7ba6af985f6ac6a6c9eba45d8f25388ef58 --username=databaseworks --repo=rentalworksweb --generate --override --changelog-filename=RELEASE_NOTES.md -D issues -m --group-by label