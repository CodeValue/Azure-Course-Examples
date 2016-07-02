Get-Process | Where-Object {($_.Name -eq "iexplore") -or ($_.Name -eq "chrome") -or ($_.Name -eq "firefox")}

Get-WMIObject Win32_LogicalDisk | ForEach-Object {$_.freespace}
Get-WMIObject Win32_LogicalDisk | ForEach-Object {$_.freespace / 1MB}
Get-WMIObject Win32_LogicalDisk | ForEach-Object {$_.freespace / 1GB}


#invoking a static .NET member
[DateTime]::Now

#range of number
1..10

#string format
"{0:N1} and {1:N2}" -f (100/3),(76/3)

#this is just a string
"c:\Windows\notepad.exe"

#invoke te string
& "c:\Windows\notepad.exe"


"A B C" -split " "
"A/,B/,C/," -split "/,"
"A", "B", "C" -join " "