
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./", "./"]
COPY . /app/debug

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS debug
WORKDIR /app
EXPOSE 5031
COPY --from=build /app/debug .

CMD ["sleep", "infinity"]