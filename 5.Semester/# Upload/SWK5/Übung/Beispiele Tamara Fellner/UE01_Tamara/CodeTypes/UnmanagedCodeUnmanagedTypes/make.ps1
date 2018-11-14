param ([switch] $Clean)

if ($Clean){
    Remove-Item *.exe,*.dll, *.exp, *.lib, *.obj
    return
}

cl /LD Calc.cpp
cl App.cpp Calc.lib