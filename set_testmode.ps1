$mode = "test"
New-Item "src/RentalWorksWeb/App_Data/Temp/Downloads" -type directory
Copy-Item "src/RentalWorksAPI/Application.$mode.config" "src/RentalWorksAPI/Application.config"
Copy-Item "src/RentalWorksQuikScan/Application.$mode.config" "src/RentalWorksQuikScan/Application.config"
Copy-Item "src/RentalWorksQuikScan/Application.$mode.config" "src/RentalWorksQuikScan/ApplicationConfig.js"
Copy-Item "src/RentalWorksTest/Application.$mode.config" "src/RentalWorksTest/Application.config"
Copy-Item "src/RentalWorksWeb/Application.$mode.config" "src/RentalWorksWeb/Application.config"
Copy-Item "src/RentalWorksWeb/ApplicationConfig.$mode.js" "src/RentalWorksWeb/ApplicationConfig.js"
