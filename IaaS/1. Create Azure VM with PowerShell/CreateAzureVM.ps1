[CmdletBinding()]
Param(
	[string]$tenantId,
	[string]$subscriptionId,
	[string]$rgName = 'CourseScript',
	[string]$locName = 'West Europe',
	[string]$saName = 'azurecourseinternal123'
)

#Login to Azure
Login-AzureRmAccount -TenantId $tenantId -SubscriptionId $subscriptionId
Write-Information 'Logged in'

#Create a Resource Group
New-AzureRmResourceGroup -Name $rgName -Location $locName
Write-Information 'Resource Group created'

#Create a Storage Account
$saType = 'Standard_LRS'
New-AzureRmStorageAccount -Name $saName -ResourceGroupName $rgName –Type $saType -Location $locName
Write-Information 'Storage Account created'

#Create a Virtual Network
$subnet=New-AzureRmVirtualNetworkSubnetConfig -Name 'MySubnet' -AddressPrefix 10.0.1.0/24
$vnetName = 'TestNet'
$subnetIndex=0
$vnet=New-AzureRmVirtualNetwork -Name $vnetName -ResourceGroupName $rgName -Location $locName -AddressPrefix 10.0.0.0/16 -Subnet $subnet
Write-Information 'VNET created'

#Create NIC
$nicName="MyNic"
$pip = New-AzureRmPublicIpAddress -Name $nicName -ResourceGroupName $rgName -Location $locName -AllocationMethod Dynamic
$nic = New-AzureRmNetworkInterface -Name $nicName -ResourceGroupName $rgName -Location $locName -SubnetId $vnet.Subnets[$subnetIndex].Id -PublicIpAddressId $pip.Id
Write-Information 'NIC created'

#Create VM
$vmName="TestVM"
$vmSize="Standard_A2"
$vm=New-AzureRmVMConfig -VMName $vmName -VMSize $vmSize

$diskSize=200
$diskLabel="Data"
$diskName="Data"
$storageAcc=Get-AzureRmStorageAccount -ResourceGroupName $rgName -Name $saName
$vhdURI=$storageAcc.PrimaryEndpoints.Blob.ToString() + "vhds/" + $vmName + $diskName  + ".vhd"
Add-AzureRmVMDataDisk -VM $vm -Name $diskLabel -DiskSizeInGB $diskSize -VhdUri $vhdURI  -CreateOption empty

$pubName="MicrosoftWindowsServer"
$offerName="WindowsServer"
$skuName="2012-R2-Datacenter"
$cred=Get-Credential -Message "Type the name and password of the local administrator account."
$vm=Set-AzureRmVMOperatingSystem -VM $vm -Windows -ComputerName $vmName -Credential $cred -ProvisionVMAgent -EnableAutoUpdate
$vm=Set-AzureRmVMSourceImage -VM $vm -PublisherName $pubName -Offer $offerName -Skus $skuName -Version "latest"
$vm=Add-AzureRmVMNetworkInterface -VM $vm -Id $nic.Id

$diskName="OSDisk"
$storageAcc=Get-AzureRmStorageAccount -ResourceGroupName $rgName -Name $saName
$osDiskUri=$storageAcc.PrimaryEndpoints.Blob.ToString() + "vhds/" + $diskName  + ".vhd"
$vm=Set-AzureRmVMOSDisk -VM $vm -Name $diskName -VhdUri $osDiskUri -CreateOption fromImage

New-AzureRmVM -ResourceGroupName $rgName -Location $locName -VM $vm
Write-Information 'VM Created'
