using System;

public class MapNumericToEnumActionTest
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public int AgeType { get; set; }
}

public class UserResponse
{
    public Age AgeType { get; set; }
    
    public enum Age
    {
        Young,
        Middle,
        Old
    }
}