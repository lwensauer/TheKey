# TheKey
Programmieraufgabe

Aufgabe:
Erstelle ein Full Stack System Frontend / Backend mit folgender Aufgabestellung:
1. Das Backend ruft zyklisch (alle paar Sekunden) die Blogbeiträge von der Seite internate.org ab (über die Wordpress API - https://developer.wordpress.org/rest-api/reference/posts/)
2. Das Backend verarbeitet die Blogbeiträge zu einer einfachen Word Count Map ({“und”: 5, “der”: 3, ...})
3. Das Backend sendet nach der Verarbeitung die Map per WebSocket an das Frontend
4. Das Frontend zeigt die neuen Beiträge an und aktualisiert sich selbstständig neu bei neuen Daten.

Bonuspunkte:
- Eventgetriebene Verarbeitung
- Aktualisierung im Frontend nur bei tatsächlich neuen Blogbeiträgen - nicht immer komplett neu
- Microservice-Architektur

Programmiersprachen:
- Backend in einer gängigen, modernen Programmiersprache (zB Scala, Java, C#)- Frameworks dürfen gerne genutzt werden
- Frontend in Javascript / Typescript React oder Angular
- Datenspeicherung gerne in-memory

Was wollen wir sehen:
- hohe Codequalität
- Testabdeckung
- Production-ready code - so wie du auch eine Aufgabe hier in der Firma lösen würdest
- Abgabe bitte als github mit Anweisungen wie wir es testen können innerhalb von 1 Woche
