# Schritt 09 – Löschen und Archivieren

## Ziel
Dateien kontrolliert aus dem aktiven Wissensbestand entfernen oder reversibel archivieren.

## Vorher entscheiden
Permanentes Löschen und Archivieren sind unterschiedliche Funktionen. Vor Implementierung festlegen:
- welche Dateitypen erlaubt sind,
- ob nur einzelne Dateien oder auch Ordner betroffen sein dürfen,
- ob Archivierung bevorzugt wird,
- wie Namenskollisionen behandelt werden,
- wie spätere SQLite-Einträge und Embeddings bereinigt werden,
- welche Bestätigung das Tool benötigt.

## Vorgehen
1. API-Vertrag für Löschen und Archivieren festlegen.
2. Pfad, Existenz und Dateityp prüfen.
3. Nur Dateien innerhalb des Vaults akzeptieren.
4. Operation ausführen und tatsächlichen Status zurückgeben.
5. Bei Archivierung Zielpfad und Kollisionen behandeln.
6. Indexänderung sofort durchführen oder Synchronisierung markieren.
7. Nur in einem temporären Test-Vault testen.

## Vorsicht
- Kein rekursives Löschen von Verzeichnissen in der ersten Version.
- Kein Überschreiben eines vorhandenen Archivziels.
- Erfolg nur melden, wenn die Datei wirklich entfernt/verschoben wurde.
- Pfadvalidierung aus dem Vault-Service wiederverwenden.

## Abnahme
- [ ] Semantik dokumentiert
- [ ] nur erlaubte Dateien betroffen
- [ ] Fehler und Konflikte eindeutig
- [ ] tatsächlicher Zustand geprüft
- [ ] späterer Suchindex bleibt konsistent
