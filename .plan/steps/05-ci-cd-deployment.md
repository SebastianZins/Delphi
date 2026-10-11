# Schritt 05 – GitHub Actions und Deployment

## Ziel
Delphi reproduzierbar bauen, testen, als Image veröffentlichen und auf Atlas aktualisieren.

## Vorab klären
- Welcher Branch deployt?
- Wird die GitHub Container Registry verwendet?
- Kann Atlas selbst pullen oder braucht es einen self-hosted Runner bzw. einen Pull-basierten Deploy-Mechanismus?
- Wie werden Registry-Zugangsdaten sicher hinterlegt?
- Wie sieht Healthcheck und Rollback aus?

Nicht voraussetzen, dass GitHub-hosted Runner Atlas direkt erreichen können.

## Pipeline
1. Checkout und .NET-10-SDK einrichten
2. Restore, Release-Build und Tests
3. Docker-Image bauen
4. Image eindeutig mit Commit-SHA taggen und veröffentlichen
5. Deployment nur bei erfolgreichem Build/Test
6. Container aktualisieren und Healthcheck prüfen
7. Logs und Rückfallmöglichkeit dokumentieren

## Docker / Persistenz
- Vault als persistentes Bind-Mount/Volume einbinden, nie ins Image kopieren.
- Dienst an `brain_net` anschließen.
- Keine unnötigen Host-Ports veröffentlichen.
- Konfiguration über Umgebung; Secrets nicht ins Repository oder Image.
- Passende Dateirechte und Container-UID/GID prüfen.
- Eindeutige Image-Tags statt ausschließlich `latest`.

## Migration
Python-Dienste bleiben parallel aktiv, bis Delphi erfolgreich End-to-End getestet wurde. Erst dann Open WebUI umstellen und alte Dienste abschalten.

## Abnahme
- [ ] CI baut und testet
- [ ] fehlerhafte Tests verhindern Deployment
- [ ] Atlas bezieht das Image zuverlässig
- [ ] Vault bleibt bei Updates erhalten
- [ ] Healthcheck erfolgreich
- [ ] Rollback ist nachvollziehbar

## Referenzen
- https://docs.github.com/actions
- https://docs.github.com/packages/working-with-a-github-packages-registry/working-with-the-container-registry
- https://docs.docker.com/compose/
