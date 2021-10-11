[CmdletBinding()]
param(
    [string] $name,
    [string] $env='Production',
    [int] $port=3000,
    [bool] $buildImage
)

$current_location = Get-Location
$current_location = "$current_location\scripts\win"

. "$current_location\commands\Get-Image-Name.ps1"
. "$current_location\commands\Docker-Commands.ps1"

$ImageName = $ImageName = Get-Image-Name -env $env
$IsImageExists = Check-Image-Exists $ImageName

if ( $IsImageExists -eq $true)
{
    $IsContainerExists = Check-Container-Exists -name $name
    
    if ($IsContainerExists -eq $false)
    {
        $IsPortInUse = Check-Port-Is-Used -port $port
        if ($IsPortInUse -eq $false)
        {
            Create-Container `
                -name $name `
                -imageName $ImageName `
                -port $port
        }
        else
        {
            Write-Warning "Not able to create container. Another container use same port"
        }
    }
    else
    {
        Write-Warning "Not able to create container. Another container has same name"
    }
} 
elseif ($null -ne $buildImage)
{
    . "$current_location\commands\Get-Build-Plan.ps1"
    
    $ImageName = Get-Image-Name -env $env
    $BuildPlan = Get-Build-Plan -env $env
    $ServiceUrl = "http://+:$port"
    
    Build-Image -imageName $ImageName `
        -buildPlan $BuildPlan `
        -env $env `
        -port $port `
        -serviceUrl $ServiceUrl

    $IsPortInUse = Check-Port-Is-Used -port $port
    if ($IsPortInUse -eq $false)
    {
        Create-Container `
                -name $name `
                -imageName $ImageName `
                -port $port
    }
    else
    {
        Write-Warning "Not able to create container. Another container use same port"
    }
}
