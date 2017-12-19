$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"
Start-Process -FilePath $msbuild -ArgumentList "/p:Configuration=Debug RentalWorksWeb.sln" -NoNewWindow
