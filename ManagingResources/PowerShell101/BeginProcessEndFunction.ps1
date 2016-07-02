Function Test-Demo
 {
  Param ($Param1)
  Begin{ write-host "Starting"}
  Process{ write-host "processing" $_ for $Param1}
  End{write-host "Ending"}
 }

 Echo Testing1, Testing2 | Test-Demo Sample 