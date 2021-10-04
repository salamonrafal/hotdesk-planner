

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