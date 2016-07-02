 param([string] $ServiceName)
get-service $ServiceName | stop-service -PassThru 
