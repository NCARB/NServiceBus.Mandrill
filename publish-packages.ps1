param (
    [Parameter(Mandatory=$true)][string]$source,
    [Parameter(Mandatory=$true)][string]$apiToken
 )

$packages = ".\new-packages"

Write-Host "********************************"
Write-Host "Publishing ALL PACKAGES:"
Write-Host "********************************"
$nupacks = Get-ChildItem $packages

for ($i=0; $i -lt $nupacks.Count; $i++) {
    $outfile = $nupacks[$i].Name
	Write-Host "Publishing <${outfile}> to [${source}]"
	$pathtofile = Join-Path -Path $packages -ChildPath $outfile
	nuget push $pathtofile $apiToken -Source $source
	
}
Write-Host "********************************"