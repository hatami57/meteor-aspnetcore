## Meteor ASP.NET Core Library

This library has `Meteor` library as a base and implements some `aspcore` related helpers and utilities like:

## `BaseController` class
A base class that contains mostly needed things in a controller class.

## Attributes | Action Filters

- `OperationResult`: An attribute that automatically wrap action results as an `OperationResult`
- `AssignUserActionFilter`: Auto assign `HttpContext.User` object to any action argument which is implemented `INeedUser` interface
- `IgnoreOperationResult`: An action attribute that cause ignoring wrapping its output as an `OperationResult` object (when the whole controller has `OperationResult` attribute)

## Database Messages

Implementing a new base class for message plus some default database message operations which contain user object (`INeedUser`):

- `MessageByUserAsync`
- `DbMessageByUserAsync`
- `DbDefaultSelectByUserAsync`
- `DbDefaultInsertByUserAsync`
- `DbDefaultUpdateByUserAsync`
- `DbDefaultDeleteByUserAsync`
- etc.

## Model Binders

There are some automatic model binders for useful objects like `LocalDate` class in `NodaTime` library, that can be used in controller actions.

## Extensions

For `MvcOptions`:
- `AddMeteorModelBinder()`
- `AddMeteorFilters()`

For `IMvcBuilder`:
- `AddMeteorJsonConverters()`

For `IServiceCollection`:
- `AddMeteorJwtAuthentication()`

## And Some Other Features

That needed to be documented here...
