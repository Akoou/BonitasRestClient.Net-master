powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& { Import-Module '.\src\packages\psake.4.4.1\tools\psake.psm1'; Invoke-psake '.\default.ps1' default -parameters @{build_parameters} -properties @{build_properties}; if ($lastexitcode -ne 0) {write-host "ERROR: $lastexitcode" -fore RED; exit $lastexitcode} }"
