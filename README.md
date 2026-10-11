# Delphi – Umsetzungsplan

> **Projekt:** Delphi  
> **Ziel:** Lokaler Wissensdienst für Markdown-basierte Wissensverwaltung und den Zugriff eines LLMs über REST und MCP.  
> **Repository:** `delphi`  
> **Codeprojekt:** `Delphi.Api`  
> **Docker-Container:** `brain-delphi`  
> **Zielplattform:** Ubuntu-Server Atlas, Docker-Netzwerk `brain_net`  
> **Technologie:** .NET 10  
> **Stand:** 11. Oktober 2026

## 1. Ziel und Leitlinien

Delphi ersetzt die bisherigen Python-Dienste für Markdown-Suche und Schreibzugriffe durch eine gemeinsame .NET-Anwendung. Das LLM soll Funktionen zum Lesen, Schreiben und Suchen über MCP-Werkzeuge verwenden können. Dieselben fachlichen Funktionen sollen außerdem über REST-Endpunkte erreichbar sein, damit sie einfach manuell getestet und später auch von anderen Clients genutzt werden können.

Grundsätze:

- **Markdown bleibt die maßgebliche Datenquelle.**
- REST und MCP verwenden dieselbe Geschäftslogik; die Implementierung wird nicht doppelt gepflegt.
- Die erste Version bleibt klein und wird schrittweise erweitert.
- Alle Modelle und Verarbeitungsdienste sollen lokal laufen. Für Embeddings ist zunächst Ollama vorgesehen.
- SQLite wird später als regenerierbarer Suchindex ergänzt. Der Index ist nicht die Quelle der Wahrheit.
- Delphi läuft zunächst nur im internen Docker-Netzwerk. Eine spätere Autorisierung soll möglich sein, wird aber nicht Teil des MVP.
- Die bisherigen Python-Dienste werden erst abgeschaltet, wenn die entsprechenden Delphi-Funktionen getestet und in Open WebUI umgestellt wurden.

## 2. Bestehende Umgebung

| Komponente | Aktueller Stand / vorgesehene Nutzung |
|---|---|
| Server | Ubuntu-Server Atlas |
| Docker-Netzwerk | `brain_net` |
| Open WebUI | `http://192.168.178.125:3000` |
| Markdown-Vault | `/opt/brain/vault-test` |
| Vault-Struktur | `Inbox/`, `Knowledge/`, `Templates/`, `INDEX.md` |
| Ollama | Bereits vorhanden; später für lokale Embeddings |
| Python-Suchdienst | `brain-markdown-search`, soll durch Delphi ersetzt werden |
| Python-Schreibdienst | `brain-vault-write-mcp`, soll durch Delphi ersetzt werden |
| GitHub | Neues Repository `delphi` |
| Deployment | GitHub Actions baut und deployed den Dienst auf Atlas |

**Wichtig:** Der Vault wird als persistentes Verzeichnis in den Container eingebunden und nicht in das Docker-Image kopiert. Delphi erhält nur Zugriff auf den dafür vorgesehenen Vault-Pfad.

## 3. Zielarchitektur

```text
Open WebUI / LLM                  Manuelle Tests / andere Clients
        |                                      |
       MCP                                    REST
        |                                      |
        +------------------+-------------------+
                           |
                    Delphi.Api (.NET 10)
                           |
              Gemeinsame Geschäftslogik
                           |
             +-------------+-------------+
             |                           |
       Markdown-Vault              Später: SQLite
       Quelle der Wahrheit        regenerierbarer Index
                                         |
                                  Später: Embeddings
                                  lokal über Ollama
```

### Schichten

- **REST:** HTTP-Endpunkte für Tests und externe Clients.
- **MCP:** Werkzeuge, die das LLM aufrufen kann.
- **Services:** Gemeinsame Geschäftslogik für Dateizugriff, Suche und spätere Indexierung.
- **Vault-Zugriff:** Kapselt Lesen und Schreiben im erlaubten Markdown-Verzeichnis.
- **Search:** Beginnt mit direkter Suche in Markdown-Dateien. Später wird die Implementierung durch einen SQLite-FTS5-Index ergänzt.
- **Configuration:** Pfade und Laufzeitkonfiguration kommen aus Konfiguration bzw. Umgebungsvariablen, nicht aus fest codierten absoluten Pfaden im Code.

REST und MCP sind keine getrennten Anwendungen. Sie sind zwei Schnittstellen zur gleichen Anwendung und greifen auf dieselben Services zu.

