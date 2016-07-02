$message = New-Object System.Net.Mail.MailMessage("tamir.dresher@gmail.com", "tamir.dresher@gmail.com")


Add-Type -AssemblyName PresentationCore,PresentationFramework	
$ButtonType = [System.Windows.MessageBoxButton]::YesNo
$MessageboxTitle = "Test pop-up message title"
$Messageboxbody = "Are you sure you want to stop this script execution?"
$MessageIcon = [System.Windows.MessageBoxImage]::Warning
[System.Windows.MessageBox]::Show($Messageboxbody,$MessageboxTitle,$ButtonType,$messageicon)
