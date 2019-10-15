osql.exe -S justin4\sql2012 -d rentalworks_dfs_2019_001 -U dbworks -P db2424 -Q "exec fw_installhotfixes 'O'"
osql.exe -S justin4\sql2012 -d rentalworks_dfs_2019_001 -U dbworks -P db2424 -Q "exec fw_grantall 'rentalworksweb'"
