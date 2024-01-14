set -e

version=$1
key=$2

projects=("Task" "Data" "Http" "Config")

for project in ${projects[@]}
do
	echo @version
	sed -i -r "s/<Version>.*<\/Version>/<Version>$version<\/Version>/" "src\H.Tools.$project\H.Tools.$project.csproj"
done

dotnet build -c Release

for project in ${projects[@]}
do
	dotnet nuget push "src\H.Tools.$project\bin\Release\H.Tools.$project.$version.nupkg" --api-key $key --source https://api.nuget.org/v3/index.json
done
