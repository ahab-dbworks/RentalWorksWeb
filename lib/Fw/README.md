# DatabaseWorks Web FrameWork

## Synching the Fw repository to your project repository

### Learn How to Set System Environment Variables
- https://www.youtube.com/watch?v=C-U9SGaNbwY

### Set System Environment Variables

Please set the following variable values to the directory of the root of their git repositories on your computer (NO TRAILING BACKSLASH).  If you are only working on RentalWorksWeb, then you will only need the first 2.

1. DwFwPath
2. DwRentalWorksWebPath
3. DwGateWorksWebPath
4. DwTransWorksWebPath
5. DwMediaWorksWebPath

### Update the Fw for RentalWorks Web

To sync the Fw repository to RentalWorksWeb, run the following PowerShell script in the root of the Fw repository:
update_Fw_RentalWorksWeb.ps1

It will log to the file below, overwriting the file each run
update_Fw_RentalWorksWeb.ps1.log
