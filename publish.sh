set -e

version=$1
key=$2

if [ -z "$2" ]; then
  echo "error params"
  exit 1
fi

single=$3
projects=("Task" "Data" "Http" "Config" "EntityFramework" "Sqlite" "Wpf" "Asp")
if [ "$single" != "" ]; then
	projects=("$single")
fi

for project in ${projects[@]}
do
	sed -i -r "s/<Version>.*<\/Version>/<Version>$version<\/Version>/" "src\H.Tools.$project\H.Tools.$project.csproj"
	sed -i -r "s/<AssemblyVersion>.*<\/AssemblyVersion>/<AssemblyVersion>$version<\/AssemblyVersion>/" "src\H.Tools.$project\H.Tools.$project.csproj"
done

dotnet build -c Release

for project in ${projects[@]}
do
	dotnet nuget push "src\H.Tools.$project\bin\Release\H.Tools.$project.$version.nupkg" --api-key $key --source https://api.nuget.org/v3/index.json
done

git add -A
git commit -m "chore v$version"