FROM mcr.microsoft.com/dotnet/core/sdk:2.2-bionic AS build
WORKDIR /app

COPY . ./
RUN dotnet publish ProjectNamakli.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-bionic
WORKDIR /app
COPY --from=build /app/ProjectNamakli.Api/out .
ENTRYPOINT ["dotnet", "ProjectNamakli.Api.dll"]