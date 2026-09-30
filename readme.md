# Mixed Reality – Unity Übung 1

## Projektbeschreibung

Ziel dieses Projekts ist die 3D-Simulation des Sonnensystems in Unity.
Das Modell enthält die Sonne sowie die acht Planeten und soll ihre Masse und Gravitation korrekt kalkulieren bzw. berücksichtigen.

## 1. Schritt: Projekt erstellen

- Ich habe ein neues 3D-Projekt in Unity erstellt.
- Projektname: wie in der Angabe spezifiziert mit mr-ue01-Pospischek
- Verwendete Unity-Version: 6.6

## 2. Assets

Für die Darstellung der Planeten wurde das Asset
"Planets of the Solar System 3D" verwendet: https://assetstore.unity.com/packages/3d/environments/planets-of-the-solar-system-3d-90219

Das Asset wurde über den Unity Package Manager in das Projekt importiert.

## 3. Aufbau des Sonnensystems

Ich habe die unter Assets verfügbaren Prefab Elemente in die Szene reingezogen. Dann habe ich ein Script erstellt, dass die 2 Eigenschaften der Planeten definiert: 
- radius
- distanceToSun

Danach wurde dieses Script als Komponente den Assets hinzugefügt und die 2 Werte basierend auf der Angabe und einem jeweils so gewählten Faktor eingefügt. 

Der Radius wurde mit dem Faktor 10^3 umgerechnet, da laut der Angabe die Erde eine Kugel mit einem Durchmesser von 6,3 sein soll, aber einen Durchmesser von 6,371 km hat. Für die Sonne soll eine Kugel mit 696 Einheiten Durchmesser gewählt werden, während diese einen Durchmesser von 696,342 km hat. Für die Erde wurde somit 6,371 gewählt und für die Sonne 696,342 um die Kommawerte nicht zu verlieren.

Für die Distanz wurde der Faktor 10^6 verwendet. Dies wird in der Angabe mit der X-Koordinate der Erde mit 1510 erklärt, wobei die Erde einen Abstand von 151480000 km zur Sonne im Zentrum bei (0,0,0) hat. Damit hier die Daten aber korrekt sind, wurde der Wert 1514,8 für die Erde gewählt.

Für die Sonne wurden auch noch im Inspektor die 3 Koordinaten für "Scale" von dem Standard des Assets mit 2 auf 1 umgestellt, damit die Sonne nicht doppelt so groß dargestellt wird. 

## 4. Kamera Einstellung

Dann habe ich durch experimentieren, dass die Kamera bei einem FOV von 75° bei einer Position von (600,50,0) bei einer Rotation um 90 in der X-Koordinate um eine Vogelperspektive zu erreichen der rechte Teil der Sonne und der nächste Planet Merkur sichtbar sind. Somit habe ich dies als Anfangspunkt gewählt für Kamera und das Skript CameraMovement.cs geschrieben, mit dem die Kamera mit den Pfeil-Tasten in die X- und Y-Richtung bewegt werden. Das bedeutet, dass mit der rechten Pfeiltaste entlang der X-Achse die Planeten entdeckt werden können. Die größeren Planten können von weiter weg betrachtet werden, in dem man mit der Pfeiltaste nach oben die Y-Position der Kamera ändert. 

Zuletzt habe ich noch ein Limit eingeführt, weil eine negative Y-Position keinen Sinn macht. Dies würde die Kamera in die untere Hälfte der Planeten gewegen, die Kugeln entlang der X-Achse sind. Auch eine Bewegung der Kamera in die negative X-Richtung über die Sonne hinaus ist sinnlos, weshalb ich hier -1000 gewählt habe. 

## 5.1 Umlaufbahnen

Für den 2. Teil der Aufgabe sollen die Umlaufbahnen simuliert werden. Dafür habe ich jedem der Planeten (außer der Sonne) im Unity Inspector einen Rigid Body hinzugefügt und "Use Gravity" deaktiviert, wie es in der Angabe steht. Im Script Planet.cs habe ich dann die Masse als Wert eingefügt.

