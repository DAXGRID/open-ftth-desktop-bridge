FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./*sln ./

COPY ./src/OpenFTTH.DesktopBridge/*.csproj ./src/OpenFTTH.DesktopBridge/
COPY ./test/OpenFTTH.DesktopBridge.Tests/*.csproj ./test/OpenFTTH.DesktopBridge.Tests/

RUN dotnet restore --packages ./packages

COPY . ./
WORKDIR /app/src/OpenFTTH.DesktopBridge
RUN dotnet publish -c Release -o out --packages ./packages

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /app

COPY --from=build-env /app/src/OpenFTTH.DesktopBridge/out .
ENTRYPOINT ["dotnet", "OpenFTTH.DesktopBridge.dll"]

EXPOSE 80 443
