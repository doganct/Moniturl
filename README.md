Moniturl | Monitoring Target Application

Setup
1. Clone project.

2. Change settings in appsettings.json file. (Connection string, mail smtp info and seq url)

3. To create database with ef, you should call dotnet ef database update -p src/Moniturl.Data -s src/Moniturl.Hosting in project root folder.

4. Project need seq for logging. Therefore you should install seq local machine or run docker image and change appsettings.json server url section.

Docker command :
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest


Application Third Party Dependencies
1. Seq
2. Hangfire
3. Serilog
4. Automapper
5. Jquery
6. Bootstrap
7. MailKit


Application use entity framework core, repository pattern and specification pattern for data manipulation and queries.



