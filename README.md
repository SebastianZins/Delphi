# Delphi

**Lokaler Wissensdienst für Markdown, REST und MCP**  
Repository: `delphi` · .NET 10 · Anwendung: `Delphi.Api` · Container: `brain-delphi`

## Ziel
Delphi ersetzt die bisherigen Python-Dienste für Markdown-Suche und Schreibzugriffe. REST und MCP sind zwei Schnittstellen zur **gleichen Geschäftslogik**. Markdown bleibt die maßgebliche Datenquelle; SQLite/FTS5 und Embeddings sind später rekonstruierbare Indizes.

## Architektur
```text
Open WebUI / LLM -- MCP --+
                          +--> Delphi.Api (.NET 10) --> Markdown-Vault
Tests / andere Clients REST+                          --> später SQLite/FTS5
                                                     --> später Embeddings via Ollama
```

## Umgebung
- Server: Atlas, Ubuntu
- Docker-Netz: `brain_net`
- bisheriger Vault-Kandidat: `/opt/brain/vault-test` (**vor produktivem Einsatz bestätigen**)
- Python-Dienste: `brain-markdown-search`, `brain-vault-write-mcp`
- Deployment: GitHub Actions; genauer Mechanismus (Runner/Pull) vor Schritt 05 festlegen.

## MVP
- Dateien und Ordner auflisten
- Markdown lesen
- neue Markdown-Datei erstellen, ohne bestehende Datei zu überschreiben
- Text anhängen
- direkte Volltextsuche ohne Index
- REST und MCP für dieselben Funktionen
- Tests und Docker-Build

Nicht im MVP: gezielte Bearbeitung, Löschen, Normalisierung, SQLite, Embeddings, automatische Sortierung und Autorisierung.

## Roadmap
| Schritt | Inhalt | Status |
|---|---|---|
| 01 | .NET-Grundgerüst, REST, MCP, Healthcheck | 🟡 Geplant |
| 02 | Vault lesen und auflisten, Pfadgrenzen | 🟡 Geplant |
| 03 | Erstellen und Anhängen | 🟡 Geplant |
| 04 | Direkte Volltextsuche | 🟡 Geplant |
| 05 | CI/CD und Deployment | 🟡 Geplant |
| 06 | Open WebUI umstellen, Python ablösen | 🟡 Geplant |
| 07 | SQLite/FTS5 | 🟡 Später |
| 08 | Normalisierung und gezielte Bearbeitung | 🟡 Später |
| 09 | Löschen/Archivieren | 🟡 Später |
| 10 | lokale Embeddings/semantische Suche | 🟡 Später |
| 11 | Autorisierung und weitere Funktionen | 🟡 Später |

Status: 🟢 abgeschlossen · 🟡 geplant/offen · 🔴 blockiert/Problem.

## Leitlinien
1. Markdown ist die Quelle der Wahrheit.
2. Kein doppelter Fachcode für REST und MCP.
3. Dateioperationen dürfen den konfigurierten Vault nicht verlassen.
4. Tests arbeiten nur in temporären Test-Vaults.
5. Keine Secrets im Repository, Image oder Logs.
6. Keine unnötigen Host-Ports veröffentlichen.
7. Python-Dienste erst nach erfolgreichem End-to-End-Test abschalten.
8. Erfolg bei Schreibvorgängen nur melden, wenn die Operation tatsächlich erfolgreich war; wichtige Schreibvorgänge durch erneutes Lesen verifizieren.

## Definition of Done für das MVP
- [ ] Lokal und in Docker baubar und startbar
- [ ] REST und MCP verwenden dieselben Services
- [ ] Auflisten, Lesen, Erstellen, Anhängen und Suchen funktionieren
- [ ] Pfad-Traversal und Zugriff außerhalb des Vaults werden abgewiesen
- [ ] Unit- und Integrationstests bestehen
- [ ] GitHub Actions baut/testet und deployed nachvollziehbar
- [ ] Open WebUI nutzt Delphi erfolgreich
- [ ] Python-Dienste erst danach abschalten

## Learn more
[Wiki](https://github.com/SebastianZins/Delphi/wiki/Delphi)

## Referenzen
- [.NET 10](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-10/overview)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/)
- [MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk)
- [MCP-Dokumentation](https://modelcontextprotocol.io/)
- [Docker Compose](https://docs.docker.com/compose/)
- [GitHub Actions](https://docs.github.com/actions)
- [GitHub Container Registry](https://docs.github.com/packages/working-with-a-github-packages-registry/working-with-the-container-registry)
- [SQLite FTS5](https://www.sqlite.org/fts5.html)
- [Ollama API](https://docs.ollama.com/api)
- [Ollama Embeddings](https://docs.ollama.com/capabilities/embeddings)

Links sind Einstiegspunkte. SDK-Versionen und aktuelle API-Verträge vor der Implementierung prüfen.
