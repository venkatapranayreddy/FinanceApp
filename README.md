Install from nugetgallery
1. Microsoft.extensions.identity.core
2. microsoft.aspnetcore.idenity.entityframework
3. microsoft.aspnetcore.authentication.jwtbearer
4. microsoft.entityframework.core
5.  microsoft.entityframework.tools
6. microsoft.entityframework.sqlserver

(to create the database)
dotnet ef migrations add <yourverisonwords>
dotnet ef database update
-----------------------
(to run the code)
dotnet build
dotnet watch run

Use swagger for checking API's
