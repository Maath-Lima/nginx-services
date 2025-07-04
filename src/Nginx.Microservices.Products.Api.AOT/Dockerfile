FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.21 AS build

WORKDIR /app

RUN apk add --no-cache clang build-base zlib-dev

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine3.21
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["./Nginx.Microservices.Products.Api.AOT"]