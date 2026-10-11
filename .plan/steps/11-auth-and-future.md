# Schritt 11 – Autorisierung und weitere Funktionen

## Ziel
Delphi für weitere Clients und wachsende Anforderungen vorbereiten. Autorisierung ist nicht Teil des MVP.

## Bis dahin
- Dienst nicht unnötig auf Host-Ports veröffentlichen.
- Zugriff auf internes Docker-Netzwerk begrenzen.
- „Lokal“ nicht mit vollständiger Sicherheit gleichsetzen.
- REST- und MCP-Zugriffspunkte dokumentieren, damit später beide geschützt werden.

## Vor Umsetzung klären
- Welche Clients greifen zu?
- Gemeinsamer API-Schlüssel oder individuelle Identitäten?
- Getrennte Rechte für Lesen, Schreiben und Löschen?
- Secret-Rotation und Audit-Logs?
- Wie wird der konkrete MCP-Transport authentifiziert?

## Mögliche spätere Funktionen
- automatische Klassifizierung und Sortierung
- Vorschläge für interne Verlinkungen
- Änderungsverlauf/Git-Integration
- Backup und Wiederherstellung
- Metriken und Diagnostik
- differenzierte Rechte für schreibende Tools

Neue Funktionen erst aufnehmen, wenn ein konkreter Anwendungsfall und prüfbare Abnahmekriterien vorliegen. Die Dateiverwaltung muss deterministisch bleiben.

## Abnahme
- [ ] Clients und Bedrohungsmodell beschrieben
- [ ] REST und MCP berücksichtigt
- [ ] Rechte passend zu Operationen definiert
- [ ] Secrets nicht im Repository
- [ ] Erweiterungen haben Tests und klare Anforderungen

## Referenzen
- https://learn.microsoft.com/aspnet/core/security/
- https://modelcontextprotocol.io/
- https://docs.docker.com/engine/security/
