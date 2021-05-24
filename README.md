# learn-a-ton-umbraco9
Dedicated to the weekly meetup sessions with Carole and Emma.

## Getting Started
Clone repo, open *MyCustomUmbracoSolution.sln* in Visual Studio 2019 latest with .net 5.0 (if you use a different version, you might experience a different behaviour).
Hit F5. You can enter the Umbraco back office with username `admin@test.com` and password `Test!23456`.

## Some Useful Tooling

### Swagger
Install NuGet package:
	Swashbuckle.AspNetCore

In \Startup.cs in method ConfigureServices(...) add at the end (postion is not strictly important):
```
services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
});
```

Also in \Startup.cs in method Configure(...) add app.UseSwagger();app.UseSwaggerUI(...): in the .IsDevelopment() clause like so:
```
if (env.IsDevelopment())
{
	app.UseDeveloperExceptionPage();

	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
}
```

Now you can add a new profile *swagger* to your launchSettings.json:
```
...
	"Swagger": {
		"commandName": "Project",
		"launchBrowser": true,
		"launchUrl": "swagger",
		"environmentVariables": {
			"ASPNETCORE_ENVIRONMENT": "Development"
		},
		"applicationUrl": "https://localhost:44341;http://localhost:43622"
	}
...
```
*(you might use different ports on your localhost)*

### Configuration
In appsettings.json add:
```
...
	"AllowedHosts": [ "http://localhost:8080" ],
	"MyCustomSection": {
		"Example": "Demo only"
	},
...
```

Create a new classes:
```
public class MyCustomSectionModel
{
	public string Example { get; set; }
}

public class AppConfigModel
{
	public MyCustomSectionModel MyCustomSection { get; set; }
	public IEnumerable<string> AllowedHosts { get; set; }
}
```

In \Startup.cs in method ConfigureServices(...) add at the beginning:
```
var appConfig = new AppConfigModel();
_config.Bind(appConfig);
services.Configure<AppConfigModel>(_config);
```
The first two lines make the settings in from e.g. appsettings.json, appsettings.Development.json, lauchSettings.json or user-secrets etc. within the scope of the ConfigureServices(...) method available.
You will see an example in the CORS section.
The third line registers an AppConfigModel instance in the DI container and makes it injectable wherever you need it.


### CORS
In \Startup.cs add right after `public class Startup {`:
```
readonly string CorsAllowedOrigins = "_corsAllowedOrigins";
```

Then in method ConfigureServices(...) add at the end (postion is not strictly important):
```
services.AddCors(o =>
	o.AddPolicy(name: CorsAllowedOrigins,
		builder =>
		{
			builder.WithOrigins(appConfig.AllowedHosts.ToArray());
		})
);
```
See how *appConfig.AllowedHosts* is used within the CORS setup.

And finally further down in \Startup.cs in method Configure(...) add right before `app.UseUmbraco()...`:
```
app.UseCors(CorsAllowedOrigins);
```


### Store user secret local
When you don't use SqlCE in your development environment, but a central SQL-Server where you can not login with *Integrated Security*, then you may not include user credentials in the appsettings.json.

Install NuGet package:
- Microsoft.Extensions.Configuration.UserSecrets

In Terminal (open from project context menu or Tools menu) execute e.g:

```
dotnet user-secrets init

dotnet user-secrets set ConnectionStrings:umbracoDbDSN "Server=<machineName>\<instanceName>;Database=<databaseName>;User Id=<myUsername>;Password=<myPassword>;"
```
*Don't forget to replace `<machineName>`, `<instanceName>`, `<databaseName>`, `<myUsername>` and `<myPassword>` by real values ;-)*

Alternatively you can use "environmentVariables" section of `\Properties\launchSettings.json` like this:
```
...
	"environmentVariables": {
		"ASPNETCORE_ENVIRONMENT": "Development",
		"ConnectionStrings__umbracoDbDSN": "Server=<machineName>\\<instanceName>;Database=<databaseName>;User Id=<myUsername>;Password=<myPassword>;"
	},
...
```
Another critical place for credentials you may find in the SMTP section (see appsettings.Development.json). At least *Username*, *Password* and (*maybe*) *Port* should not be stored in files which go into a repo.
This time the environment variable names in `\Properties\launchSettings.json` look like this:
```
...
	"environmentVariables": {
		"ASPNETCORE_ENVIRONMENT": "Development",
		"ConnectionStrings__umbracoDbDSN": "Server=<machineName>\\<instanceName>;Database=<databaseName>;User Id=<myUsername>;Password=<myPassword>;",
		"Umbraco__CMS__Global__Smtp__Username": "<username>",
		"Umbraco__CMS__Global__Smtp__Password": "<password>",
		"Umbraco__CMS__Global__Smtp__Port": "587",
		"Umbraco__CMS__Global__Smtp__EnableSsl": "true"
	},
...
```
*Attention! compared to the appsettings.json notation all values have to be strings e.g.: Smtp *Port* or *EnableSsl*

From my point of view the launchSettings.json approche needs less effort and 
when you use the default .gitignore for Visual Studio Solutions `**/Properties/launchSettings.json` is excluded from repositoy checkin 
and thus save enough for development credentials.

Be aware of the slightly different syntax:
- user-secrets:

	`:` is used to separate section name and property

- lauchSettings.json:

	`__` (double underscore) is used to separate section name and property

	and you have to escape `\` in the server name: `"Server=MACHINENAME\\SQL-SERVER-INSTANCENAME;Database... `
	
Btw.: Emma Garland has explained the configuration option in much more details in her nice article 2020 24Days: 

https://24days.in/umbraco-cms/2020/umbraco-dotnet-core-config/

And Warren Buckley has prepared a video about configuration too:

https://www.youtube.com/watch?v=AOFdQAODU5o


