@echo off
setlocal

set "folderPath=E:\Users\Ariel\Development\Projects\EDBG\Assets"
set "outputFile=E:\Users\Ariel\Development\Projects\EDBG\AssetList.txt"

for /R "%folderPath%" %%F in (*) do (
    echo %%~dpF%%~nxF >> "%outputFile%"
)

endlocal