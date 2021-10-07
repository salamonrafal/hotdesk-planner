$nugetPath = "C:\Users\rafcio0584\.nuget\packages\"
$exitCode = 0
$threshold = 75
$thresholdType = 'line'
$excludeFiles = './Api/Properties/**/*.*'
$directoryCoverage = '.\.coverage\'
$directoryReportCoverage = 'reports\'
$directoryHistoryCoverage = 'history\'
$integrationDll = './Tests/Integration/bin/Debug/net5.0/Integration.dll'
$integrationReportFileName = 'report-integration.json'
$unitDll = './Tests/Unit/bin/Debug/net5.0/Unit.dll'
$outputReportFileName = 'report.xml'
$errors = @()
$pathFileIntegrationReport = "$directoryCoverage$integrationReportFileName" 
$pathFileOutputReport = "$directoryCoverage$outputReportFileName"
$pathOutputReportCoverage = "$directoryCoverage$directoryReportCoverage"
$pathOutputHistoryCoverage = "$directoryCoverage$directoryHistoryCoverage"
$cmdIntegration = "coverlet $integrationDll --target `"dotnet`" --targetargs `"test ./Tests/Integration --no-build`" -o `"$pathFileIntegrationReport`" --verbosity minimal --threshold $threshold --threshold-type $thresholdType --exclude-by-file `"$excludeFiles`""
$cmdUnit = "coverlet $unitDll --target `"dotnet`" --targetargs `"test ./Tests/Unit --no-build`" -o `"$pathFileOutputReport`" --merge-with  `"$pathFileIntegrationReport`" --format opencover --verbosity minimal --threshold $threshold --threshold-type $thresholdType --exclude-by-file `"$excludeFiles`""
$cmdReportGenerator = $nugetPath + "reportgenerator\4.8.13\tools\net5.0\ReportGenerator.exe  `"-reports:$pathFileOutputReport`" `"-targetdir:$pathOutputReportCoverage`" `"-reporttypes:Html;HtmlSummary;Xml`" `"-verbosity: Warning`" `"-historydir:$pathOutputHistoryCoverage`""


Write-Output "Collecting Integration tests"
$cmdIntegration
Invoke-Expression $cmdIntegration
if ($LastExitCode -gt 0) {
    $errors += "Integration";
}

Write-Output "Collecting Unit tests"
$cmdUnit
Invoke-Expression $cmdUnit
if ($LastExitCode -gt 0) {
    $errors += "Unit";
}

Write-Output "Generate report"
$cmdReportGenerator
Invoke-Expression $cmdReportGenerator
if ($LastExitCode -gt 0) {
    $errors += "ReportGenerator";
}

if ($errors.length -gt 0) {
    
    Write-Error -Message "One of Test return error: $errors" -Category OpenError
    $exitCode = 1;
}

exit $exitCode;