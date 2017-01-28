pushd $PSScriptRoot/../bin/Release
$nupkg = ls | where { $_.Extension -eq ".nupkg" } | select -First 1 | foreach { $_.Name }
nuget push $nupkg $env:NugetApiKey -Source https://www.nuget.org/api/v2/package
popd