# scripts/run-tests.ps1

# 1) Build the .NET API
Write-Host "▶ Building .NET API..."
dotnet build .\Api\Api\Api\Api.csproj --configuration Release
if ($LASTEXITCODE -ne 0) {
  Write-Error ".NET build failed, aborting."
  exit 1
}

# 2) Install & lint Python
Write-Host "▶ Installing Python deps and linting..."
Set-Location .\Python
python -m pip install --upgrade pip
pip install -r requirements.txt
pip install flake8

# Exclude both the generated grpc_service folder and your Services folder
flake8 --max-line-length=120 --exclude=grpc_service,Services .

if ($LASTEXITCODE -ne 0) {
  Write-Error "Python lint failed."
  exit 1
}
Set-Location ..

Write-Host "Build & lint succeeded!"
