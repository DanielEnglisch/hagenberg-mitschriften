- [Assemblys](#assemblys)
    - [Aufbau con Assemblys](#aufbau-con-assemblys)
    - [Aufbau von Module](#aufbau-von-module)
        - [Metadaten](#metadaten)
    - [Arten von Assemblys](#arten-von-assemblys)
        - [Private Assemblys](#private-assemblys)
        - [Shared Assemblys](#shared-assemblys)
        - [Digitales Signieren von Assemblys](#digitales-signieren-von-assemblys)
    - [Resourcen](#resourcen)
    - [Das Assembly Manifest](#das-assembly-manifest)
    - [Global Assambly Cache (GAC)](#global-assambly-cache-gac)
    - [.NET-Core: Framework-depenedent Deployment](#net-core-framework-depenedent-deployment)
    - [.NET-Core: Self-contained Deployment](#net-core-self-contained-deployment)

# Assemblys

Zusammenfassung von IL-Code, Metadaten und Resourcen in Assembly Dateien (.dll). Resources können eingebettet oder referenziert werden. Code und Metadaten können auf mehrere Module verteilt werden. Enthält Manifest, das den Inhalt des Assemblys beschreibt.

## Aufbau von Assemblys

Besteht aus mehreren Modulen, damit selektiv benötigte Daten geladen werden können.

## Aufbau von Module

* **PE/COFF**: Standard Windows Object Format
* **CLR-Header**: Versionsnummer, Entrypoint, Referenz auf Metadaten
* **IL/Maschinencode**: Kann auch vorkompiliert sein
* **Resourcen**
* **Metadaten**: Enthaltenen und Referenzierte Typen

### Metadaten

Bestehen aus Definitionstabellen:

* **TypeDef**: definierte Typen
* **Field**: Typ unud Attribute (Zugriffsrechte,...) der Datenkomponenten.
* **Method**: Signatur, Attribute, Parameter der Methode

Und aus Referenzmodellen:

* **AssemblyRef**: Verweis auf referenzierte Assemblies
* **ModuleRef**
* **TypeRef**

## Arten von Assemblys

### Private Assemblys

Werden nur von einer Anwendung verwendet und werden nicht über der Registry anderen Anwendungen zur Verfügung gestellt. Werden durch kopieren installiert (.dll liegt im Verzeichnis der .exe). Kann in Unterverzeichnissen liegen bzw deren Pfad durch eine .exe.config Datei festgelegt werden.

### Shared Assemblys

Werden anwendungsübergreifend verwendet. Es wird ein Strong Name zugewiesen, der auf die verwendete Version hinweist. (DLL Hell wird dadurch vermieden). String Name: Hauptversion.Nebenversion.BuildNr.LfdN.

Diese Assemblys werden im Global Assembly Cache (GAC) gespeichert und verwaltet. Es können somit mehrer Versionen der gleichen Assembly gleichzeitig geladen und verwendet werden.
**Jede Anwendung ist fest an bestimmte Assembly-Versionen gebunden.**

### Digitales Signieren von Assemblys

Zur feststellung von veränderten .dll Dateien. Ist seit .NET 4.0 standardmäßig deaktiviert.

## Resourcen

Um Resourcen plattformunabhängig verwenden zu können werden sie in Binärform gespeichert und in den Manifesten referenziert. Verwenden einer generischen .resource Endung. Für sprachdateien können pro Sprache eigene Resource Assemblys verwendet werden.

## Das Assembly Manifest

Enthält:

* Referenzierte Assemblys
* Public Key, Versionsnummer
* Liste der enthaltenen Module
* Exportierte Typen
* Assembly Art (Library oder .exe)

## Global Assambly Cache (GAC)

Zentraler Speicherort für Shared Assemblys. Enthält Komponenten in verschiedenn Versionen und Architekturausführungen (x64/x86, etc.).
GAC_MSIL: Architekturunabhängiger Code.

## .NET-Core: Framework-depenedent Deployment

Verwendung einer geteilten Installation von .NET. Vermeidung von Verdoppelung aber dafür können spezielle Anforderungen nicht umgesetzt werden, da man mit der installierten Variante auskommen muss.

## .NET-Core: Self-contained Deployment

Für jede Applikation eine .NET Version die nicht mit anderen geteilt wird. Mehrere Runtimes können nebeneinander existieren, Speicher wird dafür verdoppelt.

TreeShaking: Nur die wirklich verwendeten Teile des Frameworks werden gepackt.