## 4. MVP-Umfang

### Im MVP enthalten

1. **Dateien und Verzeichnisse auflisten**
2. **Markdown-Dateien lesen**
3. **Markdown-Dateien erstellen**
4. **Inhalte an bestehende Dateien anhängen**
5. **Volltextsuche direkt in den Markdown-Dateien**

Die Funktionen sollen sowohl über REST als auch als MCP-Werkzeuge verfügbar sein.

### Nicht im MVP enthalten

- Gezieltes Ersetzen oder Bearbeiten einzelner Abschnitte
- Löschen oder Archivieren von Dateien
- Textnormalisierung und Verdichtung
- Embeddings und semantische Suche
- SQLite-Index
- Automatische Sortierung und Klassifizierung von Notizen
- Benutzerverwaltung und vollständige Autorisierung

Diese Funktionen kommen erst nach dem stabilen MVP in separaten Schritten hinzu.

## 5. Umsetzungsschritte

### Schritt 1 – Repository und .NET-Grundgerüst

**Ziel:** Ein minimal lauffähiges, containerisierbares .NET-Projekt erstellen.

Aufgaben:
- Neues GitHub-Repository `delphi` anlegen.
- .NET-10-Projekt `Delphi.Api` einrichten.
- Grundstruktur für REST, MCP, Services, Vault und Suche anlegen.
- Konfiguration über Umgebungsvariablen bzw. `appsettings` vorsehen.
- Logging und eine einheitliche Fehlerantwort einrichten.
- Dockerfile und lokale Build-Anleitung hinzufügen.
- GitHub Actions zunächst für Build und Tests einrichten.

**Abnahmekriterien:**
- Das Projekt lässt sich lokal bauen und testen.
- Die Anwendung startet lokal und in einem Container.
- Ein Health-Endpunkt meldet den Anwendungsstatus.
- Ein einfacher REST-Endpunkt und ein MCP-Testwerkzeug sind erreichbar.
- Keine Zugangsdaten oder geheimen Werte liegen im Repository.

### Schritt 2 – Sicherer Markdown-Dateizugriff

**Ziel:** Dateien und Verzeichnisse im Vault lesen und auflisten.

Aufgaben:
- Vault-Pfad konfigurierbar machen.
- Dateien und Verzeichnisse relativ zum Vault adressieren.
- Verzeichnisse auflisten und Markdown-Dateien lesen.
- Pfade normalisieren und Zugriffe außerhalb des Vaults ablehnen.
- Fehler für nicht vorhandene Dateien, ungültige Pfade und fehlende Berechtigungen verständlich zurückgeben.
- Dateizugriff über einen zentralen Service kapseln.

**Abnahmekriterien:**
- REST und MCP liefern dieselben Dateiinhalte.
- Das LLM kann Dateien und Verzeichnisse finden und Markdown-Inhalte lesen.
- Der Zugriff ist auf den Vault begrenzt.
- Fehlerfälle liefern klare Fehlermeldungen statt falscher Erfolgsmeldungen.

### Schritt 3 – Dateien erstellen und Inhalte ergänzen

**Ziel:** Die beiden Schreibfunktionen aus dem bisherigen Python-Dienst ersetzen.

Aufgaben:
- Neue Markdown-Dateien erstellen.
- Vorhandene Dateien um Inhalte ergänzen.
- Keine bestehende Datei stillschweigend überschreiben, wenn eine neue Datei erstellt werden soll.
- Eingaben und Zielpfade validieren.
- Ergebnisse mit Pfad und Status zurückgeben.
- Schreiboperationen nach Möglichkeit atomar ausführen.
- Nach kritischen Schreibvorgängen den gespeicherten Inhalt erneut lesen und überprüfen.

**Abnahmekriterien:**
- Neue Dateien und Ergänzungen funktionieren über REST und MCP.
- Ein erneutes Lesen bestätigt den tatsächlich gespeicherten Inhalt.
- Fehler werden als Fehler gemeldet; Delphi behauptet keinen erfolgreichen Schreibvorgang, wenn dieser nicht stattgefunden hat.
- Ein vorhandener Dateiname führt beim Erstellen nicht zu einem unbemerkten Überschreiben.

### Schritt 4 – Direkte Volltextsuche

**Ziel:** Relevante Markdown-Dateien ohne Datenbankindex auffinden.

