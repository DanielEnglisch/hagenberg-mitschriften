$NSwagCmd = "C:\Users\andir\OneDrive\Hagenberg\5. Semester\09_SWK5\Uebung\#Software\NSwag\NSwag\nswag.exe"
$SwaggerUri = "http://localhost:56114/swagger/v1/swagger.json"
$ProxyClass = "ConverterProxy"
$ProxyNamespace = "CurrencyConverter.Proxy"

$ProjectFolder = $PSScriptRoot
$SwaggerFile = Join-Path $ProjectFolder "swagger.json"
$OutputFile  = Join-Path $ProjectFolder "$ProxyClass.cs"

echo "& $NSwagCmd swagger2csclient /input:"$SwaggerFile" /classname:$ProxyClass /namespace:$ProxyNamespace /output:$OutputFile /GenerateResponseClasses /WrapResponses:true /InjectHttpClient:true"
& $NSwagCmd swagger2csclient /input:"$SwaggerFile" /classname:$ProxyClass /namespace:$ProxyNamespace /output:$OutputFile /GenerateResponseClasses /WrapResponses:true /InjectHttpClient:true