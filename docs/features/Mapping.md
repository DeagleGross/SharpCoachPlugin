# Generate Mapping Code for user types

Imagine you are developing a WebApi, and there is an endpoint `POST /users`, 
which creates another User.

And there are 2 different classes: 
`UserCreateRequest` (input in controller handler) 
and `UserCreateResponse` (output of controller handler)

Here are definitions of these models
```csharp
public class UserCreateRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
```

```csharp
public class UserCreateResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
```

You obviously need some kind of method to convert one model to another:
```csharp
public static UserCreateResponse ConvertUserCreate(UserCreateRequest r) => ...
```

You can use plugin action to generate implementation of such a method.
Contract requires to define a method with:
- response type
- name
- single parameter with name

After that you can press Alt+Enter (or any other your personal binding) to
open interactive menu and find `Map internals of models` action.
Click it, and method body will be generated automatically