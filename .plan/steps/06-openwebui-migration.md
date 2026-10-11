# Schritt 06 – Open WebUI umstellen und Python ablösen

## Ziel
Open WebUI verwendet Delphi als MCP-Anbieter. Python-Dienste werden erst nach erfolgreicher Abnahme abgeschaltet.

## Vorgehen
1. MCP-Endpunkt aus dem Open-WebUI-Container erreichbar machen.
2. Kompatiblen MCP-Transport für die installierte Open-WebUI-Version bestätigen.
3. Vorherige Python-MCP-Konfiguration sichern.
4. Tool-Namen, Beschreibungen, Eingaben, Ausgaben und Fehlerfälle prüfen.
5. Open WebUI auf Delphi umstellen.
6. End-to-End-Testfälle ausführen.
7. Bei Problemen zurückschalten; Python-Dienste erst nach stabiler Beobachtungsphase abschalten.

## Testfälle
- Vault auflisten
- vorhandene Testdatei lesen
- bekannten Begriff suchen
- Testnotiz erstellen
- Datei erneut lesen
- Inhalt ergänzen und erneut lesen
- vorhandenen Dateinamen und Suchbegriff ohne Treffer testen

Alle Schreibtests verwenden einen dedizierten Testpfad. Keine produktiven Notizen für Smoke-Tests verändern.

## Werkzeugbeschreibungen
Beschreiben, wann ein Tool zu verwenden ist, welche Parameter erforderlich sind und was bei existierenden/fehlenden Dateien passiert. Erfolg und Fehler müssen für das Modell eindeutig sein. Das LLM darf nicht aus einer Absicht auf einen erfolgreichen Schreibvorgang schließen.

## Abnahme
- [ ] Open WebUI erkennt Delphi-Tools
- [ ] Lese- und Suchaufrufe funktionieren
- [ ] Schreibvorgänge werden erneut gelesen
- [ ] Fehlerfälle korrekt behandelt
- [ ] Rückfallmöglichkeit bleibt bis zur Abnahme erhalten
- [ ] alte Python-Dienste erst danach abgeschaltet
