FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build-env
WORKDIR /app

COPY ./*sln ./

COPY ./src/OpenFTTH.DesktopBridge/*.csproj ./src/OpenFTTH.DesktopBridge/
COPY ./test/OpenFTTH.DesktopBridge.Tests/*.csproj ./test/OpenFTTH.DesktopBridge.Tests/

RUN dotnet restore --packages ./packages

COPY . ./
WORKDIR /app/src/OpenFTTH.DesktopBridge
RUN dotnet publish -c Release -o out --packages ./packages

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:10.0-alpine
WORKDIR /app

COPY --from=build-env --chown=app:app /app/src/OpenFTTH.DesktopBridge/out .
EXPOSE 5000
USER app
ENTRYPOINT ["dotnet", "OpenFTTH.DesktopBridge.dll"]
