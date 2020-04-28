FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.11 as builder

ADD . /app
WORKDIR /app

RUN dotnet publish -c Release -r linux-musl-x64

FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1.3-alpine3.11

COPY --from=builder /app/bin/Release/netcoreapp3.1/linux-musl-x64/publish /app

EXPOSE 8080

CMD ["/app/todo-api"]
