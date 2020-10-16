COMMANDS

To Create Migration
dotnet ef migrations add MIGRATION_NAME -p src/Moniturl.Data -s src/Moniturl.Hosting

To Update Database
dotnet ef database update -p src/Moniturl.Data -s src/Moniturl.Hosting

Docker setup
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest