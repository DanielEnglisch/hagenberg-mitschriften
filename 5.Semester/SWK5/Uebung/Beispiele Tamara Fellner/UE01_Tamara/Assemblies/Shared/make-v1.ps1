param ([switch] $Clean)

if ($Clean){
    Remove-Item *.exe,*.dll
    return
}

New-Item -ItemType Directory -Force -Path C:\temp\bin\v1
New-Item -ItemType Directory -Force -Path C:\temp\bin\v2

$bin = "C:/temp/bin/v1"

csc /t:library /keyfile:MyPubPriv.key /out:$bin/Calc.dll Calc1.cs
csc /t:exe /r:$bin/Calc.dll App.cs