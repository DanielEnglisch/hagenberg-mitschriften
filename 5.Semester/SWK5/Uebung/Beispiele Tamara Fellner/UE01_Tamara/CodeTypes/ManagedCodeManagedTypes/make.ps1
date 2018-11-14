param ([switch] $Clean)

if ($Clean){
    Remove-Item *.exe,*.dll, *.exp, *.lib, *.obj
    return
}

cl /clr /LD Calc.cpp
cl /clr /FUCalc.dll /FeCppApp.exe App.cpp
csc /t:exe /r:Calc.dll /out:CsApp.exe App.cs