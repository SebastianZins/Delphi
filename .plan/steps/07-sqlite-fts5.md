# Schritt 07 – SQLite/FTS5 als Suchindex

## Ziel
Volltextsuche beschleunigen, ohne Markdown als maßgebliche Quelle abzulösen. SQLite ist ein abgeleiteter Index und muss neu aufgebaut werden können.

## Datenmodell als Ausgangspunkt
- Metadaten: relativer Pfad, Änderungszeit, Größe, optional Inhaltshash und Indexierungszeit.
- FTS5: indexierter Markdown-Text und nötige Metadaten für Treffer.
Das konkrete Schema erst nach Definition der Suchanforderungen festlegen.

## Vorgehen
1. SQLite-Datei in persistentem, beschreibbarem Verzeichnis konfigurieren.
2. Initialen Scan des Vaults implementieren.
3. Dateien anhand von Hash oder verlässlichen Metadaten auf Änderungen prüfen.
4. Neue/geänderte Dateien indexieren und nicht mehr vorhandene entfernen.
5. Vollständigen Rebuild als explizite Operation bereitstellen.
6. Regelmäßigen Abgleich ergänzen; ein Dateisystem-Watcher darf nicht die einzige Konsistenzstrategie sein.
7. Suchservice hinter derselben Schnittstelle halten.

## Vorsicht
- Dateien können auch durch Obsidian oder manuell verändert werden.
- FTS5 hat eigene Query-Syntax; Eingaben validieren und Verhalten dokumentieren.
- Indexupdates transaktional ausführen.
- SQLite ist kein Backup der Markdown-Dateien.
- Index bei Fehlern nicht als aktuell markieren, wenn Aktualisierung scheiterte.

## Tests / Abnahme
- [ ] Erstindex
- [ ] geänderte/neue/gelöschte Dateien
- [ ] vollständiger Neuaufbau
- [ ] Sonderzeichen/Umlaute
- [ ] ungültige Suchanfrage
- [ ] Markdown bleibt Quelle der Wahrheit
- [ ] API- und MCP-Verträge bleiben stabil

## Referenzen
- https://www.sqlite.org/fts5.html
- https://learn.microsoft.com/dotnet/standard/data/sqlite/
