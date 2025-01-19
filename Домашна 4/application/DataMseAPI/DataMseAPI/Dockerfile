FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DataMseAPI.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY techincal_analysis.exe techincal_analysis.exe
RUN apt-get update && apt-get install -y curl
EXPOSE 80
ENTRYPOINT ["dotnet", "DataMseAPI.dll"]