# Deploy to Azure App Service (for .NET)

## 1. Build and publish

Open PowerShell at the project root and run:

```powershell
dotnet publish --configuration Release --output .\bin\publish-azure
```

## 2. Compress the publish output

```powershell
Compress-Archive -Path "bin\publish-azure\*" -DestinationPath "bin\publish-azure.zip" -Force
```

## 3. Sign in to Azure

```powershell
az login
```

## 4. Create Resource Group, App Service Plan and Web App

Run the following commands, replacing values in <> with your actual names/values:

```powershell
az group create --name <rg-name> --location <location>
az appservice plan create --name <plan-name> --resource-group <rg-name> --sku <sku> --is-linux false
az webapp create --name <webapp-name> --resource-group <rg-name> --plan <plan-name> --runtime "dotnet:9"
```

## 5. Deploy the zip package

```powershell
az webapp deploy --resource-group <rg-name> --name <webapp-name> --src-path "bin\publish-azure.zip" --type zip
```

## 6. Configure environment variables for the MCP app

Also set these app settings. Use the command below, replacing values in <> with your real values:

```powershell
az webapp config appsettings set --resource-group <rg-name> --name <webapp-name> --settings AZURE_TENANT_ID=<tenant-id> AZURE_CLIENT_ID=<client-id> AZURE_CLIENT_SECRET=<client-secret> AZURE_SCOPE=https://graph.microsoft.com/.default
```

## 7. Verify the deployment

```powershell
# Test GET request
curl https://<webapp-name>.azurewebsites.net/

# Test POST request (for MCP)
curl -X POST https://<webapp-name>.azurewebsites.net/ -v
```

## Important Notes

### Project configuration for Azure
- The project uses **Microsoft.NET.Sdk.Web** (not the standard SDK) so it runs correctly on Azure App Service
- Do **NOT** enable PublishSingleFile=true for Azure (that's for local or single-file scenarios)
- The project includes **web.config** for IIS when running on Azure App Service (Windows)
- SelfContained=false so the app uses the shared runtime on Azure

### Helpful commands
- List available .NET runtimes:
  ```powershell
  az webapp list-runtimes --os windows
  ```
- Stream logs in real time:
  ```powershell
  az webapp log tail --resource-group <rg-name> --name <webapp-name>
  ```
- Restart the app:
  ```powershell
  az webapp restart --resource-group <rg-name> --name <webapp-name>
  ```

## Access
After deployment the app will be available at:
```
https://<webapp-name>.azurewebsites.net
```
