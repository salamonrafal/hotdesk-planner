# hotdesk-planner
[![.NET](https://github.com/salamonrafal/hotdesk-planner/actions/workflows/dotnet.yml/badge.svg)](https://github.com/salamonrafal/hotdesk-planner/actions/workflows/dotnet.yml)


## Unit & Integration tests

I use to test coverage NUnit test framework and FluentAssertion for both type of testing. All type of test placed inside `Tests` directory. 

### Integration tests

#### Structure of directories:

```textmate

- Test
    - Integration
        - AppData 
            : Directory contains test data for feeding Test server MongoDB
            
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
Do if some reasons can broke connection with MongoDB server or MongoDB server will be unreachable then Integration test will be work.
For those reasons tests use external package [Mongo2Go](https://github.com/Mongo2Go/Mongo2Go) 