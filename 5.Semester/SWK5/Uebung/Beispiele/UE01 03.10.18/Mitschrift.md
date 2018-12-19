
# Unterschiede Assemblies zu C++-Libraries

- IL-Code anstatt Maschinencode
- Metadaten und Manifest vorhanden, die Features wie Introspection, etc. erlaube
- Ressourcen (Bilder, Fonts, etc.) können mitgepackt werden
- IL-Code ist unabhängig von der Entwicklungssprache

Generierung von IL-Code ist wesentlich einfacher als Maschinencode zu generieren --> Erleichterung für Compilerbauer.
Die Optimierungen werden anschließend von der Microsoft-Dot-NET-Plattform durchgeführt.

# Besprechung des IL-Codes

Ausgangssituation: sum = 17, operandCount = 3, number = 5

	public void Add(double number)
	{ // IL_0000:  nop
		this/*IL_0001:  ldarg.0*/.sum += number;
		++this.operandCount;
	}
	
	.method public hidebysig instance void  Add(float64 number) cil managed
	{
	  // Code size       30 (0x1e)
	  .maxstack  8
	  IL_0000:  nop
	  IL_0001:  ldarg.0
	  IL_0002:  ldarg.0
	  IL_0003:  ldfld      float64 Calculator::sum
	  IL_0008:  ldarg.1
	  IL_0009:  add
	  IL_000a:  stfld      float64 Calculator::sum
	  IL_000f:  ldarg.0
	  IL_0010:  ldarg.0
	  IL_0011:  ldfld      int32 Calculator::operandCount
	  IL_0016:  ldc.i4.1
	  IL_0017:  add
	  IL_0018:  stfld      int32 Calculator::operandCount
	  IL_001d:  ret
	} // end of method Calculator::Add

`IL_0000:  nop` ermöglicht es zu Beginn der Methode einen Breakpoint zu setzen  
`IL_0001:  ldarg.0` nimm den Parameter mit dem Index 0 (`this`-Pointer) und lege ihn auf den Stack --> Stack: this  
`IL_0001:  ldarg.0` --> Stack: this, this  
`IL_0003:  ldfld` schreibt den Wert der Variablen sum   auf den Stack; ein this  wird vom Stack genommen (Welches Objekt wird verwendet?) --> Stack: 17, sum  
`IL_0008:  ldarg.1` nimmt den Parameter mit dem Index 1   (`number`) und legt ihn auf den Stack --> Stack: 5, 17, this  
`IL_0009:  add` nimmt die obersten zwei Werte vom Stack, addiert diese und legt das Ergebnis auf den Stack --> Stack: 22, this

`IL_000a:  stfld` speichert den obersten Wert auf dem Stack in das Feld `sum`, wobei ein `this` vom Stack genommen wird (Welches Objekt wird verwendet?) --> Stack: <empty>  
`IL_000f:  ldarg.0` --> Stack: this  
`IL_0010:  ldarg.0` --> Stack: this, this  
`IL_0011:  ldfld` --> Stack: 3, this  
`IL_0016:  ldc.i4.1` lädt die Integer-Konstante 1 auf   dem Stack --> Stack: 1, 3, this  
`IL_0017:  add` --> Stack: 4, this   
`IL_0018:  stfld` --> Stack: <empty>  
`IL_001d:  ret` end of method  


Woher weiß die exe wo die dlls sind?
Weil sie im selben Verzeichnis waren.

Anderes Verzeichnis?
csc /t:library /out:bin/Calc.dll Calc.cs
-> mit out kann die dll wo anders als im Hauptverzeichnis gespeichert werden.

Bei den anderen muss natürlich angegeben werden wo diese dlls sind.

csc /t:library /out:bin/AdvCalc.dll /:rbin/Calc.dll AdvCalc.vb

csc /t:exe /r:bin/AdvCalc.dll /r:bin/Calc.dll App.cs

Der Anwendung muss auch mitgeteilt werden wo die dlls sind, zum compilierzeitpunkt wird nicht gespeichert wo die dlls sind. Lösung: .config datei. Dort kann mit AssemblyBinding den Pfad angeben. Mit Assembly binding wird bei runtime versucht die Assemblys zu finden. Da die config Datei App.exe wie unser Programm heißt sollte es automatisch beim start der App.exe mit ausgeführt werden. 

#### Strong Name

#### Schlüsselpaar generierung für signieren von assemblys
sn zur Schlüssel Paar generierung
```cmd
sn -k keyPar.snk
```
#### Extrahieren des Public Key:
```
sn -p keyPair.snk publicKey.snk
mkdir c:\temp\bin\v1 
```

assembly in den temp folder ablegen und vom compiler signieren lassen  
```cmd
csc \t:library /keyFile:keyPair.snk /out:C:/temp/bin/v1/Calc.dll Calc.cs
```
```
csc /t:exe /r:c:/temp/bin/v1/Calc.dll App.cs
```

Jetzt gibt es ein dependent assembly in der App.exe.config. Der PublicKeyToken muss noch extrahiert werden.

```
sn -t publicKey.snk
```

Dieser muss kopiert werden und bei der App.exe.config rein kopiert werden. Und die Location der dll muss auch angegeben werden.

Falls eine neue Version erscheint kann man `<codebase>` kopieren und die neue version einfügen. Daher kann ohne neu übersetzen in der config spezifiziert werden das eine neue Version verwendet werden soll mit: `<bindingredirecht oldversion ="..", newVersion="..">`


### c++
#### UnmanagedCode
Andere command line wird verwendet -> cross tolls command prompt for vs 2017  

LD für dll erzeugung
```cmd
cl /LD Calc.cpp
```

```cmd
cl App.cpp Calc.lib
```

#### ManagedcodeUnmanagedTypes
```cmd
cl App.cpp Calc.lib /clr
```

IDisposable ist ein net Klasse
GC.SuppressFinalize -> GC ruft dann nicht mehr Finalize auf 
Daher kann man mit C# Dispose explizit eine Ressource freigeben

#### Resources

resx datei in binärformat übersetzen und mit compilieren
```cmd
resgen MyRes.resx
cscs /resource:MyRes.resources App.cs
```
```cmd
resgen MyRes.de.resx
mkdir de
al /embedresource:MyRes.de.resources /culture:de /out:de\App.resources.dll
cscs /resource:MyRes.resources App.cs
```
```cmd
csc /t:exe /resource:MyRes.resources App.cs
App
```



Keine Compiler Commandos oder IL Code beim Kurztest!
