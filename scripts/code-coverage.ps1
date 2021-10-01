$nugetsPath = "C:\Users\rafcio0584\.nuget\packages\"

$directoryCoverage = '.\.coverage\'
$directoryReportCoverage = 'reports\'

$integrationDll = './Tests/Integration/bin/Debug/net5.0/Integration.dll'
$integrationReportFileName = 'report-integration.json'

$unitDll = './Tests/Unit/bin/Debug/net5.0/Unit.dll'
$outputReportFileName = 'report.xml'

$pathFileIntegrationReport = "$directoryCoverage$integrationReportFileName" 
$pathFileOutputReport = "$directoryCoverage$outputReportFileName"
$pathOutputReportCoverage = "$directoryCoverage$directoryReportCoverage"



$cmdIntegration = "coverlet $integrationDll --target `"dotnet`" --targetargs `"test ./Tests/Integration --no-build`" -o `"$pathFileIntegrationReport`""
$cmdUnit = "coverlet $unitDll --target `"dotnet`" --targetargs `"test ./Tests/Unit --no-build`" -o `"$pathFileOutputReport`" --merge-with  `"$pathFileIntegrationReport`" --format opencover"
$cmdReportGenerator = $nugetsPath + "reportgenerator\4.8.13\tools\net5.0\ReportGenerator.exe  `"-reports:$pathFileOutputReport`" `"-targetdir:$pathOutputReportCoverage`" `"-reporttypes:Html;Xml`""

Write-Output "Collecting Integration tests"
$cmdIntegration
Invoke-Expression $cmdIntegration

Write-Output "Collecting Unit tests"
$cmdUnit
Invoke-Expression $cmdUnit

Write-Output "Generate report"
$cmdReportGenerator
Invoke-Expression $cmdReportGenerator
