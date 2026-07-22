# Mind Trail

The Mind Trail is a web application that captures and organizes thoughts and ideas as interconnected cards. 
Each card can reference other cards, as well as external sources like books and publications.

## Requirements

* .NET 10 SDK

## Getting started

To build the application, go to the root folder of the application and run the `dotnet build` command.

To run the unit tests, go to the root folder of the application and run the `dotnet tests` command.

To run the application, go to the `src/MindTrail.WebHost` folder of the application
and run the `dotnet run` command.

ℹ️ The [appsettings.json](src/MindTrail.WebHost/appsettings.json) file 
already contains the minimal working configuration to run the application on a local machine.

ℹ️ To automatically create (and update) a database when launching the application,
you need to set the `EfCore:ApplyMigrationsAutomatically` parameter
in the [appsettings.json](src/MindTrail.WebHost/appsettings.json) file.

## External dependencies

There are no external dependencies yet.

## Logging

[NLog](https://nlog-project.org/) is used as a logger provider.

NLog settings are located in the [appsettings.json](src/MindTrail.WebHost/appsettings.json) file,
in the `LoggingFeatures:NLog` section.

By default, the [NLog.config](src/MindTrail.WebHost/NLog.config) file is used for the NLog configuration.

It is also possible to use the [NLog.loki.config](src/MindTrail.WebHost/NLog.loki.config) file
when the Loki server is used for logging.

## Docker

ℹ️ Ensure that you have [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed.

### Building an image

To build an image locally from a Dockerfile, launch the Docker Desktop, go to
the `src/MindTrail.WebHost` folder and run the script [docker-build.bat](src/MindTrail.WebHost/docker-build.bat).

If necessary, specify the relevant value of the version number:

```
.\docker-build.bat 1.0.0.1
```

If you omit the version, it will insert `1.0.0.0`.

### Running a container

The [mind-trail.env](src/MindTrail.WebHost/mind-trail.env) file is used to configure the container.
It is based on the [appsettings.json](src/MindTrail.WebHost/appsettings.json) file.

To create and run a new container locally from an image, launch the Docker Desktop, 
go to the `src/MindTrail.WebHost` folder of the application and
run the script [docker-run.bat](src/MindTrail.WebHost/docker-run.bat).

If necessary, specify the launch port and a specific configuration file (.env):

```
.\docker-run.bat 12345 my-congig-file.env
```

By default, port `12345` and configuration 
file [mind-trail.env](src/MindTrail.WebHost/mind-trail.env) are used.
Therefore, by default, the URL of the running application will be as follows:
http://localhost:12345/swagger.