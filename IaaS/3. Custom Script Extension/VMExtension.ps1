Get-AzureVMAvailableExtension | Format-Table -Property ExtensionName, Publisher
Get-AzureVMAvailableExtension | ?{$_.ExtensionName -eq "CustomScriptExtension"}

$rgName = “AzureCourse”
$saName = "AzureInternalCourse123"
$saKey = "<Key Here>"
$vmName = “BootcampVM”
Set-AzureRmVMCustomScriptExtension -ResourceGroupName $rgName -VMName $vmName -Location "West Europe" -Name "MyScriptExtension" -StorageAccountName $saName -StorageAccountKey $saKey -ContainerName "scripts" -FileName "helloworld.ps1" -Run "helloworld.ps1"
