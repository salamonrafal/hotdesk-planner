# hotdesk-planner
[![.NET](https://github.com/salamonrafal/hotdesk-planner/actions/workflows/dotnet.yml/badge.svg)](https://github.com/salamonrafal/hotdesk-planner/actions/workflows/dotnet.yml) ![GitHub last commit](https://img.shields.io/github/last-commit/salamonrafal/hotdesk-planner?label=Last%20commit) ![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/salamonrafal/hotdesk-planner)

* [Unit & Integration tests](#Unit--Integration-tests)
    * [Integration tests](#Integration-tests)
        * [Structure of directories](#Structure-of-directories)
        * [Mocking MongoDB](#Mocking-MongoDB)
        * [Code Coverage](#Code-Coverage)
    * [Unit tests](#Unit-tests)
        * [Structure of directories](#Structure-of-directories)
* [Model validation](#Model-validation)
  * [Validation Types](#Validation-Types)
* [Docker image](#Docker-image)
  * [Manual create docker image](#Manual-create-docker-image)
* [Manual create docker container](#Manual-create-docker-container)
  * [Scripts to create docker images & containers](#Scripts-to-create-docker-images--containers)
    * [Script build image](#Script-build-image)
    * [Script create container](#Script-create-container)

## Unit & Integration tests

I use to test coverage NUnit test framework and FluentAssertion for both type of testing. All type of test placed inside `Tests` directory. 

### Integration tests

#### Structure of directories:

```textmate
- Test
    - Integration
        - AppData 
            : Directory contains test data for feeding test server MongoDB
            
        - ApplicationFactories 
            : Directory contains object for create  test server application
            
        - Fixtures
            : Directory contains test fixtures 
            
        - Helpers
            : Directory contains tools and objects use in test
            
        - Tests
            : Directory contains test cases
            
```

#### Mocking MongoDB 

Integration tests are free  of network dependencies. 
For example if some reasons can broke connection with MongoDB server or MongoDB server will be unreachable then Integration test will be work.
For those reasons tests use external package [Mongo2Go](https://github.com/Mongo2Go/Mongo2Go) 

#### Code Coverage

For calculate code coverage was used [Coverlet](https://github.com/coverlet-coverage/coverlet#coverlet) and for generating report human readable was used [ReportGenerator](https://github.com/danielpalme/ReportGenerator#reportgenerator).
For create coverage report you have to use script:

**For Windows platform**
```cmd
C:repos\hotdesk-planner> .\scripts\win\code-coverage.ps1
```

**Reports**
Script generate two versions of code coverage: HTML and XML. Both versions placed inside `./.coverage/reports/` directory.

### Unit tests

#### Structure of directories:

```textmate
- Test
	- Unit	
        - Api
            : Directory contains test cases for project Api
            
        - Core
            :  Directory contains test cases for project Core
            
        - Infrastructure
            :  Directory contains test cases for project Infrastructure
            
        - Helpers
            : Directory contains tools and objects use in test
            
        - AppData
            : Directory contains test data for feeding tests
            
```

## Model validation

Service use `FluentValidation` for validating data models before run any action with repository.
All Validators you find in `Core.Validators`. Any information about external package for validating use in project you able to read: [https://docs.fluentvalidation.net/en/latest/index.html](https://docs.fluentvalidation.net/en/latest/index.html)

### Validation Types

Service has defined rules of set depends what action you need  to execute:

* _Core.Enums.ValidationModelType.GetOne_
* _Core.Enums.ValidationModelType.Insert_
* _Core.Enums.ValidationModelType.Update_
* _Core.Enums.ValidationModelType.Delete_

## Docker image

### Manual create docker image
You able to create manual docker image with service from source. Below you find command with description

```shell
docker build -t helpdesk-service/dev \
  --build-arg SERVICE_BUILD_PLAN=Debug \
  --build-arg SERVICE_PORT=3002 \
  --build-arg SERVICE_ENV=Development \
  --build-arg SERVICE_URL=http://+:3002 .
```

__Build arguments__
* SERVICE_BUILD_PLAN _You can choose between ___Debug___ or ___Release____
* SERVICE_PORT _Define for which port service will be available_
* SERVICE_ENV _Define is it ___Production___ or ___Development___ environment_
* SERVICE_URL _Define for which url service should listening_

### Manual create docker container
You able create manual docker container with service. Below you will find command:

```shell
docker run -d -p 3002:3002 --name service-name helpdesk-service/dev
```
### Scripts to create docker images & containers
#### Script build image

**For windows:** ![Win](data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/PjwhRE9DVFlQRSBzdmcgIFBVQkxJQyAnLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4nICAnaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkJz48c3ZnIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDUwIDUwIiBpZD0iTGF5ZXJfMSIgdmVyc2lvbj0iMS4xIiB2aWV3Qm94PSIwIDAgNTAgNTAiIHhtbDpzcGFjZT0icHJlc2VydmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiPjxwYXRoIGQ9Ik0xMS45LDM0LjdsOC44LDEuM1YyNS45aC04LjhWMzQuN3ogTTExLjksMjQuMWg4LjhWMTMuOWwtOC44LDEuOFYyNC4xeiBNMjIuMywzNi41bDExLjgsMS44ICBWMjUuOUgyMi4zVjM2LjV6IE0yMi4zLDEzLjV2MTAuNmgxMS44VjExLjdMMjIuMywxMy41eiIgZmlsbD0iIzBDQjNFRSIgaWQ9IldpbjhfMl8iLz48cGF0aCBkPSJNMjUsMUMxMS43LDEsMSwxMS43LDEsMjVzMTAuNywyNCwyNCwyNHMyNC0xMC43LDI0LTI0UzM4LjMsMSwyNSwxeiBNMjUsNDRDMTQuNSw0NCw2LDM1LjUsNiwyNVMxNC41LDYsMjUsNiAgczE5LDguNSwxOSwxOVMzNS41LDQ0LDI1LDQ0eiIgZmlsbD0iIzBDQjNFRSIvPjwvc3ZnPg==)
```powershell
./scripts/win/build-image.ps1 [--env=[Development|Production]] [--port=[0-9+]] [--args=[*]]
```

* _--env_ - Define environment. **Default: _Production_**
* _--port_ - Define port for which service should listening. **Default: _3000_**
* _--args_  - Define additional arguments.

#### Script create container

**For windows:** ![Win](data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/PjwhRE9DVFlQRSBzdmcgIFBVQkxJQyAnLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4nICAnaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkJz48c3ZnIGVuYWJsZS1iYWNrZ3JvdW5kPSJuZXcgMCAwIDUwIDUwIiBpZD0iTGF5ZXJfMSIgdmVyc2lvbj0iMS4xIiB2aWV3Qm94PSIwIDAgNTAgNTAiIHhtbDpzcGFjZT0icHJlc2VydmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiPjxwYXRoIGQ9Ik0xMS45LDM0LjdsOC44LDEuM1YyNS45aC04LjhWMzQuN3ogTTExLjksMjQuMWg4LjhWMTMuOWwtOC44LDEuOFYyNC4xeiBNMjIuMywzNi41bDExLjgsMS44ICBWMjUuOUgyMi4zVjM2LjV6IE0yMi4zLDEzLjV2MTAuNmgxMS44VjExLjdMMjIuMywxMy41eiIgZmlsbD0iIzBDQjNFRSIgaWQ9IldpbjhfMl8iLz48cGF0aCBkPSJNMjUsMUMxMS43LDEsMSwxMS43LDEsMjVzMTAuNywyNCwyNCwyNHMyNC0xMC43LDI0LTI0UzM4LjMsMSwyNSwxeiBNMjUsNDRDMTQuNSw0NCw2LDM1LjUsNiwyNVMxNC41LDYsMjUsNiAgczE5LDguNSwxOSwxOVMzNS41LDQ0LDI1LDQ0eiIgZmlsbD0iIzBDQjNFRSIvPjwvc3ZnPg==)
```powershell
./scripts/win/create-image.ps1
```