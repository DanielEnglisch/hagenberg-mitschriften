# Befehle

Neues Projekt
```cmd
dotnet new console
```

Projekt builden
```cmd
dotnet build
```

Projekt starten
```cmd
dotnet run
```

Projekt publishen (--sel-contained, keine Abhängigkeiten auf .net)
```cmd
dotnet publish --self-contained --runtime win-x64 --output winx64
```

Projekt publishen für linux(--sel-contained, keine Abhängigkeiten auf .net)
```cmd
dotnet publish --self-contained --runtime alpine-x64 --output winx64
```