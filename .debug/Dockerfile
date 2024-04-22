
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./", "./"]
COPY . /app/debug

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS debug
WORKDIR /app
COPY --from=build /app/debug .

CMD ["sleep", "infinity"]

# builder.WebHost.UseUrls("http://*:5031");