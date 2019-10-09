param([string]$subject,[string]$body,[string]$attachment)
$smtp = new-object Net.Mail.SmtpClient("ftp.dbworks.com")
$smtp.UseDefaultCredentials = $true;
$objMailMessage = New-Object System.Net.Mail.MailMessage
$objMailMessage.From = $Env:DwRegressionTestEmail
$objMailMessage.To.Add($Env:DwRegressionTestEmail)
$objMailMessage.Subject = $subject
$objMailMessage.Body = $body
$objMailMessage.Attachments.Add($attachment)
$smtp.send($objMailMessage)