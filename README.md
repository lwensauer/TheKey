Backend in C#/.Net welches periodisch neue Blog-Einträge von internate.org über die WordPress-API abruft.
Für jeden neuen Post werden die Anzahl der Wörter ermittelt. Diese Info wird an Clients über SignalR(WebSocket) gesendet.

## Testen 
### Voraussetzungen
* .NET 6 SDK https://dotnet.microsoft.com/en-us/download/dotnet/6.0
* NodeJS https://nodejs.org/en/

### Starten vom backend:
Im Verzeichnis `backend/TheKey.Backend` folgenden Befehl ausführen
#### `dotnet run`

### Starten vom frontend:
Im Verzeichnis `frontend` folgenden Befehl ausführen
#### `npm start`
Die Seite http://localhost:3000 wird zum Anzeigen im Browser geöffnet.

## Doku  

### backend
Es wird 15 Sekunden gewartet bis der erste Aufruf der der WordPress-API stattfindet.
Nach jeden Verarbeitung eines Blog-Post wird eine Sekunden pausiert. 

#### Endpoints
* WebSocket für Frontends https://localhost:5001/hubs/blog
* https://localhost:5001/blogentry Get-Endpunkte welcher alle in-memory Blog-Posts liefert
* [Swagger](https://localhost:5001/swagger/v1/swagger.json)

#### Verwendete Frameworks/Bibliotheken
* [SignalR](https://docs.microsoft.com/de-de/aspnet/core/signalr/introduction?view=aspnetcore-6.0) - siehe Doku
* [Swagger](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio) - siehe Doku
* [MediatR](https://github.com/jbogard/MediatR) - Mediator für CQRS beim Verarbeiten von Blog-Posts
* [WordPressPCL](https://github.com/wp-net/WordPressPCL) - Client Bibilothek zum Aufruf der WordPress-API
* [HTML Agility Pack](https://html-agility-pack.net/) - Parsen von HTML-Texten für den Word-Counter von Blog-Posts (sind immer mit HTML-Tags versehen)

### frontend
React-App erstellt mit Hilfe von [Create React App](https://github.com/facebook/create-react-app).
Verbindung zum Server (Websocket zum Endpunkt https://localhost:44389/hubs/blog' ) erfolgt mit SignalR.

Server schickt den Clients bei jedem Blog-Post ein Objekt mit folgenden Feldern:
* Id - ID des Blog-Eintrages (int)
* Message - Info über Anzahl Wörter des Blog-Postes
* Title - Titel des Blog-Postes
* WordCounterMap - Word Count Map ({“und”: 5, “der”: 3, ...}) über Blog-Post (ohne Titel)
* Content = post.Content - Post, HTML formatiert

Client zeigt diese Info an.

## "Live"-Demo
![GifCapture](https://user-images.githubusercontent.com/100235211/181298307-f1ba7bf9-ecf9-40ce-a800-9b9e9c41d050.gif)

