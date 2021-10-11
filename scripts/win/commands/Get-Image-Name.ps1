. "$current_location\commands\Environments.ps1"

function Get-Image-Name
{
    param (
        [string] $env
    )
    
    $ImageName = "helpdesk-service"

    if ($env -eq [Environments]::Production)
    {
        return "$ImageName/prod"
    } else {

        return "$ImageName/dev"
    }
}