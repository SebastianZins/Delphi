# Schritt 03 – Dateien erstellen und Inhalte anhängen

## Ziel
Die bisherigen grundlegenden Python-Schreibfunktionen ersetzen: neue Markdown-Datei anlegen und Inhalt an eine bestehende Datei anhängen.

## API-Verhalten
- `create_note`: erstellt eine neue Datei und schlägt fehl, wenn der Pfad bereits existiert.
- `append_note`: ergänzt eine bestehende Datei.
- REST und MCP verwenden dieselben Services; Namen und Schema nach Implementierung dokumentieren.

## Vorgehen
1. Pfad, Dateiendung und Inhalt validieren.
2. Zielpfad mit demselben Vault-Service wie beim Lesen auflösen.
3. Beim Erstellen eine Dateioperation verwenden, die vorhandene Dateien nicht überschreibt.
4. Beim Anhängen Existenz, Typ und Schreibrechte prüfen.
5. Zeilenumbruch-Verhalten explizit definieren. Inhalte nicht unbemerkt in einen einzigen Absatz umformen.
6. Status und relativen Pfad zurückgeben.
7. Nach Schreiboperation in Integrationstests erneut lesen und Inhalt vergleichen.

## Vorsicht
- Das frühere Python-Testverhalten zeigte, dass ein Modell Text als einen langen Absatz anhängen kann. Delphi sollte Eingaben nicht heimlich umformatieren, aber Zeilenumbrüche und optionalen Trenner dokumentieren.
- Nebenläufige Schreibvorgänge können kollidieren. Konkurrenzverhalten dokumentieren und bei Bedarf pro Datei synchronisieren.
- I/O-Fehler, volle Datenträger, fehlende Berechtigungen und bestehende Dateien sauber behandeln.
- Keine Erfolgsmeldung allein aufgrund einer geplanten Aktion.

## Tests / Abnahme
- [ ] Datei neu erstellen
- [ ] bestehende Datei wird nicht überschrieben
- [ ] Text an vorhandene Datei anhängen
- [ ] Zeilenumbrüche korrekt
- [ ] REST und MCP geben eindeutiges Resultat
- [ ] erneutes Lesen bestätigt gespeicherten Inhalt

## Referenz
- https://learn.microsoft.com/dotnet/api/system.io.file
