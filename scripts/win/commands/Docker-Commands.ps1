. "$current_location\commands\Helpers.ps1"

function Build-Image
{
    param(
        [string] $imageName,
        [string] $buildPlan,
        [string] $env,
        [string] $serviceUrl,
        [int] $port
    )

    [string] $Action = [string]::Format(
        "docker build -t {0} " +
            "--build-arg SERVICE_BUILD_PLAN={1} " + 
            "--build-arg SERVICE_PORT={2} " +
            "--build-arg SERVICE_ENV={3} " + 
            "--build-arg SERVICE_URL={4} .",
        $ImageName, 
        $buildPlan, 
        $port, 
        $env, 
        $serviceUrl 
    )
    Write-Output "Build image service"
    Write-Verbose $Action
    Invoke-Expression $Action
}

function Create-Container
{
    param(
        [string] $name,
        [string] $imageName,
        [int] $port
    )
    
    [string] $Ports = [string]::Format("{0}:{1}",$port, $port)
    [string] $Action = [string]::Format("docker run -d -p {0} --name {1} {2}", $Ports, $name,  $imageName)

    Write-Output "Create service container"
    Write-Verbose $Action
    Invoke-Expression $Action
}

function Check-Image-Exists
{
    param(
        [string] $imageName
    )

    $result = $null

    [string] $Action = [string]::Format("docker images -q {0}", $imageName)
    $result = Invoke-Expression $Action
    
    if ($null -ne $result) {
        return $true
    }
    
    return $false
}

function Check-Container-Exists
{
    param (
        [string] $name
    )

    [string] $Command = [string]::Format("docker ps -f name={0}", $name)
    $data = Convert-Docker-Output-To-Array-List -command $Command

    if ($data["Rows"] -gt 1)
    {
        $value = $data["Data"][1][[DockerImageListColumns]::NAMES]
        if ($value -eq $name)
        {
            return $true
        }
    }

    return $false;
}

function Check-Port-Is-Used
{
    param (
        [int] $port
    )

    [string] $Command = [string]::Format("docker ps -f publish={0}", $port)
    $data = Convert-Docker-Output-To-Array-List -command $Command
    if ($data["Rows"] -gt 1)
    {
        $value = $data["Data"][1][[DockerImageListColumns]::PORTS]

        if ($value -match $port)
        {
            return $true
        }
    }

    return $false;
}