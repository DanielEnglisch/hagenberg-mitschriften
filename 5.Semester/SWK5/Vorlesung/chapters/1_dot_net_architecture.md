- [.NET Architektur](#net-architektur)
  - [Komponenten des .NET Full-Frameworks](#komponenten-des-net-full-frameworks)
  - [Weitere Varianten von .NET](#weitere-varianten-von-net)
  - [.NET Core](#net-core)
  - [Die .NET Plattform](#die-net-plattform)
  - [.NET Framework und die UWP (Windows 10)](#net-framework-und-die-uwp-windows-10)
  - [.NET Standard](#net-standard)
  - [Framework Compatibility Mode](#framework-compatibility-mode)
  - [Common Language Runtime](#common-language-runtime)
    - [Unterschiede zur JVM](#unterschiede-zur-jvm)
    - [Common Type System (CTS)](#common-type-system-cts)
    - [Common Language Specification (CLS)](#common-language-specification-cls)
    - [Intermediate Language (IL)](#intermediate-language-il)
    - [Virtual Execution System (VES)](#virtual-execution-system-ves)
  - [Zusammenfassung](#zusammenfassung)
  - [Just in Time Compiler](#just-in-time-compiler)
  - [.NET Native (.NET Core)](#net-native-net-core)

# .NET Architektur

## Komponenten des .NET Full-Frameworks

* **Common Language Runtime**: Führt .NET Code aus (vgl. JVM)
* **.NET Framework Class Library**
  * Base Class Library
  * ADO.NET: Database Zugriff
  * ASP.NET: Für REST APis und Web
  * WPF: Windows presentation framework
  * WCF
* **Common Type System**: Damit mehrere Sprachen auf das Framework zugreifen können. Bereitet Klassen für alle Sprachen auf
* C#, C++, VB.NET, F#,...

## Weitere Varianten von .NET

Da das FullFramework nicht alles abdecken kann oder nicht immer notwendig ist, wurden verschiedene Varianten von .NET eingeführt.

* .NET Core: Open Source Entwicklung von Microsoft
  * hat sich in den letzten 2-3 Jahren etabliert
  * CoreCLR als Laufzeitumgebung
  * Unterstützt mehrere Plattformen
  * CoreFX enthält Basisfunktionalität der Class Library
* Mono
  * Open Source
  * Runtime mit .NET kompatibel
  * Mehrere Plattformen
* Xamarin
  * basiert auf Mono
  * Native entwicklung auf mobilen Plattformen
  * Von Microsoft 2016 übernommen

## .NET Core

Um Komptiablität verschiedener Versionen zu standardisieren.  
Verwendung des Packagemanagers *Nuget* um Komponenten nachzuladen.  
Self-contained deployment: Applikation wird gemeinsam mit den benötigten Framework deployed.

## Die .NET Plattform

## .NET Framework und die UWP (Windows 10)

Die Verwendung des Full Frames erleichtert die Entwicklung von grafischen Windows Desktop Anwendungen erheblich. Diese docken an der Win32 API an.

## .NET Standard

Da mehrere Varianten des .NET Frameworks und somit viele verschiedene Basisklassen exitsieren, ist das Portieren von Code sehr aufwändig. Es wurde der .NET Standard entwicklet, der vorgibt, welche Methoden in den APIs vorhanden sein müssen. Die Frameworks können diesen Standard in einer gewissen Version unterstützen. Es herrscht Binärkompatiblität für die verschiedenen Class Libraries.

## Framework Compatibility Mode

DLLs die unter einer beliebige Version und Variante des Frameworks entwickelt wurden können mit dem Kompatiblitätsmodus wegen der Binärkompatiblität wobmöglich bei API Gleichheit des .NET Standards verwendet werden.  

## Common Language Runtime

Aufgaben sind die Speicherverwenaltung, Sicherheitsüberprüfung und das Laden dynamischer Komponenten. Stellt Verbindung zum OS her und überprüft die Verfügbarkeit der benötigten Features. Versteht eine Zwischensprache.

CLR ist eine virtualle Maschiene und kann auf andere Plattformen portiert werden. Dies ist kein Nachteil, denn es können Optimierungen in der Laufzeit durchgeführt werden. 

### Unterschiede zur JVM

* JVM interpretiert den Bytecode
* Oft verwendete Code Bereiche werden nativ übersetzt
* CLR Übersetzt Zwischencode immer -> JIT-Compiler
* CLR unterstützt mehrere Sprachen/Paradigmen
* CLR hat Selbstdefinierte Wertetypen
* CLR unterstützt Call by reference
* CLR verwendet Methodenzeiger

### Common Type System (CTS)

Legt fest wie Datentypen im Speicher dargestellt werden, somit können sie von verschiednenen Sprachen verwendet werden.

Beispiel: Person Klasse wird in Visual Basic implementiert und zur .dll kompiliert. Die Studen Klasse wird in C# implementiert und leitet von Person ab. C# Kompiler findet Person.dll, wenn als Referenzparameter die Person.dll mitgegeben wird.

### Common Language Specification (CLS)

Bei manchen Sprachen wird die Groß/Kleinschreibung anders behandelt. Die Specification gibt an ab wann eine Komponente Sprachübergreifend verwendbar ist. Weitere Beispiele: Verschiedene Namen von Feldern und Methoden, ..

### Intermediate Language (IL)

Es wird in eine Zwischensprache übersetzt die der JIT-Compiler versteht.

### Virtual Execution System (VES)

Just in Time: Nur wenn eine Methode aufgerufen wird.

## Zusammenfassung

Quelcode -> Compiler für .NET -> Code/Metadaten (IL)
Ausführung mit VES : JIT-Compiler->Native Code

## Just in Time Compiler

Zwischenspreche wird immer kompiliert.
Sobald eine Methode aufgerufen wird, wird diese falls noch nicht kompiliert nicht aufgerufen, sondern der Stub zum anstoßen des JIT-Compilers, der die gewünschte Methode übersetzt.

## .NET Native (.NET Core)

Es wird hierbei gleich nativer Code nach Optimierung erzeugt.
Bessere Ausführungszeiten (Programmstart), Kleinerer Memoryfootprint, Kleinere Pakete. Wird bei UWP Apps verwendet.