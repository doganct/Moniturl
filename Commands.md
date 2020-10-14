COMMANDS

To Create Migration
dotnet ef migrations add MIGRATION_NAME -p src/Moniturl.Data -s src/Moniturl.Hosting

To Update Database
dotnet ef database update -p src/Moniturl.Data -s src/Moniturl.Hosting

Docker setup
docker run -d --restart unless-stopped --name seq -e ACCEPT_EULA=Y -v PATH:/data -p 8081:80 datalust/seq:latest