Es wurden erneut die Zahlen aus der Tabelle in der Angabe übernommen und im Inspektor eingestellt. Die Erde hat zum Beispiel bei der Variable den Wert 1, Neptun hat eine Masse von 17,15.

Im Script setze ich dann die Masse basierend auf dem Input im Inspektor, wenn ein Rigid Body existiert. Dies ist der Fall, weil ich dies im Inspektor unter "Add Component" hinzugefügt habe. 

## 5.2 Gravitation

Das Gravitationszentrum des Sonnensystems soll die Sonne sein. Daher wird die Variable sun dem file Planet.cs hinzugefügt. Somit haben die Assets nun 4 Inputfelder im Inspektor und das Asset der Sonne wurde allen Planeten bis auf der Sonne selbst hinzugefügt.

Nun soll die Gravitation immer in Richtung der Sonne wirken. Daher kann die Größe F wie in der Angabe beschrieben als gravitationalForce in void Start() berechnet werden. Der Vektor in der X-Z-Ebene, der vom Planeten aus zur Sonne zeigt, muss aber kontinuierlich berechnet werden. Nach dem AI-Hinweis habe ich dafür FixedUpdate() verwendet. In der Funktion wird, wenn es sich nicht um die Sonne handelt, der Vektor vom Planeten zur Sonne berechnet mit der Differenz der Koordinaten der Sonne und der Planetenkoordinaten. Von dem Vektor wird dann der Normalvektor mit einer Länge 1 gebildet und der Faktor F multipliziert. Somit wird die Gravitationskraft F in die Richtung zu der Sonne ausgeübt indem sie dem Rigid Body hinzugefügt wird. 

## 5.3 Geschwindigkeit

Da sich die Planeten auf der X-Achse befinden und innerhalb der X-Z-Ebene bewegen sollen, muss die Geschwindigkeit in die positive Z-Richtung erfolgen um der Gravitation entgegen zu wirken. Somit bekommt der Rigid Body eine Velocity mit der Z-Koordinate berechnet nach der Formel in der Angabe. 

Die Gravitationskonstante G wird mit 100 als private float hinzugefügt. Außerdem wird mit r der Abstand zwischen der Sonne und dem Planeten benötigt, was schon vorhin mit der Variable distanceToSun implementiert wurde. Zuletzt wird noch die Masse der Sonne als fixe Variable hinzugefügt und so die Z-Koordinate berechnet und dann der gesamte Vektor gesetzt. 

## 5.4 Umlaufbahnen

Für die Umlaufbahnen soll laut Angabe die Komponente Trail Renderer verwendet werden, die allen Planeten im Inspector hinzugefügt wurde. Die Zeit wurde von 5 auf 50 erhöht, damit die Bahnen länger sichtbar sind. Dann wurde noch ein Material unter Assets angelegt und im Inspektor dem Trail Renderer hinzugefügt, damit die Umlaufbahnen weiß dargestellt werden.

## 6. Hintergrund

Zuletzt wurde noch das Skybox Material, das im Asset Package enthalten ist, dem Environment als Skybox Material hinzugefügt. 

## 7. Verwendung von AI

Ich habe ChatGPT für folgende Aufgaben verwendet: 
- Für ein Tutorial zum erfolgreichen Import des Asset Sets in das Projekt 
- Für die Verwendung von Mathf.Max, weil ich diesen Befehl einfach nicht gekannt habe und zuerst unschöne if-Schleifen verwendet habe. 
- Für die Verwendung von void FixedUpdate(), für physikalische Berechnungen, da die Methode in festen Zeitintervallen aufgerufen wird und die Physiksimulation dadurch unabhängig von der Bildrate aktualisiert werden kann.
- Für "Trouble Shooting", als die Umlaufbahnen in lila angezeigt wurden, obwohl die Farbe weiß eingestellt war. Durch ChatGPT wusste ich dann, dass ein Material dafür erstellt werden musste. 
- Für eine Anleitung, wie korrekt das Skybox Material unter Lightning eingestellt wird. 
- Grundsätzlich für Feedback zu allen erstellten .cs-Files gemeinsam mit dem automatischen Clean-up und Hinweisen, die in Visual Studio zur Anwendung kommen.