# Project Overview – BookScanner

## Ziel der Anwendung
**BookScanner** ist eine Windows-WPF-Anwendung zur persönlichen Bücherverwaltung.  
Die App dient der privaten Organisation von Büchern und ist **nicht** als öffentlicher Katalog oder Verkaufssystem gedacht.

Die Anwendung verwaltet **beliebig viele Bücherlisten**.  
Listen sind logisch gruppiert und können vom Nutzer erweitert werden.

---

## Listen-Konzept

### Listen
- Es können **beliebig viele Listen** existieren.
- Jede Liste besitzt:
  - einen **Namen**
  - eine **Kategorie**
- Bücher können in genau **eine Liste** eingeordnet werden (v1).

### Kategorien
- Kategorien dienen der groben fachlichen Einordnung von Listen.
- Anfangskategorien (vordefiniert):
  - **Datenbank** (z. B. gelesene / vorhandene Bücher)
  - **Wunschliste** (vorgemerkte Bücher)
- Weitere Kategorien sind zukünftig möglich, aber nicht zwingend für v1.

### Tags
- Bücher können mit **frei definierbaren Tags** versehen werden.
- Tags dienen ausschließlich der inhaltlichen Beschreibung (z. B. Genre, Thema).
- Tags haben **keine fachliche Logik** und beeinflussen keine Workflows.
- Sortieren/Filtern nach Tags ist **nicht zwingend vorzuplanen**.

---

## Grundprinzip
- Bücher werden möglichst **automatisiert erfasst**, nicht manuell gepflegt.
- Externe Buchdaten werden über **mehrere unabhängige Datenquellen** bezogen.
- Fällt eine Quelle aus oder liefert keine Treffer, wird auf weitere Quellen zurückgegriffen.
- Kostenpflichtige Dienste sind **optional** und vollständig deaktivierbar.

---

## Zentrale Anwendungsfälle (Use Cases)

### UC-01: Buch zu einer Liste hinzufügen (Titel/Autor)
1. Nutzer wählt eine **Zielliste**.
2. Nutzer gibt **Titel** und optional **Autor** ein.
3. Die Anwendung führt eine Buchsuche über konfigurierte Datenquellen durch.
4. Treffer werden gesammelt, zusammengeführt und bewertet.
5. Der beste Treffer wird vorgeschlagen.
6. Nach Bestätigung wird das Buch der gewählten Liste hinzugefügt.
7. Optional werden **Tags** vergeben.

---

### UC-02: Buch zu einer Liste hinzufügen (Bild-Scan)
1. Nutzer wählt eine **Zielliste**.
2. Nutzer wählt ein oder mehrere Bilder (z. B. Cover, Rückseite).
3. Optional:
   - Eine Bild-Analyse extrahiert ISBN/Titel/Autor (falls aktiviert).
4. Mit den extrahierten oder manuellen Suchdaten wird eine Buchsuche gestartet.
5. Treffer aus mehreren Quellen werden zusammengeführt und bewertet.
6. Der beste Treffer wird vorgeschlagen.
7. Nach Bestätigung wird das Buch der gewählten Liste hinzugefügt.
8. Optional werden **Tags** vergeben.

Hinweis:
- Ist die Bild-Analyse deaktiviert, erfolgt direkt eine textbasierte Suche.
- Die App muss ohne Bild-Analyse vollständig funktionsfähig bleiben.

---

### UC-03: Bücherlisten anzeigen und pflegen
- Anzeige aller vorhandenen Listen, gruppiert nach **Kategorie**.
- Anzeige der Bücher einer Liste.
- Entfernen einzelner Bücher aus einer Liste.
- Bearbeiten von:
  - Listennamen
  - Kategorienzuordnung
  - Tags eines Buches

Es besteht **keine Pflicht**, folgende Funktionen vorzusehen:
- Sortieren
- Filtern
- Paging

---

### UC-04: Listen verwalten
- Erstellen neuer Listen mit:
  - Name
  - Kategorie
- Umbenennen bestehender Listen.
- Löschen leerer Listen.
- Kategorien sind logisch, nicht technisch zwingend.

---

## Externe Services & APIs (präzise benannt)

### Buchdaten-Lookups (kostenfrei)
Diese Dienste liefern bibliografische Metadaten:

- **Open Library API**
  - Primäre Quelle
  - ISBN-Lookup
  - Titel-/Autor-Suche
  - Kein API-Key erforderlich

- **Deutsche Nationalbibliothek (DNB, SRU)**
  - Fokus auf deutschsprachige Titel
  - Öffentliche bibliografische Daten
  - Fallback-Quelle

- **Google Books API** (optional)
  - Gute Trefferquote
  - API-Key erforderlich
  - Nutzung optional und konfigurierbar

---

### Bild-Analyse / Vision (optional, kostenpflichtig)
- **OpenAI Vision API**
  - Extrahiert ISBN, Titel, Autor aus Buchbildern
  - Wird nur genutzt, wenn:
    - explizit aktiviert
    - sinnvoll (z. B. keine ISBN erkennbar)
  - Muss vollständig deaktivierbar sein

---

## Such- und Fallback-Strategie
- Die Anwendung kennt eine **konfigurierbare Reihenfolge** aktiver Buchdatenquellen.
- Quellen werden:
  - nacheinander (Fallback)
  - oder gesammelt (Aggregation)
  abgefragt.
- Treffer werden:
  - zusammengeführt
  - dedupliziert
  - bewertet
- Exakte ISBN-Treffer haben immer höchste Priorität.

---

## Konfiguration & Feature-Flags
- Aktivierung/Deaktivierung einzelner Datenquellen
- Aktivierung/Deaktivierung der Bild-Analyse
- Maximale Trefferanzahl
- Timeouts für externe Abfragen

Die Anwendung muss stabil bleiben, auch wenn:
- alle externen Quellen fehlschlagen
- kostenpflichtige Dienste deaktiviert sind

---

## UI-Prinzipien
- Reines MVVM-Pattern
- Keine Fachlogik im UI
- Übersichtliche Darstellung von:
  - Listen
  - Kategorien
  - Bucheinträgen
- Dialoge für:
  - Suche
  - Scan
  - Listenverwaltung
- Fokus auf Klarheit, Wartbarkeit und Erweiterbarkeit

---

## Architektur-Leitlinien
- Fachlogik ist vollständig von der UI getrennt.
- Externe Services sind austauschbar.
- Die App ist testbar ohne externe APIs.
- Erweiterungen (Persistenz, neue Kategorien, neue Quellen) sind vorgesehen, aber nicht erzwungen.

---

_Ende der Project Overview_