$today = Get-Date
$after = $today.AddYears(10)
New-SelfSignedCertificate  -DnsName "localhost" -CertStoreLocation "Cert:\LocalMachine\My" `
-KeySpec "KeyExchange" `
-KeyUsage "DigitalSignature", "KeyEncipherment" `
-Type "SSLServerAuthentication" -NotAfter $after `
-Subject "CN=localhost, OU=IT, O=Andreas Iver Inc, L=Bergen, S=Vestland, C=Norway" `
-Provider "Microsoft Enhanced RSA and AES Cryptographic Provider" `
-HashAlgorithm "SHA256" `
-KeyLength 2048

$password = "secret" | ConvertTo-SecureString -AsPlainText -Force
$cert = Get-ChildItem -Path "Cert:\LocalMachine\My\" -DnsName "localhost"
$thumb = $cert.Thumbprint[3]
Export-PfxCertificate -Cert "Cert:\LocalMachine\My\$thumb" -FilePath "$env:USERPROFILE\.aspnet\https\fireguardcert.pfx" -Password $password

dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\fireguardcert.pfx -p secret --trust