Aufgaben:
- Markdown-Dateien im Vault durchsuchen.
- Suchbegriffe und Treffer nachvollziehbar behandeln.
- Treffer mit Dateipfad und passendem Textausschnitt zurückgeben.
- Suchergebnisse begrenzen und deterministisch sortieren.
- Dateien, die gerade erstellt oder ergänzt wurden, direkt in der Suche berücksichtigen.
- Die Suchlogik hinter einer Service-Schnittstelle kapseln, damit später SQLite verwendet werden kann.

**Abnahmekriterien:**
- Begriffe werden in bestehenden und neu geschriebenen Dateien gefunden.
- Die Treffer enthalten Pfad und relevanten Textausschnitt.
- Änderungen am Vault werden ohne manuellen Indexaufbau sichtbar.
- Die Suche bleibt bei einem leeren Vault oder nicht gefundenen Begriffen stabil.

### Schritt 5 – GitHub Actions und Deployment auf Atlas

**Ziel:** Delphi reproduzierbar bauen und auf dem Server bereitstellen.

Aufgaben:
- GitHub-Actions-Workflow für Restore, Build und Tests erstellen.
- Docker-Image bauen und in einer Registry bereitstellen.
- Atlas so konfigurieren, dass das Image aktualisiert und der Container neu gestartet werden kann.
- Den Dienst in `brain_net` einbinden.
- Den Vault persistent und mit passenden Dateirechten mounten.
- Healthcheck und einfache Prüfung nach dem Deployment einrichten.
- Konfiguration und Secrets über GitHub Secrets bzw. die Serverumgebung verwalten.

**Abnahmekriterien:**
- Ein erfolgreicher Push auf den festgelegten Deployment-Branch startet den Build- und Deployment-Prozess.
- Ein fehlerhafter Build wird nicht ausgerollt.
- Der Container startet nach einem Deployment mit Zugriff auf den vorhandenen Vault.
- Markdown-Dateien bleiben bei Image-Updates erhalten.
- Der Dienst ist nur über die vorgesehenen internen Verbindungen erreichbar; es wird kein unnötiger Host-Port veröffentlicht.

**Reihenfolgehinweis:** Die ersten Schritte können lokal umgesetzt werden. Das produktive Deployment wird spätestens nach erfolgreichem Datei- und Suchtest abgeschlossen. Bis Delphi zuverlässig funktioniert, bleiben die Python-Dienste verfügbar.

### Schritt 6 – Open WebUI umstellen und Python-Dienste ablösen

**Ziel:** Die tatsächlichen LLM-Workflows auf Delphi umstellen.

Aufgaben:
- MCP-Verbindung in Open WebUI auf Delphi umstellen.
- Verfügbare Werkzeuge und deren Beschreibungen prüfen.
- Lesen, Erstellen, Ergänzen und Suchen anhand konkreter Testfälle prüfen.
- Sicherstellen, dass das Modell Werkzeugergebnisse korrekt interpretiert und Fehler nicht als Erfolg darstellt.
- Alte Python-Dienste erst nach erfolgreicher Prüfung abschalten.

**Abnahmekriterien:**
- Open WebUI kann die Delphi-Werkzeuge aufrufen.
- Die Werkzeugantworten sind für das LLM eindeutig.
- Ein Testablauf kann eine Notiz suchen, lesen, erstellen oder ergänzen und das Ergebnis erneut lesen.
- Die alten Python-Dienste werden nicht mehr für dieselben Funktionen benötigt.

### Schritt 7 – SQLite mit FTS5 als Suchindex

**Ziel:** Die Volltextsuche bei wachsendem Vault beschleunigen, ohne Markdown als Datenquelle abzulösen.

Aufgaben:
- SQLite und FTS5 integrieren.
- Indexierte Dateien und Metadaten verwalten.
- Änderungen an Markdown-Dateien zuverlässig erkennen und indexieren.
- Initialen und vollständigen Neuaufbau des Index ermöglichen.
- Fehler und veraltete Indexeinträge behandeln.
- Suchservice so erweitern, dass die Schnittstelle für REST und MCP gleich bleibt.

**Abnahmekriterien:**
- Die Volltextsuche nutzt SQLite FTS5.
- Der Index kann nach einem Verlust aus dem Vault neu aufgebaut werden.
- Markdown-Dateien bleiben maßgeblich.
- Änderungen werden zeitnah erkannt oder über einen klar definierten Synchronisierungsschritt übernommen.

