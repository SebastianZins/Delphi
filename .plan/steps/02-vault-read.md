# Schritt 02 – Vault lesen und auflisten

## Ziel
Dateien und Verzeichnisse im konfigurierten Markdown-Vault auflisten und Markdown-Dateien lesen. Kein Zugriff außerhalb des Vaults.

## Vorgehen
1. Vault-Root über Konfiguration/Umgebungsvariable, z. B. `VAULT_ROOT`, setzen.
2. Beim Start Existenz und Zugriffsrechte prüfen.
3. Einen zentralen Vault-Service implementieren.
4. Client-Pfade als relativ behandeln und normalisieren.
5. Auflösen des absoluten Zielpfads gegen den Vault-Root prüfen.
6. Verzeichnislisten und Leseoperationen als REST-Endpunkte und MCP-Tools bereitstellen.
7. Einheitliche Fehler für nicht vorhandene Datei, falschen Typ, unzulässigen Pfad und I/O-Fehler liefern.

## Pfadsicherheit
String-Prefix-Prüfungen allein reichen nicht zwingend. `..`, absolute Pfade, gemischte Trennzeichen und Symlinks berücksichtigen. Festlegen, ob Symlinks überhaupt erlaubt sind; für das MVP sollten Links, die aus dem Vault führen, abgewiesen werden. Der Client darf den Vault-Root nicht selbst bestimmen.

## Tests
- Datei lesen und Verzeichnis auflisten
- leeres Verzeichnis und nicht vorhandene Datei
- `../`-Traversal und absolute Pfade
- Symlink nach außerhalb
- Dateiname mit Umlauten und Leerzeichen
- Zugriff auf Verzeichnis, wo eine Datei erwartet wird

## Vorsicht
Nur `.md`-Dateien anbieten, sofern kein anderer Bedarf bestätigt wurde. Inhalte nicht vollständig in Logs schreiben. Ergebnisse möglichst mit relativem Pfad zurückgeben. Tests nur in einem temporären Vault.

## Abnahme
- [ ] REST und MCP lesen dieselben Inhalte
- [ ] Dateiliste und Dateiinhalte korrekt
- [ ] Zugriff außerhalb des Vaults blockiert
- [ ] Fehler klar und nicht als Erfolg dargestellt

## Referenzen
- https://learn.microsoft.com/dotnet/api/system.io.path
- https://learn.microsoft.com/aspnet/core/web-api/
