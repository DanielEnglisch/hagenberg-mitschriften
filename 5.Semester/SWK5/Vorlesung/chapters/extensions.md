# Spracherweiterungen C# 

## Neuerungen C# 3.0

### Objektinitialisierer(Object initializer)

Initialisierung mit Werten kann man nicht erzwingen.
Auch Collections können so erzeugt werden.
Erzeugung von Objekten mit Key-Value Syntax.

```csharp=
List<Employee> empls = new List<Employee> { 
    new Employee { Id = 1, Name = "Jacak", City = "Linz" }, 
    new Employee { Id = 2, Name = "Dobler", City = "Hagenberg" }
}
```

### Automatische Typableitung (Local Variable Type Inference)

Anstatt auf der linken und rechten Seite einer Zuweisung bei der Definition dasselbe hingeschrieben werden muss kann das Keyword `var` für lokale Variablen verwendet werden. Die Änderung der Datentyps zur aufzwit ist nicht möglich! Verwendung bei Anonymen Typen/Klassen.

```csharp=
var myList = new List<String>();
```

### Anonyme Typen

Gibt es auch in Java, nur das der anonymt Typ durch ein Interface (Functional-Interface) festgelegt wird, vergleiche EventHandler in Java. Da, der Datentyp anonym angelegt wird, muss var verwendet werden.

```csharp=
var myObj = {Id = 1 , name = "MyName"};
```

### Lambda-Ausdrücke

Sind oberflächlich genauso wie in Java. Anonyme Methoden mit delegates möglich.

### Erweiterungsmethoden (Extension Methods)

Problem: Hat man keine Kontrolle über ein Interface, kann zusätzliche Funktionalität nur mittels Klassenmethoden implementiert werden.
```csharp=
static class Enumerator { 
    public static int Sum(IEnumerable<int> numbers) { 
        int sum = 0; 
        foreach (int i in numbers) 
            sum += i; 
            return sum;
    }
}
```

Beim Aufruf der Methode muss das Objekt, dessen Klasse erweitert worden ist, als Parameter übergeben werden:
```csharp=
var numbers = new List<int> { 2, 3, 5, 7 }; 
int s = Enumerator.Sum(numbers);
```

Man möchte die Erweiterungsmethode aber wie eine Objektmethode aufrufen.

Lösung:
```csharp=
namespace EnumeratorExt { 
    public static class Enumerator { 
        // this bedeutet es ist eine 
        // Erweiterungsmethode für IEnumerable
        public static int Sum(this IEnumerable<int> numbers) { 
            int sum = 0; 
            foreach (int i in numbers) 
                sum += i; 
                return sum;
        }
    }
}
```

Die Erweiterungsmethode kann nun wie eine Objektmethode aufgerufen werden:
```csharp=
using EnumeratorExt; 
var numbers = new List<int> { 2, 3, 5, 7 };
int s = numbers.Sum();
```

Nur wenn der Namespace verwendet wird, ist die Erweiterungsmethode `Sum()` sichtbar. Beim nachverfolgen von Methodenaufrufen muss auf diese Option geachtet werden.


### Automatische Implementierung von Properties

Kürzere Schreibweise von Properties seit C# 3.0.

```csharp=
class Employee { 
    public string Name { get; set; }
    // anstatt von
    // get { return name;} set { name = value; }
}
```

### LINQ: Language Integrated Query

Ist in C#/VB .Net eingebaut.
Abfragesprache auf beliebige Datenbehälter, ob Datenbank, XML oder Objektbehälter.

Beispiel:
```csharp=
using System.Linq; 
IEnumerable<Employee> employees = …; 
var query = from e in employees 
            where e.City == "Linz"
            select new { Id = e.Id, Name = e.Name };
``` 

LINQ-Abfragen werden in Aufrufe von Erweiterungsmethoden übersetzt:
```csharp=
// employess ist IEnumerable 
// und Where und Select, etc. sind Erweiterungsmethoden 
// dieses Interfaces
var query = employees.Where(e => e.City == "Linz")
                    .Select(e => new { Id = e.Id, Name = e.Name });
```

## Neuerungen in C# 4.0
### Benannte Parameter

Reihenfolge der Parameter wird durch die Angabe des Parameternames angegeben:

```csharp=
class Rational { 
    public Rational(int num, int denom) { … } …
}
Rational r1 = new Rational(1, 2);
```

Durch Qualifizierung mit dem Namen des Formalparameters können in C# die Aktualparameter in beliebiger Reihenfolge übergeben werden.
```csharp=
new Rational(num:1, denom:4);
new Rational(denom:4, num:1);
```

