Store user secret local
--------------------------------------------------------------------------------------------
Install NuGet package:
	Microsoft.Extensions.Configuration.UserSecrets

In Terminal (open from project context menu or Tools menu) execute e.g.:
	dotnet user-secrets init

	dotnet user-secrets set ConnectionStrings:umbracoDbDSN "Server=.\SQL2017;Database=umb-v9-beta2;Integrated Security=true"

Alternatively you can use "environmentVariables" section of \Properties\launchSettings.json like this:
...
	"environmentVariables": {
		"ASPNETCORE_ENVIRONMENT": "Development",
		"ConnectionStrings__umbracoDbDSN": "Server=.\\SQL2017;Database=umb-v9-beta2;Integrated Security=true;"
	},
...

From my point of view the launchSettings.json approche needs less effort and 
when you use the default .gitignore for Visual Studio Solutions **/Properties/launchSettings.json is excluded from repositoy checkin 
and thus save enough for development credentials.
Be aware of the slightly different syntax:
	- user-secrets:
		: is used to separate section name and property

	- lauchSettings.json:
		__ (double underscore) is used to separate section name and property

		and you have to escape \ in the server name: "Server=MACHINENAME\\SQL-SERVER-INSTANCENAME;Database...


Swagger
--------------------------------------------------------------------------------------------
Install NuGet package:
	Swashbuckle.AspNetCore

In \Startup.cs in method ConfigureServices(...) add at the end (postion is not stricly important):
	services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
	});

Also in \Startup.cs in method Configure(...) add app.UseSwagger();app.UseSwaggerUI(...): in the .IsDevelopment() clause like so:
	if (env.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();

		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
	}

Now you can add to your launch profile

CORS
--------------------------------------------------------------------------------------------
	services.AddCors(o =>
		o.AddPolicy(name: CorsAllowedOrigins,
			builder =>
			{
				builder.WithOrigins(appConfig.AllowedHosts.ToArray());
			})
	);