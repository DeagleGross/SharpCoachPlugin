using System;

public class MapEnumToStringActionTest
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public Age AgeType { get; set; }
    
    public enum Age
    {
        Young,
        Middle,
        Old
    }
}

public class UserResponse
{
    public string AgeType { get; set; }
}