### Schritt 8 – Textnormalisierung und gezielte Bearbeitung

**Ziel:** Inhalte konsistenter machen und bestehende Notizen kontrolliert verändern.

Aufgaben:
- Regeln zur Textnormalisierung definieren und mit Beispielen testen.
- Gezieltes Ersetzen von Abschnitten implementieren.
- Eindeutigkeit der Zielstelle prüfen; bei mehreren Treffern abbrechen, statt unklar zu ändern.
- Vor Änderungen den bisherigen Inhalt sichern oder eine geeignete Wiederherstellungsmöglichkeit vorsehen.
- Änderungen nach dem Schreiben erneut lesen und verifizieren.

**Abnahmekriterien:**
- Normalisierung ist deterministisch und verändert Inhalte nicht ungewollt.
- Eine Änderung wird nur durchgeführt, wenn die Zielstelle eindeutig ist.
- Mehrdeutige oder nicht gefundene Abschnitte erzeugen einen klaren Fehler.
- Die gespeicherte Änderung kann anschließend verifiziert werden.

### Schritt 9 – Löschen und Archivieren

**Ziel:** Dateien kontrolliert entfernen oder aus dem aktiven Wissensbestand nehmen.

Die genaue Löschstrategie wird vor der Implementierung festgelegt. Permanentes Löschen und Archivieren sollen nicht stillschweigend gleichgesetzt werden.

Aufgaben:
- Verhalten für Löschen und optionales Archivieren festlegen.
- Pfad- und Existenzprüfung implementieren.
- Ergebnis und Fehler eindeutig zurückgeben.
- SQLite-Index und spätere Embeddings konsistent aktualisieren.
- Tests für nicht vorhandene Dateien und unzulässige Pfade ergänzen.

**Abnahmekriterien:**
- Löschoperationen betreffen ausschließlich erlaubte Dateien im Vault.
- Die API meldet den tatsächlichen Zustand.
- Der Suchindex enthält keine gelöschten Inhalte mehr.

### Schritt 10 – Lokale Embeddings und semantische Suche

**Ziel:** Inhalte auch dann finden, wenn die Suchanfrage andere Begriffe als die Markdown-Datei verwendet.

Aufgaben:
- Lokales Embedding-Modell über Ollama evaluieren, zunächst `nomic-embed-text` als Kandidat.
- Geschwindigkeit, Speicherbedarf und Qualität auf Atlas testen.
- Embeddings als abgeleitete Daten speichern und mit Dateiversion bzw. Inhaltssignatur verknüpfen.
- Neuindexierung bei Inhaltsänderungen ermöglichen.
- Semantische Suche zunächst getrennt von der Volltextsuche testen.
- Später beide Sucharten kombinieren, falls die Ergebnisse dadurch besser werden.

**Abnahmekriterien:**
- Die Embedding-Erzeugung benötigt keinen externen Dienst.
- Veraltete Embeddings werden erkannt und aktualisiert.
- Embeddings können aus Markdown-Inhalten neu aufgebaut werden.
- Die Qualität wird anhand konkreter Suchbeispiele bewertet, bevor semantische Suche zum Standard wird.

### Schritt 11 – Autorisierung und weitere Wissensfunktionen

**Ziel:** Delphi für spätere Nutzung durch weitere Clients oder Dienste vorbereiten.

Mögliche spätere Aufgaben:
- Authentifizierung und Autorisierung ergänzen.
- Schreib- und Löschrechte differenzieren.
- Automatische Sortierung und Klassifizierung von Notizen.
- Zusätzliche Werkzeuge für Wissensverknüpfung und Pflege des Wissensmodells.
- Überwachung, Backups und Wiederherstellung weiter ausbauen.

Dieser Schritt beginnt erst, wenn die vorherigen Funktionen stabil sind und konkrete Anforderungen vorliegen.

## 6. Tests und Qualitätssicherung

Mindestens folgende Testarten sind vorgesehen:

- **Unit-Tests:** Pfadvalidierung, Suchlogik, Eingabevalidierung und Fehlerfälle.
- **Integrationstests:** REST- und MCP-Aufrufe gegen einen temporären Test-Vault.
- **Dateioperationen:** Erstellen, Anhängen, erneutes Lesen und Verhalten bei existierenden Dateien.
- **Sicherheit der Dateipfade:** Relative Pfade, `..`, ungültige Pfade und Zugriffe außerhalb des Vaults.
- **Suchtests:** Treffer, keine Treffer, neue Dateien und geänderte Dateien.
- **Deployment-Test:** Containerstart, Vault-Mount, Healthcheck und Persistenz nach einem Image-Update.
- **LLM-Workflowtests:** Werkzeugauswahl, Umgang mit Fehlern und Verifikation von Schreibvorgängen.

