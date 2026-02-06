FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY CA_Presentation/CA_Presentation.csproj CA_Presentation/
COPY CA_Application/CA_Application.csproj CA_Application/
COPY CA_Domain/CA_Domain.csproj CA_Domain/
COPY CA_Infrastructure/CA_Infrastructure.csproj CA_Infrastructure/

RUN dotnet restore CA_Presentation/CA_Presentation.csproj

COPY . .

RUN dotnet publish CA_Presentation/CA_Presentation.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS run

WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish /app/

ENTRYPOINT ["dotnet", "CA_Presentation.dll"]