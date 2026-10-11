# Schritt 10 – Lokale Embeddings und semantische Suche

## Ziel
Inhaltlich passende Notizen auch bei abweichender Wortwahl finden. Embeddings lokal über Ollama erzeugen; keine externe API voraussetzen.

## Modell-Evaluation
`nomic-embed-text` ist ein Kandidat, keine endgültige Festlegung. Modellversion, Lizenz, Vektordimension, Speicherbedarf, Qualität und CPU-Leistung auf Atlas prüfen.

## Vorgehen
1. Lokales Modell über Ollama bereitstellen.
2. Repräsentative Notizen und Suchanfragen zusammenstellen.
3. Latenz, Durchsatz und Trefferqualität messen.
4. Embedding-Erzeugung hinter einem Service kapseln.
5. Vektoren mit Dateipfad, Chunk-ID und Inhaltshash verknüpfen.
6. Geänderte Dateien neu einbetten; gelöschte Inhalte entfernen.
7. Vollständigen Rebuild unterstützen.
8. Semantische Suche zunächst getrennt von Volltextsuche anbieten.
9. Ergebnisse vergleichen; hybride Suche nur bei nachgewiesenem Nutzen ergänzen.

## Chunking und Speicher
Lange Dateien ggf. entlang von Überschriften und Absätzen teilen. Jeder Chunk muss auf Ursprungsdatei und Abschnitt zurückführbar sein. Speicherlösung nach realistischer Vault-Größe wählen; nicht vorschnell eine zusätzliche Vektordatenbank einführen.

## Tests und Evaluation
Testmenge mit erwarteten Treffern, Synonymen, ähnlichen falschen Dokumenten, kurzen/l langen Dateien und deutschen Begriffen anlegen. Qualität nicht an einem Einzelfall bewerten.

## Vorsicht
- Ollama-Ausfälle dürfen keinen Index als aktuell markieren, obwohl Embeddings fehlen.
- Inhaltshashes verhindern unnötige Neuberechnung.
- Modellwechsel kann inkompatible Vektoren erzeugen; Modellkennung und Dimension mit speichern.
- Markdown bleibt die Quelle der Wahrheit.

## Abnahme
- [ ] Embeddings lokal erzeugt
- [ ] Leistung auf Atlas gemessen
- [ ] Updates und Löschungen konsistent
- [ ] Neuaufbau möglich
- [ ] Suchqualität dokumentiert

## Referenzen
- https://docs.ollama.com/api
- https://docs.ollama.com/capabilities/embeddings
