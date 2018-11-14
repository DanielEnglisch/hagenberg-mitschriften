param ([switch] $Clean)

if ($Clean){
    Remove-Item *.exe,*.dll
    return
}

csc /t:library Calc.cs
vbc /t:library /r:Calc.dll AdvCalc.vb
csc /t:exe /r:Calc.dll /r:AdvCalc.dll App.cs