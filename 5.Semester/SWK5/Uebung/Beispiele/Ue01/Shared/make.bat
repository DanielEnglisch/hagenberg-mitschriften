mkdir c:\temp\bin\v2
csc /t:library /keyfile:keyPair.snk /out:c:/temp/bin/v2/Calc.dll Calc.cs
csc /t:exe /r:c:/temp/bin/v2/Calc.dll App.cs
