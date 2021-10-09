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

**For windows:** <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAABS1JREFUWEfFl1tsVFUUhv+1zyC9zEw7pwVDRaBAxUtiIiqiKBUl3NqZci1EeTC+ie8YCcEaIw8Y3zT6oDHxBlYsdqYgEWIRFRAriTHRBBTkZhU6Z9p6ZoB0Zv/mnLl02k5pG9t4kkkmZ6+19rfXba8jGOvTTMM/ubc6ZSTnCOB31An0GinP7703/OfQKKmxmJRRCTfzFm+xtQaQTSSWiKCskB7BbtFoh2CPXWXuwwPSN5L9mwOQ4t1vrdeUnQqYO5Kx/HUhz1Nhp91hvoMm0cPpDgtQfCA63UjJpwAWFlZmHyhX3DXhVEAmFZLTwDFCN14LVl4utF4QwL+/6yGt1T4A03JKJKHUYZAtSSVfXF9VfgEidNdJKTrQPcOjuRIia6H1Uojk2/5TQa/pDVaeHAwxBCCzeTuA4jzhdk39QiJU+cNowuCPdC1IAbsEqjZP/pqmrh1sYwBASevVKiXGSQhuy5yMhOyKBwMv5k47GoKMV3z7Y1tJ7gREZdQ6UwYXXFtVcSlrph/ASbi22LH+mFOTsjkeMnePas8OTvJd7poN5blLA/54MPC+o1casZ4S8sNsSLTgu0Rd4LHsgXIAriDwUX/Msd0Oma8O2byJCvUwfJ2xFRAs1tTzQLlTgGqIeDLyP9tB896srjcc2wHhy3n5tNEOVTS7+eu+dOq8yPoVIrMzQu12feBJh9IJi6GM+wm6P0AeFqp1EG4DsGwY7wwAcJK0NGIdEZHFjrwGfktMC9zt9AkXwNtmbQKRdjVJBTzSG6o44QvHnqHwvSGbUB4fEwAAJzE15UR/dXCDHazYmwX4HERDZqMv7aC53Pnva4s+S8q74wHgHjQSOwRwqesFsiURqlgnaKZRWhSL5rXX5+yg+faEAIStLRC8mXF0LH7KrBR/pKdGI3U6e8okMfN6yLwwEQDl+2Kzkh6ey+5lGGqulLZFlwvlYPol++wfzaJs7x7vEKCJyjvfupGtFoLLxRuONkLkkwzVRTtozsgS+lqvzqMxoJu5S9qjI+gz7lPC6QWrgLCcBCu05m21LkIhrSdsHAigccluMG+fUICwdSnXaR2A/zUEmsvE39ozV6vUmVwSKpl1vS5wfiKSsCzcXZ0SfTaXhKLmpMuw2OoSSHk6Lthi15tvTQSAN2I9D+CNTBla8VPmlHQjao22QMmaNIAcsusDbosd7yrwRqKHAXnSTWQtexMNgQ1pgPxKICmCRf8EK46PJ4A7Z6TkeK4Vk+vtUMVn6cuog5NKOmO/ZOc+Qn8dr69YUhq+MlXEUwtIjRB3aME8gDUCtXZMdwEpJW2xowp4NOP+M/Eq857cZeR6IRLdCMieXO2K7LDrA68UrnOKry1WP+rrOBJtAuSlnK3M6dMpl33SlN86N6H7im4sNseD5scFIQa/HH4geVrID7Kup+Y38ZBZO2QgceyVHLw6Td0wTuY6lQPhjGSnAttuNloP66VCIxn0g/kT8tCh1Lm3oY7kD6UkjxoGt/bWVX4/Gm/4w9GFSZHXsjHP6CS0VrWJhvKOfBuFx/I0hDOWV+WFiBB8BUqLJyUHun8qv5DzShNV2fyemSnolRCuBfHEgLGcuKypVg/efGAODDqaM4rBMJoVsWgYFycB+Tu9xlvz5sEB4k7MOVlvTKyY0lnIzojfhr6IFdTk6yJSMxr353Jay2kx9Ha7ztx7s5F+RIBsn/D+Za3WGpsEzsepBArBkLQoql1R77arzPB//zgttEszjTJvz6ykTlYL0iAEYx54zvZ0lP0x1mr5F+v+ztGlgywzAAAAAElFTkSuQmCC" alt="Win" />
```powershell
./scripts/win/build-image.ps1 [--env=[Development|Production]] [--port=[0-9+]] [--args=[*]]
```

