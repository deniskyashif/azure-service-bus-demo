# Azure Service Bus Demo

Runs on .NET 7

## Run the Sender/Listener

1. Add a connection string and the queue name in appsettings.json
2. `cd path/to/{Sender|Listener}/`
3. `dotnet run`

## Create a Windows Service

1. `cd path/to/{Sender|Listener}`
2. `dotnet publish -o publish/ -c Release`
3. `sc.exe create "{Service Name}" binpath="path/to/publish/{Sender|Listener}.exe"
