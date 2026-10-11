# Schritt 01 – Repository und .NET-Grundgerüst

## Ziel
Ein minimal lauffähiges .NET-10-Projekt mit REST, MCP, Healthcheck, Tests und Docker erstellen. Noch keine Vault-Schreibfunktionen.

## Vorgehen
1. Repository `delphi` anlegen; README, `.gitignore` und Lizenz-/Projektmetadaten nach Bedarf.
2. `src/Delphi.Api` und `tests/Delphi.Api.Tests` anlegen.
3. REST-API mit `GET /health` erstellen.
4. Aktuelles MCP-C#-SDK nach offizieller Anleitung einbinden; zunächst ein ungefährliches `ping`-/Status-Tool bereitstellen.
5. REST- und MCP-Handler von Anfang an auf gemeinsame Services ausrichten.
6. Logging, Konfiguration und zentrale Fehlerbehandlung ergänzen.
7. Dockerfile mit passendem .NET-10-SDK-/Runtime-Image bauen.
8. CI für Restore, Build und Tests hinzufügen.

## Strukturvorschlag
`Api/`, `Mcp/`, `Services/`, `Vault/`, `Search/`, `Configuration/` unter `src/Delphi.Api`. Nicht für jede Klasse vorsorglich Interfaces anlegen; nur sinnvolle Grenzen schaffen.

## Vorsicht
- SDK-API und MCP-Transport anhand aktueller Dokumentation prüfen. Nicht annehmen, dass jeder Transport mit der installierten Open-WebUI-Version funktioniert.
- Keine Secrets in Git, Image oder Logs.
- Noch keinen Schreibzugriff auf den realen Vault geben.
- SDK- und Runtime-Version müssen zueinander passen; möglichst reproduzierbare Image-Tags verwenden.

## Tests / Abnahme
- [ ] `dotnet build` und `dotnet test` erfolgreich
- [ ] `/health` antwortet
- [ ] MCP-Testwerkzeug ist aufrufbar
- [ ] Docker-Build und Containerstart funktionieren
- [ ] Lokale Startanleitung ist dokumentiert

## Referenzen
- https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview
- https://learn.microsoft.com/aspnet/core/web-api/
- https://github.com/modelcontextprotocol/csharp-sdk
- https://modelcontextprotocol.io/
- https://docs.docker.com/build/building/multi-stage/
