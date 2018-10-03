# .NET Fortgeschrittene Konzepte von C\#

## Abgrenzung C# - Java/C++

Merkmale von Java

* OOP
* Metainformationen
* Exceptions
* statische und dynamische Typisierung
* Garbage Collection

Merkmale von C++

* Überladen von Operatoren
* Möglichkeit, Pointer zu verwenden

Neue Eigenschaften

* Attribute (vs. Annotationen)
* Call by Reference
* Wertetypen (Eigene Structs)

## Neue Operatoren
as: Casting  
?: Nullable  
...

## Typen

* Stack
  * Wertetyp
    * Einfache Typen (int, double,...)
    * Enumeration
    * Struktur
* Heap
  * Referenztypen
    * Interface-Typ
    * Pointer-Typ
    * Array
    * Klasse
    * Delegate

Automatische Konvertierung zwischen Referenz und Wertedatentypen (Boxing bzw. Unboxing). Wird in einer Klasse ein String gespeichert, wird nur eine Referenz darauf im Heap gespeichert. Eine Struktur hat einen signifikant geringern Overhead in der Erzeugung.

### Einfache Typen

sbyte, byte, short, ushort, int, uint, long, ulong, float, double, decimal, bool, char

Verwendung dieser Datentypen sind nur Alias für die CTS Typen z.B int -> System.Int32

### Strukturen

Wie in C++. Kann ein Interface implementieren kann aber nicht von einem Referenzdatentyp abgeleitet werden bzw. diesen implementieren. Verwenden zum Abbilden von einfachen Datentypen für effizente Verwendung ohne Objekterzeugungsoverhead.

### Nullable Types

Nullable Types sind Wertetypen, die als Wert auch null annehmen können.
Normalerweise kann ein int nicht den wert null haben. Mit folgenden Synntax ist dies Möglich:

```csharp
int? i1 = 10;
int? i2 = null;
int j1 = i1.Value;
int j2 = (int)i1;
int j3 = i2 ?? 0;
```

## Klassen

Neue Bestandteile: Properties, Indexer, Operatoren, Konstanten, Finializer/Desktuktor

## Sichtbarkeit

* public: überall
* protected: nur abgeleitete Klasse
* internal: selbe Assembly
* protected internal:protected oder internal
* private internal: protected und internal
* private: deklerierende Klasse

Die Standardsichtbarkeit ist anders als in Java.

## Destructor

Wird aufgerufen, bevor der Garbage Collector das Objekt freigibt.  
Dient zur Ressourcenfreigabe (Schließen von Files)

## Verwenden von IDisposable

Zeigt an, dass diese Klasse Resourcen verwaltet, die freigegeben werden müssen. Wenn die Klasse nicht mehr benötigt werden muss explizit die Dispose Methode aufgerufen werden. Der Finalizer ruft vor beseitigung die Dispose Methode auf. Wenn die Dispose Methode explizit aufgreufen wurde, wird ein Flag gesetzt, der verhindert, dass das Objekt nicht zweimal durch den Finalizer Disposed wird.

Kurzschreibweise:
```csharp
using(A a = new A()){
    // Verwenden von a
} // Automatisches Aufrufen von a.Dispose()
```

## Felder und Konstanten

Wenn ein Objekt konstant sein soll kann das Keyword readonly verwendet werden.

## Arten von Parametern

* Eingangsparameter
* Übergangsparameter mit ref (explizit angeben)
* Ausgangsparameter mit out (muss nicht initialisiert werden)

## Variable Anzahl von Parametern

```csharp
double Sum(params double[] values);

Sum(1.2, 3.4, 5.7)

```

## Properties

Verwendung wie Felder nur, dass benuterdefinierte Aktionen beim Setzen bzw. Lesen ausgeführt werden können. Relisiert Uniform Access Principle, also kann nicht zwischen Datenkomponente und Property rein synntaktisch nicht unterschieden werdn.

## Vererbung

Nur Einfachvererbung möglich. Automatische Vererbung von der Klasse Object. Öffentliche und protected Methoden werden vererbt.

### Überschrieben von Methoden

Für dynamische Bindung MUSS das Keyword virtual verwendet werden. Um sie zu überschrieben muss override ode new angegeben werden.

#### Überschrieben von Methoden mit new

Neue Methode, die den selben Namen hat, wie die in der Basisklasse.
Mit new deklerierte Methoden muss der statische Typ (Links) derselbe sein, in dem die Dekleration gschehen ist.

**Anwendung**: Austausch von Basisklassen soll sich nicht auf Funktionalität von abgeleiteten Klassen auswirken. (Fragile Base Class Problem: Es wird unbeabsichtigt eine Methode überschrieben).

## Abstrakte Klassen und Interfaces