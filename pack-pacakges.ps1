$packages = ".\new-packages"

if(Test-Path -Path $packages ){
	Remove-Item –path $packages –recurse
	Write-Host "Deleting all files from [${packages}]"
}

$projects = ".\src\NServiceBus.Mandrill" 

Foreach ($i in $projects) {
    nuget pack $i -OutputDirectory $packages
}

Write-Host "********************************"
Write-Host "CREATED PACKAGES:"
Write-Host "********************************"
$nupacks = Get-ChildItem $packages

for ($i=0; $i -lt $nupacks.Count; $i++) {
    $outfile = $nupacks[$i].Name
	Write-Host $outfile
}
Write-Host "********************************"