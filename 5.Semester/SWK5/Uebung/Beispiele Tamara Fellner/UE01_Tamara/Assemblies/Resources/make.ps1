param ([switch] $Clean)

if ($Clean){
    Remove-Item *.exe,*.dll
    return
}

resgen MyRes.txt
resgen MyRes.de-AT.txt


csc /t:exe /resource:MyRes.resources App.cs


al /embedresource:MyRes.de-AT.resources /culture:de-AT /out:de-AT\App.Resources.dll