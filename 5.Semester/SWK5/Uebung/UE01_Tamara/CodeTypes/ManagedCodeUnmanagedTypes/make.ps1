param ([switch] $Clean)

if ($Clean){
    Remove-Item *.exe,*.dll, *.exp, *.lib, *.obj
    return
}

cl /clr /LD Calc.cpp
cl /clr App.cpp Calc.lib