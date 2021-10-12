. "$current_location\commands\Environments.ps1"

function Get-Build-Plan
{
    param (
        [string] $env
    )
    
    if ($env -eq [Environments]::Production)
    {
        return "Release"
    } else {

        return "Debug"
    }
}