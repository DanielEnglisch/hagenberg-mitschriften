# Generics

## Einstiegsfragen

Warum kann in Java auf Template-Parameter der new-Operator nicht angewandt werden?

Auswirkungen von ?
```csharp=
ArrayList<? extends Person> pList = new ArrayList<Student>();
```
Keine Möglichkeit Methoden aufzurufen, die einen Eingangeparameter haben.

```csharp=
ArrayList<Person> pList = new ArrayList<Student>();
```
Hier können hingegen alle Personen Objekte eingefügt werden.

## Probleme bei Objekt-basierten Behältern

Einsatz vom Datentyp `object` anstatt Generics erzeugt einen erheblichen Overhead beim Un/Boxing. Aufpassen auf statischer/dynamischer Typ zu Laufzeit. Wenn nur ein homogener Stack erzeugt werden will kann trotzdem zb. ein double oder string hineingeworfen werden.

```csharp=
// unboxing / boxing laufzeit verlust
Stack s = new Stack(10); s.Push(1);
int i = (int)s.Pop();
```

```csharp=
// typsicherheit zur Laufzeit nicht überprüfbar
s.Push(3.14); 
string str = (string)s.Pop();
```

## Verwendung von Generics

Anstatt der Verwendung von `object` wird mit den Diamantklammern `class Stack<ElementType>{...}` ein generischer Typ erzeugt. Kein Casten notwending, da der beinhaltete Typ des Behälters zur Kompilezeit festgelegt wird. Typsicherheit und bessere Performance sind deutliche Vorteile gegenüber der Verwendung von `object`.

```csharp=
Stack<int> s1 = new Stack<int>(10);
s1.Push(1);
int i= s1.Pop();
```

Bei Java müssen Wrapperklassen verwendet werden (nur Referenzdatentypen/Objekttypen verwendbar).

## Derivation Constraint

Beim Aufruf vom Methoden bei generischen Typen z.B `compareTo` wird in C++ vorausgesetzt, dass nur Typen verwendet werden, die diese Methoden unterstützen. Der C# Kompiler lässt dies nicht zu.

```csharp=
public class LinkedList<K,V> { 
    public V Find(K key) { 
        while (…) { 
            if (key.CompareTo(current.key)) { // Syntaxfehler
…
```

In C# kann man Contraints zum generischen Typ angeben, um weniger Probleme beim Instanziieren von diesem Datentyp zu bekommen (Aufruf von nicht implementierten Methoden).

```csharp=
public class LinkedList<K,V> where K : IComparable<K> { 
    V Find(K key) { 
        while (…) { 
            if (key.CompareTo(current.key)) {
…
```

Durch constraints kann man den Verwender einschränken und vorgeben welche DT übergeben werden können --> strengere Typenüberprüfungen, Fehler beim Instanzieren mit falscher DT.
Unterschiede zu C++ und Java für die Klausur relevant auch einordnen können.

## Constructor Constraint

Es kann ein Default Kontruktor von einem generischen Typ gefordert werden, falls das Anlegen einer Variable dieses Typs erforderlich ist. Auch andere Konstruktoren können gefordert werden. Man kann auch bestimmen, dass ein Wertetyp übergeben wird: where V: struct.

```csharp=
public class Node<K,V> where V : new() { 
    private K key; 
    private V value; 
    public Node() { 
        key = default(K); 
        value = new V();
    }
}
```

## Struct Constraint

Es kann auch von einem generischen Typ gefordert werden, dass er ein struct ist.

## Generische Methoden

Wie in Java können auch generische Methoden definiert werden, wobei auch wieder Contraints angegeben werden können.

```csharp=
public class Math { 
    public static T Min<T>(T a, T b) where T : IComparable<T> {
        if (a.CompareTo(b) < 0) 
            return a;
        else 
            return b;
    }
}
```

Methode kann mit beliebigem Typ instanziert werden.
```csharp=
string minStr = Math.Min<string>("abc", "efg");
```

Parametertyp kann meistens vom Compiler ermittelt werden.
```csharp=
string minStr = Math.Min("abc", "efg");
```

Beim Aufrufen dieser Methode muss nicht unbedingt der generische Typ angegeben werden, wenn der Kompiler durch die übergebenen Variablen den Typ selbst bestimmen kann.

## Implementierung von Generics in .NET

Das Laufzeitsystem von C# kennt generische Typen, in Java kennt der Bytecode diese nicht. C# weiß zu Laufzeit welche Datentypen eingesetzt wurden.

Auf IL Ebene gibt es Generics. Erst zur Laufzeit folgt die Instanzierung. Erst dann wird für T ein Typ eingesetzt. Für alle refernztypen gemeinsam wird einmal instanziert. In Java hingegen schon beim Kompilieren.

## Vorteile von Generics
Kein Code-bloat ->
    - Mehrfache Instanzierung von Templates für gleiche Elementtypen. 
    - Längere Ladezeiten, erhöhter Speicherbedarf.

Generics werden mit Assemblys ausgeliefert unf können wiederverwendet werden, somit wird CodeBloat verhindert.

Keine Typenkonversion bei Referenztypen.

Kein Un/Boxing bei Wertetypen. Beim Boxing wird WrapperObjekt erzeugt, mit geringen Nutzdatenanteil und großem Overhead.
```csharp=
List<int> list = new List<int>(); 
list.add(100); 
int item = list[0]; // hier kein unboxing
```

Geringerer Speicherplatzbedarf bei Behältern mit Wertetypen.

Zugriff auf Typeparameter mit Reflection während der Laufzeit.

```csharp=
List<int> list = new List<int>(); 
Type collType = list.GetType();
Type[] paramType = collType.GetTypeParameters();
```
Type ist das Gegenstück zu GetClass() in Java.

## Unterschiede zu Generics in Java

Primitive Datentypen können in Java nicht verwendet werden.
Es können keine Contraints definiert werden.
 
Type erasure: In Java geht die Typeninformation von generischen Typen verloren, es wird nur `Object` zurückgegeben. Dieses Phänomen gibt es NICHT in C#.
```csharp=
ArrayList<Integer> list = ...
list.add(1); // Type erasure 
```
Integer geht zur Laufzeit verloren, kann nicht überreflection gefundne werden. Statische Typen werden gehen nicht verloren.

Statische Metadaten können schon herausgefunden werden.
```cshart=
class X implements List<String>{...}
```

Man darf für T keine Konstruktoren voraussetzen. Kein T[] möglich eher Objekt[].

Für Generics musste die JVM nicht erweitert werden, da diese sowieso zur Kompilzeit übersetzt werden und es werden keine Metadaten gespeichert.

Zur Laufzeit müssen Typenkonversionen durchgeführt werden, was die Performance verschechtert.

## Unterschiede zu C++ Templates

- Generics sind typisierte Klassen, Templates sind „Macros“ 
    - Templates werden zur Compilezeit instanziert. - Generics werden zur Laufzeit instanziert.
- Generics erhöhen Typsicherheit (bereits zur Compilezeit) 
    - Viele C++-Compiler kompilieren Template-Code nicht. 
    - Erst bei Template-Instanzierung wird Code erzeugt. 
    - Erst bei Template-Instanzierung wird überprüft, ob Operationen auf TemplateParameter möglich sind.
- `Code Bloat`
    - Mehrfache Instanzierung von Templates für gleiche Elementtypen. 
    - Längere Ladezeiten, erhöhter Speicherbedarf.
- Generics bieten weniger Funktionalität
    - Auf Parameter generischer Typen dürfen keine Operatoren angewandt werden.

In Templates kann auch die Implementation von festgelegten Methoden oder Konstrukturen festgelegt werden.




















