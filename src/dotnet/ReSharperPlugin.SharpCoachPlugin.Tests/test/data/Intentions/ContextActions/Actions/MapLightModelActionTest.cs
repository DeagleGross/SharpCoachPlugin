using System;

public class MapModelsActionTestText
{
    public static UserResponse ToResponse{caret}(UserRequest request)
    {
        Console.WriteLine("hello!");
        return null;
    }
}

public class UserRequest
{
    public long Id { get; set; }
    
    public string Name { get; set; }
}

public class UserResponse
{
    public long Id { get; set; }
    
    public string Name { get; set; }
}