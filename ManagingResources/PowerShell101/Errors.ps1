ThisCmdlet-DoesNotExist # will cause as error
Write-Host errors count: $error.Count
Write-Host last error: $error[0]

Get-WmiObject Win32_BIOS -comp 'localhost','not-here'
$invocationInfo=$error[0].InvocationInfo 
Write-Host "the exception was thrown from: $($invocationInfo.ScriptName): $($invocationInfo.Line) ($($invocationInfo.ScriptLineNumber))"
Write-Host the exception message is: $error[0].Exception.Message 