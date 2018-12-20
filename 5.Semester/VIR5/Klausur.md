# Klausur 2018

Folien:
Gründe für Einsatz von Docker (CD, DevOps, Microservices), Szenario: erklären welche bereiche relevant sind wo kann man docker einsetzen

Images, Files, best practices, unterschied container und vm,
einfaches dockerfile schreiben, cmds aus den übungen/folien
docker-compose wie funktioniert es

übungen noch einmal machen
datamangement wichtig


networking: wie geht portbinding, keine details
jenkins keine relevanz  

keine unterlagen

---

## Ausarbeitung

### Theorie

#### Gründe für Einsatz von Docker (CD, DevOps, Microservices)

- `Was ist Continuous Delivery?`  
Eine Vorgehensweise bei der sichergestellt wird das jede Änderung am System releasable ist und das ein release zu jeder Zeit erstellt werden kann.

- `Was ist ein MicroService?`  
Bei einem MicroService wird eine einzelne Applikation entwickelt mit einer kleinen Anzahl von services. Jeder service läuft in einem eigenen Prozess und kommuniziert mithilfe von lightweight mechanismen. Diese services können unabhängig voneinander deployed werden (meist automatisch).

- `Was ist DevOps?`  
DevOps ist eine software developement methode die kommunikation, kollaboration, integration, automation und kooperations messung zwischen software developers und anderen it professionals ermöglicht.
Das Ziel dieser Methode ist es einer Organisation rapide software entwicklung zu ermöglichen.

#### Docker Allgemein: erklären welche Bereiche relevant sind wo kann man docker einsetzen

- `Was ist Docker bzw. was macht Docker?`  
Docker ist eine container Technologie die sogennante `operating-system-level virtualization` kurz "containerization" betreibt. Dieser Container können beliebige Applikationen enthalten.

- `Was sind Docker Container?`
  - Docker container wrappen eine software in ein komplettes filesystem das alles hat was benötigt wird um diese software zu starten (Code, runtime, system tools, system libraries). -> Garantie das die Software immer gleich läuft egal in welcher Umgebung.
  - Container laufen auf einer Maschine und benutzen denselben `OS kernel`, dadurch sind sie leichtgewichtiger als virtuelle maschinen
  - Starten sofort und benutzen weniger RAM
  - Container benutzen ein `layered filesystem` und sharen gleiche files und benutzen so weniger speicher

- `Wie kann man Docker einsetzen?`  
Docker kann eingesetzt werden um continuous integration und continuois deployment zu ermöglichen. Zusätzlich erlaubt es Docker den devlopement lifecycle zu streamlinen und mit standardisierten Umgebungen zu arbeiten.
Dadurch das Container einfach ausgetauscht werden können und auch unabhängig von einander agieren können, wird so die Entwicklung um ein vielfaches verschnellert.

  Use docker to push applications into a test environment to execute automatic/manual tests. After everything is fixed the image can be pushed to the production environment.

- `Was ist ein Docker Image? Und was ist der Unterschied zu einem Container?`  
Ein Image ist eine Art Snapshot von einem Container, wie eine angehaltene VM. Dieses Image besteht aus mehreren Layern die übereinander gestackt sind.  
Ein Container ist die Laufzeit Instanz eines Images.

#### Unterschied Container und VM

- `Was ist der Unterschied zwischen VMs und Container?`  
Container und VMs haben eine ähnliche Ressourcen Isolation funktionieren aber anders.  

- Virtual Machines  
VMs bauen darauf auf ein Hardware based Environment zu virtualisieren, also wird neben dem OS auch die Hardware simuliert.

- Container  
Container simulieren und sind eine Abstraktion auf dem app layer der code und dependencies zusammenpackt. Mehrere Container können auf der selben Maschine laufen und benutzen denselben OS kernel. Laufen als isolierter Prozess.

#### Dockerfile

- `Was ist ein Dockerfile? Beispiel dockerfile?`  
Ein Dockerfile enthält Instruktionen für den Zusammenbau eines Images. Zb. Files oder Directories erstellen, Packages oder Software konfigurieren bzw installieren, Metadaten zum Image schreiben.  

    Python Dockerfile: 

    ```dockerfile
    FROM ubuntu:latest
    MAINTAINER YOU "you@here.com"

    RUN apt-get update
    RUN apt-get install -y \ 
        python \ 
        python-pip \
        wget
    RUN pip install Flask

    ADD hello.py /home/hello.py

    WORKDIR /home
    ```

- `Geben Sie 6 Beispiel Instruktionen in einem Dockerfile an und erklären Sie diese.`
  - From  
    Basis Image das benutzt wird
  - RUN  
    Ein Kommando kann ausgeführt werden
  - ADD, COPY  
    Ressourcen hinzufügen
  - CMD, ENTRYPOINT  
    Startup Kommandos
  - EXPOSE  
    Freigegebene Ports bekanntgeben
  - VOLUME  
    Volume bekannt geben

- `Was sind im Environment variables in Zusammenhang mit einem Dockerfile? Wie kann eine Variable verwendet werden?`  
ENV oder ARG, können in Instruktionen mit $ oder ${} verwendet werden.

#### Docker Compose

- `Wie können mehrere Container definiert und gestartet werden?`  
Mit Docker Compose. In einem Compose file wird jeder Service konfiguriert. Anschließend kann mit `docker-compose build` und `docker-compose up -d`.

#### Docker Networking

- `Was ist eine Bridge? Wie kann eine Bridge erstellt werden?`  
Aus Netzwerksicht: Ein Bridge Network ist ein Link Layer Device das traffic zwischen Netzwerk Segmenten weiterleitet.
Bei Docker können Container die zur selben Bridge connected sind miteinander kommunizieren. Dabei sind die Container von anderen Containern die nicht mir der Bridge verbunden sind isoliert.
Jeder Container ist standardmäßig mit der `Default Bridge` verbunden.

  Beispiel:
  ```dockerfile
  docker network create -d bridge hello
  CONTAINER=$(docker container run -d --name world alpine /bin/sh -c "while true;"
  docker network connect hello world
  docker container run --rm --network hello alpine /bin/sh -c "ping -c 1 world"
  docker container stop world
  docker container rm world
  docker network rm hello
  ```

- `Wie kann ein Container Port freigegeben werden? Zeigen sie zwei Möglichkeiten.`
  - Im Dockerfile mit EXPOSE
  - Beim starten des containers:
    >docker container run -d -p 80:8080 IMAGENAME
