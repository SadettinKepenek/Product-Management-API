FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
EXPOSE 5011:5011
WORKDIR /src


COPY ProductManagement.API/ProductManagement.API.csproj ./
RUN dotnet restore


COPY ProductManagement.API/ .
RUN dotnet publish -c Release -o /app/out



FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src


WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ProductManagement.API.dll"]