Tests sollen einen eigenen temporären Vault verwenden und keine produktiven Wissensdateien verändern.

## 7. Bewusste Architekturentscheidungen

- **Eine Anwendung statt Microservices:** REST, MCP und Geschäftslogik werden gemeinsam ausgeliefert.
- **Markdown als Quelle der Wahrheit:** SQLite und Embeddings sind rekonstruierbare Hilfsdaten.
- **Direkte Dateisuche vor SQLite:** Das MVP bleibt klein und benötigt keinen Index-Synchronisierungsmechanismus.
- **Lokale Embeddings:** Keine Abhängigkeit von externen KI-APIs.
- **Keine Autorisierung im MVP:** Die Zugriffsschicht wird trotzdem so aufgebaut, dass Authentifizierung später ergänzt werden kann.
- **Schrittweise Migration:** Die Python-Dienste bleiben verfügbar, bis Delphi ihre Aufgaben nachweislich übernimmt.
- **Keine unnötigen Host-Ports:** Interne Kommunikation erfolgt über `brain_net`, soweit möglich.

## 8. Risiken und Gegenmaßnahmen

| Risiko | Gegenmaßnahme |
|---|---|
| LLM meldet einen Schreibvorgang fälschlich als erfolgreich | Werkzeugantworten eindeutig gestalten und Schreibvorgänge erneut lesen |
| Dateien werden versehentlich überschrieben | Erstellen und Anhängen klar trennen; vorhandene Dateien beim Erstellen ablehnen |
| Pfadmanipulation greift außerhalb des Vaults zu | Pfade zentral validieren und den Vault-Zugriff kapseln |
| SQLite-Index weicht vom Vault ab | Neuaufbau ermöglichen und Index als abgeleitet behandeln |
| Embeddings sind auf Atlas zu langsam | Erst nach Messungen entscheiden und bei Bedarf Modell oder Umfang anpassen |
| Deployment unterbricht bestehende Workflows | Python-Dienste bis zur erfolgreichen Umstellung behalten |
| Fehlende Autorisierung wird später zum Problem | Zugriff auf das interne Docker-Netzwerk begrenzen und Authentifizierung als spätere Erweiterung vorsehen |

## 9. Konkrete Startreihenfolge

1. GitHub-Repository `delphi` anlegen.
2. .NET-10-Projekt `Delphi.Api` und Testprojekt erstellen.
3. REST-Grundgerüst, Health-Endpunkt und MCP-Adapter implementieren.
4. Vault-Pfad konfigurierbar machen und Lesezugriff ergänzen.
5. Erstellen und Anhängen implementieren.
6. Direkte Volltextsuche ergänzen.
7. Tests und Docker-Build vervollständigen.
8. GitHub Actions und Deployment auf Atlas einrichten.
9. Open WebUI auf Delphi umstellen.
10. Python-Such- und Schreibdienste nach erfolgreicher Abnahme abschalten.

## 10. Definition of Done für das MVP

- [ ] Delphi als .NET-10-Anwendung lokal und in Docker läuft.
- [ ] REST und MCP dieselben fachlichen Funktionen bereitstellen.
- [ ] Dateien und Verzeichnisse im Vault aufgelistet werden können.
- [ ] Markdown-Dateien gelesen werden können.
- [ ] Neue Markdown-Dateien erstellt und bestehende Dateien ergänzt werden können.
- [ ] Volltextsuche direkt in den Markdown-Dateien funktioniert.
- [ ] Zugriffe außerhalb des Vaults abgewiesen werden.
- [ ] Automatisierte Tests für zentrale Erfolgs- und Fehlerfälle bestehen.
- [ ] GitHub Actions das Image baut und auf Atlas deployt.
- [ ] Open WebUI die Delphi-Werkzeuge erfolgreich verwendet.
- [ ] Die bisherigen Python-Dienste erst nach erfolgreicher Umstellung abgeschaltet werden.

---

**Nächster konkreter Schritt:** Repository `delphi` erstellen und das minimale .NET-10-Grundgerüst mit REST, MCP, Tests und Docker aufsetzen. Danach wird jede Funktion einzeln implementiert und abgenommen.
