using System;

public class MapEnumToNumericActionTest
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
    public long AgeType { get; set; }
}