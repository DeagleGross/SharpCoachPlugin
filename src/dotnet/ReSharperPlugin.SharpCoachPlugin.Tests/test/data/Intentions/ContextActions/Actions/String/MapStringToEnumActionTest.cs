using System;

public class MapStringToEnumActionTest
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public string AgeType { get; set; }
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