* _--env_ - Define environment. **Default: _Production_**
* _--port_ - Define port for which service should listening. **Default: _3000_**
* _--args_  - Define additional arguments.

#### Script create container

**For windows:** <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAABS1JREFUWEfFl1tsVFUUhv+1zyC9zEw7pwVDRaBAxUtiIiqiKBUl3NqZci1EeTC+ie8YCcEaIw8Y3zT6oDHxBlYsdqYgEWIRFRAriTHRBBTkZhU6Z9p6ZoB0Zv/mnLl02k5pG9t4kkkmZ6+19rfXba8jGOvTTMM/ubc6ZSTnCOB31An0GinP7703/OfQKKmxmJRRCTfzFm+xtQaQTSSWiKCskB7BbtFoh2CPXWXuwwPSN5L9mwOQ4t1vrdeUnQqYO5Kx/HUhz1Nhp91hvoMm0cPpDgtQfCA63UjJpwAWFlZmHyhX3DXhVEAmFZLTwDFCN14LVl4utF4QwL+/6yGt1T4A03JKJKHUYZAtSSVfXF9VfgEidNdJKTrQPcOjuRIia6H1Uojk2/5TQa/pDVaeHAwxBCCzeTuA4jzhdk39QiJU+cNowuCPdC1IAbsEqjZP/pqmrh1sYwBASevVKiXGSQhuy5yMhOyKBwMv5k47GoKMV3z7Y1tJ7gREZdQ6UwYXXFtVcSlrph/ASbi22LH+mFOTsjkeMnePas8OTvJd7poN5blLA/54MPC+o1casZ4S8sNsSLTgu0Rd4LHsgXIAriDwUX/Msd0Oma8O2byJCvUwfJ2xFRAs1tTzQLlTgGqIeDLyP9tB896srjcc2wHhy3n5tNEOVTS7+eu+dOq8yPoVIrMzQu12feBJh9IJi6GM+wm6P0AeFqp1EG4DsGwY7wwAcJK0NGIdEZHFjrwGfktMC9zt9AkXwNtmbQKRdjVJBTzSG6o44QvHnqHwvSGbUB4fEwAAJzE15UR/dXCDHazYmwX4HERDZqMv7aC53Pnva4s+S8q74wHgHjQSOwRwqesFsiURqlgnaKZRWhSL5rXX5+yg+faEAIStLRC8mXF0LH7KrBR/pKdGI3U6e8okMfN6yLwwEQDl+2Kzkh6ey+5lGGqulLZFlwvlYPol++wfzaJs7x7vEKCJyjvfupGtFoLLxRuONkLkkwzVRTtozsgS+lqvzqMxoJu5S9qjI+gz7lPC6QWrgLCcBCu05m21LkIhrSdsHAigccluMG+fUICwdSnXaR2A/zUEmsvE39ozV6vUmVwSKpl1vS5wfiKSsCzcXZ0SfTaXhKLmpMuw2OoSSHk6Lthi15tvTQSAN2I9D+CNTBla8VPmlHQjao22QMmaNIAcsusDbosd7yrwRqKHAXnSTWQtexMNgQ1pgPxKICmCRf8EK46PJ4A7Z6TkeK4Vk+vtUMVn6cuog5NKOmO/ZOc+Qn8dr69YUhq+MlXEUwtIjRB3aME8gDUCtXZMdwEpJW2xowp4NOP+M/Eq857cZeR6IRLdCMieXO2K7LDrA68UrnOKry1WP+rrOBJtAuSlnK3M6dMpl33SlN86N6H7im4sNseD5scFIQa/HH4geVrID7Kup+Y38ZBZO2QgceyVHLw6Td0wTuY6lQPhjGSnAttuNloP66VCIxn0g/kT8tCh1Lm3oY7kD6UkjxoGt/bWVX4/Gm/4w9GFSZHXsjHP6CS0VrWJhvKOfBuFx/I0hDOWV+WFiBB8BUqLJyUHun8qv5DzShNV2fyemSnolRCuBfHEgLGcuKypVg/efGAODDqaM4rBMJoVsWgYFycB+Tu9xlvz5sEB4k7MOVlvTKyY0lnIzojfhr6IFdTk6yJSMxr353Jay2kx9Ha7ztx7s5F+RIBsn/D+Za3WGpsEzsepBArBkLQoql1R77arzPB//zgttEszjTJvz6ykTlYL0iAEYx54zvZ0lP0x1mr5F+v+ztGlgywzAAAAAElFTkSuQmCC" alt="Win" />
```powershell
./scripts/win/create-image.ps1
```