Hauptanwendungsgebiet: Parameterübergabe bei Methoden mit langen Parameterlisten und optionalen Parametern.

### Optionale Parameter

Wie in C++, sie jedoch am Ende der Methodensignator definiert werden, außer es werden benannte Parameter verwendet, bei denen die Reihenfolge natürlich egal ist.

```csharp=
class Rational { 
    public Rational(int num = 0, int denom = 1) { … } …
}
```

Beim Aufruf müssen für Parameter am Ende der Liste keine Werte übergeben werden.
```csharp=
Rational r1 = new Rational(); // Rational(0,1) 
Rational r2 = new Rational(5); // Rational(5,1)
```

Mithilfe benannter Parameter können Parameter selektiv übergeben werden:
```csharp=
Rational r3 = new Rational(denom: 5); 
// Rational(0,5)
```

### Dynamische Typprüfung

Mit `dynamic` wird vergleichbar mit`let`oder `var` aus JavaScript keine Typanprüfung mehr durchgeführt und es kann während der Laufzeit verschiedene Typen zugewiesen werden.

```csharp=
dynamic d = "abc"; 
if (condition) d = new int[] { 5, 17, 3, 8 }; 
int len = d.Length; // runtime checks if method Length is available
object obj = d[1]; // runtime checks if indexer is defined for dynamic type d.
```

Anwendung: Einbindung von Skriptsprachen und COM-Komponenten zur Laufzeit.

```csharp=
string script = @"def factorial(n): 
                    for i in range(1, n): n = n * i 
                    return n";
ScriptEngine engine = Python.CreateEngine();
ScriptScope scriptScope = engine.CreateScope(); 
ScriptSource scriptSource = engine.CreateScriptSourceFromString(script, 
    SourceCodeKind.Statements);

scriptSource.Execute(scriptScope);

dynamic mathScript = scriptScope;
int fact = mathScript.factorial(5);
```

### Kovarianz und Kontravarianz

- Gegeben seien zwei Typen U und Vmit einer Relation <, die eine Ordnung auf Typen definiert im Bezug auf den Wertebereich. 
    - Beispiel 1: $int < float < double$
    - Beispiel 2: $V < U$, falls V ist eine Unterklasse von U.
- Sei f : $U -> U‘$ ist eine Abbildung, die einen Typ U auf einen anderen Typ U‘ abbildet.
    - Beispiel: $T -> IEnumerable<T> oder T -> T[]$
- Sei $V < U$. Dann ist 
    -  f ist **kovariant**, wenn $f(V) < f(U)$
    -  f ist **kontravariant**, wenn $f(U) < f(V)$
    - f ist invariant, wenn fweder kovariant noch kontravariant ist.

### Kovarianz bei Feldern

Die Abbildung $T -> T[]$ eines Referenztyps T ist in C# und Java kovariant (Ordnungsrelation ist Vererbungsbeziehung).
- U is subtype of $V => U[]$ is subtype of $V[]$

```csharp=
object[] objArr; 
string[] strArr = new string[] { "abc", "efg" };
objArr = strArr;
```

Allerdings geht dadurch die Typsicherheit verloren.
- Zur Laufzeit kann eine ArrayTypeMismatchException (C#) auftreten.
```csharp=
objArr[0] = DateTime.Now; // throws ArrayTypeMismatchException 
string s = strArr[0];
```

Für Wertetypen (Ordnungsrelation Wertebereich) gilt dies nicht.

```csharp=
double[] fa = new float[3]; // syntax error
```

### Kovarianz bei Generics

Einer Liste von Objects kann keine Liste von Strings generische zugewiesen werden, aus dem selben Grund wie im vorherigen Punkt.

Würde ```Liste<string>``` von ```List<obj>``` abgeleitet werden, wäre sie kovariant. Aber Compiler lässt Zuweisung nicht zu. Man kann dem statischen Typ ```List<obj>``` nicht den dynamischen Typ ```List<string>``` zuweisen. Auch hier geht Typensicherheit verloren.

### Kontravarianz bei Generics

Dasselbe gilt ab C# 4.0 schon auf Interfaces, die speziel gekennzeichnet sind, z.B IEnumerable.

```csharp=
public interface IEnumerable<out T>{...}
```

### Ko- und Kontravarianz bei Delegates


## Neuerungen in C# 5.0

### Vereinfachte asynchrone Programmierung



## Neuerungen in C# 6.0

### Null-Conditional Operator

### Initialisierer für Auto-Properties

### Verkürzte Methodendefinition

### Operator nameof

### String-Interpolation


## Neuerungen in C# 7.0

### Tupel

### Pattern-Matching

### Referenzvariablen

### Verbesserungen bei Literalen

### asyncMain