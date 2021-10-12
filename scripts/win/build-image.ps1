[CmdletBinding()]
param(
    [string] $env='Production',
    [int] $port=3000,
    [array] $args=@{}
)

$current_location = Get-Location
$current_location = "$current_location\scripts\win"

. "$current_location\commands\Get-Build-Plan.ps1"
. "$current_location\commands\Get-Image-Name.ps1"
. "$current_location\commands\Docker-Commands.ps1"

$ImageName = Get-Image-Name -env $env
$BuildPlan = Get-Build-Plan -env $env
$ServiceUrl = "http://+:$port"

Build-Image -imageName $ImageName `
    -buildPlan $BuildPlan `
    -env $env `
    -port $port `
    -serviceUrl $ServiceUrl
