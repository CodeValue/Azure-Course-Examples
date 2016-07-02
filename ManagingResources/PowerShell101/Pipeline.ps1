Get-Command Add-Computer | Get-Help

Get-Service | Where-Object {$_.DependentServices -ne $null}
Get-Service | ? DependentServices -ne $null

Get-Service | Select-Object {$_.DisplayName +" " + $_.CanStop }
