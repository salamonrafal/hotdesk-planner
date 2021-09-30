$cmdIntegration = coverlet ./Tests/Integration/bin/Debug/net5.0/Integration.dll --target "dotnet" --targetargs "test ./Tests/Integration --no-build" -o ".coverage/report-integration.json"
$cmdUnit = coverlet ./Tests/Unit/bin/Debug/net5.0/Unit.dll --target "dotnet" --targetargs "test ./Tests/Unit --no-build" -o ".coverage/report.json" --merge-with  ".coverage/report-integration.json"


$cmdIntegration
$cmdUnit