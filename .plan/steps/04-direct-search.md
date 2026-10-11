# Schritt 04 – Direkte Volltextsuche

## Ziel
Markdown-Dateien ohne Datenbankindex durchsuchen. Neue und ergänzte Inhalte sollen unmittelbar auffindbar sein.

## Ergebnisformat
Mindestens relativer Dateipfad und passender Textausschnitt; optional Zeilennummer und nachvollziehbarer Score. Trefferzahl begrenzen und stabil sortieren.

## Vorgehen
1. `SearchService` mit klarer Schnittstelle anlegen.
2. Nur Markdown-Dateien unter dem Vault durchsuchen.
3. Suchsemantik festlegen: Groß-/Kleinschreibung, mehrere Begriffe, Phrasen und leere Abfrage.
4. Treffer-Snippets erzeugen statt vollständige Dokumente für jede Suche zurückzugeben.
5. Ergebniszahl begrenzen; deterministische Sortierung.
6. Service-Schnittstelle so wählen, dass später SQLite/FTS5 implementiert werden kann, ohne REST/MCP zu ändern.

## Vorsicht
- Direkte Textsuche ist keine semantische Suche: Synonyme ohne gemeinsame Begriffe werden nicht zuverlässig gefunden.
- Nicht den ganzen Vault als Antwort zurückgeben.
- Temporäre Dateien und nicht erlaubte Dateitypen ausschließen.
- Fehler beim Lesen einzelner Dateien nachvollziehbar behandeln.

## Tests / Abnahme
- [ ] Treffer und keine Treffer
- [ ] mehrere Treffer und Unterordner
- [ ] neue/ergänzte Datei sofort auffindbar
- [ ] leere Anfrage und viele Treffer
- [ ] Pfad und Kontext in Ergebnissen
- [ ] REST und MCP verwenden denselben Suchservice

## Später
SQLite FTS5 erst ergänzen, wenn die direkte Suche funktioniert und der Nutzen eines Indexes klar ist.

## Referenz
- https://learn.microsoft.com/dotnet/standard/io/
