#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./product.api/product.api.csproj" --disable-parallel
RUN dotnet publish "./product.api/product.api.csproj" -c Release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./connection.json /app
COPY --from=build /app ./
EXPOSE 6000
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "product.api.dll"]
