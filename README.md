# WebSocket-Angular
.NET Core 2.1 Signalr WebSocket and Angular 6 client.

## Dependencies
 - Node 10.8.0
 - npm 6.2.0
   - Global: @angular/cli@6.1.2
 - dotnet 2.1.302
## Setup
### Server
> Run server as a console application with the port 8060. 
> If you choose to use a different port, 
> you must update the port on the client side as well which can be found [here](Client/src/app/components/chat/chat.component.ts#L20).
### Client
> Install node packages and run on port 4200. If you want to run the client on a different port, 
> you must update the port found [here](Server/Startup.cs#L38).

## Build Steps
### Client
ng cli is configured to output build to wwwroot on the Server.
```bash
# Install packages
cd Client/
npm i
# Build prod
ng build --prod
```
### Server
```bash
# Assuming you are still in Client
cd ../Server
dotnet build -c Release
```
#### Run Server
```bash
# Use sudo for running on port 80
sudo dotnet bin/Release/netcoreapp2.1/WebSocket.Server.dll
```
If you are using windows, run PowerShell as admin and use the same command as above without sudo:
```powershell
dotnet Server/bin/Release/netcoreapp2.1/WebSocket.Server.dll
```
