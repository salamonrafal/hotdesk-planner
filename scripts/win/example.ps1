enum DockerImageListColumns
{
    ID = 0
    IMAGE = 1
    COMMAND = 2
    CREATED = 3
    STATUS = 4
    PORTS = 5
    NAMES = 6
}


function Split-StringOnLiteralString
{
    trap
    {
        Write-Error "An error occurred using the Split-StringOnLiteralString function. This was most likely caused by the arguments supplied not being strings"
    }

    if ($args.Length -ne 2) `
    {
        Write-Error "Split-StringOnLiteralString was called without supplying two arguments. The first argument should be the string to be split, and the second should be the string or character on which to split the string."
    } `
    else `
    {
        if (($args[0]).GetType().Name -ne "String") `
        {
            Write-Warning "The first argument supplied to Split-StringOnLiteralString was not a string. It will be attempted to be converted to a string. To avoid this warning, cast arguments to a string before calling Split-StringOnLiteralString."
            $strToSplit = [string]$args[0]
        } `
        else `
        {
            $strToSplit = $args[0]
        }

        if ((($args[1]).GetType().Name -ne "String") -and (($args[1]).GetType().Name -ne "Char")) `
        {
            Write-Warning "The second argument supplied to Split-StringOnLiteralString was not a string. It will be attempted to be converted to a string. To avoid this warning, cast arguments to a string before calling Split-StringOnLiteralString."
            $strSplitter = [string]$args[1]
        } `
        elseif (($args[1]).GetType().Name -eq "Char") `
        {
            $strSplitter = [string]$args[1]
        } `
        else `
        {
            $strSplitter = $args[1]
        }

        $strSplitterInRegEx = [regex]::Escape($strSplitter)

        [regex]::Split($strToSplit, $strSplitterInRegEx)
    }
}

function Convert-Docker-Output-To-Array-List
{
    param(
        [string] $command
    )
    
    $returnData = @{
        Data = New-Object System.Collections.Generic.List[System.Object]
        Rows = 0
        Columns = 0
        _previous_cols = 0
    }
    
    foreach ($row in Invoke-Expression $command)
    {
        $columns = Split-StringOnLiteralString $row "   "
        $columnResult = New-Object System.Collections.Generic.List[System.Object];
        $returnData["Columns"] = 0

        foreach ($columnValue in $columns)
        {
            $columnValue = [string]::join("", ($columnValue.Split("`n")))
            $columnValue = $columnValue.Trim()

            if ($columnValue -ne "")
            {
                $columnResult.Add($columnValue.Trim())
                $returnData["Columns"] = $returnData["Columns"] + 1
            }
        }

        if ($columnResult.Count -gt 0)
        {
            $returnData["Data"].Add($columnResult)
            $returnData["Rows"] = $returnData["Rows"] + 1
            $returnData["_previous_cols"] = $returnData["Columns"]
        }
    }
    
    return $returnData 
}


Check-Port-Is-Used -port 3002

$test = Check-Port-Is-Used -port 3002

if ($test -eq $true)
{
    Write-Output "Container exists"
}
else
{
    Write-Output "Sorry :("
}



