dotnet build -c Release

varsion=$1
key=$2

dotnet nuget push "src\H.Tools.Task\bin\Release\H.Tools.Task.$version.nupkg" --api-key $key --source https://api.nuget.org/v3/index.json
dotnet nuget push "src\H.Tools.Task\bin\Release\H.Tools.Data.$version.nupkg" --api-key $key --source https://api.nuget.org/v3/index.json
dotnet nuget push "src\H.Tools.Http\bin\Release\H.Tools.Http.$version.nupkg" --api-key $key --source https://api.nuget.org/v3/index.json
dotnet nuget push "src\H.Tools.Config\bin\Release\H.Tools.Config.$version.nupkg" --api-key $$key --source https://api.nuget.org/v3/index.json
