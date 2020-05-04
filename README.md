```bash
# build all services
docker-compose build

# run all services
docker-compose up -d

# stop all and remove volumes
docker-compose down -v

# run e2e tests (make sure to rebuild changed services)
dotnet restore e2e-tests/e2e-tests.fsproj
dotnet run -p e2e-tests/e2e-tests.fsproj

# other compose commands: see docker-compose documentation
```