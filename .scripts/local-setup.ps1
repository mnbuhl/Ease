function Remove-EaseTemplatesNupkg {
    foreach ($file in Get-ChildItem -Path . -Filter "Ease.Templates.*.nupkg" -Recurse) {
        Remove-Item $file.FullName
    }
}

function Perform-Cleanup {
    Remove-EaseTemplatesNupkg
    dotnet new uninstall .\
    dotnet new uninstall "Ease.Templates"
}

try {
    Perform-Cleanup
    dotnet build -c Release
    nuget pack Ease.nuspec -NoDefaultExcludes
    dotnet new install (Get-ChildItem -Path .\ -Filter "Ease.Templates.*.nupkg" | Select-Object -First 1).Name
    Remove-EaseTemplatesNupkg
} catch {
    Write-Host "An error occurred: $_"
    Perform-Cleanup
    exit 1
}