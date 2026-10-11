# Schritt 08 – Textnormalisierung und gezielte Bearbeitung

## Ziel
Inhalte konsistenter machen und bestehende Notizen kontrolliert verändern.

## Normalisierung
Vor Implementierung Regeln und Beispiele festlegen. Denkbare Regeln: Zeilenenden vereinheitlichen, Leerzeichen am Zeilenende entfernen und übermäßige Leerzeilen behandeln. Unicode-Normalisierung nur bei konkretem Bedarf. Keine ungefragte Umformulierung und keine Beschädigung von Frontmatter, Tabellen, Links oder Codeblöcken.

## Gezielte Bearbeitung
Separate Operationen vorsehen, z. B. Abschnitt anhand einer eindeutigen Überschrift ersetzen oder exaktes Textfragment ersetzen. Keine mehrdeutige universelle „edit anything“-Operation.

Ablauf:
1. Datei lesen.
2. Zielstelle suchen.
3. Prüfen, ob genau ein Treffer vorliegt.
4. Bei null oder mehreren Treffern abbrechen.
5. Änderung schreiben.
6. Datei erneut lesen und Ergebnis verifizieren.

## Vorsicht
- Vorherige Version über Git oder eine andere Wiederherstellungsmöglichkeit schützen.
- Änderungen nur innerhalb des Vaults.
- Mehrdeutigkeit nicht erraten.
- Tests mit LF/CRLF, Umlauten, Frontmatter, Tabellen und Codeblöcken durchführen.

## Abnahme
- [ ] Regeln sind spezifiziert und deterministisch
- [ ] eindeutige Zielstelle wird geändert
- [ ] null/mehrere Treffer führen zu klarem Fehler
- [ ] Ergebnis wird erneut gelesen
- [ ] Markdown-Struktur bleibt erhalten
